using System;
using System.Collections.Generic;

using ShiftCo.ifmo_ca_lab_3.Evaluation;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Tony;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Tony.AInterfaces;
using ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Lexington;

using Terminal = ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Lexington.TokenType;

namespace ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Parseltongue
{
    public static class Parser
    {
        private static List<Token> _tokens;
        private static Token _token;
        private static int _tokensIterator;
        private static Stack<int> _tokensIteratorStack;

        public static IExpression Parse(List<Token> tokens)
        {
            Prepare(tokens);
            var result = GetSymbol(NonTerminal.Root);
            if (result.success)
            {
                // TODO
                return ((List<IExpression>)result.value)[0];
            }
            else
            {
                throw new Exception("Parse error: No suitable parse tree.");
            }
        }

        private static void Prepare(List<Token> tokens)
        {
            _tokens = tokens;
            _tokensIterator = 0;
            _tokensIteratorStack = new Stack<int>();
        }

        private static Result GetSymbol(object symbol)
        {
            Result result;
            if (symbol is Terminal)
            {
                NextToken();
                result = GetTerminal((Terminal)symbol);
            }
            else
            {
                result = GetNonTerminal((NonTerminal)symbol);
            }
            if (!result.success || !(symbol is NonTerminal.Expression))
            {
                return result;
            }
            return new Result(true, BuildExpression((List<IExpression>)result.value));
        }

        private static Result GetTerminal(Terminal requestedSymbol)
        {
            if (_token.Type != requestedSymbol)
            {
                return new Result(false);
            }

            switch (requestedSymbol)
            {
                case Terminal.Symbol:
                    return new Result(true, new Expression("symbol", _token.Content));
                case Terminal.Number:
                    return new Result(true, new Value(Convert.ToInt32(_token.Content)));
                default:
                    return new Result(true);
            }
        }

        private static Result GetNonTerminal(NonTerminal requestedSymbol)
        {
            var productions = Grammar.Rules[requestedSymbol];
            foreach (var production in productions)
            {
                SavePosition();
                var productionMatches = true;
                var correspondingObjects = new List<IExpression>();
                foreach (var symbol in production)
                {
                    var result = GetSymbol(symbol);
                    if (!result.success)
                    {
                        productionMatches = false;
                        break;
                    }

                    switch (result.value)
                    {
                        case null:
                            continue;
                        case List<IExpression> list:
                            correspondingObjects.AddRange(list);
                            break;
                        default:
                            correspondingObjects.Add((IExpression)result.value);
                            break;
                    }
                }

                if (productionMatches)
                {
                    return new Result(true, correspondingObjects);
                }

                RestorePosition();
            }

            return new Result(false);
        }

        private static Expression BuildExpression(List<IExpression> objectsList)
        {
            var head = (string)objectsList[0].Key;
            var operands = objectsList.GetRange(1, objectsList.Count - 1);
            return new Expression(head) { Operands = operands };
        }

        private static void SavePosition()
        {
            _tokensIteratorStack.Push(_tokensIterator);
        }

        private static void RestorePosition()
        {
            _tokensIterator = _tokensIteratorStack.Pop();
            _token = _tokens[_tokensIterator];
        }

        private static void NextToken()
        {
            _token = _tokens[_tokensIterator];
            _tokensIterator++;
        }
    }
}

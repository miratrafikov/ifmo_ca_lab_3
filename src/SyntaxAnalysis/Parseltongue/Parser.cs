using System;
using System.Collections.Generic;

using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Util;
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

        public static IElement Parse(List<Token> tokens)
        {
            Prepare(tokens);
            var result = GetSymbol(NonTerminal.Root);
            if (result.success)
            {
                // TODO
                return ((List<IElement>)result.value)[0];
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
            return new Result(true, BuildExpression((List<IElement>)result.value));
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
                    return new Result(true, new Symbol(_token.Content));
                case Terminal.Number:
                    return new Result(true, new Integer(Convert.ToInt32(_token.Content)));
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
                var correspondingObjects = new List<IElement>();
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
                        case List<IElement> list:
                            correspondingObjects.AddRange(list);
                            break;
                        default:
                            correspondingObjects.Add((IElement)result.value);
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

        private static Expression BuildExpression(List<IElement> objectsList)
        {
            var operands = objectsList.GetRange(1, objectsList.Count - 1);
            // TODO
            switch (objectsList[0].ToString())
            {
                case "sum":
                    return new Expression(Head.Sum) { Operands = operands };
                case "mul":
                    return new Expression(Head.Mul) { Operands = operands };
                case "pow":
                    return new Expression(Head.Pow) { Operands = operands };
                case "set":
                    return new Expression(Head.Sum) { Operands = operands };
                case "delayed":
                    return new Expression(Head.Sum) { Operands = operands };
                default:
                    return new Expression(Head.Expression) { Operands = operands };
            }
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

using System;
using System.Collections.Generic;

using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Patterns;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;
using static ShiftCo.ifmo_ca_lab_3.Evaluation.Util.Head;
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
            if (!result.success)
            {
                return result;
            }
            switch (symbol)
            {
                case NonTerminal.Expression:
                    return new Result(true, BuildExpression((List<IElement>)result.value));
                case NonTerminal.Pattern:
                    return new Result(true, BuildPattern((List<IElement>)result.value));
                default:
                    return result;
            }

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
                case Terminal.Underscores:
                    return new Result(true, new Symbol(_token.Content)); // TODO
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
            return new Expression(objectsList[0].ToString()) { Operands = operands };
        }

        private static IPattern BuildPattern(List<IElement> objects)
        {
            string patternName = ((Symbol)objects[0]).Value;
            string typeName = objects.Count == 2 ? "" : ((Symbol)objects[2]).Value;
            int underscores = ((Symbol)objects[1]).Value.Length;
            switch (underscores, typeName)
            {
                case (1, ""):
                    return new ElementPattern(patternName);
                case (1, "symbol"):
                    return new ElementPattern(patternName);
                case (1, "integer"):
                    return new IntegerPattern(patternName);
                case (3, ""):
                    return new NullableSequencePattern(patternName);
                default:
                    throw new Exception("TODO");
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

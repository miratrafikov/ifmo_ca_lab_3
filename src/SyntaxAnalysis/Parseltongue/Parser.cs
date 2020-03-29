using System;
using System.Collections.Generic;

using ShiftCo.ifmo_ca_lab_3.Commons.Exceptions;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Interfaces;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Patterns;
using ShiftCo.ifmo_ca_lab_3.Evaluation.Types;
using ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Lexington;

using Terminal = ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Lexington.TokenType;

namespace ShiftCo.ifmo_ca_lab_3.SyntaxAnalysis.Parseltongue
{
    public static class Parser
    {
        private static List<Token> s_tokens;
        private static Token s_token;
        private static int s_tokensIterator;
        private static Stack<int> s_tokensIteratorStack;

        public static IElement Parse(List<Token> tokens)
        {
            Prepare(tokens);
            var result = GetSymbol(NonTerminal.Root);
            if (result.Success)
            {
                // TODO
                return ((List<IElement>)result.Value)[0];
            }
            else
            {
                throw new NoSuitableParseTreeException();
            }
        }

        private static void Prepare(List<Token> tokens)
        {
            s_tokens = tokens;
            s_tokensIterator = 0;
            s_tokensIteratorStack = new Stack<int>();
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
            if (!result.Success)
            {
                return result;
            }
            switch (symbol)
            {
                case NonTerminal.Expression:
                    return new Result(true, BuildExpression((List<IElement>)result.Value));
                case NonTerminal.Pattern:
                    return new Result(true, BuildPattern((List<IElement>)result.Value));
                default:
                    return result;
            }

        }

        private static Result GetTerminal(Terminal requestedSymbol)
        {
            if (s_token.Type != requestedSymbol)
            {
                return new Result(false);
            }

            switch (requestedSymbol)
            {
                case Terminal.Symbol:
                    return new Result(true, new Symbol(s_token.Content));
                case Terminal.Number:
                    return new Result(true, new Integer(Convert.ToInt32(s_token.Content)));
                case Terminal.Underscores:
                    return new Result(true, new Symbol(s_token.Content)); // TODO
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
                    if (!result.Success)
                    {
                        productionMatches = false;
                        break;
                    }

                    switch (result.Value)
                    {
                        case null:
                            continue;
                        case List<IElement> list:
                            correspondingObjects.AddRange(list);
                            break;
                        default:
                            correspondingObjects.Add((IElement)result.Value);
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
            var head = ((Symbol)objectsList[0]).Value;
            var operands = objectsList.GetRange(1, objectsList.Count - 1);
            return new Expression(head, operands);
        }

        private static IPattern BuildPattern(List<IElement> objects)
        {
            var patternName = ((Symbol)objects[0]).Value;
            var typeName = objects.Count == 2 ? "" : ((Symbol)objects[2]).Value;
            var underscores = ((Symbol)objects[1]).Value.Length;
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
                    throw new DebugException();
            }
        }

        private static void SavePosition()
        {
            s_tokensIteratorStack.Push(s_tokensIterator);
        }

        private static void RestorePosition()
        {
            s_tokensIterator = s_tokensIteratorStack.Pop();
            s_token = s_tokens[s_tokensIterator];
        }

        private static void NextToken()
        {
            s_token = s_tokens[s_tokensIterator];
            s_tokensIterator++;
        }
    }
}

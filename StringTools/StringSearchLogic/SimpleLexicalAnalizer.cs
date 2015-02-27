using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSearchLogic
{
    /// <summary>
    /// Very Simple Lexical Analyzer. Does almost nothing
    /// </summary>
    class SimpleLexicalAnalyzer
    {
        public void AnalizeTokens(List<Token> tokenList)
        {
            analyzeTokens(tokenList);
        }

        private void analyzeTokens(List<Token> tokenList)
        {
            if (!(CheckParenthesis(tokenList) && CheckGramar(tokenList)))
                throw new ArgumentException("Your query is not correct!");
        }

        private bool CheckParenthesis(List<Token> tokenList)
        {
            bool result = true;
            Stack<Token> parethesisStack = new Stack<Token>();
            foreach (var token in tokenList)
            {
                if (token.TokenType == TokenType.LeftParenthesis)
                    parethesisStack.Push(token);
                else if (token.TokenType == TokenType.RightParenthesis)
                {
                    if (parethesisStack.Count > 0)
                        parethesisStack.Pop();
                    else
                    {
                        result = false;
                        break;
                    }
                }
            }
            if (parethesisStack.Count != 0) result = false;
            return result;
        }

        private bool CheckGramar(List<Token> tokenList)
        {
            bool result = true;
            if ((tokenList.First().TokenType != TokenType.LeftParenthesis && tokenList.First().TokenType != TokenType.Word) ||
                (tokenList.Last().TokenType != TokenType.RightParenthesis && tokenList.Last().TokenType != TokenType.Word))
                result = false;
            return result;
        }
    }
}

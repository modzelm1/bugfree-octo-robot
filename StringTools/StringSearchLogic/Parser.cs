using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSearchLogic
{
    class Parser
    {
        StringBuilder tmpStringBuilder;
        List<Token> tokenList;

        public Parser()
        {
            tmpStringBuilder = new StringBuilder();
            tokenList = new List<Token>();
        }

        public List<Token> Tokenize(string input)
        {
            //helper varible for handling quotation mark (")
            bool ignoreWhitespace = true;

            foreach (var a in input)
            {
                if (char.IsWhiteSpace(a))
                {
                    if (ignoreWhitespace) //will create token from collected chars ore ignore whitespace
                        createToken();
                    else
                        appendChar(a); //use whitespace as a part of token value
                }
                else if (a == '"')
                {
                    ignoreWhitespace = (!ignoreWhitespace);
                    createToken();
                }
                else if (a == '(')
                {
                    createToken();
                    addToken(new Token() { TokenType = TokenType.LeftParenthesis, Value = "(" });
                }
                else if (a == ')')
                {
                    createToken();
                    addToken(new Token() { TokenType = TokenType.RightParenthesis, Value = ")" });
                }
                else if (char.IsLetterOrDigit(a) || char.IsPunctuation(a))
                {
                    appendChar(a);
                }
            }

            //create token form last iteratioin
            createToken();

            //repalce white spaces wit default operator
            addDefaultOperator();

            return tokenList;
        }

        private void appendChar(char c)
        {
            tmpStringBuilder.Append(c);
        }

        /// <summary>
        /// creates proper token, based on collected chars
        /// </summary>
        private void createToken()
        {
            string tmp = tmpStringBuilder.ToString();

            if (string.IsNullOrWhiteSpace(tmp))
                return;

            tmp = tmp.Trim();

            if (tmp.Equals("AND", StringComparison.CurrentCultureIgnoreCase))
                addToken(new Token() { TokenType = TokenType.AndOperator, Value = "AND" });
            else if (tmp.Equals("OR", StringComparison.CurrentCultureIgnoreCase))
                addToken(new Token() { TokenType = TokenType.OrOperator, Value = "OR" });
            else
                addToken(new Token() { TokenType = TokenType.Word, Value = tmp });
        }

        /// <summary>
        /// add token to output list
        /// </summary>
        /// <param name="t"></param>
        private void addToken(Token t)
        {
            tokenList.Add(t);
            //lastAdded = t;
            tmpStringBuilder.Clear();
        }

        /// <summary>
        /// cobines all tokens on the same level with default operator (replaces white spaces)
        /// </summary>
        private void addDefaultOperator(string defautOperator = "AND")
        {
            List<Token> tmpToken = tokenList;
            tokenList = new List<Token>();
            Token latmp = new Token() { TokenType = TokenType.Empty, Value = string.Empty };
            foreach (var token in tmpToken)
            {
                if ((latmp.TokenType == TokenType.Word || latmp.TokenType == TokenType.RightParenthesis) && 
                    (token.TokenType != TokenType.RightParenthesis && token.TokenType != TokenType.AndOperator && token.TokenType != TokenType.OrOperator))
                    tokenList.Add(new Token() { TokenType = TokenType.AndOperator, Value = defautOperator });
                tokenList.Add(token);
                latmp = token;
            }
        }
    }
}

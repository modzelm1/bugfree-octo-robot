using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSearchLogic
{
    class QueryProcessor
    {
        public Func<string, bool> ProcessTokenList(List<Token> listOfTokens)
        {
            List<Token> tmp = new List<Token>();
            Stack<Token> tokenStack = new Stack<Token>();

            //iterate over token list
            foreach (var token in listOfTokens)
            {
                //if it is nested query, create query tree
                if (token.TokenType == TokenType.RightParenthesis)
                {
                    tmp = new List<Token>();
                    while (tokenStack.Count > 0 && tokenStack.Peek().TokenType != TokenType.LeftParenthesis)
                    {
                        tmp.Add(tokenStack.Pop());
                    }
                    tmp.Reverse();
                    //clean Left Parenthesis
                    if (tokenStack.Count > 0 && tokenStack.Peek().TokenType == TokenType.LeftParenthesis)
                        tokenStack.Pop();
                    QueryTreeBuilder qtb = new QueryTreeBuilder();
                    var f = qtb.BuildQueryTree(tmp);
                    tokenStack.Push(new Token() { Expr = f, TokenType = TokenType.Expr });
                }
                else
                {
                    tokenStack.Push(token);
                }
            }
            var ret = tokenStack.ToList(); ret.Reverse();
            QueryTreeBuilder qb = new QueryTreeBuilder();
            return qb.BuildQueryTree(ret);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSearchLogic
{
    class QueryTreeBuilder
    {
        List<Token> listOfTokens;
        Stack<Func<string, bool>> exprStack;
        Stack<Token> oprStack;

        public Func<string, bool> BuildQueryTree(List<Token> listOfTokens)
        {
            this.listOfTokens = listOfTokens;
            ConvertTokenListToTree();
            StackExprUnion();
            StackExprIntersection();

            if (exprStack.Count != 1)
                throw new Exception("Query is not correct!");
            //single function as a result
            return exprStack.Pop();
        }

        private void StackExprUnion()
        {
            while (oprStack.Count > 0)
            {
                oprStack.Pop();
                var tmp = ComparerFunctionsFactory.ComparersUnion(exprStack.Pop(), exprStack.Pop());
                exprStack.Push(tmp);
            }
        }

        private void StackExprIntersection()
        {
            while (exprStack.Count > 1)
            {
                //oprStack.Pop();
                var tmp = ComparerFunctionsFactory.ComparersIntersection(exprStack.Pop(), exprStack.Pop());
                exprStack.Push(tmp);
            }
        }

        private void ConvertTokenListToTree()
        {
            exprStack = new Stack<Func<string, bool>>();
            oprStack = new Stack<Token>();

            for (int i = 0; i < listOfTokens.Count; i++)
            {
                if (listOfTokens[i].TokenType == TokenType.OrOperator)
                    StackExprUnion();
                else if (listOfTokens[i].TokenType == TokenType.Word)
                {
                    exprStack.Push(ComparerFunctionsFactory.CreateContainsComparer(listOfTokens[i].Value));
                }
                else if (listOfTokens[i].TokenType == TokenType.Expr)
                {
                    exprStack.Push(listOfTokens[i].Expr);
                }
                else if (listOfTokens[i].TokenType == TokenType.AndOperator)
                {
                    oprStack.Push(listOfTokens[i]);
                }
                else
                    continue;
            }
        }
    }
}

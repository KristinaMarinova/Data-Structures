namespace Problem04.BalancedParentheses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BalancedParenthesesSolve : ISolvable
    {
        public bool AreBalanced(string parentheses)
        {
            Stack<char> stack = new Stack<char>();
            bool isBalanced = true;

            for (int i = 0; i < parentheses.Length; i++)
            {
                char currentChar = parentheses[i];

                if (parentheses[i] == '{')
                {
                    stack.Push(parentheses[i]);
                }

                if (parentheses[i] == '[')
                {
                    stack.Push(parentheses[i]);
                }

                if (parentheses[i] == '(')
                {
                    stack.Push(parentheses[i]);
                }


                if (currentChar == ')')
                {
                    if (stack.Any() && stack.Peek() == '(')
                    {
                        stack.Pop();

                    }
                    else
                    {
                        isBalanced = false;
                    }
                }
                if (currentChar == ']')
                {
                    if (stack.Any() && stack.Peek() == '[')
                    {
                        stack.Pop();

                    }
                    else
                    {
                        isBalanced = false;
                    }
                }
                if (currentChar == '}')
                {
                    if (stack.Any() && stack.Peek() == '{')
                    {
                        stack.Pop();
                    }
                    else
                    {
                        isBalanced = false;
                    }
                }
            }

            return isBalanced;
        }
    }
}
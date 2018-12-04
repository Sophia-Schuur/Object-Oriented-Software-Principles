
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptS321
{
    public class ExpTree
    {
        
        public static Dictionary<string, double> dictionary = new Dictionary<string, double>();
        private string expression;
        private Node root;

        //expression getter
        public string GetExpression()
        {
            return expression;
        }
        //expression setter
        public void SetExpression(string expression)
        {
            this.expression = expression;
        }

        //constructor
        public ExpTree(string expression)
        {
            dictionary.Clear();
            this.expression = expression;
            BuildTree(expression);
        }

        //Sets user inputted value for user inputted variable
        public void SetVar(string varName, double varValue)
        {
            try
            {
                dictionary.Add(varName, varValue);
            }

            catch (ArgumentException)
            {
                dictionary[varName] = varValue;
            }
        }
       
        //will be overridden in each node class
        public double Eval()
        {          
            return root.Eval();
        }

        //base node class
        public abstract class Node
        {
            public abstract double Eval();
        }

        //Node that will only ever contain a numerical value (int)
        private class NumericalValueNode : Node 
        {
            double value = 0.0;

            //constructor
            public NumericalValueNode(double value)
            {
                this.value = value;
            }
            public override double Eval()
            {
                return value;
            }
        }

        //Node that contains a variable (A1, B2, etc..)
        public class VariableNode : Node
        {
            private string dictionaryName = null;
            double value = 0.0;

            //constructor
            public VariableNode(string newName)
            {
                dictionaryName = newName;
                value = 0.0;
            }
            public override double Eval()
            {
                value = dictionary[dictionaryName];
                return value;
            }
        }

        //Node that contains an operand (+, -, *, /)
        private class OperatorNode : Node
        {
            public Node left, right = null;
            private char operand;

            //constructor
            public OperatorNode(char newOperand)
            {
                operand = newOperand;
                left = null;
                right = null;
            }

            //Evaulate based on the operand 
            public override double Eval()
            {
                if (operand == '+')
                {
                    return left.Eval() + right.Eval();
                }
                else if (operand == '-')
                {
                    return left.Eval() - right.Eval();
                }
                else if (operand == '*')
                {
                    return left.Eval() * right.Eval();
                }
                else if (operand == '/')
                {
                    return left.Eval() / right.Eval();
                }
                else
                {
                    return 0.0; 
                }
            }          
        }

        //Build a new tree. Use whenever a new expression is entered
        Node BuildTree(string expression)
        {
            Stack<Node> stack = new Stack<Node>();

            //reserve the operators as a variable
            string deliminators = @"([-/\+\*\(\)])";

            //split expression into individual tokens as defined by the deliminators.
            var tokens = Regex.Split(expression, deliminators).Where(s => s != String.Empty).ToList<string>();

            //organize the tokens in adherence to the Shunting Yard algorithm
            var ShuntingList = ShuntingYard(tokens);

            foreach (var tok in ShuntingList)
            {
                //if the token is a letter, it is a variable. Insert new variable node
                if (Char.IsLetter(tok[0]))
                {
                    //does the dictionary already contain this key?
                        if (!dictionary.ContainsKey(tok))
                        {
                            //if not, add to the dictionary
                            SetVar(tok, 0);
                            //push onto the stack
                            stack.Push(new VariableNode(tok));
                        }

                    //user entered the same variable twice. oops
                    else
                    { 
                        Console.WriteLine($"[!] ERROR: You cannot enter the same variable twice. Terminating.");
                        Environment.Exit(0);
                    }
                }

                //if the token is a digit, insert new numerical value node and compute normally
                else if (Char.IsDigit(tok[0]))
                {
                    stack.Push(new NumericalValueNode(Double.Parse(tok)));
                }
                //operator token. Insert new OperatorNode and pop 
                else
                {
                    var z = new OperatorNode(tok[0]);
                    z.right = stack.Pop();
                    z.left = stack.Pop();
                    stack.Push(z);
                }
            }
            root = stack.Pop(); 
            return root;
        }

        //reorganize the list of tokens to adhere to the Shunting Yard algorithm
        //thank you to the wikipedia psuedo code
        private List<string> ShuntingYard(List<string> tokens)
        {
            //we need the operators on a separate stack
            var operatorStack = new Stack<string>();

            //define list we want to return
            var ShuntingList = new List<string>();

            foreach (var tok in tokens)
            {
                switch (tok)
                {
                    case "+":
                    case "-":
                    case "*":
                    case "/":
                        //are there more operators?
                        while (operatorStack.Count > 0)
                        {
                            //if the top of the stack is less precedent than the current token, break
                            if (setPrecedence(operatorStack.Peek()) < setPrecedence(tok))
                            {
                                break;
                            }
                            //otherwise, add to list
                            ShuntingList.Add(operatorStack.Pop());
                        }
                        operatorStack.Push(tok);
                        break;

                    //begin opening parentheses. Push normally
                    case "(":
                        operatorStack.Push(tok);
                        break;

                    //found closed parentheses? 
                    case ")":
                        while (operatorStack.Count > 0 && operatorStack.Peek() != "(")
                        {
                            ShuntingList.Add(operatorStack.Pop());
                        }
                        operatorStack.Pop();
                        break;

                    //not an operator? Add to list
                    default:
                        ShuntingList.Add(tok);
                        break;
                }
            }

            //Got all our operators? Add to list
            while (operatorStack.Count > 0)
            {
                ShuntingList.Add(operatorStack.Pop());
            }
            return ShuntingList;
        }

        //set operator precedences
        private static int setPrecedence(string token)
        {
            switch (token)
            {
                //multiplication and division take medium precedence
                case "*":
                case "/":
                    return 2;
                //addition and subtraction take less 
                case "+":
                case "-":
                    return 1;

                //parentheses take highest
                case "(":
                    return 0;
                case ")":
                    return 3;

                //none of the above?
                default:
                    return -1;
            }

        }
        public string[] GetVariables()
        {
            return dictionary.Keys.ToArray();
        }
    }
}

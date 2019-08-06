using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;

namespace Calculator_Project_2__WPF_
{
    class Calculator
    {
        public TextBox TheTextBox { set; get; } = new TextBox();

        private bool DoCleanTextBoxInNextInsert { set; get; } = false;

        private bool radiansOn { set; get; } = true;






        public void ClearTextBox()
        {
            TheTextBox.Text = "";
        }

        public void CalculateResult()
        {
            string equation = TheTextBox.Text;
            if (ThereIsBracketError(equation))
                throw new BracketErrorException();

            TheTextBox.Text = Calculate(equation).ToString("f40").TrimEnd('0').TrimEnd(',');
            TheTextBox.SelectionStart = TheTextBox.Text.Length;
        }

        public void PressNumberButton(string number)
        {
            if (DoCleanTextBoxInNextInsert == true)
            {
                ClearTextBox();
                DoCleanTextBoxInNextInsert = false;
            }

            InsertString(number);
        }

        public void PressBracketButton(string bracket)
        {
            if (DoCleanTextBoxInNextInsert == true)
            {
                ClearTextBox();
                DoCleanTextBoxInNextInsert = false;
            }

            InsertString(bracket);
        }

        public void PressOperatorButton(string operate)
        {
            DoCleanTextBoxInNextInsert = false;
            InsertString(operate);
        }

        public void PressFunctionButton(string function)
        {
            if (DoCleanTextBoxInNextInsert == true)
            {
                ClearTextBox();
                DoCleanTextBoxInNextInsert = false;
            }

            InsertFunction(function);
        }

        public void PressDegreeButton()
        {
            radiansOn = false;
        }

        public void PressRadianButton()
        {
            radiansOn = true;
        }

        public void ClearTextBoxInNextInsert()
        {
            DoCleanTextBoxInNextInsert = true;
        }

        public void CancelClearTextBoxInNextInsert()
        {
            DoCleanTextBoxInNextInsert = false;
        }










        private void InsertString(string text)
        {
            int cursorIndex = TheTextBox.SelectionStart;
            TheTextBox.Text = TheTextBox.Text.Insert(cursorIndex, text);
            TheTextBox.SelectionStart = cursorIndex + text.Length;
        }

        private void InsertFunction(string function)
        {
            int cursorIndex = TheTextBox.SelectionStart;
            TheTextBox.Text = TheTextBox.Text.Insert(cursorIndex, function + "()");
            TheTextBox.SelectionStart = cursorIndex + function.Length + 1;
        }

        private double Calculate(string equation)
        {
            equation = InfixToPostfix(equation);
            double result = CalculatePostfixEquation(equation);
            return result;
        }

        private double CalculatePostfixEquation(string equation)
        {
            List<string> equationList = EquationToList(equation);
            Stack<string> stackForCalculate = new Stack<string>();

            for (int i = 0; i < equationList.Count; i++)
            {
                if (IsNumber(equationList[i]))
                {
                    stackForCalculate.Push(equationList[i]);
                }
                else
                {
                    if (IsOneInputOperator(equationList[i]))
                    {
                        double var1 = double.Parse(stackForCalculate.Pop());

                        switch (equationList[i])
                        {
                            case ("sin"):
                                var1 = MyMath.Sin(var1, radiansOn);
                                break;
                            case ("cos"):
                                var1 = MyMath.Cos(var1, radiansOn);
                                break;
                            case ("tan"):
                                var1 = MyMath.Tan(var1, radiansOn);
                                break;
                            case ("arcsin"):
                                var1 = MyMath.Asin(var1, radiansOn);
                                break;
                            case ("arccos"):
                                var1 = MyMath.Acos(var1, radiansOn);
                                break;
                            case ("arctan"):
                                var1 = MyMath.Atan(var1, radiansOn);
                                break;
                            case ("ln"):
                                var1 = MyMath.Ln(var1);
                                break;
                            case "log":
                                var1 = MyMath.Log10(var1);
                                break;
                            case ("sqrt"):
                                var1 = MyMath.Sqrt(var1);
                                break;
                            case "factorial":
                                var1 = MyMath.Factorial(int.Parse(var1.ToString()));
                                break;
                            default:
                                throw new UnknownOperatorException();
                        }

                        stackForCalculate.Push(var1.ToString());
                    }
                    else
                    {
                        double var1 = double.Parse(stackForCalculate.Pop());
                        double var2 = double.Parse(stackForCalculate.Pop());

                        switch (equationList[i])
                        {
                            case "+":
                                var1 = var2 + var1;
                                break;
                            case "-":
                                var1 = var2 - var1;
                                break;
                            case "*":
                                var1 = var2 * var1;
                                break;
                            case "/":
                                var1 = var2 / var1;
                                break;
                            case "%":
                                var1 = var2 % var1;
                                break;
                            case "^":
                                var1 = MyMath.Pow(var2, var1);
                                break;
                            default:
                                throw new UnknownOperatorException();
                        }

                        stackForCalculate.Push(var1.ToString());
                    }
                }
            }

            double result = double.Parse(stackForCalculate.Pop());
            return result;
        }

        private static bool IsOneInputOperator(string v)
        {
            string[] oneInputOperator = { "sin", "cos", "tan", "arcsin", "arccos", "arctan", "ln", "log", "sqrt", "factorial" };
            return oneInputOperator.Contains(v);
        }

        private static string InfixToPostfix(string v)
        {
            string result = "";
            v = FormatThisStringForCalculate(v);
            List<string> infixList = EquationToList(v);
            Stack<string> stack = new Stack<string>();


            for (int i = 0; i < infixList.Count; i++)
            {
                if (IsNumber(infixList[i]))
                {
                    result += " " + infixList[i] + " ";
                }
                else if (infixList[i] == '('.ToString())
                {
                    stack.Push(infixList[i].ToString());
                }
                else if (infixList[i] == ')'.ToString())
                {
                    while (stack.Peek() != "(")
                    {
                        result += " " + stack.Pop() + " ";
                    }
                    if (stack.Peek() == "(")
                        stack.Pop();
                }
                else // is operator
                {
                    while (GetOperatorPrecendence(stack.Peek()) >= GetOperatorPrecendence(infixList[i].ToString()))
                    {
                        result += " " + stack.Pop() + " ";
                    }
                    stack.Push(infixList[i].ToString());
                }
            }

            result = Regex.Replace(result, @"( )+", @" ");

            return result;
        }

        private static string FormatThisStringForCalculate(string v)
        {
            v = v.ToLower();
            v = "(" + v + ")";
            v = v.Replace("e", " " + Math.Exp(1).ToString());
            v = v.Replace("π", " " + Math.PI.ToString());
            v = v.Replace("pi", " " + Math.PI.ToString());
            // +: one or more
            // *: zero or more
            v = Regex.Replace(v, @"(\d|\))( )+(\d|\w)", @"$1*$3");
            v = Regex.Replace(v, @"(\)|\d)( )*(\w)", @"$1*$3");
            v = Regex.Replace(v, @"(^)-", @"0-");
            v = Regex.Replace(v, @"\(-", @"(0-");
            v = v.Replace(" ", "");

            return v;
        }

        private static List<string> EquationToList(string v)
        {
            List<string> result = new List<string>();

            for (int i = 0; i < v.Length; i++)
            {
                string temp = "";

                while (IsNumber(v[i]))
                {
                    temp += v[i++];
                }
                if (temp != "")
                {
                    result.Add(temp);
                    temp = "";
                    i--;
                }


                while (IsCharacter(v[i]))
                {
                    temp += v[i++];
                }
                if (temp != "")
                {
                    result.Add(temp);
                    temp = "";
                    i--;
                }

                if (v[i] == '(')
                    result.Add(v[i].ToString());
                if (v[i] == ')')
                    result.Add(v[i].ToString());
                if (IsOperator(v[i].ToString()))
                    result.Add(v[i].ToString());

            }

            return result;
        }

        private static int GetOperatorPrecendence(string v)
        {
            string[] hightPriorityOperator = { "^", "sqrt", "log", "ln", "factorial", "sin", "cos", "tan", "arcsin", "arccos", "arctan" };

            if (hightPriorityOperator.Contains(v))
                return 2;
            if (v == "*" || v == "/" || v == "%")
                return 1;
            if (v == "+" || v == "-")
                return 0;
            else
                return -1;
        }

        private static bool IsNumber(char v)
        {
            string characterThatAreConsideredNumber = "1234567890,.";
            return characterThatAreConsideredNumber.Contains(v.ToString());
        }

        private static bool IsCharacter(char v)
        {
            return (v >= 'a' && v <= 'z');
        }

        private static bool IsNumber(string v)
        {
            double x = 0;
            return double.TryParse(v, out x);
        }

        private static bool IsOperator(string v)
        {
            string[] stringsThatAreOperator = { "+", "-", "*", "/", "%", "^", "sqrt", "log", "ln", "factorial", "sin", "cos", "tan", "arcsin", "arccos", "arctan" };
            return stringsThatAreOperator.Contains(v);
        }

        private static bool ThereIsBracketError(string equation)
        {
            int n = 0;
            foreach (char character in equation)
            {
                if (character == '(')
                    n++;
                if (character == ')')
                    n--;
                if (n < 0)
                    return true;
            }

            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator_Project_2__WPF_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Calculator myCalculator = new Calculator();

        private Brush highLightColor = new SolidColorBrush(Color.FromRgb(0x00, 0x00, 0xff));
        private Brush normalButtonColor = new SolidColorBrush(Color.FromRgb(0xe1, 0xe1, 0xe1));


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Load(object sender, EventArgs e)
        {
            degreeButton_Click(sender, e);
            myCalculator.TheTextBox = inputTextBox;
        }

        private void radianButton_Click(object sender, EventArgs e)
        {
            myCalculator.PressRadianButton();
            radianButton.Background = this.highLightColor;
            degreeButton.Background = this.normalButtonColor;
        }

        private void degreeButton_Click(object sender, EventArgs e)
        {
            myCalculator.PressDegreeButton();

            radianButton.Background = this.normalButtonColor;
            degreeButton.Background = this.highLightColor;
        }

        private void zeroButton_Click(object sender, EventArgs e)
        {
            myCalculator.PressNumberButton("0");
        }

        private void oneButton_Click(object sender, EventArgs e)
        {
            myCalculator.PressNumberButton("1");
        }

        private void twoButton_Click(object sender, EventArgs e)
        {
            myCalculator.PressNumberButton("2");
        }

        private void threeButton_Click(object sender, EventArgs e)
        {
            myCalculator.PressNumberButton("3");
        }

        private void fourButton_Click(object sender, EventArgs e)
        {
            myCalculator.PressNumberButton("4");
        }

        private void fiveButton_Click(object sender, EventArgs e)
        {
            myCalculator.PressNumberButton("5");
        }

        private void sixButton_Click(object sender, EventArgs e)
        {
            myCalculator.PressNumberButton("6");
        }

        private void sevenButton_Click(object sender, EventArgs e)
        {
            myCalculator.PressNumberButton("7");
        }

        private void eightButton_Click(object sender, EventArgs e)
        {
            myCalculator.PressNumberButton("8");
        }

        private void nineButton_Click(object sender, EventArgs e)
        {
            myCalculator.PressNumberButton("9");
        }

        private void decimalSeperatorButton_Click(object sender, EventArgs e)
        {
            myCalculator.PressNumberButton(",");
        }

        private void piButton_Click(object sender, EventArgs e)
        {
            myCalculator.PressNumberButton("π");
        }

        private void expButton_Click(object sender, EventArgs e)
        {
            myCalculator.PressNumberButton("e");
        }

        private void plusButton_Click(object sender, EventArgs e)
        {
            myCalculator.PressOperatorButton("+");
        }

        private void subtractButton_Click(object sender, EventArgs e)
        {
            myCalculator.PressOperatorButton("-");
        }

        private void multiplyButton_Click(object sender, EventArgs e)
        {
            myCalculator.PressOperatorButton("*");
        }

        private void divisionButton_Click(object sender, EventArgs e)
        {
            myCalculator.PressOperatorButton("/");
        }

        private void moduloButton_Click(object sender, EventArgs e)
        {
            myCalculator.PressOperatorButton("%");
        }

        private void powerButton_Click(object sender, EventArgs e)
        {
            myCalculator.PressOperatorButton("^");
        }

        private void openBracketButton_Click(object sender, EventArgs e)
        {
            myCalculator.PressBracketButton("(");
        }

        private void closeBracketButton_Click(object sender, EventArgs e)
        {
            myCalculator.PressBracketButton(")");
        }

        private void arcsinButton_Click(object sender, EventArgs e)
        {
            myCalculator.PressFunctionButton("arcsin");
        }

        private void arccosButton_Click(object sender, EventArgs e)
        {
            myCalculator.PressFunctionButton("arccos");
        }

        private void arctanButton_Click(object sender, EventArgs e)
        {
            myCalculator.PressFunctionButton("arctan");
        }

        private void sinButton_Click(object sender, EventArgs e)
        {
            myCalculator.PressFunctionButton("sin");
        }

        private void cosButton_Click(object sender, EventArgs e)
        {
            myCalculator.PressFunctionButton("cos");
        }

        private void tanButton_Click(object sender, EventArgs e)
        {
            myCalculator.PressFunctionButton("tan");
        }

        private void sqrtButton_Click(object sender, EventArgs e)
        {
            myCalculator.PressFunctionButton("sqrt");
        }

        private void lnButton_Click(object sender, EventArgs e)
        {
            myCalculator.PressFunctionButton("ln");
        }

        private void logButton_Click(object sender, EventArgs e)
        {
            myCalculator.PressFunctionButton("log");
        }

        private void factorialButton_Click(object sender, EventArgs e)
        {
            myCalculator.PressFunctionButton("factorial");
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            myCalculator.ClearTextBox();
        }

        private void equalButton_Click(object sender, EventArgs e)
        {
            try
            {
                myCalculator.CalculateResult();
                myCalculator.ClearTextBoxInNextInsert();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            equalButton_Click(sender, e);
        }

        private void textBox_Click(object sender, MouseEventArgs e)
        {
            myCalculator.CancelClearTextBoxInNextInsert();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void viewHelpButton_Click(object sender, EventArgs e)
        {
            Window helpWindow = new HelpWindow();
            helpWindow.Show();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Opdracht5RomeinseRekenmachine
{
    public partial class Form1 : Form
    {
        private const int buttonHorizontalOfset = 20;
        private const int numeralButtonVerticalOfset = 60;
        private const int opperatorButtonVerticalOfset = 110;

        private const int buttonWidth = 50;
        private const int buttonHeight = 40;

        private const int buttonHorizontalSpacing = 10;

        private List<ButtonFunction> buttonFunctions;
        private RomanCalculator romanCalculator;

        public Form1()
        {
            InitializeComponent();



            buttonFunctions = new List<ButtonFunction>();
            romanCalculator = new RomanCalculator();



            Action<int, int, string, Action<object>, Object> createButton = CreateButtonLambda();



            int changingButtonHorizontalOfset = buttonHorizontalOfset;
            Action<object> numeralButtonFunction = (object numeral) =>
            {
                if (typeof(char).IsInstanceOfType(numeral))
                {
                    romanCalculator.AddNumeral((char)numeral);
                    ResetLabelSum();
                }
                else
                {
                    throw new Exception("the given numberal is not of the right type");
                }
            };
            Action<object> createNumeralButton = (object numeral) =>
            {
                if (typeof(char).IsInstanceOfType(numeral))
                {
                    createButton.Invoke(changingButtonHorizontalOfset, numeralButtonVerticalOfset, ""+(char)numeral, numeralButtonFunction, numeral);
                    changingButtonHorizontalOfset += buttonWidth + buttonHorizontalSpacing;
                }
                else
                {
                    throw new Exception("the given numberal is not of the right type");
                }
            };
            romanCalculator.RunLambdaTroughRomanNumerals(createNumeralButton);



            changingButtonHorizontalOfset = buttonHorizontalOfset;
            
            Action<object> resetRomanCalculator = (object irrelavent) =>
            {
                romanCalculator = new RomanCalculator();
                ResetLabelSum();
                ResetLabelResult();
            };
            createButton.Invoke(changingButtonHorizontalOfset, opperatorButtonVerticalOfset, "C", resetRomanCalculator, null);
            changingButtonHorizontalOfset += buttonWidth + buttonHorizontalSpacing;
           
            Action<object> GetSumResult = (object irrelavent) =>
            {
                ResetLabelResult();
            };
            createButton.Invoke(changingButtonHorizontalOfset, opperatorButtonVerticalOfset, "=", GetSumResult, null);
            
            Action<object> SelectOpperator = (object opperator) =>
            {
                romanCalculator.AddOpperator(opperator);
                ResetLabelSum();
            };
            Action<object> createOperatorButton = (object opperator) =>
            {
                changingButtonHorizontalOfset += buttonWidth + buttonHorizontalSpacing;
                createButton.Invoke(changingButtonHorizontalOfset, opperatorButtonVerticalOfset, opperator.ToString(), SelectOpperator, opperator);
            };
            romanCalculator.RunLambdaTroughOpperators(createOperatorButton);
        }
        private Action<int, int, string, Action<object>, object> CreateButtonLambda()
        {
            Action<int, int, string, Action<object>, object> createButtonLambda = (int horizontaOfset, int verticalOfset, string text, Action<object> function, object functionParameter) =>
            {
                Button newButton = new Button();

                newButton.Location = new Point(horizontaOfset, verticalOfset);
                newButton.Width = buttonWidth;
                newButton.Height = buttonHeight;

                newButton.Text = text;

                newButton.Click += new EventHandler(UseButton);

                buttonFunctions.Add(new ButtonFunction(newButton, function, functionParameter));

                Controls.Add(newButton);
            };
            return createButtonLambda;
        }

        private void UseButton(object sender, EventArgs e)
        {
            foreach(ButtonFunction buttonFunction in buttonFunctions)
            {
                if (buttonFunction.IsSameButton(sender))
                {
                    buttonFunction.DoFunction();
                    break;
                }
            }
        }

        private void ResetLabelSum()
        {
            LabelSum.Text = romanCalculator.GetSumText();
        }
        private void ResetLabelResult()
        {
            LabelResult.Text = romanCalculator.GetResult();
        }

        private class ButtonFunction
        {
            object button;
            Action<object> function;
            object functionParamiter;

            internal ButtonFunction(object button, Action<object> function, object functionParamiter)
            {
                this.button = button;
                this.function = function;
                this.functionParamiter = functionParamiter;
            }
            internal bool IsSameButton(object button)
            {
                if (this.button == button)
                {
                    return true;
                }
                return false;
            }
            internal void DoFunction()
            {
                function.Invoke(functionParamiter);
            }
        }
    }
}

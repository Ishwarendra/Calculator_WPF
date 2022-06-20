using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastNumber, result;
        Operator selectedOperator;

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

        }
        #endregion

        #region Number Buttons
        private void button_Click(object sender, RoutedEventArgs e)
        {
            int selectedNum = int.Parse((sender as Button).Content.ToString());

            if (resultLabel.Content.ToString() == "0")
                resultLabel.Content = $"{selectedNum}";
            else
                resultLabel.Content = $"{resultLabel.Content}{selectedNum}";
        }
        #endregion

        #region Top Buttons (AC, +/-, %) and PointButton
        private void acButton_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
            result = 0;
            lastNumber = 0;
        }

        private void negativeButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber *= -1;
                resultLabel.Content = lastNumber.ToString();
            }
        }

        private void percentageButton_Click(object sender, RoutedEventArgs e)
        {
            double tempNumber;
            if (double.TryParse(resultLabel.Content.ToString(), out tempNumber))
            {
                tempNumber /= 100;
                if (lastNumber != 0)
                    tempNumber *= lastNumber;

                resultLabel.Content = tempNumber.ToString();
            }
        }

        private void pointButton_Click(object sender, RoutedEventArgs e)
        {
            //if (!resultLabel.Content.ToString().Contains('.'))
            //    resultLabel.Content = $"{resultLabel.Content}.";
        }

        #endregion

        #region Operation Button (= and other 4 operators)

        private void operationButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
                resultLabel.Content = "0";

            if (sender == multiplicationButton)
                selectedOperator = Operator.Multiplication;
            else if (sender == additionButton)
                selectedOperator = Operator.Addition;
            else if (sender == subtractionButton)
                selectedOperator = Operator.Subtraction;
            else if (sender == divisionButton)
                selectedOperator = Operator.Division;
        }

        private void equalButton_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;
            if (double.TryParse(resultLabel.Content.ToString(), out newNumber))
            {
                switch (selectedOperator)
                {
                    case Operator.Addition:
                        result = SimpleMath.Add(lastNumber, newNumber);
                        break;
                    case Operator.Subtraction:
                        result = SimpleMath.Subtraction(lastNumber, newNumber);
                        break;
                    case Operator.Multiplication:
                        result = SimpleMath.Multiplication(lastNumber, newNumber);
                        break;
                    case Operator.Division:
                        result = SimpleMath.Division(lastNumber, newNumber);
                        break;
                }

                resultLabel.Content = result.ToString();
            }
        }

        #endregion
    }

    #region Operation Class
    public enum Operator
    {
        Addition,
        Subtraction,
        Multiplication,
        Division,
    }

    public class SimpleMath
    {
        public static double Add(double a, double b)
        {
            return a + b;
        }
        public static double Subtraction(double a, double b)
        {
            return a - b;
        }
        public static double Multiplication(double a, double b)
        {
            return a * b;
        }
        public static double Division(double a, double b)
        {
            if (b == 0)
            {
                MessageBox.Show("Division by Zero is not allowed.", "Invalid Operation", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0.0;
            }

            return a / b;
        }
    }

    #endregion
}

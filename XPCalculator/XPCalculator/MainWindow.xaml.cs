using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

namespace XPCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            calculator = new Calculator();
            InitializeComponent();
        }

        int activeOperator = 0;
        string consoleOutput;
        double a = 0;
        double b = 0;
        double rez = 0;
        Calculator calculator;

        private void Button_Click_Sum(object sender, RoutedEventArgs e)
        {
            activeOperator = 1;
            a = Convert.ToDouble(tb_console.Text.ToString());
            tb_console.Text = "";
        }

        private void Button_Click_Sub(object sender, RoutedEventArgs e)
        {
            a = Convert.ToDouble(tb_console.Text.ToString());
            tb_console.Text = "";
            activeOperator = 2;
        }

        private void Button_Click_Mul(object sender, RoutedEventArgs e)
        {
            a = Convert.ToDouble(tb_console.Text.ToString());
            tb_console.Text = "";
            activeOperator = 3;
        }

        private void Button_Click_Div(object sender, RoutedEventArgs e)
        {
            a = Convert.ToDouble(tb_console.Text.ToString());
            tb_console.Text = "";
            activeOperator = 4;
        }

        private void Button_Click_Rez(object sender, RoutedEventArgs e)
        {
            b = Convert.ToDouble(tb_console.Text.ToString());
            switch (activeOperator)
            {
                case 0:
                    consoleOutput = "";
                    break;

                case 1:
                    consoleOutput = calculator.sum(a, b).ToString();
                    break;

                case 2:
                    consoleOutput = calculator.sub(a, b).ToString();
                    break;

                case 3:
                    consoleOutput = calculator.mul(a, b).ToString();
                    break;

                case 4:
                    consoleOutput = calculator.div(a, b).ToString();
                    break;
            }
            activeOperator = 0;
            tb_console.Text = consoleOutput;
        }

        private void Button_Click_Clear_Console(object sender, RoutedEventArgs e)
        {
            activeOperator = 0;
            tb_console.Text = "";
        }
    }
}

using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AvaloniaCalculator;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void BtnCalculate_Click(object sender, RoutedEventArgs e)
    {
        string num1Text = txtNum1.Text;
        if (double.TryParse(txtNum1.Text, out double num1) && double.TryParse(txtNum2.Text, out double num2))
        {
            double sum = num1 + num2;
            lblResult.Text = $"Result: {sum}";
        }
        else
        {
            // Mostrar mensaje de error si los datos son incorrectos
            lblResult.Text = "Error: Invalid input. Enter valid numbers.";
        }
    }
}
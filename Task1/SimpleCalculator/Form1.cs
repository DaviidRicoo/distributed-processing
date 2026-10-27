using System;
using System.Windows.Forms;

namespace SimpleCalculator
{
    public partial class Form1 : Form
    {
        private TextBox txtNum1;
        private TextBox txtNum2;
        private Button btnCalculate;
        private Label lblResult;

        public Form1()
        {
            this.Text = "Simple Calculator";
            this.Width = 300;
            this.Height = 250;

            txtNum1 = new TextBox() { Left = 20, Top = 20, Width = 100 };
            txtNum2 = new TextBox() { Left = 20, Top = 50, Width = 100 };
            
            btnCalculate = new Button() { Text = "Calculate", Left = 20, Top = 80, Width = 100 };
            btnCalculate.Click += BtnCalculate_Click; // Evento de clic[cite: 4]
            
            lblResult = new Label() { Text = "Result: ", Left = 20, Top = 120, Width = 200 };

            this.Controls.Add(txtNum1);
            this.Controls.Add(txtNum2);
            this.Controls.Add(btnCalculate);
            this.Controls.Add(lblResult);
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtNum1.Text, out double num1) && double.TryParse(txtNum2.Text, out double num2))
            {
                double sum = num1 + num2;
                lblResult.Text = $"Result: {sum}";
            }
            else
            {
                //Incorrrect input
                MessageBox.Show("Invalid input. Please enter valid numbers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
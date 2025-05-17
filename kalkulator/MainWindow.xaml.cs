using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using kalkulator.Model;

namespace kalkulator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Calculator calc = new Calculator(0, "", 0);
        private bool newOperation = true;

        private async Task ShowError(string? message)
        {
            tbx_wyswietlacz.Text = message;
            await Task.Delay(2000);
            btn_clear_Click(null, null);
        }

            private void btn_1_Click(object? sender, RoutedEventArgs? e)
        {
            if (sender is Button btn)
            {
                string? value = btn.Content.ToString();
                if (newOperation)
                {
                    tbx_wyswietlacz.Text = "";
                    newOperation = false;
                }
                if (tbx_wyswietlacz.Text.Contains(","))
                {
                    if (value != ",")
                    {
                        tbx_wyswietlacz.Text += value;
                    }
                } else
                {
                    tbx_wyswietlacz.Text += value;
                }
                if(tbx_wyswietlacz.Text == ",")
                {
                    tbx_wyswietlacz.Text = "0,";
                }
                if (double.TryParse(tbx_wyswietlacz.Text, out double parsedValue))
                {
                    calc.Value1 = parsedValue;
                }
                calc.LastClickIsNumber = true;
            }
        }

        private void btn_2_Click(object? sender, RoutedEventArgs? e)
        {
            if (sender is Button btn)
            {
                if (calc.LastClickIsNumber)
                {
                    if (!string.IsNullOrEmpty(calc.Op))
                    {
                        calc.calculate();
                        tbx_wyswietlacz.Text = calc.Result.ToString();
                    }
                    else
                    {
                        calc.Result = calc.Value1;
                    }
                }
                calc.Op = btn.Content.ToString();
                newOperation = true;
                calc.LastClickIsNumber = false;
            }
        }

        private async void btn_equals_Click(object? sender, RoutedEventArgs? e)
        {
            if (!string.IsNullOrEmpty(calc.Op))
            {
                if (calc.LastClickIsNumber)
                {
                    if (calc.Op == "/" && calc.Value1 == 0)
                    {
                        await ShowError("Dzielenie przez zero!");
                        return;
                    }
                    calc.calculate();
                }
                else
                {
                    calc.Value1 = calc.LastValue;
                    calc.calculate();
                }
                tbx_wyswietlacz.Text = calc.Result.ToString();
                calc.LastValue = calc.Value1;
                newOperation = true;
                calc.LastClickIsNumber = false;
            }
        }

        private void btn_change_Click(object? sender, RoutedEventArgs? e)
        {
            if (double.TryParse(tbx_wyswietlacz.Text, out double parsedValue))
            {
                calc.Value1 = parsedValue;
            }
            if (calc.Value1 != 0)
            {
                calc.Value1 *= -1;
                tbx_wyswietlacz.Text = calc.Value1.ToString();
                calc.LastClickIsNumber = true;
            }
        }

        private void btn_clear_Click(object? sender, RoutedEventArgs? e)
        {
            calc.Value1 = 0;
            calc.Result = 0;
            calc.LastValue = 0;
            calc.Op = "";
            calc.LastClickIsNumber = true;
            tbx_wyswietlacz.Text = "";
            newOperation = true;
        }

        private void btn_backspace_Click(object? sender, RoutedEventArgs? e)
        {
            string wys = tbx_wyswietlacz.Text;
            if (!string.IsNullOrEmpty(wys))
            {
                tbx_wyswietlacz.Text = wys.Substring(0, wys.Length - 1);
                double.TryParse(tbx_wyswietlacz.Text, out double parsedValue);
                calc.Value1 = parsedValue;
            }
        }

        private async void btn_root_Click(object? sender, RoutedEventArgs? e)
        {
            if (calc.LastClickIsNumber)
            {
                if (!string.IsNullOrEmpty(calc.Op))
                {
                    calc.calculate();
                    tbx_wyswietlacz.Text = calc.Result.ToString();
                }
                else
                {
                    calc.Result = calc.Value1;
                }
            }
            if (double.TryParse(tbx_wyswietlacz.Text, out double val))
            {
                if (val < 0)
                {
                    await ShowError("Błąd: pierwiastek z ujemnej!");
                    return;
                }
                val = Math.Sqrt(val);
                calc.Result = val;
                tbx_wyswietlacz.Text = calc.Result.ToString();
                calc.LastClickIsNumber = false;
            }
        }
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D0:
                case Key.NumPad0:
                    btn_1_Click(btn_0, null);
                    break;
                case Key.D1:
                case Key.NumPad1:
                    btn_1_Click(btn_1, null);
                    break;
                case Key.D2:
                case Key.NumPad2:
                    btn_1_Click(btn_2, null);
                    break;
                case Key.D3:
                case Key.NumPad3:
                    btn_1_Click(btn_3, null);
                    break;
                case Key.D4:
                case Key.NumPad4:
                    btn_1_Click(btn_4, null);
                    break;
                case Key.D5:
                case Key.NumPad5:
                    btn_1_Click(btn_5, null);
                    break;
                case Key.D6:
                case Key.NumPad6:
                    btn_1_Click(btn_6, null);
                    break;
                case Key.D7:
                case Key.NumPad7:
                    btn_1_Click(btn_7, null);
                    break;
                case Key.D8:
                case Key.NumPad8:
                    btn_1_Click(btn_8, null);
                    break;
                case Key.D9:
                case Key.NumPad9:
                    btn_1_Click(btn_9, null);
                    break;
                case Key.Add:
                case Key.OemPlus when Keyboard.IsKeyDown(Key.LeftShift):
                    btn_2_Click(btn_add, null);
                    break;
                case Key.Subtract:
                case Key.OemMinus:
                    btn_2_Click(btn_substract, null);
                    break;
                case Key.Multiply:
                    btn_2_Click(btn_multiply, null);
                    break;
                case Key.Divide:
                case Key.Oem2:
                    btn_2_Click(btn_divide, null);
                    break;
                case Key.Enter:
                    btn_equals_Click(btn_equals, null);
                    break;
                case Key.Back:
                    btn_backspace_Click(btn_backspace, null);
                    break;
                case Key.Escape:
                    btn_clear_Click(btn_clear, null);
                    break;
                case Key.Decimal:
                case Key.OemComma:
                case Key.OemPeriod:
                    btn_1_Click(btn_dot, null);
                    break;
            }
        }

    }
}
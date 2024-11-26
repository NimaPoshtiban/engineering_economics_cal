using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for Etcwindow.xaml
    /// </summary>
    public partial class Etcwindow : Window
    {
        private double _lastValue = 0;
        public string formula_i { get; set; } = @"\sqrt[n]{f/p} = interest";
        public string formula_ie { get; set; } = @"(1+\frac{r}{m})^{m}-1 = i_{eff}";
        public string formula_iec { get; set; } = @"e^{r}-1 = i_{eff}";
        public string formula_n { get; set; } = @"\left\lceil\frac {\ln(\frac{F}{P})}{\ln(i+1)}\right\rceil = n";



        public Etcwindow()
        {
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(Key.Enter == e.Key)
            {
                if(i.IsSelected)
                {
                    i_button_click(sender, e);
                }
                if(ie.IsSelected)
                {
                    ie_button_click(sender, e);
                }
                if (iec.IsSelected)
                {
                    iec_button_click(sender, e);
                }
                if (n.IsSelected)
                {
                    n_button_click(sender, e);
                }
            }
        }

        private void i_f_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^\-?[0-9]*\.?[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"\{(f|\-?\d*\.?\d*)");
            iformula.Formula = Regex.Replace(iformula.Formula, reg.Pattern, @"{" + tb.Text, RegexOptions.IgnoreCase);
            formula_i = iformula.Formula;
        }

        private void i_n_TextChanged(object sender, TextChangedEventArgs e)
        {
 
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"\[(n|\-?\d*\.?\d*)");
            iformula.Formula = Regex.Replace(iformula.Formula, reg.Pattern, @"[" + tb.Text, RegexOptions.IgnoreCase);
            formula_i = iformula.Formula;
        }

        private void i_p_TextChanged(object sender, TextChangedEventArgs e)
        {

            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^\-?[0-9]*\.?[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"\/(p|\-?\d*\.?\d*)");
            iformula.Formula = Regex.Replace(iformula.Formula, reg.Pattern, @"/" + tb.Text, RegexOptions.IgnoreCase);
            formula_i = iformula.Formula;
        }

        private void i_button_click(object sender, RoutedEventArgs e)
        {
            double.TryParse(i_f.Text, out double f);
            double.TryParse(i_p.Text, out double p);
            int.TryParse(i_n.Text, out int n);
            _lastValue = calculate_interest(f, p, n);
            iresult.Text = _lastValue.ToString("");
            iformula.Formula = formula_i.Replace("interest", iresult.Text);
            formula_i = iformula.Formula;
            ((MainWindow)Application.Current.MainWindow).resultLabel.Content = iresult.Text;
        }

        [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
        [DllImport("C:\\Users\\nima\\source\\repos\\NimaPoshtiban\\Csharp-Projects-CalculatorWPF\\bin\\Debug\\net8.0-windows7.0\\formulas.dll")]
        static extern  double calculate_interest(double f, double p, int n);

        [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
        [DllImport("C:\\Users\\nima\\source\\repos\\NimaPoshtiban\\Csharp-Projects-CalculatorWPF\\bin\\Debug\\net8.0-windows7.0\\formulas.dll")]
        static extern double calculate_effective_interest_rate(double r, double m);

        [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
        [DllImport("C:\\Users\\nima\\source\\repos\\NimaPoshtiban\\Csharp-Projects-CalculatorWPF\\bin\\Debug\\net8.0-windows7.0\\formulas.dll")]
        static extern double calculate_effective_interest_rate_c(double r);


        [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
        [DllImport("C:\\Users\\nima\\source\\repos\\NimaPoshtiban\\Csharp-Projects-CalculatorWPF\\bin\\Debug\\net8.0-windows7.0\\formulas.dll")]
       static  extern int calculate_n(double i, double f, double p);
        private void ie_r_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^\-?[0-9]*\.?[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"c\{(r|\-?\d*\.?\d*)");
            ieformula.Formula = Regex.Replace(ieformula.Formula, reg.Pattern, @"c{" + tb.Text, RegexOptions.IgnoreCase);
            formula_ie = ieformula.Formula;
        }

        private void ie_m_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^\-?[0-9]*\.?[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"\^\{(m|\d+)");
            ieformula.Formula = Regex.Replace(ieformula.Formula, reg.Pattern, @"^{" + tb.Text, RegexOptions.IgnoreCase);
            var reg_2 = new RegularExpressionAttribute(@"\}\{(m|\d+)");
            ieformula.Formula = Regex.Replace(ieformula.Formula, reg_2.Pattern, @"}{" + tb.Text, RegexOptions.IgnoreCase);
            formula_ie = ieformula.Formula;
        }

        private void ie_button_click(object sender, RoutedEventArgs e)
        {
            double.TryParse(ie_r.Text, out double r);
            int.TryParse(ie_m.Text, out int m);
            _lastValue = calculate_effective_interest_rate(r,m);
            ieresult.Text = _lastValue.ToString("");
            ieformula.Formula = formula_ie.Replace("i_{eff}", ieresult.Text);
            formula_ie = iformula.Formula;
            ((MainWindow)Application.Current.MainWindow).resultLabel.Content = ieresult.Text;
        }


   

        private void iec_r_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^\-?[0-9]*\.?[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"\^\{(r|\-?\d*\.?\d*)");
            iecformula.Formula = Regex.Replace(iecformula.Formula, reg.Pattern, @"^{" + tb.Text, RegexOptions.IgnoreCase);
            formula_iec = iecformula.Formula;
        }
        private void iec_button_click(object sender, RoutedEventArgs e)
        {
            double.TryParse(iec_r.Text, out double r);
        
            _lastValue = calculate_effective_interest_rate_c(r);
            iecresult.Text = _lastValue.ToString("");
            iecformula.Formula = formula_iec.Replace("i_{eff}", iecresult.Text);
            formula_iec = iecformula.Formula;
            ((MainWindow)Application.Current.MainWindow).resultLabel.Content = iecresult.Text;
        }

        private void n_f_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^\-?[0-9]*\.?[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"c\{(f|\-?\d*\.?\d*)");
            nformula.Formula = Regex.Replace(nformula.Formula, reg.Pattern, @"c{" + tb.Text, RegexOptions.IgnoreCase);
            formula_n = nformula.Formula;
        }

        private void n_p_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^\-?[0-9]*\.?[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"(P|\-?\d*\.?\d*)\}\)");
            nformula.Formula = Regex.Replace(nformula.Formula, reg.Pattern,  tb.Text+@"})", RegexOptions.IgnoreCase);
            formula_n = nformula.Formula;
        }

        private void n_i_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^\-?[0-9]*\.?[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"\((i|\-?\d*\.?\d*\+)");
            nformula.Formula = Regex.Replace(nformula.Formula, reg.Pattern, "("+tb.Text+@"+", RegexOptions.IgnoreCase);
            formula_n = nformula.Formula;
        }

        private void n_button_click(object sender, RoutedEventArgs e)
        {
            double.TryParse(n_f.Text, out double f);
            double.TryParse(n_p.Text, out double p);
            double.TryParse(n_i.Text, out  double i );
            _lastValue = calculate_n(i, f, p);
            nresult.Text = _lastValue.ToString("");
            nformula.Formula = formula_n.Replace("= n", "= " + nresult.Text);
            formula_n = nformula.Formula;
            ((MainWindow)Application.Current.MainWindow).resultLabel.Content = nresult.Text;
        }
    }
}

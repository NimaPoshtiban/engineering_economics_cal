using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
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
    /// Interaction logic for fwindow.xaml
    /// </summary>
    public partial class Fwindow : Window
    {
        private double _lastValue =0;
        public string formula_f { get; set; } = @"P\cdot(1+i)^{n} = F";
        public string formula_fa { get; set; } = @"A\cdot\left[\frac{(1+i)^{n}-1}{i}\right] = F";
        public string formula_fac { get; set; } = @"A\cdot\left[\frac{(e)^{r\cdot n}-1}{e^{r}-1}\right] = F";
        public string formula_fc { get; set; } = @"P\cdot(e)^{r\cdot n} = F";



        public Fwindow()
        {
            InitializeComponent();
        }

        private void iv_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^\-?[0-9]*\.?[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"\+(i|\-?\d*\.?\d*)");
            f_formula.Formula = Regex.Replace(f_formula.Formula, reg.Pattern, "+" +  tb.Text, RegexOptions.IgnoreCase);
            formula_f = f_formula.Formula;
        }

        private void pv_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^\-?[0-9]*\.?[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"^(P|\-?\d*\.?\d*)");
            f_formula.Formula = Regex.Replace(f_formula.Formula, reg.Pattern, tb.Text, RegexOptions.IgnoreCase);
            formula_f = f_formula.Formula;
        }

        private void nv_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"\^\{(n|\-?\d*\.?\d*)");
            f_formula.Formula = Regex.Replace(f_formula.Formula, reg.Pattern, @"^{"+ tb.Text, RegexOptions.IgnoreCase);
            formula_f = f_formula.Formula;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double.TryParse(iv.Text, out double interest);
            double.TryParse(pv.Text, out double p);
            int.TryParse(nv.Text, out int n);
      

            _lastValue = calculate_future(p, interest, n);
            fr.Text = _lastValue.ToString("");
            f_formula.Formula = formula_f.Replace("F", fr.Text);
            formula_f = f_formula.Formula;
            ((MainWindow)Application.Current.MainWindow).resultLabel.Content = fr.Text;
        }



        private void iva_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^\-?[0-9]*\.?[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"\+(i|\-?\d*\.?\d*)");
            fa_formula.Formula = Regex.Replace(fa_formula.Formula, reg.Pattern, "+" + tb.Text, RegexOptions.IgnoreCase);
            var reg_2 = new RegularExpressionAttribute(@"(i|\-?\d*\.?\d*)\}\\right");
            fa_formula.Formula = Regex.Replace(fa_formula.Formula, reg_2.Pattern, tb.Text + @"}\right", RegexOptions.IgnoreCase);
            formula_fa = fa_formula.Formula;
        }
        private void nva_TextChanged(object sender, TextChangedEventArgs e)
        {

            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"\^\{(n|\-?\d*\.?\d*)");
            fa_formula.Formula = Regex.Replace(fa_formula.Formula, reg.Pattern, @"^{" + tb.Text, RegexOptions.IgnoreCase);
            formula_fa = fa_formula.Formula;
        }
        private void av_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^\-?[0-9]*\.?[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"^(A|\-?\d*\.?\d*)");
            fa_formula.Formula = Regex.Replace(fa_formula.Formula, reg.Pattern, tb.Text, RegexOptions.IgnoreCase);
            formula_fa = fa_formula.Formula;
        }
        private void Button_Click_a(object sender, RoutedEventArgs e)
        {
            double.TryParse(iva.Text, out double interest);
            double.TryParse(av.Text, out double a);
            int.TryParse(nva.Text, out int n);


            _lastValue = calcualate_future_via_annual_uniform(a, interest, n);
            fra.Text = _lastValue.ToString("");
            fa_formula.Formula = formula_fa.Replace("F", fra.Text);
            formula_fa = fa_formula.Formula;
            ((MainWindow)Application.Current.MainWindow).resultLabel.Content = fra.Text;
        }

  
        [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
        [DllImport("formulas.dll")]
        static extern double calculate_future(double p, double i, int n);
        [DllImport("formulas.dll")]
        static extern double calcualate_future_via_annual_uniform(double annual_uniform,
                                                   double interest, int n);
        [DllImport("formulas.dll")]
        static extern double calcualate_future_via_annual_uniform_c(double a, double r, int n);



        private void fanc_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^\-?[0-9]*\.?[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"^(A|\-?\d*\.?\d*)");
            fac_formula.Formula = Regex.Replace(fac_formula.Formula, reg.Pattern, tb.Text, RegexOptions.IgnoreCase);
            formula_fac = fac_formula.Formula;
        }

        private void faic_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^\-?[0-9]*\.?[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"\^\{(r|\-?\d*\.?\d*)\\cdot");
            fac_formula.Formula = Regex.Replace(fac_formula.Formula, reg.Pattern, @"^{" + tb.Text+@"\cdot", RegexOptions.IgnoreCase);
            var reg_2 = new RegularExpressionAttribute(@"\^\{(r|\-?\d*\.?\d*)");
            fac_formula.Formula = Regex.Replace(fac_formula.Formula, reg_2.Pattern, @"^{" + tb.Text , RegexOptions.IgnoreCase);
            formula_fac = fac_formula.Formula;
        }
        private void farc_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"\\cdot(n|\-?\d*\.?\d*)");
            fac_formula.Formula = Regex.Replace(fac_formula.Formula, reg.Pattern, @"\cdot" + tb.Text, RegexOptions.IgnoreCase);
            formula_fac = fac_formula.Formula;
        }
        private void Button_Click_fc(object sender, RoutedEventArgs e)
        {
            double.TryParse(fanc.Text, out double a);
            double.TryParse(faic.Text, out double r);
            int.TryParse(farc.Text, out int n);


            _lastValue = calcualate_future_via_annual_uniform_c(a, r, n);
            frac.Text = _lastValue.ToString("");
            fac_formula.Formula = formula_fac.Replace("F", frac.Text);
            formula_fac = fac_formula.Formula;
            ((MainWindow)Application.Current.MainWindow).resultLabel.Content = frac.Text;
        }
        [DllImport("formulas.dll")]
        static extern  double calculate_future_c(double p, double r, int n);

        private void fcr_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^\-?[0-9]*\.?[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"\^\{(r|\-?\d*\.?\d*)");
            fc_formula.Formula = Regex.Replace(fc_formula.Formula, reg.Pattern, @"^{" + tb.Text, RegexOptions.IgnoreCase);
            formula_fc = fc_formula.Formula;
        }

        private void fcp_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^\-?[0-9]*\.?[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"^(P|\-?\d*\.?\d*)");
            fc_formula.Formula = Regex.Replace(fc_formula.Formula, reg.Pattern, tb.Text, RegexOptions.IgnoreCase);
            formula_fc = fc_formula.Formula;
        }

        private void fcn_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"\\cdot\s(n|\-?\d*\.?\d*)");
            fc_formula.Formula = Regex.Replace(fc_formula.Formula, reg.Pattern, @"\cdot "+tb.Text, RegexOptions.IgnoreCase);
            formula_fc = fc_formula.Formula;
        }

        private void Button_Click_fcc(object sender, RoutedEventArgs e)
        {
            double.TryParse(fcr.Text, out double p);
            double.TryParse(fcp.Text, out double r);
            int.TryParse(fcn.Text, out int n);


            _lastValue = calculate_future_c(p, r, n);
            rfc.Text = _lastValue.ToString("");
            fc_formula.Formula = formula_fc.Replace("F", rfc.Text);
            formula_fc = fc_formula.Formula;
            ((MainWindow)Application.Current.MainWindow).resultLabel.Content = rfc.Text;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Enter == e.Key)
            {
                if (sf.IsSelected)
                {
                    Button_Click(sender, e);
                }
                if (fa.IsSelected)
                {
                    Button_Click_a(sender, e);
                }
                if (fac.IsSelected)
                {
                    Button_Click_fc(sender, e);
                }
                if (fc.IsSelected)
                {
                    Button_Click_fcc(sender, e);
                }
            }
        }

    
    }
}

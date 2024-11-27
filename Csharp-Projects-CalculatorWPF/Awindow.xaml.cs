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
    /// Interaction logic for Awindow.xaml
    /// </summary>
    public partial class Awindow : Window
    {
        private double _lastValue = 0;
        public string formula_ap { get; set; } = @"P\cdot\left[\frac{i\cdot(1+i)^{n}}{(1+i)^{n}-1}\right] = A";
        public string formula_af { get; set; } = @"F\cdot\left[\frac{i}{(1+i)^{n}-1}\right] = A";
        public string formula_ag { get; set; } = @"G\cdot\left[\frac{1}{i}-\frac{n}{(1+i)^{n}-1}\right] = A";
        public string formula_apc { get; set; } = @"P\cdot\left[\frac{ e^{r\cdot n}\cdot (e^{r}-1)}{e^{r\cdot n}-1}\right] = A";
        public string formula_afc { get; set; } = @"F\cdot\left[\frac{e^{r}-1}{e^{r\cdot n}-1}\right] = A";



        public Awindow()
        {
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Enter == e.Key)
            {
                if (avp.IsSelected)
                {
                    ap_button_click(sender, e);
                }
                if (avf.IsSelected)
                {
                    af_button_click(sender, e);
                }
                if (avg.IsSelected)
                {
                    ag_button_click(sender, e);
                }
                if (avpc.IsSelected)
                {
                    apc_button_click(sender, e);
                }
                if (avfc.IsSelected)
                {
                    afc_button_click(sender, e);
                }
                //if (pg.IsSelected)
                //{
                //    Pg_button_click(sender, e);
                //}
                //if (pgeo.IsSelected)
                //{
                //    pgeo_button_click(sender, e);
                //}

                //if (pc.IsSelected)
                //{
                //    pc_button_click(sender, e);
                //}
                //if (pca.IsSelected)
                //{
                //    pca_button_click(sender, e);
                //}
            }
        }
















        [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
        [DllImport("C:\\Users\\nima\\Source\\Repos\\NimaPoshtiban\\Csharp-Projects-CalculatorWPF\\formulas.dll")]
        static extern  double calculate_annual_uniform_via_present(double present,
                                                   double interest, int n);
        [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
        [DllImport("C:\\Users\\nima\\Source\\Repos\\NimaPoshtiban\\Csharp-Projects-CalculatorWPF\\formulas.dll")]
        static  extern  double calculate_annual_uniform_via_future(double future,
                                                          double interest, int n);
        [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
        [DllImport("C:\\Users\\nima\\Source\\Repos\\NimaPoshtiban\\Csharp-Projects-CalculatorWPF\\formulas.dll")]
        static extern  double calculate_annual_uniform_via_gradient(double gradient,
                                                            double interest, int n);
        [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
        [DllImport("C:\\Users\\nima\\Source\\Repos\\NimaPoshtiban\\Csharp-Projects-CalculatorWPF\\formulas.dll")]
        static extern  double calculate_annual_uniform_via_present_c(double p, double r, int n);
        [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
        [DllImport("C:\\Users\\nima\\Source\\Repos\\NimaPoshtiban\\Csharp-Projects-CalculatorWPF\\formulas.dll")]
        static extern double calculate_annual_uniform_via_future_c(double f, double r, int n);
        [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
        [DllImport("C:\\Users\\nima\\Source\\Repos\\NimaPoshtiban\\Csharp-Projects-CalculatorWPF\\formulas.dll")]
        static extern  double calculate_annual_uniform_via_gradient_c(double g, double r,
                                                              int n);
        #region 
        private void ap_changed(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^\-?[0-9]*\.?[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"^(P|\-?\d*\.?\d*)");
            apformula.Formula = Regex.Replace(apformula.Formula, reg.Pattern, tb.Text, RegexOptions.IgnoreCase);
            formula_ap = apformula.Formula;

        }

        private void an_changed(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"\^\{(n|\-?\d*\.?\d*)");
            apformula.Formula = Regex.Replace(apformula.Formula, reg.Pattern, @"^{" +tb.Text, RegexOptions.IgnoreCase);
            formula_ap = apformula.Formula;
        }

        private void ai_changed(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^\-?[0-9]*\.?[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"\{(i|\-?\d*\.?\d*)\\");
            apformula.Formula = Regex.Replace(apformula.Formula, reg.Pattern, "{" + tb.Text+"\\", RegexOptions.IgnoreCase);
            var reg_2 = new RegularExpressionAttribute(@"\+(i|\-?\d*\.?\d*)");
            apformula.Formula = Regex.Replace(apformula.Formula,  reg_2.Pattern , "+" + tb.Text, RegexOptions.IgnoreCase);
            formula_ap = apformula.Formula;
        }

        private void ap_button_click(object sender, RoutedEventArgs e)
        {
            double.TryParse(ai.Text, out double interest);
            double.TryParse(ap.Text, out double p);
            int.TryParse(an.Text, out int n);
            _lastValue = calculate_annual_uniform_via_present(p, interest, n);
            afpresult.Text = _lastValue.ToString("");
            apformula.Formula = formula_ap.Replace("A", afpresult.Text);
            formula_ap = apformula.Formula;
            ((MainWindow)Application.Current.MainWindow).resultLabel.Content = afpresult.Text;
        }

        #endregion
        #region
        private void af_changed(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^\-?[0-9]*\.?[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"^(F|\-?\d*\.?\d*)");
            afformula.Formula = Regex.Replace(afformula.Formula, reg.Pattern, tb.Text, RegexOptions.IgnoreCase);
            formula_af = afformula.Formula;

        }

        private void afn_changed(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"\^\{(n|\-?\d*\.?\d*)");
            afformula.Formula = Regex.Replace(afformula.Formula, reg.Pattern, @"^{" + tb.Text, RegexOptions.IgnoreCase);
            formula_af = afformula.Formula;
        }

        private void afi_changed(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^\-?[0-9]*\.?[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"c\{(i|\-?\d*\.?\d*)");
            afformula.Formula = Regex.Replace(afformula.Formula, reg.Pattern, @"c{" + tb.Text , RegexOptions.IgnoreCase);
            var reg_2 = new RegularExpressionAttribute(@"\+(i|\-?\d*\.?\d*)");
            afformula.Formula = Regex.Replace(afformula.Formula, reg_2.Pattern, "+" + tb.Text, RegexOptions.IgnoreCase);
            formula_af = afformula.Formula;
        }

        private void af_button_click(object sender, RoutedEventArgs e)
        {
            double.TryParse(afi.Text, out double interest);
            double.TryParse(af.Text, out double f);
            int.TryParse(afn.Text, out int n);
            _lastValue = calculate_annual_uniform_via_future(f, interest, n);
            afresult.Text = _lastValue.ToString("");
            afformula.Formula = formula_af.Replace("A", afresult.Text);
            formula_af = afformula.Formula;
            ((MainWindow)Application.Current.MainWindow).resultLabel.Content = afresult.Text;
        }

        #endregion
        #region

        private void ag_changed(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^\-?[0-9]*\.?[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"^(G|\-?\d*\.?\d*)");
            agformula.Formula = Regex.Replace(agformula.Formula, reg.Pattern, tb.Text, RegexOptions.IgnoreCase);
            formula_ag = agformula.Formula;

        }

        private void agn_changed(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"\^\{(n|\-?\d*\.?\d*)");
            agformula.Formula = Regex.Replace(agformula.Formula, reg.Pattern, @"^{" + tb.Text, RegexOptions.IgnoreCase);
            var reg_2 = new RegularExpressionAttribute(@"\-\\frac\{(n|\-?\d*\.?\d*)");
            agformula.Formula = Regex.Replace(agformula.Formula, reg_2.Pattern, @"-\frac{" + tb.Text, RegexOptions.IgnoreCase);
            formula_ag = agformula.Formula;
        }

        private void agi_changed(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^\-?[0-9]*\.?[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"\}\{(i|\-?\d*\.?\d*)\}");
            agformula.Formula = Regex.Replace(agformula.Formula, reg.Pattern, @"}{" + tb.Text +"}", RegexOptions.IgnoreCase);
            var reg_2 = new RegularExpressionAttribute(@"\+(i|\-?\d*\.?\d*)");
            agformula.Formula = Regex.Replace(agformula.Formula, reg_2.Pattern, "+" + tb.Text, RegexOptions.IgnoreCase);
            formula_ag = agformula.Formula;
        }

        private void ag_button_click(object sender, RoutedEventArgs e)
        {
            double.TryParse(agi.Text, out double interest);
            double.TryParse(ag.Text, out double g);
            int.TryParse(agn.Text, out int n);
            _lastValue = calculate_annual_uniform_via_gradient(g, interest, n);
            agresult.Text = _lastValue.ToString("");
            agformula.Formula = formula_ag.Replace("A", agresult.Text);
            formula_ag = agformula.Formula;
            ((MainWindow)Application.Current.MainWindow).resultLabel.Content = agresult.Text;
        }
        #endregion


        #region
        private void apc_changed(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^\-?[0-9]*\.?[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"^(P|\-?\d*\.?\d*)");
            apcformula.Formula = Regex.Replace(apcformula.Formula, reg.Pattern, tb.Text, RegexOptions.IgnoreCase);
            formula_apc = apcformula.Formula;

        }

        private void apcn_changed(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"\\cdot (n|\-?\d*\.?\d*)\}");
            apcformula.Formula = Regex.Replace(apcformula.Formula, reg.Pattern, @"\cdot "+tb.Text+"}", RegexOptions.IgnoreCase);
            formula_apc = apcformula.Formula;
        }

        private void apcr_changed(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^\-?[0-9]*\.?[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"\^\{(r|\-?\d*\.?\d*)");
            apcformula.Formula = Regex.Replace(apcformula.Formula, reg.Pattern, "^{" + tb.Text, RegexOptions.IgnoreCase);
            formula_apc = apcformula.Formula;
        }

        private void apc_button_click(object sender, RoutedEventArgs e)
        {
            double.TryParse(apcr.Text, out double r);
            double.TryParse(apc.Text, out double p);
            int.TryParse(apcn.Text, out int n);
            _lastValue = calculate_annual_uniform_via_present_c(p, r, n);
            apcresult.Text = _lastValue.ToString("");
            apcformula.Formula = formula_apc.Replace("A", apcresult.Text);
            formula_apc = apcformula.Formula;
            ((MainWindow)Application.Current.MainWindow).resultLabel.Content = apcresult.Text;
        }
        #endregion

        #region
        private void afc_changed(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^\-?[0-9]*\.?[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"^(F|\-?\d*\.?\d*)");
            afcformula.Formula = Regex.Replace(afcformula.Formula, reg.Pattern, tb.Text, RegexOptions.IgnoreCase);
            formula_afc = afcformula.Formula;

        }

        private void afcn_changed(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"\\cdot (n|\-?\d*\.?\d*)\}");
            afcformula.Formula = Regex.Replace(afcformula.Formula, reg.Pattern, @"\cdot " + tb.Text + "}", RegexOptions.IgnoreCase);
            formula_afc = afcformula.Formula;
        }

        private void afcr_changed(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^\-?[0-9]*\.?[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"\^\{(r|\-?\d*\.?\d*)");
            afcformula.Formula = Regex.Replace(afcformula.Formula, reg.Pattern, "^{" + tb.Text, RegexOptions.IgnoreCase);
            formula_afc = afcformula.Formula;
        }

        private void afc_button_click(object sender, RoutedEventArgs e)
        {
            double.TryParse(afcr.Text, out double r);
            double.TryParse(afc.Text, out double p);
            int.TryParse(afcn.Text, out int n);
            _lastValue = calculate_annual_uniform_via_future_c(p, r, n);
            afcresult.Text = _lastValue.ToString("");
            afcformula.Formula = formula_afc.Replace("A", afcresult.Text);
            formula_afc = afcformula.Formula;
            ((MainWindow)Application.Current.MainWindow).resultLabel.Content = afcresult.Text;
        }
        #endregion


    }
}

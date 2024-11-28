using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for Pwindow.xaml
    /// </summary>
    public partial class Pwindow : Window
    {
        private double _lastValue = 0;

        public string formula { get; set; } = @"\sum_{n=start}^{end}{A\cdot\left[\frac{1}{\left(1+i\right)^{n}}\right]} = P";
        public string formula_pg { get; set; } = @"A\cdot\left[\frac{(1+i)^{n}-1}{i\cdot(1+i)^{n}}\right] + \frac{G}{i}\cdot\left[\frac{(1+i)^{n}-1}{i\cdot(1+i)^{n}} - \frac{n}{(1+i)^{n}}\right] = P_{T}";
        public string formula_geo { get; set; } = @"A_{1}\cdot\left[\frac{1-(1+g)^{n}\cdot(1+i)^{-n}}{i-g}\right] = P";

        public string formula_pc { get; set; } = @"\frac{F}{(e)^{r\cdot n}} = P";
        public string formula_pca { get; set; } = @"A\cdot\left[\frac{e^{r\cdot n}-1}{e^{r\cdot n}\cdot e^{r}-1}\right] = P";


        public Pwindow()
        {
            InitializeComponent();

        }

        private void pcal_Click(object sender, RoutedEventArgs e)
        {
            double.TryParse(pi.Text, out double i);
            int.TryParse(ps.Text, out int start);
            int.TryParse(pe.Text, out int end);
            double.TryParse(pa.Text, out double a);
            _lastValue = calculate_summation(start, end, i, a);
            presult.Text = _lastValue.ToString("");
            sump.Formula = formula.Replace("P", presult.Text);
            formula = sump.Formula;
            ((MainWindow)Application.Current.MainWindow).resultLabel.Content = presult.Text;
        }

        private void sp_text_changed(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;

            tb.Text = Regex.Replace(tb.Text, @"\D+", "0", RegexOptions.Multiline);

            var reg = new RegularExpressionAttribute(@"n=(\w+|\d+)?");
            sump.Formula = Regex.Replace(sump.Formula, reg.Pattern, "n=" + tb.Text, RegexOptions.IgnoreCase);
            formula = sump.Formula;
        }

        private void ep_text_changed(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            tb.Text = Regex.Replace(tb.Text, @"\D+", "0", RegexOptions.Multiline);

            var reg = new RegularExpressionAttribute(@"(\^{(end|\d*)})");

            sump.Formula = Regex.Replace(sump.Formula, reg.Pattern, "^{" + tb.Text + "}", RegexOptions.IgnoreCase);
            formula = sump.Formula;
        }

        private void pa_text_changed(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            tb.Text = Regex.Replace(tb.Text, @"D*[^\.|\d|\-]D*", "0", RegexOptions.Multiline);
            var reg = new RegularExpressionAttribute(@"(\{A)\}|(\{\-?\d*.?\d*\\cdot)");
            sump.Formula = Regex.Replace(sump.Formula, reg.Pattern, "{" + tb.Text + @"\cdot", RegexOptions.IgnoreCase);
            formula = sump.Formula;
        }


        private void pi_text_changed(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            tb.Text = Regex.Replace(tb.Text, @"D*[^\.|\d|\-]D*", "0", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            var reg = new RegularExpressionAttribute(@"(\+i\\right|\+\-?\d*\.?\d*\\right)");
            sump.Formula = Regex.Replace(sump.Formula, reg.Pattern, "+" + tb.Text + @"\right", RegexOptions.IgnoreCase);
            formula = sump.Formula;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Enter == e.Key)
            {
                if (p.IsSelected)
                {
                    pcal_Click(sender, e);
                }
                if (pg.IsSelected)
                {
                    Pg_button_click(sender, e);
                }
                if (pgeo.IsSelected)
                {
                    pgeo_button_click(sender, e);
                }

                if (pc.IsSelected)
                {
                    pc_button_click(sender, e);
                }
                if (pca.IsSelected)
                {
                    pca_button_click(sender, e);
                }
            }
        }

        private void Pg_button_click(object sender, RoutedEventArgs e)
        {
            double.TryParse(pti.Text, out double i);
            double.TryParse(pta.Text, out double a);
            double.TryParse(ptg.Text, out double g);
            int.TryParse(ptn.Text, out int n);
            _lastValue = calculate_summation_with_gradient(g, a, i, n);
            ptresult.Text = _lastValue.ToString("");
            pgoformula.Formula = formula_pg.Replace("P_{T}", ptresult.Text);
            formula_pg = pgoformula.Formula;
            ((MainWindow)Application.Current.MainWindow).resultLabel.Content = ptresult.Text;
        }


        private void pti_text_changed(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.Replace(tb.Text, @"(D*[^\.|\d|\-]D*)", "0", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            var reg = new RegularExpressionAttribute(@"(\+i\)|\+\-?\d*\.?\d*\))");
            pgoformula.Formula = Regex.Replace(pgoformula.Formula, reg.Pattern, "+" + tb.Text + @")", RegexOptions.IgnoreCase);
            var reg_2 = new RegularExpressionAttribute(@"(i\\cdot)|(\-?\d+\.?\d*\\cdot)");
            pgoformula.Formula = Regex.Replace(pgoformula.Formula, reg_2.Pattern, tb.Text + @"\cdot", RegexOptions.IgnoreCase);
            var reg_3 = new RegularExpressionAttribute(@"(\{i\}\\cdot)|(\{\-?\d*\.?\d*\}\\cdot)");
            pgoformula.Formula = Regex.Replace(pgoformula.Formula, reg_3.Pattern, "{" + tb.Text + @"}\cdot", RegexOptions.IgnoreCase);
            formula_pg = pgoformula.Formula;
        }

        private void pta_text_changed(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            tb.Text = Regex.Replace(tb.Text, @"D*[^\.|\d|\-]D*", "0", RegexOptions.Multiline);
            var reg = new RegularExpressionAttribute(@"(^A\\)|(^\-?\d*\.?\d*\\)");
            pgoformula.Formula = Regex.Replace(pgoformula.Formula, reg.Pattern, tb.Text + "\\", RegexOptions.IgnoreCase);
            formula_pg = pgoformula.Formula;
        }

        private void ptg_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            tb.Text = Regex.Replace(tb.Text, @"D*[^\.|\d|\-]D*", "0", RegexOptions.Multiline);
            var reg = new RegularExpressionAttribute(@"(\+\s*\\frac\{G\})|(\+\s*\\frac\{\-*\d*\.?\d*\})");
            pgoformula.Formula = Regex.Replace(pgoformula.Formula, reg.Pattern, "+ \\frac{" + tb.Text + "}", RegexOptions.IgnoreCase);
            formula_pg = pgoformula.Formula;
        }

        private void ptn_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.Replace(tb.Text, @"\D+", "0", RegexOptions.Multiline);

            var reg = new RegularExpressionAttribute(@"(\^\{n\})|(\^\{\-*\d*\.*\d*\})");
            pgoformula.Formula = Regex.Replace(pgoformula.Formula, reg.Pattern, "^{" + tb.Text + "}", RegexOptions.IgnoreCase);
            var reg_2 = new RegularExpressionAttribute(@"(\-\s*\\frac\{n\})|(\-\s*\\frac\{\-*\d*\.*\d*\})");
            pgoformula.Formula = Regex.Replace(pgoformula.Formula, reg_2.Pattern, "- \\frac{" + tb.Text + "}", RegexOptions.IgnoreCase);
            formula_pg = pgoformula.Formula;
        }

        private void pgeo_button_click(object sender, RoutedEventArgs e)
        {
            double.TryParse(pgeoi.Text, out double i);
            double.TryParse(pgeoa.Text, out double a);
            double.TryParse(pgeog.Text, out double g);
            int.TryParse(pgeon.Text, out int n);
            if (i == g)
            {
                MessageBox.Show("i cannot be equal to g", "Invalid operation", MessageBoxButton.OK, MessageBoxImage.Error);
                ((MainWindow)Application.Current.MainWindow).resultLabel.Content = "Error";
            }
            _lastValue = calculate_present_via_geometric_gradient(a, i, g, n);
            pgeoresult.Text = _lastValue.ToString("");
            geoformula.Formula = formula_geo.Replace("P", pgeoresult.Text);
            formula_geo = geoformula.Formula;
            ((MainWindow)Application.Current.MainWindow).resultLabel.Content = pgeoresult.Text;
        }

        private void pgeoi_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.Replace(tb.Text, @"D*[^\.|\d|\-]D*", "", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            var reg = new RegularExpressionAttribute(@"\}\}\{(i|\-*\d*\.*\d*)");
            geoformula.Formula = Regex.Replace(geoformula.Formula, reg.Pattern, "}}{" + tb.Text, RegexOptions.IgnoreCase);
            var reg_2 = new RegularExpressionAttribute(@"\\cdot\((1\+i|\-*\d*\.?\d*)");
            geoformula.Formula = Regex.Replace(geoformula.Formula, reg_2.Pattern, @"\cdot(" + tb.Text, RegexOptions.IgnoreCase);
            formula_geo = geoformula.Formula;
        }

        private void pgeoa_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            tb.Text = Regex.Replace(tb.Text, @"D*[^\.|\d|\-]D*", "", RegexOptions.Multiline);
            var reg = new RegularExpressionAttribute(@"(^A_\{1}|^\-*\d*\.*\d*)");
            geoformula.Formula = Regex.Replace(geoformula.Formula, reg.Pattern, tb.Text, RegexOptions.IgnoreCase);
            formula_geo = geoformula.Formula;
        }

        private void pgeog_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.Replace(tb.Text, @"D*[^&\.|&\d|&\-]D*", "0", RegexOptions.Multiline);
            var reg = new RegularExpressionAttribute(@"\-\(1(\+g|\+\-?\d*\.?\d*)");
            geoformula.Formula = Regex.Replace(geoformula.Formula, reg.Pattern, "-(1+" + tb.Text, RegexOptions.IgnoreCase);
            var reg_2 = new RegularExpressionAttribute(@"\-(g|\-?\d*\.?\d*)\}\\");
            geoformula.Formula = Regex.Replace(geoformula.Formula, reg_2.Pattern, @"-" + tb.Text + @"}\", RegexOptions.IgnoreCase);

            formula_geo = geoformula.Formula;
        }

        private void pgeon_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^\-?[0-9]*\.?[0-9]*$") ? tb.Text : "0";

            var reg = new RegularExpressionAttribute(@"(\^\{n\})|(\^\{\d*\.?\d*\})");
            geoformula.Formula = Regex.Replace(geoformula.Formula, reg.Pattern, "^{" + tb.Text + "}", RegexOptions.IgnoreCase);
            var reg_2 = new RegularExpressionAttribute(@"(\^\{\-n\})|(\^\{\-\d*\.?\d*\})");
            geoformula.Formula = Regex.Replace(geoformula.Formula, reg_2.Pattern, @"^{-" + tb.Text + "}", RegexOptions.IgnoreCase);
            formula_geo = geoformula.Formula;
        }


        private void pcr_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^\-?[0-9]*\.?[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"(r|\-?\d*\.?\d*)\\cdot");
            pcformula.Formula = Regex.Replace(pcformula.Formula, reg.Pattern,  tb.Text+@"\cdot", RegexOptions.IgnoreCase);
            formula_pc = pcformula.Formula;
        }

        private void pcn_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^\-?[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"(n|\-?\d*\.?\d*)\}\}");
            pcformula.Formula = Regex.Replace(pcformula.Formula, reg.Pattern,   tb.Text+"}}", RegexOptions.IgnoreCase);
            formula_pc = pcformula.Formula;
        }

        private void pcf_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^\-?[0-9]*\.?[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"c\{(F|\-?\d*\.?\d*)");
            pcformula.Formula = Regex.Replace(pcformula.Formula, reg.Pattern, "c{"+ tb.Text , RegexOptions.IgnoreCase);
            formula_pc = pcformula.Formula;
        }

        private void pc_button_click(object sender, RoutedEventArgs e)
        {
            double.TryParse(pcr.Text, out double r);
            int.TryParse(pcn.Text, out int n);
            double.TryParse(pcf.Text, out double f);
 
            _lastValue = calculate_present_c(f,r,n);
            pcresult.Text = _lastValue.ToString("");
            pcformula.Formula = formula_pc.Replace("P", pcresult.Text);
            formula_pc = pcformula.Formula;
            ((MainWindow)Application.Current.MainWindow).resultLabel.Content = pcresult.Text;
        }


        private void pcar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^\-?[0-9]*\.?[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"\^\{(r|\-?\d*\.?\d*)");
            pcaformula.Formula = Regex.Replace(pcaformula.Formula, reg.Pattern, "^{"+tb.Text , RegexOptions.IgnoreCase);
            formula_pca = pcaformula.Formula;
        }

        

        private void pcan_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^\-?[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"(\\cdot\sn|\\cdot\s\-?\d+\.?\d*)");
            pcaformula.Formula = Regex.Replace(pcaformula.Formula, reg.Pattern, @"\cdot " + tb.Text , RegexOptions.IgnoreCase);
            formula_pca = pcaformula.Formula;
        }

        private void pcaa_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (String.IsNullOrEmpty(tb.Text))
            {
                tb.Text = "0";
            }
            tb.Text = Regex.IsMatch(tb.Text, @"^\-?[0-9]*\.?[0-9]*$") ? tb.Text : "0";
            var reg = new RegularExpressionAttribute(@"^(A|\-?\d*\.?\d*)");
            pcaformula.Formula = Regex.Replace(pcaformula.Formula, reg.Pattern,tb.Text, RegexOptions.IgnoreCase);
            formula_pca = pcaformula.Formula;
        }

        private void pca_button_click(object sender, RoutedEventArgs e)
        {
            double.TryParse(pcar.Text, out double r);
            int.TryParse(pcan.Text, out int n);
            double.TryParse(pcaa.Text, out double a);

            _lastValue = calculate_present_via_annual_uniform_c(a, r, n);
            pcaresult.Text = _lastValue.ToString("");
            pcaformula.Formula = formula_pca.Replace("P", pcaresult.Text);
            formula_pca = pcaformula.Formula;
            ((MainWindow)Application.Current.MainWindow).resultLabel.Content = pcaresult.Text;
        }

        [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
        [DllImport("formulas.dll")]
        static extern double calculate_summation(int start, int end, double interest,
                             double annual_uniform);
        [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
        [DllImport("formulas.dll")]
        static extern double calculate_summation_with_gradient(double gradient, double annual_uniform, double interest, int n);

        [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
        [DllImport("formulas.dll")]
        static extern double calculate_present_via_geometric_gradient(double a1, double i, double g, int n);
        [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
        [DllImport("formulas.dll")]
        static extern double calculate_present_c(double f, double r, double n);
        [DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory)]
        [DllImport("formulas.dll")]
        static extern double calculate_present_via_annual_uniform_c(double a, double r, int n);

     
    }
}

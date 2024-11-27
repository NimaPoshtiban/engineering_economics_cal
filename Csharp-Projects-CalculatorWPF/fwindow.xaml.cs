using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
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
        public Fwindow()
        {
            InitializeComponent();
        }
      

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double.TryParse(iv.Text, out double interest);
            double.TryParse(pv.Text, out double p);
            int.TryParse(nv.Text, out int n);
      

            _lastValue = calculate_future(p, interest, n);
            fr.Text = _lastValue.ToString("");
            ((MainWindow)Application.Current.MainWindow).resultLabel.Content = fr.Text;
        }
       

        private void Button_Click_a(object sender, RoutedEventArgs e)
        {
            double.TryParse(iva.Text, out double interest);
            double.TryParse(av.Text, out double a);
            int.TryParse(nva.Text, out int n);


            _lastValue = calcualate_future_via_annual_uniform(a, interest, n);
            fra.Text = _lastValue.ToString("");
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
        private void Button_Click_fc(object sender, RoutedEventArgs e)
        {
            double.TryParse(fanc.Text, out double a);
            double.TryParse(farc.Text, out double r);
            int.TryParse(fanc.Text, out int n);


            _lastValue = calcualate_future_via_annual_uniform_c(a, r, n);
            frac.Text = _lastValue.ToString("");
            ((MainWindow)Application.Current.MainWindow).resultLabel.Content = frac.Text;
        }
        [DllImport("formulas.dll")]
        static extern   double calculate_future_c(double p, double r, int n);
        private void Button_Click_fcc(object sender, RoutedEventArgs e)
        {
            double.TryParse(fanc.Text, out double p);
            double.TryParse(farc.Text, out double r);
            int.TryParse(fanc.Text, out int n);


            _lastValue = calculate_future_c(p, r, n);
            rfc.Text = _lastValue.ToString("");
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

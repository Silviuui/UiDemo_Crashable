using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace UiDemo
{
    /// <summary>
    /// Interaction logic for CashCount.xaml
    /// </summary>
    public partial class CashCount : Window
    {
        public static bool isOpen = false;
        private double total = 0.0;

        public double getTotal()
        {
            return total;
        }

        public CashCount()
        {
           InitializeComponent();
        }


        private void Textfield_Focus_Changed(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (!(tb.Text == ""))
            {
                tb.Text = tb.Text + ".00";
            }

            update_total();
        }

        public void update_total()
        {
            double field1A, field5A, field10A, field20A, field50A, field100A, field1Ac, field5Ac, field10Ac, field25Ac;

            try
            {
                field1A = Double.Parse(DollarPiece1.Text, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (System.Exception e)
            {
                field1A = 0;
            }
            try
            {
                field5A = Double.Parse(DollarPiece5.Text, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (System.Exception e)
            {
                field5A = 0;
            }
            try
            {
                field10A = Double.Parse(DollarPiece10.Text, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (System.Exception e)
            {
                field10A = 0;
            }
            try
            {
                field20A = Double.Parse(DollarPiece20.Text, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (System.Exception e)
            {
                field20A = 0;
            }
            try
            {
                field50A = Double.Parse(DollarPiece50.Text, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (System.Exception e)
            {
                field50A = 0;
            }
            try
            {
                field100A = Double.Parse(DollarPiece100.Text, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (System.Exception e)
            {
                field100A = 0;
            }
            try
            {
                field1Ac = Double.Parse(CentPiece1.Text, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (System.Exception e)
            {
                field1Ac = 0;
            }
            try
            {
                field5Ac = Double.Parse(CentPiece5.Text, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (System.Exception e)
            {
                field5Ac = 0;
            }
            try
            {
                field10Ac = Double.Parse(CentPiece10.Text, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (System.Exception e)
            {
                field10Ac = 0;
            }
            try
            {
                field25Ac = Double.Parse(CentPiece25.Text, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (System.Exception e)
            {
                field25Ac = 0;
            }

            total = field1A + 5*field5A + 10*field10A + 20*field20A + 50*field50A + 100*field100A + 0.01*field1Ac + 0.05*field5Ac + 0.1*field10Ac + 0.25*field25Ac;

            TotalLB.Text = "" + (Math.Round((total), 2)).ToString();

            if (!TotalLB.Text.ToString().Contains("."))
            {
                TotalLB.Text = "$" + TotalLB.Text + ".00";
            }
        }

        
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}

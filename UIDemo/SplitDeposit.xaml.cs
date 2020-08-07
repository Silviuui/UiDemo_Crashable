using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UiDemo
{
    /// <summary>
    /// Interaction logic for SplitDeposit.xaml
    /// </summary>
    public partial class SplitDeposit : UserControl
    {
        public SplitDeposit()
        {
            InitializeComponent();
            accountnrlabel.Content = accountnumber.ToString();
            transnrlabel.Content = transactionnumber.ToString();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        //Function to get random number
        private static readonly Random getrandom = new Random();
        private static readonly object syncLock = new object();
        public static int GetRandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return getrandom.Next(min, max);
            }
        }

        private void Textfield_Focus_Changed(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            if (!tb.Text.Contains("$") && !(tb.Text == ""))
            {
                tb.Text = "$" + tb.Text + ".00";
            }

            update_total();

        }

        public void update_total()
        {
            double field1, field2, field3;
            try
            {
                field1 = Double.Parse(cashintb.Text.Substring(1), System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (System.Exception e)
            {
                field1 = 0;
            }

            try
            {
                field2 = Double.Parse(onustb.Text.Substring(1), System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (System.Exception e)
            {
                field2 = 0;
            }

            try
            {
                field3 = Double.Parse(notonustb.Text.Substring(1), System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (System.Exception e)
            {
                field3 = 0;
            }

            TotalLB.Content = "$" + (Math.Round((field1 + field2 + field3), 2)).ToString();

            if (!TotalLB.Content.ToString().Contains("."))
            {
                TotalLB.Content = TotalLB.Content + ".00";
            }





        }

        int accountnumber = GetRandomNumber(100000000, 999999999);
        int transactionnumber = GetRandomNumber(100000, 999999);

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //cancel
            cashintb.Text = "";
            onustb.Text = "";
            notonustb.Text = "";

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //accept
            transactionnumber++;
            transnrlabel.Content = transactionnumber.ToString();

            cashintb.Text = "";
            onustb.Text = "";
            notonustb.Text = "";
        }


        public bool enablebutonel
        {
            get
            {

                return true;

            }
            set
            {
                CashC.IsEnabled = value;
                CashC_Copy.IsEnabled = value;
            }
        }
    }
}

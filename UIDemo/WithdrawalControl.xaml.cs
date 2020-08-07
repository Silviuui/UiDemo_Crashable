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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UiDemo
{
    /// <summary>
    /// Interaction logic for WithdrawalControl.xaml
    /// </summary>
    public partial class WithdrawalControl : UserControl
    {
        public WithdrawalControl()
        {
            InitializeComponent();
            accountnrlabel.Content = accountnumber.ToString();
            transnrlabel.Content = transactionnumber.ToString();
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

   

        int accountnumber = GetRandomNumber(100000000, 999999999);
        int transactionnumber = GetRandomNumber(100000, 999999);


        public bool enablebutonel
        {
            get
            {

                return true;

            }
            set
            {
                CashC.IsEnabled = value;
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
            double field1, field2;
            try
            {
                field1 = Double.Parse(check1.Text.Substring(1), System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (System.Exception e)
            {
                field1 = 0;
            }

            try
            {
                field2 = Double.Parse(check2.Text.Substring(1), System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (System.Exception e)
            {
                field2 = 0;
            }

           

            TotalLB.Content = "$" + (Math.Round((field1 + field2), 2)).ToString();

            if (!TotalLB.Content.ToString().Contains("."))
            {
                TotalLB.Content = TotalLB.Content + ".00";
            }

            TotalLB2.Content = TotalLB.Content;






        }

    }
}

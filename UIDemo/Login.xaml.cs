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

namespace UIDouble
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            System.Windows.Application.Current.Shutdown();
            //do my stuff before closing
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (user.Text.Contains("admin") && pass.Password.Contains("password"))
            {
                //this.Close();
                this.Hide();
            }
            else
            {
                badlogin.Visibility = System.Windows.Visibility.Visible;
            }
        }
    }
}

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
using System.Configuration;
using System.Text.RegularExpressions;

namespace UiDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        int accountnumber = GetRandomNumber(100000000,999999999);
        int transactionnumber = GetRandomNumber(100000, 999999);

        DepositControl depositc = new DepositControl(false);
        WithdrawalControl withdrawalc = new WithdrawalControl();
        SplitDeposit splitd = new SplitDeposit();

        public MainWindow()
        {
            InitializeComponent();


            this.ContentControl1.Content = depositc;
            
        

            DateLabel.Content = DateTime.Now.ToString("MM/dd/yyyy");
            TimeLabel.Content = DateTime.Now.ToString("hh:mm:ss tt");
            UpdateTimeDelay();


            //this.ContentControl1.accountnrlabel.Content = accountnumber.ToString();


            var Loginpage = new UIDouble.Login();

            Loginpage.ShowDialog();

            LoadDefaultValues();
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



        //update time every second
        public async Task UpdateTimeDelay()
        {
            await Task.Delay(1000);
            TimeLabel.Content = DateTime.Now.ToString("hh:mm:ss tt");
            UpdateTimeDelay();
        }


        //value for scale changed
        private void Slider_ValueChanged(object sender,
           RoutedPropertyChangedEventArgs<double> e)
        {
            // ... Get Slider reference.
            var slider = sender as Slider;
            // ... Get Value.
            double value = slider.Value;
            // ... Set Window Title.

            //this.Title = "Value: " + value.ToString("0.0") + "/" + slider.Maximum;
        }



        private void Deposit_Checked(object sender, RoutedEventArgs e)
        {

            try
            {

                this.ContentControl1.Content = depositc;
            }

            catch (Exception exc)
            {
            }
            

            //Handle(sender as CheckBox);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //set title


            this.Title = this.Title + " "+ DateTime.Now.ToString();
        }


        private void CheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            //set title


            this.Title = "UiDemo";
        } 
        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            //butonel enabled

            CashCountBorder.IsEnabled = true;

            
            if (this.ContentControl1.Content.GetType() == typeof(DepositControl))
            {
                depositc.enablebutonel = true;
            }
            if (this.ContentControl1.Content.GetType() == typeof(SplitDeposit))
            {
                splitd.enablebutonel = true;
            }
            if (this.ContentControl1.Content.GetType() == typeof(WithdrawalControl))
            {
                withdrawalc.enablebutonel = true;
            }
        }

        private void CheckBox_UnChecked_1(object sender, RoutedEventArgs e)
        {
            //butonel disabled
            CashCountBorder.IsEnabled = false;

            if (this.ContentControl1.Content.GetType() == typeof(DepositControl))
            {
                depositc.enablebutonel = false;
            }

            if (this.ContentControl1.Content.GetType() == typeof(SplitDeposit))
            {
                splitd.enablebutonel = false;
            }

            if (this.ContentControl1.Content.GetType() == typeof(WithdrawalControl))
            {
                withdrawalc.enablebutonel = false;
            }
        }

        private void Withdrawalradiobutton_Checked(object sender, RoutedEventArgs e)
        {
            this.ContentControl1.Content = withdrawalc;
        }


        private void Splitradiobutton_Checked(object sender, RoutedEventArgs e)
        {
            this.ContentControl1.Content = splitd;
        }

        private void graphLabel_Checked(object sender, RoutedEventArgs e)
        {
            MainTitle.Content = "[Ui]Path";
        }

        private void graphLabel_Unchecked(object sender, RoutedEventArgs e)
        {
            MainTitle.Content = "UiPath";
        }

        private void additionalitem_Checked(object sender, RoutedEventArgs e)
        {
            additionallogo.Visibility = Visibility.Visible;
        }

        private void additionalitem_UnChecked(object sender, RoutedEventArgs e)
        {
            additionallogo.Visibility = Visibility.Hidden;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        #region Load / Save values from App Configuration

        private void UpdateAppConfiguration()
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>
            {
                {"EnableCrashSimulation", EnableCrashSimulation.IsChecked.Value.ToString()},
                {"CrashMethod", CrashMethod.Text },
                {"CrashRate", CrashRateTextBox.Text }
            };

            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;

                foreach (KeyValuePair<string, string> pair in keyValuePairs)
                {
                    if (settings[pair.Key] == null)
                    {
                        settings.Add(pair.Key, pair.Value);
                    }
                    else
                    {
                        settings[pair.Key].Value = pair.Value;
                    }
                }

                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

        // Load the default values from app.config
        private void LoadDefaultValues()
        {
            bool enableCrashSimulation = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableCrashSimulation"]);
            string crashType = ConfigurationManager.AppSettings["CrashMethod"];
            string crashRate = ConfigurationManager.AppSettings["CrashRate"];

            EnableCrashSimulation.IsChecked = enableCrashSimulation;           
            CrashMethod.SelectedValue = crashType;
            CrashRateTextBox.Text = crashRate;
        }

        #endregion


        #region Crash Simulator implementation

        private void EnableCrashSimulation_Checked(object sender, RoutedEventArgs e)
        {
            if (this.IsInitialized)
                UpdateCrashSimulator();
        }

        private void EnableCrashSimulation_UnChecked(object sender, RoutedEventArgs e)
        {
            if (this.IsInitialized)
                UpdateCrashSimulator();
        }

        private void CrashMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsInitialized)
                UpdateCrashSimulator();
        }

        private void CrashRateTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.IsInitialized)
                UpdateCrashSimulator();
        }

        // Allow only digits to be entered into the CrashRateTextBox
        private static readonly Regex _regex = new Regex("[0-9]+");
        private void CrashRateTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !_regex.IsMatch(e.Text);
        }

        private void UpdateCrashSimulator()
        {
            // Get if crash simulator is enabled or not
            bool crashEnabled = EnableCrashSimulation.IsChecked.Value;

            // Get the selected Crash Type
            CrashSimulatorType cType;
            switch (CrashMethod.Text)
            {
                case "Random":
                    cType = CrashSimulatorType.Random;
                    break;
                default:
                    cType = CrashSimulatorType.OperationCount;
                    break;
            }

            // Get the crash rate
            int crashRate = 5;
            if (null != CrashRateTextBox && !String.IsNullOrWhiteSpace(CrashRateTextBox.Text))
                crashRate = Convert.ToInt32(CrashRateTextBox.Text);

            if (crashEnabled)
                depositc.EnabledCrashSimulator(cType, crashRate);
            else
                depositc.DisableCrashSimulator();

            EnableCrashControls(crashEnabled);

            UpdateAppConfiguration();
        }

        private void EnableCrashControls(bool enable)
        {
            CrashMethod.IsEnabled = enable;

            CrashRateTextBox.IsEnabled = enable;
        }

        #endregion
    }
}

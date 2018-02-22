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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddNannyWindow.xaml
    /// </summary>
    public partial class AddNannyWindow : Window
    {
        BL.IBL myBl = BL.FactoryBL.GetBL();

        public AddNannyWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        /// <summary>
        /// checks exceptions and adding the nanny
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_ok(object sender, RoutedEventArgs e)
        {

            #region check exceptions
            bool isChecked = false;
            try
            {
                // checks empty fields
                if (string.IsNullOrEmpty(idTextBox.Text))
                    throw new Exception("fill the id!");
                if (!int.TryParse(idTextBox.Text, out int i))
                    throw new Exception("enter id in numbers!");

                bool alreadyExist = myBl.NanniesByPredicate().Any(x => x.Id == int.Parse(idTextBox.Text));
                if (alreadyExist)
                    throw new Exception("The nanny already exist!");

                if (string.IsNullOrEmpty(firstNameTextBox.Text))
                    throw new Exception("fill the first name!");
                if (int.TryParse(firstNameTextBox.Text, out i))
                    throw new Exception("invalid first name!");

                if (string.IsNullOrEmpty(lastNameTextBox.Text))
                    throw new Exception("fill the last name!");
                if (int.TryParse(lastNameTextBox.Text, out i))
                    throw new Exception("invalid last name!");

                if (string.IsNullOrEmpty(dateOfBirthDatePicker.Text))
                    throw new Exception("fill the date!");
                if (dateOfBirthDatePicker.SelectedDate > DateTime.Now)
                    throw new Exception("invalid date!");

                if (string.IsNullOrEmpty(experienceTextBox.Text))
                    throw new Exception("fill the experience!");
                if (!int.TryParse(experienceTextBox.Text, out i))
                    throw new Exception("enter experience in numbers!");

                if (string.IsNullOrEmpty(cellphoneTextBox.Text))
                    throw new Exception("fill the cellphone!");
                if (int.TryParse(cellphoneTextBox.Text, out i))
                    throw new Exception("invalid cellphone!");
                if (cellphoneTextBox.Text == "like: 052-1234578")
                    throw new Exception("fill real cellphone!");

                if (string.IsNullOrEmpty(floorTextBox.Text))
                    throw new Exception("fill the floor!");
                if (!int.TryParse(floorTextBox.Text, out i))
                    throw new Exception("enter floor in numbers!");

                if (string.IsNullOrEmpty(hourlyWageTextBox.Text))
                    throw new Exception("fill the hourly wage!");
                if (!int.TryParse(hourlyWageTextBox.Text, out i))
                    throw new Exception("enter hourly wage in numbers!");

                if (string.IsNullOrEmpty(addressTextBox.Text))
                    throw new Exception("fill the address!");
                if (int.TryParse(addressTextBox.Text, out i))
                    throw new Exception("invalid address!");

                if (string.IsNullOrEmpty(maxChildAgeTextBox.Text))
                    throw new Exception("fill the max child age!");
                if (!int.TryParse(maxChildAgeTextBox.Text, out i))
                    throw new Exception("enter max child age in numbers!");

                if (string.IsNullOrEmpty(maxChildNumberTextBox.Text))
                    throw new Exception("fill the max child number!");
                if (!int.TryParse(maxChildNumberTextBox.Text, out i))
                    throw new Exception("enter max child number in numbers!");

                if (string.IsNullOrEmpty(minChildAgeTextBox.Text))
                    throw new Exception("fill the min child age!");
                if (!int.TryParse(minChildAgeTextBox.Text, out i))
                    throw new Exception("enter min child age in numbers!");

                int min = int.Parse(minChildAgeTextBox.Text);
                int max = int.Parse(maxChildAgeTextBox.Text);
                if (max < min)
                    throw new Exception("max child age can't be less than min child age");

                if (string.IsNullOrEmpty(monthlyWageTextBox.Text))
                    throw new Exception("fill the monthly wage!");
                if (!int.TryParse(monthlyWageTextBox.Text, out i))
                    throw new Exception("enter monthly wage in numbers!");

                if (string.IsNullOrEmpty(recommendationTextBox.Text))
                    throw new Exception("fill the  recommendation!");
                if (int.TryParse(recommendationTextBox.Text, out i))
                    throw new Exception("invalid recommendation !");

                int enter, exit; // help variables
                if ((bool)checkSun.IsChecked)
                {
                    if (string.IsNullOrEmpty(enterSun.Text))
                        throw new Exception("fill the enter box!");
                    if (!int.TryParse(enterSun.Text, out i))
                        throw new Exception("invalid enter box!");

                    if (string.IsNullOrEmpty(exitSun.Text))
                        throw new Exception("fill the exit box!");
                    if (!int.TryParse(exitSun.Text, out i))
                        throw new Exception("invalid exit box!");

                    enter = int.Parse(enterSun.Text);
                    exit = int.Parse(exitSun.Text);
                    if (exit < enter)
                        throw new Exception("exit time is earlier than the enter time!");
                }

                if ((bool)checkMon.IsChecked)
                {
                    if (string.IsNullOrEmpty(enterMon.Text))
                        throw new Exception("fill the enter box!");
                    if (!int.TryParse(enterMon.Text, out i))
                        throw new Exception("invalid enter box!");

                    if (string.IsNullOrEmpty(exitMon.Text))
                        throw new Exception("fill the exit box!");
                    if (!int.TryParse(exitMon.Text, out i))
                        throw new Exception("invalid exit box!");

                    enter = int.Parse(enterMon.Text);
                    exit = int.Parse(exitMon.Text);
                    if (exit < enter)
                        throw new Exception("exit time is earlier than the enter time!");

                }

                if ((bool)checkTue.IsChecked)
                {
                    if (string.IsNullOrEmpty(enterTue.Text))
                        throw new Exception("fill the enter box!");
                    if (!int.TryParse(enterTue.Text, out i))
                        throw new Exception("invalid enter box!");

                    if (string.IsNullOrEmpty(exitTue.Text))
                        throw new Exception("fill the exit box!");
                    if (!int.TryParse(exitTue.Text, out i))
                        throw new Exception("invalid exit box!");

                    enter = int.Parse(enterTue.Text);
                    exit = int.Parse(exitTue.Text);
                    if (exit < enter)
                        throw new Exception("exit time is earlier than the enter time!");
                }

                if ((bool)checkWed.IsChecked)
                {
                    if (string.IsNullOrEmpty(enterWed.Text))
                        throw new Exception("fill the enter box!");
                    if (!int.TryParse(enterWed.Text, out i))
                        throw new Exception("invalid enter box!");

                    if (string.IsNullOrEmpty(exitWed.Text))
                        throw new Exception("fill the exit box!");
                    if (!int.TryParse(exitWed.Text, out i))
                        throw new Exception("invalid exit box!");

                    enter = int.Parse(enterWed.Text);
                    exit = int.Parse(exitWed.Text);
                    if (exit < enter)
                        throw new Exception("exit time is earlier than the enter time!");
                }

                if ((bool)checkThu.IsChecked)
                {
                    if (string.IsNullOrEmpty(enterThu.Text))
                        throw new Exception("fill the enter box!");
                    if (!int.TryParse(enterThu.Text, out i))
                        throw new Exception("invalid enter box!");

                    if (string.IsNullOrEmpty(exitThu.Text))
                        throw new Exception("fill the exit box!");
                    if (!int.TryParse(exitThu.Text, out i))
                        throw new Exception("invalid exit box!");

                    enter = int.Parse(enterThu.Text);
                    exit = int.Parse(exitThu.Text);
                    if (exit < enter)
                        throw new Exception("exit time is earlier than the enter time!");
                }

                if ((bool)checkFri.IsChecked)
                {
                    if (string.IsNullOrEmpty(enterFri.Text))
                        throw new Exception("fill the enter box!");
                    if (!int.TryParse(enterFri.Text, out i))
                        throw new Exception("invalid enter box!");

                    if (string.IsNullOrEmpty(exitFri.Text))
                        throw new Exception("fill the exit box!");
                    if (!int.TryParse(exitFri.Text, out i))
                        throw new Exception("invalid exit box!");

                    enter = int.Parse(enterFri.Text);
                    exit = int.Parse(exitFri.Text);
                    if (exit < enter)
                        throw new Exception("exit time is earlier than the enter time!");
                }


                isChecked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DATA ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            if (!isChecked)
                return;
            #endregion

            #region adding the nanny



            BE.Nanny myNanny = new BE.Nanny() // create the new nanny for ading
            {
                Address = addressTextBox.Text,
                Cellphone = cellphoneTextBox.Text,
                DateOfBirth = (DateTime)dateOfBirthDatePicker.SelectedDate,
                ElevatorExist = (bool)elevatorExistCheckBox.IsChecked,
                Experience = int.Parse(experienceTextBox.Text),
                FirstName = firstNameTextBox.Text,
                Floor = int.Parse(floorTextBox.Text),
                HolidaysByTMT = (bool)holidaysByTMTCheckBox.IsChecked,
                HourlyWage = int.Parse(hourlyWageTextBox.Text),
                Id = int.Parse(idTextBox.Text),
                LastName = lastNameTextBox.Text,
                MaxChildAge = int.Parse(maxChildAgeTextBox.Text),
                MaxChildNumber = int.Parse(maxChildNumberTextBox.Text),
                MinChildAge = int.Parse(minChildAgeTextBox.Text),
                MonthlyWage = int.Parse(monthlyWageTextBox.Text),
                NumOfContract = 0,
                PerHourSalary = (bool)perHourSalaryCheckBox.IsChecked,
                Recommendation = recommendationTextBox.Text

            };
            myNanny.WorkDays[0] = (bool)checkSun.IsChecked;
            myNanny.WorkDays[1] = (bool)checkMon.IsChecked;
            myNanny.WorkDays[2] = (bool)checkTue.IsChecked;
            myNanny.WorkDays[3] = (bool)checkWed.IsChecked;
            myNanny.WorkDays[4] = (bool)checkThu.IsChecked;
            myNanny.WorkDays[5] = (bool)checkFri.IsChecked;
            myNanny.WorkDays[6] = (bool)checkSat.IsChecked;

            if ((bool)checkSun.IsChecked)
            {
                myNanny.Schedule[0, 0] = int.Parse(enterSun.Text);
                myNanny.Schedule[1, 0] = int.Parse(exitSun.Text);
            }
            if ((bool)checkMon.IsChecked)
            {
                myNanny.Schedule[0, 1] = int.Parse(enterMon.Text);
                myNanny.Schedule[1, 1] = int.Parse(exitMon.Text);
            }
            if ((bool)checkTue.IsChecked)
            {
                myNanny.Schedule[0, 2] = int.Parse(enterTue.Text);
                myNanny.Schedule[1, 2] = int.Parse(exitTue.Text);
            }
            if ((bool)checkWed.IsChecked)
            {
                myNanny.Schedule[0, 3] = int.Parse(enterWed.Text);
                myNanny.Schedule[1, 3] = int.Parse(exitWed.Text);
            }
            if ((bool)checkThu.IsChecked)
            {
                myNanny.Schedule[0, 4] = int.Parse(enterThu.Text);
                myNanny.Schedule[1, 4] = int.Parse(exitThu.Text);
            }
            if ((bool)checkFri.IsChecked)
            {
                myNanny.Schedule[0, 5] = int.Parse(enterFri.Text);
                myNanny.Schedule[1, 5] = int.Parse(exitFri.Text);
            }
            try
            {
                myBl.AddNanny(myNanny);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ADD NANNY ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            #endregion


            MessageBox.Show("The nanny added successfully :)", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();

        }

        private void Click_cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}


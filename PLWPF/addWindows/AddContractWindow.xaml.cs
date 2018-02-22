using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AddContractWindow.xaml
    /// </summary>
    public partial class AddContractWindow : Window
    {
        BL.IBL myBl = BL.FactoryBL.GetBL();
        string childName;
        Child myChild;
        IEnumerable<Nanny> nannies;

        /// <summary>
        /// c-tor
        /// </summary>
        /// <param name="UntreatedChilds"></param>
        public AddContractWindow(IEnumerable<Child> UntreatedChilds)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            //childs comboBox definations
            IEnumerable<Child> untreatedChilds = UntreatedChilds;
            ComboBoxItem newItem;
            foreach (Child myChild in untreatedChilds)
            {
                newItem = new ComboBoxItem();
                newItem.Content = myChild.FirstName + " " + myChild.LastName;
                comboBoxChilds.Items.Add(newItem);
            }

            // salary combBox definations

            newItem = new ComboBoxItem();
            newItem.Content = "Monthly";
            salaryTypeCB.Items.Add(newItem);

            newItem = new ComboBoxItem();
            newItem.Content = "Hourly";
            salaryTypeCB.Items.Add(newItem);

        }

        /// <summary>
        /// closing the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// checks exceptions and adding the contract
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_ok(object sender, RoutedEventArgs e)
        {

            #region check exceptions

            try
            {
                if (comboBoxChilds.SelectedItem == null)
                    throw new Exception("no child selected!");

                if (contartEndDatePicker.SelectedDate < contartBeginDatePicker.SelectedDate)
                    throw new Exception("invalid end date ");

                if (salaryTypeCB.SelectedItem == null)
                    throw new Exception("no salary type selected!");

                if (nanniesListBox.SelectedItem == null)
                    throw new Exception("select nanny!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            #endregion

            #region add the contract

            string nannyName = ((ListBoxItem)nanniesListBox.SelectedItem).Content.ToString().Split(':').First();
            Nanny myNanny = (myBl.NanniesByPredicate(x => (x.FirstName + " " + x.LastName) == nannyName)).First();

            Contract myContract = new Contract()
            {
                ChildId = myChild.Id,
                NannyId = myNanny.Id,
                Met = metCheckBox.IsChecked.Value,
                ContractBegin = (DateTime)contartBeginDatePicker.SelectedDate,
                ContractEnd = (DateTime)contartEndDatePicker.SelectedDate
            };


            if (((ComboBoxItem)salaryTypeCB.SelectedItem).Content.ToString() == "Hourly")  // checks the wanted contract type
                myContract.ConType = ContractType.hourly;
            else myContract.ConType = ContractType.monthly;

            try
            {
                myBl.AddContract(myContract);
            }
            catch (Exception mes)
            {
                MessageBox.Show(mes.Message, "ADD CONTRACT ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            #endregion

            MessageBox.Show("The contract added successfully :)", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SalaryTypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (distanceTB.Text == "")
            {
                MessageBox.Show("fill the distance", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            distanceTB.IsEnabled = true;
            if (comboBoxChilds.SelectedItem == null) // if no child selected we can't resume
                return;

            comboBoxChilds.IsEnabled = false; // we will update the datalist only by the contract type : there is "SelectionChanged" only for the type and not for the kid
            nanniesListBox.Items.Clear(); // clearing the textBox from the previous choice

            childName = ((ComboBoxItem)comboBoxChilds.SelectedItem).Content.ToString();
            myChild = (myBl.UntreatedChilds(x => (x.FirstName + " " + x.LastName) == childName)).First();
            Mother myMother = (myBl.MothersByPredicate(x => x.Id == myChild.MotherId)).First();

            nannies = myBl.NanniesByPredicate();

            try   // checks if there is nannies in the dataBase
            {
                if (nannies.Count() == 0) // if there is no nanny 
                    throw new Exception("There is no nanny in the dataBase!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            nannies = myBl.MatchedNanny(myMother); // filtering the nannies
            if (nannies.Count() == 0)
                nannies = myBl.FiveBestMatched(myMother);

            string dis = distanceTB.Text;
            Thread myThread;
            foreach (Nanny myNanny in nannies) // launching thread per each nanny
            {

                myThread = new Thread(() => CheckDistanceOfNanny_Thread(myMother, myNanny, dis));
                myThread.Name = "check_nannyThread: " + myNanny.FirstName + " " + myNanny.LastName;
                myThread.Start();
            }

        }

        /// <summary>
        /// Filter the nannies by their address.
        /// appropriate distance is 10 km .
        /// this thread also puts the data in the dataList of the nannies.
        /// </summary>
        /// <param name="myMother"></param>
        private void CheckDistanceOfNanny_Thread(Mother myMother, Nanny myNanny, string distance)
        {

            int dis = myBl.CalculateDistance(myMother.Address, myNanny.Address);
            int MAX_DISTANCE = int.Parse(distance) * 1000; // in km
            if (dis < MAX_DISTANCE)
            {
                Action<Nanny, int> act = AddNannyToList;
                Dispatcher.BeginInvoke(act, myNanny, dis);
            }

        }

        /// <summary>
        /// functions for the dispatcher. it will update the dataList of the nannies.
        /// the list can be empty if there is no nanny with the salary type that the mother require
        /// </summary>
        /// <param name="myMother"></param>
        private void AddNannyToList(Nanny myNanny, int distance)
        {

            string salaryType = ((ComboBoxItem)salaryTypeCB.SelectedItem).Content.ToString();

            // filter by per hour salary
            if (salaryType == "Hourly" && myNanny.PerHourSalary == false) // nanny: monthly , selected: hourly
                return;
            if (salaryType == "Monthly" && myNanny.PerHourSalary == true) // nanny: hourly , selected: monthly
                return;

            // adding the nanny details to the lists of the nannies
            ListBoxItem newItem = new ListBoxItem();
            newItem.Content =
                myNanny.FirstName + " " + myNanny.LastName + ": "
                + "\tAge " + (DateTime.Today.Year - myNanny.DateOfBirth.Year) + ", "
                + myNanny.Experience + " Years of experience" + ", "
                + "Range of child age: " + myNanny.MinChildAge + "-" + myNanny.MaxChildAge + ", "
            + "Address: " + myNanny.Address + ", "
            + "Distance: " + Math.Round((distance / 1000.0),2);
            nanniesListBox.Items.Add(newItem);

        }

        #region mouse definations for zooming labels

        private void CancelButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            CancelButton.FontSize = 15;
        }

        private void CancelButton_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            CancelButton.FontSize = 25;
        }

        private void Backutton_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Backutton.FontSize = 25;
        }

        private void Backutton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Backutton.FontSize = 15;
        }

        #endregion

    }
}

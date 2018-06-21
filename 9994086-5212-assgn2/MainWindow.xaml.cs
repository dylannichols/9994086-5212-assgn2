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

namespace _9994086_5212_assgn2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // List to store instances of Customer class
        static List<Customer> CustomerDB = new List<Customer>();
        static int SelectedItemIndex;
        public MainWindow()
        {
            InitializeComponent();
            LoadDB();
        }

        public void DisplayCustomers()
        {
            foreach (var x in CustomerDB)
            {
                listBox.Items.Add(x.GetCustomer());
            }
        }

        public void ClearDisplay()
        {
            listBox.Items.Clear();

        }

        // Adds base content to CustomerDB
        public void LoadDB()
        {
            CustomerDB.Add(new Customer("Jaarna", "Kereopa", "123-2514"));
            CustomerDB.Add(new Customer("Sue", "Stook", "123-1263"));
            CustomerDB.Add(new Customer("Jamie", "Allom", "123-3658"));
            CustomerDB.Add(new Customer("Brian", "Janes", "123-9898"));
        }

        // When clear button is clicked, clears content of the input boxes of the form
        private void ClearBoxes()
        {
            firstName.Text = "";
            lastName.Text = "";
            phone.Text = "";
        }

        // Adds all items in CustomerDB to the listbox
        private void ListCustomers(object sender, RoutedEventArgs e)
        {
            ClearDisplay();
            DisplayCustomers();
        }

        // Clears the listbox
        private void ClearList(object sender, RoutedEventArgs e)
        {
            ClearDisplay();
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            ClearBoxes();
            firstName.Focus();
            add.IsEnabled = true;
        }

        // Searches for the name entered into the search bar and if found, displays in the listbox
        private void Search(object sender, RoutedEventArgs e)
        {
            var toFind = search.Text.ToUpper();
            int result = -1;

            if (toFind == "")
            {
                MessageBox.Show("You must enter a customer name.", "Invalid input");
            }
            else
            {
                search.Text = "";
                ClearDisplay();
                for (var i = 0; i < CustomerDB.Count; i++)
                {
                    result = CustomerDB.FindIndex(x => x.FName.ToUpper() == toFind || x.LName.ToUpper() == toFind || $"{x.FName} {x.LName}".ToUpper() == toFind);
                }
                if (result == -1)
                {
                    MessageBox.Show("Customer not found, please try again", "Invalid input");
                    search.Focus();
                }
                else
                {
                    listBox.Items.Add(CustomerDB[result].GetCustomer());
                }
            }
        }

        private void SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            add.IsEnabled = false;
            if (listBox.SelectedItem != null)
            {  
                var selected = listBox.SelectedItem as string;

                var info = selected.Split('\t');

                firstName.Text = info[0];
                lastName.Text = info[1];
                phone.Text = info[2];

                SelectedItemIndex = CustomerDB.FindIndex(x => x.FName == info[0]);
            }
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a customer to update.", "Invalid input");
                ClearDisplay();
                DisplayCustomers();
            }
            else if (firstName.Text == "" || lastName.Text == "" || phone.Text == "")
            {
                MessageBox.Show("All textboxes must be filled to continue.", "Invalid input");
            }
            else
            {
                CustomerDB[SelectedItemIndex].FName = firstName.Text;
                CustomerDB[SelectedItemIndex].LName = lastName.Text;
                CustomerDB[SelectedItemIndex].Phone = phone.Text;
                ClearBoxes();
                ClearDisplay();
                DisplayCustomers();
                MessageBox.Show("Customer details updated.", "Success");
                add.IsEnabled = true;
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            if (firstName.Text == "" || lastName.Text == "" || phone.Text == "")
            {
                MessageBox.Show("All textboxes must be filled to continue.", "Invalid input");
            }
            else
            {
                CustomerDB.Add(new Customer(firstName.Text, lastName.Text, phone.Text));
                ClearBoxes();
                ClearDisplay();
                DisplayCustomers();
                MessageBox.Show("New customer has been added.", "Success");
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a customer to delete.", "Invalid input");
                ClearDisplay();
                DisplayCustomers();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you wanted to delete this customer?", "Delte Customer", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    CustomerDB.Remove(CustomerDB[SelectedItemIndex]);
                    ClearBoxes();
                    ClearDisplay();
                    DisplayCustomers();
                    MessageBox.Show("The customer has been deleted.", "Success");
                    add.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show("Operation cancelled.", "Cancelled");
                }
            }
        }
    }
}

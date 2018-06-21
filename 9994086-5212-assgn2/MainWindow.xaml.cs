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
        private void ClearBoxes(object sender, RoutedEventArgs e)
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
        // Searches for the name entered into the search bar and if found, displays in the listbox
        private void Search(object sender, RoutedEventArgs e)
        {
            var toFind = search.Text.ToUpper();
            int result = -1;

            if (toFind == "")
            {
                MessageBox.Show("You must enter a customer name.");
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
                    MessageBox.Show("Customer not found, please try again");
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
            var selected = listBox.SelectedItem as string;
            var info = selected.Split('\t');

            firstName.Text = info[0];
            lastName.Text = info[1];
            phone.Text = info[2];

            SelectedItemIndex = CustomerDB.FindIndex(x => x.FName == info[0]);
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a customer to update.");
                ClearDisplay();
                DisplayCustomers();
            }
            else if (firstName.Text == "" || lastName.Text == "" || phone.Text == "")
            {
                MessageBox.Show("All textboxes must be filled to continue.");
            }
            else
            {

            }
        }

       
    }
}

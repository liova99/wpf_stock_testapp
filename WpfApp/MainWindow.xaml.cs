using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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

using MySql.Data.MySqlClient;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        MainViewModel mainViewModel = new MainViewModel();
        
        public MainWindow()
        {
            InitializeComponent();

            // 
            DataContext = mainViewModel;
            

        }


        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {

                //add To DB
                Product product = new Product() { Name = nameTextBox.Text, Price = decimal.Parse(priceTextBox.Text), Category = categoryCombobox.Text };
                mainViewModel.AddToDB(product);
            }
            catch
            {
                MessageBox.Show(@"Invalid Data or Duplicate Entry", "Please Try Again");
            }
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            // Selected Item
            Product pruduct = (Product)dataGrid.SelectedItem;

            mainViewModel.RemoveFromDB();


        }
    }
}

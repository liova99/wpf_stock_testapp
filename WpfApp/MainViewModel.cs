using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Windows.Input;
using System.Configuration;

namespace WpfApp
{
    class MainViewModel
    {
        private Product product;
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<String> Categories { get; set; }

        public Product SelectedProduct
        {
            get { return product; }
            set { product = value;
            DeleteCommand.SetEnable(value != null);  }
        }

        private string connectionString;

        public ActionCommand DeleteCommand { get; set; }

        public  MainViewModel()
        {
            Products = new ObservableCollection<Product>(); 
            Categories = new ObservableCollection<string>();
            DeleteCommand = new ActionCommand(RemoveFromDB);
            connectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;

            // MySQL Connection
            // @ means that i can use the long String in many lines
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {

                // Open Connection 
                con.Open();

               
                // Show Categories
                var categories = con.Query<String>("SELECT name FROM  leo_markt.category");

                foreach (var prod in categories)
                {
                    Categories.Add(prod.ToString());
                }
            }
            ShowDB();
        }

        public void AddToDB(Product product)
        {

            // MySQL Connection
            // @ means that i can use the long String in many lines
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                // Open Connection 
                con.Open();

                var prodactsVariable = con.Execute(@"INSERT INTO `leo_markt`.`products`
                                         (`name`, `price`, `category`) VALUES (@Name, @Price, @Category)", product);
            }
            ShowDB();

        }

        public void ShowDB()
        {

            // MySQL Connection
            // @ means that i can use the long String in many lines
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {

                // Open Connection 
                con.Open();

                var prodactsVariable = con.Query<Product>("SELECT id, name, price,category FROM  leo_markt.products");
                Products.Clear();
                foreach (var prod in prodactsVariable)
                {
                    Products.Add(prod);
                }

            }
        }

        public void RemoveFromDB()
        {
            // MySQL Connection
            // @ means that i can use the long String in many lines
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                // Open Connection 
                con.Open();


                var prodactsVariable = con.Execute(@"DELETE FROM  `leo_markt`.`products` WHERE 
                                         Id = @Id", SelectedProduct);
            }
            ShowDB();
        }
       
        
    }
}

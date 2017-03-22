using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using BakeryClassLibrary;
using System.Data.SqlClient;

namespace LoveItBakeryG11.Pages
{
    /// <summary>
    /// Interaction logic for till_Page.xaml
    /// </summary>
    public partial class till_Page : Page
    {
        List<OrderLine> basket = new List<OrderLine>();
        public till_Page()
        {
            InitializeComponent();

            PopulateButtons();
            lvBasket.ItemsSource = basket;
        }
        private int GetBasketPrice()
        {
            int total = 0;
            foreach (OrderLine line in basket)
            {
                total += line.linePrice;
            }
            return total;
        } //Here because total order price is not normally calculated until after the Order obj is created.
        private string UpdateTotalPriceLabel()
        {
            string totalPriceString;
            totalPriceString = (GetBasketPrice()/100m).ToString("C2");
            return totalPriceString;
        }
        public void PopulateButtons()
        {
            int bcount = gridButtonGrid.Children.Count;
            gridButtonGrid.Children.RemoveRange(0, bcount);
            string connectionString = SqlTools.GetConnectionString();
            string queryString = "SELECT * FROM dbo.Product";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        string id = reader["Id"].ToString();
                        string name = reader["Name"].ToString();
                        int price = int.Parse(reader["Price"].ToString());
                        //Create button
                        Button newBtn = new Button()
                        {
                            Content = string.Format("{0}:{1}:{2}", name, id, price),
                            Tag = (id)
                        };
                        newBtn.Margin = new Thickness(2, 2, 2, 2);
                        newBtn.Click += new RoutedEventHandler(newBtn_Click);
                        newBtn.FontSize = 24;
                        gridButtonGrid.Children.Add(newBtn);
                    }
                }
                finally
                {
                    reader.Close();
                    connection.Close();
                }

            }
        } //Clears and then repopulates the product buttons grid
        private void RefreshListView()
        {
            lvBasket.ItemsSource = null;
            lvBasket.ItemsSource = basket;
        }
        private void newBtn_Click(object sender, RoutedEventArgs e)
        {
            bool createnewline = true;
            string productId = (sender as Button).Tag.ToString();
            foreach (OrderLine line in basket)
            {
                if (productId == line.GetProductID())
                {
                    line.IncrementQuantity();
                    line.CalculatePrice();
                    createnewline = false;
                }

            }
            if (createnewline)
            {
                Product product = new Product(productId);
                OrderLine newLine = new OrderLine(product, 1);
                basket.Add(newLine);
            }
            RefreshListView();
            lblOrderPrice.Content = UpdateTotalPriceLabel();

        }
        private void btnTillFinish_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Finished?", "Confirm", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Order order = new Order("CU-pwgef", basket, true);
                if (order.SendToServer())
                {
                    MainWindow.GreatSuccess();
                }
                basket = new List<OrderLine>();
                RefreshListView();
            }
        }
        private void btnTillCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Cancel", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                basket = new List<OrderLine>();
                RefreshListView();
            }

        }


    }
}

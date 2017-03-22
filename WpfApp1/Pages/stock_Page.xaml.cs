using System;
using System.Windows;
using System.Windows.Controls;
using BakeryClassLibrary;

namespace LoveItBakeryG11.Pages
{
    /// <summary>
    /// Interaction logic for stock_Page.xaml
    /// </summary>
    public partial class stock_Page : Page
    {
        public stock_Page()
        {
            InitializeComponent();
        }
        public void AddProduct()
        {
            int price = 0;
            int alert = 0;
            bool tbCheck;
            try
            {
                price = Convert.ToInt16(priceBox.Text);
                alert = Convert.ToInt16(alertBox.Text);
                tbCheck = true;
            }
            catch
            {
                tbCheck = false;
                MessageBox.Show("Enter valid input");

            }
            if (tbCheck)
            {
               
                Product product = new Product(nameBox.Text, descBox.Text, alert, price);
                bool success = product.SendToServer();
                if (success) { MainWindow.GreatSuccess(); }
            }

        }
        private void exitbutton_Click(object sender, RoutedEventArgs e)
        {
            addStockGrid.Visibility = Visibility.Collapsed;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddProduct();
            nameBox.Clear();
            descBox.Clear();
            priceBox.Clear();
            qtyBox.Clear();
            alertBox.Clear();
        }

        private void btnStockEdit_Click(object sender, RoutedEventArgs e)
        {
            addStockGrid.Visibility = Visibility.Visible;
        }
    }
}

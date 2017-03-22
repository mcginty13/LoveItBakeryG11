using System.Windows;
using System.Windows.Controls;
using BakeryClassLibrary;

namespace LoveItBakeryG11.Pages
{
    /// <summary>
    /// Interaction logic for customers_Page.xaml
    /// </summary>
    public partial class customers_Page : Page
    {
        public customers_Page()
        {
            InitializeComponent();
        }
        private void btnCustomerAdd_Click(object sender, RoutedEventArgs e)
        {
            grdCustomerAdd.Visibility = Visibility.Visible;
        }

        private void btnCancelAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            grdCustomerAdd.Visibility = Visibility.Collapsed;
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            AddCustomer();
        }

        private void AddCustomer()
        {
            string[] address = new string[6];
            string name = adName.Text;
            string email = ademail.Text;
            string contNo = adcontno.Text;
            address[0] = ad1.Text;
            address[1] = ad2.Text;
            address[2] = ad3.Text;
            address[3] = adcity.Text;
            address[4] = adcounty.Text;
            address[5] = adpcode.Text;
            Customer customer = new Customer(name, email, contNo, address);
            bool success = customer.SendToServer();

            if (success)
            {
                MainWindow.GreatSuccess();
            }
        }
    }
}

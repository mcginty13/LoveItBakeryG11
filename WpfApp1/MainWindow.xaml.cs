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
using OnScreenKeyboard;
using LoveItBakeryG11.Pages;
using BakeryClassLibrary;
using System.Data.SqlClient;

namespace LoveItBakeryG11
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Grid RootGrid = new Grid();
        public MainWindow()
        {

            InitializeComponent();
            populateLoginButtons();
            EventManager.RegisterClassHandler(typeof(TextBox), TextBox.GotFocusEvent, new RoutedEventHandler(this.txtbx_GotFocus));
            EventManager.RegisterClassHandler(typeof(TextBox), TextBox.LostFocusEvent, new RoutedEventHandler(this.txtbx_LostFocus));
            RootGrid = grdRoot;
            OnScreenKeyboard.OnScreenKeyboard.createOsk(RootGrid);
        }
        private void populateLoginButtons()
        {
            string connectionString = SqlTools.GetConnectionString();
            string queryString = "SELECT * FROM dbo.Users";

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
                        string password = reader["Password"].ToString();

                        //Create button
                        Viewbox buttonBox = new Viewbox();
                        TextBlock txtBlock = new TextBlock();
                        txtBlock.Text = string.Format("{0} : {1}", name, id);
                        txtBlock.Padding = new Thickness(10, 10, 10, 10);
                        txtBlock.FontSize = 30;
                        buttonBox.Child = txtBlock;
                        buttonBox.StretchDirection = StretchDirection.DownOnly;
                        Button newBtn = new Button()
                        {
                            Content = buttonBox,
                            Tag = new ControlTag(id, name, password)
                        };
                        newBtn.Margin = new Thickness(2, 2, 2, 2);
                        newBtn.Click += new RoutedEventHandler(loginButton_Click);
                        grdUserLogon.Children.Add(newBtn);
                    }
                }
                finally
                {
                    reader.Close();
                    connection.Close();
                }

            }
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            loginGrid.Visibility = Visibility.Collapsed;
            tabsGrid.Visibility = Visibility.Visible;
            User currentUser = User.setCurrentUser((sender as Button).Tag.ToString());
        }

        private void btnCloseApp_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            loginGrid.Visibility = Visibility.Collapsed;
            tabsGrid.Visibility = Visibility.Visible;

        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            tabsGrid.Visibility = Visibility.Collapsed;
            loginGrid.Visibility = Visibility.Visible;
        }



        public static void keyboardBtn_Click(object sender, RoutedEventArgs e)
        {
            OnScreenKeyboard.OnScreenKeyboard.buttonPress(RootGrid, sender);
        }

        private void txtbx_GotFocus(object sender, RoutedEventArgs e)
        {
            OnScreenKeyboard.OnScreenKeyboard.showOsk(RootGrid);
        }

        private void txtbx_LostFocus(object sender, RoutedEventArgs e)
        {
            OnScreenKeyboard.OnScreenKeyboard.hideOsk(RootGrid);
        }

        public static void GreatSuccess()
        {
            Success suc = new Success();
            suc.Show();
        }
    }
}

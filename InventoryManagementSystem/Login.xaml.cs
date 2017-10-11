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

namespace InventoryManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //add user check here
            var username = txtUsername.Text;
            var password = txtPassword.Password;

            var context = new InventoryManagementSystem.InventoryDBEntities();

            var getPass = (from user in context.users
                          where user.emailAddress == username
                          select user.password).FirstOrDefault();

            if(password == getPass)
            {
                //opens MainScreen
                MainScreen ms = new MainScreen();
                ms.Show();
                this.Close();
            }
            
        }

        private void btnX_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}

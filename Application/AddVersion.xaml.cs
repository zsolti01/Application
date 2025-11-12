using ApplicationWpf;
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
using System.Windows.Shapes;

namespace Application
{
    /// <summary>
    /// Interaction logic for AddVersion.xaml
    /// </summary>
    public partial class AddVersion : Window
    {
        private readonly DatabaseStatements db = new DatabaseStatements();
        private readonly DatabaseStatements _databaseStatements = new DatabaseStatements();
        private readonly MainWindow _mainWindow;

        public AddVersion()
        {
            InitializeComponent();
        }

        private void exit(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Goodbye!");
            this.Close();
        }

        private void back(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void add(object sender, RoutedEventArgs e)
        {
            if (pw.Password == pwagain.Password)
            {
                var version = new
                {
                    Version = v.Text,
                    UserName = un.Text,
                    UserPassword = pw.Password,
                    Salt = "",
                };
                MessageBox.Show(db.AddNewUser(version).ToString());
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Password don't match!");
            }
        }
    }
}

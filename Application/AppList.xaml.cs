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
    /// Interaction logic for AppList.xaml
    /// </summary>
    public partial class AppList : Window
    {
        private readonly DatabaseStatements _databaseStatements = new DatabaseStatements();
        private readonly MainWindow _mainWindow;

        public AppList()
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

        private void list(object sender, RoutedEventArgs e)
        {
            Versions.ItemsSource = _databaseStatements.VersionList();
        }
    }
}

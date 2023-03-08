using OOO_Спорт_Мастер.Classes;
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

namespace OOO_Спорт_Мастер
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Helper.DB = new Entini.OOO_Спорт_МастерEntities(); 
        }

        private void ButtonEnter_Click(object sender, RoutedEventArgs e)
        {
            string login = TextBoxLogin.Text;
            string password = TextBoxPassword.Text;

            Helper.User = Helper.DB.User.Where(x=> x.UserLogin == login & x.UserPassword == password).ToList().FirstOrDefault();
            if(Helper.User != null)
            {
                ProductWindow product = new ProductWindow();
                product.Show();
            }
            else
            {
                MessageBox.Show("Неправальный логин или пароль");
            }

           
        }
    }
}

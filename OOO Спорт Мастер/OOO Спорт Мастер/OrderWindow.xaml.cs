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
using System.Windows.Shapes;

namespace OOO_Спорт_Мастер
{
    /// <summary>
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        public List<Order> orders;
        public List<Classes.ProductClass> orderList;
        public OrderWindow()
        {
            InitializeComponent();
            this.orders = ProductWindow.order;
            Helper.DB = new Entini.OOO_Спорт_МастерEntities();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowOrder();
        }

        void ShowOrder()
        {
            int i = 0;
            foreach(var item  in orders)
            {
                var data = Helper.DB.Product.Where(x => x.ProductArticle == orders[i].Article).ToList();
                //orderList.Add(data);
                i++;
            }


            ListViewOrder.ItemsSource = null;

            ListViewOrder.ItemsSource =orders;
            TextBoxDecriptionOrder.Text = "Количество позиции в заказе" ;
        }
    }
}

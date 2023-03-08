using System;
using System.Collections.Generic;
using System.IO;
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

namespace OOO_Спорт_Мастер.Classes
{
    /// <summary>
    /// Логика взаимодействия для ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        public string Path = System.AppDomain.CurrentDomain.BaseDirectory;

        List<Entini.Product> productsView = new List<Entini.Product>();
        List<Entini.Product> orderAdd = new List<Entini.Product>();

        public static List<Classes.Order> order;

        int filterDiscount, filterCategory;
        string sort, searchProduct;
        int countOrder,i;

        public ProductWindow()
        {
           
            InitializeComponent();
            Helper.DB = new Entini.OOO_Спорт_МастерEntities();
        }
        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LabelUser.Content = Helper.User.UserName + " Роль:" + Helper.User.Role.RoleName;
            i = 0;
            order = new List<Classes.Order>();

            var category = Helper.DB.Category.Select(x=> x.CategoryName).ToList();
            category.Insert(0, "Все категории");
            comboBoxCategory.Items.Clear();
            comboBoxCategory.ItemsSource = category;
           
            comboBoxCategory.SelectedIndex = 0;
            ShowProduct();
            

        }
        void ShowProduct()
        {
            ListViewProduct.Items.Clear();
            productsView = Helper.DB.Product.ToList();

            if (filterCategory != 0)
            {
                productsView = productsView.Where(x => x.ProductCategoryId == filterCategory).ToList();
            }
            if (!String.IsNullOrEmpty(TextBoxSearch.Text))
            {
                productsView = productsView.Where(x => x.ProductName.Contains(searchProduct)).ToList();
            }
            if (sort == "ASC")
            {
                productsView = productsView.OrderBy(x => x.ProductCost).ToList();
            }
            else
            {
                productsView = productsView.OrderByDescending(x => x.ProductCost).ToList();
            }

            int i = 0;
            foreach (var item in productsView)
            {

                ListViewProduct.Items.Add(productsView[i]);
                i++;
            }

        }

        private void comboBoxCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filterCategory = comboBoxCategory.SelectedIndex;
            ShowProduct();
        }

        private void comboBoxDiscoint_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filterDiscount = comboBoxDiscoint.SelectedIndex;
            ShowProduct();
        }

        private void ButtonOrder_Click(object sender, RoutedEventArgs e)
        {
           
            OrderWindow orderWindow = new OrderWindow();
            orderWindow.Show();
            this.Hide();
        }

        private void ListViewProduct_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(countOrder >= 1)
            {

                orderAdd.Add((Entini.Product)(sender as ListView).SelectedItem);

                int index = order.FindIndex(x => x.Article == orderAdd[i].ProductArticle);
                if (index < 0)
                {
                    Classes.Order orderClass = new Classes.Order();
                    orderClass.Article = orderAdd[i].ProductArticle;
                    orderClass.Count = 1;
                    order.Add(orderClass);
                }
                else
                {
                    order[index].Count++;
                }


                ButtonOrder.IsEnabled = true;
                i++;
            }
           countOrder++;
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchProduct = TextBoxSearch.Text;
            ShowProduct();
        }

        private void ComboBoxSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxSort.SelectedIndex == 0) sort = "ASC";
            else sort = "DESC";
            ShowProduct();
        }

       
    }
}

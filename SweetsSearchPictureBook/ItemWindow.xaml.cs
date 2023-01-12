using Newtonsoft.Json;
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

namespace SweetsSearchPictureBook
{
    /// <summary>
    /// ItemWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class ItemWindow : Window
    {
        public Rootobject jsonKeyWord = new Rootobject();
        public string url,keyword;
        public int num;

        public ItemWindow()
        {
            InitializeComponent();
        }

        private void ItemWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Collapsed;
        }

        private void ItemWindow_Loaded(object sender, RoutedEventArgs e)
        {
            keyword = Window1.keyWord;
            jsonKeyWord = JsonConvert.DeserializeObject<Rootobject>(keyword);
            url = jsonKeyWord.item[num].image;
            BitmapImage imageSource = new BitmapImage(new Uri(url));
            itemImage.Source = imageSource;

            snackName.Text = jsonKeyWord.item[num].name;
            itemMaker.Content = jsonKeyWord.item[num].maker;
            itemType.Content = jsonKeyWord.item[num].type;
            itemKeyword.Content = jsonKeyWord.item[num].tags;
            itemPrice.Content = jsonKeyWord.item[num].price;
        }
    }
}

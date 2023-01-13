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
        public string word;

        public ItemWindow()
        {
            InitializeComponent();
        }

        private void ItemWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Collapsed;
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            keyword = Window1.keyWord;
            jsonKeyWord = JsonConvert.DeserializeObject<Rootobject>(keyword);
            url = jsonKeyWord.item[num].image;
            BitmapImage imageSource = new BitmapImage(new Uri(url));
            itemImage.Source = imageSource;
            Check();
            snackName.Text = jsonKeyWord.item[num].name;
            if (jsonKeyWord.item[num].maker.ToString() != "{}")
            {
                itemMaker.Content = jsonKeyWord.item[num].maker;
            }
            else
            {
                itemMaker.Content = "メーカー不明";
            }

            if (jsonKeyWord.item[num].price.ToString() != "{}")
            {
                itemPrice.Content = jsonKeyWord.item[num].price + "円";
            }
            else
            {
                itemPrice.Content = "値段不明";
            }
        }

        private void buttonCloseItemWin_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //ローマ字を日本語に変換
        public string Check()
        {
            switch (jsonKeyWord.item[num].type.ToString())
            {
                case "cookie":
                    itemType.Content = "クッキー";
                    break;
                case "senbei":
                    itemType.Content = "せんべい";
                    break;                
                case "snack":
                    itemType.Content = "スナック";
                    break;
                case "chocolate":
                    itemType.Content = "チョコレート";
                    break;
                case "candy":
                    itemType.Content = "キャンディー";
                    break;
                default:
                    break;
            }
            return "";
        }
    }
}

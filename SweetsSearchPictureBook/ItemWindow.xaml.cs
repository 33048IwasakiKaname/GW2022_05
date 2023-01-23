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

            try
            {
                ItemInfo();
                Check();
                NullChecker();  
            }
            catch (IndexOutOfRangeException)
            {

            }
            catch (ArgumentNullException)
            {

            }           
        }

        private void ButtonCloseItemWin_Click(object sender, RoutedEventArgs e)
        {
            var win1 = new Window1();
            win1.tbItemPrice_1.Text = "変更しました";
            this.Close();
        }

        //アイテム情報
        public void ItemInfo()
        {
            url = jsonKeyWord.item[num].image;
            BitmapImage imageSource = new BitmapImage(new Uri(url));
            itemImage.Source = imageSource;
            snackName.Text = jsonKeyWord.item[num].name;
            var objTag = jsonKeyWord.item[num].tags.tag.ToString();
            objTag = objTag.Replace(Environment.NewLine, "");
            itemTag.Text = objTag.Replace("[", "").Replace("]", "").Replace("\"", "");  
        }

        //nullかどうかチェック
        public void NullChecker()
        {
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
                if (jsonKeyWord.item[num].price.ToString() != "0")
                {
                    itemPrice.Content = jsonKeyWord.item[num].price + "円";
                }
            }
            else
            {
                itemPrice.Content = "データなし";
            }
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

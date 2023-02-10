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
        public Rootobject_only jsonKeyWord_only = new Rootobject_only();
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

            try
            {               
                jsonKeyWord = JsonConvert.DeserializeObject<Rootobject>(keyword);
                ItemInfo();
                Check();
                NullChecker();  
            }
            catch (JsonSerializationException)
            {
                jsonKeyWord_only = JsonConvert.DeserializeObject<Rootobject_only>(keyword);
                OnlyItem(jsonKeyWord_only);
                NullCheckerOnly(jsonKeyWord_only);
                CheckOnly(jsonKeyWord_only);
                return;
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
            this.Close();
        }
        
        //アイテム情報
        public void ItemInfo()
        {
            switch (Window1.pageCount)
            {
                case 2:
                    num += 16;
                    break;
                case 3:
                    num += 32;
                    break;
                case 4:
                    num += 48;
                    break;
                case 5:
                    num += 64;
                    break;
                default:
                    break;
            }
            if (jsonKeyWord.item[num].image == null)
            {
                jsonKeyWord.item[num].image = "https://www.shoshinsha-design.com/wp-content/uploads/2020/05/noimage-760x460.png";
            }
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

        //アイテム情報(データが一つのみ)
        public void OnlyItem(Rootobject_only json)
        {
            BitmapImage imageSource = new BitmapImage(new Uri(json.item.image));
            itemImage.Source = imageSource;
            snackName.Text = json.item.name;
            var objTag = json.item.tags.tag.ToString();
            objTag = objTag.Replace(Environment.NewLine, "");
            itemTag.Text = objTag.Replace("[", "").Replace("]", "").Replace("\"", "");
        }

        //nullかどうかチェック(データ一つのみ)
        public void NullCheckerOnly(Rootobject_only json)
        {
            if (json.item.maker.ToString() != "{}")
            {
                itemMaker.Content = json.item.maker;
            }
            else
            {
                itemMaker.Content = "メーカー不明";
            }

            if (json.item.price.ToString() != "{}")
            {
                if (json.item.price.ToString() != "0")
                {
                    itemPrice.Content = json.item.price + "円";
                }
            }
            else
            {
                itemPrice.Content = "データなし";
            }
        }

        //ローマ字を日本語に変換(データが一つの時)
        public string CheckOnly(Rootobject_only json)
        {
            switch (json.item.type.ToString())
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

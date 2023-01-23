using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
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
    /// Window1.xaml の相互作用ロジック
    /// </summary>
    public partial class Window1 : Window
    {
        
        public string url = "https://www.sysbird.jp/webapi/?apikey=guest&format=json&max=16";

        public WebClient wc = new WebClient();
        public static Rootobject jsonKeyWord = new Rootobject();
        public ItemWindow itemWindow = new ItemWindow();

        public static string keyWord;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            keyWord = wc.DownloadString(url);
            jsonKeyWord = JsonConvert.DeserializeObject<Rootobject>(keyWord);
            ItemInfo();
        }

        public Window1()
        {
            InitializeComponent();
        }

        public void Search_Click(object sender, RoutedEventArgs e)
        {
            

            try
            {
                keyWord = wc.DownloadString(url + "&keyword=" + tbFreeWord.Text + "&type=" + cbClass.SelectedIndex + 
                    "&maker" + cbMaker.SelectedItem);
                keyWord = keyWord.Replace("\"type\":{}", "\"type\":\"データなし\"");
                jsonKeyWord = JsonConvert.DeserializeObject<Rootobject>(keyWord);
            }
            catch (JsonReaderException)
            {
                MessageBox.Show("問題が発生しました。\n別のワードを入力してください。", "検索エラー",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(JsonSerializationException)
            {
                keyWord = wc.DownloadString(url + "&keyword=" + tbFreeWord.Text + "&type=" + cbClass.SelectedIndex + "&max=" + 10);
                jsonKeyWord = JsonConvert.DeserializeObject<Rootobject>(keyWord);
            }
            catch (Exception)
            {
                MessageBox.Show("エラー", "エラー発生", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            itemScrollViewer.ScrollToTop();
            if (int.Parse(jsonKeyWord.count) == 0)
            {
                MessageBox.Show("見つかりませんでした");
                return;
            }

            Clear();
            ItemInfo();
        }

        //アイテム数が0の時
        public void ItemCount()
        {
            if (int.Parse(jsonKeyWord.count) == 0)
            {
                MessageBox.Show("見つかりませんでした");
                return;
            }
        }

        //値段がnullかどうかチェック
        public string PriceCheck(int num)
        {
            if (jsonKeyWord.item[num].price.ToString() != "{}")
            {
                if (jsonKeyWord.item[num].price.ToString() != "0")
                {
                    return jsonKeyWord.item[num].price.ToString() + "円";
                }
                else
                {
                    return "データなし";
                }              
            }
            else
            {
                return "データなし";
            }
        }

        //アイテム情報
        public void ItemInfo()
        {
            try
            {

                Button[] buttonArray = { btItemUrl_1,btItemUrl_2,btItemUrl_3,btItemUrl_4,btItemUrl_5,btItemUrl_6,
                                         btItemUrl_7,btItemUrl_8,btItemUrl_9,btItemUrl_10,btItemUrl_11,btItemUrl_12,
                                         btItemUrl_13,btItemUrl_14,btItemUrl_15,btItemUrl_16};
                for (int i = 0; i < int.Parse(jsonKeyWord.count); i++)
                {
                    buttonArray[i].IsEnabled = true;
                }
                

                TextBlock[] tbItemPrice = { tbItemPrice_1,tbItemPrice_2,tbItemPrice_3,tbItemPrice_4,tbItemPrice_5,
                                            tbItemPrice_6,tbItemPrice_7,tbItemPrice_8,tbItemPrice_9,tbItemPrice_10,
                                            tbItemPrice_11,tbItemPrice_12,tbItemPrice_13,tbItemPrice_14,tbItemPrice_15,
                                            tbItemPrice_16};
                for (int i = 0; i < int.Parse(jsonKeyWord.count); i++)
                {
                    tbItemPrice[i].Text = PriceCheck(i);
                }


                TextBlock[] tbItemName = { tbItemName_1,tbItemName_2, tbItemName_3, tbItemName_4, tbItemName_5,
                                           tbItemName_6,tbItemName_7,tbItemName_8,tbItemName_9,tbItemName_10,
                                           tbItemName_11,tbItemName_12,tbItemName_13,tbItemName_14,tbItemName_15,
                                           tbItemName_16};
                for (int i = 0; i < int.Parse(jsonKeyWord.count); i++)
                {
                    tbItemName[i].Text = jsonKeyWord.item[i].name;
                }


                string itemUrl_1="", itemUrl_2="", itemUrl_3="";
                Hashtable hash = new Hashtable();
                for (int i = 1; i < 3; i++)
                {
                    hash["itemUrl_" + i] = jsonKeyWord.item[i].image;
                }
                //var a = hash.Keys;
               
                //1つ目
                //var itemUrl_1 = jsonKeyWord.item[0].image;
                BitmapImage imageSource_1 = new BitmapImage(new Uri(itemUrl_1));
                pbItemImage_1.Source = imageSource_1;

                //2つ目
                //var itemUrl_2 = jsonKeyWord.item[1].image;
                BitmapImage imageSource_2 = new BitmapImage(new Uri(itemUrl_2));
                pbItemImage_2.Source = imageSource_2;

                //3つ目           
                //var itemUrl_3 = jsonKeyWord.item[2].image;
                BitmapImage imageSource_3 = new BitmapImage(new Uri(itemUrl_3));
                pbItemImage_3.Source = imageSource_3;

                //4つ目             
                var itemUrl_4 = jsonKeyWord.item[3].image;
                BitmapImage imageSource_4 = new BitmapImage(new Uri(itemUrl_4));
                pbItemImage_4.Source = imageSource_4;

                //5つ目
                var itemUrl_5 = jsonKeyWord.item[4].image;
                BitmapImage imageSource_5 = new BitmapImage(new Uri(itemUrl_5));
                pbItemImage_5.Source = imageSource_5;

                //6つ目
                var itemUrl_6 = jsonKeyWord.item[5].image;
                BitmapImage imageSource_6 = new BitmapImage(new Uri(itemUrl_6));
                pbItemImage_6.Source = imageSource_6;

                //7つ目
                var itemUrl_7 = jsonKeyWord.item[6].image;
                BitmapImage imageSource_7 = new BitmapImage(new Uri(itemUrl_7));
                pbItemImage_7.Source = imageSource_7;

                //8つ目
                var itemUrl_8 = jsonKeyWord.item[7].image;
                BitmapImage imageSource_8 = new BitmapImage(new Uri(itemUrl_8));
                pbItemImage_8.Source = imageSource_8;

                //9つ目
                var itemUrl_9 = jsonKeyWord.item[8].image;
                BitmapImage imageSource_9 = new BitmapImage(new Uri(itemUrl_9));
                pbItemImage_9.Source = imageSource_9;

                //10つ目
                var itemUrl_10 = jsonKeyWord.item[9].image;
                BitmapImage imageSource_10 = new BitmapImage(new Uri(itemUrl_10));
                pbItemImage_10.Source = imageSource_10;

                //11つ目
                var itemUrl_11 = jsonKeyWord.item[10].image;
                BitmapImage imageSource_11 = new BitmapImage(new Uri(itemUrl_11));
                pbItemImage_11.Source = imageSource_11;

                //12つ目
                var itemUrl_12 = jsonKeyWord.item[11].image;
                BitmapImage imageSource_12 = new BitmapImage(new Uri(itemUrl_12));
                pbItemImage_12.Source = imageSource_12;

                //13つ目
                var itemUrl_13 = jsonKeyWord.item[12].image;
                BitmapImage imageSource_13 = new BitmapImage(new Uri(itemUrl_13));
                pbItemImage_13.Source = imageSource_13;

                //14つ目
                var itemUrl_14 = jsonKeyWord.item[13].image;
                BitmapImage imageSource_14 = new BitmapImage(new Uri(itemUrl_14));
                pbItemImage_14.Source = imageSource_14;

                //15つ目
                var itemUrl_15 = jsonKeyWord.item[14].image;
                BitmapImage imageSource_15 = new BitmapImage(new Uri(itemUrl_15));
                pbItemImage_15.Source = imageSource_15;

                //16つ目
                var itemUrl_16 = jsonKeyWord.item[15].image;
                BitmapImage imageSource_16 = new BitmapImage(new Uri(itemUrl_16));
                pbItemImage_16.Source = imageSource_16;
            }
            //catch (IndexOutOfRangeException)
            //{

            //}
            //catch (NullReferenceException)
            //{

            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("エラーが発生しました");
            //}
            catch (NullReferenceException)
            {

            }

        }

        //アイテム情報クリア
        public void Clear()
        {
            tbItemPrice_1.Text = null;
            tbItemName_1.Text = null;
            pbItemImage_1.Source = null;
            btItemUrl_1.IsEnabled = false;

            tbItemPrice_2.Text = null;
            tbItemName_2.Text = null;
            pbItemImage_2.Source = null;
            btItemUrl_2.IsEnabled = false;

            tbItemPrice_3.Text = null;
            tbItemName_3.Text = null;
            pbItemImage_3.Source = null;
            btItemUrl_3.IsEnabled = false;

            tbItemPrice_4.Text = null;
            tbItemName_4.Text = null;
            pbItemImage_4.Source = null;
            btItemUrl_4.IsEnabled = false;

            tbItemPrice_5.Text = null;
            tbItemName_5.Text = null;
            pbItemImage_5.Source = null;
            btItemUrl_5.IsEnabled = false;

            tbItemPrice_6.Text = null;
            tbItemName_6.Text = null;
            pbItemImage_6.Source = null;
            btItemUrl_6.IsEnabled = false;

            tbItemPrice_7.Text = null;
            tbItemName_7.Text = null;
            pbItemImage_7.Source = null;
            btItemUrl_7.IsEnabled = false;

            tbItemPrice_8.Text = null;
            tbItemName_8.Text = null;
            pbItemImage_8.Source = null;
            btItemUrl_8.IsEnabled = false;

            tbItemPrice_9.Text = null;
            tbItemName_9.Text = null;
            pbItemImage_9.Source = null;
            btItemUrl_9.IsEnabled = false;

            tbItemPrice_10.Text = null;
            tbItemName_10.Text = null;
            pbItemImage_10.Source = null;
            btItemUrl_10.IsEnabled = false;

            tbItemPrice_11.Text = null;
            tbItemName_11.Text = null;
            pbItemImage_11.Source = null;
            btItemUrl_11.IsEnabled = false;

            tbItemPrice_12.Text = null;
            tbItemName_12.Text = null;
            pbItemImage_12.Source = null;
            btItemUrl_12.IsEnabled = false;

            tbItemPrice_13.Text = null;
            tbItemName_13.Text = null;
            pbItemImage_13.Source = null;
            btItemUrl_13.IsEnabled = false;

            tbItemPrice_14.Text = null;
            tbItemName_14.Text = null;
            pbItemImage_14.Source = null;
            btItemUrl_14.IsEnabled = false;

            tbItemPrice_15.Text = null;
            tbItemName_15.Text = null;
            pbItemImage_15.Source = null;
            btItemUrl_15.IsEnabled = false;

            tbItemPrice_16.Text = null;
            tbItemName_16.Text = null;
            pbItemImage_16.Source = null;
            btItemUrl_16.IsEnabled = false;
        }

        public void ItemEnabled()
        {
            
        }

        //インフォメーションボタン
        private void Button_Infomation(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("値段が データなし となる場合がありますがご了承ください");
        }

        //ウィンドウ閉じるイベント
        private void ButtonCloseWin1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private void BtItemUrl1_Click(object sender, RoutedEventArgs e)
        {
            itemWindow.num = 0;
            itemWindow.Show();
        }

        private void BtItemUrl2_Click(object sender, RoutedEventArgs e)
        {
            itemWindow.num = 1;
            itemWindow.Show();
        }

        private void BtItemUrl3_Click(object sender, RoutedEventArgs e)
        {
            itemWindow.num = 2;
            itemWindow.Show();
        }

        private void BtItemUrl4_Click(object sender, RoutedEventArgs e)
        {
            itemWindow.num = 3;
            itemWindow.Show();
        }

        private void BtItemUrl5_Click(object sender, RoutedEventArgs e)
        {
            itemWindow.num = 4;
            itemWindow.Show();
        }

        private void BtItemUrl6_Click(object sender, RoutedEventArgs e)
        {
            itemWindow.num = 5;
            itemWindow.Show();
        }

        private void BtItemUrl7_Click(object sender, RoutedEventArgs e)
        {
            itemWindow.num = 6;
            itemWindow.Show();
        }

        private void BtItemUrl8_Click(object sender, RoutedEventArgs e)
        {
            itemWindow.num = 7;
            itemWindow.Show();
        }

        private void BtItemUrl9_Click(object sender, RoutedEventArgs e)
        {
            itemWindow.num = 8;
            itemWindow.Show();
        }

        private void BtItemUrl10_Click(object sender, RoutedEventArgs e)
        {
            itemWindow.num = 9;
            itemWindow.Show();
        }

        private void BtItemUrl11_Click(object sender, RoutedEventArgs e)
        {
            itemWindow.num = 10;
            itemWindow.Show();
        }

        private void BtItemUrl12_Click(object sender, RoutedEventArgs e)
        {
            itemWindow.num = 11;
            itemWindow.Show();
        }

        private void btItemUrl13_Click(object sender, RoutedEventArgs e)
        {
            itemWindow.num = 12;
            itemWindow.Show();
        }

        private void BtItemUrl14_Click(object sender, RoutedEventArgs e)
        {
            itemWindow.num = 13;
            itemWindow.Show();
        }

        private void BtItemUrl15_Click(object sender, RoutedEventArgs e)
        {
            itemWindow.num = 14;
            itemWindow.Show();
        }

        private void BtItemUrl16_Click(object sender, RoutedEventArgs e)
        {
            itemWindow.num = 15;
            itemWindow.Show();
        }
    }
}

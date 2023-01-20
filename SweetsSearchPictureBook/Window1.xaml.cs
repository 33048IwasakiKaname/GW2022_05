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
            itemInfo();
        }

        public Window1()
        {
            InitializeComponent();
        }

        public void Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                keyWord = wc.DownloadString(url + "&keyword=" + tbFreeWord.Text + "&type=" + cbClass.SelectedIndex);
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
                try
                {
                    keyWord = wc.DownloadString(url + "&keyword=" + tbFreeWord.Text + "&type=" + cbClass.SelectedIndex + "&max=10");
                    jsonKeyWord = JsonConvert.DeserializeObject<Rootobject>(keyWord);
                }
                catch (Exception)
                {
                    MessageBox.Show("ーーーーーーーーエラーーーーーーーー");
                }
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

            itemInfo();
        }

        public void countCheck()
        {

        }

        public void itemCount()
        {
            if (int.Parse(jsonKeyWord.count) == 0)
            {
                MessageBox.Show("見つかりませんでした");
                return;
            }
        }

        public void webBrowser()
        {
            System.Diagnostics.Process.Start(jsonKeyWord.item[0].url);
        }

        public string priceCheck(int num)
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

        public void itemInfo()
        {            
            try
            {
                //1つ目
                tbItemPrice_1.Text = priceCheck(0);
                tbItemName_1.Text = jsonKeyWord.item[0].name;
                var itemUrl_1 = jsonKeyWord.item[0].image;
                BitmapImage imageSource_1 = new BitmapImage(new Uri(itemUrl_1));
                pbItemImage_1.Source = imageSource_1;

                //2つ目
                tbItemPrice_2.Text = priceCheck(1);
                tbItemName_2.Text = jsonKeyWord.item[1].name;
                var itemUrl_2 = jsonKeyWord.item[1].image;
                BitmapImage imageSource_2 = new BitmapImage(new Uri(itemUrl_2));
                pbItemImage_2.Source = imageSource_2;

                //3つ目
                tbItemPrice_3.Text = priceCheck(2);
                tbItemName_3.Text = jsonKeyWord.item[2].name;               
                var itemUrl_3 = jsonKeyWord.item[2].image;
                BitmapImage imageSource_3 = new BitmapImage(new Uri(itemUrl_3));
                pbItemImage_3.Source = imageSource_3;

                //4つ目
                tbItemPrice_4.Text = priceCheck(3);
                tbItemName_4.Text = jsonKeyWord.item[3].name;                
                var itemUrl_4 = jsonKeyWord.item[3].image;
                BitmapImage imageSource_4 = new BitmapImage(new Uri(itemUrl_4));
                pbItemImage_4.Source = imageSource_4;

                //5つ目
                tbItemPrice_5.Text = priceCheck(4);
                tbItemName_5.Text = jsonKeyWord.item[4].name;
                var itemUrl_5 = jsonKeyWord.item[4].image;
                BitmapImage imageSource_5 = new BitmapImage(new Uri(itemUrl_5));
                pbItemImage_5.Source = imageSource_5;

                //6つ目
                tbItemPrice_6.Text = priceCheck(5);
                tbItemName_6.Text = jsonKeyWord.item[5].name;
                var itemUrl_6 = jsonKeyWord.item[5].image;
                BitmapImage imageSource_6 = new BitmapImage(new Uri(itemUrl_6));
                pbItemImage_6.Source = imageSource_6;

                //7つ目
                tbItemPrice_7.Text = priceCheck(6);
                tbItemName_7.Text = jsonKeyWord.item[6].name;
                var itemUrl_7 = jsonKeyWord.item[6].image;
                BitmapImage imageSource_7 = new BitmapImage(new Uri(itemUrl_7));
                pbItemImage_7.Source = imageSource_7;

                //8つ目
                tbItemPrice_8.Text = priceCheck(7);
                tbItemName_8.Text = jsonKeyWord.item[7].name;
                var itemUrl_8 = jsonKeyWord.item[7].image;
                BitmapImage imageSource_8 = new BitmapImage(new Uri(itemUrl_8));
                pbItemImage_8.Source = imageSource_8;

                //9つ目
                tbItemPrice_9.Text = priceCheck(8);
                tbItemName_9.Text = jsonKeyWord.item[8].name;
                var itemUrl_9 = jsonKeyWord.item[8].image;
                BitmapImage imageSource_9 = new BitmapImage(new Uri(itemUrl_9));
                pbItemImage_9.Source = imageSource_9;

                //10つ目
                tbItemPrice_10.Text = priceCheck(9);
                tbItemName_10.Text = jsonKeyWord.item[9].name;
                var itemUrl_10 = jsonKeyWord.item[9].image;
                BitmapImage imageSource_10 = new BitmapImage(new Uri(itemUrl_10));
                pbItemImage_10.Source = imageSource_10;

                //11つ目
                tbItemPrice_11.Text = priceCheck(10);
                tbItemName_11.Text = jsonKeyWord.item[10].name;
                var itemUrl_11 = jsonKeyWord.item[10].image;
                BitmapImage imageSource_11 = new BitmapImage(new Uri(itemUrl_11));
                pbItemImage_11.Source = imageSource_11;

                //12つ目
                tbItemPrice_12.Text = priceCheck(11);
                tbItemName_12.Text = jsonKeyWord.item[11].name;
                var itemUrl_12 = jsonKeyWord.item[11].image;
                BitmapImage imageSource_12 = new BitmapImage(new Uri(itemUrl_12));
                pbItemImage_12.Source = imageSource_12;

                //13つ目
                tbItemPrice_13.Text = priceCheck(12);
                tbItemName_13.Text = jsonKeyWord.item[12].name;
                var itemUrl_13 = jsonKeyWord.item[12].image;
                BitmapImage imageSource_13 = new BitmapImage(new Uri(itemUrl_13));
                pbItemImage_13.Source = imageSource_13;

                //14つ目
                tbItemPrice_14.Text = priceCheck(13);
                tbItemName_14.Text = jsonKeyWord.item[13].name;
                var itemUrl_14 = jsonKeyWord.item[13].image;
                BitmapImage imageSource_14 = new BitmapImage(new Uri(itemUrl_14));
                pbItemImage_14.Source = imageSource_14;

                //15つ目
                tbItemPrice_15.Text = priceCheck(14);
                tbItemName_15.Text = jsonKeyWord.item[14].name;
                var itemUrl_15 = jsonKeyWord.item[14].image;
                BitmapImage imageSource_15 = new BitmapImage(new Uri(itemUrl_15));
                pbItemImage_15.Source = imageSource_15;

                //16つ目
                tbItemPrice_16.Text = priceCheck(15);
                tbItemName_16.Text = jsonKeyWord.item[15].name;
                var itemUrl_16 = jsonKeyWord.item[15].image;
                BitmapImage imageSource_16 = new BitmapImage(new Uri(itemUrl_16));
                pbItemImage_16.Source = imageSource_16;
            }
            catch (IndexOutOfRangeException)
            {
                
            }
            catch (NullReferenceException)
            {

            }
            catch (Exception)
            {
                MessageBox.Show("エラーが発生しました");
            }
            
        }

        private void button_Infomation(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("値段が データなし となる場合がありますがご了承ください");
        }

        private void buttonCloseWin1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btItemUrl1_Click(object sender, RoutedEventArgs e)
        {
            itemWindow.num = 0;
            itemWindow.Show();
        }

        private void btItemUrl2_Click(object sender, RoutedEventArgs e)
        {
            itemWindow.num = 1;
            itemWindow.Show();
        }

        private void btItemUrl3_Click(object sender, RoutedEventArgs e)
        {
            itemWindow.num = 2;
            itemWindow.Show();
        }

        private void btItemUrl4_Click(object sender, RoutedEventArgs e)
        {
            itemWindow.num = 3;
            itemWindow.Show();
        }

        private void btItemUrl5_Click(object sender, RoutedEventArgs e)
        {
            itemWindow.num = 4;
            itemWindow.Show();
        }

        private void btItemUrl6_Click(object sender, RoutedEventArgs e)
        {
            itemWindow.num = 5;
            itemWindow.Show();
        }

        private void btItemUrl7_Click(object sender, RoutedEventArgs e)
        {
            itemWindow.num = 6;
            itemWindow.Show();
        }

        private void btItemUrl8_Click(object sender, RoutedEventArgs e)
        {
            itemWindow.num = 7;
            itemWindow.Show();
        }

        private void btItemUrl9_Click(object sender, RoutedEventArgs e)
        {
            itemWindow.num = 8;
            itemWindow.Show();
        }

        private void btItemUrl10_Click(object sender, RoutedEventArgs e)
        {
            itemWindow.num = 9;
            itemWindow.Show();
        }

        private void btItemUrl11_Click(object sender, RoutedEventArgs e)
        {
            itemWindow.num = 10;
            itemWindow.Show();
        }

        private void btItemUrl12_Click(object sender, RoutedEventArgs e)
        {
            itemWindow.num = 11;
            itemWindow.Show();
        }

        private void btItemUrl13_Click(object sender, RoutedEventArgs e)
        {
            itemWindow.num = 12;
            itemWindow.Show();
        }

        private void btItemUrl14_Click(object sender, RoutedEventArgs e)
        {
            itemWindow.num = 13;
            itemWindow.Show();
        }

        private void btItemUrl15_Click(object sender, RoutedEventArgs e)
        {
            itemWindow.num = 14;
            itemWindow.Show();
        }

        private void btItemUrl16_Click(object sender, RoutedEventArgs e)
        {
            itemWindow.num = 15;
            itemWindow.Show();
        }
    }
}

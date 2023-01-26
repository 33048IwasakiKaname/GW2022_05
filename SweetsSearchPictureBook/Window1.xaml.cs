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
        public string newUrl;

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
            Clear();

            newUrl = url + "&keyword=" + tbFreeWord.Text + "&type=" + cbClass.SelectedIndex;
            try
            {
                if (cbArea.SelectedIndex != 0)
                {
                    newUrl = newUrl + "&type=99" + "&keyword=" + cbArea.Text;
                }

                keyWord = wc.DownloadString(newUrl);
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
                keyWord = wc.DownloadString(url + "&keyword=" + tbFreeWord.Text + "&type=" +
                        cbClass.SelectedIndex + "&max=" + 10);
                jsonKeyWord = JsonConvert.DeserializeObject<Rootobject>(keyWord);

                try
                {
                    
                }
                catch (JsonSerializationException)
                {
                    
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
            //======================お菓子のボタン設定======================
            Button[] buttonArray = { btItemUrl_1,btItemUrl_2,btItemUrl_3,btItemUrl_4,btItemUrl_5,btItemUrl_6,
                                         btItemUrl_7,btItemUrl_8,btItemUrl_9,btItemUrl_10,btItemUrl_11,btItemUrl_12,
                                         btItemUrl_13,btItemUrl_14,btItemUrl_15,btItemUrl_16};
            for (int i = 0; i < int.Parse(jsonKeyWord.count); i++)
            {
                buttonArray[i].IsEnabled = true;
            }


            //======================お菓子の値段設定======================
            TextBlock[] tbItemPrice = { tbItemPrice_1,tbItemPrice_2,tbItemPrice_3,tbItemPrice_4,tbItemPrice_5,
                                            tbItemPrice_6,tbItemPrice_7,tbItemPrice_8,tbItemPrice_9,tbItemPrice_10,
                                            tbItemPrice_11,tbItemPrice_12,tbItemPrice_13,tbItemPrice_14,tbItemPrice_15,
                                            tbItemPrice_16};
            for (int i = 0; i < int.Parse(jsonKeyWord.count); i++)
            {
                tbItemPrice[i].Text = PriceCheck(i);
            }


            //======================お菓子の名前設定======================
            TextBlock[] tbItemName = { tbItemName_1,tbItemName_2, tbItemName_3, tbItemName_4, tbItemName_5,
                                           tbItemName_6,tbItemName_7,tbItemName_8,tbItemName_9,tbItemName_10,
                                           tbItemName_11,tbItemName_12,tbItemName_13,tbItemName_14,tbItemName_15,
                                           tbItemName_16};
            for (int i = 0; i < int.Parse(jsonKeyWord.count); i++)
            {
                tbItemName[i].Text = jsonKeyWord.item[i].name;
            }


            //======================お菓子の画像設定======================

            BitmapImage imageSource_1,imageSource_2,imageSource_3,imageSource_4,imageSource_5,imageSource_6,imageSource_7,
                        imageSource_8,imageSource_9,imageSource_10,imageSource_11,imageSource_12,imageSource_13,
                        imageSource_14,imageSource_15,imageSource_16;

            imageSource_1 = imageSource_2 = imageSource_3 = imageSource_4 = imageSource_5 = imageSource_6 =
            imageSource_7 = imageSource_8 = imageSource_9 = imageSource_10 = imageSource_11 = imageSource_12 =
            imageSource_13 = imageSource_14 = imageSource_15 = imageSource_16 = null;

            try
            {
                imageSource_1 = new BitmapImage(new Uri(jsonKeyWord.item[0].image));
                imageSource_2 = new BitmapImage(new Uri(jsonKeyWord.item[1].image));
                imageSource_3 = new BitmapImage(new Uri(jsonKeyWord.item[2].image));
                imageSource_4 = new BitmapImage(new Uri(jsonKeyWord.item[3].image));
                imageSource_5 = new BitmapImage(new Uri(jsonKeyWord.item[4].image));
                imageSource_6 = new BitmapImage(new Uri(jsonKeyWord.item[5].image));
                imageSource_7 = new BitmapImage(new Uri(jsonKeyWord.item[6].image));
                imageSource_8 = new BitmapImage(new Uri(jsonKeyWord.item[7].image));
                imageSource_9 = new BitmapImage(new Uri(jsonKeyWord.item[8].image));
                imageSource_10 = new BitmapImage(new Uri(jsonKeyWord.item[9].image));
                imageSource_11 = new BitmapImage(new Uri(jsonKeyWord.item[10].image));
                imageSource_12 = new BitmapImage(new Uri(jsonKeyWord.item[11].image));
                imageSource_13 = new BitmapImage(new Uri(jsonKeyWord.item[12].image));
                imageSource_14 = new BitmapImage(new Uri(jsonKeyWord.item[13].image));
                imageSource_15 = new BitmapImage(new Uri(jsonKeyWord.item[14].image));
                imageSource_16 = new BitmapImage(new Uri(jsonKeyWord.item[15].image));                
            }
            catch (Exception)
            {
                
            }
            


            Image[] pbItemImage = { pbItemImage_1, pbItemImage_2, pbItemImage_3, pbItemImage_4, pbItemImage_5,
                                        pbItemImage_6,pbItemImage_7,pbItemImage_8,pbItemImage_9,pbItemImage_10,
                                        pbItemImage_11,pbItemImage_12,pbItemImage_13,pbItemImage_14,pbItemImage_15,
                                        pbItemImage_16};

            BitmapImage[] imageSorce = {imageSource_1,imageSource_2,imageSource_3,imageSource_4,imageSource_5,
                                        imageSource_6,imageSource_7,imageSource_8,imageSource_9,imageSource_10,
                                        imageSource_11,imageSource_12,imageSource_13,imageSource_14,imageSource_15,
                                        imageSource_16};



            for (int i = 0; i < int.Parse(jsonKeyWord.count); i++)
            {
                pbItemImage[i].Source = imageSorce[i];
            }


            //IndexOutOfRangeException

        }

        //アイテム情報クリア
        public void Clear()
        {
            TextBlock[] tbItemPrice = { tbItemPrice_1,tbItemPrice_2,tbItemPrice_3,tbItemPrice_4,tbItemPrice_5,
                                            tbItemPrice_6,tbItemPrice_7,tbItemPrice_8,tbItemPrice_9,tbItemPrice_10,
                                            tbItemPrice_11,tbItemPrice_12,tbItemPrice_13,tbItemPrice_14,tbItemPrice_15,
                                            tbItemPrice_16};
            TextBlock[] tbItemName = { tbItemName_1,tbItemName_2, tbItemName_3, tbItemName_4, tbItemName_5,
                                           tbItemName_6,tbItemName_7,tbItemName_8,tbItemName_9,tbItemName_10,
                                           tbItemName_11,tbItemName_12,tbItemName_13,tbItemName_14,tbItemName_15,
                                           tbItemName_16};
            Image[] pbItemImage = { pbItemImage_1, pbItemImage_2, pbItemImage_3, pbItemImage_4, pbItemImage_5,
                                        pbItemImage_6,pbItemImage_7,pbItemImage_8,pbItemImage_9,pbItemImage_10,
                                        pbItemImage_11,pbItemImage_12,pbItemImage_13,pbItemImage_14,pbItemImage_15,
                                        pbItemImage_16};
            Button[] btItemUrl = { btItemUrl_1,btItemUrl_2,btItemUrl_3,btItemUrl_4,btItemUrl_5,btItemUrl_6,btItemUrl_7,
                                        btItemUrl_8,btItemUrl_9,btItemUrl_10,btItemUrl_11,btItemUrl_12,btItemUrl_13,
                                        btItemUrl_14,btItemUrl_15,btItemUrl_16};

            for (int i = 0; i < 16; i++)
            {
                tbItemPrice[i].Text = null;
                tbItemName[i].Text = null;
                pbItemImage[i].Source = null;
                btItemUrl[i].IsEnabled = false;
            }
        }

        //アイテムが1つの時
        public void JsonOnlyOne(Item json)
        {
            tbItemName_1.Text = json.name;
        }

        //インフォメーションボタン
        private void Button_Infomation(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("・値段が データなし となる場合がありますがご了承ください。\n" +
                "・地域限定を指定した場合はキーワード検索は無効になり、その地域のお菓子がオールジャンルで表示されます。\n" + 
                "・まれに指定ワードに沿わない結果が表示される場合があります。");
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

        private void toggleBtArea_Checked(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("オンです");
        }
    }
}

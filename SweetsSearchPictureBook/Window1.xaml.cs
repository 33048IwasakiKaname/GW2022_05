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
        public Rootobject_only jsonKeyWord_only = new Rootobject_only();
        public ItemWindow itemWindow = new ItemWindow();

        public static string keyWord;
        public string newUrl;
        public int changeNum = 0;
        public int checkNumber = 0;
        
        //ウィンドウロード時
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            checkNumber = 1;
            cbArea.IsEnabled = false;
            keyWord = wc.DownloadString(url);
            jsonKeyWord = JsonConvert.DeserializeObject<Rootobject>(keyWord);
            ItemInfo();
        }

        public Window1()
        {
            InitializeComponent();
        }

        //検索ボタンクリック
        public void Search_Click(object sender, RoutedEventArgs e)
        {
            ItemSearch();
        }

        public void ItemSearch()
        {
            Clear();
            itemScrollViewer.ScrollToTop();
            newUrl = url + "&keyword=" + tbFreeWord.Text + "&type=" + cbClass.SelectedIndex;
            if (toggleBtSort.IsChecked == true && toggleBtArea.IsChecked == false)
            {
                newUrl += "&order=r";
            }
            try
            {
                if (cbArea.SelectedIndex != 0)
                {
                    var a = cbArea.SelectedItem.ToString();
                    a = a.Replace("System.Windows.Controls.ComboBoxItem:","");
                    newUrl = newUrl + "&type=99" + "&keyword=" + a;
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
            catch (JsonSerializationException)
            {
                try
                {   //カレー専用
                    keyWord = wc.DownloadString(newUrl + "&max=" + 10);
                    jsonKeyWord = JsonConvert.DeserializeObject<Rootobject>(keyWord);
                }
                catch (JsonSerializationException)
                {
                    try
                    {   //jsonデータが1つしかなかったとき
                        jsonKeyWord_only = JsonConvert.DeserializeObject<Rootobject_only>(keyWord);
                        OnlyItem(jsonKeyWord_only);
                        return;
                    }
                    catch (JsonSerializationException)
                    {
                        MessageBox.Show("もう一度入力しなおすか、検索ワードを変更してください。");
                    }
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
                tbFreeWord.Text = null;
                ItemSearch();
                return;
            }

            ItemInfo();
        }

        //アイテム数が0個の時
        public void ItemCount()
        {
            if (int.Parse(jsonKeyWord.count) == 0)
            {
                MessageBox.Show("見つかりませんでした");
                return;
            }
        }

        //アイテム数が1つのとき
        public void OnlyItem(Rootobject_only json)
        {
            btItemUrl_1.IsEnabled = true;                     
            tbItemName_1.Text = json.item.name.ToString();
            BitmapImage imageSource_17 = new BitmapImage(new Uri(json.item.image));
            pbItemImage_1.Source = imageSource_17;

            var a = json.item.price.ToString();

            if (json.item.price.ToString() != "{}")
            {
                if (json.item.price.ToString() != "0")
                {
                    tbItemPrice_1.Text = json.item.price.ToString() + "円";
                }    
            }
            else
            {
                tbItemPrice_1.Text = "データなし";
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

        //インフォメーションボタン(message box)
        private void Button_Infomation(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("・お菓子の表示が遅れる場合がありますが、そのままでお待ちください\n" +
                "・値段が データなし となる場合がありますがご了承ください。\n" +
                "・地域限定を指定した場合はキーワード検索は無効になり、その地域のお菓子がオールジャンルで表示されます。\n" + 
                "・まれに指定ワードに沿わない結果が表示される場合があります。");
        }

        //ウィンドウ閉じるイベント
        private void ButtonCloseWin1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        //アイテムウィンドウ表示
        public void ItemWindowShow(int num)
        {
            itemWindow.num = num;
            //itemWindow.Topmost = true;
            itemWindow.ShowDialog();
        }

        //地域限定トグルボタンイベント(チェック時)
        public void ToggleBtArea_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("地域限定検索を使用する場合は他の検索機能(キーワード検索・ジャンル検索・並び替え)が" +
                            "使用できません。\n" +　"地域限定検索のみ使用可能です。");
            cbArea.IsEnabled = true;           
            cbClass.IsEnabled = false;
            cbClass.SelectedIndex = 0;
            tbFreeWord.IsEnabled = false;
            toggleBtSort.IsChecked = false;
            toggleBtSort.IsEnabled = false;         
            cbArea.SelectedIndex = 1;
            changeNum = 1;
            ItemSearch();
        }

        //地域限定トグルボタンイベント(チェック外れたとき)
        private void ToggleBtArea_Unchecked(object sender, RoutedEventArgs e)
        {
            changeNum = 0;
            cbArea.IsEnabled = false;
            cbArea.SelectedIndex = 0;
            cbClass.IsEnabled = true;
            tbFreeWord.IsEnabled = true;
            toggleBtSort.IsEnabled = true;

            ItemSearch();
        }

        //並び替えトグルボタンイベント(チェック時)
        private void ToggleBtSort_Checked(object sender, RoutedEventArgs e)
        {
            Search_Click(sender,e);
        }


        private void BtItemUrl1_Click(object sender, RoutedEventArgs e)
        {
            ItemWindowShow(0);
        }

        private void BtItemUrl2_Click(object sender, RoutedEventArgs e)
        {
            ItemWindowShow(1);
        }

        private void BtItemUrl3_Click(object sender, RoutedEventArgs e)
        {
            ItemWindowShow(2);
        }

        private void BtItemUrl4_Click(object sender, RoutedEventArgs e)
        {
            ItemWindowShow(3);
        }

        private void BtItemUrl5_Click(object sender, RoutedEventArgs e)
        {
            ItemWindowShow(4);
        }

        private void BtItemUrl6_Click(object sender, RoutedEventArgs e)
        {
            ItemWindowShow(5);
        }

        private void BtItemUrl7_Click(object sender, RoutedEventArgs e)
        {
            ItemWindowShow(6);
        }

        private void BtItemUrl8_Click(object sender, RoutedEventArgs e)
        {
            ItemWindowShow(7);
        }

        private void BtItemUrl9_Click(object sender, RoutedEventArgs e)
        {
            ItemWindowShow(8);
        }

        private void BtItemUrl10_Click(object sender, RoutedEventArgs e)
        {
            ItemWindowShow(9);
        }

        private void BtItemUrl11_Click(object sender, RoutedEventArgs e)
        {
            ItemWindowShow(10);
        }

        private void BtItemUrl12_Click(object sender, RoutedEventArgs e)
        {
            ItemWindowShow(11);
        }

        private void BtItemUrl13_Click(object sender, RoutedEventArgs e)
        {
            ItemWindowShow(12);
        }

        private void BtItemUrl14_Click(object sender, RoutedEventArgs e)
        {
            ItemWindowShow(13);
        }

        private void BtItemUrl15_Click(object sender, RoutedEventArgs e)
        {
            ItemWindowShow(14);
        }

        private void BtItemUrl16_Click(object sender, RoutedEventArgs e)
        {
            ItemWindowShow(15);
        }

        private void cbClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (checkNumber != 0)
            {
                ItemSearch();
            }   
        }

        private void cbArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {         
            if (toggleBtArea.IsChecked == true && changeNum == 1)
            {
                ItemSearch();
            }
        }

        
    }
}

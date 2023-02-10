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
using System.IO;

namespace SweetsSearchPictureBook
{
    /// <summary>
    /// Window1.xaml の相互作用ロジック
    /// </summary>
    public partial class Window1 : Window
    {        
        public string url = "https://www.sysbird.jp/webapi/?apikey=guest&format=json&max=";

        public WebClient wc = new WebClient();
        public static Rootobject jsonKeyWord = new Rootobject();
        public Rootobject_only jsonKeyWord_only = new Rootobject_only();
        public ItemWindow itemWindow = new ItemWindow();

        public static string keyWord; //テキストボックス内のテキスト
        public string newUrl; //更新したURL
        public int changeNum = 0; //地域限定コンボボックスの処理にしよう
        public int checkNumber = 0; //ジャンルコンボボックスの処理に使用
        public int min = 0; //jsonのmin値
        public int max = 16; //jsonのmax値
        public static int pageCount = 1; //現在のページ番号
        public int itemMax = 80; //最大取得数
        public int count = 0; //アイテムが16個以上かどうかで使用する値
        public int printCount = 16; //表示できる最大個数
        public const int page_1num = 16;
        public const int page_2num = 32;
        public const int page_3num = 48;
        public const int page_4num = 64;
        public const int page_5num = 80;

        //ウィンドウロード時
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            checkNumber = 1;
            cbArea.IsEnabled = false;
            keyWord = wc.DownloadString(url + itemMax);
            keyWord = keyWord.Replace("\"type\":{}", "\"type\":\"データなし\"");
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

        //検索処理
        public void ItemSearch()
        {
            ReturnFirstPage();
            Clear();

            newUrl = url + itemMax + "&keyword=" + tbFreeWord.Text + "&type=" + cbClass.SelectedIndex;
            if (toggleBtSort.IsChecked == true && toggleBtArea.IsChecked == false)
            {
                newUrl += "&order=r";
            }
            try
            {
                if (cbArea.SelectedIndex != 0)
                {
                    string a = cbArea.SelectedItem.ToString();
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
            ItemCount();
            ItemInfo();
            
        }

        //アイテム数が0個の時
        public void ItemCount()
        {
            if (int.Parse(jsonKeyWord.count) == 0)
            {
                MessageBox.Show("見つかりませんでした");
                cbClass.SelectedIndex = 0;
                tbFreeWord.Text = null;
                ItemSearch();
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
            Clear();
            NextReturnButtonCheck();
            //======================お菓子のボタン設定======================
            Button[] buttonArray = { btItemUrl_1,btItemUrl_2,btItemUrl_3,btItemUrl_4,btItemUrl_5,btItemUrl_6,
                                         btItemUrl_7,btItemUrl_8,btItemUrl_9,btItemUrl_10,btItemUrl_11,btItemUrl_12,
                                         btItemUrl_13,btItemUrl_14,btItemUrl_15,btItemUrl_16};
            ItemCountNum();
            for (int i = 0; i < count; i++)
            {
                buttonArray[i].IsEnabled = true;
            }


            //======================お菓子の値段設定======================
            TextBlock[] tbItemPrice = { tbItemPrice_1,tbItemPrice_2,tbItemPrice_3,tbItemPrice_4,tbItemPrice_5,
                                            tbItemPrice_6,tbItemPrice_7,tbItemPrice_8,tbItemPrice_9,tbItemPrice_10,
                                            tbItemPrice_11,tbItemPrice_12,tbItemPrice_13,tbItemPrice_14,tbItemPrice_15,
                                            tbItemPrice_16};
            ItemCountNum();
            for (int i = 0; i < count; i++)
            {
                tbItemPrice[i].Text = PriceCheck(min + i);
            }


            //======================お菓子の名前設定======================
            TextBlock[] tbItemName = { tbItemName_1,tbItemName_2, tbItemName_3, tbItemName_4, tbItemName_5,
                                           tbItemName_6,tbItemName_7,tbItemName_8,tbItemName_9,tbItemName_10,
                                           tbItemName_11,tbItemName_12,tbItemName_13,tbItemName_14,tbItemName_15,
                                           tbItemName_16};
            ItemCountNum();
            for (int i = 0; i < count; i++)
            {
                tbItemName[i].Text = jsonKeyWord.item[min + i].name;
            }
            

            //======================お菓子の画像設定======================

            BitmapImage imageSource_1,imageSource_2,imageSource_3,imageSource_4,imageSource_5,imageSource_6,imageSource_7,
                        imageSource_8,imageSource_9,imageSource_10,imageSource_11,imageSource_12,imageSource_13,
                        imageSource_14,imageSource_15,imageSource_16;

            imageSource_1 = imageSource_2 = imageSource_3 = imageSource_4 = imageSource_5 = imageSource_6 =
            imageSource_7 = imageSource_8 = imageSource_9 = imageSource_10 = imageSource_11 = imageSource_12 =
            imageSource_13 = imageSource_14 = imageSource_15 = imageSource_16 = null;

            BitmapImage[] imageSource = { imageSource_1, imageSource_2, imageSource_3, imageSource_4,
                                                  imageSource_5,imageSource_6,imageSource_7,imageSource_8,
                                                  imageSource_9,imageSource_10,imageSource_11,imageSource_12,
                                                  imageSource_13,imageSource_14,imageSource_15,imageSource_16,};
            ItemCountNum();
            for (int i = 0; i < count; i++)
            {
                if (jsonKeyWord.item[min + i].image == null)
                {
                    jsonKeyWord.item[min + i].image = "https://www.shoshinsha-design.com/wp-content/uploads/2020/05/noimage-760x460.png";
                }
                imageSource[i] = new BitmapImage(new Uri(jsonKeyWord.item[min + i].image));
            };

            Image[] pbItemImage = { pbItemImage_1, pbItemImage_2, pbItemImage_3, pbItemImage_4, pbItemImage_5,
                                        pbItemImage_6,pbItemImage_7,pbItemImage_8,pbItemImage_9,pbItemImage_10,
                                        pbItemImage_11,pbItemImage_12,pbItemImage_13,pbItemImage_14,pbItemImage_15,
                                        pbItemImage_16};
            ItemCountNum();
            for (int i = 0; i < count; i++)
            {
                pbItemImage[i].Source = imageSource[i];
            }
            
        }

        //1ページ目に戻る
        public void ReturnFirstPage()
        {
            pageCount = 1;
            min = 0;
            max = 16;
        }

        //次のページへ
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            
            itemScrollViewer.ScrollToTop();
            pageCount++;
            max += printCount;
            min += printCount;
            ItemInfo();
        }

        //前のページへ
        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            itemScrollViewer.ScrollToTop();
            pageCount--;
            max -= printCount;
            min -= printCount;
            ItemInfo();
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

        //取得したjsonの数がいくつか判定
        public void ItemCountNum()
        {
            switch (pageCount)
            {
                case 1:
                    if (int.Parse(jsonKeyWord.count) > page_1num)
                    {
                        count = page_1num;
                        nextButton.IsEnabled = true;
                    }
                    else
                    {
                        count = int.Parse(jsonKeyWord.count);
                        nextButton.IsEnabled = false;
                    }
                    return;
                case 2:
                    if (int.Parse(jsonKeyWord.count) < page_2num)
                    {
                        count = int.Parse(jsonKeyWord.count)-page_1num;
                        nextButton.IsEnabled = false;
                    }
                    return;
                case 3:
                    if (int.Parse(jsonKeyWord.count) < page_3num)
                    {
                        count = int.Parse(jsonKeyWord.count) - page_2num;
                        nextButton.IsEnabled = false;
                    }
                    return;
                case 4:
                    if (int.Parse(jsonKeyWord.count) < page_4num)
                    {
                        count = int.Parse(jsonKeyWord.count) - page_3num;
                        nextButton.IsEnabled = false;
                    }
                    return;
                case 5:
                    if (int.Parse(jsonKeyWord.count) < page_5num)
                    {
                        count = int.Parse(jsonKeyWord.count) - page_4num;
                        nextButton.IsEnabled = false;
                    }
                    return;

                default:
                    return;
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

        //ページ遷移ボタンのvisible判定
        public void NextReturnButtonCheck()
        {
            switch (pageCount)
            {
                case 1:
                    returnButton.IsEnabled = false;
                    return;
                case 2:
                    returnButton.IsEnabled = true;
                    return;
                case 3:
                    nextButton.IsEnabled = true;
                    return;
                case 4:
                    nextButton.IsEnabled = true;
                    return;
                case 5:
                    nextButton.IsEnabled = false;
                    return;
                default:
                    returnButton.IsEnabled = true;
                    return;
            }
        }

        //トグルボタンのチェックが外れた時
        public void Unchecked()
        {
            changeNum = 0;
            cbArea.IsEnabled = false;
            cbArea.SelectedIndex = 0;
            cbClass.IsEnabled = true;
            tbFreeWord.IsEnabled = true;
            toggleBtSort.IsEnabled = true;
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
            if (pageCount != 1)
            {
                ReturnButton_Click(sender, e);
            }
            ItemSearch();
        }

        //地域限定トグルボタンイベント(チェック外れたとき)
        private void ToggleBtArea_Unchecked(object sender, RoutedEventArgs e)
        {
            Unchecked();
            if (pageCount != 1)
            {
                ReturnButton_Click(sender, e);
            }
            ItemSearch();
        }

        //並び替えトグルボタンイベント(チェック時)
        private void ToggleBtSort_Checked(object sender, RoutedEventArgs e)
        {
            if (pageCount != 1)
            {
                ReturnButton_Click(sender, e);
            }           
            Search_Click(sender,e);
        }

        //並び替えトグルボタンイベント(チェック外れたとき)
        private void toggleBtSort_Unchecked(object sender, RoutedEventArgs e)
        {
            Unchecked();
            if (pageCount != 1)
            {
                ReturnButton_Click(sender, e);
            }
            ItemSearch();
        }

        //ジャンルコンボボックス変更時
        private void cbClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (checkNumber != 0)
            {
                ItemSearch();
            }
        }

        //地域限定コンボボックス変更時
        private void cbArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (toggleBtArea.IsChecked == true && changeNum == 1)
            {
                ItemSearch();
            }
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
    }
}

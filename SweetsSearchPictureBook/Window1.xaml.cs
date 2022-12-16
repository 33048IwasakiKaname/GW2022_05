using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public static WebClient wc = new WebClient();
        public string url = "https://www.sysbird.jp/webapi/?apikey=guest&format=json&max=16";

        public string keyWord;
        public Rootobject jsonKeyWord = new Rootobject();

        public Window1()
        {
            InitializeComponent();

            cbClass.Items.Add("すべて");
            cbClass.Items.Add("せんべい・和風");
            cbClass.Items.Add("クッキー・洋菓子");
            cbClass.Items.Add("スナック");
            cbClass.Items.Add("チョコレート");
            cbClass.Items.Add("飴・ガム");        
            cbClass.SelectedValue = "すべて";

            cbMaker.Items.Add("すべて");
            cbMaker.Items.Add("サッポロ");
            cbMaker.Items.Add("チロルチョコ");          
            cbMaker.Items.Add("不二家");
            cbMaker.Items.Add("カバヤ");
            cbMaker.Items.Add("ロッテ");
            cbMaker.Items.Add("明治製菓");
            cbMaker.Items.Add("亀田製菓");
            cbMaker.Items.Add("森永");
            cbMaker.Items.Add("おやつカンパニー");
            cbMaker.Items.Add("カルビー");
            cbMaker.Items.Add("ハウス");
            cbMaker.Items.Add("東ハト");
            cbMaker.Items.Add("グリコ");
            cbMaker.Items.Add("ネスレ");
            cbMaker.Items.Add("エスビー");
            cbMaker.Items.Add("フルタ");
            cbMaker.Items.Add("ギンビス");
            cbMaker.Items.Add("ブルボン");
            cbMaker.Items.Add("ヤマヨシ");
            cbMaker.Items.Add("UHA味覚糖");
            cbMaker.Items.Add("湖池屋");
            cbMaker.Items.Add("日清シスコ");
            cbMaker.SelectedValue = "すべて";
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            keyWord = wc.DownloadString(url + "&keyword=" + tbFreeWord.Text);
            jsonKeyWord = JsonConvert.DeserializeObject<Rootobject>(keyWord);

            //1つ目
            tbItemName_1.Text = jsonKeyWord.item[0].name;
            tbItemPrice_1.Text = jsonKeyWord.item[0].price.ToString() + "円";
            var itemUrl_1 = jsonKeyWord.item[0].image;
            BitmapImage imageSource_1 = new BitmapImage(new Uri(itemUrl_1));
            pbItemImage_1.Source = imageSource_1;

            //2つ目
            tbItemName_2.Text = jsonKeyWord.item[1].name;
            tbItemPrice_2.Text = jsonKeyWord.item[1].price.ToString() + "円";
            var itemUrl_2 = jsonKeyWord.item[1].image;
            BitmapImage imageSource_2 = new BitmapImage(new Uri(itemUrl_2));
            pbItemImage_2.Source = imageSource_2;

            //3つ目
            tbItemName_3.Text = jsonKeyWord.item[2].name;
            tbItemPrice_3.Text = jsonKeyWord.item[2].price.ToString() + "円";
            var itemUrl_3 = jsonKeyWord.item[2].image;
            BitmapImage imageSource_3 = new BitmapImage(new Uri(itemUrl_3));
            pbItemImage_3.Source = imageSource_3;

            //4つ目
            tbItemName_4.Text = jsonKeyWord.item[3].name;
            tbItemPrice_4.Text = jsonKeyWord.item[3].price.ToString() + "円";
            var itemUrl_4 = jsonKeyWord.item[3].image;
            BitmapImage imageSource_4 = new BitmapImage(new Uri(itemUrl_4));
            pbItemImage_4.Source = imageSource_4;

            //5つ目
            tbItemName_5.Text = jsonKeyWord.item[4].name;
            tbItemPrice_5.Text = jsonKeyWord.item[4].price.ToString() + "円";
            var itemUrl_5 = jsonKeyWord.item[4].image;
            BitmapImage imageSource_5 = new BitmapImage(new Uri(itemUrl_5));
            pbItemImage_5.Source = imageSource_5;

            //6つ目
            tbItemName_6.Text = jsonKeyWord.item[5].name;
            tbItemPrice_6.Text = jsonKeyWord.item[5].price.ToString() + "円";
            var itemUrl_6 = jsonKeyWord.item[5].image;
            BitmapImage imageSource_6 = new BitmapImage(new Uri(itemUrl_6));
            pbItemImage_6.Source = imageSource_6;

            //7つ目
            tbItemName_7.Text = jsonKeyWord.item[6].name;
            tbItemPrice_7.Text = jsonKeyWord.item[6].price.ToString() + "円";
            var itemUrl_7 = jsonKeyWord.item[6].image;
            BitmapImage imageSource_7 = new BitmapImage(new Uri(itemUrl_7));
            pbItemImage_7.Source = imageSource_7;

            //8つ目
            tbItemName_8.Text = jsonKeyWord.item[7].name;
            tbItemPrice_8.Text = jsonKeyWord.item[7].price.ToString() + "円";
            var itemUrl_8 = jsonKeyWord.item[7].image;
            BitmapImage imageSource_8 = new BitmapImage(new Uri(itemUrl_8));
            pbItemImage_8.Source = imageSource_8;

            //9つ目
            tbItemName_9.Text = jsonKeyWord.item[8].name;
            tbItemPrice_9.Text = jsonKeyWord.item[8].price.ToString() + "円";
            var itemUrl_9 = jsonKeyWord.item[8].image;
            BitmapImage imageSource_9 = new BitmapImage(new Uri(itemUrl_9));
            pbItemImage_9.Source = imageSource_9;

            //10つ目
            tbItemName_10.Text = jsonKeyWord.item[9].name;
            tbItemPrice_10.Text = jsonKeyWord.item[9].price.ToString() + "円";
            var itemUrl_10 = jsonKeyWord.item[9].image;
            BitmapImage imageSource_10 = new BitmapImage(new Uri(itemUrl_10));
            pbItemImage_10.Source = imageSource_10;

            //11つ目
            tbItemName_11.Text = jsonKeyWord.item[10].name;
            tbItemPrice_11.Text = jsonKeyWord.item[10].price.ToString() + "円";
            var itemUrl_11 = jsonKeyWord.item[10].image;
            BitmapImage imageSource_11 = new BitmapImage(new Uri(itemUrl_11));
            pbItemImage_11.Source = imageSource_11;

            //12つ目
            tbItemName_12.Text = jsonKeyWord.item[11].name;
            tbItemPrice_12.Text = jsonKeyWord.item[11].price.ToString() + "円";
            var itemUrl_12 = jsonKeyWord.item[11].image;
            BitmapImage imageSource_12 = new BitmapImage(new Uri(itemUrl_12));
            pbItemImage_12.Source = imageSource_12;

            //13つ目
            tbItemName_13.Text = jsonKeyWord.item[12].name;
            tbItemPrice_13.Text = jsonKeyWord.item[12].price.ToString() + "円";
            var itemUrl_13 = jsonKeyWord.item[12].image;
            BitmapImage imageSource_13 = new BitmapImage(new Uri(itemUrl_13));
            pbItemImage_13.Source = imageSource_13;

            //14つ目
            tbItemName_14.Text = jsonKeyWord.item[13].name;
            tbItemPrice_14.Text = jsonKeyWord.item[13].price.ToString() + "円";
            var itemUrl_14 = jsonKeyWord.item[13].image;
            BitmapImage imageSource_14 = new BitmapImage(new Uri(itemUrl_14));
            pbItemImage_14.Source = imageSource_14;

            //15つ目
            tbItemName_15.Text = jsonKeyWord.item[14].name;
            tbItemPrice_15.Text = jsonKeyWord.item[14].price.ToString() + "円";
            var itemUrl_15 = jsonKeyWord.item[14].image;
            BitmapImage imageSource_15 = new BitmapImage(new Uri(itemUrl_15));
            pbItemImage_15.Source = imageSource_15;

            //16つ目
            tbItemName_16.Text = jsonKeyWord.item[15].name;
            tbItemPrice_16.Text = jsonKeyWord.item[15].price.ToString() + "円";
            var itemUrl_16 = jsonKeyWord.item[15].image;
            BitmapImage imageSource_16 = new BitmapImage(new Uri(itemUrl_16));
            pbItemImage_16.Source = imageSource_16;
        }
    }
}

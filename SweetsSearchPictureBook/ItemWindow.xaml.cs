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

        public Window1 window1 = new Window1();
        public Rootobject jsonKeyWord = new Rootobject();
        public string url;
        public int num;

        public ItemWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            jsonKeyWord = JsonConvert.DeserializeObject<Rootobject>(window1.keyWord);
            url = jsonKeyWord.item[num].url;
            BitmapImage imageSource = new BitmapImage(new Uri(url));
            itemImage.Source = imageSource;
        }
    }
}

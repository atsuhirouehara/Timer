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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Timer.View
{
    /// <summary>
    /// ComparePage.xaml の相互作用ロジック
    /// </summary>
    public partial class ComparePage : Page
    {
        public ComparePage()
        {
            InitializeComponent();
        }

        // トップページへ
        private void TopPage_Click(object sender, RoutedEventArgs e)
        {
            var topPage = new TopPage();
            NavigationService.Navigate(topPage);
        }

        // 履歴ページへ
        private void HistoryPage_Click(object sender, RoutedEventArgs e)
        {
            var historyPage = new HistoryPage();
            NavigationService.Navigate(historyPage);
        }
    }
}

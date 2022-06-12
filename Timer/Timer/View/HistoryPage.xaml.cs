using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Timer.ViewModel;

namespace Timer.View
{
    /// <summary>
    /// HistoryPage.xaml の相互作用ロジック
    /// </summary>
    public partial class HistoryPage : Page
    {
        public HistoryPage()
        {
            InitializeComponent();
        }

        // トップページへ
        private void TopPage_Click(object sender, RoutedEventArgs e)
        {
            var topPage = new TopPage();
            NavigationService.Navigate(topPage);
        }

        // 比較ページへ
        private void ComparePage_Click(object sender, RoutedEventArgs e)
        {
            var comparePage = new ComparePage();
            NavigationService.Navigate(comparePage);
        }


        // 取得ボタン押下時の処理
        private void GetHistory_Button(object sender, RoutedEventArgs e)
        {
            TimerUsecase timerUsecase = new();
            DataTable data = timerUsecase.SendToRepository(fromDate.ToString(), untilDate.ToString());
            DataGrid.DataContext = data;
        }
    }
}

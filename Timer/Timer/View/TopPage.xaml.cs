using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Timer.ViewModel;

namespace Timer.View
{

    /// <summary>
    /// TopPage.xaml の相互作用ロジック
    /// </summary>
    public partial class TopPage : Page
    {

        readonly DispatcherTimer dispatcherTimer;   // タイマーオブジェクト
        DateTime StartTime;                         // カウント開始時刻
        TimeSpan nowtimespan;                       // Startボタンが押されてから現在までの経過時間
        TimeSpan oldtimespan;                       // 一時停止ボタンが押されるまでに経過した時間の蓄積

        public TopPage()
        {
            InitializeComponent();

            
            // コンポーネントの状態を初期化　
            lblTime.Content = "00:00:00";
            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;
            btnReset.IsEnabled = true;

            // タイマーのインスタンスを生成
            dispatcherTimer = new DispatcherTimer(DispatcherPriority.Normal)
            {
                Interval = new TimeSpan(0, 0, 0, 1)
            };
#pragma warning disable CS8622 // パラメーターの型における参照型の NULL 値の許容が、ターゲット デリゲートと一致しません。おそらく、NULL 値の許容の属性が原因です。
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
        }

        // タイマー Tick処理
        void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            nowtimespan = DateTime.Now.Subtract(StartTime);
            lblTime.Content = oldtimespan.Add(nowtimespan).ToString(@"hh\:mm\:ss");

        }

        // ボタンクリック時の処理分岐
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Control ctrl = (Control)sender;
            switch (ctrl.Name)
            {
                case "btnStart":
                    TimerStart();
                    break;

                case "btnStop":
                    TimerStop();
                    break;

                case "btnReset":
                    TimerReset();
                    break;

                case "btnSave":
                    Save(lblTime.Content, textBox.Text);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// タイマー操作：開始
        /// </summary>
        private void TimerStart()
        {
            btnStart.IsEnabled = false;
            btnStop.IsEnabled = true;
            btnReset.IsEnabled = false;
            StartTime = DateTime.Now;
            dispatcherTimer.Start();
        }

        /// <summary>
        /// タイマー操作：停止
        /// </summary>
        private void TimerStop()
        {
            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;
            btnReset.IsEnabled = true;
            oldtimespan = oldtimespan.Add(nowtimespan);
            dispatcherTimer.Stop();
        }

        /// <summary>
        /// タイマー操作：リセット
        /// </summary>
        private void TimerReset()
        {
            oldtimespan = new TimeSpan();
            lblTime.Content = "00:00:00";
        }

        /// <summary>
        /// 保存(Save)用ボタンの機能
        /// </summary>
        private void Save(object time, string text)
        {
            TimerUsecase timerUsecase = new TimerUsecase();
            var sendResult = timerUsecase.SendToRepository(time, text);

            if (!sendResult)
            {
                MessageBox.Show("保存に失敗しました。");
            }
            else
            {
                MessageBox.Show("保存に成功しました。");
                textBox.Clear();
                TimerReset();
            }
        }

        // 比較ページへ
        private void ComparePage_Click(object sender, RoutedEventArgs e)
        {
            var comparePage = new ComparePage();
            NavigationService.Navigate(comparePage);
        }

        // 履歴ページへ
        private void HistoryPage_Click(object sender, RoutedEventArgs e)
        {
            var historyPage = new HistoryPage();
            NavigationService.Navigate(historyPage);
        }
    }

    
}

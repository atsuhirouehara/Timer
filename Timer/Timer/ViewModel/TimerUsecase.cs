using System.Data;
using Timer.Model;

namespace Timer.ViewModel
{
    internal class TimerUsecase
    {

        /// <summary>
        /// データ保存を呼び出すメソッド
        /// </summary>
        /// <param name="time"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool SendToRepository(object time, string text)
        {
            DataBaseConnect dataBaseConnect = new();
            var saveResult = dataBaseConnect.InsertRecord(time, text);

            if (saveResult)
            {
                return true;
            }
            return false;
        }

        // 指定した期間内データ取得を呼び出すメソッド
        public DataTable SendToRepository(string fromDate, string untilDate)
        {
            DataBaseConnect dataBaseConnect = new();
            DataTable data = dataBaseConnect.GetHistoryData(fromDate, untilDate);
            return data;
        }
    }
}

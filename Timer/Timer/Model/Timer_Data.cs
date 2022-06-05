using SQLite;

namespace Timer.Model
{
    public class Timer_Data
    {

        /// <summary>
        /// 保存日時
        /// </summary>
        [PrimaryKey]
        public string? SaveDateTime { get; set; }

        /// <summary>
        /// 学習時間
        /// </summary>
        public string? TotalTime { get; set; }

        /// <summary>
        /// コメント
        /// </summary>
        public string? Text { get; set; }
    }
}

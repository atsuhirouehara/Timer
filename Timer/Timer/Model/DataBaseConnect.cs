using System;
using System.Windows;
using System.Data.SQLite;
using System.Text;
using System.Data;

namespace Timer.Model
{
    public class DataBaseConnect
    {


        /// <summary>
        /// データベースへ保存するメソッド
        /// </summary>
        /// <param name="time"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool InsertRecord(object time, string text)
        {
            CreateTable();

            DateTime dt = DateTime.Now;
            dt.ToString();
            var insert_query = "INSERT INTO Timer_Data (SaveDateTime,TotalTime,Text) VALUES (" + $"'{dt}','{time}','{text}')"; 
            var result = ExecuteNonQuery(insert_query.ToString());

            if (!result)
            {
                return false;
            }
            else
            {
                return true;
            }
        }    

        /// <summary>
        /// データベースから選択された期間のデータを取得するメソッド
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="untilDate"></param>
        /// <returns></returns>
        public DataTable GetHistoryData(string fromDate, string untilDate)
        {
            var get_query = "SELECT * FROM Timer_Data where SaveDateTime BETWEEN" + $"'{fromDate}'" + "AND" + $"'{untilDate}'";
            DataTable data = GetData(get_query);

            return data;
        }

        /// <summary>
        /// クエリを実行するメソッド
        /// </summary>
        /// <param name="query"></param>
        private bool ExecuteNonQuery(string query)
        {
            try
            {
                // 接続先を指定
                using (var conn = new SQLiteConnection("Data Source=C:/SQLite/TImer_Data/Timer.db;Version=3;"))
                using (var command = conn.CreateCommand())
                {
                    // 接続
                    conn.Open();

                    // コマンドの実行処理
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                //例外が発生した時はメッセージボックスを表示
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// テーブルが存在しなければ作成するメソッド
        /// </summary>
        private void CreateTable()
        {
            // テーブル名が存在しなければ作成する
            StringBuilder query = new();
            query.Clear();
            query.Append("CREATE TABLE IF NOT EXISTS Timer_Data (");
            query.Append(" SaveDateTime TEXT NOT NULL");
            query.Append(" ,TotalTime TEXT");
            query.Append(" ,Text TEXT");
            query.Append(" ,primary key (SaveDateTime)");
            query.Append(")");

            // クエリー実行
            ExecuteNonQuery(query.ToString());
        }

        /// <summary>
        /// 指定範囲のデータを抽出し、DataTableへ格納し返却するメソッド
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private DataTable GetData(string query)
        {
     
            DataTable dt = new DataTable();

            try
            {
                // 接続先を指定
                using (var conn = new SQLiteConnection("Data Source=C:/SQLite/TImer_Data/Timer.db;Version=3;"))
                using (var command = conn.CreateCommand())
                {
                    // 接続
                    conn.Open();

                    // コマンドの実行処理
                    command.CommandText = query;
                    // 読み込む処理が
                    command.ExecuteNonQuery();

                    // DataAdapterの生成
                    SQLiteDataAdapter da = new SQLiteDataAdapter(command);

                    // データベースからデータを取得
                    da.Fill(dt);

                }

            }
            catch (Exception ex)
            {
                //例外が発生した時はメッセージボックスを表示
                MessageBox.Show(ex.Message);
            }
            return dt;
        }
    }
}

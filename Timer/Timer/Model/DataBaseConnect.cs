using System;
using System.Data.SQLite;
using System.Windows;

namespace Timer.Model
{
    public class DataBaseConnect
    {
        /// <summary>
        /// データベースへ保存するためのメソッド
        /// </summary>
        /// <param name="time"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool saveToDb(object time, string text)
        {

                DateTime dt = DateTime.Now;
                string sql_insert = "INSERT INTO Timer_Data ( SaveDateTime, TotalTime, Text) VALUES('" + dt + "','" + time + "','" + text + "')";

                //DbctlClass
                Dbctl dbctl = new Dbctl();

                try
                {
                    dbctl.ExecuteNonQuery(sql_insert);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    dbctl.Close();
                    return false;
                }
                finally
                {
                    dbctl.Close();
                }

            return true;
        }

        /// <summary>
        /// データベースとやりとりするためのクラス
        /// </summary>
        class Dbctl
        {
            private readonly SQLiteConnection conn;

            /// <summary>
            /// DBと接続する処理
            /// </summary>
            public Dbctl()
            {
                string conn_str = "Data Source=C:/Users/user/Desktop/MyProject/Timer/Timer.db;Version=3;";
                conn = new SQLiteConnection(conn_str);

                conn.Open();
            }

            /// <summary>
            /// DBを閉じる処理
            /// </summary>
            public void Close()
            {
                conn.Close();
                conn.Dispose();
            }

            /// <summary>
            /// クエリで結果を返さないSQL文などを実行するときに使用
            /// </summary>
            public void ExecuteNonQuery(string sql)
            {
                SQLiteCommand sqlCom = new SQLiteCommand(sql, conn);
                sqlCom.ExecuteNonQuery();
            }

            /// <summary>
            /// Adapterでまとめてデータを取得するときに使用
            /// </summary>
            public SQLiteDataAdapter ExecuteQueryAdapter(string sql)
            {
                SQLiteDataAdapter Adapter = new SQLiteDataAdapter(sql, conn);
                return Adapter;
            }

            /// <summary>
            /// データを取得してそのデータを１つずつや1行ずつ編集取得していくことができ、データ分析するときに使用
            /// </summary>
            public SQLiteDataReader ExecuteQueryReader(string sql)
            {
                SQLiteCommand sqlCom = new SQLiteCommand(sql, conn);
                SQLiteDataReader reader = sqlCom.ExecuteReader();

                return reader;
            }

        }
    }
}

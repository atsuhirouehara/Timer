using System;
using System.Data.SQLite;
using System.Text;
using System.Windows;
using System.Runtime.InteropServices;

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
        public static bool SaveToDb(object time, string text)
        {

            DateTime dt = DateTime.Now;
            string sql_insert = "INSERT INTO Timer_Data ( SaveDateTime, TotalTime, Text) VALUES('" + dt + "','" + time + "','" + text + "')";

            //DbctlClass
            Dbctl dbctl = new();

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

            // INIファイルを読み込む準備
            // Win32APIの GetPrivateProfileString を使う宣言
            [DllImport("KERNEL32.DLL", CharSet = CharSet.Unicode)]
            public static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);
            // iniファイル名を決める（実行ファイルが置かれたフォルダと同じ場所）
            readonly string iniFileName = AppDomain.CurrentDomain.BaseDirectory + "information.ini";

            private readonly SQLiteConnection conn;

            /// <summary>
            /// DBと接続する処理
            /// </summary>
            public Dbctl()
            {
                // iniファイルから文字列を取得
                StringBuilder iniFilePath = new(1024);
                GetPrivateProfileString("DataBase_info", "SQLite_Path", "SQLiteFilePathが設定されていません", iniFilePath, Convert.ToUInt32(iniFilePath.Capacity), iniFileName);
                
                string conn_str = "Data Source=" + iniFilePath.ToString() + ";Version=3;";
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
                SQLiteCommand sqlCom = new(sql, conn);
                sqlCom.ExecuteNonQuery();
            }

            /// <summary>
            /// Adapterでまとめてデータを取得するときに使用
            /// </summary>
            public SQLiteDataAdapter ExecuteQueryAdapter(string sql)
            {
                SQLiteDataAdapter Adapter = new(sql, conn);
                return Adapter;
            }

            /// <summary>
            /// データを取得してそのデータを１つずつや1行ずつ編集取得していくことができ、データ分析するときに使用
            /// </summary>
            public SQLiteDataReader ExecuteQueryReader(string sql)
            {
                SQLiteCommand sqlCom = new(sql, conn);
                SQLiteDataReader reader = sqlCom.ExecuteReader();

                return reader;
            }

        }
    }
}

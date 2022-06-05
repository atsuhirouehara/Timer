using System;
using SQLite;
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
        public static bool SaveToDb(object time, string text)
        {
            string databaseName = "Timer.db";
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // ドキュメント内にDBファイルが自動生成される
            string databasePath = System.IO.Path.Combine(folderPath, databaseName);

            using var connection = new SQLiteConnection(databasePath);

            try
            {
                DateTime dt = DateTime.Now;
                var timer_data = new Timer_Data()
                {
                    SaveDateTime = dt.ToString(),
                    TotalTime = time.ToString(),
                    Text = text,
                };

                
                connection.CreateTable<Timer_Data>();
                connection.Insert(timer_data);
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                connection.Close();
                return false;
            }

            return true;
        }
    }
}

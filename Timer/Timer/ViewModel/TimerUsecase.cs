using Timer.Model;

namespace Timer.ViewModel
{
    internal class TimerUsecase
    {
        
        /// <summary>
        /// Repositoryへデータを送るためのメソッド
        /// </summary>
        /// <param name="time"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool SendToRepository(object time, string text)
        {
            var result = true;

            var saveResult = DataBaseConnect.SaveToDb(time, text);

            if (saveResult)
            {
                return result;
            }
            return false;
        }
    }
}

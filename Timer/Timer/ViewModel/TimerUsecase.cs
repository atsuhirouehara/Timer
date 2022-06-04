using Timer.Model;

namespace Timer.ViewModel
{
    internal class TimerUsecase
    {
        
        DataBaseConnect DbRepository = new DataBaseConnect();

        /// <summary>
        /// Repositoryへデータを送るためのメソッド
        /// </summary>
        /// <param name="time"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool sendToRepository(object time, string text)
        {
            var result = true;

            var saveResult = DbRepository.saveToDb(time, text);

            if (saveResult)
            {
                return result;
            }
            return result = false;
        }
    }
}

using System;
namespace LanguageForum.Global
{
    public class SingleData
    {

        #region Instance
        public double currentLat, currentLng;
        public String locationDesc;
        public string lessionCheck;
        public string currentLesson;
        public int startProgress;
        #endregion

        public static SingleData instance = null;
        public static SingleData getInstance()
        {
            if (instance == null)
            {
                instance = new SingleData();

                //instance.currentLat = 0;
                //instance.currentLng = 0;
                //instance.locationDesc = "";
                //instance.currentLesson = "";
                //instance.lessionCheck = "";
                //instance.startProgress = 0;
            }

            return instance;
        }
    }
}

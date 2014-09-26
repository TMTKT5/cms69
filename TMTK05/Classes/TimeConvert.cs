#region

using System;

#endregion

namespace TMTK05.Classes
{
    public static class TimeConvert
    {
        #region Public Methods

        // <summary>
        // Convert dateTime to readable version 
        // </summary>
        public static string GetTimeAgo(DateTime strDate)
        {
            if (!IsDate(Convert.ToString(strDate)))
                return "";

            var t = DateTime.UtcNow - Convert.ToDateTime(strDate);

            var deltaSeconds = t.TotalSeconds;

            var deltaMinutes = deltaSeconds / 60.0f;
            int minutes;

            if (deltaSeconds < 5)
            {
                return "Zojuist";
            }

            if (deltaSeconds < 60)
            {
                return Math.Floor(deltaSeconds) + " seconden geleden";
            }

            if (deltaSeconds < 120)
            {
                return "Een minuut geleden";
            }

            if (deltaMinutes < 60)
            {
                return Math.Floor(deltaMinutes) + " minuten geleden";
            }

            if (deltaMinutes < 120)
            {
                return "Een uur geleden";
            }

            if (deltaMinutes < (24 * 60))
            {
                minutes = (int)Math.Floor(deltaMinutes / 60);
                return minutes + " uur geleden";
            }

            if (deltaMinutes < (24 * 60 * 2))
            {
                return "Gisteren";
            }

            if (deltaMinutes < (24 * 60 * 7))
            {
                minutes = (int)Math.Floor(deltaMinutes / (60 * 24));
                return minutes + " dagen geleden";
            }

            if (deltaMinutes < (24 * 60 * 14))
            {
                return "Afgelopen week";
            }

            if (deltaMinutes < (24 * 60 * 31))
            {
                minutes = (int)Math.Floor(deltaMinutes / (60 * 24 * 7));
                return minutes + " weken geleden";
            }

            if (deltaMinutes < (24 * 60 * 61))
            {
                return "Afgelopen maand";
            }

            if (deltaMinutes < (24 * 60 * 365.25))
            {
                minutes = (int)Math.Floor(deltaMinutes / (60 * 24 * 30));
                return minutes + " maanden geleden";
            }

            if (deltaMinutes < (24 * 60 * 731))
            {
                return "Afgelopen jaar";
            }

            minutes = (int)Math.Floor(deltaMinutes / (60 * 24 * 365));

            return minutes + " jaren geleden";
        }

        #endregion Public Methods

        #region Private Methods

        private static bool IsDate(string o)
        {
            DateTime tmp;

            return DateTime.TryParse(o, out tmp);
        }

        #endregion Private Methods
    }
}
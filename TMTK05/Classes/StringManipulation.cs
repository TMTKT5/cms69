#region

using System;

#endregion

namespace TMTK05.Classes
{
    public static class StringManipulation
    {
        /// <summary>
        /// Convert DateTime to MySql date 
        /// </summary>
        public static string DateTimeToMySql(DateTime value)
        {
            var year = String.Format("{0:yyyy}", value);
            var month = String.Format("{0:MM}", value);
            var day = String.Format("{0:dd}", value);

            return year + "-" + month + "-" + day;
        }

        /// <summary>
        /// Lowercase string. 
        /// </summary>
        public static string ToLowerFast(string value)
        {
            var output = value.ToCharArray();
            for (var i = 0; i < output.Length; i++)
            {
                if (output[i] >= 'A' &&
                    output[i] <= 'Z')
                {
                    output[i] = (char) (output[i] + 32);
                }
            }
            return new string(output);
        }

        /// <summary>
        /// Uppercase string. 
        /// </summary>
        public static string ToUpperFast(string value)
        {
            var output = value.ToCharArray();
            for (var i = 0; i < output.Length; i++)
            {
                if (output[i] >= 'a' &&
                    output[i] <= 'z')
                {
                    output[i] = (char) (output[i] - 32);
                }
            }
            return new string(output);
        }
    }
}
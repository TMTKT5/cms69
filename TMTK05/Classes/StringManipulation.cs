#region

using System;
using System.Linq;

#endregion

namespace TMTK05.Classes
{
    public static class StringManipulation
    {
        /// <summary>
        ///     Convert DateTime to MySql date
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DateTimeToMySql(DateTime value)
        {
            var year = String.Format("{0:yyyy}", value);
            var month = String.Format("{0:MM}", value);
            var day = String.Format("{0:dd}", value);

            return year + "-" + month + "-" + day;
        }

        /// <summary>
        ///     Lowercase string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
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
        ///     Uppercase string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
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

        /// <summary>
        ///     Captalize first letter of a string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string CaptalizeFirstLetter(this string data)
        {
            var chars = data.ToCharArray();

            // Find the Index of the first letter
            var charac = data.First(char.IsLetter);
            var i = data.IndexOf(charac);

            // capitalize that letter
            chars[i] = Char.ToUpperFast(chars[i]);

            return new string(chars);
        }
    }
}

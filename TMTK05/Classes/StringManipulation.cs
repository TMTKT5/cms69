/*
 * Copyright (C) 2014 Owain van Brakel.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#region

using System;
using System.Linq;

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

        /// <summary>
        /// Captalize first letter of a string
        /// </summary>
        public static string CaptalizeFirstLetter(this string data)
        {
            var chars = data.ToCharArray();

            // Find the Index of the first letter
            var charac = data.First(char.IsLetter);
            var i = data.IndexOf(charac);

            // capitalize that letter
            chars[i] = char.ToUpper(chars[i]);

            return new string(chars);
        }
    }
}
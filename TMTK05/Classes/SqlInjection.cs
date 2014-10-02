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
using System.Web;

#endregion

namespace TMTK05.Classes
{
    public static class SqlInjection
    {
        #region Public Methods

        // <summary>
        // Replace certain chars so SQL injection can't be done on input fields
        // </summary>
        public static String SafeSqlLiteral(String inputSql)
        {
            var sql = inputSql;

            if (String.IsNullOrEmpty(sql))
                return "";

            sql = sql.Replace("+", ",");
            sql = sql.Replace("--", "++");
            sql = sql.Replace("&", "+-+-");
            sql = sql.Replace("%", "[%]");
            sql = sql.Replace("_", "[_]");
            sql = sql.Replace("[", "[[]");
            sql = sql.Replace("]", "[]]");
            sql = sql.Replace("'", "''");
            sql = sql.Replace("/*", "[/]*");
            sql = sql.Replace("*/", "[*]/");

            return sql;
        }

        // <summary>
        // Replace chars so the user sees the exact thing they did put in
        // </summary>
        public static String SafeSqlLiteralRevert(String inputSql)
        {
            var sql = inputSql;

            if (String.IsNullOrEmpty(sql))
                return "";

            sql = sql.Replace("[*]/", "*/");
            sql = sql.Replace("[/]*", "/*");
            sql = sql.Replace("''", "'");
            sql = sql.Replace("[]]", "]");
            sql = sql.Replace("[[]", "[");
            sql = sql.Replace("[_]", "_");
            sql = sql.Replace("[%]", "%");
            sql = sql.Replace("+-+-", "&");
            sql = sql.Replace("++", "--");
            sql = sql.Replace(",", "+");

            return HttpUtility.HtmlEncode(sql);
        }

        #endregion Public Methods
    }
}
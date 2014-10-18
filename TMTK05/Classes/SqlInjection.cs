#region

using System;
using System.Web;

#endregion

namespace TMTK05.Classes
{
    public static class SqlInjection
    {
        #region Public Methods

        /// <summary>
        /// Replace certain chars so SQL injection can't be done on input fields 
        /// </summary>
        /// <param name="inputSql">
        /// </param>
        /// <returns>
        /// </returns>
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

        /// <summary>
        /// Replace chars so the user sees the exact thing they did put in 
        /// </summary>
        /// <param name="inputSql">
        /// </param>
        /// <returns>
        /// </returns>
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
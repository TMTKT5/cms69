#region

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

#endregion

namespace TMTK05.Attributes
{
    public static class HtmlExtensions
    {
        private static MvcHtmlString File(this HtmlHelper html, string name)
        {
            var tb = new TagBuilder("input");
            tb.Attributes.Add("type", "file");
            tb.Attributes.Add("name", name);
            tb.GenerateId(name);
            return MvcHtmlString.Create(tb.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString FileFor<TModel, TProperty>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression)
        {
            var name = GetFullPropertyName(expression);
            return html.File(name);
        }

        #region Helpers

        private static string GetFullPropertyName<T, TProperty>(Expression<Func<T, TProperty>> exp)
        {
            MemberExpression memberExp;

            if (!TryFindMemberExpression(exp.Body, out memberExp))
                return string.Empty;

            var memberNames = new Stack<string>();

            do
            {
                memberNames.Push(memberExp.Member.Name);
            } while (TryFindMemberExpression(memberExp.Expression, out memberExp));

            return string.Join(".", memberNames.ToArray());
        }

        private static bool TryFindMemberExpression(Expression exp, out MemberExpression memberExp)
        {
            memberExp = exp as MemberExpression;

            if (memberExp != null)
                return true;

            if (!IsConversion(exp) || !(exp is UnaryExpression)) return false;
            memberExp = ((UnaryExpression)exp).Operand as MemberExpression;

            return memberExp != null;
        }

        private static bool IsConversion(Expression exp)
        {
            return (exp.NodeType == ExpressionType.Convert || exp.NodeType == ExpressionType.ConvertChecked);
        }

        #endregion
    }
}
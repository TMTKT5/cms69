#region

using System;

#endregion

namespace TMTK05.Classes
{
    public class OneTimePasswordException : Exception
    {
        #region Public Constructors

        public OneTimePasswordException(string message)
            : base(message)
        {
        }

        #endregion Public Constructors
    }
}
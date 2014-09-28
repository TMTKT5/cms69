#region

using System;
using System.Runtime.Serialization;

#endregion

namespace TMTK05.Classes
{
    public class OneTimePasswordException : Exception
    {
        #region Public Constructors

        public OneTimePasswordException()
        {
        }

        public OneTimePasswordException(string message)
            : base(message)
        {
        }

        public OneTimePasswordException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public OneTimePasswordException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion Public Constructors
    }
}
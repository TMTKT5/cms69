#region

using System;
using System.Runtime.Caching;

#endregion

namespace TMTK05.Classes
{
    public static class TimeBasedOneTimePassword
    {
        #region Public Fields

        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        #endregion Public Fields

        #region Private Fields

        private static readonly MemoryCache Cache;

        #endregion Private Fields

        #region Public Constructors

        static TimeBasedOneTimePassword()
        {
            Cache = new MemoryCache("TimeBasedOneTimePassword");
        }

        #endregion Public Constructors

        #region Public Methods

        private static string GetPassword(string secret)
        {
            return GetPassword(secret, GetCurrentCounter());
        }

        public static bool IsValid(string secret, string password, int checkAdjacentIntervals = 1)
        {
            // Keeping a cache of the secret/password combinations that have been requested allows
            // us to make this a real one time use system. Once a secret/password combination has
            // been tested, it cannot be tested again until after it is no longer valid. See
            // http: //tools.ietf.org/html/rfc6238#section-5.2 for more info.
            var cacheKey = String.Format("{0}_{1}", secret, password);

            if (Cache.Contains(cacheKey))
            {
                throw new OneTimePasswordException(
                    "You cannot use the same secret/iterationNumber combination more than once.");
            }

            Cache.Add(cacheKey, cacheKey, new CacheItemPolicy { SlidingExpiration = TimeSpan.FromMinutes(2) });

            if (password == GetPassword(secret))
                return true;

            for (var i = 1; i <= checkAdjacentIntervals; i++)
            {
                if (password == GetPassword(secret, GetCurrentCounter() + i))
                    return true;

                if (password == GetPassword(secret, GetCurrentCounter() - i))
                    return true;
            }

            return false;
        }

        #endregion Public Methods

        #region Private Methods

        private static long GetCurrentCounter()
        {
            return GetCurrentCounter(DateTime.UtcNow, UnixEpoch, 30);
        }

        private static long GetCurrentCounter(DateTime now, DateTime epoch, int timeStep)
        {
            return (long)(now - epoch).TotalSeconds / timeStep;
        }

        private static string GetPassword(string secret, long counter, int digits = 6)
        {
            return HashedOneTimePassword.GeneratePassword(secret, counter, digits);
        }

        #endregion Private Methods
    }
}
#region

using System;

#endregion

namespace Localex.Exceptions
{
    public class LocalexException : Exception
    {
        public LocalexException(string message) : base(message)
        {
        }
    }
}
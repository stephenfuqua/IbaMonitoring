using System;

namespace safnet.iba
{
    /// <summary>
    /// A custom exception for the IBA site.
    /// </summary>
    public class IbaArgumentException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IbaArgumentException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public IbaArgumentException(string message) : base(message) { }
    }
}

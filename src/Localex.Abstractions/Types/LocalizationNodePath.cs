#region

using System;
using System.Collections.Generic;

#endregion

namespace Localex.Abstractions.Types
{
    /// <summary>
    /// Localization path wrapper.
    /// </summary>
    public class LocalizationNodePath
    {
        /// <summary>
        /// Node path.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Path segments. <see cref="Path"/> value delimited by ':'.
        /// </summary>
        public IEnumerable<string> Segments => Path?.Split(':', StringSplitOptions.RemoveEmptyEntries);

        /// <summary>
        /// Creates new <see cref="LocalizationNodePath"/> instance from <see cref="string"/>. 
        /// </summary>
        /// <param name="path">Node path.</param>
        public LocalizationNodePath(string path)
        {
            Path = path;
        }

        /// <summary>
        /// Implicit operator for creating new <see cref="LocalizationNodePath"/> instance from <see cref="string"/>.
        /// </summary>
        /// <param name="source">Node path.</param>
        /// <returns>Instance of <see cref="LocalizationNodePath"/>.</returns>
        public static implicit operator LocalizationNodePath(string source)
        {
            return new LocalizationNodePath(source);
        }
    }
}
#region

using System.Collections.Generic;

#endregion

namespace Localex.Abstractions.Sources
{
    /// <summary>
    /// Represents an interface for implementing localization provider.
    /// </summary>
    public interface ILocalizationProvider
    {
        /// <summary>
        /// Localization source this provider built from.
        /// </summary>
        ILocalizationSource Source { get; }

        /// <summary>
        /// Loaded localization nodes.
        /// </summary>
        IEnumerable<ILocalizationNode> Nodes { get; }

        /// <summary>
        /// Loads localization node(s).
        /// </summary>
        void Load();
    }
}
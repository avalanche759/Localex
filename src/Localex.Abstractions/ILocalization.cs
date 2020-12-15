#region

using System.Collections.Generic;
using Localex.Abstractions.Sources;

#endregion

namespace Localex.Abstractions
{
    /// <summary>
    /// Represents an interface for implementing localization.
    /// </summary>
    public interface ILocalization : ILocalizationNode
    {
        /// <summary>
        /// Gets all localization sources, used by the current localization instance.
        /// </summary>
        /// <returns>Localization sources.</returns>
        IEnumerable<ILocalizationSource> GetSources();
    }
}
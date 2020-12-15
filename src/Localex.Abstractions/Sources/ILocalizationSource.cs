#region

using System.Globalization;
using Localex.Abstractions.Configuration;

#endregion

namespace Localex.Abstractions.Sources
{
    /// <summary>
    /// Represents an interface for implementing localization source.
    /// </summary>
    public interface ILocalizationSource
    {
        /// <summary>
        /// Source type.
        /// </summary>
        string Type { get; }

        /// <summary>
        /// Base localization node path.
        /// </summary>
        string BasePath { get; }

        /// <summary>
        /// Source language culture.
        /// </summary>
        CultureInfo LanguageCulture { get; }

        /// <summary>
        /// Localization engine configuration.
        /// </summary>
        ILocalizationEngineConfiguration EngineConfiguration { get; }

        /// <summary>
        /// Builds localization provider from the current source.
        /// </summary>
        /// <returns>Built localization provider.</returns>
        ILocalizationProvider Build();
    }
}
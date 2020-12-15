#region

using System.Globalization;
using Localex.Abstractions.Configuration;
using Localex.Abstractions.Sources;

#endregion

namespace Localex.Abstractions.Builders
{
    /// <summary>
    /// Represents an interface for implementing localization builder.
    /// </summary>
    public interface ILocalizationBuilder
    {
        /// <summary>
        /// Localization language culture.
        /// </summary>
        CultureInfo LanguageCulture { get; }

        /// <summary>
        /// Configuration, used when adding and configuring new localization nodes.
        /// </summary>
        ILocalizationEngineConfiguration EngineConfiguration { get; }

        /// <summary>
        /// Configuration, used for parsing localization node value templates.
        /// </summary>
        ILocalizationValueTemplateParserConfiguration TemplateParserConfiguration { get; }

        /// <summary>
        /// Adds and configures new localization source.
        /// </summary>
        /// <param name="localizationSource">Localization source.</param>
        /// <returns>Configured localization builder.</returns>
        ILocalizationBuilder WithSource(ILocalizationSource localizationSource);

        /// <summary>
        /// Builds localization.
        /// </summary>
        /// <returns>Built localization.</returns>
        ILocalization Build();
    }
}
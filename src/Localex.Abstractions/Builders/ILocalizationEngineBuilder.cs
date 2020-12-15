#region

using System;
using System.Globalization;
using Localex.Abstractions.Configuration;

#endregion

namespace Localex.Abstractions.Builders
{
    /// <summary>
    /// Represents an interface for implementing localization engine builder.
    /// </summary>
    public interface ILocalizationEngineBuilder
    {
        /// <summary>
        /// Configuration, used when adding and configuring new localization nodes.
        /// </summary>
        ILocalizationEngineConfiguration EngineConfiguration { get; }

        /// <summary>
        /// Configuration, used for parsing localization node value templates.
        /// </summary>
        ILocalizationValueTemplateParserConfiguration TemplateParserConfiguration { get; }

        /// <summary>
        /// Adds and configures new localization.
        /// </summary>
        /// <param name="languageCulture">Localization language culture.</param>
        /// <param name="configureLocalization">Action, used to configure localization</param>
        /// <returns>Configured localization engine builder.</returns>
        ILocalizationEngineBuilder WithLocalization(CultureInfo languageCulture,
            Action<ILocalizationBuilder> configureLocalization);

        /// <summary>
        /// Builds localization engine.
        /// </summary>
        /// <returns>Built localization engine.</returns>
        ILocalizationEngine Build();
    }
}
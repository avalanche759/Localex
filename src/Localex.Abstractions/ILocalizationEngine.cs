#region

using System.Collections.Generic;
using System.Globalization;
using Localex.Abstractions.Configuration;
using Localex.Abstractions.Templates;

#endregion

namespace Localex.Abstractions
{
    /// <summary>
    /// Represents an interface for implementing localization engine.
    /// </summary>
    public interface ILocalizationEngine
    {
        /// <summary>
        /// Configuration for <see cref="ILocalizationNodeParser"/>.
        /// </summary>
        ILocalizationEngineConfiguration Configuration { get; }

        /// <summary>
        /// Configuration for <see cref="ILocalizationValueTemplateParser"/>.
        /// </summary>
        ILocalizationValueTemplateParserConfiguration TemplateParserConfiguration { get; }

        /// <summary>
        /// Gets all localizations, related to current localization engine.
        /// </summary>
        /// <returns>All localizations.</returns>
        IEnumerable<ILocalization> GetLocalizations();

        /// <summary>
        /// Gets localization by the specified <see cref="CultureInfo"/> instance.
        /// </summary>
        /// <param name="languageCulture"><see cref="CultureInfo"/> instance.</param>
        /// <returns>Specified localization.</returns>
        ILocalization GetLocalization(CultureInfo languageCulture = null);
    }
}
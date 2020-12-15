#region

using Localex.Abstractions.Configuration;

#endregion

namespace Localex.Abstractions.Templates
{
    /// <summary>
    /// Represents an interface for implementing localization value template parser.
    /// </summary>
    public interface ILocalizationValueTemplateParser
    {
        /// <summary>
        /// Configuration used for parsing templates.
        /// </summary>
        ILocalizationValueTemplateParserConfiguration Configuration { get; }

        /// <summary>
        /// Parses localization value template.
        /// </summary>
        /// <param name="source">String source.</param>
        /// <returns>Parsed localization value template.</returns>
        ILocalizationValueTemplate Parse(string source);
    }
}
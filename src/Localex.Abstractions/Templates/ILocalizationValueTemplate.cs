#region

using System.Collections.Generic;
using Localex.Abstractions.Templates.Parameters;

#endregion

namespace Localex.Abstractions.Templates
{
    /// <summary>
    /// Represents an interface for implementing localization value template.
    /// </summary>
    public interface ILocalizationValueTemplate
    {
        /// <summary>
        /// Represents a string value of the current localization value template.
        /// </summary>
        string Value { get; }

        /// <summary>
        /// Template formatting parameters.
        /// </summary>
        IEnumerable<ILocalizationValueTemplateParameter> Parameters { get; }

        /// <summary>
        /// Formats string.
        /// </summary>
        /// <param name="localizationStringTemplateParameterValues">Formatting parameters.</param>
        /// <returns>Formatted template string.</returns>
        string Format(params ILocalizationValueTemplateParameterValue[] localizationStringTemplateParameterValues);
    }
}
#region

using System.Collections.Generic;
using Localex.Abstractions.Templates.Parameters;

#endregion

namespace Localex.Abstractions.Templates
{
    /// <summary>
    /// Represents an interface for implementinh localizqtion value template formatter.
    /// </summary>
    public interface ILocalizationValueTemplateFormatter
    {
        /// <summary>
        /// Formats localization value template with specified parameters.
        /// </summary>
        /// <param name="parameters">Formatting parameters.</param>
        /// <param name="localizationStringTemplateParameterValues">Formatting parameters values.</param>
        /// <returns>Value indicating formatting ability.</returns>
        bool CanFormat(IEnumerable<ILocalizationValueTemplateParameter> parameters,
            IEnumerable<ILocalizationValueTemplateParameterValue> localizationStringTemplateParameterValues);

        /// <summary>
        /// Formats localization value template with specified parameters.
        /// </summary>
        /// <param name="parameters">Formatting parameters.</param>
        /// <param name="localizationStringTemplateParameterValues">Formatting parameters values.</param>
        /// <returns>Formatted string.</returns>
        string Format(IEnumerable<ILocalizationValueTemplateParameter> parameters,
            IEnumerable<ILocalizationValueTemplateParameterValue> localizationStringTemplateParameterValues);
    }
}
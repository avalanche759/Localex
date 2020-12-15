#region

using System.Globalization;

#endregion

namespace Localex.Abstractions.Configuration
{
    /// <summary>
    /// Represents an interface for implementing localization engine configuration.
    /// </summary>
    public interface ILocalizationEngineConfiguration
    {
        /// <summary>
        /// Default language culture. E.g. <see cref="CultureInfo.CurrentCulture"/>.
        /// </summary>
        CultureInfo DefaultLanguageCulture { get; }

        /// <summary>
        /// Identifier for property, designating localization node value. E.g. Value.
        /// </summary>
        string ValuePropertyName { get; }
    }
}
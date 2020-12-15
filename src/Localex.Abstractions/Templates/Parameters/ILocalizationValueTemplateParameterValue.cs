namespace Localex.Abstractions.Templates.Parameters
{
    /// <summary>
    /// Represents an interface for implementing localization value template parameter value.
    /// </summary>
    public interface ILocalizationValueTemplateParameterValue
    {
        /// <summary>
        /// Parameter name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Formats localization value template parameter.
        /// </summary>
        /// <param name="parameter">Parameter information.</param>
        /// <returns></returns>
        string FormatParameter(ILocalizationValueTemplateParameter parameter);
    }
}
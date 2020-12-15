namespace Localex.Abstractions.Configuration
{
    /// <summary>
    /// Represents an interface for implementing localization value template parser configuration.
    /// </summary>
    public interface ILocalizationValueTemplateParserConfiguration
    {
        /// <summary>
        /// Parameter start string. E.g "{", "{{". 
        /// </summary>
        string ParameterStartString { get; }

        /// <summary>
        /// Parameter end string. E.g "}", "}}".
        /// </summary>
        string ParameterEndString { get; }

        /// <summary>
        /// Options start string. E.g. "?", ":".
        /// </summary>
        string OptionsStartString { get; }

        /// <summary>
        /// Options delimiter string. E.g. "&", ";".
        /// </summary>
        string OptionsDelimiter { get; }
    }
}
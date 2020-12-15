namespace Localex.Abstractions.Templates.Parameters
{
    /// <summary>
    /// Represents an interface for implementing localization value template parameter option.
    /// </summary>
    public interface ILocalizationValueTemplateParameterOption
    {
        /// <summary>
        /// Parameter option key
        /// </summary>
        string Key { get; }

        /// <summary>
        /// Parameter option value.
        /// </summary>
        string Value { get; }
    }
}
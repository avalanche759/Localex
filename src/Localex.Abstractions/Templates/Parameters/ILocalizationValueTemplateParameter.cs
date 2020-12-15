#region

using System.Collections.Generic;

#endregion

namespace Localex.Abstractions.Templates.Parameters
{
    /// <summary>
    /// Represents an interface for implementing localization value parameter
    /// </summary>
    public interface ILocalizationValueTemplateParameter
    {
        /// <summary>
        /// Parameter name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Parameter format value.
        /// </summary>
        string Format { get; }

        /// <summary>
        /// Parameter options.
        /// </summary>
        IEnumerable<ILocalizationValueTemplateParameterOption> Options { get; }
    }
}
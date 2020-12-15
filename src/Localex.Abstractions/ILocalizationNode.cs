#region

using System.Collections.Generic;
using System.Globalization;
using Localex.Abstractions.Templates;
using Localex.Abstractions.Templates.Parameters;
using Localex.Abstractions.Types;

#endregion

namespace Localex.Abstractions
{
    /// <summary>
    /// Represents an interface for implementing localization node.
    /// </summary>
    public interface ILocalizationNode
    {
        /// <summary>
        /// Represents information about current localization node language.
        /// </summary>
        CultureInfo LanguageCulture { get; }

        /// <summary>
        /// Current localization node identifier.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Localization value template.
        /// </summary>
        ILocalizationValueTemplate Template { get; }

        /// <summary>
        /// Gets all child nodes, contained in the current localization node.
        /// </summary>
        /// <returns>Child nodes.</returns>
        IEnumerable<ILocalizationNode> GetChildNodes();

        /// <summary>
        /// Gets child node by the specified node path.
        /// </summary>
        /// <param name="nodePath">Node path.</param>
        /// <returns>Returns <see langword="null"/>, if there's no localization node by specified path</returns>
        ILocalizationNode GetNode(LocalizationNodePath nodePath);

        /// <summary>
        /// Gets child node by the specified node path.
        /// </summary>
        /// <param name="nodePath">Node path.</param>
        ILocalizationNode this[LocalizationNodePath nodePath] { get; }

        /// <summary>
        /// Formats <see cref="Template"/> with the specified parameters.
        /// </summary>
        /// <param name="localizationStringTemplateParameterValues">Formatting parameters.</param>
        /// <returns>Formatted string.</returns>
        string Format(params ILocalizationValueTemplateParameterValue[] localizationStringTemplateParameterValues);
    }
}
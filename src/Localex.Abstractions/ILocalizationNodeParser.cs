#region

using System.Collections.Generic;

#endregion

namespace Localex.Abstractions
{
    /// <summary>
    /// Represents an interface for implementing localization node parser.
    /// </summary>
    public interface ILocalizationNodeParser
    {
        /// <summary>
        /// Type of parser. E.g Json, Xml, Yaml.
        /// </summary>
        string Type { get; }

        /// <summary>
        /// Shows, that <paramref name="source"/> can be parsed. 
        /// </summary>
        /// <param name="source">Source data for parsing.</param>
        /// <returns>Ability for parsing from specified source text.</returns>
        bool CanParse(string source);

        /// <summary>
        /// Parses localization node.
        /// </summary>
        /// <param name="source">Parsing source. E.g JSON contents.</param>
        /// <returns>Successfully parsed localization nodes.</returns>
        IEnumerable<ILocalizationNode> Parse(string source);
    }
}
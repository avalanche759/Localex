#region

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Localex.Abstractions;
using Localex.Abstractions.Templates;
using Localex.Abstractions.Templates.Parameters;
using Localex.Abstractions.Types;

#endregion

namespace Localex
{
    public class LocalizationNode : ILocalizationNode
    {
        public CultureInfo LanguageCulture { get; }

        public string Id { get; }

        public ILocalizationValueTemplate Template { get; }

        private readonly IEnumerable<ILocalizationNode> _childNodes;

        public LocalizationNode(CultureInfo languageCulture, string id, ILocalizationValueTemplate template,
            IEnumerable<ILocalizationNode> childNodes)
        {
            LanguageCulture = languageCulture;
            Id = id;
            Template = template;

            _childNodes = childNodes;
        }

        public IEnumerable<ILocalizationNode> GetChildNodes()
        {
            return _childNodes;
        }

        public ILocalizationNode GetNode(LocalizationNodePath nodePath)
        {
            ILocalizationNode node = this;

            if (nodePath.Segments.Count() > 1)
            {
                foreach (string pathSegment in nodePath.Segments)
                {
                    node = node?.GetNode(pathSegment);
                }
            }

            if (node == this)
            {
                string firstSegment = nodePath.Segments.First();
                return _childNodes
                    .FirstOrDefault(x => x.Id.Equals(firstSegment) || x.Id.SequenceEqual(firstSegment));
            }

            return node;
        }

        public ILocalizationNode this[LocalizationNodePath nodePath] => GetNode(nodePath);

        public string Format(
            params ILocalizationValueTemplateParameterValue[] localizationStringTemplateParameterValues)
        {
            return Template.Format(localizationStringTemplateParameterValues);
        }
    }
}
#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Localex.Abstractions;
using Localex.Abstractions.Builders;
using Localex.Abstractions.Configuration;
using Localex.Abstractions.Sources;
using Localex.Exceptions;

#endregion

namespace Localex.Builders
{
    public class LocalizationBuilder : ILocalizationBuilder
    {
        public CultureInfo LanguageCulture { get; }

        public ILocalizationEngineConfiguration EngineConfiguration { get; }

        public ILocalizationValueTemplateParserConfiguration TemplateParserConfiguration { get; }

        private readonly ICollection<ILocalizationSource> _localizationSources;

        public LocalizationBuilder(CultureInfo languageCulture,
            ILocalizationEngineConfiguration localizationEngineConfiguration,
            ILocalizationValueTemplateParserConfiguration templateParserConfiguration)
        {
            LanguageCulture = languageCulture;
            EngineConfiguration = localizationEngineConfiguration;
            TemplateParserConfiguration = templateParserConfiguration;
            _localizationSources = new Collection<ILocalizationSource>();
        }

        public ILocalizationBuilder WithSource(ILocalizationSource localizationSource)
        {
            _localizationSources.Add(localizationSource);

            return this;
        }

        public ILocalization Build()
        {
            IDictionary<string, List<ILocalizationNode>> localizationNodes =
                new Dictionary<string, List<ILocalizationNode>>();

            foreach (ILocalizationSource localizationSource in _localizationSources)
            {
                var localizationProvider = localizationSource.Build();

                string nodePath = localizationSource.BasePath ?? "";

                localizationProvider.Load();

                IEnumerable<ILocalizationNode> nodes = localizationProvider.Nodes;

                foreach (ILocalizationNode localizationNode in nodes)
                {
                    if (localizationNodes.ContainsKey(nodePath))
                    {
                        localizationNodes[nodePath].Add(localizationNode);
                    }
                    else localizationNodes.Add(nodePath, new List<ILocalizationNode>() {localizationNode});
                }
            }

            ICollection<ILocalizationNode> nodesToAdd = new Collection<ILocalizationNode>();

            foreach (string baseNodesPath in localizationNodes.Keys)
            {
                string path = baseNodesPath ?? string.Empty;

                string[] baseNodesPathSegments = path.Split(':',
                    StringSplitOptions.RemoveEmptyEntries);

                IList<ILocalizationNode> inlineNodes = localizationNodes[baseNodesPath];

                foreach (string segment in baseNodesPathSegments.Reverse())
                {
                    ILocalizationNode newChildNode = new LocalizationNode(LanguageCulture, segment, null,
                        new Collection<ILocalizationNode>(inlineNodes));
                    inlineNodes = new List<ILocalizationNode>();
                    inlineNodes.Add(newChildNode);
                }

                foreach (var nodeToAdd in inlineNodes)
                {
                    var existing = nodesToAdd.FirstOrDefault(node => node.Id.Equals(nodeToAdd.Id));

                    if (existing != null)
                    {
                        ILocalizationNode combinedNode = new LocalizationNode(
                            LanguageCulture,
                            existing.Id, existing.Template,
                            nodeToAdd.GetChildNodes().Concat(existing.GetChildNodes()));

                        nodesToAdd.Remove(existing);
                        
                        nodesToAdd.Add(combinedNode);
                    }
                    else nodesToAdd.Add(nodeToAdd);
                }
            }

            return new Localization(LanguageCulture, _localizationSources, nodesToAdd);
        }
    }
}
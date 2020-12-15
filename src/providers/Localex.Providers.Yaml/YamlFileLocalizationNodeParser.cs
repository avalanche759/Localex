#region

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using Localex.Abstractions;
using Localex.Abstractions.Configuration;
using Localex.Abstractions.Templates;
using Localex.Templates;
using YamlDotNet.Serialization;

#endregion

namespace Localex.Providers.Yaml
{
    public class YamlFileLocalizationNodeParser : ILocalizationNodeParser
    {
        public string Type => "Yaml";

        private readonly ILocalizationEngineConfiguration _localizationEngineConfiguration;
        private readonly ILocalizationValueTemplateParser _localizationValueTemplateParser;

        private readonly CultureInfo _languageCulture;

        private readonly IDeserializer _deserializer;

        public YamlFileLocalizationNodeParser(CultureInfo languageCulture,
            ILocalizationEngineConfiguration localizationEngineConfiguration,
            ILocalizationValueTemplateParserConfiguration localizationValueTemplateParserConfiguration)
        {
            _languageCulture = languageCulture;

            _localizationEngineConfiguration = localizationEngineConfiguration;

            _localizationValueTemplateParser =
                new LocalizationValueTemplateParser(localizationValueTemplateParserConfiguration);

            _deserializer = new DeserializerBuilder().Build();
        }


        public bool CanParse(string source)
        {
            try
            {
                _deserializer.Deserialize<ExpandoObject>(source);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<ILocalizationNode> Parse(string source)
        {
            List<KeyValuePair<string, object>> nodes = _deserializer.Deserialize<ExpandoObject>(source).ToList();

            foreach (KeyValuePair<string, object> node in nodes)
            {
                yield return ParseNode(node);
            }
        }

        private ILocalizationNode ParseNode(KeyValuePair<string, object> nodeSource)
        {
            string nodeName = nodeSource.Key;

            string nodeValue = "";

            ICollection<ILocalizationNode> inlineNodes = new Collection<ILocalizationNode>();

            if (nodeSource.Value is Dictionary<object, object> inlineObject)
            {
                foreach (KeyValuePair<object, object> inlineNode in inlineObject)
                {
                    if (inlineNode.Key is string key)
                    {
                        KeyValuePair<string, object> subNode = new KeyValuePair<string, object>(key, inlineNode.Value);

                        if (subNode.Key == _localizationEngineConfiguration.ValuePropertyName
                            && subNode.Value is string stringValue)
                        {
                            nodeValue = stringValue;
                        }
                        else
                        {
                            inlineNodes.Add(ParseNode(subNode));
                        }
                    }
                }
            }
            else if (nodeSource.Value is string value)
            {
                nodeValue = value;
            }

            return new LocalizationNode(
                _languageCulture,
                nodeName,
                _localizationValueTemplateParser.Parse(nodeValue),
                inlineNodes
            );
        }
    }
}
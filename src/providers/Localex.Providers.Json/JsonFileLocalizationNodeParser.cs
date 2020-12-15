#region

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Localex.Abstractions;
using Localex.Abstractions.Configuration;
using Localex.Abstractions.Templates;
using Localex.Templates;
using Newtonsoft.Json.Linq;

#endregion

namespace Localex.Providers.Json
{
    public class JsonFileLocalizationNodeParser : ILocalizationNodeParser
    {
        public string Type => "Json";

        private readonly ILocalizationEngineConfiguration _localizationEngineConfiguration;
        private readonly ILocalizationValueTemplateParser _localizationValueTemplateParser;

        private readonly CultureInfo _languageCulture;

        public JsonFileLocalizationNodeParser(CultureInfo languageCulture,
            ILocalizationEngineConfiguration localizationEngineConfiguration,
            ILocalizationValueTemplateParserConfiguration localizationValueTemplateParserConfiguration)
        {
            _languageCulture = languageCulture;

            _localizationEngineConfiguration = localizationEngineConfiguration;

            _localizationValueTemplateParser =
                new LocalizationValueTemplateParser(localizationValueTemplateParserConfiguration);
        }

        public bool CanParse(string source)
        {
            try
            {
                JObject.Parse(source);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<ILocalizationNode> Parse(string source)
        {
            JObject jsonObject = JObject.Parse(source);

            foreach (JProperty property in jsonObject.Properties())
            {
                yield return ParseNode(property);
            }
        }

        private ILocalizationNode ParseNode(JProperty nodeProperty)
        {
            string nodeName = nodeProperty.Name;

            string nodeValue = "";

            ICollection<ILocalizationNode> inlineNodes = new Collection<ILocalizationNode>();

            if (nodeProperty.Value is JObject inlineObject)
            {
                JProperty valueProperty = inlineObject.Property(_localizationEngineConfiguration.ValuePropertyName);
                if (valueProperty != null)
                {
                    nodeValue = valueProperty.Value.ToString();
                }

                foreach (JProperty property in nodeProperty.Value.OfType<JProperty>()
                    .Where(x => !x.Name.Equals(_localizationEngineConfiguration.ValuePropertyName)))
                {
                    inlineNodes.Add(ParseNode(property));
                }
            }

            else if (nodeProperty.Value.Type == JTokenType.String)
            {
                nodeValue = nodeProperty.Value.ToString();
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
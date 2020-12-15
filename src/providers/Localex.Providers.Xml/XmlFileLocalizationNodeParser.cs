#region

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Localex.Abstractions;
using Localex.Abstractions.Configuration;
using Localex.Abstractions.Templates;
using Localex.Templates;

#endregion

namespace Localex.Providers.Xml
{
    public class XmlFileLocalizationNodeParser : ILocalizationNodeParser
    {
        public string Type => "Xml";

        private readonly ILocalizationEngineConfiguration _localizationEngineConfiguration;
        private readonly ILocalizationValueTemplateParser _localizationValueTemplateParser;

        private readonly CultureInfo _languageCulture;

        public XmlFileLocalizationNodeParser(CultureInfo languageCulture,
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
                XDocument.Parse(source);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<ILocalizationNode> Parse(string source)
        {
            XDocument document = XDocument.Parse(source);

            XElement root = document.Nodes().First() as XElement;

            foreach (XElement nodeSource in root.Nodes())
            {
                yield return ParseNode(nodeSource);
            }
        }

        public ILocalizationNode ParseNode(XElement nodeSource)
        {
            string nodeName = nodeSource.Name.LocalName;

            string nodeValue = "";

            ICollection<ILocalizationNode> inlineNodes = new Collection<ILocalizationNode>();

            if (nodeSource.FirstNode is XElement)
            {
                foreach (XElement inlineNode in nodeSource.Nodes().OfType<XElement>())
                {
                    if (inlineNode.Name.LocalName == _localizationEngineConfiguration.ValuePropertyName
                        && inlineNode.FirstNode is XText stringValue)
                    {
                        nodeValue = stringValue.Value;
                    }
                    else
                    {
                        inlineNodes.Add(ParseNode(inlineNode));
                    }
                }
            }
            else if (nodeSource.FirstNode is XText value)
            {
                nodeValue = value.Value;
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
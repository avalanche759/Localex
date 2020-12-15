#region

using Localex.Abstractions.Builders;
using Localex.Providers.File;

#endregion

namespace Localex.Providers.Xml
{
    public static class LocalizationBuilderXmlExtensions
    {
        public static ILocalizationBuilder WithXmlFile(this ILocalizationBuilder localizationBuilder,
            string filePath,
            bool isOptional = false,
            string basePath = null
        )
        {
            return localizationBuilder.WithSource(new FileLocalizationSource(
                "Xml",
                basePath,
                filePath,
                isOptional,
                localizationBuilder.LanguageCulture,
                new XmlFileLocalizationNodeParser(
                    localizationBuilder.LanguageCulture,
                    localizationBuilder.EngineConfiguration,
                    localizationBuilder.TemplateParserConfiguration),
                localizationBuilder.EngineConfiguration,
                localizationBuilder.TemplateParserConfiguration
            ));
        }
    }
}
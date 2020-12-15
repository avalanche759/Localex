#region

using Localex.Abstractions.Builders;
using Localex.Providers.File;

#endregion

namespace Localex.Providers.Yaml
{
    public static class LocalizationBuilderYamlExtensions
    {
        public static ILocalizationBuilder WithYamlFile(this ILocalizationBuilder localizationBuilder,
            string filePath,
            bool isOptional = false,
            string basePath = null
        )
        {
            return localizationBuilder.WithSource(new FileLocalizationSource(
                "Yaml",
                basePath,
                filePath,
                isOptional,
                localizationBuilder.LanguageCulture,
                new YamlFileLocalizationNodeParser(
                    localizationBuilder.LanguageCulture,
                    localizationBuilder.EngineConfiguration,
                    localizationBuilder.TemplateParserConfiguration),
                localizationBuilder.EngineConfiguration,
                localizationBuilder.TemplateParserConfiguration
            ));
        }
    }
}
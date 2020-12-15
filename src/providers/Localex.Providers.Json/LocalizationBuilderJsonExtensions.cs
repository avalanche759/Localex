#region

using Localex.Abstractions.Builders;
using Localex.Providers.File;

#endregion

namespace Localex.Providers.Json
{
    public static class LocalizationBuilderJsonExtensions
    {
        public static ILocalizationBuilder WithJsonFile(this ILocalizationBuilder localizationBuilder,
            string filePath,
            bool isOptional = false,
            string basePath = null
        )
        {
            return localizationBuilder.WithSource(new FileLocalizationSource(
                "Json",
                basePath,
                filePath,
                isOptional,
                localizationBuilder.LanguageCulture,
                new JsonFileLocalizationNodeParser(
                    localizationBuilder.LanguageCulture,
                    localizationBuilder.EngineConfiguration,
                    localizationBuilder.TemplateParserConfiguration),
                localizationBuilder.EngineConfiguration,
                localizationBuilder.TemplateParserConfiguration
            ));
        }
    }
}
#region

using System.Globalization;
using Localex.Abstractions;
using Localex.Abstractions.Configuration;
using Localex.Abstractions.Sources;

#endregion

namespace Localex.Providers.File
{
    public class FileLocalizationSource : ILocalizationSource
    {
        public string Type { get; }

        public string BasePath { get; }

        public CultureInfo LanguageCulture { get; }

        public ILocalizationEngineConfiguration EngineConfiguration { get; }

        public ILocalizationProvider Build()
        {
            return new FileLocalizationProvider(this);
        }

        public ILocalizationValueTemplateParserConfiguration TemplateParserConfiguration { get; }

        public string FilePath { get; }

        public bool IsOptional { get; }

        public ILocalizationNodeParser LocalizationNodeParser { get; }

        public FileLocalizationSource(
            string type, string basePath,
            string filePath,
            bool isOptional,
            CultureInfo languageCulture,
            ILocalizationNodeParser localizationNodeParser,
            ILocalizationEngineConfiguration engineConfiguration,
            ILocalizationValueTemplateParserConfiguration templateParserConfiguration)
        {
            Type = type;
            BasePath = basePath;
            FilePath = filePath;
            IsOptional = isOptional;
            LanguageCulture = languageCulture;
            LocalizationNodeParser = localizationNodeParser;
            EngineConfiguration = engineConfiguration;
            TemplateParserConfiguration = templateParserConfiguration;
        }
    }
}
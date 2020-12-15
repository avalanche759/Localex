#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Localex.Abstractions;
using Localex.Abstractions.Configuration;
using Localex.Exceptions;

#endregion

namespace Localex
{
    public class LocalizationEngine : ILocalizationEngine
    {
        public ILocalizationEngineConfiguration Configuration { get; }

        public ILocalizationValueTemplateParserConfiguration TemplateParserConfiguration { get; }

        private readonly IEnumerable<ILocalization> _localizations;

        public LocalizationEngine(IEnumerable<ILocalization> localizations,
            ILocalizationEngineConfiguration localizationEngineConfiguration,
            ILocalizationValueTemplateParserConfiguration templateParserConfiguration)
        {
            _localizations = localizations;
            Configuration = localizationEngineConfiguration;
            TemplateParserConfiguration = templateParserConfiguration;
        }

        public IEnumerable<ILocalization> GetLocalizations()
        {
            return _localizations;
        }

        public ILocalization GetLocalization(CultureInfo languageCulture = null)
        {
            CultureInfo localizationCultureInfo = languageCulture
                                                  ?? Configuration.DefaultLanguageCulture
                                                  ?? throw new ArgumentNullException(
                                                      nameof(languageCulture),
                                                      "Please specify language culture or set DefaultLanguageCulture in localization engine configuration.");

            ILocalization foundLocalization = _localizations
                .FirstOrDefault(
                    localization => localizationCultureInfo
                        .Equals(localization.LanguageCulture));

            if (foundLocalization == null)
            {
                throw new LocalexException(
                    $"There's no registered localization related to {localizationCultureInfo} language culture.");
            }

            return foundLocalization;
        }
    }
}
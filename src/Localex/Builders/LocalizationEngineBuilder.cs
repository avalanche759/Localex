#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Localex.Abstractions;
using Localex.Abstractions.Builders;
using Localex.Abstractions.Configuration;
using Localex.Configuration;
using Localex.Exceptions;

#endregion

namespace Localex.Builders
{
    public class LocalizationEngineBuilder : ILocalizationEngineBuilder
    {
        private readonly ICollection<ILocalization> _localizations;

        public ILocalizationEngineConfiguration EngineConfiguration { get; set; }
        public ILocalizationValueTemplateParserConfiguration TemplateParserConfiguration { get; set; }

        public LocalizationEngineBuilder()
        {
            if (EngineConfiguration is null)
                EngineConfiguration = LocalizationEngineConfiguration.Default;

            if (TemplateParserConfiguration is null)
                TemplateParserConfiguration = LocalizationValueTemplateParserConfiguration.Default;

            _localizations = new Collection<ILocalization>();
        }

        public ILocalizationEngineBuilder WithLocalization(CultureInfo languageCulture,
            Action<ILocalizationBuilder> configureLocalization)
        {
            ILocalizationBuilder localizationBuilder =
                new LocalizationBuilder(languageCulture, EngineConfiguration, TemplateParserConfiguration);

            if (configureLocalization is null)
            {
                throw new LocalexEngineBuilderException(
                    "Localization configurator isn't valid (null-equal). Please specify a valid configurator function.");
            }

            configureLocalization.Invoke(localizationBuilder);

            _localizations.Add(localizationBuilder.Build());

            return this;
        }

        public ILocalizationEngine Build()
        {
            if (_localizations.Count == 0)
            {
                throw new LocalexEngineBuilderException(
                    "No registered localizations found. Please use ILocalizationEngineBuilder.WithLocalization method to configure at least one localization.");
            }

            return new LocalizationEngine(_localizations, EngineConfiguration, TemplateParserConfiguration);
        }
    }
}
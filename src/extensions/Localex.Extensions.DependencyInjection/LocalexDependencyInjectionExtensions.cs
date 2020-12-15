#region

using System;
using Localex.Abstractions;
using Localex.Abstractions.Builders;
using Localex.Builders;
using Localex.Exceptions;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Localex.Extensions.DependencyInjection
{
    public static class LocalexDependencyInjectionExtensions
    {
        public static IServiceCollection AddLocalization(this IServiceCollection serviceCollection,
            Action<ILocalizationEngineBuilder> configureLocalizationEngine)
        {
            ILocalizationEngineBuilder localizationEngineBuilder = new LocalizationEngineBuilder();

            if (configureLocalizationEngine is null)
            {
                throw new LocalexException(
                    "Localization engine configurator isn't valid (null-equal). Please specify a valid configurator function.");
            }

            configureLocalizationEngine(localizationEngineBuilder);

            ILocalizationEngine localizationEngine = localizationEngineBuilder.Build();

            return serviceCollection.AddSingleton<ILocalizationEngine>(localizationEngine);
        }
    }
}
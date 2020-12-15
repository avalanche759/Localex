#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Autofac;
using Localex.Abstractions;
using Localex.Abstractions.Builders;
using Localex.Builders;
using Localex.Configuration;
using Localex.Extensions.DependencyInjection;
using Localex.Extensions.DependencyInjection.Autofac;
using Localex.Templates.Parameters;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Localex.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            //
            //  This example shows how to create and configure a localization engine.
            //  Comment or uncomment lines you want or don't want
            //
            // This is standard way to do that:
            //
            ILocalizationEngine localizationEngine = Configure();


            //
            //  Also, you can do that via Autofac or via Microsoft Dependency Injection:
            //

            //ILocalizationEngine localizationEngine = RegisterViaAutofac(); //Registering via Autofac.
            //ILocalizationEngine localizationEngine = RegisterViaMicrosoftDependencyInjection(); //Registering via Microsoft Dependency Injection.

            IEnumerable<ILocalization> localizations = localizationEngine.GetLocalizations().ToList();

            #region Select display language

            for (int i = 0; i < localizations.Count(); i++)
            {
                ILocalization availableLocalization = localizations.ElementAt(i);

                CultureInfo availableLanguage = availableLocalization.LanguageCulture;

                Console.WriteLine(
                    $"{(i + 1).ToString()}. {availableLanguage.NativeName} ({availableLanguage.TwoLetterISOLanguageName}).");
            }

            string selectedLanguageId;
            int languageId;
            
            do
            {
                selectedLanguageId = RequestString();
            } while (!(int.TryParse(selectedLanguageId, out languageId) && languageId > 0 &&
                       languageId <= localizations.Count()));

            ILocalization localization = localizations.ElementAt(languageId - 1);

            #endregion

            #region Test code

            string startupMessageLocalizationPath = "StartupMessage";

            string questionsLocalizationPath = "Questions";
            string nameQuestionLocalizationPath = $"{questionsLocalizationPath}:Name";
            string ageQuestionLocalizationPath = $"{questionsLocalizationPath}:Age";

            string greetingLocalizationPath = "Greeting";

            string quitMessageLocalizationPath = "QuitMessage";

            Console.WriteLine(
                localization[startupMessageLocalizationPath].Template.Value);

            Console.WriteLine(
                localization[nameQuestionLocalizationPath].Template.Value);

            
            string name = RequestString();

            Console.WriteLine(
                localization[ageQuestionLocalizationPath]
                    .Format(new LocalizationValueTemplateParameterValue("Name", name)));

            int age = int.Parse(RequestString());

            Console.WriteLine(
                localization[greetingLocalizationPath].Format(
                    new LocalizationValueTemplateParameterValue("Name", (parameter) =>
                    {
                        string nameCase = parameter.Options.FirstOrDefault(_ => _.Key == "case")?.Value;

                        if (nameCase == "upper")
                        {
                            return name.ToUpper();
                        }

                        return name;
                    }),
                    new LocalizationValueTemplateParameterValue("Age", age.ToString())));

            Console.WriteLine(localization[quitMessageLocalizationPath].Template.Value);
            Console.ReadKey();

            #endregion
        }

        static ILocalizationEngine Configure()
        {
            ILocalizationEngineBuilder localizationEngineBuilder = new LocalizationEngineBuilder
            {
                EngineConfiguration =
                    LocalizationEngineConfiguration.Default,
                TemplateParserConfiguration =
                    LocalizationValueTemplateParserConfiguration.Default 
            };

            Defaults.ConfigureLocalizationEngine(localizationEngineBuilder);

            ILocalizationEngine localizationEngine = localizationEngineBuilder.Build();

            return localizationEngine;
        }

        static ILocalizationEngine RegisterViaMicrosoftDependencyInjection()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddLocalization(Defaults.ConfigureLocalizationEngine);

            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            ILocalizationEngine localizationEngine = serviceProvider.GetService<ILocalizationEngine>();

            return localizationEngine;
        }

        static ILocalizationEngine RegisterViaAutofac()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterLocalization(Defaults.ConfigureLocalizationEngine);

            IContainer container = containerBuilder.Build();

            ILocalizationEngine localizationEngine = container.Resolve<ILocalizationEngine>();

            return localizationEngine;
        }

        static string RequestString()
        {
            string result;
            do
            {
                Console.Write("> ");

                result = Console.ReadLine();
            } while (string.IsNullOrEmpty(result) || string.IsNullOrWhiteSpace(result));

            return result;
        }
    }
}
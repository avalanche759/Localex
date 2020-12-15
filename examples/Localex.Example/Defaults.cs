#region

using System.Globalization;
using Localex.Abstractions.Builders;
using Localex.Providers.Json;
using Localex.Providers.Xml;
using Localex.Providers.Yaml;

#endregion

namespace Localex.Example
{
    public static class Defaults
    {
        public static void ConfigureLocalizationEngine(ILocalizationEngineBuilder localizationEngineBuilder)
        {
            localizationEngineBuilder
                .WithLocalization(CultureInfo.GetCultureInfo("en-US"),
                    english => english
                        .WithJsonFile("english.json"))
                
                .WithLocalization(CultureInfo.GetCultureInfo("ru-RU"),
                    russian => russian
                        .WithYamlFile("russian.yml"))
                
                .WithLocalization(CultureInfo.GetCultureInfo("pl-PL"),
                    polish => polish
                        .WithXmlFile("polish.xml"));
        }
    }
}
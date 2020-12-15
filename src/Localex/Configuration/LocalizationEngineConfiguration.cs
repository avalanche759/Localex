#region

using System.Globalization;
using Localex.Abstractions.Configuration;

#endregion

namespace Localex.Configuration
{
    public class LocalizationEngineConfiguration : ILocalizationEngineConfiguration
    {
        public CultureInfo DefaultLanguageCulture { get; }

        public string ValuePropertyName { get; }

        public LocalizationEngineConfiguration(CultureInfo defaultLanguageCulture,
            string valuePropertyName)
        {
            DefaultLanguageCulture = defaultLanguageCulture;
            ValuePropertyName = valuePropertyName;
        }

        public static ILocalizationEngineConfiguration Default => new LocalizationEngineConfiguration(
            CultureInfo.CurrentCulture,
            "Value");
    }
}
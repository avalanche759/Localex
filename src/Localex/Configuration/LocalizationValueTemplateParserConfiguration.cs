#region

using Localex.Abstractions.Configuration;

#endregion

namespace Localex.Configuration
{
    public class LocalizationValueTemplateParserConfiguration : ILocalizationValueTemplateParserConfiguration
    {
        public string ParameterStartString { get; }
        public string ParameterEndString { get; }
        public string OptionsStartString { get; }
        public string OptionsDelimiter { get; }

        public LocalizationValueTemplateParserConfiguration(
            string parameterStartString,
            string parameterEndString,
            string optionsStartString,
            string optionsDelimiter)
        {
            ParameterStartString = parameterStartString;
            ParameterEndString = parameterEndString;
            OptionsStartString = optionsStartString;
            OptionsDelimiter = optionsDelimiter;
        }

        public static ILocalizationValueTemplateParserConfiguration Default { get; } =
            new LocalizationValueTemplateParserConfiguration(
                "{",
                "}",
                "?",
                ";");
    }
}
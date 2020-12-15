#region

using Localex.Abstractions.Templates.Parameters;

#endregion

namespace Localex.Templates.Parameters
{
    public class LocalizationValueTemplateParameterOption : ILocalizationValueTemplateParameterOption
    {
        public string Key { get; }

        public string Value { get; }

        public LocalizationValueTemplateParameterOption(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
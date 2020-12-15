#region

using System;
using Localex.Abstractions.Templates.Parameters;

#endregion

namespace Localex.Templates.Parameters
{
    public class LocalizationValueTemplateParameterValue : ILocalizationValueTemplateParameterValue
    {
        public string Name { get; }

        public LocalizationValueTemplateParameterValue(string name, string value)
            : this(name, parameter => value)
        {
        }


        private readonly Func<ILocalizationValueTemplateParameter, string> _formatValueFactory;

        public LocalizationValueTemplateParameterValue(string name,
            Func<ILocalizationValueTemplateParameter, string> formatValueFactory)
        {
            Name = name;
            _formatValueFactory = formatValueFactory;
        }

        public string FormatParameter(ILocalizationValueTemplateParameter parameter)
        {
            return _formatValueFactory?.Invoke(parameter) ?? string.Empty;
        }
    }
}
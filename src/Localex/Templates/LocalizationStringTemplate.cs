#region

using System.Collections.Generic;
using System.Linq;
using Localex.Abstractions.Templates;
using Localex.Abstractions.Templates.Parameters;

#endregion

namespace Localex.Templates
{
    public class LocalizationValueTemplate : ILocalizationValueTemplate
    {
        public string Value { get; }
        public IEnumerable<ILocalizationValueTemplateParameter> Parameters { get; }

        public LocalizationValueTemplate(string value, IEnumerable<ILocalizationValueTemplateParameter> parameters)
        {
            Value = value;
            Parameters = parameters;
        }

        public string Format(
            params ILocalizationValueTemplateParameterValue[] localizationStringTemplateParameterValues)
        {
            string resultValue = Value;

            foreach (ILocalizationValueTemplateParameterValue parameterValue in
                localizationStringTemplateParameterValues)
            {
                foreach (ILocalizationValueTemplateParameter parameter in Parameters)
                {
                    if (parameter.Name == parameterValue.Name)
                    {
                        resultValue = resultValue.Replace(parameter.Format, parameterValue.FormatParameter(parameter));
                    }
                }
            }

            return resultValue;
        }

        public static ILocalizationValueTemplate Empty =>
            new LocalizationValueTemplate("", Enumerable.Empty<ILocalizationValueTemplateParameter>());
    }
}
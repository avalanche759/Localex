#region

using System.Collections.Generic;
using Localex.Abstractions.Templates.Parameters;

#endregion

namespace Localex.Templates.Parameters
{
    public class LocalizationValueTemplateParameter : ILocalizationValueTemplateParameter
    {
        public string Name { get; }
        public string Format { get; }
        public IEnumerable<ILocalizationValueTemplateParameterOption> Options { get; }

        public LocalizationValueTemplateParameter(string name, string format,
            IEnumerable<ILocalizationValueTemplateParameterOption> options)
        {
            Name = name;
            Format = format;
            Options = options;
        }
    }
}
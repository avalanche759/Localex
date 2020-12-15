#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using Localex.Abstractions.Configuration;
using Localex.Abstractions.Templates;
using Localex.Abstractions.Templates.Parameters;
using Localex.Templates.Parameters;

#endregion

namespace Localex.Templates
{
    public class LocalizationValueTemplateParser : ILocalizationValueTemplateParser
    {
        private string ParameterRegex { get; set; } = @"\{{((?<ParameterName>\w+)((?=\{0})(?<Options>[^}}]+))?)\}}";

        private string OptionRegex => @"(?<Key>\w+)=(?<Value>[^{{}}={0}{1}]+)";

        public LocalizationValueTemplateParser(
            ILocalizationValueTemplateParserConfiguration localizationValueTemplateParserConfiguration)
        {
            Configuration = localizationValueTemplateParserConfiguration;
        }

        public ILocalizationValueTemplateParserConfiguration Configuration { get; }

        public ILocalizationValueTemplate Parse(string source)
        {
            ICollection<ILocalizationValueTemplateParameter> parameters =
                new Collection<ILocalizationValueTemplateParameter>();
            string optionsRegex = string.Format(OptionRegex, Configuration.ParameterStartString,
                Configuration.ParameterEndString);
            foreach (Match parameterMatch in Regex.Matches(source,
                string.Format(ParameterRegex, Configuration.OptionsStartString)))
            {
                if (parameterMatch.Success)
                {
                    string parameterName = parameterMatch.Groups["ParameterName"].Value;

                    if (parameters.Any(existingParameter => existingParameter.Name.Equals(parameterName)))
                    {
                        throw new Exception("Parameter with specified name alteady exists.");
                    }

                    string optionsString = parameterMatch.Groups["Options"].Value;
                    if (optionsString.StartsWith(Configuration.OptionsStartString))
                    {
                        optionsString = optionsString.Remove(0, Configuration.OptionsStartString.Length);
                    }

                    string[] options = optionsString.Split(Configuration.OptionsDelimiter,
                        StringSplitOptions.RemoveEmptyEntries);


                    ICollection<ILocalizationValueTemplateParameterOption> optionsCollection =
                        new Collection<ILocalizationValueTemplateParameterOption>();
                    foreach (string option in options)
                    {
                        Match optionMatch = Regex.Match(option, optionsRegex);

                        if (optionMatch.Success)
                        {
                            string optionKey = optionMatch.Groups["Key"].Value;
                            string optionValue = optionMatch.Groups["Value"].Value;

                            optionsCollection.Add(new LocalizationValueTemplateParameterOption(optionKey, optionValue));
                        }
                    }

                    ILocalizationValueTemplateParameter parameter = new LocalizationValueTemplateParameter(
                        parameterName,
                        parameterMatch.Value,
                        optionsCollection
                    );

                    parameters.Add(parameter);
                }
            }

            return new LocalizationValueTemplate(source, parameters);
        }
    }
}
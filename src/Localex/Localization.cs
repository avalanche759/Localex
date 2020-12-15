#region

using System.Collections.Generic;
using System.Globalization;
using Localex.Abstractions;
using Localex.Abstractions.Sources;
using Localex.Templates;

#endregion

namespace Localex
{
    public class Localization : LocalizationNode, ILocalization
    {
        private readonly IEnumerable<ILocalizationSource> _localizationSources;

        public Localization(CultureInfo languageCulture, IEnumerable<ILocalizationSource> localizationSources,
            IEnumerable<ILocalizationNode> localizationNodes)
            : base(languageCulture,
                $"Root_{languageCulture.Name}",
                LocalizationValueTemplate.Empty,
                localizationNodes)
        {
            _localizationSources = localizationSources;
        }

        public IEnumerable<ILocalizationSource> GetSources()
        {
            return _localizationSources;
        }
    }
}
#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Localex.Abstractions;
using Localex.Abstractions.Sources;

#endregion

namespace Localex.Providers.File
{
    public class FileLocalizationProvider : ILocalizationProvider
    {
        private readonly FileLocalizationSource _fileLocalizationSource;

        public FileLocalizationProvider(FileLocalizationSource fileLocalizationSource)
        {
            Source = _fileLocalizationSource = fileLocalizationSource;
        }

        private IEnumerable<ILocalizationNode> Parse(string fileContents)
        {
            if (_fileLocalizationSource.LocalizationNodeParser.CanParse(fileContents))
            {
                return _fileLocalizationSource.LocalizationNodeParser.Parse(fileContents);
            }

            return Enumerable.Empty<ILocalizationNode>();
        }

        public ILocalizationSource Source { get; }
        public IEnumerable<ILocalizationNode> Nodes { get; private set; }

        public void Load()
        {
            try
            {
                using (StreamReader fileReader = new StreamReader(_fileLocalizationSource.FilePath))
                {
                    string contents = fileReader.ReadToEnd();

                    Nodes = Parse(contents);
                }
            }
            catch (Exception exception)
            {
                if (!_fileLocalizationSource.IsOptional)
                    throw exception;
            }
        }
    }
}
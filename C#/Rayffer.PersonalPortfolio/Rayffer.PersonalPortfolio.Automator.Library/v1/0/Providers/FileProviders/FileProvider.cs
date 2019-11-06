using Rayffer.PersonalPortfolio.Automator.Library.v1._0.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Rayffer.PersonalPortfolio.Automator.Library.v1._0.Providers.FileProviders
{
    public class FileProvider : IFileProvider
    {
        public string GetInfo(string file)
        {
            if (!File.Exists(file))
            {
                throw new FileNotFoundException();
            }
            return File.ReadAllText(file);
        }
    }
}
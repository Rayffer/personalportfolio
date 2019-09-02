using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayffer.PersonalPortfolio.SoundscapeManager.DTO
{
    public class SoundCollection
    {
        public SoundCollection()
        {
            SoundNames = new List<string>();
        }
        public string Name { get; set; }
        public List<string> SoundNames { get; set; }
    }
}

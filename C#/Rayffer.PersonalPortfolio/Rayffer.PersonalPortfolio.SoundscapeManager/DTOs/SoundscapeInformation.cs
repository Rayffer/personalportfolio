using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayffer.PersonalPortfolio.SoundscapeManager.DTO
{
    public class SoundscapeInformation
    {
        public SoundscapeInformation()
        {
            SoundscapeSounds = new List<SoundscapeSound>();
        }
        public string Name { get; set; }
        public List<SoundscapeSound> SoundscapeSounds { get; set; }
    }
}

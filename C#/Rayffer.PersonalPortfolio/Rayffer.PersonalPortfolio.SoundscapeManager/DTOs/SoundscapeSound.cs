using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayffer.PersonalPortfolio.SoundscapeManager.DTO
{
    public class SoundscapeSound
    {
        public string Name { get; set; }
        public bool IsLooping { get; set; }
        public int Volume { get; set; }
        public int IntervalSeconds { get; set; }
    }
}

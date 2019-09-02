using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayffer.PersonalPortfolio.SoundscapeManager.Types
{
    public enum LoopIntervals
    {
        [Description("No Interval")]
        NoInterval = 1,
        [Description("5 seconds")]
        _5_Seconds,
        [Description("10 seconds")]
        _10_Seconds,
        [Description("15 seconds")]
        _15_Seconds,
        [Description("20 seconds")]
        _20_Seconds,
        [Description("25 seconds")]
        _25_Seconds,
        [Description("30 seconds")]
        _30_Seconds,
        [Description("45 seconds")]
        _45_Seconds,
        [Description("1 minute")]
        _1_Minute,
        [Description("2 minutes")]
        _2_Minutes,
        [Description("3 minutes")]
        _3_Minutes,
    }
}

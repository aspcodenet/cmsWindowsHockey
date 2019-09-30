using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmsWindowsHockey.Hockey
{
    class HockeyTeam
    {
        public string Serie { get; set; }
        public string Name { get; set; }
        public int GamesPlayed { get; set; }
        public int ThreeP { get; set; }
        public int TwoP { get; set; }
        public int OneP { get; set; }
        public int ZeroP { get; set; }
        public int Goals { get; set; }
        public int GoalsInslappta { get; set; }

        public string Coach { get; set; }
        public string Season { get; set; }

        public int Points { get { return ThreeP * 3 + TwoP * 2 + OneP; } }
    }
}

using GPR.Rank;
using System.Collections.Generic;
using TShockAPI;

namespace GPR
{
    public class GPlayer
    {
        public GPlayer(int index)
        {
            Index = index;
        }
        public int Index { get; internal set; }
        public TSPlayer Player => TShock.Players[Index];
        public string Name => Player.Name;
        internal GConfig.GPRLevel _level;
        public GConfig.GPRLevel Level { get { return _level; } set { this.RankUpDirect(value); } }
        public List<GConfig.GPRLevel> NextLevel => Level._rankupLevel;
        public static implicit operator TSPlayer(GPlayer gplr) => gplr.Player;
        public static implicit operator GPlayer(TSPlayer plr) => plr.GPlayer();
    }
}

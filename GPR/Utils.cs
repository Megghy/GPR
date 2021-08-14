using System.Collections.Generic;
using System.Linq;
using TShockAPI;

namespace GPR
{
    public static class Utils
    {
        public static GPlayer GPlayer(this TSPlayer plr) => plr.GetData<GPlayer>("GPR.GPlayer");
        public static int Balance(this GPlayer gplr) => UnifiedEconomyFramework.UEFPlugin.Balance(gplr.Name);
        public static void ChangeBalance(this GPlayer gplr, int num)
        {
            if (num >= 0)
                UnifiedEconomyFramework.UEFPlugin.MoneyUp(gplr.Name, num);
            else
                UnifiedEconomyFramework.UEFPlugin.MoneyDown(gplr.Name, -num);
        }
        public static bool HasItem(this GPlayer gplr, int type, int stack) => gplr.Player.TPlayer.inventory.Any(item => item.type == type && item.stack >= stack);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using TShockAPI;

namespace GPR.Rank
{
    public static class RankSystem
    {
        public static bool TryRankUp(this GPlayer gplr, GConfig.GPRLevel nextLevel = null)
        {
            if (nextLevel is not null)
            {
                if (gplr.Balance() >= nextLevel.Cost)
                {
                    gplr.ChangeBalance(-nextLevel.Cost);
                    gplr.RankUpDirect(nextLevel);
                    gplr.Player.SendSuccessMessage($"成功升级为 {nextLevel.DisplayName}");
                    return true;
                }
                else
                    gplr.Player.SendErrorMessage($"余额不足. 你的余额为: {gplr.Balance()}");
            }
            else if (gplr.NextLevel is not null)
                gplr.SendMultiLevelInfo(gplr.NextLevel);
            else
                gplr.Player.SendErrorMessage($"你已达到最高等级");
            return false;
        }
        public static void RankUpDirect(this GPlayer gplr, GConfig.GPRLevel nextLevel)
        {
            gplr._level = nextLevel;
            gplr.Player.tempGroup = TShock.Groups.GetGroupByName("superadmin");
            nextLevel.RankupCommand.ForEach(c => Commands.HandleCommand(gplr.Player, c));
            gplr.Player.tempGroup = default;
        }
        public static void SendMultiLevelInfo(this GPlayer gplr, List<GConfig.GPRLevel> levels)
        {
            var sb = new StringBuilder();
            levels.ForEach(l => {
                sb.Append($"{levels.IndexOf(l) + 1}: {l.DisplayName}, 要求: {l.Cost}");
                if (l.RquireItem.Any())
                    l.RquireItem.ForEach(i => sb.Append($"{Lang.GetItemNameValue(i.Type).Color(gplr.HasItem(i.Type, i.Stack) ? "ADDE79" : "D67373")}: {i.Stack} 个, "));
                sb.Append(Environment.NewLine);
            });
            gplr.Player.SendInfoMessage($"你当前的等级为: {gplr.Level}, 可进阶的等级如下:\n{sb}");
        }
    }
}

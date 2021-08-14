using GPR.Rank;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;

namespace GPR
{
    [ApiVersion(2, 1)]
    public class GPRMain : TerrariaPlugin
    {
        public GPRMain(Main game) : base(game) { Instance = this; }
        public override void Initialize()
        {
            Commands.ChatCommands.Add(new("gpr.use", OnCommand, "gpr") { AllowServer = false });
        }
        public override string Name => "GPR"; 
        public override string Author => "Megghy";
        public override string Description => "一个职业加技能框架";
        public override Version Version => System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        public static GPRMain Instance { get; internal set; }
        public static GConfig Config { get; internal set; } = new();
        public static List<TSPlayer> OnlinePlayers => TShock.Players.Where(p => p is { Active: true }).ToList();
        void OnCommand(CommandArgs args)
        {
            var plr = args.Player;
            var cmd = args.Parameters;
            var gplr = plr.GPlayer();
            if (cmd.Any())
                switch (cmd.First())
                {
                    case "up":
                    case "rankup":
                        gplr.TryRankUp();
                        break;
                    default:
                        Help();
                        break;
                }
            else
                Help();
            void Help()
            {

            }
        }
    }
}

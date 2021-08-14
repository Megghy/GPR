using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TShockAPI;

namespace GPR
{
    public class GConfig
    {
        public static void Load()
        {
            try
            {
                var directoryPath = Path.Combine(TShock.SavePath, "GPR");
                var configPath = Path.Combine(directoryPath, "GPRConfig.json");
                var skillPath = Path.Combine(directoryPath, "Skill");
                if (!Directory.Exists(directoryPath) || !File.Exists(configPath))
                {
                    Directory.CreateDirectory(directoryPath);
                    Directory.CreateDirectory(skillPath);
                    FileTools.CreateIfNot(configPath, JsonConvert.SerializeObject(new GConfig()
                    {
                        DefaultLevel = "lv1",
                        AutoCreateGroup = true,
                        Level = {
                            new()
                            {
                                DisplayName = "萌新",
                                Name = "lv1",
                                Description = "冒险开始的地方",
                                ParentLevel = { },
                                RankupLevel = { "lv2" },
                                RankupCommand = { },
                                Cost = 0,
                                Info = new()
                                {
                                    InheriteInfo = true,
                                    ChatPrefix = "[萌新]",
                                    Permission = { },
                                    Skills = { },
                                }
                            },
                            new()
                            {
                                DisplayName = "冒险者",
                                Name = "lv2",
                                Description = "继续前进",
                                ParentLevel = { "lv1"},
                                RankupLevel = { "lv2" },
                                RankupCommand = { "/bc {player}升级成为了{displayname}!", "/i 29 3" },
                                Cost = 500,
                                Info = new()
                                {
                                    InheriteInfo = true,
                                    ChatPrefix = "[冒险者]",
                                    Permission = { "tshock.tp.self" },
                                    Skills = { },
                                }
                            }
                        }
                    }, Formatting.Indented));
                }
                GPRMain.Config = JsonConvert.DeserializeObject<GConfig>(File.ReadAllText(configPath));
                TShock.Log.ConsoleInfo("<GPR> 成功读取配置文件");
            }
            catch (Exception ex)
            {
                TShock.Log.ConsoleError("<GPR> 无法读取配置文件: " + ex.Message);
                Console.ReadKey();
            }
        }
        public string DefaultLevel { get; set; }
        public bool AutoCreateGroup { get; set; }
        public List<GPRLevel> Level { get; set; }
        public class GPRLevel
        {
            public string DisplayName { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public List<string> ParentLevel { get; set; }
            public List<string> RankupLevel { get; set; }
            public List<string> RankupCommand { get; set; }
            public int Cost { get; set; }
            public List<RquireItem> RquireItem { get; set; }
            public LevelInfo Info { get; set; }
            [JsonIgnore]
            public List<GPRLevel> _fromLevel { get; internal set; }
            [JsonIgnore]
            public List<GPRLevel> _rankupLevel { get; internal set; }
        }
        public struct RquireItem
        {
            public int Type { get; set; }
            public int Stack { get; set; }
        }
        public class LevelInfo
        {
            public bool InheriteInfo { get; set; }
            public string ChatPrefix { get; set; }
            public List<string> Permission { get; set; }
            public List<SkillInfo> Skills { get; set; }
        }
        public class SkillInfo
        {
            public List<int> BindItems { get; set; }
            public string FilePath { get; set; }
        }
    }
}

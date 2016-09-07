using GodsRevenge.Mounts;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;

namespace GodsRevenge
{
    public class GodsRevenge : Mod
    {
        public const string captiveElementHead = "GodsRevenge/NPCs/Abomination/CaptiveElement_Head_Boss_";
        public const string captiveElement2Head = "GodsRevenge/NPCs/Abomination/CaptiveElement2_Head_Boss_";
        public int CustomSpawnRate = 0;
        public bool CustomSpawn = false;
        public int CustomMaxSpawn = 0;
        private double pressedActivateDrillHotKeyTime;
        public GodsRevenge()
        {
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };
        }
        public override void Load()
        {
            RegisterHotKey("Activate Drill", "z");
        }
        public override void HotKeyPressed(string name)
        {
            Player player = Main.player[Main.myPlayer];
            if (name == "Activate Drill" && player.HasBuff(GetBuff("DrillMount").Type) >= 0)
            {
                if (Math.Abs(Main.time - pressedActivateDrillHotKeyTime) > 60)
                {
                    pressedActivateDrillHotKeyTime = Main.time;
                    PlayerClass modPlayer = player.GetModPlayer<PlayerClass>(this);
                    string msg = "";
                    if (modPlayer.drillActivated)
                    {
                        modPlayer.drillActivated = false;
                        msg = "Deactivated";
                    }
                    else
                    {
                        modPlayer.drillActivated = true;
                        msg = "Activated";
                    }
                    Main.NewText($"The Drill has been {msg}.");
                }
            }
            base.HotKeyPressed(name);
        }
        public override void ChatInput(string text)
        {
            if (text[0] != '/')
            {
                return;
            }
            text = text.Substring(1);
            int index = text.IndexOf(' ');
            string command;
            string[] args;
            if (index < 0)
            {
                command = text.ToLower();
                args = new string[0];
            }
            else
            {
                command = text.Substring(0, index);
                args = text.Substring(index + 1).Split(' ');
            }
            if (command == "players")
            {
                string players = "";
                foreach (var player in Main.PlayerList)
                    players += player.Name + " ";
                NetMessage.SendData(25, -1, -1, $"{players}", 255, 0, 255, 51, 0);
            }
            else if (command == "kill")
            {
                Kill(args);
            }
            else if (command == "tooltip")
            {
                FindTooltipWith(args);
            }
            else if (command == "spawnrate")
            {
                ChangeSpawnRate(args);
            }
        }
        public void ChangeSpawnRate(string[] args)
        {
            // /spawnrate true/false
            // /spawnrateset spawnrate maxspawn


            if (args.Length == 1)
            {
                if (args[0] == "true" || args[0] == "false")
                {
                    var netMessage = GetPacket();
                    netMessage.Write((byte)ModMessageType.ChangeSpawnRate);
                    netMessage.Write((byte)SpawnRate.IsEnabled);
                    netMessage.Write((bool)bool.Parse(args[0]));
                    netMessage.Send();
                }
                else
                    Main.NewText($"You must set this variable to true or false! ({args.ToString()})", 255, 0, 0);
            }
            else if (args.Length == 2)
            {
                int num1 = 0;
                int num2 = 0;

                if (int.TryParse(args[0], out num1) && int.TryParse(args[1], out num2))
                {
                    var netMessage = GetPacket();
                    netMessage.Write((byte)ModMessageType.ChangeSpawnRate);
                    netMessage.Write((byte)SpawnRate.ChangeValues);
                    netMessage.Write((int)num1);
                    netMessage.Write((int)num2);
                    netMessage.Send();
                }
                else
                    Main.NewText($"You must a valid number for the spawn rate and the maximum spawn. ({args.ToString()})", 255, 0, 0);
            }
            else
                Main.NewText($"Invalid amount of parameters ({args.ToString()})", 255, 0, 0);
        }

        public void FindTooltipWith(string[] keywords)
        {
            string msg = string.Join("", keywords);
            List<Item> items = new List<Item>();
            for (int i = 0; i < Main.itemTexture.Length; i++)
            {
                Item item = new Item();
                item.SetDefaults(i);
                items.Add(item);
            }
            StreamWriter file = new StreamWriter("d:\\TerrariaTooltipSearch.txt");
            foreach (var item in items.FindAll(x => x.toolTip.Contains(msg) || x.toolTip2.Contains(msg)))
            {
                file.WriteLine($"Name: {item.name}, ID: {item.type}{Environment.NewLine}Tooltip 1: {item.toolTip}{Environment.NewLine}Tooltip2: {item.toolTip2}");
            }
            file.Close();
        }
        public void Kill(string[] args)
        {
            var netMessage = GetPacket();
            netMessage.Write((byte)ModMessageType.KillPlayer);
            netMessage.Write((string)string.Join("", args));
            netMessage.Send();
        }
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            ModMessageType msgType = (ModMessageType)reader.ReadByte();
            string playerName = Netplay.Clients[whoAmI].Name;
            switch (msgType)
            {
                case ModMessageType.ChangeTime:
                    int time = reader.ReadInt32();
                    string msg = reader.ReadString();
                    bool day = reader.ReadBoolean();

                    Main.time = time;
                    Main.dayTime = day;
                    NetMessage.SendData(25, -1, -1, $"The time has been changed to: {msg} ({time}) by {playerName}.", 255, 0, 255, 51, 0);
                    NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f, 0);
                    break;

                case ModMessageType.SpawnNPC:
                    int positionX = reader.ReadInt32();
                    int positionY = reader.ReadInt32();
                    int npcType = reader.ReadInt32();
                    string npcName = reader.ReadString();
                    NPC.NewNPC(positionX, positionY, npcType, 0, 0f, 0f, 0f, 0f, 255);
                    NetMessage.SendData(25, -1, -1, $"{playerName} spawned ({npcType}) {npcName}.", 255, 255, 140, 0, 0);
                    NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f, 0);
                    break;

                case ModMessageType.KillPlayer:
                    string namePlayerToKill = reader.ReadString();
                    var player = Main.PlayerList.Find(x => x.Name == namePlayerToKill);
                    if (player != null)
                    {
                        Main.player[player.Player.whoAmI].statLife = 0;
                        NetMessage.SendData(25, -1, -1, $"{namePlayerToKill} was killed succesfuly by {playerName}.", 255, 255, 140, 0, 0);
                        NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f, 0, 0, 0);
                    }
                    else
                        NetMessage.SendData(25, -1, -1, $"Could not find {namePlayerToKill}.", 255, 255, 140, 0, 0);
                    break;
                case ModMessageType.ChangeSpawnRate:
                    SpawnRate spawnType = (SpawnRate)reader.ReadByte();
                    switch (spawnType)
                    {
                        case SpawnRate.IsEnabled:
                            CustomSpawn = reader.ReadBoolean();
                            NetMessage.SendData(25, -1, -1, $"Custom spawn rates set to: {CustomSpawn} by {playerName}.", 255, 255, 140, 0, 0);
                            break;
                        case SpawnRate.ChangeValues:
                            CustomSpawnRate = reader.ReadInt32();
                            CustomMaxSpawn = reader.ReadInt32();
                            NetMessage.SendData(25, -1, -1, $"Spawn rate changed to {CustomSpawnRate} and max spawn rate to {CustomMaxSpawn} by {playerName}.", 255, 255, 140, 0, 0);
                            break;
                        default:
                            break;
                    }
                    break;
                case ModMessageType.DestroyBlock:
                    for (int i = 0; i < 3; i++)
                        for (int j = 0; j < 3; j++)
                        {
                            WorldGen.KillTile((int)Main.player[whoAmI].position.X / 16 + (Main.player[whoAmI].direction * i), (int)Main.player[whoAmI].position.Y / 16 + j);
                            NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f, 0);
                        }

                    break;
            }
        }
        public enum ModMessageType : byte
        {
            SpawnNPC,
            ChangeTime,
            KillPlayer,
            ChangeSpawnRate,
            DestroyBlock
        }
        public enum SpawnRate : byte
        {
            IsEnabled,
            ChangeValues
        }
    }
}

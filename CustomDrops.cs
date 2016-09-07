using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GodsRevenge
{
    public class CustomDrops : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            Random rnd = new Random();
            if (npc.type == NPCID.WallofFlesh)
            {
                if (rnd.Next(14) == 0)
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ImpulseStrikeTome"), 1);
                if (rnd.Next(14) == 0)
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GhostwalkerHammer"), 1);     
            }
            if(npc.type == NPCID.WyvernHead)
            {
                if(rnd.Next(25) == 0)
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DragonSword"), 1);
            }
            if(npc.type == NPCID.MartianSaucer)
            {
                if (rnd.Next(10) == 0)
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("KatachimiSword"), 1);
            }
            if(npc.type == NPCID.Paladin)
            {
                if (rnd.Next(20) == 0)
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FaithkeeperHammer"), 1);
            }
            if(npc.type == NPCID.Golem)
            {
                if (rnd.Next(5) == 0)
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("StrangeCoal"), 1);
            }
        }
    }
}

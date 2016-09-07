using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;

namespace GodsRevenge
{
    public class ChangeSpawnRate : GlobalNPC
    {
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            GodsRevenge godsRevenge = (GodsRevenge)mod;
            if (godsRevenge.CustomSpawn)
            {
                spawnRate = godsRevenge.CustomSpawnRate;
                maxSpawns = godsRevenge.CustomMaxSpawn;
            }
            else
                base.EditSpawnRate(player, ref spawnRate, ref maxSpawns);
        }
    }
}

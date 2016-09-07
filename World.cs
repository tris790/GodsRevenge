using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;
using System.IO;

namespace GodsRevenge
{
    public class WorldClass : ModWorld
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {

            int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
            if (ShiniesIndex == -1)
            {
                // Shinies pass removed by some other mod.
                return;
            }
            tasks.Insert(ShiniesIndex + 1, new PassLegacy("God Ores", delegate (GenerationProgress progress)
            {
                progress.Message = "Adding God Ore";

                for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); k++)
                {
                    WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)WorldGen.worldSurfaceLow, Main.maxTilesY), (double)WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6), mod.TileType("GodOre"), false, 0f, 0f, false, true);
                }
            }));
        }
    }
}

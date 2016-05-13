using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;

namespace GodsRevenge.Items
{
	public class CustomPickaxe : ModItem
	{
		public override void SetDefaults()
		{
            item.name = "Zeus Pickaxe";
            item.damage = 99;
            item.melee = true;
            item.width = 40;
            item.height = 40;
            item.toolTip = "This could destroy the whole earth 2.0.";
            item.useTime = 1;
            item.useAnimation = 10;
            item.pick = 220;
            item.useStyle = 1;
            item.knockBack = 6;
            item.value = 999999;
            item.rare = 5;
            item.useSound = 2;
            item.autoReuse = true;
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Wood, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(10) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, mod.DustType("Sparkle"));
            }
        }
    }
}

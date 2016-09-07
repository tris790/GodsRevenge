using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace GodsRevenge
{
    public class PlayerClass : ModPlayer
    {
        private const int saveVersion = 0;
        public bool Pet = false;
        public bool exampleLightPet = false;
        public bool thomasMinion = false;
        public bool theChestPet = false;
        public bool invincible = false;
        public bool ghostlyBarrier = false;
        public bool drillActivated = false;
        public override void Kill(double damage, int hitDirection, bool pvp, string deathText)
        {
            foreach (var item in player.armor)
            {
                if (item.name == "Prometheus Shield")
                    player.respawnTimer = 300;
            }
        }
        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            if (Main.gameMenu == false)
            {
                var player = Main.player[Main.myPlayer];
                if (player.HasBuff(mod.BuffType("TrainMount")) >= 0)
                {
                    foreach (var layer in layers)
                    {
                        if (!layer.Name.ToLower().Contains("mount"))
                            layer.visible = false;
                    }

                }
            }

            base.ModifyDrawLayers(layers);
        }
        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref string deathText)
        {
            if (player.HasBuff(mod.BuffType("InvincibilityBuff")) >= 0)
                return false;
            else
                return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref deathText);
        }
        public override void ResetEffects()
        {
            exampleLightPet = false;
            Pet = false;
            thomasMinion = false;
            theChestPet = false;
            invincible = false;
            if (Main.myPlayer != -1 && !Main.gameMenu)
            {
                if (player.mount.Type != -1)
                    if (player.HasBuff(mod.GetBuff("DrillMount").Type) < 0)
                        drillActivated = false;
            }
        }

        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (player.name == "gh" || player.name == "Aristea")
            {

                //if (Main.rand.Next(1) == 0 && drawInfo.shadow == 0f)
                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
                {
                    //int dust = Dust.NewDust(new Vector2(drawInfo.position.X, drawInfo.position.Y + 40) + new Vector2(2f, 2f), player.width, 30, mod.DustType("EtherealFlame"), player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, GodColour.GodColourRed, 3f);
                    int dust0 = Dust.NewDust(new Vector2(drawInfo.position.X - 50, drawInfo.position.Y - 70) - new Vector2(2f, 2f), player.width + 100, player.height + 70, mod.DustType("MoneyDust"), player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, Colors.CoinGold, 3f);

                    Main.dust[dust0].noGravity = true;
                    Main.dust[dust0].velocity *= 1.8f;
                    Main.dust[dust0].velocity.Y -= 0.5f;
                    Main.playerDrawDust.Add(dust0);
                }
                //r *= 0.1f;
                //g *= 0.2f;
                //b *= 0.7f;
                fullBright = true;
            }
            if (player.name == "allo" || player.name.ToLower() == "william") //|| player.name == "gh"
            {

                //if (Main.rand.Next(1) == 0 && drawInfo.shadow == 0f)
                if (Main.rand.Next(4) == 0 && drawInfo.shadow == 0f)
                {
                    int dust0 = Dust.NewDust(new Vector2(drawInfo.position.X - 50, drawInfo.position.Y - 70) - new Vector2(2f, 2f), player.width + 100, player.height + 70, mod.DustType("WilliamDust"), player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 3f);

                    Main.dust[dust0].noGravity = true;
                    Main.dust[dust0].velocity *= 1.8f;
                    Main.dust[dust0].velocity.Y -= 0.5f;
                    Main.playerDrawDust.Add(dust0);

                }
                r *= 0.1f;
                g *= 0.2f;
                b *= 0.7f;
                fullBright = true;
            }
        }
    }
}


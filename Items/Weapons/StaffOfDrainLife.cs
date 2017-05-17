using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using HeartOfCrimson.Projectiles;

namespace HeartOfCrimson.Items.Weapons
{
    public class StaffOfDrainLife : ModItem
    {
        private const float LATCH_DISTANCE = 50f;

        public override void SetDefaults()
        {
            item.name = "Staff of Drain Life";
            item.damage = 2;
            item.magic = true;
            item.mana = 1;
            item.width = 40;
            item.height = 40;
            item.toolTip = "Siphons life from the target.";
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 5;
            Item.staff[item.type] = true; // this makes the useStyle animate as a staff instead of as a gun
            item.noMelee = true; // so the item's animation doesn't do damage
            item.knockBack = 0;
            item.value = 10000;
            item.rare = 2;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("DrainLifeParticle");
            item.shootSpeed = 0f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockback)
        {
            Vector2 mousePos = new Vector2(Main.mouseX + Main.screenPosition.X, Main.mouseY + Main.screenPosition.Y);

            NPC nearestNPC = null;
            float nearestDist = -1f;
            
            foreach(NPC npc in Main.npc)
            {
                if(!npc.active || npc.friendly)
                {
                    continue;
                }

                float dist = Vector2.Distance(mousePos, npc.position);
                if (dist <= LATCH_DISTANCE && nearestNPC == null)
                {
                    nearestNPC = npc;
                    nearestDist = dist;
                }
                else if (dist <= LATCH_DISTANCE && dist < nearestDist)
                {
                    nearestNPC = npc;
                    nearestDist = dist;
                }
            }

            if(nearestNPC != null)
            {
                
                Projectile.NewProjectile(nearestNPC.Center, new Vector2(0,0), type, damage, knockback, player.whoAmI, (int)DrainLifeParticle.DrainLifeParticleType.WeaponProjectile);
            }

            return false; // do not spawn a default projectile
        }
    }
}
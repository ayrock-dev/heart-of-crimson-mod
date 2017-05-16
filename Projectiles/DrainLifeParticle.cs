using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HeartOfCrimson.Projectiles
{
    public class DrainLifeParticle : ModProjectile
    {
        public enum DrainLifeParticleType : int
        {
            WeaponProjectile = 2,
            EffectParticle = 3
        }

        public override void SetDefaults()
        {
            projectile.name = "Drain Life Particle";
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 600;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if(projectile.ai[0] == (int)DrainLifeParticleType.WeaponProjectile)
            {
                Player owner = Main.player[projectile.owner];
                owner.statLife += damage;
                owner.HealEffect(damage, true);
            }
        }

        public override bool NewTileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            if (projectile.ai[0] == (int)DrainLifeParticleType.EffectParticle)
            {
                width = 0;
                height = 0;
                return false;
            }
            return true;
        }

        public override bool? CanCutTiles()
        {
            return false;
        }

        public override bool CanDamage()
        {
            return projectile.ai[0] == (int)DrainLifeParticleType.WeaponProjectile;
        }

        public override void AI()
        {
            if(projectile.ai[0] == (int)DrainLifeParticleType.EffectParticle)
            {
                Player owner = Main.player[projectile.owner];
                int dist = (int)Vector2.Distance(owner.position, projectile.position);
                if (projectile.ai[1] == 0 && dist < 50)
                {
                    projectile.ai[1] = 1; // mark this particle for death;
                    projectile.timeLeft = 30;
                    
                }
                if (projectile.ai[1] == 1)
                {
                    projectile.alpha = (projectile.timeLeft / 30) * 255;
                }
                Vector2 dir = Vector2.Normalize(owner.position - projectile.position) * 0.25f;
                projectile.velocity += dir;
                projectile.scale *= 0.99f;
                float light = 0.15f * projectile.scale;
                Lighting.AddLight(projectile.position, light, light * 2f, light);
            }
        }

        public override void Kill(int timeLeft)
        {
            if(projectile.ai[0] == (int)DrainLifeParticleType.WeaponProjectile)
            {
                Player owner = Main.player[projectile.owner];

                Random rand = new Random();
                float angle;
                Vector2 dir;
                float speed;

                for (int k = 0; k < 15; k++)
                {
                    angle = (float)rand.NextDouble() * MathHelper.TwoPi;
                    dir = new Vector2((float)Math.Cos(angle), -(float)Math.Sin(angle));
                    speed = ((float)rand.NextDouble() * 3.2f);
                    Projectile.NewProjectile(projectile.position, dir * speed, mod.ProjectileType("DrainLifeParticle"), 0, 0, owner.whoAmI, (int)DrainLifeParticleType.EffectParticle, 0);
                }
                Main.PlaySound(SoundID.Item25, projectile.position);
            }
        }
    }
}

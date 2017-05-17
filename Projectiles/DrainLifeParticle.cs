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

        private const int DEFAULT_TIME_LEFT = 600;
        private const int DEFAULT_ALPHA = 150;
        private const int FRAMES_TO_DIE = 2;

        public override void SetDefaults()
        {
            projectile.name = "Drain Life Particle";
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.timeLeft = DEFAULT_TIME_LEFT;
            projectile.alpha = DEFAULT_ALPHA;
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

        public override bool PreAI()
        {
            if((int)projectile.ai[1] == 1)
            {
                projectile.Kill();
                return false;
            }
            return true;
        }

        public override void AI()
        {
            if(projectile.ai[0] == (int)DrainLifeParticleType.EffectParticle)
            {
                Player owner = Main.player[projectile.owner];
                if ((int)projectile.ai[1] == -1)
                {
                    int dist = (int)Vector2.Distance(owner.position, projectile.position);
                    if (dist < 40)
                    { 
                        projectile.ai[1] = FRAMES_TO_DIE; // mark this particle for death;
                    }
                }
                else
                {
                    projectile.ai[1] = (int)projectile.ai[1] - 1;
                    projectile.alpha = ((int)projectile.ai[1] / FRAMES_TO_DIE) * DEFAULT_ALPHA;
                }
                Vector2 dir = Vector2.Normalize(owner.position - projectile.position);
                var speed = projectile.velocity.Length();
                projectile.velocity = Vector2.Normalize(projectile.velocity + dir) * speed;
                if (projectile.scale > 0.25f)
                {
                    projectile.scale *= 0.99f;
                }
                float light = 0.5f * projectile.scale;
                Lighting.AddLight(projectile.position, light, light * 1.5f, light);

                Random rand = new Random(projectile.GetHashCode());
                int chanceForDust = rand.Next(0, 60);
                if(chanceForDust == 1)
                {
                    Dust.NewDust(projectile.position + projectile.velocity, projectile.width / 2, projectile.height / 2, mod.DustType("Sparkle"), projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
                }
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

                for (int k = 0; k < 8; k++)
                {
                    angle = (float)rand.NextDouble() * MathHelper.TwoPi;
                    dir = new Vector2((float)Math.Cos(angle), -(float)Math.Sin(angle));
                    speed = ((float)rand.NextDouble() * 4f) + 4f;
                    Projectile.NewProjectile(projectile.position, dir * speed, mod.ProjectileType("DrainLifeParticle"), 0, 0, owner.whoAmI, (int)DrainLifeParticleType.EffectParticle, -1);
                }
                Main.PlaySound(SoundID.Item25, projectile.position);
            }
        }
    }
}

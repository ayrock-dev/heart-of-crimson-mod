
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace HeartOfCrimson
{
	public class HoCModPlayer : ModPlayer
	{
		public bool HasInfiltration;
        public int InfiltrationCooldown;
		
		public override void ResetEffects()
		{
			HasInfiltration = false;
            InfiltrationCooldown = 0;
        }

		public override void GetWeaponDamage(Item item, ref int damage) 
		{
			if (HasInfiltration) {
				damage *= 2;
			}
		}
    }
}
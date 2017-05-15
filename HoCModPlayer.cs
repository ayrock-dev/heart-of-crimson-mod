
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
		
		public override void ResetEffects()
		{
			HasInfiltration = false;
		}

		public override void GetWeaponDamage(Item item, ref int damage) 
		{
			if (HasInfiltration) {
				damage *= 2;
			}
		}

		public override void DrawEffects (PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
		{
			if (HasInfiltration) {
				a = 0.5f;
			}
		}
	}
}
using Terraria;
using Terraria.ModLoader;

namespace HeartOfCrimson.Buffs
{
	public class Infiltrator : ModBuff
	{
		public override void SetDefaults()
		{
			Main.buffName[Type] = "Infiltrator";
			Main.buffTip[Type] = "You have invisibility";
            canBeCleared = false;
        }

		public override void Update(Player player, ref int buffIndex) 
		{
			player.GetModPlayer<HoCModPlayer>(mod).HasInfiltration = true;
        }
	}
}

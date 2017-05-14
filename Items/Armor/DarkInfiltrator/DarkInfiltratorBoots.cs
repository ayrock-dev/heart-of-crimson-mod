using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HeartOfCrimson.Items.Armor.DarkInfiltrator
{
	public class DarkInfiltratorBoots : ModItem
	{
		public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
		{
			equips.Add(EquipType.Legs);
			return true;
		}

		public override void SetDefaults()
		{
			item.name = "Dark Infiltrator Boots";
			item.width = 18;
			item.height = 18;
			AddTooltip("Shady boots.");
			item.value = 10000;
			item.rare = 2;
			item.defense = 60;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HeartOfCrimson.Items.Armor.DarkInfiltrator
{
	public class DarkInfiltratorCloak : ModItem
	{
		public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
		{
			equips.Add(EquipType.Body);
			return true;
		}

		public override void SetDefaults()
		{
			item.name = "Dark Infiltrator Cloak";
			item.width = 18;
			item.height = 18;
			AddTooltip("This is a modded body armor.");
			item.value = 10000;
			item.rare = 2;
			item.defense = 60;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.ShadowScale, 4);
			recipe.AddIngredient(ItemID.Silk, 8);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TissueSample, 4);
			recipe.AddIngredient(ItemID.Silk, 8);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}

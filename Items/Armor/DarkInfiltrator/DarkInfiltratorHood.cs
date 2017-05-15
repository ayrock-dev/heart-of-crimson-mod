using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HeartOfCrimson.Items.Armor.DarkInfiltrator
{
	public class DarkInfiltratorHood : ModItem
	{
		public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
		{
			equips.Add(EquipType.Head);
			return true;
		}

		public override void SetDefaults()
		{
			item.name = "Dark Infiltrator Hood";
			item.width = 18;
			item.height = 18;
			AddTooltip("Shady looking!");
			item.value = 10000;
			item.rare = 2;
			item.defense = 60;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("DarkInfiltratorCloak") && legs.type == mod.ItemType("DarkInfiltratorBoots");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Infiltrator";
			player.GetModPlayer<HoCModPlayer>(mod).HasInfiltration = true;
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
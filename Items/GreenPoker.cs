using Terraria.ID;
using Terraria.ModLoader;

namespace HeartOfCrimson.Items
{
	public class GreenPoker : ModItem
	{
		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.TerraBlade);
			item.name = "Green Poker";
			item.toolTip = "I will poke you with this green poker!";
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

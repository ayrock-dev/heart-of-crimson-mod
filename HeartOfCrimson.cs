using Terraria.ModLoader;

namespace HeartOfCrimson
{
	class HeartOfCrimson : Mod
	{
		public HeartOfCrimson()
		{
			Properties = new ModProperties()
			{
				Autoload = true,
				AutoloadGores = true,
				AutoloadSounds = true
			};
		}
	}
}

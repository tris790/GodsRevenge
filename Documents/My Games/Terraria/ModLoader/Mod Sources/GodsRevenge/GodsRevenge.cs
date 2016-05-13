using Terraria.ModLoader;

namespace GodsRevenge
{
    class GodsRevenge : Mod
    {
        public GodsRevenge()
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

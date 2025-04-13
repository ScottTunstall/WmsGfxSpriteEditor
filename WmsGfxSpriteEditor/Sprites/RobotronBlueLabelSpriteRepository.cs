using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WmsGfxSpriteEditor.Sprites
{
    public class RobotronBlueLabelSpriteRepository : ISpriteRepository
    {
        private readonly List<SpriteInfo> _sprites = new();

        /// <summary>
        /// Gets the number of sprites in the repository
        /// </summary>
        public int Count => _sprites.Count;

        /// <summary>
        /// Initializes a new instance of the RobotronSpriteRepository class and populates it with Robotron sprite data
        /// </summary>
        public RobotronBlueLabelSpriteRepository()
        {
            Load();
        }

        /// <summary>
        /// Gets all available sprites
        /// </summary>
        /// <returns>A collection of sprite information</returns>
        public IEnumerable<SpriteInfo> GetAllSprites() => _sprites.AsReadOnly();

        /// <summary>
        /// Gets a sprite by its index
        /// </summary>
        /// <param name="index">The zero-based index of the sprite</param>
        /// <returns>The sprite information or null if the index is out of range</returns>
        public SpriteInfo? GetSpriteByIndex(int index) => index >= 0 && index < _sprites.Count ? _sprites[index] : null;

        /// <summary>
        /// Populates the repository with Robotron sprite data
        /// </summary>
        public void Load()
        {
            _sprites.Clear();

            // Taken from https://www.seanriddle.com/robotronsprites.txt
            _sprites.Add(new SpriteInfo("familydeath", 1083, 6, 11));
            _sprites.Add(new SpriteInfo("1000", 1177, 6, 5));
            _sprites.Add(new SpriteInfo("2000", 1207, 6, 5));
            _sprites.Add(new SpriteInfo("3000", 1237, 6, 5));
            _sprites.Add(new SpriteInfo("4000", 1267, 6, 5));
            _sprites.Add(new SpriteInfo("5000", 1297, 6, 5));
            _sprites.Add(new SpriteInfo("mommy1", 1375, 4, 14));
            _sprites.Add(new SpriteInfo("mommy2", 1431, 4, 14));
            _sprites.Add(new SpriteInfo("mommy3", 1487, 4, 14));
            _sprites.Add(new SpriteInfo("mommy4", 1543, 4, 14));
            _sprites.Add(new SpriteInfo("mommy5", 1599, 4, 14));
            _sprites.Add(new SpriteInfo("mommy6", 1655, 4, 14));
            _sprites.Add(new SpriteInfo("mommy7", 1711, 4, 14));
            _sprites.Add(new SpriteInfo("mommy8", 1767, 4, 14));
            _sprites.Add(new SpriteInfo("mommy9", 1823, 4, 14));
            _sprites.Add(new SpriteInfo("mommy10", 1879, 4, 14));
            _sprites.Add(new SpriteInfo("mommy11", 1935, 4, 14));
            _sprites.Add(new SpriteInfo("mommy12", 1991, 4, 14));
            _sprites.Add(new SpriteInfo("daddy1", 2095, 5, 13));
            _sprites.Add(new SpriteInfo("daddy2", 2160, 5, 13));
            _sprites.Add(new SpriteInfo("daddy3", 2225, 5, 13));
            _sprites.Add(new SpriteInfo("daddy4", 2290, 5, 13));
            _sprites.Add(new SpriteInfo("daddy5", 2355, 5, 13));
            _sprites.Add(new SpriteInfo("daddy6", 2420, 5, 13));
            _sprites.Add(new SpriteInfo("daddy7", 2485, 5, 13));
            _sprites.Add(new SpriteInfo("daddy8", 2550, 5, 13));
            _sprites.Add(new SpriteInfo("daddy9", 2615, 5, 13));
            _sprites.Add(new SpriteInfo("daddy10", 2680, 5, 13));
            _sprites.Add(new SpriteInfo("daddy11", 2745, 5, 13));
            _sprites.Add(new SpriteInfo("daddy12", 2810, 5, 13));
            _sprites.Add(new SpriteInfo("mikey1", 2923, 3, 11));
            _sprites.Add(new SpriteInfo("mikey2", 2956, 3, 11));
            _sprites.Add(new SpriteInfo("mikey3", 2989, 3, 11));
            _sprites.Add(new SpriteInfo("mikey4", 3022, 3, 11));
            _sprites.Add(new SpriteInfo("mikey5", 3055, 3, 11));
            _sprites.Add(new SpriteInfo("mikey6", 3088, 3, 11));
            _sprites.Add(new SpriteInfo("mikey7", 3121, 3, 11));
            _sprites.Add(new SpriteInfo("mikey8", 3154, 3, 11));
            _sprites.Add(new SpriteInfo("mikey9", 3187, 3, 11));
            _sprites.Add(new SpriteInfo("mikey10", 3220, 3, 11));
            _sprites.Add(new SpriteInfo("mikey11", 3253, 3, 11));
            _sprites.Add(new SpriteInfo("mikey12", 3286, 3, 11));
            _sprites.Add(new SpriteInfo("hulk1", 3357, 7, 16));
            _sprites.Add(new SpriteInfo("hulk2", 3469, 7, 16));
            _sprites.Add(new SpriteInfo("hulk3", 3581, 7, 16));
            _sprites.Add(new SpriteInfo("hulk4", 3693, 7, 16));
            _sprites.Add(new SpriteInfo("hulk5", 3805, 7, 16));
            _sprites.Add(new SpriteInfo("hulk6", 3917, 7, 16));
            _sprites.Add(new SpriteInfo("hulk7", 4029, 7, 16));
            _sprites.Add(new SpriteInfo("hulk8", 4141, 7, 16));
            _sprites.Add(new SpriteInfo("hulk9", 4253, 7, 16));
            _sprites.Add(new SpriteInfo("sphereoid1", 5394, 8, 15));
            _sprites.Add(new SpriteInfo("sphereoid2", 5514, 8, 15));
            _sprites.Add(new SpriteInfo("sphereoid3", 5634, 8, 15));
            _sprites.Add(new SpriteInfo("sphereoid4", 5754, 8, 15));
            _sprites.Add(new SpriteInfo("sphereoid5", 5874, 8, 15));
            _sprites.Add(new SpriteInfo("sphereoid6", 5994, 8, 15));
            _sprites.Add(new SpriteInfo("sphereoid7", 6114, 8, 15));
            _sprites.Add(new SpriteInfo("sphereoid8", 6234, 8, 15));
            _sprites.Add(new SpriteInfo("enforcer1", 6378, 5, 11));
            _sprites.Add(new SpriteInfo("enforcer2", 6433, 5, 11));
            _sprites.Add(new SpriteInfo("enforcer3", 6488, 5, 11));
            _sprites.Add(new SpriteInfo("enforcer4", 6543, 5, 11));
            _sprites.Add(new SpriteInfo("enforcer5", 6598, 5, 11));
            _sprites.Add(new SpriteInfo("enforcer6", 6653, 5, 11));
            _sprites.Add(new SpriteInfo("enforcerbullet1", 6724, 4, 7));
            _sprites.Add(new SpriteInfo("enforcerbullet2", 6752, 4, 7));
            _sprites.Add(new SpriteInfo("enforcerbullet3", 6780, 4, 7));
            _sprites.Add(new SpriteInfo("enforcerbullet4", 6808, 4, 7));
            _sprites.Add(new SpriteInfo("player", 8044, 6, 16));
            _sprites.Add(new SpriteInfo("brain1", 8561, 7, 16));
            _sprites.Add(new SpriteInfo("brain2", 8673, 7, 16));
            _sprites.Add(new SpriteInfo("brain3", 8785, 7, 16));
            _sprites.Add(new SpriteInfo("brain4", 8897, 7, 16));
            _sprites.Add(new SpriteInfo("brain5", 9009, 7, 16));
            _sprites.Add(new SpriteInfo("brain6", 9121, 7, 16));
            _sprites.Add(new SpriteInfo("brain7", 9233, 7, 16));
            _sprites.Add(new SpriteInfo("brain8", 9345, 7, 16));
            _sprites.Add(new SpriteInfo("brain9", 9457, 7, 16));
            _sprites.Add(new SpriteInfo("brain10", 9569, 7, 16));
            _sprites.Add(new SpriteInfo("brain11", 9681, 7, 16));
            _sprites.Add(new SpriteInfo("brain12", 9793, 7, 16));
            _sprites.Add(new SpriteInfo("player1", 13851, 4, 12));
            _sprites.Add(new SpriteInfo("player2", 13899, 4, 12));
            _sprites.Add(new SpriteInfo("player3", 13947, 4, 12));
            _sprites.Add(new SpriteInfo("player4", 13995, 4, 12));
            _sprites.Add(new SpriteInfo("player5", 14043, 4, 12));
            _sprites.Add(new SpriteInfo("player6", 14091, 4, 12));
            _sprites.Add(new SpriteInfo("player7", 14139, 4, 12));
            _sprites.Add(new SpriteInfo("player8", 14187, 4, 12));
            _sprites.Add(new SpriteInfo("player9", 14235, 4, 12));
            _sprites.Add(new SpriteInfo("player10", 14283, 4, 12));
            _sprites.Add(new SpriteInfo("player11", 14331, 4, 12));
            _sprites.Add(new SpriteInfo("player12", 14379, 4, 12));
            _sprites.Add(new SpriteInfo("electrode1", 15253, 5, 9));
            _sprites.Add(new SpriteInfo("electrode2", 15298, 5, 9));
            _sprites.Add(new SpriteInfo("electrode3", 15343, 5, 9));
            _sprites.Add(new SpriteInfo("electrode4", 15388, 5, 9));
            _sprites.Add(new SpriteInfo("electrode5", 15433, 5, 9));
            _sprites.Add(new SpriteInfo("electrode6", 15478, 5, 9));
            _sprites.Add(new SpriteInfo("electrode7", 15523, 5, 9));
            _sprites.Add(new SpriteInfo("electrode8", 15568, 5, 9));
            _sprites.Add(new SpriteInfo("electrode9", 15613, 5, 9));
            _sprites.Add(new SpriteInfo("electrode10", 15658, 5, 9));
            _sprites.Add(new SpriteInfo("electrode11", 15703, 5, 9));
            _sprites.Add(new SpriteInfo("electrode12", 15748, 5, 9));
            _sprites.Add(new SpriteInfo("electrode13", 15793, 3, 9));
            _sprites.Add(new SpriteInfo("electrode14", 15820, 3, 9));
            _sprites.Add(new SpriteInfo("electrode15", 15847, 3, 9));
            _sprites.Add(new SpriteInfo("electrode16", 15874, 5, 9));
            _sprites.Add(new SpriteInfo("electrode17", 15919, 5, 9));
            _sprites.Add(new SpriteInfo("electrode18", 15964, 5, 9));
            _sprites.Add(new SpriteInfo("electrode19", 16009, 9, 7));
            _sprites.Add(new SpriteInfo("electrode20", 16072, 9, 7));
            _sprites.Add(new SpriteInfo("electrode21", 16135, 9, 7));
            _sprites.Add(new SpriteInfo("electrode22", 16198, 5, 9));
            _sprites.Add(new SpriteInfo("electrode23", 16243, 5, 9));
            _sprites.Add(new SpriteInfo("electrode24", 16288, 5, 9));
            _sprites.Add(new SpriteInfo("electrode25", 16333, 5, 10));
            _sprites.Add(new SpriteInfo("electrode26", 16383, 5, 10));
            _sprites.Add(new SpriteInfo("electrode27", 16433, 5, 10));
            _sprites.Add(new SpriteInfo("grunt1", 16499, 5, 13));
            _sprites.Add(new SpriteInfo("grunt2", 16564, 5, 13));
            _sprites.Add(new SpriteInfo("grunt3", 16629, 5, 13));
            _sprites.Add(new SpriteInfo("quark1", 20710, 8, 15));
            _sprites.Add(new SpriteInfo("quark2", 20830, 8, 15));
            _sprites.Add(new SpriteInfo("quark3", 20950, 8, 15));
            _sprites.Add(new SpriteInfo("quark4", 21070, 8, 15));
            _sprites.Add(new SpriteInfo("quark5", 21190, 8, 15));
            _sprites.Add(new SpriteInfo("quark6", 21310, 8, 15));
            _sprites.Add(new SpriteInfo("quark7", 21430, 8, 15));
            _sprites.Add(new SpriteInfo("quark8", 21550, 8, 15));
            _sprites.Add(new SpriteInfo("quark9", 21670, 8, 15));
            _sprites.Add(new SpriteInfo("tank1", 21790, 7, 16));
            _sprites.Add(new SpriteInfo("tank2", 21902, 7, 16));
            _sprites.Add(new SpriteInfo("tank3", 22014, 7, 16));
            _sprites.Add(new SpriteInfo("tank4", 22126, 7, 16));
            _sprites.Add(new SpriteInfo("smallfont0", 59947, 2, 5));
            _sprites.Add(new SpriteInfo("smallfont1", 59958, 2, 5));
            _sprites.Add(new SpriteInfo("smallfont2", 59969, 2, 5));
            _sprites.Add(new SpriteInfo("smallfont3", 59980, 2, 5));
            _sprites.Add(new SpriteInfo("smallfont4", 59991, 2, 5));
            _sprites.Add(new SpriteInfo("smallfont5", 60002, 2, 5));
            _sprites.Add(new SpriteInfo("smallfont6", 60013, 2, 5));
            _sprites.Add(new SpriteInfo("smallfont7", 60024, 2, 5));
            _sprites.Add(new SpriteInfo("smallfont8", 60035, 2, 5));
            _sprites.Add(new SpriteInfo("smallfont9", 60046, 2, 5));
            _sprites.Add(new SpriteInfo("smallfontA", 60119, 2, 5));
            _sprites.Add(new SpriteInfo("smallfontB", 60130, 2, 5));
            _sprites.Add(new SpriteInfo("smallfontC", 60141, 2, 5));
            _sprites.Add(new SpriteInfo("smallfontD", 60152, 2, 5));
            _sprites.Add(new SpriteInfo("smallfontE", 60163, 2, 5));
            _sprites.Add(new SpriteInfo("smallfontF", 60174, 2, 5));
            _sprites.Add(new SpriteInfo("smallfontG", 60185, 2, 5));
            _sprites.Add(new SpriteInfo("smallfontH", 60196, 2, 5));
            _sprites.Add(new SpriteInfo("smallfontI", 60207, 2, 5));
            _sprites.Add(new SpriteInfo("smallfontJ", 60218, 2, 5));
            _sprites.Add(new SpriteInfo("smallfontK", 60229, 2, 5));
            _sprites.Add(new SpriteInfo("smallfontL", 60240, 2, 5));
            _sprites.Add(new SpriteInfo("smallfontM", 60251, 3, 5));
            _sprites.Add(new SpriteInfo("smallfontN", 60267, 2, 5));
            _sprites.Add(new SpriteInfo("smallfontO", 60278, 2, 5));
            _sprites.Add(new SpriteInfo("smallfontP", 60289, 2, 5));
            _sprites.Add(new SpriteInfo("smallfontQ", 60300, 2, 5));
            _sprites.Add(new SpriteInfo("smallfontR", 60311, 2, 5));
            _sprites.Add(new SpriteInfo("smallfontS", 60322, 2, 5));
            _sprites.Add(new SpriteInfo("smallfontT", 60333, 2, 5));
            _sprites.Add(new SpriteInfo("smallfontU", 60344, 2, 5));
            _sprites.Add(new SpriteInfo("smallfontV", 60355, 2, 5));
            _sprites.Add(new SpriteInfo("smallfontW", 60366, 3, 5));
            _sprites.Add(new SpriteInfo("smallfontX", 60382, 2, 5));
            _sprites.Add(new SpriteInfo("smallfontY", 60393, 2, 5));
            _sprites.Add(new SpriteInfo("smallfontZ", 60404, 2, 5));
            _sprites.Add(new SpriteInfo("smallfont(", 60415, 2, 5));
            _sprites.Add(new SpriteInfo("smallfont)", 60426, 2, 5));
            _sprites.Add(new SpriteInfo("largefont0", 60563, 3, 6));
            _sprites.Add(new SpriteInfo("largefont1", 60582, 3, 6));
            _sprites.Add(new SpriteInfo("largefont2", 60601, 3, 6));
            _sprites.Add(new SpriteInfo("largefont3", 60620, 3, 6));
            _sprites.Add(new SpriteInfo("largefont4", 60639, 3, 6));
            _sprites.Add(new SpriteInfo("largefont5", 60658, 3, 6));
            _sprites.Add(new SpriteInfo("largefont6", 60677, 3, 6));
            _sprites.Add(new SpriteInfo("largefont7", 60696, 3, 6));
            _sprites.Add(new SpriteInfo("largefont8", 60715, 3, 6));
            _sprites.Add(new SpriteInfo("largefont9", 60734, 3, 6));
            _sprites.Add(new SpriteInfo("largefontA", 60864, 3, 6));
            _sprites.Add(new SpriteInfo("largefontB", 60883, 3, 6));
            _sprites.Add(new SpriteInfo("largefontC", 60902, 3, 6));
            _sprites.Add(new SpriteInfo("largefontD", 60921, 3, 6));
            _sprites.Add(new SpriteInfo("largefontE", 60940, 3, 6));
            _sprites.Add(new SpriteInfo("largefontF", 60959, 3, 6));
            _sprites.Add(new SpriteInfo("largefontG", 60978, 3, 6));
            _sprites.Add(new SpriteInfo("largefontH", 60997, 3, 6));
            _sprites.Add(new SpriteInfo("largefontI", 61016, 3, 6));
            _sprites.Add(new SpriteInfo("largefontJ", 61035, 3, 6));
            _sprites.Add(new SpriteInfo("largefontK", 61054, 3, 6));
            _sprites.Add(new SpriteInfo("largefontL", 61073, 3, 6));
            _sprites.Add(new SpriteInfo("largefontM", 61092, 3, 6));
            _sprites.Add(new SpriteInfo("largefontN", 61111, 3, 6));
            _sprites.Add(new SpriteInfo("largefontO", 61130, 3, 6));
            _sprites.Add(new SpriteInfo("largefontP", 61149, 3, 6));
            _sprites.Add(new SpriteInfo("largefontQ", 61168, 3, 6));
            _sprites.Add(new SpriteInfo("largefontR", 61187, 3, 6));
            _sprites.Add(new SpriteInfo("largefontS", 61206, 3, 6));
            _sprites.Add(new SpriteInfo("largefontT", 61225, 3, 6));
            _sprites.Add(new SpriteInfo("largefontU", 61244, 3, 6));
            _sprites.Add(new SpriteInfo("largefontV", 61263, 3, 6));
            _sprites.Add(new SpriteInfo("largefontW", 61282, 3, 6));
            _sprites.Add(new SpriteInfo("largefontX", 61301, 3, 6));
            _sprites.Add(new SpriteInfo("largefontY", 61320, 3, 6));
            _sprites.Add(new SpriteInfo("largefontZ", 61339, 3, 6));
            _sprites.Add(new SpriteInfo("largefont(", 61358, 2, 6));
            _sprites.Add(new SpriteInfo("largefont)", 61371, 2, 6));
            _sprites.Add(new SpriteInfo("largefont:", 61383, 1, 5));
            _sprites.Add(new SpriteInfo("largefontarrowleft", 61403, 3, 6));
        }
    }
}

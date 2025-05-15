using WmsGfxSpriteEditor.Sprites;

namespace WmsGfxSpriteEditor.ROMs.Robotron.Shared
{
    /// <summary>
    /// Repository for Robotron Blue Label and Tie-Die sprite data.
    /// Provides access to sprite information from the ROM files.
    /// </summary>
    public class RobotronBlueLabelSpriteRepository : ISpriteRepository
    {
        private const int BitsPerPixel = 4;

        private readonly List<SpriteInfo> _sprites = new();

        /// <summary>
        /// Gets the total number of sprites available in the repository.
        /// </summary>
        public int Count => _sprites.Count;

        /// <summary>
        /// Initializes a new instance of the <see cref="RobotronBlueLabelSpriteRepository"/> class.
        /// Automatically loads the predefined Robotron sprite data upon construction.
        /// </summary>
        public RobotronBlueLabelSpriteRepository()
        {
            Load();
        }

        /// <summary>
        /// Gets all available sprites in the repository.
        /// </summary>
        /// <returns>An immutable collection of all sprite information.</returns>
        public IReadOnlyCollection<SpriteInfo> GetAllSprites() => _sprites.AsReadOnly();

        /// <summary>
        /// Retrieves sprite information for a specific index in the repository.
        /// </summary>
        /// <param name="index">The zero-based index of the sprite to retrieve.</param>
        /// <returns>
        /// The <see cref="SpriteInfo"/> object if the index is valid; otherwise, <c>null</c>.
        /// </returns>
        public SpriteInfo? GetSpriteInfoByIndex(int index) => index >= 0 && index < _sprites.Count ? _sprites[index] : null;

        /// <summary>
        /// Populates the repository with predefined Robotron sprite data.
        /// Clears any existing sprites and adds all Robotron sprites with their ROM offsets and dimensions.
        /// </summary>
        /// <remarks>
        /// Sprite data is sourced from https://www.seanriddle.com/robotronsprites.txt
        /// Each sprite entry contains:
        /// - Descriptive name
        /// - Memory offset in the ROM
        /// - Width in bytes (each byte contains 2 pixels)
        /// - Height in pixels
        /// - Bits Per Pixel
        /// All sprites default to linear storage format.
        /// </remarks>
        public void Load()
        {
            _sprites.Clear();

            // Data sourced from https://www.seanriddle.com/robotronsprites.txt
            // Format: new SpriteInfo(name, offset, widthInBytes, height)
            // All sprites use default linear format (isLinear = true)
            _sprites.Add(new("familydeath", 1083, 6, 11, BitsPerPixel));
            _sprites.Add(new("1000", 1177, 6, 5, BitsPerPixel));
            _sprites.Add(new("2000", 1207, 6, 5, BitsPerPixel));
            _sprites.Add(new("3000", 1237, 6, 5, BitsPerPixel));
            _sprites.Add(new("4000", 1267, 6, 5, BitsPerPixel));
            _sprites.Add(new("5000", 1297, 6, 5, BitsPerPixel));
            _sprites.Add(new("mommy1", 1375, 4, 14, BitsPerPixel));
            _sprites.Add(new("mommy2", 1431, 4, 14, BitsPerPixel));
            _sprites.Add(new("mommy3", 1487, 4, 14, BitsPerPixel));
            _sprites.Add(new("mommy4", 1543, 4, 14, BitsPerPixel));
            _sprites.Add(new("mommy5", 1599, 4, 14, BitsPerPixel));
            _sprites.Add(new("mommy6", 1655, 4, 14, BitsPerPixel));
            _sprites.Add(new("mommy7", 1711, 4, 14, BitsPerPixel));
            _sprites.Add(new("mommy8", 1767, 4, 14, BitsPerPixel));
            _sprites.Add(new("mommy9", 1823, 4, 14, BitsPerPixel));
            _sprites.Add(new("mommy10", 1879, 4, 14, BitsPerPixel));
            _sprites.Add(new("mommy11", 1935, 4, 14, BitsPerPixel));
            _sprites.Add(new("mommy12", 1991, 4, 14, BitsPerPixel));
            _sprites.Add(new("daddy1", 2095, 5, 13, BitsPerPixel));
            _sprites.Add(new("daddy2", 2160, 5, 13, BitsPerPixel));
            _sprites.Add(new("daddy3", 2225, 5, 13, BitsPerPixel));
            _sprites.Add(new("daddy4", 2290, 5, 13, BitsPerPixel));
            _sprites.Add(new("daddy5", 2355, 5, 13, BitsPerPixel));
            _sprites.Add(new("daddy6", 2420, 5, 13, BitsPerPixel));
            _sprites.Add(new("daddy7", 2485, 5, 13, BitsPerPixel));
            _sprites.Add(new("daddy8", 2550, 5, 13, BitsPerPixel));
            _sprites.Add(new("daddy9", 2615, 5, 13, BitsPerPixel));
            _sprites.Add(new("daddy10", 2680, 5, 13, BitsPerPixel));
            _sprites.Add(new("daddy11", 2745, 5, 13, BitsPerPixel));
            _sprites.Add(new("daddy12", 2810, 5, 13, BitsPerPixel));
            _sprites.Add(new("mikey1", 2923, 3, 11, BitsPerPixel));
            _sprites.Add(new("mikey2", 2956, 3, 11, BitsPerPixel));
            _sprites.Add(new("mikey3", 2989, 3, 11, BitsPerPixel));
            _sprites.Add(new("mikey4", 3022, 3, 11, BitsPerPixel));
            _sprites.Add(new("mikey5", 3055, 3, 11, BitsPerPixel));
            _sprites.Add(new("mikey6", 3088, 3, 11, BitsPerPixel));
            _sprites.Add(new("mikey7", 3121, 3, 11, BitsPerPixel));
            _sprites.Add(new("mikey8", 3154, 3, 11, BitsPerPixel));
            _sprites.Add(new("mikey9", 3187, 3, 11, BitsPerPixel));
            _sprites.Add(new("mikey10", 3220, 3, 11, BitsPerPixel));
            _sprites.Add(new("mikey11", 3253, 3, 11, BitsPerPixel));
            _sprites.Add(new("mikey12", 3286, 3, 11, BitsPerPixel));
            _sprites.Add(new("hulk1", 3357, 7, 16, BitsPerPixel));
            _sprites.Add(new("hulk2", 3469, 7, 16, BitsPerPixel));
            _sprites.Add(new("hulk3", 3581, 7, 16, BitsPerPixel));
            _sprites.Add(new("hulk4", 3693, 7, 16, BitsPerPixel));
            _sprites.Add(new("hulk5", 3805, 7, 16, BitsPerPixel));
            _sprites.Add(new("hulk6", 3917, 7, 16, BitsPerPixel));
            _sprites.Add(new("hulk7", 4029, 7, 16, BitsPerPixel));
            _sprites.Add(new("hulk8", 4141, 7, 16, BitsPerPixel));
            _sprites.Add(new("hulk9", 4253, 7, 16, BitsPerPixel));
            _sprites.Add(new("sphereoid1", 5394, 8, 15, BitsPerPixel));
            _sprites.Add(new("sphereoid2", 5514, 8, 15, BitsPerPixel));
            _sprites.Add(new("sphereoid3", 5634, 8, 15, BitsPerPixel));
            _sprites.Add(new("sphereoid4", 5754, 8, 15, BitsPerPixel));
            _sprites.Add(new("sphereoid5", 5874, 8, 15, BitsPerPixel));
            _sprites.Add(new("sphereoid6", 5994, 8, 15, BitsPerPixel));
            _sprites.Add(new("sphereoid7", 6114, 8, 15, BitsPerPixel));
            _sprites.Add(new("sphereoid8", 6234, 8, 15, BitsPerPixel));
            _sprites.Add(new("enforcer1", 6378, 5, 11, BitsPerPixel));
            _sprites.Add(new("enforcer2", 6433, 5, 11, BitsPerPixel));
            _sprites.Add(new("enforcer3", 6488, 5, 11, BitsPerPixel));
            _sprites.Add(new("enforcer4", 6543, 5, 11, BitsPerPixel));
            _sprites.Add(new("enforcer5", 6598, 5, 11, BitsPerPixel));
            _sprites.Add(new("enforcer6", 6653, 5, 11, BitsPerPixel));
            _sprites.Add(new("enforcerbullet1", 6724, 4, 7, BitsPerPixel));
            _sprites.Add(new("enforcerbullet2", 6752, 4, 7, BitsPerPixel));
            _sprites.Add(new("enforcerbullet3", 6780, 4, 7, BitsPerPixel));
            _sprites.Add(new("enforcerbullet4", 6808, 4, 7, BitsPerPixel));
            _sprites.Add(new("player", 8044, 6, 16, BitsPerPixel));
            _sprites.Add(new("brain1", 8561, 7, 16, BitsPerPixel));
            _sprites.Add(new("brain2", 8673, 7, 16, BitsPerPixel));
            _sprites.Add(new("brain3", 8785, 7, 16, BitsPerPixel));
            _sprites.Add(new("brain4", 8897, 7, 16, BitsPerPixel));
            _sprites.Add(new("brain5", 9009, 7, 16, BitsPerPixel));
            _sprites.Add(new("brain6", 9121, 7, 16, BitsPerPixel));
            _sprites.Add(new("brain7", 9233, 7, 16, BitsPerPixel));
            _sprites.Add(new("brain8", 9345, 7, 16, BitsPerPixel));
            _sprites.Add(new("brain9", 9457, 7, 16, BitsPerPixel));
            _sprites.Add(new("brain10", 9569, 7, 16, BitsPerPixel));
            _sprites.Add(new("brain11", 9681, 7, 16, BitsPerPixel));
            _sprites.Add(new("brain12", 9793, 7, 16, BitsPerPixel));
            _sprites.Add(new("player1", 13851, 4, 12, BitsPerPixel));
            _sprites.Add(new("player2", 13899, 4, 12, BitsPerPixel));
            _sprites.Add(new("player3", 13947, 4, 12, BitsPerPixel));
            _sprites.Add(new("player4", 13995, 4, 12, BitsPerPixel));
            _sprites.Add(new("player5", 14043, 4, 12, BitsPerPixel));
            _sprites.Add(new("player6", 14091, 4, 12, BitsPerPixel));
            _sprites.Add(new("player7", 14139, 4, 12, BitsPerPixel));
            _sprites.Add(new("player8", 14187, 4, 12, BitsPerPixel));
            _sprites.Add(new("player9", 14235, 4, 12, BitsPerPixel));
            _sprites.Add(new("player10", 14283, 4, 12, BitsPerPixel));
            _sprites.Add(new("player11", 14331, 4, 12, BitsPerPixel));
            _sprites.Add(new("player12", 14379, 4, 12, BitsPerPixel));
            _sprites.Add(new("electrode1", 15253, 5, 9, BitsPerPixel));
            _sprites.Add(new("electrode2", 15298, 5, 9, BitsPerPixel));
            _sprites.Add(new("electrode3", 15343, 5, 9, BitsPerPixel));
            _sprites.Add(new("electrode4", 15388, 5, 9, BitsPerPixel));
            _sprites.Add(new("electrode5", 15433, 5, 9, BitsPerPixel));
            _sprites.Add(new("electrode6", 15478, 5, 9, BitsPerPixel));
            _sprites.Add(new("electrode7", 15523, 5, 9, BitsPerPixel));
            _sprites.Add(new("electrode8", 15568, 5, 9, BitsPerPixel));
            _sprites.Add(new("electrode9", 15613, 5, 9, BitsPerPixel));
            _sprites.Add(new("electrode10", 15658, 5, 9, BitsPerPixel));
            _sprites.Add(new("electrode11", 15703, 5, 9, BitsPerPixel));
            _sprites.Add(new("electrode12", 15748, 5, 9, BitsPerPixel));
            _sprites.Add(new("electrode13", 15793, 3, 9, BitsPerPixel));
            _sprites.Add(new("electrode14", 15820, 3, 9, BitsPerPixel));
            _sprites.Add(new("electrode15", 15847, 3, 9, BitsPerPixel));
            _sprites.Add(new("electrode16", 15874, 5, 9, BitsPerPixel));
            _sprites.Add(new("electrode17", 15919, 5, 9, BitsPerPixel));
            _sprites.Add(new("electrode18", 15964, 5, 9, BitsPerPixel));
            _sprites.Add(new("electrode19", 16009, 9, 7, BitsPerPixel));
            _sprites.Add(new("electrode20", 16072, 9, 7, BitsPerPixel));
            _sprites.Add(new("electrode21", 16135, 9, 7, BitsPerPixel));
            _sprites.Add(new("electrode22", 16198, 5, 9, BitsPerPixel));
            _sprites.Add(new("electrode23", 16243, 5, 9, BitsPerPixel));
            _sprites.Add(new("electrode24", 16288, 5, 9, BitsPerPixel));
            _sprites.Add(new("electrode25", 16333, 5, 10, BitsPerPixel));
            _sprites.Add(new("electrode26", 16383, 5, 10, BitsPerPixel));
            _sprites.Add(new("electrode27", 16433, 5, 10, BitsPerPixel));
            _sprites.Add(new("grunt1", 16499, 5, 13, BitsPerPixel));
            _sprites.Add(new("grunt2", 16564, 5, 13, BitsPerPixel));
            _sprites.Add(new("grunt3", 16629, 5, 13, BitsPerPixel));
            _sprites.Add(new("quark1", 20710, 8, 15, BitsPerPixel));
            _sprites.Add(new("quark2", 20830, 8, 15, BitsPerPixel));
            _sprites.Add(new("quark3", 20950, 8, 15, BitsPerPixel));
            _sprites.Add(new("quark4", 21070, 8, 15, BitsPerPixel));
            _sprites.Add(new("quark5", 21190, 8, 15, BitsPerPixel));
            _sprites.Add(new("quark6", 21310, 8, 15, BitsPerPixel));
            _sprites.Add(new("quark7", 21430, 8, 15, BitsPerPixel));
            _sprites.Add(new("quark8", 21550, 8, 15, BitsPerPixel));
            _sprites.Add(new("quark9", 21670, 8, 15, BitsPerPixel));
            _sprites.Add(new("tank1", 21790, 7, 16, BitsPerPixel));
            _sprites.Add(new("tank2", 21902, 7, 16, BitsPerPixel));
            _sprites.Add(new("tank3", 22014, 7, 16, BitsPerPixel));
            _sprites.Add(new("tank4", 22126, 7, 16, BitsPerPixel));
            _sprites.Add(new("smallfont0", 59947, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfont1", 59958, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfont2", 59969, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfont3", 59980, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfont4", 59991, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfont5", 60002, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfont6", 60013, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfont7", 60024, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfont8", 60035, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfont9", 60046, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfontA", 60119, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfontB", 60130, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfontC", 60141, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfontD", 60152, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfontE", 60163, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfontF", 60174, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfontG", 60185, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfontH", 60196, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfontI", 60207, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfontJ", 60218, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfontK", 60229, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfontL", 60240, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfontM", 60251, 3, 5, BitsPerPixel));
            _sprites.Add(new("smallfontN", 60267, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfontO", 60278, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfontP", 60289, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfontQ", 60300, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfontR", 60311, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfontS", 60322, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfontT", 60333, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfontU", 60344, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfontV", 60355, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfontW", 60366, 3, 5, BitsPerPixel));
            _sprites.Add(new("smallfontX", 60382, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfontY", 60393, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfontZ", 60404, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfont(", 60415, 2, 5, BitsPerPixel));
            _sprites.Add(new("smallfont)", 60426, 2, 5, BitsPerPixel));
            _sprites.Add(new("largefont0", 60563, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefont1", 60582, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefont2", 60601, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefont3", 60620, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefont4", 60639, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefont5", 60658, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefont6", 60677, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefont7", 60696, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefont8", 60715, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefont9", 60734, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefontA", 60864, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefontB", 60883, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefontC", 60902, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefontD", 60921, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefontE", 60940, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefontF", 60959, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefontG", 60978, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefontH", 60997, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefontI", 61016, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefontJ", 61035, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefontK", 61054, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefontL", 61073, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefontM", 61092, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefontN", 61111, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefontO", 61130, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefontP", 61149, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefontQ", 61168, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefontR", 61187, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefontS", 61206, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefontT", 61225, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefontU", 61244, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefontV", 61263, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefontW", 61282, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefontX", 61301, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefontY", 61320, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefontZ", 61339, 3, 6, BitsPerPixel));
            _sprites.Add(new("largefont(", 61358, 2, 6, BitsPerPixel));
            _sprites.Add(new("largefont)", 61371, 2, 6, BitsPerPixel));
            _sprites.Add(new("largefont:", 61383, 1, 5, BitsPerPixel));
            _sprites.Add(new("largefontarrowleft", 61403, 3, 6, BitsPerPixel));
        }
    }
}
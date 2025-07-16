using WmsGfxSpriteEditor.Sprites;

namespace WmsGfxSpriteEditor.Roms.Robotron2084.Shared.Sprites
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
        /// Initializes a new instance of the <see cref="RobotronBlueLabelSpriteRepository"/> class.
        /// Automatically loads the predefined Robotron sprite data upon construction.
        /// </summary>
        public RobotronBlueLabelSpriteRepository()
        {
            Load();
        }

        /// <summary>
        /// Gets the total number of sprites available in the repository.
        /// </summary>
        public int Count => _sprites.Count;

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
            // Format: new SpriteInfo(index, name, offset, widthInBytes, height)
            // All sprites use default linear format (isLinear = true)
            _sprites.Add(new(0, "familydeath", 1083, 6, 11, BitsPerPixel));
            _sprites.Add(new(1, "1000", 1177, 6, 5, BitsPerPixel));
            _sprites.Add(new(2, "2000", 1207, 6, 5, BitsPerPixel));
            _sprites.Add(new(3, "3000", 1237, 6, 5, BitsPerPixel));
            _sprites.Add(new(4, "4000", 1267, 6, 5, BitsPerPixel));
            _sprites.Add(new(5, "5000", 1297, 6, 5, BitsPerPixel));
            _sprites.Add(new(6, "mommy1", 1375, 4, 14, BitsPerPixel));
            _sprites.Add(new(7, "mommy2", 1431, 4, 14, BitsPerPixel));
            _sprites.Add(new(8, "mommy3", 1487, 4, 14, BitsPerPixel));
            _sprites.Add(new(9, "mommy4", 1543, 4, 14, BitsPerPixel));
            _sprites.Add(new(10, "mommy5", 1599, 4, 14, BitsPerPixel));
            _sprites.Add(new(11, "mommy6", 1655, 4, 14, BitsPerPixel));
            _sprites.Add(new(12, "mommy7", 1711, 4, 14, BitsPerPixel));
            _sprites.Add(new(13, "mommy8", 1767, 4, 14, BitsPerPixel));
            _sprites.Add(new(14, "mommy9", 1823, 4, 14, BitsPerPixel));
            _sprites.Add(new(15, "mommy10", 1879, 4, 14, BitsPerPixel));
            _sprites.Add(new(16, "mommy11", 1935, 4, 14, BitsPerPixel));
            _sprites.Add(new(17, "mommy12", 1991, 4, 14, BitsPerPixel));
            _sprites.Add(new(18, "daddy1", 2095, 5, 13, BitsPerPixel));
            _sprites.Add(new(19, "daddy2", 2160, 5, 13, BitsPerPixel));
            _sprites.Add(new(20, "daddy3", 2225, 5, 13, BitsPerPixel));
            _sprites.Add(new(21, "daddy4", 2290, 5, 13, BitsPerPixel));
            _sprites.Add(new(22, "daddy5", 2355, 5, 13, BitsPerPixel));
            _sprites.Add(new(23, "daddy6", 2420, 5, 13, BitsPerPixel));
            _sprites.Add(new(24, "daddy7", 2485, 5, 13, BitsPerPixel));
            _sprites.Add(new(25, "daddy8", 2550, 5, 13, BitsPerPixel));
            _sprites.Add(new(26, "daddy9", 2615, 5, 13, BitsPerPixel));
            _sprites.Add(new(27, "daddy10", 2680, 5, 13, BitsPerPixel));
            _sprites.Add(new(28, "daddy11", 2745, 5, 13, BitsPerPixel));
            _sprites.Add(new(29, "daddy12", 2810, 5, 13, BitsPerPixel));
            _sprites.Add(new(30, "mikey1", 2923, 3, 11, BitsPerPixel));
            _sprites.Add(new(31, "mikey2", 2956, 3, 11, BitsPerPixel));
            _sprites.Add(new(32, "mikey3", 2989, 3, 11, BitsPerPixel));
            _sprites.Add(new(33, "mikey4", 3022, 3, 11, BitsPerPixel));
            _sprites.Add(new(34, "mikey5", 3055, 3, 11, BitsPerPixel));
            _sprites.Add(new(35, "mikey6", 3088, 3, 11, BitsPerPixel));
            _sprites.Add(new(36, "mikey7", 3121, 3, 11, BitsPerPixel));
            _sprites.Add(new(37, "mikey8", 3154, 3, 11, BitsPerPixel));
            _sprites.Add(new(38, "mikey9", 3187, 3, 11, BitsPerPixel));
            _sprites.Add(new(39, "mikey10", 3220, 3, 11, BitsPerPixel));
            _sprites.Add(new(40, "mikey11", 3253, 3, 11, BitsPerPixel));
            _sprites.Add(new(41, "mikey12", 3286, 3, 11, BitsPerPixel));
            _sprites.Add(new(42, "hulk1", 3357, 7, 16, BitsPerPixel));
            _sprites.Add(new(43, "hulk2", 3469, 7, 16, BitsPerPixel));
            _sprites.Add(new(44, "hulk3", 3581, 7, 16, BitsPerPixel));
            _sprites.Add(new(45, "hulk4", 3693, 7, 16, BitsPerPixel));
            _sprites.Add(new(46, "hulk5", 3805, 7, 16, BitsPerPixel));
            _sprites.Add(new(47, "hulk6", 3917, 7, 16, BitsPerPixel));
            _sprites.Add(new(48, "hulk7", 4029, 7, 16, BitsPerPixel));
            _sprites.Add(new(49, "hulk8", 4141, 7, 16, BitsPerPixel));
            _sprites.Add(new(50, "hulk9", 4253, 7, 16, BitsPerPixel));
            _sprites.Add(new(51, "sphereoid1", 5394, 8, 15, BitsPerPixel));
            _sprites.Add(new(52, "sphereoid2", 5514, 8, 15, BitsPerPixel));
            _sprites.Add(new(53, "sphereoid3", 5634, 8, 15, BitsPerPixel));
            _sprites.Add(new(54, "sphereoid4", 5754, 8, 15, BitsPerPixel));
            _sprites.Add(new(55, "sphereoid5", 5874, 8, 15, BitsPerPixel));
            _sprites.Add(new(56, "sphereoid6", 5994, 8, 15, BitsPerPixel));
            _sprites.Add(new(57, "sphereoid7", 6114, 8, 15, BitsPerPixel));
            _sprites.Add(new(58, "sphereoid8", 6234, 8, 15, BitsPerPixel));
            _sprites.Add(new(59, "enforcer1", 6378, 5, 11, BitsPerPixel));
            _sprites.Add(new(60, "enforcer2", 6433, 5, 11, BitsPerPixel));
            _sprites.Add(new(61, "enforcer3", 6488, 5, 11, BitsPerPixel));
            _sprites.Add(new(62, "enforcer4", 6543, 5, 11, BitsPerPixel));
            _sprites.Add(new(63, "enforcer5", 6598, 5, 11, BitsPerPixel));
            _sprites.Add(new(64, "enforcer6", 6653, 5, 11, BitsPerPixel));
            _sprites.Add(new(65, "enforcerbullet1", 6724, 4, 7, BitsPerPixel));
            _sprites.Add(new(66, "enforcerbullet2", 6752, 4, 7, BitsPerPixel));
            _sprites.Add(new(67, "enforcerbullet3", 6780, 4, 7, BitsPerPixel));
            _sprites.Add(new(68, "enforcerbullet4", 6808, 4, 7, BitsPerPixel));
            _sprites.Add(new(69, "player", 8044, 6, 16, BitsPerPixel));
            _sprites.Add(new(70, "brain1", 8561, 7, 16, BitsPerPixel));
            _sprites.Add(new(71, "brain2", 8673, 7, 16, BitsPerPixel));
            _sprites.Add(new(72, "brain3", 8785, 7, 16, BitsPerPixel));
            _sprites.Add(new(73, "brain4", 8897, 7, 16, BitsPerPixel));
            _sprites.Add(new(74, "brain5", 9009, 7, 16, BitsPerPixel));
            _sprites.Add(new(75, "brain6", 9121, 7, 16, BitsPerPixel));
            _sprites.Add(new(76, "brain7", 9233, 7, 16, BitsPerPixel));
            _sprites.Add(new(77, "brain8", 9345, 7, 16, BitsPerPixel));
            _sprites.Add(new(78, "brain9", 9457, 7, 16, BitsPerPixel));
            _sprites.Add(new(79, "brain10", 9569, 7, 16, BitsPerPixel));
            _sprites.Add(new(80, "brain11", 9681, 7, 16, BitsPerPixel));
            _sprites.Add(new(81, "brain12", 9793, 7, 16, BitsPerPixel));
            _sprites.Add(new(82, "player1", 13851, 4, 12, BitsPerPixel));
            _sprites.Add(new(83, "player2", 13899, 4, 12, BitsPerPixel));
            _sprites.Add(new(84, "player3", 13947, 4, 12, BitsPerPixel));
            _sprites.Add(new(85, "player4", 13995, 4, 12, BitsPerPixel));
            _sprites.Add(new(86, "player5", 14043, 4, 12, BitsPerPixel));
            _sprites.Add(new(87, "player6", 14091, 4, 12, BitsPerPixel));
            _sprites.Add(new(88, "player7", 14139, 4, 12, BitsPerPixel));
            _sprites.Add(new(89, "player8", 14187, 4, 12, BitsPerPixel));
            _sprites.Add(new(90, "player9", 14235, 4, 12, BitsPerPixel));
            _sprites.Add(new(91, "player10", 14283, 4, 12, BitsPerPixel));
            _sprites.Add(new(92, "player11", 14331, 4, 12, BitsPerPixel));
            _sprites.Add(new(93, "player12", 14379, 4, 12, BitsPerPixel));
            _sprites.Add(new(94, "electrode1", 15253, 5, 9, BitsPerPixel));
            _sprites.Add(new(95, "electrode2", 15298, 5, 9, BitsPerPixel));
            _sprites.Add(new(96, "electrode3", 15343, 5, 9, BitsPerPixel));
            _sprites.Add(new(97, "electrode4", 15388, 5, 9, BitsPerPixel));
            _sprites.Add(new(98, "electrode5", 15433, 5, 9, BitsPerPixel));
            _sprites.Add(new(99, "electrode6", 15478, 5, 9, BitsPerPixel));
            _sprites.Add(new(100, "electrode7", 15523, 5, 9, BitsPerPixel));
            _sprites.Add(new(101, "electrode8", 15568, 5, 9, BitsPerPixel));
            _sprites.Add(new(102, "electrode9", 15613, 5, 9, BitsPerPixel));
            _sprites.Add(new(103, "electrode10", 15658, 5, 9, BitsPerPixel));
            _sprites.Add(new(104, "electrode11", 15703, 5, 9, BitsPerPixel));
            _sprites.Add(new(105, "electrode12", 15748, 5, 9, BitsPerPixel));
            _sprites.Add(new(106, "electrode13", 15793, 3, 9, BitsPerPixel));
            _sprites.Add(new(107, "electrode14", 15820, 3, 9, BitsPerPixel));
            _sprites.Add(new(108, "electrode15", 15847, 3, 9, BitsPerPixel));
            _sprites.Add(new(109, "electrode16", 15874, 5, 9, BitsPerPixel));
            _sprites.Add(new(110, "electrode17", 15919, 5, 9, BitsPerPixel));
            _sprites.Add(new(111, "electrode18", 15964, 5, 9, BitsPerPixel));
            _sprites.Add(new(112, "electrode19", 16009, 9, 7, BitsPerPixel));
            _sprites.Add(new(113, "electrode20", 16072, 9, 7, BitsPerPixel));
            _sprites.Add(new(114, "electrode21", 16135, 9, 7, BitsPerPixel));
            _sprites.Add(new(115, "electrode22", 16198, 5, 9, BitsPerPixel));
            _sprites.Add(new(116, "electrode23", 16243, 5, 9, BitsPerPixel));
            _sprites.Add(new(117, "electrode24", 16288, 5, 9, BitsPerPixel));
            _sprites.Add(new(118, "electrode25", 16333, 5, 10, BitsPerPixel));
            _sprites.Add(new(119, "electrode26", 16383, 5, 10, BitsPerPixel));
            _sprites.Add(new(120, "electrode27", 16433, 5, 10, BitsPerPixel));
            _sprites.Add(new(121, "grunt1", 16499, 5, 13, BitsPerPixel));
            _sprites.Add(new(122, "grunt2", 16564, 5, 13, BitsPerPixel));
            _sprites.Add(new(123, "grunt3", 16629, 5, 13, BitsPerPixel));
            _sprites.Add(new(124, "quark1", 20710, 8, 15, BitsPerPixel));
            _sprites.Add(new(125, "quark2", 20830, 8, 15, BitsPerPixel));
            _sprites.Add(new(126, "quark3", 20950, 8, 15, BitsPerPixel));
            _sprites.Add(new(127, "quark4", 21070, 8, 15, BitsPerPixel));
            _sprites.Add(new(128, "quark5", 21190, 8, 15, BitsPerPixel));
            _sprites.Add(new(129, "quark6", 21310, 8, 15, BitsPerPixel));
            _sprites.Add(new(130, "quark7", 21430, 8, 15, BitsPerPixel));
            _sprites.Add(new(131, "quark8", 21550, 8, 15, BitsPerPixel));
            _sprites.Add(new(132, "quark9", 21670, 8, 15, BitsPerPixel));
            _sprites.Add(new(133, "tank1", 21790, 7, 16, BitsPerPixel));
            _sprites.Add(new(134, "tank2", 21902, 7, 16, BitsPerPixel));
            _sprites.Add(new(135, "tank3", 22014, 7, 16, BitsPerPixel));
            _sprites.Add(new(136, "tank4", 22126, 7, 16, BitsPerPixel));
            _sprites.Add(new(137, "smallfont0", 59947, 2, 5, BitsPerPixel));
            _sprites.Add(new(138, "smallfont1", 59958, 2, 5, BitsPerPixel));
            _sprites.Add(new(139, "smallfont2", 59969, 2, 5, BitsPerPixel));
            _sprites.Add(new(140, "smallfont3", 59980, 2, 5, BitsPerPixel));
            _sprites.Add(new(141, "smallfont4", 59991, 2, 5, BitsPerPixel));
            _sprites.Add(new(142, "smallfont5", 60002, 2, 5, BitsPerPixel));
            _sprites.Add(new(143, "smallfont6", 60013, 2, 5, BitsPerPixel));
            _sprites.Add(new(144, "smallfont7", 60024, 2, 5, BitsPerPixel));
            _sprites.Add(new(145, "smallfont8", 60035, 2, 5, BitsPerPixel));
            _sprites.Add(new(146, "smallfont9", 60046, 2, 5, BitsPerPixel));
            _sprites.Add(new(147, "smallfontA", 60119, 2, 5, BitsPerPixel));
            _sprites.Add(new(148, "smallfontB", 60130, 2, 5, BitsPerPixel));
            _sprites.Add(new(149, "smallfontC", 60141, 2, 5, BitsPerPixel));
            _sprites.Add(new(150, "smallfontD", 60152, 2, 5, BitsPerPixel));
            _sprites.Add(new(151, "smallfontE", 60163, 2, 5, BitsPerPixel));
            _sprites.Add(new(152, "smallfontF", 60174, 2, 5, BitsPerPixel));
            _sprites.Add(new(153, "smallfontG", 60185, 2, 5, BitsPerPixel));
            _sprites.Add(new(154, "smallfontH", 60196, 2, 5, BitsPerPixel));
            _sprites.Add(new(155, "smallfontI", 60207, 2, 5, BitsPerPixel));
            _sprites.Add(new(156, "smallfontJ", 60218, 2, 5, BitsPerPixel));
            _sprites.Add(new(157, "smallfontK", 60229, 2, 5, BitsPerPixel));
            _sprites.Add(new(158, "smallfontL", 60240, 2, 5, BitsPerPixel));
            _sprites.Add(new(159, "smallfontM", 60251, 3, 5, BitsPerPixel));
            _sprites.Add(new(160, "smallfontN", 60267, 2, 5, BitsPerPixel));
            _sprites.Add(new(161, "smallfontO", 60278, 2, 5, BitsPerPixel));
            _sprites.Add(new(162, "smallfontP", 60289, 2, 5, BitsPerPixel));
            _sprites.Add(new(163, "smallfontQ", 60300, 2, 5, BitsPerPixel));
            _sprites.Add(new(164, "smallfontR", 60311, 2, 5, BitsPerPixel));
            _sprites.Add(new(165, "smallfontS", 60322, 2, 5, BitsPerPixel));
            _sprites.Add(new(166, "smallfontT", 60333, 2, 5, BitsPerPixel));
            _sprites.Add(new(167, "smallfontU", 60344, 2, 5, BitsPerPixel));
            _sprites.Add(new(168, "smallfontV", 60355, 2, 5, BitsPerPixel));
            _sprites.Add(new(169, "smallfontW", 60366, 3, 5, BitsPerPixel));
            _sprites.Add(new(170, "smallfontX", 60382, 2, 5, BitsPerPixel));
            _sprites.Add(new(171, "smallfontY", 60393, 2, 5, BitsPerPixel));
            _sprites.Add(new(172, "smallfontZ", 60404, 2, 5, BitsPerPixel));
            _sprites.Add(new(173, "smallfont(", 60415, 2, 5, BitsPerPixel));
            _sprites.Add(new(174, "smallfont)", 60426, 2, 5, BitsPerPixel));
            _sprites.Add(new(175, "largefont0", 60563, 3, 6, BitsPerPixel));
            _sprites.Add(new(176, "largefont1", 60582, 3, 6, BitsPerPixel));
            _sprites.Add(new(177, "largefont2", 60601, 3, 6, BitsPerPixel));
            _sprites.Add(new(178, "largefont3", 60620, 3, 6, BitsPerPixel));
            _sprites.Add(new(179, "largefont4", 60639, 3, 6, BitsPerPixel));
            _sprites.Add(new(180, "largefont5", 60658, 3, 6, BitsPerPixel));
            _sprites.Add(new(181, "largefont6", 60677, 3, 6, BitsPerPixel));
            _sprites.Add(new(182, "largefont7", 60696, 3, 6, BitsPerPixel));
            _sprites.Add(new(183, "largefont8", 60715, 3, 6, BitsPerPixel));
            _sprites.Add(new(184, "largefont9", 60734, 3, 6, BitsPerPixel));
            _sprites.Add(new(185, "largefontA", 60864, 3, 6, BitsPerPixel));
            _sprites.Add(new(186, "largefontB", 60883, 3, 6, BitsPerPixel));
            _sprites.Add(new(187, "largefontC", 60902, 3, 6, BitsPerPixel));
            _sprites.Add(new(188, "largefontD", 60921, 3, 6, BitsPerPixel));
            _sprites.Add(new(189, "largefontE", 60940, 3, 6, BitsPerPixel));
            _sprites.Add(new(190, "largefontF", 60959, 3, 6, BitsPerPixel));
            _sprites.Add(new(191, "largefontG", 60978, 3, 6, BitsPerPixel));
            _sprites.Add(new(192, "largefontH", 60997, 3, 6, BitsPerPixel));
            _sprites.Add(new(193, "largefontI", 61016, 3, 6, BitsPerPixel));
            _sprites.Add(new(194, "largefontJ", 61035, 3, 6, BitsPerPixel));
            _sprites.Add(new(195, "largefontK", 61054, 3, 6, BitsPerPixel));
            _sprites.Add(new(196, "largefontL", 61073, 3, 6, BitsPerPixel));
            _sprites.Add(new(197, "largefontM", 61092, 3, 6, BitsPerPixel));
            _sprites.Add(new(198, "largefontN", 61111, 3, 6, BitsPerPixel));
            _sprites.Add(new(199, "largefontO", 61130, 3, 6, BitsPerPixel));
            _sprites.Add(new(200, "largefontP", 61149, 3, 6, BitsPerPixel));
            _sprites.Add(new(201, "largefontQ", 61168, 3, 6, BitsPerPixel));
            _sprites.Add(new(202, "largefontR", 61187, 3, 6, BitsPerPixel));
            _sprites.Add(new(203, "largefontS", 61206, 3, 6, BitsPerPixel));
            _sprites.Add(new(204, "largefontT", 61225, 3, 6, BitsPerPixel));
            _sprites.Add(new(205, "largefontU", 61244, 3, 6, BitsPerPixel));
            _sprites.Add(new(206, "largefontV", 61263, 3, 6, BitsPerPixel));
            _sprites.Add(new(207, "largefontW", 61282, 3, 6, BitsPerPixel));
            _sprites.Add(new(208, "largefontX", 61301, 3, 6, BitsPerPixel));
            _sprites.Add(new(209, "largefontY", 61320, 3, 6, BitsPerPixel));
            _sprites.Add(new(210, "largefontZ", 61339, 3, 6, BitsPerPixel));
            _sprites.Add(new(211, "largefont(", 61358, 2, 6, BitsPerPixel));
            _sprites.Add(new(212, "largefont)", 61371, 2, 6, BitsPerPixel));
            _sprites.Add(new(213, "largefont:", 61383, 1, 5, BitsPerPixel));
            _sprites.Add(new(214, "largefontarrowleft", 61403, 3, 6, BitsPerPixel));
        }
    }
}
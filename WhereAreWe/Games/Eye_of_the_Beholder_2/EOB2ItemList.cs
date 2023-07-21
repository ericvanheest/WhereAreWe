using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public enum EOB2ItemStringIndex
    {
        None = 0,
        Empty = 1,
        LeatherArmor = 2,
        Robe = 3,
        Staff = 4,
        Dagger = 5,
        ShortSword = 6,
        LockPicks = 7,
        Spellbook = 8,
        ClericHolySymbol = 9,
        LeatherBoots = 10,
        IronRations = 11,
        NULL = 12,
        Rock = 13,
        GreyKey = 14,
        CopperKey = 15,
        SetOfBones = 16,
        Scroll = 17,
        Axe = 18,
        Chainmail = 19,
        Potion = 20,
        Rations = 21,
        Mace = 22,
        PaladinsHolySymbol = 23,
        Parchment = 24,
        SkullKey = 25,
        DarkMoonKey = 26,
        Shield = 27,
        Skull = 28,
        Femur = 29,
        LongSword = 30,
        Helmet = 31,
        PlateMail = 32,
        Sling = 33,
        SpiderKey = 34,
        StoneGem = 35,
        StoneDagger = 36,
        StoneSphere = 37,
        StoneCross = 38,
        StoneNecklace = 39,
        Horn = 40,
        Ring = 41,
        GlassSphere = 42,
        FireSphere = 43,
        Dart = 44,
        Bow = 45,
        LongBow = 46,
        CompositeBow = 47,
        Arrow = 48,
        BracersOfProtection = 49,
        Amulet = 50,
        Cloak = 51,
        ScaleMail = 52,
        Tome = 53,
        Flail = 54,
        Wand = 55,
        TwoHandedSword = 56,
        BoneKey = 57,
        TuningFork = 58,
        Vial = 59,
        Trident = 60,
        RedGem = 61,
        GreenGem = 62,
        BlueGem = 63,
        PurpleGem = 64,
        MantisKey = 65,
        MantisIdol = 66,
        PolishedShield = 67,
        MedusaShield = 68,
        EyeOfTalon = 69,
        CrystalKey = 70,
        ShellKey = 71,
        Tooth = 72,
        CrimsonKey = 73,
        TalonsTongue = 74,
        HiltOfTalon = 75,
        CrystalHammer = 76,
        JhonasCloak = 77,
        CrimsonRing = 78,
        Spear = 79,
        BandedArmor = 80,
        RingOfProtection = 81,
        Necklace = 82,
        NecklaceOfAdornment = 83,
        Polearm = 84,
        SceptreOfKinglyMight = 85,
        KhelbensCoin = 86,
        Coin = 87,
        AmuletOfLife = 88,
        StickyPaper = 89,
        AmuletOfDeath = 90,
        NorthWind = 91,
        MaceThumper = 92,
        DaggerYargon = 93,
        RottenFood = 94,
        MagicDust = 95,
        AxeTheBait = 96,
        ShortSwordSting = 97,
        PotionOfCurePoison = 98,
        ACompleteSetOfElfBones = 99,
        EastWind = 100,
        ACompleteSetOfDwarfBones = 101,
        DaggerSaShull = 102,
        WestWind = 103,
        LongSwordHathKull = 104,
        SouthWind = 105,
        CloakMoonshade = 106,
        RustyDagger = 107,
        Talon = 108,
        TropeletSeed = 109,
        LongSwordHunger = 110,
        PolearmLeech = 111,
        Halberd = 112,
        RottenRations = 113,
        DragonSkinArmor = 114,
        SoulGem = 115,
        HeartGem = 116,
        BodyGem = 117,
        BrahmasBoots = 118,
        AmuletOfResurrection = 119,
        Bauble = 120,
        Mapaj = 121,
        TheShallRejoice = 122,
        Last
    }

    public enum EOB2ItemTableDefault
    {
        Empty_0 = 0,
        LeatherArmor_1 = 1,
        Robe_2 = 2,
        Staff_3 = 3,
        Dagger_4 = 4,
        ShortSword_5 = 5,
        LockPicks_6 = 6,
        Spellbook_7 = 7,
        ClericHolySymbol_8 = 8,
        LeatherBoots_9 = 9,
        IronRations_16 = 16,
        NULL_26 = 26,
        NULL_27 = 27,
        NULL_28 = 28,
        NULL_29 = 29,
        NULL_30 = 30,
        NULL_31 = 31,
        JeweledKey_17 = 17,
        PotionOfGiantStrength_18 = 18,
        Gem_19 = 19,
        SkullKey_20 = 20,
        Wand_21 = 21,
        Scroll_22 = 22,
        Ring_23 = 23,
        Ring_24 = 24,
        Ring_25 = 25,
        AdamantiteDart_26 = 26,
        Scroll_27 = 27,
        Scroll_28 = 28,
        Scroll_29 = 29,
        IronRations_30 = 30,
        PaladinHolySymbol_31 = 31,
        WandOfSlivias_32 = 32,
        DwarfBones_33 = 33,
        Key_34 = 34,
        CommissionAndLetterOfMarque_35 = 35,
        Axe_36 = 36,
        Dagger_37 = 37,
        Dart_38 = 38,
        AdamantiteDart_39 = 39,
        Halberd_40 = 40,
        Chainmail_41 = 41,
        Helmet_42 = 42,
        DwarfHelmet_43 = 43,
        SilverKey_44 = 44,
        AdamantiteLongSword_45 = 45,
        Mace_46 = 46,
        Longsword_47 = 47,
        PotionOfHealing_48 = 48,
        Guinsoo_49 = 49,
        Gem_50 = 50,
        OrbOfPower_51 = 51,
        DwarvenHealingPotion_52 = 52,
        Rock_53 = 53,
        PotionOfExtraHealing_54 = 54,
        Rations_55 = 55,
        FancyRobe_56 = 56,
        Rock_57 = 57,
        IgneousRock_58 = 58,
        MageScrollOfDetectMagic_59 = 59,
        Spear_60 = 60,
        Staff_61 = 61,
        StoneMedallion_62 = 62,
        Rations_63 = 63,
        HalflingBones_64 = 64,
        LockPicks_65 = 65,
        Rock_66 = 66,
        Dart_67 = 67,
        Rations_68 = 68,
        Rations_69 = 69,
        ClericScrollOfBless_70 = 70,
        Rock_71 = 71,
        MageScrollOfArmor_72 = 72,
        Arrow_73 = 73,
        Shield_74 = 74,
        Arrow_75 = 75,
        Rations_76 = 76,
        Rations_77 = 77,
        LeatherBoots_78 = 78,
        PotionOfHealing_79 = 79,
        SilverKey_80 = 80,
        PotionOfGiantStrength_81 = 81,
        GoldKey_82 = 82,
        Rock_83 = 83,
        SilverKey_84 = 84,
        Bow_85 = 85,
        StoneDagger_86 = 86,
        SilverKey_87 = 87,
        MageScrollOfInvisibility_88 = 88,
        Rations_89 = 89,
        Rations_90 = 90,
        MageScrollOfShield_91 = 91,
        Sling_92 = 92,
        Arrow_93 = 93,
        SilverKey_94 = 94,
        PotionOfHealing_95 = 95,
        Rock_96 = 96,
        Gem_97 = 97,
        Gem_98 = 98,
        Arrow_99 = 99,
        Chainmail_100 = 100,
        Shield_101 = 101,
        Arrow_102 = 102,
        IronRations_103 = 103,
        IronRations_104 = 104,
        SilverKey_105 = 105,
        Gem_106 = 106,
        Gem_107 = 107,
        Arrow_108 = 108,
        PotionOfHealing_109 = 109,
        PotionOfExtraHealing_110 = 110,
        MageScrollOfDetectMagic_111 = 111,
        Backstabber_112 = 112,
        Rations_113 = 113,
        Shield_114 = 114,
        Rations_115 = 115,
        PotionOfHealing_116 = 116,
        Rock_117 = 117,
        ClericScrollOfFlameBlade_118 = 118,
        Rock_119 = 119,
        MageScrollOfFireball_120 = 120,
        ClericScrollOfCauseLightWounds_121 = 121,
        Gem_122 = 122,
        Arrow_123 = 123,
        Rock_124 = 124,
        LongSword_125 = 125,
        Wand_126 = 126,
        Arrow_127 = 127,
        Mace_128 = 128,
        Ring_129 = 129,
        DwarvenKey_130 = 130,
        Arrow_131 = 131,
        Ring_132 = 132,
        Rock_133 = 133,
        PotionOfHealing_134 = 134,
        Mace_135 = 135,
        PotionOfCurePoison_136 = 136,
        PotionOfCurePoison_137 = 137,
        Medallion_138 = 138,
        Robe_139 = 139,
        DrowCleaver_140 = 140,
        StoneScepter_141 = 141,
        Wand_142 = 142,
        PotionOfHealing_143 = 143,
        MageScrollOfFlameArrow_144 = 144,
        ClericScrollOfSlowPoison_145 = 145,
        IronRations_146 = 146,
        IronRations_147 = 147,
        IronRations_148 = 148,
        DwarvenHelmet_149 = 149,
        DwarvenShield_150 = 150,
        Rock_151 = 151,
        Arrow_152 = 152,
        DwarvenKey_153 = 153,
        Rock_154 = 154,
        ClericScrollOfHoldPerson_155 = 155,
        IronRations_156 = 156,
        Spear_157 = 157,
        StoneNecklace_158 = 158,
        ClericScrollOfAid_159 = 159,
        MageScrollOfHaste_160 = 160,
        IronRations_161 = 161,
        ClericScrollOfDetectMagic_162 = 162,
        IronRations_163 = 163,
        LongSword_164 = 164,
        IronRations_165 = 165,
        CommissionAndLetterOfMarque_166 = 166,
        PotionOfPoison_167 = 167,
        IronRations_168 = 168,
        MageScrollOfDispelMagic_169 = 169,
        Rock_170 = 170,
        PlateMail_171 = 171,
        DwarvenKey_172 = 172,
        ScaleMail_173 = 173,
        Axe_174 = 174,
        Sling_175 = 175,
        Key_176 = 176,
        Ring_177 = 177,
        MageScrollOfInvisibility10_178 = 178,
        Key_179 = 179,
        ClericScrollOfPrayer_180 = 180,
        Boots_181 = 181,
        KenkuEgg_182 = 182,
        KenkuEgg_183 = 183,
        KenkuEgg_184 = 184,
        KenkuEgg_185 = 185,
        KenkuEgg_186 = 186,
        KenkuEgg_187 = 187,
        KenkuEgg_188 = 188,
        KenkuEgg_189 = 189,
        KenkuEgg_190 = 190,
        KenkuEgg_191 = 191,
        DwarvenKey_192 = 192,
        DwarvenKey_193 = 193,
        MageScrollOfHoldPerson_194 = 194,
        StoneRing_195 = 195,
        Rock_196 = 196,
        ClericScrollOfDispelMagic_197 = 197,
        ClericScrollOfCureSeriousWounds_198 = 198,
        Key_199 = 199,
        NULL_200 = 200,
        NULL_201 = 201,
        NULL_202 = 202,
        NULL_203 = 203,
        NULL_204 = 204,
        NULL_205 = 205,
        DwarvenShield_206 = 206,
        Rock_207 = 207,
        MacePlus3_208 = 208,
        Bracers_209 = 209,
        Wand_210 = 210,
        DwarvenKey_211 = 211,
        Ring_212 = 212,
        ClericScrollOfFlameBlade_213 = 213,
        MageScrollOfFireball_214 = 214,
        ChieftainHalberd_215 = 215,
        IronRations_216 = 216,
        Necklace_217 = 217,
        ClericScrollOfBless_218 = 218,
        Arrow_219 = 219,
        ClericScrollOfProtectEvil10_220 = 220,
        ClericScrollOfRemoveParalysis_221 = 221,
        ClericScrollOfSlowPoison_222 = 222,
        ClericScrollOfCreateFood_223 = 223,
        Key_224 = 224,
        Medallion_225 = 225,
        Ring_226 = 226,
        Arrow_227 = 227,
        Arrow_228 = 228,
        Arrow_229 = 229,
        Arrow_230 = 230,
        SlicerPlus3_231 = 231,
        Bracers_232 = 232,
        Ring_233 = 233,
        MageScrollOfFear_234 = 234,
        JeweledKey_235 = 235,
        BandedArmor_236 = 236,
        Arrow_237 = 237,
        Arrow_238 = 238,
        Arrow_239 = 239,
        DrowKey_240 = 240,
        MageScrollOfLightningBolt_241 = 241,
        Key_242 = 242,
        PotionOfHealing_243 = 243,
        DrowKey_244 = 244,
        ClericScrollOfCureLightWounds_245 = 245,
        JeweledKey_246 = 246,
        RubyKey_247 = 247,
        Rock_248 = 248,
        Wand_249 = 249,
        Shield_250 = 250,
        ClericScrollOfPrayer_251 = 251,
        ClericScrollOfNeutralPoison_252 = 252,
        ClericScrollOfCureCriticalWounds_253 = 253,
        Medallion_254 = 254,
        Ring_255 = 255,
        Ring_256 = 256,
        NightStalker_257 = 257,
        ClericScrollOfHoldPerson_258 = 258,
        Rock_259 = 259,
        RubyKey_260 = 260,
        MageScrollOfInvisibility10_261 = 261,
        DrowBow_262 = 262,
        DrowKey_263 = 263,
        ClericScrollOfProtectEvil_264 = 264,
        DrowBoots_265 = 265,
        PotionOfExtraHealing_266 = 266,
        ClericScrollOfRaiseDead_267 = 267,
        RubyKey_268 = 268,
        DrowKey_269 = 269,
        JeweledKey_270 = 270,
        MageScrollOfShield_271 = 271,
        Wand_272 = 272,
        PlateMailOfGreatBeauty_273 = 273,
        Flail_274 = 274,
        DrowKey_275 = 275,
        Robe_276 = 276,
        ScepterOfKinglyMight_277 = 277,
        MageScrollOfIceStorm_278 = 278,
        LockPicks_279 = 279,
        DrowKey_280 = 280,
        ClericScrollOfDetectMagic_281 = 281,
        PotionOfPoison_282 = 282,
        MageScrollOfStoneskin_283 = 283,
        Arrow_284 = 284,
        Arrow_285 = 285,
        Arrow_286 = 286,
        DrowKey_287 = 287,
        ClericScrollOfDispelMagic_288 = 288,
        ClericScrollOfCureSeriousWounds_289 = 289,
        MageScrollOfInvisibility_290 = 290,
        ClericScrollOfFlameBlade_291 = 291,
        ClericScrollOfProtectEvil10_292 = 292,
        MageScrollOfArmor_293 = 293,
        DrowShield_294 = 294,
        ClericScrollOfRaiseDead_295 = 295,
        DrowBoots_296 = 296,
        PotionOfExtraHealing_297 = 297,
        Spear_298 = 298,
        Wand_299 = 299,
        ClericScrollOfRaiseDead_300 = 300,
        Chainmail_301 = 301,
        Rock_302 = 302,
        DwarvenKey_303 = 303,
        PlateMail_304 = 304,
        PotionOfPoison_305 = 305,
        Wand_306 = 306,
        ClericScrollOfFlameBlade_307 = 307,
        ClericScrollOfCureCriticalWounds_308 = 308,
        Wand_309 = 309,
        StoneHolySymbol_310 = 310,
        Arrow_311 = 311,
        Arrow_312 = 312,
        SkullKey_313 = 313,
        Ring_314 = 314,
        PotionOfGiantStrength_315 = 315,
        ClericScrollOfFlameBlade_316 = 316,
        ClericScrollOfRemoveParalysis_317 = 317,
        ClericScrollOfNeutralPoison_318 = 318,
        MageScrollOfConeOfCold_319 = 319,
        Wand_320 = 320,
        Medallion_321 = 321,
        ClericScrollOfRaiseDead_322 = 322,
        StoneOrb_323 = 323,
        DrowKey_324 = 324,
        OrbOfPower_325 = 325,
        ClericScrollOfRaiseDead_326 = 326,
        Rock_327 = 327,
        Rock_328 = 328,
        SlasherPlus4_329 = 329,
        BandedArmor_330 = 330,
        Ring_331 = 331,
        MageScrollOfHoldMonster_332 = 332,
        ClericScrollOfCureSeriousWounds_333 = 333,
        IronRations_334 = 334,
        Robe_335 = 335,
        Flicka_336 = 336,
        DrowKey_337 = 337,
        HumanBones_338 = 338,
        HumanBones_339 = 339,
        HumanBones_340 = 340,
        HumanBones_341 = 341,
        HumanBones_342 = 342,
        Ring_343 = 343,
        Bracers_344 = 344,
        LeatherArmor_345 = 345,
        Spear_346 = 346,
        PlateMail_347 = 347,
        Shield_348 = 348,
        Severious_349 = 349,
        Helmet_350 = 350,
        PaladinHolySymbol_351 = 351,
        ShortSword_352 = 352,
        LeatherBoots_353 = 353,
        LeatherArmor_354 = 354,
        IronRations_355 = 355,
        ShortSword_356 = 356,
        Dagger_357 = 357,
        LeatherArmor_358 = 358,
        IronRations_359 = 359,
        Mace_360 = 360,
        Spellbook_361 = 361,
        ClericHolySymbol_362 = 362,
        Robe_363 = 363,
        IronRations_364 = 364,
        ShortSword_365 = 365,
        ClericHolySymbol_366 = 366,
        LeatherArmor_367 = 367,
        LeatherBoots_368 = 368,
        NULL_369 = 369,
        NULL_370 = 370,
        NULL_371 = 371,
        PotionOfSpeed_372 = 372,
        Arrow_373 = 373,
        Arrow_374 = 374,
        Arrow_375 = 375,
        DwarvenKey_376 = 376,
        Rock_377 = 377,
        Rock_378 = 378,
        PotionOfExtraHealing_379 = 379,
        AdamantiteDart_380 = 380,
        Dagger_381 = 381,
        OrbOfPower_382 = 382,
        OrbOfPower_383 = 383,
        OrbOfPower_384 = 384,
        Gem_385 = 385,
        PotionOfExtraHealing_386 = 386,
        PotionOfExtraHealing_387 = 387,
        Ring_388 = 388,
        Necklace_389 = 389,
        Wand_390 = 390,
        OrbOfPower_391 = 391,
        PotionOfSpeed_392 = 392,
        OrbOfPower_393 = 393,
        OrbOfPower_394 = 394,
        OrbOfPower_395 = 395,
        IronRations_396 = 396,
        IronRations_397 = 397,
        IronRations_398 = 398,
        IronRations_399 = 399,
        SkullKey_400 = 400,
        PotionOfInvisibility_401 = 401,
        PotionOfInvisibility_402 = 402,
        PotionOfVitality_403 = 403,
        PotionOfVitality_404 = 404,
        PotionOfInvisibility_405 = 405,
        PotionOfInvisibility_406 = 406,
        Wand_407 = 407,
        NULL_408 = 408,
        StoneScepter_409 = 409,
        StoneDagger_410 = 410,
        StoneMedallion_411 = 411,
        StoneNecklace_412 = 412,
        StoneRing_413 = 413,
        StoneHolySymbol_414 = 414,
        StoneOrb_415 = 415,
        Rations_416 = 416,
        Rations_417 = 417,
        Rations_418 = 418,
        IronRations_419 = 419,
        Rations_420 = 420,
        PotionOfExtraHealing_421 = 421,
        NULL_422 = 422,
        NULL_423 = 423,
        NULL_424 = 424,
        NULL_425 = 425,
        NULL_426 = 426,
        NULL_427 = 427,
        NULL_428 = 428,
        NULL_429 = 429,
        PotionOfCurePoison_430 = 430,
        PotionOfCurePoison_431 = 431,
        PotionOfCurePoison_432 = 432,
        PotionOfCurePoison_433 = 433,
        HolySymbol_434 = 434,
        SpellBook_435 = 435,
        AdamantiteDart_436 = 436,
        AdamantiteDart_437 = 437,
        AdamantiteDart_438 = 438,
        AdamantiteDart_439 = 439,
        AdamantiteDart_440 = 440,
        AdamantiteDart_441 = 441,
        AdamantiteDart_442 = 442,
        AdamantiteDart_443 = 443,
        AdamantiteDart_444 = 444,
        AdamantiteDart_445 = 445,
        Potion_446 = 446,
        MageScrollOfVampiricTouch_447 = 447
    }

    public class EOB2Item : EOBItem
    {
        public EOBSpellIndex Spell;

        public override int SpellIndex { get { return 0; } }
        public override int EffectIndex { get { return 0; } }
        public override int ChargesCurrent { get { return Charges; } set { Charges = value; } }

        public EOB2Item(byte[] bytes, int offset = 0, int iListIndex = -1)
        {
            SetEOB2Bytes(bytes, offset, iListIndex);
        }

        public EOB2Item()
        {
        }

        public override GameNames Game { get { return GameNames.EyeOfTheBeholder2; } }

        public override Item Clone()
        {
            return new EOB2Item(GetBytes(), 0, ItemListIndex);
        }

        public override EOBItem CreateItem(byte[] bytes, int offset = 0)
        {
            return new EOB2Item(bytes, offset);
        }

        public static EOB2Item FromEOB2InventoryBytes(byte[] bytes, int offset = 0)
        {
            return new EOB2Item(bytes, offset);
        }

        public override void SetBytes(int index, byte[] bytes, int offset = 0) { SetEOB2Bytes(bytes, offset); }

        public static EOB2Item FromBasicBytes(byte[] bytes, int index, int offset = 0)
        {
            EOB2Item item = new EOB2Item();
            item.SetEOB2BasicBytes(bytes, index, offset);
            return item;
        }

        public void SetEOB2BasicBytes(byte[] bytes, int index, int offset = 0)
        {
            if (offset > bytes.Length - 14)
                return;
            // This sets an item from the basic item list (i.e. not from the master item table,
            // which includes information about where the particular item is in the dungeon, etc.)
            ItemIndex = (EOBItemIndex) index;
            m_strName = GetName(ItemIndex);
            FixedType1 = (EOBBasicType)BitConverter.ToInt16(bytes, offset);
            FixedType2 = (EOBBasicType)BitConverter.ToInt16(bytes, offset + 2);
            AC = (sbyte)bytes[offset + 4];
            Usable = (EOBUseFlags)bytes[offset + 5];
            Handed = (EOBHanded)bytes[offset + 6];
            Damage = new DamageDice(bytes[offset + 8], bytes[offset + 7], bytes[offset + 9]);
            DamageLarge = new DamageDice(bytes[offset + 11], bytes[offset + 10], bytes[offset + 12]);
            SecondaryType = (EOBSecondaryType)bytes[13];
        }

        public override byte[] GetBasicBytes()
        {
            byte[] bytes = new byte[14];
            byte[] bytesShort = BitConverter.GetBytes((short)FixedType1);
            Buffer.BlockCopy(bytesShort, 0, bytes, 0, bytesShort.Length);
            bytesShort = BitConverter.GetBytes((short)FixedType2);
            Buffer.BlockCopy(bytesShort, 0, bytes, 2, bytesShort.Length);
            bytes[4] = (byte)AC;
            bytes[5] = (byte)Usable;
            bytes[6] = (byte)Handed;
            bytes[7] = (byte)Damage.Quantity;
            bytes[8] = (byte)Damage.Faces;
            bytes[9] = (byte)Damage.Bonus;
            bytes[10] = (byte)DamageLarge.Quantity;
            bytes[11] = (byte)DamageLarge.Faces;
            bytes[12] = (byte)DamageLarge.Bonus;
            bytes[13] = (byte)SecondaryType;
            return bytes;
        }

        public override string Name
        {
            get
            {
                // Some items have specific names that don't involve the modifier
                switch ((EOB2ItemStringIndex)StringIndex)
                {
                    default: break;
                }
                string strModifier = ModifierString(ItemIndex, Modifier);
                // Include the Type and Modifier information (e.g. "Cleric Scroll" or "+4" or "of Extra Healing")
                switch (ItemIndex)
                {
                    case EOBItemIndex.ClericScroll: return String.Format("Cleric {0} of {1}", m_strName, strModifier);
                    case EOBItemIndex.MageScroll: return String.Format("Mage {0} of {1}", m_strName, strModifier);
                    case EOBItemIndex.Potion: return String.Format("{0} of {1}", m_strName, strModifier);
                    case EOBItemIndex.DwarvenHealingPotion: return "Dwarven Healing Potion";
                    case EOBItemIndex.Wand: return String.Format("{0} of {1}", m_strName, strModifier);
                    case EOBItemIndex.Key:
                    case EOBItemIndex.KenkuEgg:
                    case EOBItemIndex.Stone:
                    case EOBItemIndex.Gem: return String.Format("{0} ({1})", m_strName, strModifier);
                    case EOBItemIndex.Rations: return m_strName;
                    case EOBItemIndex.RingEffect: return String.Format("{0} {1}", m_strName, strModifier);
                    case EOBItemIndex.Bones: return String.Format("{0} ({1})", m_strName, strModifier);
                    case EOBItemIndex.TextScroll: return String.Format("{0} (Text #{1})", m_strName, strModifier);
                    default:
                        if (IsWeapon && Modifier < 0)
                            return String.Format("Cursed {0} {1}", m_strName, Modifier);
                        if (Modifier == 0)
                            return m_strName;
                        return String.Format("{0} {1}", m_strName, strModifier);
                }
            }
        }

        public override string StringName => GetName((EOB2ItemStringIndex)StringIndex);

        public static string GetName(EOB2ItemStringIndex index)
        {
            switch (index)
            {
                default: return String.Format("Unknown({0})", (int)index);
                case EOB2ItemStringIndex.Empty: return "Empty";
                case EOB2ItemStringIndex.LeatherArmor: return "Leather armor";
                case EOB2ItemStringIndex.Robe: return "Robe";
                case EOB2ItemStringIndex.Staff: return "Staff";
                case EOB2ItemStringIndex.Dagger: return "Dagger";
                case EOB2ItemStringIndex.ShortSword: return "Short sword";
                case EOB2ItemStringIndex.LockPicks: return "Lock picks";
                case EOB2ItemStringIndex.Spellbook: return "Spellbook";
                case EOB2ItemStringIndex.ClericHolySymbol: return "Cleric Holy symbol";
                case EOB2ItemStringIndex.LeatherBoots: return "Leather boots";
                case EOB2ItemStringIndex.IronRations: return "Iron Rations";
                case EOB2ItemStringIndex.NULL: return "NULL";
                case EOB2ItemStringIndex.Rock: return "Rock";
                case EOB2ItemStringIndex.GreyKey: return "Grey Key";
                case EOB2ItemStringIndex.CopperKey: return "Copper Key";
                case EOB2ItemStringIndex.SetOfBones: return "Set of bones";
                case EOB2ItemStringIndex.Scroll: return "Scroll";
                case EOB2ItemStringIndex.Axe: return "Axe";
                case EOB2ItemStringIndex.Chainmail: return "Chainmail";
                case EOB2ItemStringIndex.Potion: return "Potion";
                case EOB2ItemStringIndex.Rations: return "Rations";
                case EOB2ItemStringIndex.Mace: return "Mace";
                case EOB2ItemStringIndex.PaladinsHolySymbol: return "Paladin's Holy Symbol";
                case EOB2ItemStringIndex.Parchment: return "Parchment";
                case EOB2ItemStringIndex.SkullKey: return "Skull Key";
                case EOB2ItemStringIndex.DarkMoonKey: return "Dark Moon Key";
                case EOB2ItemStringIndex.Shield: return "Shield";
                case EOB2ItemStringIndex.Skull: return "Skull";
                case EOB2ItemStringIndex.Femur: return "Femur";
                case EOB2ItemStringIndex.LongSword: return "Long Sword";
                case EOB2ItemStringIndex.Helmet: return "Helmet";
                case EOB2ItemStringIndex.PlateMail: return "Plate Mail";
                case EOB2ItemStringIndex.Sling: return "Sling";
                case EOB2ItemStringIndex.SpiderKey: return "Spider Key";
                case EOB2ItemStringIndex.StoneGem: return "Stone Gem";
                case EOB2ItemStringIndex.StoneDagger: return "Stone Dagger";
                case EOB2ItemStringIndex.StoneSphere: return "Stone Sphere";
                case EOB2ItemStringIndex.StoneCross: return "Stone Cross";
                case EOB2ItemStringIndex.StoneNecklace: return "Stone Necklace";
                case EOB2ItemStringIndex.Horn: return "Horn";
                case EOB2ItemStringIndex.Ring: return "Ring";
                case EOB2ItemStringIndex.GlassSphere: return "Glass Sphere";
                case EOB2ItemStringIndex.FireSphere: return "Fire Sphere";
                case EOB2ItemStringIndex.Dart: return "Dart";
                case EOB2ItemStringIndex.Bow: return "Bow";
                case EOB2ItemStringIndex.LongBow: return "Long Bow";
                case EOB2ItemStringIndex.CompositeBow: return "Composite Bow";
                case EOB2ItemStringIndex.Arrow: return "Arrow";
                case EOB2ItemStringIndex.BracersOfProtection: return "Bracers of Protection";
                case EOB2ItemStringIndex.Amulet: return "Amulet";
                case EOB2ItemStringIndex.Cloak: return "Cloak";
                case EOB2ItemStringIndex.ScaleMail: return "Scale Mail";
                case EOB2ItemStringIndex.Tome: return "Tome";
                case EOB2ItemStringIndex.Flail: return "Flail";
                case EOB2ItemStringIndex.Wand: return "Wand";
                case EOB2ItemStringIndex.TwoHandedSword: return "Two Handed Sword";
                case EOB2ItemStringIndex.BoneKey: return "Bone Key";
                case EOB2ItemStringIndex.TuningFork: return "Tuning Fork";
                case EOB2ItemStringIndex.Vial: return "Vial";
                case EOB2ItemStringIndex.Trident: return "Trident";
                case EOB2ItemStringIndex.RedGem: return "Red Gem";
                case EOB2ItemStringIndex.GreenGem: return "Green Gem";
                case EOB2ItemStringIndex.BlueGem: return "Blue Gem";
                case EOB2ItemStringIndex.PurpleGem: return "Purple Gem";
                case EOB2ItemStringIndex.MantisKey: return "Mantis Key";
                case EOB2ItemStringIndex.MantisIdol: return "Mantis Idol";
                case EOB2ItemStringIndex.PolishedShield: return "Polished Shield";
                case EOB2ItemStringIndex.MedusaShield: return "Medusa Shield";
                case EOB2ItemStringIndex.EyeOfTalon: return "Eye of Talon";
                case EOB2ItemStringIndex.CrystalKey: return "Crystal Key";
                case EOB2ItemStringIndex.ShellKey: return "Shell Key";
                case EOB2ItemStringIndex.Tooth: return "Tooth";
                case EOB2ItemStringIndex.CrimsonKey: return "Crimson Key";
                case EOB2ItemStringIndex.TalonsTongue: return "Talon's Tongue";
                case EOB2ItemStringIndex.HiltOfTalon: return "Hilt of Talon";
                case EOB2ItemStringIndex.CrystalHammer: return "Crystal Hammer";
                case EOB2ItemStringIndex.JhonasCloak: return "Jhonas' Cloak";
                case EOB2ItemStringIndex.CrimsonRing: return "Crimson Ring";
                case EOB2ItemStringIndex.Spear: return "Spear";
                case EOB2ItemStringIndex.BandedArmor: return "Banded Armor";
                case EOB2ItemStringIndex.RingOfProtection: return "Ring of Protection";
                case EOB2ItemStringIndex.Necklace: return "Necklace";
                case EOB2ItemStringIndex.NecklaceOfAdornment: return "Necklace of Adornment";
                case EOB2ItemStringIndex.Polearm: return "Polearm";
                case EOB2ItemStringIndex.SceptreOfKinglyMight: return "Sceptre of Kingly Might";
                case EOB2ItemStringIndex.KhelbensCoin: return "Khelben's Coin";
                case EOB2ItemStringIndex.Coin: return "Coin";
                case EOB2ItemStringIndex.AmuletOfLife: return "Amulet of Life";
                case EOB2ItemStringIndex.StickyPaper: return "Sticky Paper";
                case EOB2ItemStringIndex.AmuletOfDeath: return "Amulet of Death";
                case EOB2ItemStringIndex.NorthWind: return "north wind";
                case EOB2ItemStringIndex.MaceThumper: return "mace \"thumper\"";
                case EOB2ItemStringIndex.DaggerYargon: return "dagger \"yargon\"";
                case EOB2ItemStringIndex.RottenFood: return "rotten food";
                case EOB2ItemStringIndex.MagicDust: return "magic dust";
                case EOB2ItemStringIndex.AxeTheBait: return "axe \"the bait\"";
                case EOB2ItemStringIndex.ShortSwordSting: return "short sword \"sting\"";
                case EOB2ItemStringIndex.PotionOfCurePoison: return "potion of cure poison";
                case EOB2ItemStringIndex.ACompleteSetOfElfBones: return "a complete set of elf bones";
                case EOB2ItemStringIndex.EastWind: return "east wind";
                case EOB2ItemStringIndex.ACompleteSetOfDwarfBones: return "a complete set of dwarf bones";
                case EOB2ItemStringIndex.DaggerSaShull: return "dagger \"sa shull\"";
                case EOB2ItemStringIndex.WestWind: return "west wind";
                case EOB2ItemStringIndex.LongSwordHathKull: return "long sword \"hath kull\"";
                case EOB2ItemStringIndex.SouthWind: return "south wind";
                case EOB2ItemStringIndex.CloakMoonshade: return "cloak \"moonshade\"";
                case EOB2ItemStringIndex.RustyDagger: return "rusty dagger";
                case EOB2ItemStringIndex.Talon: return "talon";
                case EOB2ItemStringIndex.TropeletSeed: return "tropelet seed";
                case EOB2ItemStringIndex.LongSwordHunger: return "long sword \"hunger\"";
                case EOB2ItemStringIndex.PolearmLeech: return "polearm \"leech\"";
                case EOB2ItemStringIndex.Halberd: return "halberd";
                case EOB2ItemStringIndex.RottenRations: return "rotten rations";
                case EOB2ItemStringIndex.DragonSkinArmor: return "dragon skin armor";
                case EOB2ItemStringIndex.SoulGem: return "soul gem";
                case EOB2ItemStringIndex.HeartGem: return "heart gem";
                case EOB2ItemStringIndex.BodyGem: return "body gem";
                case EOB2ItemStringIndex.BrahmasBoots: return "brahma's boots";
                case EOB2ItemStringIndex.AmuletOfResurrection: return "amulet of resurrection";
                case EOB2ItemStringIndex.Bauble: return "bauble";
                case EOB2ItemStringIndex.Mapaj: return "mapaj";
                case EOB2ItemStringIndex.TheShallRejoice: return "the shall rejoice";
            }
        }

        public void SetEOB2Bytes(byte[] bytes, int offset = 0, int iListIndex = -1)
        {
            if (offset > bytes.Length - 14 || offset < 0)
            {
                Global.LogError("SetEOB2Bytes called with offset {0} but bytes.Length only {1}", offset, bytes.Length);
                return;
            }

            Damage = DamageDice.Zero;
            SetEOB2BasicBytes(EOB2.ItemList.Value.RawBytes, bytes[offset + 4], bytes[offset + 4] * 16);
            if (iListIndex == -1)
                ItemListIndex = offset / 14;
            else
                ItemListIndex = iListIndex;
            StringIndex = bytes[offset];
            if (StringIndex != (int)EOB1ItemStringIndex.None)
                m_strName = GetName((EOB2ItemStringIndex)StringIndex);
            Unknown01 = bytes[offset + 1];
            EOBItemFlags flags = (EOBItemFlags)bytes[offset + 2];
            Magical = flags.HasFlag(EOBItemFlags.Magical);
            Identified = flags.HasFlag(EOBItemFlags.Identified);
            Nonremovable = flags.HasFlag(EOBItemFlags.Nonremovable);
            Charges = bytes[offset + 2] & 0x1F;
            Image = bytes[offset + 3];
            Floor = (EOBItemLocation)bytes[offset + 5];
            ushort iLocation = BitConverter.ToUInt16(bytes, offset + 6);
            Available = iLocation == 0xFFFF;
            InQuiver = iLocation == 0xFFFE;
            Location = EOB2MemoryHacker.PointFromPackedFive(iLocation);
            NextItem = BitConverter.ToInt16(bytes, offset + 8);
            PrevItem = BitConverter.ToInt16(bytes, offset + 10);
            MapIndex = bytes[offset + 12];
            Modifier = (sbyte) bytes[offset + 13];
            WhereEquipped = EquipLocation.None;
            Usable = EOBUseFlags.All;
        }

        public override byte[] GetBytes() { return GetItemListBytes(); }
        
        public override byte[] Serialize() { return new byte[] { GetFlagByte(), (byte)Index, (byte)ChargesCurrent }; }

        public override bool IsWeapon { get { return base.IsWeapon; } }

        public static EOBSpellIndex GetItemSpell(int index)
        {
            return EOBSpellIndex.None;
        }

        public override bool CanEquip { get { return true; } }      // Can equip almost anything in EOB2
    }

    public class EOB2ItemList : EOB123ItemList
    {
        public override GameNames Game => GameNames.EyeOfTheBeholder2;

        public override EOBItem CreateItem(int iItemCount, byte[] bytes, int iPos) { return new EOB2Item(bytes, iPos); }
        public override byte[] GetInternalBytes() { return Global.DecompressBytes(Properties.Resources.EOB2_Item_List); }

        public override byte[] GetExternalBytes(MemoryHacker hacker)
        {
            if (hacker == null || !hacker.IsValid)
                return null;
            MemoryBytes mbItems = hacker.ReadOffset(EOB2.Memory.ItemBasicList, 800);
            if (mbItems == null)
                return GetInternalBytes();
            return mbItems.Bytes;
        }

        public EOB2ItemList()
        {
            InitEOB2InternalList();
        }

        private void InitEOB2InternalList()
        {
            Items = SetFromBytes(Game, Global.DecompressBytes(Properties.Resources.EOB2_Item_List));
        }

        public override bool InitInternalList()
        {
            Items = SetFromBytes(Game, GetInternalBytes());
            return true;
        }
    }
}

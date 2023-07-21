using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public enum EOB1ItemStringIndex
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
        Null = 12,
        JeweledKey = 13,
        Potion = 14,
        Gem = 15,
        SkullKey = 16,
        Wand = 17,
        Scroll = 18,
        Ring = 19,
        RingOfProtectionPlus2 = 20,
        AdamantiteDart = 21,
        PaladinHolySymbol = 22,
        WandOfSlivias = 23,
        DwarfBones = 24,
        Key = 25,
        CommissionAndLetterOfMarque = 26,
        Axe = 27,
        Dart = 28,
        Halberd = 29,
        Chainmail = 30,
        Helmet = 31,
        DwarfHelmet = 32,
        SilverKey = 33,
        AdamantiteLongSword = 34,
        Mace = 35,
        Longsword = 36,
        Guinsoo = 37,
        OrbOfPower = 38,
        DwarvenHealingPotion = 39,
        Rock = 40,
        Rations = 41,
        FancyRobe = 42,
        IgneousRock = 43,
        Spear = 44,
        StoneMedallion = 45,
        HalflingBones = 46,
        Arrow = 47,
        Shield = 48,
        GoldKey = 49,
        Bow = 50,
        StoneDagger = 51,
        Sling = 52,
        Backstabber = 53,
        LongSword = 54,
        DwarvenKey = 55,
        Medallion = 56,
        MedallionOfAdornment = 57,
        DrowCleaver = 58,
        StoneScepter = 59,
        DwarvenHelmet = 60,
        DwarvenShield = 61,
        StoneNecklace = 62,
        PlateMail = 63,
        ScaleMail = 64,
        Boots = 65,
        KenkuEgg = 66,
        StoneRing = 67,
        Bracers = 68,
        ChieftainHalberd = 69,
        Necklace = 70,
        NecklaceOfAdornment = 71,
        LuckStoneMedallion = 72,
        Slicer = 73,
        BandedArmor = 74,
        DrowKey = 75,
        RubyKey = 76,
        NightStalker = 77,
        DrowBow = 78,
        DrowBoots = 79,
        PlateMailOfGreatBeauty = 80,
        Flail = 81,
        ScepterOfKinglyMight = 82,
        DrowShield = 83,
        StoneHolySymbol = 84,
        StoneOrb = 85,
        Slasher = 86,
        RobeOfDefense = 87,
        Flicka = 88,
        HumanBones = 89,
        Severious = 90,
        WandOfFireballs = 91,
        CurePoisonPotion = 92,
        HolySymbol = 93,
        SpellBook = 94,
        Last
    }

    public enum EOB1ItemTableDefault
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

    public class EOB1Item : EOBItem
    {
        public EOBSpellIndex Spell;

        public override int SpellIndex { get { return 0; } }
        public override int EffectIndex { get { return 0; } }
        public override int ChargesCurrent { get { return Charges; } set { Charges = value; } }

        public EOB1Item(byte[] bytes, int offset = 0, int iListIndex = -1)
        {
            SetEOB1Bytes(bytes, offset, iListIndex);
        }

        public EOB1Item()
        {
        }

        public override GameNames Game { get { return GameNames.EyeOfTheBeholder1; } }

        public override Item Clone()
        {
            return new EOB1Item(GetBytes(), 0, ItemListIndex);
        }

        public override EOBItem CreateItem(byte[] bytes, int offset = 0)
        {
            return new EOB1Item(bytes, offset);
        }

        public static EOB1Item FromEOB1InventoryBytes(byte[] bytes, int offset = 0)
        {
            return new EOB1Item(bytes, offset);
        }

        public override void SetBytes(int index, byte[] bytes, int offset = 0) { SetEOB1Bytes(bytes, offset); }

        public static EOB1Item FromBasicBytes(byte[] bytes, int index, int offset = 0)
        {
            EOB1Item item = new EOB1Item();
            item.SetEOB1BasicBytes(bytes, index, offset);
            return item;
        }

        public void SetEOB1BasicBytes(byte[] bytes, int index, int offset = 0)
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

        public override string StringName => GetName((EOB1ItemStringIndex)StringIndex);

        public static string GetName(EOB1ItemStringIndex index)
        {
            switch (index)
            {
                case EOB1ItemStringIndex.Empty: return "Empty";
                case EOB1ItemStringIndex.LeatherArmor: return "Leather armor";
                case EOB1ItemStringIndex.Robe: return "Robe";
                case EOB1ItemStringIndex.Staff: return "Staff";
                case EOB1ItemStringIndex.Dagger: return "Dagger";
                case EOB1ItemStringIndex.ShortSword: return "Short sword";
                case EOB1ItemStringIndex.LockPicks: return "Lock picks";
                case EOB1ItemStringIndex.Spellbook: return "Spellbook";
                case EOB1ItemStringIndex.ClericHolySymbol: return "Cleric Holy symbol";
                case EOB1ItemStringIndex.LeatherBoots: return "Leather boots";
                case EOB1ItemStringIndex.IronRations: return "Iron Rations";
                case EOB1ItemStringIndex.Null: return "NULL";
                case EOB1ItemStringIndex.JeweledKey: return "Jeweled Key";
                case EOB1ItemStringIndex.Potion: return "Potion";
                case EOB1ItemStringIndex.Gem: return "Gem";
                case EOB1ItemStringIndex.SkullKey: return "Skull Key";
                case EOB1ItemStringIndex.Wand: return "Wand";
                case EOB1ItemStringIndex.Scroll: return "Scroll";
                case EOB1ItemStringIndex.Ring: return "Ring";
                case EOB1ItemStringIndex.RingOfProtectionPlus2: return "Ring of Protection +2";
                case EOB1ItemStringIndex.AdamantiteDart: return "Adamantite Dart";
                case EOB1ItemStringIndex.PaladinHolySymbol: return "Paladin Holy Symbol";
                case EOB1ItemStringIndex.WandOfSlivias: return "Wand of Slivias";
                case EOB1ItemStringIndex.DwarfBones: return "Dwarf Bones";
                case EOB1ItemStringIndex.Key: return "Key";
                case EOB1ItemStringIndex.CommissionAndLetterOfMarque: return "Commission and Letter of Marque";
                case EOB1ItemStringIndex.Axe: return "Axe";
                case EOB1ItemStringIndex.Dart: return "Dart";
                case EOB1ItemStringIndex.Halberd: return "Halberd";
                case EOB1ItemStringIndex.Chainmail: return "Chainmail";
                case EOB1ItemStringIndex.Helmet: return "Helmet";
                case EOB1ItemStringIndex.DwarfHelmet: return "Dwarf Helmet";
                case EOB1ItemStringIndex.SilverKey: return "Silver Key";
                case EOB1ItemStringIndex.AdamantiteLongSword: return "Adamantite Long Sword";
                case EOB1ItemStringIndex.Mace: return "Mace";
                case EOB1ItemStringIndex.Longsword: return "Longsword";
                case EOB1ItemStringIndex.Guinsoo: return "'Guinsoo'";
                case EOB1ItemStringIndex.OrbOfPower: return "Orb of Power";
                case EOB1ItemStringIndex.DwarvenHealingPotion: return "Dwarven Healing Potion";
                case EOB1ItemStringIndex.Rock: return "Rock";
                case EOB1ItemStringIndex.Rations: return "Rations";
                case EOB1ItemStringIndex.FancyRobe: return "Fancy Robe";
                case EOB1ItemStringIndex.IgneousRock: return "Igneous Rock";
                case EOB1ItemStringIndex.Spear: return "Spear";
                case EOB1ItemStringIndex.StoneMedallion: return "Stone Medallion";
                case EOB1ItemStringIndex.HalflingBones: return "Halfling Bones";
                case EOB1ItemStringIndex.Arrow: return "Arrow";
                case EOB1ItemStringIndex.Shield: return "Shield";
                case EOB1ItemStringIndex.GoldKey: return "Gold Key";
                case EOB1ItemStringIndex.Bow: return "Bow";
                case EOB1ItemStringIndex.StoneDagger: return "Stone Dagger";
                case EOB1ItemStringIndex.Sling: return "Sling";
                case EOB1ItemStringIndex.Backstabber: return "'Backstabber'";
                case EOB1ItemStringIndex.LongSword: return "Long Sword";
                case EOB1ItemStringIndex.DwarvenKey: return "Dwarven Key";
                case EOB1ItemStringIndex.Medallion: return "Medallion";
                case EOB1ItemStringIndex.MedallionOfAdornment: return "Medallion of Adornment";
                case EOB1ItemStringIndex.DrowCleaver: return "'Drow Cleaver'";
                case EOB1ItemStringIndex.StoneScepter: return "Stone Scepter";
                case EOB1ItemStringIndex.DwarvenHelmet: return "Dwarven Helmet";
                case EOB1ItemStringIndex.DwarvenShield: return "Dwarven Shield";
                case EOB1ItemStringIndex.StoneNecklace: return "Stone Necklace";
                case EOB1ItemStringIndex.PlateMail: return "Plate Mail";
                case EOB1ItemStringIndex.ScaleMail: return "Scale Mail";
                case EOB1ItemStringIndex.Boots: return "Boots";
                case EOB1ItemStringIndex.KenkuEgg: return "Kenku Egg";
                case EOB1ItemStringIndex.StoneRing: return "Stone Ring";
                case EOB1ItemStringIndex.Bracers: return "Bracers";
                case EOB1ItemStringIndex.ChieftainHalberd: return "Chieftain Halberd";
                case EOB1ItemStringIndex.Necklace: return "Necklace";
                case EOB1ItemStringIndex.NecklaceOfAdornment: return "Necklace of Adornment";
                case EOB1ItemStringIndex.LuckStoneMedallion: return "Luck Stone Medallion";
                case EOB1ItemStringIndex.Slicer: return "'Slicer'";
                case EOB1ItemStringIndex.BandedArmor: return "Banded Armor";
                case EOB1ItemStringIndex.DrowKey: return "Drow Key";
                case EOB1ItemStringIndex.RubyKey: return "Ruby Key";
                case EOB1ItemStringIndex.NightStalker: return "'Night Stalker'";
                case EOB1ItemStringIndex.DrowBow: return "Drow Bow";
                case EOB1ItemStringIndex.DrowBoots: return "Drow Boots";
                case EOB1ItemStringIndex.PlateMailOfGreatBeauty: return "Plate Mail of Great Beauty";
                case EOB1ItemStringIndex.Flail: return "Flail";
                case EOB1ItemStringIndex.ScepterOfKinglyMight: return "Scepter of Kingly Might";
                case EOB1ItemStringIndex.DrowShield: return "Drow Shield";
                case EOB1ItemStringIndex.StoneHolySymbol: return "Stone Holy Symbol";
                case EOB1ItemStringIndex.StoneOrb: return "Stone Orb";
                case EOB1ItemStringIndex.Slasher: return "'Slasher'";
                case EOB1ItemStringIndex.RobeOfDefense: return "Robe of Defense";
                case EOB1ItemStringIndex.Flicka: return "'Flicka'";
                case EOB1ItemStringIndex.HumanBones: return "Human Bones";
                case EOB1ItemStringIndex.Severious: return "'Severious'";
                case EOB1ItemStringIndex.WandOfFireballs: return "Wand of Fireballs";
                case EOB1ItemStringIndex.CurePoisonPotion: return "Cure Poison Potion";
                case EOB1ItemStringIndex.HolySymbol: return "Holy Symbol";
                case EOB1ItemStringIndex.SpellBook: return "Spell Book";
                default: return String.Format("Unknown({0})", (int)index);
            }
        }

        public override string Name
        {
            get
            {
                // Some items have specific names that don't involve the modifier
                switch ((EOB1ItemStringIndex)StringIndex)
                {
                    case EOB1ItemStringIndex.CurePoisonPotion: return m_strName;
                    case EOB1ItemStringIndex.WandOfSlivias: return m_strName;
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

        public void SetEOB1Bytes(byte[] bytes, int offset = 0, int iListIndex = -1)
        {
            Damage = DamageDice.Zero;
            SetEOB1BasicBytes(EOB1.ItemList.Value.RawBytes, bytes[offset + 4], bytes[offset + 4] * 16);
            if (iListIndex == -1)
                ItemListIndex = offset / 14;
            else
                ItemListIndex = iListIndex;
            StringIndex = bytes[offset];
            if (StringIndex != (int) EOB1ItemStringIndex.None)
                m_strName = GetName((EOB1ItemStringIndex)StringIndex);
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
            Location = EOB1MemoryHacker.PointFromPackedFive(iLocation);
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

        public override bool CanEquip { get { return true; } }      // Can equip almost anything in EOB1
    }

    public class EOB1ItemList : EOB123ItemList
    {
        public override GameNames Game => GameNames.EyeOfTheBeholder2;

        public override EOBItem CreateItem(int iItemCount, byte[] bytes, int iPos) { return new EOB1Item(bytes, iPos); }
        public override byte[] GetInternalBytes() { return Global.DecompressBytes(Properties.Resources.EOB1_Item_List); }

        public override byte[] GetExternalBytes(MemoryHacker hacker)
        {
            if (hacker == null || !hacker.IsValid)
                return null;
            MemoryBytes mbItems = hacker.ReadOffset(EOB1.Memory.ItemBasicList, 800);
            if (mbItems == null)
                return GetInternalBytes();
            return mbItems.Bytes;
        }

        public EOB1ItemList()
        {
            InitEOB1InternalList();
        }

        private void InitEOB1InternalList()
        {
            Items = SetFromBytes(Game, Global.DecompressBytes(Properties.Resources.EOB1_Item_List));
        }

        public override bool InitInternalList()
        {
            Items = SetFromBytes(Game, GetInternalBytes());
            m_bInternal = true;
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace WhereAreWe
{
    public class MM45VoiceMemory : MM45Memory
    {
        public override int MainSearchSVN { get { return 8376; } }
        public override int MainSearchNonSVN1 { get { return 7976; } }
        public override int MainSearchNonSVN2 { get { return 8344; } }

        public override int NumChars { get { return 354564; } }
        public override int PartyInfo { get { return 339862; } }
        public override int ActingCaster { get { return 332347; } }

        public override int LastKeypress { get { return 362694; } }
        public override int GameState2 { get { return 362678; } }
        public override int SellSubscreen { get { return 362370; } }
        public override int QuestSubscreen { get { return 362574; } }
        public override int EnchantItemChar { get { return 362582; } }
        public override int EnchantItemSubscreen { get { return 362634; } }
        public override int InventorySubscreen { get { return 362640; } }
        public override int ActingCharacter { get { return 362692; } }

        public override int MM4MonsterData { get { return 263912; } }
        public override int MonsterSide { get { return 263943; } }
        public override int MM5MonsterData { get { return 263912; } }
        public override int SpecialSquares { get { return 271206; } }
        public override int GameState1 { get { return 332816; } }
        public override int GameState3 { get { return 261728; } }
        public override int NumMonsters { get { return 333777; } }
        public override int WorldSide3 { get { return 332378; } }
        public override int CreationStats { get { return 362620; } }

        // Probably correct:

        public override int InCombat { get { return 332775; } }
        public override int LoadingMap1 { get { return 231330; } }
        public override int LoadingMap2 { get { return 234302; } }
        public override int LoadingMap3 { get { return 234322; } }
        public override int LoadingMap4 { get { return 234326; } }
        public override int CurrentMapIndex { get { return 354577; } }
        public override int CurrentMapQuad { get { return 356362; } }
        public override int MapAppearance1 { get { return 342340; } }
        public override int MapAppearance2 { get { return 344124; } }
        public override int MapAppearance3 { get { return 343232; } }
        public override int MapAppearance4 { get { return 346800; } }
        public override int MapAttributes1 { get { return 342852; } }
        public override int MapAttributes2 { get { return 344636; } }
        public override int MapAttributes3 { get { return 343744; } }
        public override int MapAttributes4 { get { return 347312; } }
        public override int MapFlags1 { get { return 343108; } }
        public override int Map1North { get { return 343110; } }
        public override int Map1East { get { return 343112; } }
        public override int Map1Flags { get { return 343118; } }
        public override int MapFlags2 { get { return 344892; } }
        public override int MapFlags3 { get { return 344000; } }
        public override int MapFlags4 { get { return 347568; } }
        public override int MapCartography1 { get { return 343200; } }
        public override int MapCartography2 { get { return 344984; } }
        public override int MapCartography3 { get { return 344092; } }
        public override int MapCartography4 { get { return 347660; } }
        public override int FacingAndLocation { get { return 354574; } }
        public override int MonsterPositions { get { return 352424; } }
        public override int PartyStaticBits { get { return 355223; } }
        public override int PartyGold { get { return 355202; } }
        public override int PartyGems { get { return 355206; } }
        public override int PartyFood { get { return 355182; } }
        public override int BankGold { get { return 355210; } }
        public override int BankGems { get { return 355214; } }
        public override int Day { get { return 355176; } }
        public override int Year { get { return 355178; } }
        public override int Minutes { get { return 355180; } }
        public override int PartyBytes { get { return 354564; } }
        public override int VisibleObjects { get { return 351096; } }
        public override int WorldBits { get { return 355287; } }
        public override int QuestBits { get { return 355303; } }
        public override int MovedThisRound { get { return 332465; } }
        public override int WorldSideSurface { get { return 357916; } }
        public override int WorldSide2 { get { return 329720; } }
        public override int WorldSide4 { get { return 356741; } }
        public override int CurrentStoreWeapons { get { return 342152; } }
        public override int CurrentStoreArmor { get { return 342188; } }
        public override int CurrentStoreAccessories { get { return 342224; } }
        public override int CurrentStoreMisc { get { return 342260; } }
        public override int CloudsideStore1 { get { return 354592; } }
        public override int CloudsideStore2 { get { return 354736; } }
        public override int CloudsideStore3 { get { return 354880; } }
        public override int CloudsideStore4 { get { return 355024; } }
        public override int DarksideStore1 { get { return 355396; } }
        public override int DarksideStore2 { get { return 355540; } }
        public override int DarksideStore3 { get { return 355684; } }
        public override int DarksideStore4 { get { return 355828; } }

        // Definitely needs verification:

        public override int FloorTileSet { get { return 343166; } }
        public override int QuestStrings { get { return 505104; } }

        public override int ScriptsNonSVN1 { get { return 1196064; } }
        public override int ScriptsSVN { get { return 1245216; } }
        public override int ScriptsPointer { get { return 561822; } }
        public override int ScriptsLength { get { return 333774; } }
        public override int Strings { get { return 496248; } }
        public override int StringsPointer { get { return 333784; } }
        public override int StringsLength { get { return 333770; } }
        public override int WorldSide { get { return 637638; } }
        //public override int WorldSide5 { get { return 273446; } }

    }

    public class MM45NonVoiceMemory : MM45Memory
    {
        public override int MainSearchSVN { get { return 8352; } }              // Address of Main Block + Main Search
        public override int MainSearchNonSVN1 { get { return 7952; } }
        public override int MainSearchNonSVN2 { get { return 8320; } }

        // Offsets based on the base rather than the main search
        public override int ScriptsNonSVN1 { get { return 1196064; } }
        public override int ScriptsSVN { get { return 1245216; } }

        // Offsets based on the main search delta;
        public override int LoadingMap1 { get { return 228178; } }              // 1 byte (111 = loading)
        public override int LoadingMap2 { get { return 228182; } }              // 1 byte (164 = loading)
        public override int LoadingMap3 { get { return 228202; } }              // 1 byte (103 = loading)
        public override int LoadingMap4 { get { return 228206; } }              // 1 byte (156 = loading)
        public override int NumChars { get { return 351412; } }                 // 1 byte
        public override int PartyInfo { get { return 336710; } }                // (354 * 6) bytes
        public override int ActingCharacter { get { return 359168; } }          // 1 byte
        public override int ActingCaster { get { return 329245; } }             // 1 byte
        public override int CurrentMapIndex { get { return 351425; } }          // 1 byte

        public override int LastKeypress { get { return 359170; } }             // 1 byte
        public override int CurrentMapQuad { get { return 353210; } }           // 1 byte
        public override int MapAppearance1 { get { return 339188; } }           // 512 byte
        public override int MapAppearance2 { get { return 340972; } }           // 512 bytes (2 bytes per square)
        public override int MapAppearance3 { get { return 340080; } }           // 512 bytes (2 bytes per square)
        public override int MapAppearance4 { get { return 343648; } }           // 512 bytes (2 bytes per square)
        public override int MapAttributes1 { get { return 339700; } }           // 256 bytes
        public override int MapAttributes2 { get { return 341484; } }           // 256 bytes
        public override int MapAttributes3 { get { return 340592; } }           // 256 bytes
        public override int MapAttributes4 { get { return 344160; } }           // 256 bytes
        public override int MapFlags1 { get { return 339956; } }                // 124 bytes
        public override int Map1North { get { return 339958; } }                 // 1 byte
        public override int Map1East { get { return 339960; } }                  // 1 byte
        public override int Map1Flags { get { return 339966; } }                  // 1 byte
        public override int MapFlags2 { get { return 341740; } }                // 124 bytes
        public override int MapFlags3 { get { return 340848; } }                // 124 bytes
        public override int MapFlags4 { get { return 344416; } }                // 124 bytes
        public override int MapCartography1 { get { return 340048; } }          // 32 bytes
        public override int MapCartography2 { get { return 341832; } }          // 32 bytes
        public override int MapCartography3 { get { return 340940; } }          // 32 bytes
        public override int MapCartography4 { get { return 344508; } }          // 32 bytes
        public override int FacingAndLocation { get { return 351422; } }        // 3 bytes (f,x,y)
        public override int MM4MonsterData { get { return 260016; } }           // (95 * 60) bytes
        public override int MM5MonsterData { get { return 260016; } }           // (90 * 60) bytes
        public override int MonsterPositions { get { return 349272; } }         // (107 * 20) bytes maximum
        public override int ScriptsPointer { get { return 555702; } }           // 4 bytes, (*16, +32) 
        public override int ScriptsLength { get { return 330670; } }            // 2 bytes
        public override int Strings { get { return 490128; } }
        public override int StringsPointer { get { return 330680; } }           // 4 bytes
        public override int StringsLength { get { return 330666; } }            // 2 bytes
        public override int SpecialSquares { get { return 267310; } }           // 17120 bytes maximum, 10 per square
        public override int PartyStaticBits { get { return 352071; } }          // 32 bytes
        public override int PartyGold { get { return 352050; } }                // 4 bytes
        public override int PartyGems { get { return 352054; } }                // 4 bytes
        public override int PartyFood { get { return 352030; } }                // 2 bytes
        public override int BankGold { get { return 352058; } }                 // 4 bytes
        public override int BankGems { get { return 352062; } }                 // 4 bytes
        public override int Day { get { return 352024; } }                      // 1 byte
        public override int Year { get { return 352026; } }                     // 2 bytes
        public override int Minutes { get { return 352028; } }                  // 2 bytes
        public override int PartyBytes { get { return 351412; } }               // 1528 bytes
        public override int GameState1 { get { return 329712; } }               // 2 bytes
        public override int GameState2 { get { return 359154; } }               // 2 bytes
        public override int GameState3 { get { return 324830; } }               // 2 bytes
        public override int VisibleObjects { get { return 347944; } }           // (8 * 166) bytes mmaximum
        public override int WorldBits { get { return 352135; } }                // 16 bytes
        public override int QuestBits { get { return 352151; } }                // at least 6 bytes (probably 16)
        public override int CreationStats { get { return 359096; } }            // 7 bytes of stats (M/I/P/E/S/A/L)
        public override int FloorTileSet { get { return 340014; } }             // 1 byte
        public override int QuestStrings { get { return 501952; } }
        public override int InCombat { get { return 329671; } }                 // 1 byte
        public override int MovedThisRound { get { return 329313; } }           // 9 bytes
        public override int InventorySubscreen { get { return 359116; } }       // 1 byte (0-3)
        public override int SellSubscreen { get { return 358846; } }            // 1 byte (0-3)
        public override int QuestSubscreen { get { return 359050; } }           // 1 byte (0-2)
        public override int EnchantItemChar { get { return 359058; } }          // 1 byte
        public override int EnchantItemSubscreen { get { return 359110; } }     // 1 byte (0-3)
        public override int WorldSide { get { return 637662; } }                // 1 byte (0/1)
        //public override int WorldSide5 { get { return 267326; } }               // 1 byte (0/1)
        public override int WorldSideSurface { get { return 354764; } }         // 1 byte (0/1)   used only to determine exit maps
        public override int MonsterSide { get { return 260047; } }              // 1 byte (0/1)   which side's monsters are loaded
        public override int NumMonsters { get { return 330673; } }              // 1 byte (max 107)
        public override int WorldSide2 { get { return 326568; } }               // 1 byte (0/1)
        public override int WorldSide3 { get { return 329276; } }               // 1 byte (0/1)
        public override int WorldSide4 { get { return 353589; } }               // 1 byte (0/1)

        public override int CurrentStoreWeapons { get { return 339000; } }      // (9*4*4) bytes
        public override int CurrentStoreArmor { get { return 339036; } }        // (9*4*4) bytes
        public override int CurrentStoreAccessories { get { return 339072; } }  // (9*4*4) bytes
        public override int CurrentStoreMisc { get { return 339108; } }         // (9*4*4) bytes
        public override int CloudsideStore1 { get { return 351440; } }          // (9*4*4) bytes
        public override int CloudsideStore2 { get { return 351584; } }          // (9*4*4) bytes
        public override int CloudsideStore3 { get { return 351728; } }          // (9*4*4) bytes
        public override int CloudsideStore4 { get { return 351872; } }          // (9*4*4) bytes
        public override int DarksideStore1 { get { return 352244; } }           // (9*4*4) bytes
        public override int DarksideStore2 { get { return 352388; } }           // (9*4*4) bytes
        public override int DarksideStore3 { get { return 352532; } }           // (9*4*4) bytes
        public override int DarksideStore4 { get { return 352676; } }           // (9*4*4) bytes
    }

    public abstract class MM45Memory
    {
        public static byte[] MainSearch = new byte[] {
            0xFF, 0xCB, 0x56, 0x96, 0x92, 0x85, 0xC0, 0x74, 0x02, 0xF7, 0xE3, 0xE3, 0x05, 0x91, 0xF7, 0xE6 };

        public abstract int MainSearchSVN { get; }              // Address of Main Block + Main Search
        public abstract int MainSearchNonSVN1 { get; }
        public abstract int MainSearchNonSVN2 { get; }

        // Offsets based on the base rather than the main search
        public abstract int ScriptsNonSVN1 { get; }
        public abstract int ScriptsSVN { get; }

        // Offsets based on the main search delta;
        public abstract int LoadingMap1 { get; }              // 1 byte (111 = loading)
        public abstract int LoadingMap2 { get; }              // 1 byte (164 = loading)
        public abstract int LoadingMap3 { get; }              // 1 byte (103 = loading)
        public abstract int LoadingMap4 { get; }              // 1 byte (156 = loading)
        public abstract int NumChars { get; }                 // 1 byte
        public abstract int PartyInfo { get; }                // (354 * 6) bytes
        public abstract int ActingCharacter { get; }          // 1 byte
        public abstract int ActingCaster { get; }             // 1 byte
        public abstract int CurrentMapIndex { get; }          // 1 byte

        public abstract int LastKeypress { get; }             // 1 byte
        public abstract int CurrentMapQuad { get; }           // 1 byte
        public abstract int MapAppearance1 { get; }           // 512 byte
        public abstract int MapAppearance2 { get; }           // 512 bytes (2 bytes per square)
        public abstract int MapAppearance3 { get; }           // 512 bytes (2 bytes per square)
        public abstract int MapAppearance4 { get; }           // 512 bytes (2 bytes per square)
        public abstract int MapAttributes1 { get; }           // 256 bytes
        public abstract int MapAttributes2 { get; }           // 256 bytes
        public abstract int MapAttributes3 { get; }           // 256 bytes
        public abstract int MapAttributes4 { get; }           // 256 bytes
        public abstract int MapFlags1 { get; }                // 124 bytes
        public abstract int Map1North { get; }                 // 1 byte
        public abstract int Map1East { get; }                  // 1 byte
        public abstract int Map1Flags { get; }                  // 1 byte
        public abstract int MapFlags2 { get; }                // 124 bytes
        public abstract int MapFlags3 { get; }                // 124 bytes
        public abstract int MapFlags4 { get; }                // 124 bytes
        public abstract int MapCartography1 { get; }          // 32 bytes
        public abstract int MapCartography2 { get; }          // 32 bytes
        public abstract int MapCartography3 { get; }          // 32 bytes
        public abstract int MapCartography4 { get; }          // 32 bytes
        public abstract int FacingAndLocation { get; }        // 3 bytes (f,x,y)
        public abstract int MM4MonsterData { get; }           // (95 * 60) bytes
        public abstract int MM5MonsterData { get; }           // (90 * 60) bytes
        public abstract int MonsterPositions { get; }         // (107 * 20) bytes maximum
        public abstract int ScriptsPointer { get; }           // 4 bytes, (*16, +32) 
        public abstract int ScriptsLength { get; }            // 2 bytes
        public abstract int Strings { get; }
        public abstract int StringsPointer { get; }           // 4 bytes
        public abstract int StringsLength { get; }            // 2 bytes
        public abstract int SpecialSquares { get; }           // 17120 bytes maximum, 10 per square
        public abstract int PartyStaticBits { get; }          // 32 bytes
        public abstract int PartyGold { get; }                // 4 bytes
        public abstract int PartyGems { get; }                // 4 bytes
        public abstract int PartyFood { get; }                // 2 bytes
        public abstract int BankGold { get; }                 // 4 bytes
        public abstract int BankGems { get; }                 // 4 bytes
        public abstract int Day { get; }                      // 1 byte
        public abstract int Year { get; }                     // 2 bytes
        public abstract int Minutes { get; }                  // 2 bytes
        public abstract int PartyBytes { get; }               // 1528 bytes
        public abstract int GameState1 { get; }               // 2 bytes
        public abstract int GameState2 { get; }               // 2 bytes
        public abstract int GameState3 { get; }               // 2 bytes
        public abstract int VisibleObjects { get; }           // (8 * 166) bytes mmaximum
        public abstract int WorldBits { get; }                // 16 bytes
        public abstract int QuestBits { get; }                // at least 6 bytes (probably 16)
        public abstract int CreationStats { get; }            // 7 bytes of stats (M/I/P/E/S/A/L)
        public abstract int FloorTileSet { get; }             // 1 byte
        public abstract int QuestStrings { get; }
        public abstract int InCombat { get; }                 // 1 byte
        public abstract int MovedThisRound { get; }           // 9 bytes
        public abstract int InventorySubscreen { get; }       // 1 byte (0-3)
        public abstract int SellSubscreen { get; }            // 1 byte (0-3)
        public abstract int QuestSubscreen { get; }           // 1 byte (0-2)
        public abstract int EnchantItemChar { get; }          // 1 byte
        public abstract int EnchantItemSubscreen { get; }     // 1 byte (0-3)
        public abstract int WorldSide { get; }                // 1 byte (0/1)
        //public abstract int WorldSide5 { get; }               // 1 byte (0/1)
        public abstract int WorldSideSurface { get; }         // 1 byte (0/1)   used only to determine exit maps
        public abstract int MonsterSide { get; }              // 1 byte (0/1)   which side's monsters are loaded
        public abstract int NumMonsters { get; }              // 1 byte (max 107)
        public abstract int WorldSide2 { get; }               // 1 byte (0/1)
        public abstract int WorldSide3 { get; }               // 1 byte (0/1)
        public abstract int WorldSide4 { get; }               // 1 byte (0/1)

        public abstract int CurrentStoreWeapons { get; }      // (9*4*4) bytes
        public abstract int CurrentStoreArmor { get; }        // (9*4*4) bytes
        public abstract int CurrentStoreAccessories { get; }  // (9*4*4) bytes
        public abstract int CurrentStoreMisc { get; }         // (9*4*4) bytes
        public abstract int CloudsideStore1 { get; }          // (9*4*4) bytes
        public abstract int CloudsideStore2 { get; }          // (9*4*4) bytes
        public abstract int CloudsideStore3 { get; }          // (9*4*4) bytes
        public abstract int CloudsideStore4 { get; }          // (9*4*4) bytes
        public abstract int DarksideStore1 { get; }           // (9*4*4) bytes
        public abstract int DarksideStore2 { get; }           // (9*4*4) bytes
        public abstract int DarksideStore3 { get; }           // (9*4*4) bytes
        public abstract int DarksideStore4 { get; }           // (9*4*4) bytes

        public const int MonsterCurrentHP = 18;
        public const int MonsterCondition = 7;              // 0: good, 7:paralyzed, 15:sleep
        public const int MonsterCurrentIndex = 6;
        public const int MonsterLocalIndex = 3;
        public const int MonsterActivated = 4;
        public const int MonsterCurrentX = 0;
        public const int MonsterCurrentY = 1;

        public const int CharItemsWeapons = 166;
        public const int CharItemsArmor = 202;
        public const int CharItemsAccessories = 238;
        public const int CharItemsMisc = 274;

        public const int DarksideScriptCorrection = 0x3c000;    // ???
        public const int WoXCDR = 814645;                       // "CD-R" if this is the full-voice version

        public static MemoryGuess[] Guesses { get { return Global.MemoryGuesses.Guesses[GameNames.MightAndMagic45]; } }
    }

    public class MM45PartyInfo : PartyInfo
    {
        public override byte[] QuestBytes
        {
            get
            {
                if (Bytes == null || Bytes.Length < (CharacterSize * NumChars))
                    return null;

                MemoryStream stream = new MemoryStream(121 * NumChars + 14);
                stream.WriteByte(ActingChar);
                // Spells, skills, permanent stats, permanent resistances and awards are relevant to quests.  Other bytes not so much.
                for (int i = 0; i < NumChars; i++)
                {
                    stream.WriteByte(Bytes[i * CharacterSize + MM45.Offsets.Might]);
                    stream.WriteByte(Bytes[i * CharacterSize + MM45.Offsets.Intellect]);
                    stream.WriteByte(Bytes[i * CharacterSize + MM45.Offsets.Personality]);
                    stream.WriteByte(Bytes[i * CharacterSize + MM45.Offsets.Endurance]);
                    stream.WriteByte(Bytes[i * CharacterSize + MM45.Offsets.Speed]);
                    stream.WriteByte(Bytes[i * CharacterSize + MM45.Offsets.Accuracy]);
                    stream.WriteByte(Bytes[i * CharacterSize + MM45.Offsets.Luck]);
                    stream.WriteByte(Bytes[i * CharacterSize + MM45.Offsets.FireResist]);
                    stream.WriteByte(Bytes[i * CharacterSize + MM45.Offsets.ColdResist]);
                    stream.WriteByte(Bytes[i * CharacterSize + MM45.Offsets.ElecResist]);
                    stream.WriteByte(Bytes[i * CharacterSize + MM45.Offsets.PoisonResist]);
                    stream.WriteByte(Bytes[i * CharacterSize + MM45.Offsets.EnergyResist]);
                    stream.WriteByte(Bytes[i * CharacterSize + MM45.Offsets.MagicResist]);
                    stream.Write(Bytes, i * CharacterSize + MM45.Offsets.Skills, 121);  // Secondary skills, Awards, Spells
                }
                return stream.ToArray();
            }
        }

        public MM45PartyInfo(byte[] bytes, byte numchars, byte actingChar)
        {
            Bytes = bytes;
            NumChars = numchars;
            ActingChar = actingChar;

            Positions = new byte[numchars];
            Addresses = new int[numchars];
            for (byte i = 0; i < numchars; i++)
            {
                Positions[i] = i;
                Addresses[i] = i;
            }
        }

        public int CharsWithStatus(BasicConditionFlags cond)
        {
            int iCount = 0;
            if (Bytes == null || Bytes.Length < (CharacterSize * NumChars))
                return iCount;
            for (int iChar = 0; iChar < NumChars; iChar++)
            {
                if (Bytes[iChar * CharacterSize + MM45Character.ConditionOffset(cond)] > 0)
                    iCount++;
            }
            return iCount;
        }

        public bool HasAward(MM45AwardIndex index)
        {
            if (Bytes == null || Bytes.Length < (CharacterSize * NumChars))
                return false;
            for (int iChar = 0; iChar < NumChars; iChar++)
            {
                if (MM45Awards.IsSetFromByte(Bytes, iChar * CharacterSize + MM45.Offsets.Awards, index))
                    return true;
            }
            return false;
        }

        public override int CharacterSize { get { return MM45Character.SizeInBytes; } }
    }

    public class MM45GameInfoItem : GameInfoItem
    {
        public override MapTitleInfo GetMapTitlePair(int iMap) { return MM45MemoryHacker.GetMapTitlePair(iMap % 256, iMap / 256); }

        public MM45GameInfoItem(string desc, object val, int offset, DataType type = DataType.Auto, UInt32 mask = 0, ShowStyle style = ShowStyle.Editable, BitDescriptionDelegate fn = null)
            : base(desc, val, offset, type, mask, style, fn)
        {
        }

        public MM45GameInfoItem(string desc, object val, int offset, BitDescriptionDelegate fn)
            : base(desc, val, offset, DataType.Bits, 0, ShowStyle.Editable, fn)
        {
        }

        public MM45GameInfoItem(string desc, ByteDescriptionDelegate fn, byte[] val, int offset)
            : base(desc, fn, val, offset)
        {
        }

        public override string FacingString(int i, bool bAbbrev = false) { return MM45MemoryHacker.FacingString(i, bAbbrev); }
    }

    public class MM45GameInfo : MM345GameInfo
    {
        public MM45MapBytes Map;
        public MM45PartyBytes Party;
        public MM45Memory Memory = new MM45NonVoiceMemory();

        public override GameNames Game { get { return GameNames.MightAndMagic45; } }

        public override List<GameInfoItem> GetPartyItems()
        {
            int offset = Memory.PartyBytes;

            List<GameInfoItem> items = new List<GameInfoItem>();

            items.Add(new MM45GameInfoItem("Gold", Party.Gold, offset + MM45PartyBytes.OffsetGold));
            items.Add(new MM45GameInfoItem("Gems", Party.Gems, offset + MM45PartyBytes.OffsetGems));
            items.Add(new MM45GameInfoItem("Food", Party.Food, offset + MM45PartyBytes.OffsetFood));
            items.Add(new MM45GameInfoItem("Bank Gold", Party.BankGold, offset + MM45PartyBytes.OffsetBankGold));
            items.Add(new MM45GameInfoItem("Bank Gems", Party.BankGems, offset + MM45PartyBytes.OffsetBankGems));
            items.Add(new MM45GameInfoItem("Levitate", Party.Levitate, offset + MM45PartyBytes.OffsetLevitate, DataType.Boolean));
            items.Add(new MM45GameInfoItem("Clairvoyance", Party.Clairvoyance, offset + MM45PartyBytes.OffsetClairvoyance, DataType.Boolean));
            items.Add(new MM45GameInfoItem("Water Walk", Party.WalkOnWater, offset + MM45PartyBytes.OffsetWalkOnWater, DataType.Boolean));
            items.Add(new MM45GameInfoItem("Blessed", Party.Blessed, offset + MM45PartyBytes.OffsetBlessed));
            items.Add(new MM45GameInfoItem("Power Shield", Party.PowerShield, offset + MM45PartyBytes.OffsetPowerShield));
            items.Add(new MM45GameInfoItem("Holy Bonus", Party.HolyBonus, offset + MM45PartyBytes.OffsetHolyBonus));
            items.Add(new MM45GameInfoItem("Heroism", Party.Heroism, offset + MM45PartyBytes.OffsetHeroism));
            items.Add(new MM45GameInfoItem("Light", Party.Light, offset + MM45PartyBytes.OffsetLight));
            items.Add(new MM45GameInfoItem("Fire Res", Party.ResistFire, offset + MM45PartyBytes.OffsetResistFire));
            items.Add(new MM45GameInfoItem("Elec Res", Party.ResistElec, offset + MM45PartyBytes.OffsetResistElec));
            items.Add(new MM45GameInfoItem("Cold Res", Party.ResistCold, offset + MM45PartyBytes.OffsetResistCold));
            items.Add(new MM45GameInfoItem("Poison Res", Party.ResistPoison, offset + MM45PartyBytes.OffsetResistPoison));
            items.Add(new MM45GameInfoItem("Bits: Clouds", Party.PartyBits1, offset + MM45PartyBytes.OffsetPartyBits1, MM45Bits.CloudsDescription));
            items.Add(new MM45GameInfoItem("Bits: Dark", Party.PartyBits2, offset + MM45PartyBytes.OffsetPartyBits2, MM45Bits.DarkDescription));
            items.Add(new MM45GameInfoItem("Bits: World", Party.WorldBits, offset + MM45PartyBytes.OffsetNoteBits, MM45Bits.WorldDescription));
            items.Add(new MM45GameInfoItem("Bits: Quest", Party.QuestBits, offset + MM45PartyBytes.OffsetQuestBits, MM45Bits.QuestDescription));
            items.Add(new MM45GameInfoItem("Bits: Char", Party.CharBits, offset + MM45PartyBytes.OffsetCharBits, MM45Bits.CharDescription));
            items.Add(new MM45GameInfoItem("Quest Items", MM45Bits.QuestItemDescription, Party.QuestItems, offset + MM45PartyBytes.OffsetQuestItems));
            if (Global.Debug)
            {
                items.Add(new MM45GameInfoItem("TimePlayed", Party.SecondsPlayed, offset + MM45PartyBytes.OffsetSecondsPlayed));
                items.Add(new MM45GameInfoItem("Automap", Party.Automap, offset + MM45PartyBytes.OffsetAutomap, DataType.Boolean));
                items.Add(new MM45GameInfoItem("WizardEye", Party.WizardEye, offset + MM45PartyBytes.OffsetWizardEye, DataType.Boolean));
                items.Add(new MM45GameInfoItem("PartySize", Party.PartyCount, offset + MM45PartyBytes.OffsetPartyCount, DataType.Auto, 0, GameInfoItem.ShowStyle.Visible));
                items.Add(new MM45GameInfoItem("PlayerChars", Party.RealPartyCount, offset + MM45PartyBytes.OffsetRealPartyCount, DataType.Auto, 0, GameInfoItem.ShowStyle.Visible));
                items.Add(new MM45GameInfoItem("CharIndices", Party.arrayPartyMembers, offset + MM45PartyBytes.OffsetPartyMembers, DataType.Auto, 0, GameInfoItem.ShowStyle.Visible));
                items.Add(new MM45GameInfoItem("Facing", Party.Facing, offset + MM45PartyBytes.OffsetFacing, DataType.Facing));
                items.Add(new MM45GameInfoItem("Location:X", Party.LocationX, offset + MM45PartyBytes.OffsetLocationX));
                items.Add(new MM45GameInfoItem("Location:Y", Party.LocationY, offset + MM45PartyBytes.OffsetLocationY));
                items.Add(new MM45GameInfoItem("MapIndex", Party.MapIndex, offset + MM45PartyBytes.OffsetMapIndex, DataType.Auto, 0, GameInfoItem.ShowStyle.Visible));
                items.Add(new MM45GameInfoItem("Delay", Party.Delay, offset + MM45PartyBytes.OffsetDelay));
                items.Add(new MM45GameInfoItem("LastMap", Party.LastMap, offset + MM45PartyBytes.OffsetLastMap));
                items.Add(new MM45GameInfoItem("Difficulty", Party.Difficulty, offset + MM45PartyBytes.OffsetDifficulty));
                items.Add(new MM45GameInfoItem("Weapons1", Party.ShopWeapons, offset + MM45PartyBytes.OffsetShopWeapons));
                items.Add(new MM45GameInfoItem("Armor1", Party.ShopArmor, offset + MM45PartyBytes.OffsetShopArmor));
                items.Add(new MM45GameInfoItem("Access1", Party.ShopAccessories, offset + MM45PartyBytes.OffsetShopAccessories));
                items.Add(new MM45GameInfoItem("Misc1", Party.ShopMisc, offset + MM45PartyBytes.OffsetShopMisc));
                items.Add(new MM45GameInfoItem("CloudsEnd", Party.CloudsEnd, offset + MM45PartyBytes.OffsetCloudsEnd, DataType.Boolean));
                items.Add(new MM45GameInfoItem("DarksideEnd", Party.DarksideEnd, offset + MM45PartyBytes.OffsetDarksideEnd, DataType.Boolean));
                items.Add(new MM45GameInfoItem("WorldEnd", Party.WorldEnd, offset + MM45PartyBytes.OffsetWorldEnd, DataType.Boolean));
                items.Add(new MM45GameInfoItem("StepCounter", Party.StepCounter, offset + MM45PartyBytes.OffsetStepCounter));
                items.Add(new MM45GameInfoItem("Torch", Party.Torch, offset + MM45PartyBytes.OffsetTorch));
                items.Add(new MM45GameInfoItem("Rested", Party.Rested, offset + MM45PartyBytes.OffsetRested));
                items.Add(new MM45GameInfoItem("Weapons2", Party.ShopWeapons2, offset + MM45PartyBytes.OffsetShopWeapons2));
                items.Add(new MM45GameInfoItem("Armor2", Party.ShopArmor2, offset + MM45PartyBytes.OffsetShopArmor2));
                items.Add(new MM45GameInfoItem("Access2", Party.ShopAccessories2, offset + MM45PartyBytes.OffsetShopAccessories2));
                items.Add(new MM45GameInfoItem("Misc2", Party.ShopMisc2, offset + MM45PartyBytes.OffsetShopMisc2));
                items.Add(new MM45GameInfoItem("SoundFlag", Party.SoundFlag, offset + MM45PartyBytes.OffsetSoundFlag, DataType.Boolean));
                items.Add(new MM45GameInfoItem("MusicFlag", Party.MusicFlag, offset + MM45PartyBytes.OffsetMusicFlag, DataType.Boolean));
                items.Add(new MM45GameInfoItem("Unknown", Party.Unknown, offset + MM45PartyBytes.OffsetUnknown));
            }

            return items;
        }

        public override List<GameInfoItem> GetMiscItems()
        {
            int offsetMap = Memory.MapFlags1;
            int offsetParty = Memory.PartyBytes;

            List<GameInfoItem> items = new List<GameInfoItem>();

            items.Add(new MM45GameInfoItem("Ban Portal", Map.Flags, offsetMap + MM45MapBytes.OffsetFlags, DataType.Boolean, (UInt32)MM45MapFlags.TownPortal));
            items.Add(new MM45GameInfoItem("Ban Etherealize", Map.Flags, offsetMap + MM45MapBytes.OffsetFlags, DataType.Boolean, (UInt32)MM45MapFlags.Etherealize));
            items.Add(new MM45GameInfoItem("Ban Shelter", Map.Flags, offsetMap + MM45MapBytes.OffsetFlags, DataType.Boolean, (UInt32)MM45MapFlags.SuperShelter));
            items.Add(new MM45GameInfoItem("Ban Distortion", Map.Flags, offsetMap + MM45MapBytes.OffsetFlags, DataType.Boolean, (UInt32)MM45MapFlags.TimeDistortion));
            items.Add(new MM45GameInfoItem("Ban Beacon", Map.Flags, offsetMap + MM45MapBytes.OffsetFlags, DataType.Boolean, (UInt32)MM45MapFlags.LloydsBeacon));
            items.Add(new MM45GameInfoItem("Ban Teleport", Map.Flags, offsetMap + MM45MapBytes.OffsetFlags, DataType.Boolean, (UInt32)MM45MapFlags.Teleport));
            items.Add(new MM45GameInfoItem("Ban Resting", Map.Flags, offsetMap + MM45MapBytes.OffsetFlags, DataType.Boolean, (UInt32)MM45MapFlags.Rest));
            items.Add(new MM45GameInfoItem("Ban Saving", Map.Flags, offsetMap + MM45MapBytes.OffsetFlags, DataType.Boolean, (UInt32)MM45MapFlags.Save));
            items.Add(new MM45GameInfoItem("Year", Party.Year, offsetParty + MM45PartyBytes.OffsetYear));
            items.Add(new MM45GameInfoItem("Day", Party.Day, offsetParty + MM45PartyBytes.OffsetDay));
            items.Add(new MM45GameInfoItem("Time", Party.Minutes, offsetParty + MM45PartyBytes.OffsetMinutes, DataType.Time));
            items.Add(new MM45GameInfoItem("Dark", Map.Flags, offsetMap + MM45MapBytes.OffsetFlags, DataType.Boolean, (UInt32)MM45MapFlags.Dark));
            items.Add(new MM45GameInfoItem("Outdoor", Map.Flags, offsetMap + MM45MapBytes.OffsetFlags, DataType.Boolean, (UInt32)MM45MapFlags.Outdoor));
            items.Add(new MM45GameInfoItem("Lock Strength", Map.LockStrength, offsetMap + MM45MapBytes.OffsetLockStrength));
            items.Add(new MM45GameInfoItem("Chest Strength", Map.ChestStrength, offsetMap + MM45MapBytes.OffsetChestStrength));
            items.Add(new MM45GameInfoItem("Door Strength", Map.DoorStrength, offsetMap + MM45MapBytes.OffsetDoorStrength));
            items.Add(new MM45GameInfoItem("Grate Strength", Map.GrateStrength, offsetMap + MM45MapBytes.OffsetGrateStrength));
            items.Add(new MM45GameInfoItem("Wall Strength", Map.WallStrength, offsetMap + MM45MapBytes.OffsetWallStrength));
            items.Add(new MM45GameInfoItem("Trap Strength", Map.TrapStrength, offsetMap + MM45MapBytes.OffsetTrapStrength));
            items.Add(new MM45GameInfoItem("Escape", Map.EscapeChance, offsetMap + MM45MapBytes.OffsetEscapeChance));
            items.Add(new MM45GameInfoItem("Deaths", Party.Deathcount, offsetParty + MM45PartyBytes.OffsetDeathcount));
            items.Add(new MM45GameInfoItem("Wins", Party.WinCount, offsetParty + MM45PartyBytes.OffsetWinCount));
            items.Add(new MM45GameInfoItem("Losses", Party.LossCount, offsetParty + MM45PartyBytes.OffsetLossCount));
            if (Global.Debug)
            {
                items.Add(new MM45GameInfoItem("Walls", Map.Walls, offsetMap + MM45MapBytes.OffsetWalls));
                items.Add(new MM45GameInfoItem("Floors", Map.Floors, offsetMap + MM45MapBytes.OffsetFloors));
                items.Add(new MM45GameInfoItem("Floor", Map.DefaultFloor, offsetMap + MM45MapBytes.OffsetDefaultFloor));
                items.Add(new MM45GameInfoItem("RunX", Map.RunX, offsetMap + MM45MapBytes.OffsetRunX));
                items.Add(new MM45GameInfoItem("RunY", Map.RunY, offsetMap + MM45MapBytes.OffsetRunY));
                items.Add(new MM45GameInfoItem("WallMax", Map.WallBlocked, offsetMap + MM45MapBytes.OffsetWallBlocked));
                items.Add(new MM45GameInfoItem("FloorMax", Map.FloorBlocked, offsetMap + MM45MapBytes.OffsetFloorBlocked));
                items.Add(new MM45GameInfoItem("WallType", Map.WallType, offsetMap + MM45MapBytes.OffsetWallType));
                items.Add(new MM45GameInfoItem("Tavern", Map.TavernOffset, offsetMap + MM45MapBytes.OffsetTavernOffset));
                items.Add(new MM45GameInfoItem("Seen", Map.CartographySeen, offsetMap + MM45MapBytes.OffsetCartographySeen));
                items.Add(new MM45GameInfoItem("Explored", Map.CartographyExplored, offsetMap + MM45MapBytes.OffsetCartographyExplored));
                items.Add(new MM45GameInfoItem("Flags", (UInt32)Map.Flags, offsetMap + MM45MapBytes.OffsetFlags, MM45Bits.MapFlagsDescription));
            }

            return items;
        }

        public override List<GameInfoItem> GetMapItems()
        {
            int offset = Memory.MapFlags1;

            List<GameInfoItem> items = new List<GameInfoItem>();

            items.Add(new MM45GameInfoItem("Current Map", Map.SideMapIndex, offset + MM45MapBytes.OffsetMapIndex, DataType.Map16, 0, GameInfoItem.ShowStyle.Visible));
            if (Global.Debug)
            {
                items.Add(new MM45GameInfoItem("WorldSide", Map.Side, offset + MM45MapBytes.OffsetSide, DataType.Byte, 0, GameInfoItem.ShowStyle.Visible));
                items.Add(new MM45GameInfoItem("MapQuad", Map.Quad, offset + MM45MapBytes.OffsetQuad, DataType.Byte, 0, GameInfoItem.ShowStyle.Visible));
                items.Add(new MM45GameInfoItem("MapNorth", Map.SideMapNorth, offset + MM45MapBytes.OffsetMapNorth, DataType.Map16));
                items.Add(new MM45GameInfoItem("MapEast", Map.SideMapEast, offset + MM45MapBytes.OffsetMapEast, DataType.Map16));
                items.Add(new MM45GameInfoItem("MapSouth", Map.SideMapSouth, offset + MM45MapBytes.OffsetMapSouth, DataType.Map16));
                items.Add(new MM45GameInfoItem("MapWest", Map.SideMapWest, offset + MM45MapBytes.OffsetMapWest, DataType.Map16));
            }

            return items;
        }

        public MM45GameInfo(MM45MemoryHacker hacker)
        {
            Memory = hacker.Memory;
        }

        public MM45GameInfo(MM45MemoryHacker hacker, byte[] map, byte[] party)
        {
            Memory = hacker.Memory;
            SetMapBytes(map);
            SetPartyBytes(party);
            Bytes = Global.Combine(RawMap, RawParty);
        }

        public void SetMapBytes(byte[] bytes)
        {
            RawMap = bytes;
            Map = new MM45MapBytes(bytes);
        }

        public void SetPartyBytes(byte[] bytes)
        {
            RawParty = bytes;
            Party = new MM45PartyBytes(bytes);
            Time = Party.GameTime;
        }

        public override string GameTime(bool bFull)
        {
            return GetGameTimeString(Party.GameTime, bFull);
        }
    }

    public enum MM4Map
    {
        None = 0,
        A1Surface = 1,
        A2Surface = 2,
        A3Surface = 3,
        A4Surface = 4,
        B1Surface = 5,
        B2Surface = 6,
        B3Surface = 7,
        B4Surface = 8,
        C1Surface = 9,
        C2Surface = 10,
        C3Surface = 11,
        C4Surface = 12,
        D1Surface = 13,
        D2Surface = 14,
        D3Surface = 15,
        D4Surface = 16,
        E1Surface = 17,
        E2Surface = 18,
        E3Surface = 19,
        E4Surface = 20,
        F1Surface = 21,
        F2Surface = 22,
        F3Surface = 23,
        F4Surface = 24,
        F4WitchClouds = 25,
        C4HighMagicClouds = 26,
        D3CloudsOfXeen = 27,
        F3Vertigo = 28,
        D4Nightshadow = 29,
        C3Rivercity = 30,
        C2Asp = 31,
        A3Winterkill = 32,
        F3DwarfMine1 = 33,
        F3DwarfMine2 = 34,
        E2DwarfMine3 = 35,
        E2DwarfMine4 = 36,
        D2DwarfMine5 = 37,
        DeepMineAlpha = 38,
        DeepMineTheta = 39,
        DeepMineKappa = 40,
        DeepMineOmega = 41,
        B4CaveOfIllusionLevel1 = 42,
        B4CaveOfIllusionLevel2 = 43,
        B4CaveOfIllusionLevel3 = 44,
        B4CaveOfIllusionLevel4 = 45,
        E1VolcanoCaveLevel1 = 46,
        E1VolcanoCaveLevel2 = 47,
        E1VolcanoCaveLevel3 = 48,
        E1ShangriLa = 49,
        E1DragonCave = 50,
        F4WitchTowerLevel1 = 51,
        F4WitchTowerLevel2 = 52,
        F4WitchTowerLevel3 = 53,
        F4WitchTowerLevel4 = 54,
        C3TowerofHighMagicLevel1 = 55,
        C3TowerofHighMagicLevel2 = 56,
        C3TowerofHighMagicLevel3 = 57,
        C3TowerofHighMagicLevel4 = 58,
        D3DarzogsTowerLevel1 = 59,
        D3DarzogsTowerLevel2 = 60,
        D3DarzogsTowerLevel3 = 61,
        D3DarzogsTowerLevel4 = 62,
        D2BurlockDungeon = 63,
        D2CastleBurlockLevel1 = 64,
        D2CastleBurlockLevel2 = 65,
        D2CastleBurlockLevel3 = 66,
        A1BasenjiDungeon = 67,
        A1CastleBasenjiLevel1 = 68,
        A1CastleBasenjiLevel2 = 69,
        A1CastleBasenjiLevel3 = 70,
        C4NewcastleDungeon = 71,
        C4NewcastleFoundation = 72,
        C4NewcastleLevel1 = 73,
        C4NewcastleLevel2 = 74,
        D3XeensCastleLevel1 = 75,
        D3XeensCastleLevel2 = 76,
        D3XeensCastleLevel3 = 77,
        D3XeensCastleLevel4 = 78,
        E4AncientTempleOfYak = 79,
        C4TombOfAThousandTerrors = 80,
        B4GolemDungeon = 81,
        B1SphinxBody = 82,
        B1SphinxHead = 83,
        B1SphinxDungeon = 84,
        B2TheWarzone = 85,
        LastMain = 86,
        F4WitchCloudsNorth = 100,
        F4WitchCloudsEast = 101,
        F4WitchCloudsNorthEast = 102,
        C4HighMagicCloudsNorth = 103,
        C4HighMagicCloudsEast = 104,
        C4HighMagicCloudsNorthEast = 105,
        D3CloudsOfXeenNorth = 106,
        D3CloudsOfXeenEast = 107,
        D3CloudsOfXeenNorthEast = 108,
        F3VertigoNorth = 109,
        F3VertigoEast = 110,
        F3VertigoNorthEast = 111,
        C3RivercityNorth = 112,
        C3RivercityEast = 113,
        C3RivercityNorthEast = 114,
        F3DwarfMine1North = 115,
        F3DwarfMine2North = 116,
        E2DwarfMine3East = 117,
        DeepMineAlphaNorth = 118,
        DeepMineAlphaEast = 119,
        DeepMineAlphaNorthEast = 120,
        DeepMineThetaNorth = 121,
        DeepMineThetaEast = 122,
        DeepMineThetaNorthEast = 123,
        DeepMineKappaNorth = 124,
        DeepMineKappaEast = 125,
        DeepMineKappaNorthEast = 126,
        DeepMineOmegaNorth = 127,
        DeepMineOmegaEast = 128,
        DeepMineOmegaNorthEast = 129,
        E1DragonCaveNorth = 130,
        E1DragonCaveEast = 131,
        E1DragonCaveNorthEast = 132,
        E4AncientTempleofYakNorth = 133,
        E4AncientTempleofYakEast = 134,
        E4AncientTempleofYakNorthEast = 135,
        C4TombOfAThousandTerrorsNorth = 136,
        C4TombOfAThousandTerrorsEast = 137,
        C4TombOfAThousandTerrorsNorthEast = 138,
        B4GolemDungeonNorth = 139,
        B4GolemDungeonEast = 140,
        B4GolemDungeonNorthEast = 141,
        B1SphinxBodyNorth = 142,
        LastSub = 143,
        Unknown = -1
    }

    public enum MM5Map
    {
        None = 0,
        A1Surface = 1,
        A2Surface = 2,
        A3Surface = 3,
        A4Surface = 4,
        B1Surface = 5,
        B2Surface = 6,
        B3Surface = 7,
        B4Surface = 8,
        C1Surface = 9,
        C2Surface = 10,
        C3Surface = 11,
        C4Surface = 12,
        D1Surface = 13,
        D2Surface = 14,
        D3Surface = 15,
        D4Surface = 16,
        E1Surface = 17,
        E2Surface = 18,
        E3Surface = 19,
        E4Surface = 20,
        F1Surface = 21,
        F2Surface = 22,
        F3Surface = 23,
        F4Surface = 24,
        A1ElementalPlaneOfFire = 25,
        F1ElementalPlaneOfAir = 26,
        F4ElementalPlaneOfEarth = 27,
        A4ElementalPlaneOfWater = 28,
        A4Castleview = 29,
        A4CastleviewSewer = 30,
        E3Sandcaster = 31,
        E3SandcasterSewer = 32,
        F2Lakeside = 33,
        F2LakesideSewer = 34,
        B2Necropolis = 35,
        B2NecropolisSewer = 36,
        C2Olympus = 37,
        C2OlympusSewer = 38,
        B2GemstoneMines = 39,
        E4TrollHoles = 40,
        A4CastleKalindraLevel1 = 41,
        A4CastleKalindraLevel2 = 42,
        A4CastleKalindraLevel3 = 43,
        A4CastleKalindraDungeon = 44,
        F1CastleBlackfangLevel1 = 45,
        F1CastleBlackfangLevel2 = 46,
        F1CastleBlackfangLevel3 = 47,
        F1CastleBlackfangDungeon = 48,
        A1CastleAlamarLevel1 = 49,
        A1CastleAlamarLevel2 = 50,
        A1CastleAlamarLevel3 = 51,
        A1CastleAlamarDungeon = 52,
        A4EllingersTowerLevel1 = 53,
        A4EllingersTowerLevel2 = 54,
        A4EllingersTowerLevel3 = 55,
        A4EllingersTowerLevel4 = 56,
        A3WesternTowerLevel1 = 57,
        A3WesternTowerLevel2 = 58,
        A3WesternTowerLevel3 = 59,
        A3WesternTowerLevel4 = 60,
        D4SouthernTowerLevel1 = 61,
        D4SouthernTowerLevel2 = 62,
        D4SouthernTowerLevel3 = 63,
        D4SouthernTowerLevel4 = 64,
        F3EasternTowerLevel1 = 65,
        F3EasternTowerLevel2 = 66,
        F3EasternTowerLevel3 = 67,
        F3EasternTowerLevel4 = 68,
        D1NorthernTowerLevel1 = 69,
        D1NorthernTowerLevel2 = 70,
        D1NorthernTowerLevel3 = 71,
        D1NorthernTowerLevel4 = 72,
        C4TempleOfBarkLevel1 = 73,
        C4TempleOfBarkLevel2 = 74,
        C4TempleOfBarkLevel3 = 75,
        C4TempleOfBarkLevel4 = 76,
        C4TempleOfBarkLevel5 = 77,
        F2LostSoulsDungeonLevel1 = 78,
        F2LostSoulsDungeonLevel3 = 79,
        F2LostSoulsDungeonLevel2 = 80,
        F2LostSoulsDungeonLevel4 = 81,
        F2LostSoulsDungeonLevel5 = 82,
        D2TheGreatPyramidLevel1 = 83,
        D2TheGreatPyramidLevel2 = 84,
        D2TheGreatPyramidLevel3 = 85,
        D2TheGreatPyramidLevel4 = 86,
        B2EscapePod1 = 87,
        B1EscapePod2 = 88,
        A1SkyroadA1 = 89,
        A2SkyroadA2 = 90,
        A3SkyroadA3 = 91,
        A4SkyroadA4 = 92,
        B1SkyroadB1 = 93,
        B2SkyroadB2 = 94,
        B3SkyroadB3 = 95,
        B4SkyroadB4 = 96,
        C1SkyroadC1 = 97,
        C2SkyroadC2 = 98,
        C3SkyroadC3 = 99,
        C4SkyroadC4 = 100,
        D1SkyroadD1 = 101,
        D2SkyroadD2 = 102,
        D3SkyroadD3 = 103,
        D4SkyroadD4 = 104,
        E1SkyroadE1 = 105,
        E2SkyroadE2 = 106,
        E3SkyroadE3 = 107,
        E4SkyroadE4 = 108,
        F1SkyroadF1 = 109,
        F2SkyroadF2 = 110,
        F3SkyroadF3 = 111,
        F4SkyroadF4 = 112,
        B3DarkstoneTowerLevel1 = 113,
        B3DarkstoneTowerLevel2 = 114,
        B3DarkstoneTowerLevel3 = 115,
        B3DarkstoneTowerLevel4 = 116,
        D1DragonTowerLevel1 = 117,
        D1DragonTowerLevel2 = 118,
        D1DragonTowerLevel3 = 119,
        D1DragonTowerLevel4 = 120,
        E3DungeonOfDeathLevel1 = 121,
        E3DungeonOfDeathLevel2 = 122,
        E3DungeonOfDeathLevel3 = 123,
        E3DungeonOfDeathLevel4 = 124,
        A2SouthernSphinxLevel1 = 125,
        A2SouthernSphinxLevel2 = 126,
        A2SouthernSphinxLevel3 = 127,
        D1DragonClouds = 128,
        B3CloudsOfTheAncients = 129,
        LastMain = 130,
        A4CastleviewNorth = 130,
        A4CastleviewEast = 131,
        A4CastleviewNorthEast = 132,
        A4CastleviewSewerNorth = 133,
        A4CastleviewSewerEast = 134,
        A4CastleviewSewerNorthEast = 135,
        E3SandcasterNorth = 136,
        E3SandcasterEast = 137,
        E3SandcasterNorthEast = 138,
        E3SandcasterSewerNorth = 139,
        E3SandcasterSewerEast = 140,
        E3SandcasterSewerNorthEast = 141,
        B2GemstoneMinesNorth = 142,
        B2GemstoneMinesEast = 143,
        B2GemstoneMinesNorthEast = 144,
        E4TrollHolesNorth = 145,
        E4TrollHolesEast = 146,
        E4TrollHolesNorthEast = 147,
        C4TempleOfBarkLevel5North = 148,
        C4TempleOfBarkLevel5East = 149,
        C4TempleOfBarkLevel5NorthEast = 150,
        F2LostSoulsDungeonLevel5North = 151,
        F2LostSoulsDungeonLevel5East = 152,
        F2LostSoulsDungeonLevel5NorthEast = 153,
        D2TheGreatPyramidLevel1North = 154,
        D2TheGreatPyramidLevel1East = 155,
        D2TheGreatPyramidLevel1NorthEast = 156,
        D2TheGreatPyramidLevel2North = 157,
        D2TheGreatPyramidLevel2East = 158,
        D2TheGreatPyramidLevel2NorthEast = 159,
        D2TheGreatPyramidLevel3North = 160,
        D2TheGreatPyramidLevel3East = 161,
        D2TheGreatPyramidLevel3NorthEast = 162,
        B2EscapePod1East = 163,
        B1EscapePod2East = 164,
        E3DungeonOfDeathLevel1North = 165,
        E3DungeonOfDeathLevel1East = 166,
        E3DungeonOfDeathLevel1NorthEast = 167,
        E3DungeonOfDeathLevel2North = 168,
        E3DungeonOfDeathLevel2East = 169,
        E3DungeonOfDeathLevel2NorthEast = 170,
        E3DungeonOfDeathLevel3North = 171,
        E3DungeonOfDeathLevel3East = 172,
        E3DungeonOfDeathLevel3NorthEast = 173,
        E3DungeonOfDeathLevel4North = 174,
        E3DungeonOfDeathLevel4East = 175,
        E3DungeonOfDeathLevel4NorthEast = 176,
        A2SouthernSphinxLevel1North = 177,
        D1DragonCloudsNorth = 178,
        D1DragonCloudsEast = 179,
        D1DragonCloudsNorthEast = 180,
        B3CloudsOfTheAncientsNorth = 181,
        B3CloudsOfTheAncientsEast = 182,
        B3CloudsOfTheAncientsNorthEast = 183, 
        LastSub = 184,
        Unknown = -1
    }

    public class MM45GameState : GameState
    {
        public override GameNames Game { get { return GameNames.MightAndMagic45; } }
        public bool[] IsCaster = null;
        private Subscreen m_subScreen = Subscreen.Unknown;
        public MainState OriginalMain = MainState.Unknown;

        public override bool Casting
        {
            get
            {
                switch (Main)
                {
                    case MainState.CastQuickspell:
                    case MainState.SelectSpell:
                    case MainState.SelectSpellTarget:
                    case MainState.SelectTownPortal:
                        return true;
                    default:
                        return false;
                }

            }
        }

        public override bool ActingIsCaster
        {
            get
            {
                if (IsCaster == null || ActingCaster < 0 || ActingCaster >= IsCaster.Length)
                    return true;    // Something is wrong; show the spell list by default
                return IsCaster[ActingCaster];
            }
        }

        public override Subscreen Subscreen { get { return m_subScreen; } set { m_subScreen = value; } }
    }

    public class MM45SpellInfo : SpellInfo
    {
        public MM45Spell Spell;
        public MM45PartyInfo Party;

        public MM45SpellInfo()
        {
            Spell = null;
            Party = null;
            Game = new MM45GameState();
            Game.Main = MainState.Unknown;
            Game.InCombat = false;
            Game.Location = LocationInformation.Empty;
            Game.ActingCharAddress = -1;
            Game.ActingCaster = -1;
            Game.ActingCombatChar = -1;
        }
    }

    public class MM45BackpackBytes : MMBackpackBytes
    {
        public byte[,] Weapons;
        public byte[,] Armor;
        public byte[,] Accessories;
        public byte[,] Miscellaneous;

        public const int WeaponBaseOffset = 1;
        public const int ArmorBaseOffset = 1;
        public const int AccessoryBaseOffset = 1;
        public const int MiscBaseOffset = 0;

        public MM45BackpackBytes()
        {
            InitNull();
        }

        public void InitNull()
        {
            Weapons = Global.NullBytes(9, 4);
            Armor = Global.NullBytes(9, 4);
            Accessories = Global.NullBytes(9, 4);
            Miscellaneous = Global.NullBytes(9, 4);
        }

        public byte[][,] All
        {
            get { return new byte[][,] { Weapons, Armor, Accessories, Miscellaneous }; }
        }

        public ItemType GetType(byte[,] test)
        {
            if (test == Weapons)
                return ItemType.Weapon;
            if (test == Armor)
                return ItemType.Armor;
            if (test == Accessories)
                return ItemType.Accessory;
            if (test == Miscellaneous)
                return ItemType.Miscellaneous;
            return ItemType.None;
        }

        public MM45BackpackBytes(byte[] bytes)
        {
            InitNull();

            if (bytes == null || bytes.Length < 9 * 4 * 4)
                return;

            int iIndex = 0;
            foreach (byte[,] itemType in All)
            {
                for (int i = 0; i < 9; i++)
                    for (int j = 0; j < 4; j++)
                        itemType[i, j] = bytes[iIndex++];
            }
        }

        public void Condense()
        {
            // Make sure there are no empty item slots between full ones, as that confuses the game
            Condense(Weapons, 1);
            Condense(Armor, 1);
            Condense(Accessories, 1);
            Condense(Miscellaneous, 0);
        }

        public void Condense(byte[,] array, int iBase)
        {
            for (int i = 0; i < 9; i++)
            {
                if (array[i, iBase] != 0)
                    continue;

                bool bEnd = true;
                for(int j = i + 1; j < 9; j++)
                {
                    if (array[j, iBase] == 0)
                    continue;

                    for (int iCopy = 0; iCopy < 4; iCopy++)
                    {
                        array[i, iCopy] = array[j, iCopy];
                        array[j, iCopy] = 0;
                    }
                    bEnd = false;
                    break;
                }

                if (bEnd)
                    return;
            }
        }

        public override byte[] GetBytes()
        {
            Condense();
            MemoryStream ms = new MemoryStream(36 * 4);
            foreach (byte[,] itemType in All)
            {
                for (int i = 0; i < 9; i++)
                    for(int j = 0; j < 4; j++)
                        ms.WriteByte(itemType[i, j]);
            }

            return ms.ToArray();
        }

        private void SetBytes(byte[,] bytes, int index, byte[] bytesSet, int offset)
        {
            for (int i = 0; i < 4; i++)
                bytes[index, i] = bytesSet[offset + i];
        }

        private int Count(byte[,] bytes, int iBase)
        {
            int iCount = 0;
            for (int i = 0; i < 9; i++)
                if (bytes[i, iBase] != 0)
                    iCount++;
            return iCount;
        }

        public int WeaponCount { get { return Count(Weapons, 1); } }
        public int ArmorCount { get { return Count(Armor, 1); } }
        public int AccessoryCount { get { return Count(Accessories, 1); } }
        public int MiscCount { get { return Count(Miscellaneous, 0); } }

        public int ItemCount { get { return WeaponCount + ArmorCount + AccessoryCount + MiscCount; } }

        public static MM45BackpackBytes Create(List<Item> items)
        {
            MM45BackpackBytes backpack = new MM45BackpackBytes();
            backpack.SetFromList(items);
            return backpack;
        }

        public void SetFromList(List<Item> items)
        {
            InitNull();

            foreach (Item item in items)
                Add(item);
        }

        private bool AddBytes(byte[,] bytes, int iBase, byte[] bytesAdd, int offset, int iSuggestedPosition)
        {
            // Use the suggested position to avoid moving items around when unnecessary
            if (iSuggestedPosition >= 0 && iSuggestedPosition < 9)
            {
                if (bytes[iSuggestedPosition, iBase] == 0)
                {
                    SetBytes(bytes, iSuggestedPosition, bytesAdd, offset);
                    return true;
                }
            }

            for (int i = 0; i < 9; i++)
            {
                if (bytes[i, iBase] == 0)
                {
                    SetBytes(bytes, i, bytesAdd, offset);
                    return true;
                }
            }
            return false;
        }

        public override bool Add(Item item)
        {
            MM45Item mmItem = item as MM45Item;
            if (mmItem == null)
                return false;

            switch (mmItem.Base.Type)
            {
                case ItemType.Weapon: return AddBytes(Weapons, WeaponBaseOffset, mmItem.GetBytes(), 1, mmItem.MemoryIndex);
                case ItemType.Armor: return AddBytes(Armor, ArmorBaseOffset, mmItem.GetBytes(), 1, mmItem.MemoryIndex - 9);
                case ItemType.Accessory: return AddBytes(Accessories, AccessoryBaseOffset, mmItem.GetBytes(), 1, mmItem.MemoryIndex - 18);
                case ItemType.Miscellaneous: return AddBytes(Miscellaneous, MiscBaseOffset, mmItem.GetBytes(), 1, mmItem.MemoryIndex - 27);
                default: return false;
            }
        }

        public override bool Add(List<Item> items)
        {
            bool bAllSucceeded = true;
            foreach (Item item in items)
            {
                if (!Add(item))
                    bAllSucceeded = false;
            }
            return bAllSucceeded;
        }
    }

    public class MM45MapData : MM345MapData
    {
        public MM45MapBytes MapBytes;
        public MM45Side Side;
        public byte FloorTileSet;

        public MM45MapData()
        {
            BytesPerSquare = 2;
            Bounds = new Rectangle(0, 0, 16, 16);
            FloorTileSet = 0;
            Side = MM45Side.Clouds;
        }

        public override void CopyMetadataFrom(MapData dataCopy)
        {
            base.CopyMetadataFrom(dataCopy);

            if (dataCopy is MM45MapData)
            {
                MapBytes = ((MM45MapData)dataCopy).MapBytes;
                FloorTileSet = ((MM45MapData)dataCopy).FloorTileSet;
                Side = ((MM45MapData)dataCopy).Side;
            }
        }
    }

    public class MM45MapCartography : MM345MapCartography { }

    public class MM45SpecialSquare : MM345SpecialSquare
    {
        public MM45SpecialSquare(byte[] bytes, int iOffset, byte[] bytesActions)
        {
            SetData(bytes, iOffset, bytesActions);
        }

        public override DirectionFlags Facing
        {
            // MM4/5 has East and South switched vs. MM3
            get
            {
                switch (Dir)
                {
                    case 0: return DirectionFlags.North;
                    case 1: return DirectionFlags.East;
                    case 2: return DirectionFlags.South;
                    case 3: return DirectionFlags.West;
                    case 4: return DirectionFlags.All;
                    default: return DirectionFlags.None;
                }
            }
        }
    }

    public class MM45EncounterInfo : MM345EncounterInfo
    {
        public MM45GameState GameState;

        public MM45EncounterInfo()
            : base()
        {
            m_monsters = null;
        }

        public override string ExtraText { get { return GetExtraText(MMEffectTag.GetEffectsMM45(ActiveEffects)); } }
    }

    public class MM45String : MM345String
    {
    }

    [Flags]
    public enum MM45MapFlags : uint
    {
        Etherealize = 0x00000040,
        TownPortal = 0x00000100,
        SuperShelter = 0x00000200,
        TimeDistortion = 0x00000400,
        LloydsBeacon = 0x00000800,
        Teleport = 0x00001000,
        Rest = 0x00004000,
        Save = 0x00008000,
        Dark = 0x40000000,
        Outdoor = 0x80000000
    }

    public class MM45WorldBytes
    {
        public const int OffsetWorldBits = 0;
        public const int OffsetQuestBits = 16;

        public byte[] RawBytes;

        public byte[] WorldBits;   // 16 bytes
        public byte[] QuestBits;  // 16 bytes

        public MM45WorldBytes(byte[] bytes)
        {
            RawBytes = bytes;

            WorldBits = Global.Subset(bytes, OffsetWorldBits, 16);
            QuestBits = Global.Subset(bytes, OffsetQuestBits, 16);
        }

        public byte[] GetBytes()
        {
            MemoryStream ms = new MemoryStream(32);
            ms.Write(WorldBits, 0, 16);
            ms.Write(QuestBits, 0, 16);
            return ms.ToArray();
        }
    }

    public class MM45MapBytes
    {
        public const int OffsetMapIndex = 0;
        public const int OffsetMapNorth = 2;
        public const int OffsetMapEast = 4;
        public const int OffsetMapSouth = 6;
        public const int OffsetMapWest = 8;
        public const int OffsetFlags = 10;
        public const int OffsetWalls = 14;
        public const int OffsetFloors = 30;
        public const int OffsetDefaultFloor = 46;
        public const int OffsetRunX = 47;
        public const int OffsetWallBlocked = 48;
        public const int OffsetFloorBlocked = 49;
        public const int OffsetLockStrength = 50;
        public const int OffsetChestStrength = 51;
        public const int OffsetDoorStrength = 52;
        public const int OffsetGrateStrength = 53;
        public const int OffsetWallStrength = 54;
        public const int OffsetEscapeChance = 55;
        public const int OffsetRunY = 56;
        public const int OffsetTrapStrength = 57;
        public const int OffsetWallType = 58;
        public const int OffsetTavernOffset = 59;
        public const int OffsetCartographySeen = 60;
        public const int OffsetCartographyExplored = 92;
        public const int OffsetSide = -1;
        public const int OffsetQuad = -1;
        public const int Size = OffsetCartographyExplored + 34;

        public byte[] RawBytes;

        public UInt16 MapIndex;
        public UInt16 MapNorth;
        public UInt16 MapEast;
        public UInt16 MapSouth;
        public UInt16 MapWest;
        public MM45MapFlags Flags;
        public byte[] Walls;   // 16 bytes
        public byte[] Floors;  // 16 bytes
        public byte DefaultFloor;
        public byte RunX;
        public byte RunY;
        public byte WallBlocked;
        public byte FloorBlocked;
        public byte LockStrength;
        public byte ChestStrength;
        public byte DoorStrength;
        public byte GrateStrength;
        public byte WallStrength;
        public byte EscapeChance;
        public byte TrapStrength;
        public byte WallType;
        public byte TavernOffset;
        public byte[] CartographySeen;     // 32 bytes
        public byte[] CartographyExplored; // 32 bytes
        public byte Side;
        public byte Quad;

        public MM45MapBytes(byte[] bytes)
        {
            RawBytes = bytes;

            MapIndex = BitConverter.ToUInt16(bytes, OffsetMapIndex);
            MapNorth = BitConverter.ToUInt16(bytes, OffsetMapNorth);
            MapEast = BitConverter.ToUInt16(bytes, OffsetMapEast);
            MapSouth = BitConverter.ToUInt16(bytes, OffsetMapSouth);
            MapWest = BitConverter.ToUInt16(bytes, OffsetMapWest);
            Flags = (MM45MapFlags)BitConverter.ToUInt32(bytes, OffsetFlags);
            Walls = Global.Subset(bytes, OffsetWalls, 16);
            Floors = Global.Subset(bytes, OffsetFloors, 16);
            DefaultFloor = bytes[OffsetDefaultFloor];
            RunX = bytes[OffsetRunX];
            RunY = bytes[OffsetRunY];
            WallBlocked = bytes[OffsetWallBlocked];
            FloorBlocked = bytes[OffsetFloorBlocked];
            LockStrength = bytes[OffsetLockStrength];
            ChestStrength = bytes[OffsetChestStrength];
            DoorStrength = bytes[OffsetDoorStrength];
            GrateStrength = bytes[OffsetGrateStrength];
            WallStrength = bytes[OffsetWallStrength];
            EscapeChance = bytes[OffsetEscapeChance];
            TrapStrength = bytes[OffsetTrapStrength];
            WallType = bytes[OffsetWallType];
            TavernOffset = bytes[OffsetTavernOffset];
            CartographySeen = Global.Subset(bytes, OffsetCartographySeen, 32);
            CartographyExplored = Global.Subset(bytes, OffsetCartographyExplored, 32);
            Side = bytes[Size-2];
            Quad = bytes[Size-1];
        }

        public UInt16 SideMapIndex { get { return (UInt16) ((Side << 8) | MapIndex); } }
        public UInt16 SideMapNorth { get { return (UInt16)((Side << 8) | MapNorth); } }
        public UInt16 SideMapSouth { get { return (UInt16)((Side << 8) | MapSouth); } }
        public UInt16 SideMapEast { get { return (UInt16)((Side << 8) | MapEast); } }
        public UInt16 SideMapWest { get { return (UInt16)((Side << 8) | MapWest); } }

        public byte[] GetBytes()
        {
            MemoryStream ms = new MemoryStream(124);
            ms.Write(BitConverter.GetBytes(MapIndex), 0, 2);
            ms.Write(BitConverter.GetBytes(MapNorth), 0, 2);
            ms.Write(BitConverter.GetBytes(MapEast), 0, 2);
            ms.Write(BitConverter.GetBytes(MapSouth), 0, 2);
            ms.Write(BitConverter.GetBytes(MapWest), 0, 2);
            ms.Write(BitConverter.GetBytes((UInt32)Flags), 0, 4);
            ms.Write(Walls, 0, 16);
            ms.Write(Floors, 0, 16);
            ms.WriteByte(DefaultFloor);
            ms.WriteByte(RunX);
            ms.WriteByte(WallBlocked);
            ms.WriteByte(FloorBlocked);
            ms.WriteByte(LockStrength);
            ms.WriteByte(ChestStrength);
            ms.WriteByte(DoorStrength);
            ms.WriteByte(GrateStrength);
            ms.WriteByte(WallStrength);
            ms.WriteByte(EscapeChance);
            ms.WriteByte(RunY);
            ms.WriteByte(TrapStrength);
            ms.WriteByte(WallType);
            ms.WriteByte(TavernOffset);
            ms.Write(CartographySeen, 0, 32);
            ms.Write(CartographyExplored, 0, 32);
            ms.WriteByte(Side);
            ms.WriteByte(Quad);
            return ms.ToArray();
        }
    }

    public class MM45PartyBytes
    {
        public const int OffsetPartyCount = 0x00;
        public const int OffsetRealPartyCount = 0x01;
        public const int OffsetPartyMembers = 0x02;
        public const int OffsetFacing = 0x0A;
        public const int OffsetLocationX = 0x0B;
        public const int OffsetLocationY = 0x0C;
        public const int OffsetMapIndex = 0x0D;
        public const int OffsetSoundFlag = 0x0E;
        public const int OffsetMusicFlag = 0x0F;
        public const int OffsetDelay = 0x10;
        public const int OffsetLastMap = 0x11;
        public const int OffsetLevitate = 0x12;
        public const int OffsetAutomap = 0x13;
        public const int OffsetWizardEye = 0x14;
        public const int OffsetClairvoyance = 0x15;
        public const int OffsetWalkOnWater = 0x16;
        public const int OffsetBlessed = 0x17;
        public const int OffsetPowerShield = 0x18;
        public const int OffsetHolyBonus = 0x19;
        public const int OffsetHeroism = 0x1A;
        public const int OffsetDifficulty = 0x1B;
        public const int OffsetShopWeapons = 0x1C;
        public const int OffsetShopArmor = 0xAC;
        public const int OffsetShopAccessories = 0x13C;
        public const int OffsetShopMisc = 0x1CC;
        public const int OffsetCloudsEnd = 0x25C;
        public const int OffsetDarksideEnd = 0x25E;
        public const int OffsetWorldEnd = 0x260;
        public const int OffsetStepCounter = 0x262;
        public const int OffsetDay = 0x264;
        public const int OffsetYear = 0x266;
        public const int OffsetMinutes = 0x268;
        public const int OffsetFood = 0x26A;
        public const int OffsetLight = 0x26C;
        public const int OffsetTorch = 0x26E;
        public const int OffsetResistFire = 0x270;
        public const int OffsetResistElec = 0x272;
        public const int OffsetResistCold = 0x274;
        public const int OffsetResistPoison = 0x276;
        public const int OffsetDeathcount = 0x278;
        public const int OffsetWinCount = 0x27A;
        public const int OffsetLossCount = 0x27C;
        public const int OffsetGold = 0x27E;
        public const int OffsetGems = 0x282;
        public const int OffsetBankGold = 0x286;
        public const int OffsetBankGems = 0x28A;
        public const int OffsetSecondsPlayed = 0x28E;
        public const int OffsetRested = 0x292;
        public const int OffsetPartyBits1 = 0x293;
        public const int OffsetPartyBits2 = 0x2B3;
        public const int OffsetNoteBits = 0x2D3;
        public const int OffsetQuestBits = 0x2E3;
        public const int OffsetQuestItems = 0x2EB;
        public const int OffsetShopWeapons2 = 0x340;
        public const int OffsetShopArmor2 = 0x3D0;
        public const int OffsetShopAccessories2 = 0x460;
        public const int OffsetShopMisc2 = 0x4F0;
        public const int OffsetCharBits = 0x580;
        public const int OffsetUnknown = 0x5DA;

        public const int Size = OffsetUnknown + 30;

        public byte[] RawBytes;

        public byte PartyCount;
        public byte RealPartyCount;
        public byte[] arrayPartyMembers; // 8 bytes
        public byte Facing;
        public byte LocationX;
        public byte LocationY;
        public byte MapIndex;
        public byte SoundFlag;
        public byte MusicFlag;
        public byte Delay;
        public byte LastMap;
        public byte Levitate;
        public byte Automap;
        public byte WizardEye;
        public byte Clairvoyance;
        public byte WalkOnWater;
        public byte Blessed;
        public byte PowerShield;
        public byte HolyBonus;
        public byte Heroism;
        public byte Difficulty;
        public byte[] ShopWeapons;     // 144 bytes
        public byte[] ShopArmor;       // 144 bytes
        public byte[] ShopAccessories; // 144 bytes
        public byte[] ShopMisc;        // 144 bytes
        public byte CloudsEnd;
        public byte DarksideEnd;
        public byte WorldEnd;
        public UInt16 StepCounter;
        public UInt16 Day;
        public UInt16 Year;
        public UInt16 Minutes;
        public UInt16 Food;
        public UInt16 Light;
        public UInt16 Torch;
        public UInt16 ResistFire;
        public UInt16 ResistElec;
        public UInt16 ResistCold;
        public UInt16 ResistPoison;
        public UInt16 Deathcount;
        public UInt16 WinCount;
        public UInt16 LossCount;
        public UInt32 Gold;
        public UInt32 Gems;
        public UInt32 BankGold;
        public UInt32 BankGems;
        public UInt32 SecondsPlayed;
        public byte Rested;
        public byte[] PartyBits1;       // 32 bytes
        public byte[] PartyBits2;       // 32 bytes
        public byte[] WorldBits;        // 16 bytes
        public byte[] QuestBits;        // 8 bytes
        public byte[] QuestItems;       // 85 bytes
        public byte[] ShopWeapons2;     // 144 bytes
        public byte[] ShopArmor2;       // 144 bytes
        public byte[] ShopAccessories2; // 144 bytes
        public byte[] ShopMisc2;        // 144 bytes
        public byte[] CharBits;         // 90 bytes
        public byte[] Unknown;          // 30 bytes

        public MM45PartyBytes(byte[] bytes)
        {
            RawBytes = bytes;

            PartyCount = bytes[OffsetPartyCount];
            RealPartyCount = bytes[OffsetRealPartyCount];
            arrayPartyMembers = Global.Subset(bytes, OffsetPartyMembers, 8);
            Facing = bytes[OffsetFacing];
            LocationX = bytes[OffsetLocationX];
            LocationY = bytes[OffsetLocationY];
            MapIndex = bytes[OffsetMapIndex];
            SoundFlag = bytes[OffsetSoundFlag];
            MusicFlag = bytes[OffsetMusicFlag];
            Delay = bytes[OffsetDelay];
            LastMap = bytes[OffsetLastMap];
            Levitate = bytes[OffsetLevitate];
            Automap = bytes[OffsetAutomap];
            WizardEye = bytes[OffsetWizardEye];
            Clairvoyance = bytes[OffsetClairvoyance];
            WalkOnWater = bytes[OffsetWalkOnWater];
            Blessed = bytes[OffsetBlessed];
            PowerShield = bytes[OffsetPowerShield];
            HolyBonus = bytes[OffsetHolyBonus];
            Heroism = bytes[OffsetHeroism];
            Difficulty = bytes[OffsetDifficulty];
            ShopWeapons = Global.Subset(bytes, OffsetShopWeapons, 144);
            ShopArmor = Global.Subset(bytes, OffsetShopArmor, 144);
            ShopAccessories = Global.Subset(bytes, OffsetShopAccessories, 144);
            ShopMisc = Global.Subset(bytes, OffsetShopMisc, 144);
            CloudsEnd = bytes[OffsetCloudsEnd];
            DarksideEnd = bytes[OffsetDarksideEnd];
            WorldEnd = bytes[OffsetWorldEnd];
            StepCounter = BitConverter.ToUInt16(bytes, OffsetStepCounter);
            Day = BitConverter.ToUInt16(bytes, OffsetDay);
            Year = BitConverter.ToUInt16(bytes, OffsetYear);
            Minutes = BitConverter.ToUInt16(bytes, OffsetMinutes);
            Food = BitConverter.ToUInt16(bytes, OffsetFood);
            Light = BitConverter.ToUInt16(bytes, OffsetLight);
            Torch = BitConverter.ToUInt16(bytes, OffsetTorch);
            ResistFire = BitConverter.ToUInt16(bytes, OffsetResistFire);
            ResistElec = BitConverter.ToUInt16(bytes, OffsetResistElec);
            ResistCold = BitConverter.ToUInt16(bytes, OffsetResistCold);
            ResistPoison = BitConverter.ToUInt16(bytes, OffsetResistPoison);
            Deathcount = BitConverter.ToUInt16(bytes, OffsetDeathcount);
            WinCount = BitConverter.ToUInt16(bytes, OffsetWinCount);
            LossCount = BitConverter.ToUInt16(bytes, OffsetLossCount);
            Gold = BitConverter.ToUInt32(bytes, OffsetGold);
            Gems = BitConverter.ToUInt32(bytes, OffsetGems);
            BankGold = BitConverter.ToUInt32(bytes, OffsetBankGold);
            BankGems = BitConverter.ToUInt32(bytes, OffsetBankGems);
            SecondsPlayed = BitConverter.ToUInt32(bytes, OffsetSecondsPlayed);
            Rested = bytes[OffsetRested];
            PartyBits1 = Global.Subset(bytes, OffsetPartyBits1, 32);
            PartyBits2 = Global.Subset(bytes, OffsetPartyBits2, 32);
            WorldBits = Global.Subset(bytes, OffsetNoteBits, 16);
            QuestBits = Global.Subset(bytes, OffsetQuestBits, 8);
            QuestItems = Global.Subset(bytes, OffsetQuestItems, 85);
            ShopWeapons2 = Global.Subset(bytes, OffsetShopWeapons2, 144);
            ShopArmor2 = Global.Subset(bytes, OffsetShopArmor2, 144);
            ShopAccessories2 = Global.Subset(bytes, OffsetShopAccessories2, 144);
            ShopMisc2 = Global.Subset(bytes, OffsetShopMisc2, 144);
            CharBits = Global.Subset(bytes, OffsetCharBits, 90);
            Unknown = Global.Subset(bytes, OffsetUnknown, 30);
        }

        public bool HasQuestItem(MM45QuestItemIndex item)
        {
            if ((int)item > QuestItems.Length)
                return false;
            return QuestItems[(int)item - 1] != 0;
        }

        public GameTime GameTime => GameTime.FromMM345Values(Year, Day, Minutes);

        public byte[] GetQuestRelatedBytes()
        {
            // Returns bytes that are relevant to the quest list (i.e. bytes that change infrequently so that
            // the quest list doesn't refresh constantly)

            MemoryStream ms = new MemoryStream();
            ms.WriteByte(MapIndex);
            ms.Write(PartyBits1, 0, PartyBits1.Length);
            ms.Write(PartyBits2, 0, PartyBits2.Length);
            ms.Write(WorldBits, 0, WorldBits.Length);
            ms.Write(QuestBits, 0, QuestBits.Length);
            ms.Write(QuestItems, 0, QuestItems.Length);
            ms.Write(CharBits, 0, CharBits.Length);
            return ms.ToArray();
        }

        public byte[] GetBytes()
        {
            MemoryStream ms = new MemoryStream(1528);
            ms.WriteByte(PartyCount);
            ms.WriteByte(RealPartyCount);
            ms.Write(arrayPartyMembers, 0, 8);
            ms.WriteByte(Facing);
            ms.WriteByte(LocationX);
            ms.WriteByte(LocationY);
            ms.WriteByte(MapIndex);
            ms.WriteByte(SoundFlag);
            ms.WriteByte(MusicFlag);
            ms.WriteByte(Delay);
            ms.WriteByte(LastMap);
            ms.WriteByte(Levitate);
            ms.WriteByte(Automap);
            ms.WriteByte(WizardEye);
            ms.WriteByte(Clairvoyance);
            ms.WriteByte(WalkOnWater);
            ms.WriteByte(Blessed);
            ms.WriteByte(PowerShield);
            ms.WriteByte(HolyBonus);
            ms.WriteByte(Heroism);
            ms.WriteByte(Difficulty);
            ms.Write(ShopWeapons, 0, 144);
            ms.Write(ShopArmor, 0, 144);
            ms.Write(ShopAccessories, 0, 144);
            ms.Write(ShopMisc, 0, 144);
            ms.WriteByte(CloudsEnd);
            ms.WriteByte(DarksideEnd);
            ms.WriteByte(WorldEnd);
            ms.Write(BitConverter.GetBytes(StepCounter), 0, 2);
            ms.Write(BitConverter.GetBytes(Day), 0, 2);
            ms.Write(BitConverter.GetBytes(Year), 0, 2);
            ms.Write(BitConverter.GetBytes(Minutes), 0, 2);
            ms.Write(BitConverter.GetBytes(Food), 0, 2);
            ms.Write(BitConverter.GetBytes(Light), 0, 2);
            ms.Write(BitConverter.GetBytes(Torch), 0, 2);
            ms.Write(BitConverter.GetBytes(ResistFire), 0, 2);
            ms.Write(BitConverter.GetBytes(ResistElec), 0, 2);
            ms.Write(BitConverter.GetBytes(ResistCold), 0, 2);
            ms.Write(BitConverter.GetBytes(ResistPoison), 0, 2);
            ms.Write(BitConverter.GetBytes(Deathcount), 0, 2);
            ms.Write(BitConverter.GetBytes(WinCount), 0, 2);
            ms.Write(BitConverter.GetBytes(LossCount), 0, 2);
            ms.Write(BitConverter.GetBytes(Gold), 0, 4);
            ms.Write(BitConverter.GetBytes(Gems), 0, 4);
            ms.Write(BitConverter.GetBytes(BankGold), 0, 4);
            ms.Write(BitConverter.GetBytes(BankGems), 0, 4);
            ms.Write(BitConverter.GetBytes(SecondsPlayed), 0, 4);
            ms.WriteByte(Rested);
            ms.Write(PartyBits1, 0, 32);
            ms.Write(PartyBits2, 0, 32);
            ms.Write(WorldBits, 0, 16);
            ms.Write(QuestBits, 0, 8);
            ms.Write(QuestItems, 0, 85);
            ms.Write(ShopWeapons2, 0, 144);
            ms.Write(ShopArmor2, 0, 144);
            ms.Write(ShopAccessories2, 0, 144);
            ms.Write(ShopMisc2, 0, 144);
            ms.Write(CharBits, 0, 90);
            ms.Write(Unknown, 0, 30);
            return ms.ToArray();
        }
    }

    public class MM45VisibleObject : MM345VisibleObject
    {
        public byte X;
        public byte Y;
        public byte ImageIndex;
        public byte AnimationIndex;
        public byte[] UnknownBytes;

        public override Point Location { get { return new Point(X, Y); } }
        public override int Image { get { return ImageIndex; } }
        public override uint Unknown { get { return BitConverter.ToUInt32(UnknownBytes, 0); } }

        public MM45VisibleObject(byte[] bytes, int iOffset)
        {
            if (bytes.Length - iOffset < 8)
                return;

            X = bytes[iOffset];
            Y = bytes[iOffset + 1];
            ImageIndex = bytes[iOffset + 2];
            AnimationIndex = bytes[iOffset + 3];
            UnknownBytes = new byte[4];
            Buffer.BlockCopy(bytes, iOffset + 4, UnknownBytes, 0, 4);
        }
    }

    public class MM45CureAllInfo : CureAllInfo
    {
        public MMCondition[] Conditions;   // 8 bytes; one per character
        public MMCondition CasterCondition;
        public MM45GameState GameState;
        public Int16[] HitPoints;       // 16 bytes; two per character
        public int[] HitPointsMax;      // 16 bytes; two per character
        public UInt16 CasterSpellPoints;
        public UInt32 CasterGems;
        public GenericClass CasterClass;
        public MM345KnownSpells CasterSpells;
        public bool InCombat;
        public bool AntiMagicZone;

        public MM45CureAllInfo()
        {
        }

        public override bool IsHealer
        {
            get
            {
                switch (CasterClass)
                {
                    case GenericClass.Cleric:
                    case GenericClass.Paladin:
                    case GenericClass.Ranger:
                    case GenericClass.Druid:
                        return true;
                    default:
                        return false;

                }
            }
        }

        public override bool Valid { get { return Conditions != null && Conditions.Length > 0; } }
        public override bool IsIncapacitated { get { return CasterCondition.UnableToCast; } }
        public override bool MagicPermitted { get { return !AntiMagicZone; } }
        public override bool Combat { get { return InCombat; } }
    }

    public class MM45MemoryHacker : MM345MemoryHacker
    {
        public MM45MemoryHacker()
        {
            //m_finder = new OffsetFinder(this, File.ReadAllBytes(@"C:\GOG Games\Might and Magic VI Limited Edition\Might and Magic 4-5\DARK06.SAV"));
            m_game = GameNames.MightAndMagic45;
        }

        public MM45Memory Memory = new MM45NonVoiceMemory();
        private byte[] m_lastMapAppearance;
        private byte[] m_lastMonsterBytes;
        private MonsterLocations m_lastMonsterLocations;
        private Point m_lastMonsterPartyLocation;
        private WatchedFile m_fileXeenCur = null;
        private WatchedFile m_fileDarkCur = null;
        private MM4RosterFile m_mm4Roster;
        private MM5RosterFile m_mm5Roster;
        private MM45Side m_lastSide = MM45Side.Unknown;
        private MM45Shops m_lastShops = null;
        private MM45FileQuestInfo m_fileQuestInfo = new MM45FileQuestInfo();
        private int m_lastQuestMap = -1;
        private byte m_lastActingCharacter = 0;

        public override string SpellType2 { get { return "Druid"; } }
        public override string SpellType3 { get { return "Arcane"; } }
        public override byte[] MainSearch { get { return MM45Memory.MainSearch; } }
        public override MemoryGuess[] Guesses { get { return MM45Memory.Guesses; } }
        public override bool SeparateInventoryTypes { get { return true; } }
        public override bool AlternateGameVersion { get { return IsVoiceVersion(); } }

        public override string RosterPath
        {
            get
            {
                string strPath = base.RosterPath;
                if (strPath.ToLower().EndsWith("\\world"))
                {
                    if (Directory.Exists(strPath))
                        return strPath;
                    return strPath.Substring(0, strPath.Length - "\\world".Length);
                }
                string strWorld = Path.Combine(strPath, "WORLD");
                if (Directory.Exists(strWorld) || IsVoiceVersion())
                    return strWorld;
                return strPath;
            }
        }

        private bool IsVoiceVersion()
        {
            byte[] bytes = new byte[4];
            if (!ReadOffset(MM45Memory.WoXCDR, bytes))
                return false;
            return Global.Compare(bytes, new byte[] { 0x43, 0x44, 0x2D, 0x52 });  // "CD-R"
        }

        protected override void OnReinitialized(EventArgs e)
        {
            Memory = IsVoiceVersion() ? (MM45Memory) new MM45VoiceMemory() : (MM45Memory) new MM45NonVoiceMemory();

            MM45Side side = GetMonsterSide();

            if (side == MM45Side.Clouds)
                InitMM4MonsterList();
            else if (side == MM45Side.Darkside)
                InitMM5MonsterList();

            base.OnReinitialized(e);
        }

        public void InitMM4MonsterList()
        {
            MM45.MM4MonsterList.Value.Reinitialize(this, false);
            if (MM45.MM4MonsterList.Value.UsingInternalList)
                NeedsReinitialize = true;
        }

        public void InitMM5MonsterList()
        {
            MM45.MM5MonsterList.Value.Reinitialize(this, false);
            if (MM45.MM5MonsterList.Value.UsingInternalList)
                NeedsReinitialize = true;
        }

        public override int GetLightDistance(Point ptLocation)
        {
            if (ReadUInt16(Memory.PartyBytes + MM45PartyBytes.OffsetLight) > 0)
                return 5;
            if (((MM45MapFlags) ReadUInt32(Memory.MapFlags1 + MM45MapBytes.OffsetFlags)).HasFlag(MM45MapFlags.Dark))
                return 0;
            return 5;
        }

        private MM45PartyInfo ReadMM45PartyInfo()
        {
            if (!IsValid)
                return null;

            if (m_block == null)
                return null;

            // Might and Magic 4/5 stores the party in marching order and rearranges the entire party if the order is changed

            MM45GameState state = ReadMM45GameState();
            byte numChars = ReadByte(Memory.NumChars);

            if (numChars > 6)   // Invalid party; might be the intro screen
            {
                return new MM45PartyInfo(new byte[0], 0, 0);    // For the auto-show party window
            }

            MemoryBytes bytes = null;
            if (numChars > 0)
            {
                bytes = ReadOffset(Memory.PartyInfo, MM45Character.SizeInBytes * numChars + 1);
                bytes[bytes.Length - 1] = (byte)GetBlessValue();
            }

            byte bCharacter = ReadByte(Memory.ActingCharacter);
            if (bCharacter > 5 || !state.Inspecting)
                bCharacter = m_lastActingCharacter;     // Keeps the quest list from refreshing when there is an invalid active character
            else
                m_lastActingCharacter = bCharacter;

            byte bCaster = ReadByte(Memory.ActingCaster);

            MM45PartyInfo info = new MM45PartyInfo(bytes, numChars, bCharacter);
            info.ActingCaster = bCaster;
            info.ActingCombatChar = bCaster;
            info.InspectingCombatChar = bCaster;

            if (state == null)
                return info;

            if (state.NoActingChar)
            {
                // Don't activate random party tabs if there is no active character
                info.ActingChar = 255;
                info.ActingCaster = 255;
            }

            switch (state.Main)
            {
                case MainState.EnchantItem:
                    info.InspectingCombatChar = (byte)state.ActingCharAddress;
                    break;
                default:
                    break;
            }

            if (state.InCombat)
                info.ActingChar = info.ActingCombatChar;
            else if (state.Inspecting)
                info.ActingChar = info.InspectingCombatChar;
            else if (state.Casting)
                info.ActingChar = info.ActingCaster;

            return info;
        }

        public override PartyInfo GetPartyInfo()
        {
            if (!IsValid)
                return null;

            return ReadMM45PartyInfo();
        }

        public static MapTitleInfo GetMapTitlePair(int index) { return GetMapTitlePair(index % 256, index / 256 > 0 ? 1 : 0); }

        public static MapTitleInfo GetMapTitlePair(int index, int iSide)
        {
            if (iSide == 0)
            {
                switch ((MM4Map)index)
                {
                    case MM4Map.A1Surface: return new MapTitleInfo(index, "A-1, Cloudside Surface");
                    case MM4Map.A2Surface: return new MapTitleInfo(index, "A-2, Cloudside Surface");
                    case MM4Map.A3Surface: return new MapTitleInfo(index, "A-3, Cloudside Surface");
                    case MM4Map.A4Surface: return new MapTitleInfo(index, "A-4, Cloudside Surface");
                    case MM4Map.B1Surface: return new MapTitleInfo(index, "B-1, Cloudside Surface");
                    case MM4Map.B2Surface: return new MapTitleInfo(index, "B-2, Cloudside Surface");
                    case MM4Map.B3Surface: return new MapTitleInfo(index, "B-3, Cloudside Surface");
                    case MM4Map.B4Surface: return new MapTitleInfo(index, "B-4, Cloudside Surface");
                    case MM4Map.C1Surface: return new MapTitleInfo(index, "C-1, Cloudside Surface");
                    case MM4Map.C2Surface: return new MapTitleInfo(index, "C-2, Cloudside Surface");
                    case MM4Map.C3Surface: return new MapTitleInfo(index, "C-3, Cloudside Surface");
                    case MM4Map.C4Surface: return new MapTitleInfo(index, "C-4, Cloudside Surface");
                    case MM4Map.D1Surface: return new MapTitleInfo(index, "D-1, Cloudside Surface");
                    case MM4Map.D2Surface: return new MapTitleInfo(index, "D-2, Cloudside Surface");
                    case MM4Map.D3Surface: return new MapTitleInfo(index, "D-3, Cloudside Surface");
                    case MM4Map.D4Surface: return new MapTitleInfo(index, "D-4, Cloudside Surface");
                    case MM4Map.E1Surface: return new MapTitleInfo(index, "E-1, Cloudside Surface");
                    case MM4Map.E2Surface: return new MapTitleInfo(index, "E-2, Cloudside Surface");
                    case MM4Map.E3Surface: return new MapTitleInfo(index, "E-3, Cloudside Surface");
                    case MM4Map.E4Surface: return new MapTitleInfo(index, "E-4, Cloudside Surface");
                    case MM4Map.F1Surface: return new MapTitleInfo(index, "F-1, Cloudside Surface");
                    case MM4Map.F2Surface: return new MapTitleInfo(index, "F-2, Cloudside Surface");
                    case MM4Map.F3Surface: return new MapTitleInfo(index, "F-3, Cloudside Surface");
                    case MM4Map.F4Surface: return new MapTitleInfo(index, "F-4, Cloudside Surface");
                    case MM4Map.F4WitchClouds: return new MapTitleInfo(index, "F-4, Witch Clouds");
                    case MM4Map.C4HighMagicClouds: return new MapTitleInfo(index, "C-4, High Magic Clouds");
                    case MM4Map.D3CloudsOfXeen: return new MapTitleInfo(index, "D-3, Clouds of Xeen");
                    case MM4Map.F3Vertigo: return new MapTitleInfo(index, "F-3, Vertigo");
                    case MM4Map.D4Nightshadow: return new MapTitleInfo(index, "D-4, Nightshadow");
                    case MM4Map.C3Rivercity: return new MapTitleInfo(index, "C-3, Rivercity");
                    case MM4Map.C2Asp: return new MapTitleInfo(index, "C-2, Asp");
                    case MM4Map.A3Winterkill: return new MapTitleInfo(index, "A-3, Winterkill");
                    case MM4Map.F3DwarfMine1: return new MapTitleInfo(index, "F-3, Dwarf Mine 1");
                    case MM4Map.F3DwarfMine2: return new MapTitleInfo(index, "F-3, Dwarf Mine 2");
                    case MM4Map.E2DwarfMine3: return new MapTitleInfo(index, "E-2, Dwarf Mine 3");
                    case MM4Map.E2DwarfMine4: return new MapTitleInfo(index, "E-2, Dwarf Mine 4");
                    case MM4Map.D2DwarfMine5: return new MapTitleInfo(index, "D-2, Dwarf Mine 5");
                    case MM4Map.DeepMineAlpha: return new MapTitleInfo(index, "Deep Mine Alpha");
                    case MM4Map.DeepMineTheta: return new MapTitleInfo(index, "Deep Mine Theta");
                    case MM4Map.DeepMineKappa: return new MapTitleInfo(index, "Deep Mine Kappa");
                    case MM4Map.DeepMineOmega: return new MapTitleInfo(index, "Deep Mine Omega");
                    case MM4Map.B4CaveOfIllusionLevel1: return new MapTitleInfo(index, "B-4, Cave of Illusion Level 1");
                    case MM4Map.B4CaveOfIllusionLevel2: return new MapTitleInfo(index, "B-4, Cave of Illusion Level 2");
                    case MM4Map.B4CaveOfIllusionLevel3: return new MapTitleInfo(index, "B-4, Cave of Illusion Level 3");
                    case MM4Map.B4CaveOfIllusionLevel4: return new MapTitleInfo(index, "B-4, Cave of Illusion Level 4");
                    case MM4Map.E1VolcanoCaveLevel1: return new MapTitleInfo(index, "E-1, Volcano Cave Level 1");
                    case MM4Map.E1VolcanoCaveLevel2: return new MapTitleInfo(index, "E-1, Volcano Cave Level 2");
                    case MM4Map.E1VolcanoCaveLevel3: return new MapTitleInfo(index, "E-1, Volcano Cave Level 3");
                    case MM4Map.E1ShangriLa: return new MapTitleInfo(index, "E-1, Shangri-La");
                    case MM4Map.E1DragonCave: return new MapTitleInfo(index, "E-1, Dragon Cave");
                    case MM4Map.F4WitchTowerLevel1: return new MapTitleInfo(index, "F-4, Witch Tower Level 1");
                    case MM4Map.F4WitchTowerLevel2: return new MapTitleInfo(index, "F-4, Witch Tower Level 2");
                    case MM4Map.F4WitchTowerLevel3: return new MapTitleInfo(index, "F-4, Witch Tower Level 3");
                    case MM4Map.F4WitchTowerLevel4: return new MapTitleInfo(index, "F-4, Witch Tower Level 4");
                    case MM4Map.C3TowerofHighMagicLevel1: return new MapTitleInfo(index, "C-3, Tower of High Magic Level 1");
                    case MM4Map.C3TowerofHighMagicLevel2: return new MapTitleInfo(index, "C-3, Tower of High Magic Level 2");
                    case MM4Map.C3TowerofHighMagicLevel3: return new MapTitleInfo(index, "C-3, Tower of High Magic Level 3");
                    case MM4Map.C3TowerofHighMagicLevel4: return new MapTitleInfo(index, "C-3, Tower of High Magic Level 4");
                    case MM4Map.D3DarzogsTowerLevel1: return new MapTitleInfo(index, "D-3, Darzog's Tower Level 1");
                    case MM4Map.D3DarzogsTowerLevel2: return new MapTitleInfo(index, "D-3, Darzog's Tower Level 2");
                    case MM4Map.D3DarzogsTowerLevel3: return new MapTitleInfo(index, "D-3, Darzog's Tower Level 3");
                    case MM4Map.D3DarzogsTowerLevel4: return new MapTitleInfo(index, "D-3, Darzog's Tower Level 4");
                    case MM4Map.D2BurlockDungeon: return new MapTitleInfo(index, "D-2, Burlock Dungeon");
                    case MM4Map.D2CastleBurlockLevel1: return new MapTitleInfo(index, "D-2, Castle Burlock Level 1");
                    case MM4Map.D2CastleBurlockLevel2: return new MapTitleInfo(index, "D-2, Castle Burlock Level 2");
                    case MM4Map.D2CastleBurlockLevel3: return new MapTitleInfo(index, "D-2, Castle Burlock Level 3");
                    case MM4Map.A1BasenjiDungeon: return new MapTitleInfo(index, "A-1, Basenji Dungeon");
                    case MM4Map.A1CastleBasenjiLevel1: return new MapTitleInfo(index, "A-1, Castle Basenji Level 1");
                    case MM4Map.A1CastleBasenjiLevel2: return new MapTitleInfo(index, "A-1, Castle Basenji Level 2");
                    case MM4Map.A1CastleBasenjiLevel3: return new MapTitleInfo(index, "A-1, Castle Basenji Level 3");
                    case MM4Map.C4NewcastleDungeon: return new MapTitleInfo(index, "C-4, Newcastle Dungeon");
                    case MM4Map.C4NewcastleFoundation: return new MapTitleInfo(index, "C-4, Newcastle Foundation");
                    case MM4Map.C4NewcastleLevel1: return new MapTitleInfo(index, "C-4, Newcastle Level 1");
                    case MM4Map.C4NewcastleLevel2: return new MapTitleInfo(index, "C-4, Newcastle Level 2");
                    case MM4Map.D3XeensCastleLevel1: return new MapTitleInfo(index, "D-3, Xeen's Castle Level 1");
                    case MM4Map.D3XeensCastleLevel2: return new MapTitleInfo(index, "D-3, Xeen's Castle Level 2");
                    case MM4Map.D3XeensCastleLevel3: return new MapTitleInfo(index, "D-3, Xeen's Castle Level 3");
                    case MM4Map.D3XeensCastleLevel4: return new MapTitleInfo(index, "D-3, Xeen's Castle Level 4");
                    case MM4Map.E4AncientTempleOfYak: return new MapTitleInfo(index, "E-4, Ancient Temple of Yak");
                    case MM4Map.C4TombOfAThousandTerrors: return new MapTitleInfo(index, "C-4, Tomb of a Thousand Terrors");
                    case MM4Map.B4GolemDungeon: return new MapTitleInfo(index, "B-4, Golem Dungeon");
                    case MM4Map.B1SphinxBody: return new MapTitleInfo(index, "B-1, Sphinx Body");
                    case MM4Map.B1SphinxHead: return new MapTitleInfo(index, "B-1, Sphinx Head");
                    case MM4Map.B1SphinxDungeon: return new MapTitleInfo(index, "B-1, Sphinx Dungeon");
                    case MM4Map.B2TheWarzone: return new MapTitleInfo(index, "B-2, The Warzone");
                    case MM4Map.F4WitchCloudsNorth: return new MapTitleInfo(index, "F-4, Witch Clouds (north)");
                    case MM4Map.F4WitchCloudsEast: return new MapTitleInfo(index, "F-4, Witch Clouds (east)");
                    case MM4Map.F4WitchCloudsNorthEast: return new MapTitleInfo(index, "F-4, Witch Clouds (northeast)");
                    case MM4Map.C4HighMagicCloudsNorth: return new MapTitleInfo(index, "C-4, High Magic Clouds (north)");
                    case MM4Map.C4HighMagicCloudsEast: return new MapTitleInfo(index, "C-4, High Magic Clouds (east)");
                    case MM4Map.C4HighMagicCloudsNorthEast: return new MapTitleInfo(index, "C-4 High Magic Clouds (northeast)");
                    case MM4Map.D3CloudsOfXeenNorth: return new MapTitleInfo(index, "D-3, Clouds Of Xeen (north)");
                    case MM4Map.D3CloudsOfXeenEast: return new MapTitleInfo(index, "D-3, Clouds Of Xeen (east)");
                    case MM4Map.D3CloudsOfXeenNorthEast: return new MapTitleInfo(index, "D-3, Clouds Of Xeen (northeast)");
                    case MM4Map.F3VertigoNorth: return new MapTitleInfo(index, "F-3, Vertigo (north)");
                    case MM4Map.F3VertigoEast: return new MapTitleInfo(index, "F-3, Vertigo (east)");
                    case MM4Map.F3VertigoNorthEast: return new MapTitleInfo(index, "F-3, Vertigo (northeast)");
                    case MM4Map.C3RivercityNorth: return new MapTitleInfo(index, "C-3, Rivercity (north)");
                    case MM4Map.C3RivercityEast: return new MapTitleInfo(index, "C-3, Rivercity (east)");
                    case MM4Map.C3RivercityNorthEast: return new MapTitleInfo(index, "C-3, Rivercity (northeast)");
                    case MM4Map.F3DwarfMine1North: return new MapTitleInfo(index, "F-3, Dwarf Mine 1 (north)");
                    case MM4Map.F3DwarfMine2North: return new MapTitleInfo(index, "F-3, Dwarf Mine 2 (north)");
                    case MM4Map.E2DwarfMine3East: return new MapTitleInfo(index, "E-2, Dwarf Mine 3 (east)");
                    case MM4Map.DeepMineAlphaNorth: return new MapTitleInfo(index, "Deep Mine Alpha (north)");
                    case MM4Map.DeepMineAlphaEast: return new MapTitleInfo(index, "Deep Mine Alpha (east)");
                    case MM4Map.DeepMineAlphaNorthEast: return new MapTitleInfo(index, "Deep Mine Alpha (northeast)");
                    case MM4Map.DeepMineThetaNorth: return new MapTitleInfo(index, "Deep Mine Theta (north)");
                    case MM4Map.DeepMineThetaEast: return new MapTitleInfo(index, "Deep Mine Theta (east)");
                    case MM4Map.DeepMineThetaNorthEast: return new MapTitleInfo(index, "Deep Mine Theta (northeast)");
                    case MM4Map.DeepMineKappaNorth: return new MapTitleInfo(index, "Deep Mine Kappa (north)");
                    case MM4Map.DeepMineKappaEast: return new MapTitleInfo(index, "Deep Mine Kappa (east)");
                    case MM4Map.DeepMineKappaNorthEast: return new MapTitleInfo(index, "Deep Mine Kappa (northeast)");
                    case MM4Map.DeepMineOmegaNorth: return new MapTitleInfo(index, "Deep Mine Omega (north)");
                    case MM4Map.DeepMineOmegaEast: return new MapTitleInfo(index, "Deep Mine Omega (east)");
                    case MM4Map.DeepMineOmegaNorthEast: return new MapTitleInfo(index, "Deep Mine Omega (northeast)");
                    case MM4Map.E1DragonCaveNorth: return new MapTitleInfo(index, "E-1, Dragon Cave (north)");
                    case MM4Map.E1DragonCaveEast: return new MapTitleInfo(index, "E-1, Dragon Cave (east)");
                    case MM4Map.E1DragonCaveNorthEast: return new MapTitleInfo(index, "E-1, Dragon Cave (northeast)");
                    case MM4Map.E4AncientTempleofYakNorth: return new MapTitleInfo(index, "E-4, Ancient Templeof Yak (north)");
                    case MM4Map.E4AncientTempleofYakEast: return new MapTitleInfo(index, "E-4, Ancient Templeof Yak (east)");
                    case MM4Map.E4AncientTempleofYakNorthEast: return new MapTitleInfo(index, "E-4, Ancient Temple of Yak (northeast)");
                    case MM4Map.C4TombOfAThousandTerrorsNorth: return new MapTitleInfo(index, "C-4, Tomb of a Thousand Terrors (north)");
                    case MM4Map.C4TombOfAThousandTerrorsEast: return new MapTitleInfo(index, "C-4, Tomb of a Thousand Terrors (east)");
                    case MM4Map.C4TombOfAThousandTerrorsNorthEast: return new MapTitleInfo(index, "C-4, Tomb of a Thousand Terrors (northeast)");
                    case MM4Map.B4GolemDungeonNorth: return new MapTitleInfo(index, "B-4, Golem Dungeon (north)");
                    case MM4Map.B4GolemDungeonEast: return new MapTitleInfo(index, "B-4, Golem Dungeon (east)");
                    case MM4Map.B4GolemDungeonNorthEast: return new MapTitleInfo(index, "B-4, Golem Dungeon (northeast)");
                    case MM4Map.B1SphinxBodyNorth: return new MapTitleInfo(index, "B-1, Sphinx Body (north)");
                    default: return new MapTitleInfo(index, String.Format("UnknownMap({0})", index));
                }
            }
            int iOffsetIndex = index + 256;
            switch ((MM5Map)index)
            {
                case MM5Map.A1Surface: return new MapTitleInfo(iOffsetIndex, "A-1, Darkside Surface");
                case MM5Map.A2Surface: return new MapTitleInfo(iOffsetIndex, "A-2, Darkside Surface");
                case MM5Map.A3Surface: return new MapTitleInfo(iOffsetIndex, "A-3, Darkside Surface");
                case MM5Map.A4Surface: return new MapTitleInfo(iOffsetIndex, "A-4, Darkside Surface");
                case MM5Map.B1Surface: return new MapTitleInfo(iOffsetIndex, "B-1, Darkside Surface");
                case MM5Map.B2Surface: return new MapTitleInfo(iOffsetIndex, "B-2, Darkside Surface");
                case MM5Map.B3Surface: return new MapTitleInfo(iOffsetIndex, "B-3, Darkside Surface");
                case MM5Map.B4Surface: return new MapTitleInfo(iOffsetIndex, "B-4, Darkside Surface");
                case MM5Map.C1Surface: return new MapTitleInfo(iOffsetIndex, "C-1, Darkside Surface");
                case MM5Map.C2Surface: return new MapTitleInfo(iOffsetIndex, "C-2, Darkside Surface");
                case MM5Map.C3Surface: return new MapTitleInfo(iOffsetIndex, "C-3, Darkside Surface");
                case MM5Map.C4Surface: return new MapTitleInfo(iOffsetIndex, "C-4, Darkside Surface");
                case MM5Map.D1Surface: return new MapTitleInfo(iOffsetIndex, "D-1, Darkside Surface");
                case MM5Map.D2Surface: return new MapTitleInfo(iOffsetIndex, "D-2, Darkside Surface");
                case MM5Map.D3Surface: return new MapTitleInfo(iOffsetIndex, "D-3, Darkside Surface");
                case MM5Map.D4Surface: return new MapTitleInfo(iOffsetIndex, "D-4, Darkside Surface");
                case MM5Map.E1Surface: return new MapTitleInfo(iOffsetIndex, "E-1, Darkside Surface");
                case MM5Map.E2Surface: return new MapTitleInfo(iOffsetIndex, "E-2, Darkside Surface");
                case MM5Map.E3Surface: return new MapTitleInfo(iOffsetIndex, "E-3, Darkside Surface");
                case MM5Map.E4Surface: return new MapTitleInfo(iOffsetIndex, "E-4, Darkside Surface");
                case MM5Map.F1Surface: return new MapTitleInfo(iOffsetIndex, "F-1, Darkside Surface");
                case MM5Map.F2Surface: return new MapTitleInfo(iOffsetIndex, "F-2, Darkside Surface");
                case MM5Map.F3Surface: return new MapTitleInfo(iOffsetIndex, "F-3, Darkside Surface");
                case MM5Map.F4Surface: return new MapTitleInfo(iOffsetIndex, "F-4, Darkside Surface");
                case MM5Map.A1ElementalPlaneOfFire: return new MapTitleInfo(iOffsetIndex, "A-1, Elemental Plane of Fire");
                case MM5Map.F1ElementalPlaneOfAir: return new MapTitleInfo(iOffsetIndex, "F-1, Elemental Plane of Air");
                case MM5Map.F4ElementalPlaneOfEarth: return new MapTitleInfo(iOffsetIndex, "F-4, Elemental Plane of Earth");
                case MM5Map.A4ElementalPlaneOfWater: return new MapTitleInfo(iOffsetIndex, "A-4, Elemental Plane of Water");
                case MM5Map.A4Castleview: return new MapTitleInfo(iOffsetIndex, "A-4, Castleview");
                case MM5Map.A4CastleviewSewer: return new MapTitleInfo(iOffsetIndex, "A-4, Castleview Sewer");
                case MM5Map.E3Sandcaster: return new MapTitleInfo(iOffsetIndex, "E-3, Sandcaster");
                case MM5Map.E3SandcasterSewer: return new MapTitleInfo(iOffsetIndex, "E-3, Sandcaster Sewer");
                case MM5Map.F2Lakeside: return new MapTitleInfo(iOffsetIndex, "F-2, Lakeside");
                case MM5Map.F2LakesideSewer: return new MapTitleInfo(iOffsetIndex, "F-2, Lakeside Sewer");
                case MM5Map.B2Necropolis: return new MapTitleInfo(iOffsetIndex, "B-2, Necropolis");
                case MM5Map.B2NecropolisSewer: return new MapTitleInfo(iOffsetIndex, "B-2, Necropolis Sewer");
                case MM5Map.C2Olympus: return new MapTitleInfo(iOffsetIndex, "C-2, Olympus");
                case MM5Map.C2OlympusSewer: return new MapTitleInfo(iOffsetIndex, "C-2, Olympus Sewer");
                case MM5Map.B2GemstoneMines: return new MapTitleInfo(iOffsetIndex, "B-2, Gemstone Mines");
                case MM5Map.E4TrollHoles: return new MapTitleInfo(iOffsetIndex, "E-4, Troll Holes");
                case MM5Map.A4CastleKalindraLevel1: return new MapTitleInfo(iOffsetIndex, "A-4, Castle Kalindra Level 1");
                case MM5Map.A4CastleKalindraLevel2: return new MapTitleInfo(iOffsetIndex, "A-4, Castle Kalindra Level 2");
                case MM5Map.A4CastleKalindraLevel3: return new MapTitleInfo(iOffsetIndex, "A-4, Castle Kalindra Level 3");
                case MM5Map.A4CastleKalindraDungeon: return new MapTitleInfo(iOffsetIndex, "A-4, Castle Kalindra Dungeon");
                case MM5Map.F1CastleBlackfangLevel1: return new MapTitleInfo(iOffsetIndex, "F-1, Castle Blackfang Level 1");
                case MM5Map.F1CastleBlackfangLevel2: return new MapTitleInfo(iOffsetIndex, "F-1, Castle Blackfang Level 2");
                case MM5Map.F1CastleBlackfangLevel3: return new MapTitleInfo(iOffsetIndex, "F-1, Castle Blackfang Level 3");
                case MM5Map.F1CastleBlackfangDungeon: return new MapTitleInfo(iOffsetIndex, "F-1, Castle Blackfang Dungeon");
                case MM5Map.A1CastleAlamarLevel1: return new MapTitleInfo(iOffsetIndex, "A-1, Castle Alamar Level 1");
                case MM5Map.A1CastleAlamarLevel2: return new MapTitleInfo(iOffsetIndex, "A-1, Castle Alamar Level 2");
                case MM5Map.A1CastleAlamarLevel3: return new MapTitleInfo(iOffsetIndex, "A-1, Castle Alamar Level 3");
                case MM5Map.A1CastleAlamarDungeon: return new MapTitleInfo(iOffsetIndex, "A-1, Castle Alamar Dungeon");
                case MM5Map.A4EllingersTowerLevel1: return new MapTitleInfo(iOffsetIndex, "A-4, Ellinger's Tower Level 1");
                case MM5Map.A4EllingersTowerLevel2: return new MapTitleInfo(iOffsetIndex, "A-4, Ellinger's Tower Level 2");
                case MM5Map.A4EllingersTowerLevel3: return new MapTitleInfo(iOffsetIndex, "A-4, Ellinger's Tower Level 3");
                case MM5Map.A4EllingersTowerLevel4: return new MapTitleInfo(iOffsetIndex, "A-4, Ellinger's Tower Level 4");
                case MM5Map.A3WesternTowerLevel1: return new MapTitleInfo(iOffsetIndex, "A-3, Western Tower Level 1");
                case MM5Map.A3WesternTowerLevel2: return new MapTitleInfo(iOffsetIndex, "A-3, Western Tower Level 2");
                case MM5Map.A3WesternTowerLevel3: return new MapTitleInfo(iOffsetIndex, "A-3, Western Tower Level 3");
                case MM5Map.A3WesternTowerLevel4: return new MapTitleInfo(iOffsetIndex, "A-3, Western Tower Level 4");
                case MM5Map.D4SouthernTowerLevel1: return new MapTitleInfo(iOffsetIndex, "D-4, Southern Tower Level 1");
                case MM5Map.D4SouthernTowerLevel2: return new MapTitleInfo(iOffsetIndex, "D-4, Southern Tower Level 2");
                case MM5Map.D4SouthernTowerLevel3: return new MapTitleInfo(iOffsetIndex, "D-4, Southern Tower Level 3");
                case MM5Map.D4SouthernTowerLevel4: return new MapTitleInfo(iOffsetIndex, "D-4, Southern Tower Level 4");
                case MM5Map.F3EasternTowerLevel1: return new MapTitleInfo(iOffsetIndex, "F-3, Eastern Tower Level 1");
                case MM5Map.F3EasternTowerLevel2: return new MapTitleInfo(iOffsetIndex, "F-3, Eastern Tower Level 2");
                case MM5Map.F3EasternTowerLevel3: return new MapTitleInfo(iOffsetIndex, "F-3, Eastern Tower Level 3");
                case MM5Map.F3EasternTowerLevel4: return new MapTitleInfo(iOffsetIndex, "F-3, Eastern Tower Level 4");
                case MM5Map.D1NorthernTowerLevel1: return new MapTitleInfo(iOffsetIndex, "D-1, Northern Tower Level 1");
                case MM5Map.D1NorthernTowerLevel2: return new MapTitleInfo(iOffsetIndex, "D-1, Northern Tower Level 2");
                case MM5Map.D1NorthernTowerLevel3: return new MapTitleInfo(iOffsetIndex, "D-1, Northern Tower Level 3");
                case MM5Map.D1NorthernTowerLevel4: return new MapTitleInfo(iOffsetIndex, "D-1, Northern Tower Level 4");
                case MM5Map.C4TempleOfBarkLevel1: return new MapTitleInfo(iOffsetIndex, "C-4, Temple of Bark Level 1");
                case MM5Map.C4TempleOfBarkLevel2: return new MapTitleInfo(iOffsetIndex, "C-4, Temple of Bark Level 2");
                case MM5Map.C4TempleOfBarkLevel3: return new MapTitleInfo(iOffsetIndex, "C-4, Temple of Bark Level 3");
                case MM5Map.C4TempleOfBarkLevel4: return new MapTitleInfo(iOffsetIndex, "C-4, Temple of Bark Level 4");
                case MM5Map.C4TempleOfBarkLevel5: return new MapTitleInfo(iOffsetIndex, "C-4, Temple of Bark Level 5");
                case MM5Map.F2LostSoulsDungeonLevel1: return new MapTitleInfo(iOffsetIndex, "F-2, Lost Souls Dungeon Level 1");
                case MM5Map.F2LostSoulsDungeonLevel3: return new MapTitleInfo(iOffsetIndex, "F-2, Lost Souls Dungeon Level 3");
                case MM5Map.F2LostSoulsDungeonLevel2: return new MapTitleInfo(iOffsetIndex, "F-2, Lost Souls Dungeon Level 2");
                case MM5Map.F2LostSoulsDungeonLevel4: return new MapTitleInfo(iOffsetIndex, "F-2, Lost Souls Dungeon Level 4");
                case MM5Map.F2LostSoulsDungeonLevel5: return new MapTitleInfo(iOffsetIndex, "F-2, Lost Souls Dungeon Level 5");
                case MM5Map.D2TheGreatPyramidLevel1: return new MapTitleInfo(iOffsetIndex, "D-2, The Great Pyramid Level 1");
                case MM5Map.D2TheGreatPyramidLevel2: return new MapTitleInfo(iOffsetIndex, "D-2, The Great Pyramid Level 2");
                case MM5Map.D2TheGreatPyramidLevel3: return new MapTitleInfo(iOffsetIndex, "D-2, The Great Pyramid Level 3");
                case MM5Map.D2TheGreatPyramidLevel4: return new MapTitleInfo(iOffsetIndex, "D-2, The Great Pyramid Level 4");
                case MM5Map.B2EscapePod1: return new MapTitleInfo(iOffsetIndex, "B-2, Escape Pod 1");
                case MM5Map.B1EscapePod2: return new MapTitleInfo(iOffsetIndex, "B-1, Escape Pod 2");
                case MM5Map.A1SkyroadA1: return new MapTitleInfo(iOffsetIndex, "A-1, Skyroad A1");
                case MM5Map.A2SkyroadA2: return new MapTitleInfo(iOffsetIndex, "A-2, Skyroad A2");
                case MM5Map.A3SkyroadA3: return new MapTitleInfo(iOffsetIndex, "A-3, Skyroad A3");
                case MM5Map.A4SkyroadA4: return new MapTitleInfo(iOffsetIndex, "A-4, Skyroad A4");
                case MM5Map.B1SkyroadB1: return new MapTitleInfo(iOffsetIndex, "B-1, Skyroad B1");
                case MM5Map.B2SkyroadB2: return new MapTitleInfo(iOffsetIndex, "B-2, Skyroad B2");
                case MM5Map.B3SkyroadB3: return new MapTitleInfo(iOffsetIndex, "B-3, Skyroad B3");
                case MM5Map.B4SkyroadB4: return new MapTitleInfo(iOffsetIndex, "B-4, Skyroad B4");
                case MM5Map.C1SkyroadC1: return new MapTitleInfo(iOffsetIndex, "C-1, Skyroad C1");
                case MM5Map.C2SkyroadC2: return new MapTitleInfo(iOffsetIndex, "C-2, Skyroad C2");
                case MM5Map.C3SkyroadC3: return new MapTitleInfo(iOffsetIndex, "C-3, Skyroad C3");
                case MM5Map.C4SkyroadC4: return new MapTitleInfo(iOffsetIndex, "C-4, Skyroad C4");
                case MM5Map.D1SkyroadD1: return new MapTitleInfo(iOffsetIndex, "D-1, Skyroad D1");
                case MM5Map.D2SkyroadD2: return new MapTitleInfo(iOffsetIndex, "D-2, Skyroad D2");
                case MM5Map.D3SkyroadD3: return new MapTitleInfo(iOffsetIndex, "D-3, Skyroad D3");
                case MM5Map.D4SkyroadD4: return new MapTitleInfo(iOffsetIndex, "D-4, Skyroad D4");
                case MM5Map.E1SkyroadE1: return new MapTitleInfo(iOffsetIndex, "E-1, Skyroad E1");
                case MM5Map.E2SkyroadE2: return new MapTitleInfo(iOffsetIndex, "E-2, Skyroad E2");
                case MM5Map.E3SkyroadE3: return new MapTitleInfo(iOffsetIndex, "E-3, Skyroad E3");
                case MM5Map.E4SkyroadE4: return new MapTitleInfo(iOffsetIndex, "E-4, Skyroad E4");
                case MM5Map.F1SkyroadF1: return new MapTitleInfo(iOffsetIndex, "F-1, Skyroad F1");
                case MM5Map.F2SkyroadF2: return new MapTitleInfo(iOffsetIndex, "F-2, Skyroad F2");
                case MM5Map.F3SkyroadF3: return new MapTitleInfo(iOffsetIndex, "F-3, Skyroad F3");
                case MM5Map.F4SkyroadF4: return new MapTitleInfo(iOffsetIndex, "F-4, Skyroad F4");
                case MM5Map.B3DarkstoneTowerLevel1: return new MapTitleInfo(iOffsetIndex, "B-3, Darkstone Tower Level 1");
                case MM5Map.B3DarkstoneTowerLevel2: return new MapTitleInfo(iOffsetIndex, "B-3, Darkstone Tower Level 2");
                case MM5Map.B3DarkstoneTowerLevel3: return new MapTitleInfo(iOffsetIndex, "B-3, Darkstone Tower Level 3");
                case MM5Map.B3DarkstoneTowerLevel4: return new MapTitleInfo(iOffsetIndex, "B-3, Darkstone Tower Level 4");
                case MM5Map.D1DragonTowerLevel1: return new MapTitleInfo(iOffsetIndex, "D-1, Dragon Tower Level 1");
                case MM5Map.D1DragonTowerLevel2: return new MapTitleInfo(iOffsetIndex, "D-1, Dragon Tower Level 2");
                case MM5Map.D1DragonTowerLevel3: return new MapTitleInfo(iOffsetIndex, "D-1, Dragon Tower Level 3");
                case MM5Map.D1DragonTowerLevel4: return new MapTitleInfo(iOffsetIndex, "D-1, Dragon Tower Level 4");
                case MM5Map.E3DungeonOfDeathLevel1: return new MapTitleInfo(iOffsetIndex, "E-3, Dungeon of Death Level 1");
                case MM5Map.E3DungeonOfDeathLevel2: return new MapTitleInfo(iOffsetIndex, "E-3, Dungeon of Death Level 2");
                case MM5Map.E3DungeonOfDeathLevel3: return new MapTitleInfo(iOffsetIndex, "E-3, Dungeon of Death Level 3");
                case MM5Map.E3DungeonOfDeathLevel4: return new MapTitleInfo(iOffsetIndex, "E-3, Dungeon of Death Level 4");
                case MM5Map.A2SouthernSphinxLevel1: return new MapTitleInfo(iOffsetIndex, "A-2, Southern Sphinx Level 1");
                case MM5Map.A2SouthernSphinxLevel2: return new MapTitleInfo(iOffsetIndex, "A-2, Southern Sphinx Level 2");
                case MM5Map.A2SouthernSphinxLevel3: return new MapTitleInfo(iOffsetIndex, "A-2, Southern Sphinx Level 3");
                case MM5Map.D1DragonClouds: return new MapTitleInfo(iOffsetIndex, "D-1, Dragon Clouds");
                case MM5Map.B3CloudsOfTheAncients: return new MapTitleInfo(iOffsetIndex, "B-3, Clouds of the Ancients");
                case MM5Map.A4CastleviewNorth: return new MapTitleInfo(iOffsetIndex, "A-4, Castleview North");
                case MM5Map.A4CastleviewEast: return new MapTitleInfo(iOffsetIndex, "A-4, Castleview East");
                case MM5Map.A4CastleviewNorthEast: return new MapTitleInfo(iOffsetIndex, "A-4, Castleview NorthEast");
                case MM5Map.A4CastleviewSewerNorth: return new MapTitleInfo(iOffsetIndex, "A-4, Castleview Sewer North");
                case MM5Map.A4CastleviewSewerEast: return new MapTitleInfo(iOffsetIndex, "A-4, Castleview Sewer East");
                case MM5Map.A4CastleviewSewerNorthEast: return new MapTitleInfo(iOffsetIndex, "A-4, Castleview Sewer NorthEast");
                case MM5Map.E3SandcasterNorth: return new MapTitleInfo(iOffsetIndex, "E-3, Sandcaster North");
                case MM5Map.E3SandcasterEast: return new MapTitleInfo(iOffsetIndex, "E-3, Sandcaster East");
                case MM5Map.E3SandcasterNorthEast: return new MapTitleInfo(iOffsetIndex, "E-3, Sandcaster NorthEast");
                case MM5Map.E3SandcasterSewerNorth: return new MapTitleInfo(iOffsetIndex, "E-3, Sandcaster Sewer North");
                case MM5Map.E3SandcasterSewerEast: return new MapTitleInfo(iOffsetIndex, "E-3, Sandcaster Sewer East");
                case MM5Map.E3SandcasterSewerNorthEast: return new MapTitleInfo(iOffsetIndex, "E-3, Sandcaster Sewer NorthEast");
                case MM5Map.B2GemstoneMinesNorth: return new MapTitleInfo(iOffsetIndex, "B-2, Gemstone Mines North");
                case MM5Map.B2GemstoneMinesEast: return new MapTitleInfo(iOffsetIndex, "B-2, Gemstone Mines East");
                case MM5Map.B2GemstoneMinesNorthEast: return new MapTitleInfo(iOffsetIndex, "B-2, Gemstone Mines NorthEast");
                case MM5Map.E4TrollHolesNorth: return new MapTitleInfo(iOffsetIndex, "E-4, Troll Holes North");
                case MM5Map.E4TrollHolesEast: return new MapTitleInfo(iOffsetIndex, "E-4, Troll Holes East");
                case MM5Map.E4TrollHolesNorthEast: return new MapTitleInfo(iOffsetIndex, "E-4, Troll Holes NorthEast");
                case MM5Map.C4TempleOfBarkLevel5North: return new MapTitleInfo(iOffsetIndex, "C-4, Temple of Bark Level 5 North");
                case MM5Map.C4TempleOfBarkLevel5East: return new MapTitleInfo(iOffsetIndex, "C-4, Temple of Bark Level 5 East");
                case MM5Map.C4TempleOfBarkLevel5NorthEast: return new MapTitleInfo(iOffsetIndex, "C-4, Temple of Bark Level 5 NorthEast");
                case MM5Map.F2LostSoulsDungeonLevel5North: return new MapTitleInfo(iOffsetIndex, "F-2, Lost Souls Dungeon Level 5 North");
                case MM5Map.F2LostSoulsDungeonLevel5East: return new MapTitleInfo(iOffsetIndex, "F-2, Lost Souls Dungeon Level 5 East");
                case MM5Map.F2LostSoulsDungeonLevel5NorthEast: return new MapTitleInfo(iOffsetIndex, "F-2, Lost Souls Dungeon Level 5 NorthEast");
                case MM5Map.D2TheGreatPyramidLevel1North: return new MapTitleInfo(iOffsetIndex, "D-2, The Great Pyramid Level 1 North");
                case MM5Map.D2TheGreatPyramidLevel1East: return new MapTitleInfo(iOffsetIndex, "D-2, The Great Pyramid Level 1 East");
                case MM5Map.D2TheGreatPyramidLevel1NorthEast: return new MapTitleInfo(iOffsetIndex, "D-2, The Great Pyramid Level 1 NorthEast");
                case MM5Map.D2TheGreatPyramidLevel2North: return new MapTitleInfo(iOffsetIndex, "D-2, The Great Pyramid Level 2 North");
                case MM5Map.D2TheGreatPyramidLevel2East: return new MapTitleInfo(iOffsetIndex, "D-2, The Great Pyramid Level 2 East");
                case MM5Map.D2TheGreatPyramidLevel2NorthEast: return new MapTitleInfo(iOffsetIndex, "D-2, The Great Pyramid Level 2 NorthEast");
                case MM5Map.D2TheGreatPyramidLevel3North: return new MapTitleInfo(iOffsetIndex, "D-2, The Great Pyramid Level 3 North");
                case MM5Map.D2TheGreatPyramidLevel3East: return new MapTitleInfo(iOffsetIndex, "D-2, The Great Pyramid Level 3 East");
                case MM5Map.D2TheGreatPyramidLevel3NorthEast: return new MapTitleInfo(iOffsetIndex, "D-2, The Great Pyramid Level 3 NorthEast");
                case MM5Map.B2EscapePod1East: return new MapTitleInfo(iOffsetIndex, "B-2, Escape Pod1East");
                case MM5Map.B1EscapePod2East: return new MapTitleInfo(iOffsetIndex, "B-1, Escape Pod2East");
                case MM5Map.E3DungeonOfDeathLevel1North: return new MapTitleInfo(iOffsetIndex, "E-3, Dungeon of Death Level 1 North");
                case MM5Map.E3DungeonOfDeathLevel1East: return new MapTitleInfo(iOffsetIndex, "E-3, Dungeon of Death Level 1 East");
                case MM5Map.E3DungeonOfDeathLevel1NorthEast: return new MapTitleInfo(iOffsetIndex, "E-3, Dungeon of Death Level 1 NorthEast");
                case MM5Map.E3DungeonOfDeathLevel2North: return new MapTitleInfo(iOffsetIndex, "E-3, Dungeon of Death Level 2 North");
                case MM5Map.E3DungeonOfDeathLevel2East: return new MapTitleInfo(iOffsetIndex, "E-3, Dungeon of Death Level 2 East");
                case MM5Map.E3DungeonOfDeathLevel2NorthEast: return new MapTitleInfo(iOffsetIndex, "E-3, Dungeon of Death Level 2 NorthEast");
                case MM5Map.E3DungeonOfDeathLevel3North: return new MapTitleInfo(iOffsetIndex, "E-3, Dungeon of Death Level 3 North");
                case MM5Map.E3DungeonOfDeathLevel3East: return new MapTitleInfo(iOffsetIndex, "E-3, Dungeon of Death Level 3 East");
                case MM5Map.E3DungeonOfDeathLevel3NorthEast: return new MapTitleInfo(iOffsetIndex, "E-3, Dungeon of Death Level 3 NorthEast");
                case MM5Map.E3DungeonOfDeathLevel4North: return new MapTitleInfo(iOffsetIndex, "E-3, Dungeon of Death Level 4 North");
                case MM5Map.E3DungeonOfDeathLevel4East: return new MapTitleInfo(iOffsetIndex, "E-3, Dungeon of Death Level 4 East");
                case MM5Map.E3DungeonOfDeathLevel4NorthEast: return new MapTitleInfo(iOffsetIndex, "E-3, Dungeon of Death Level 4 NorthEast");
                case MM5Map.A2SouthernSphinxLevel1North: return new MapTitleInfo(iOffsetIndex, "A-2, Southern Sphinx Level 1 North");
                case MM5Map.D1DragonCloudsNorth: return new MapTitleInfo(iOffsetIndex, "D-1, Dragon Clouds North");
                case MM5Map.D1DragonCloudsEast: return new MapTitleInfo(iOffsetIndex, "D-1, Dragon Clouds East");
                case MM5Map.D1DragonCloudsNorthEast: return new MapTitleInfo(iOffsetIndex, "D-1, Dragon Clouds NorthEast");
                case MM5Map.B3CloudsOfTheAncientsNorth: return new MapTitleInfo(iOffsetIndex, "B-3, Clouds of the Ancients North");
                case MM5Map.B3CloudsOfTheAncientsEast: return new MapTitleInfo(iOffsetIndex, "B-3, Clouds of the Ancients East");
                case MM5Map.B3CloudsOfTheAncientsNorthEast: return new MapTitleInfo(iOffsetIndex, "B-3, Clouds of the Ancients NorthEast");
                default: return new MapTitleInfo(index, String.Format("UnknownMap({0})", iOffsetIndex));
            }
        }

        public override MapTitleInfo GetMapTitle(int index)
        {
            return GetMapTitlePair(index % 256, index / 256);
        }

        public override bool SetCharacterBytes(int iAddress, byte[] bytes)
        {
            if (!IsValid)
                return false;

            return WriteOffset(Memory.PartyInfo + (iAddress * MM45Character.SizeInBytes), bytes);
        }

        public override int MaxBackpackSize { get { return 36; } }

        public override void RandomizeBackpack(BaseCharacter baseChar, ItemType type, bool bUsableOnly, bool bSingleModifierOnly)
        {
            if (!(baseChar is MM45Character))
                return;

            // MM4/5 stores equipped/unequipped in the same list, so we can't just completely randomize the inventory
            // Also MM4/5 has four different inventories for weapons, armor, accessories, and misc.  So we have to
            // randomize them separately.
            MM45Character mmChar = baseChar as MM45Character;

            List<Item> equippedItems = GetEquippedItems(mmChar.Address);
            List<Item> backpack = new List<Item>(36-equippedItems.Count);

            int iEmptyWeapons = 9 - equippedItems.Count(n => n.ItemBaseType == ItemType.Weapon);
            int iEmptyArmor = 9 - equippedItems.Count(n => n.ItemBaseType == ItemType.Armor);
            int iEmptyAccessories = 9 - equippedItems.Count(n => n.ItemBaseType == ItemType.Accessory);
            int iEmptyMiscellaneous = 9 - equippedItems.Count(n => n.ItemBaseType == ItemType.Miscellaneous);

            // Add random items to fill up or replace the unequipped spaces in the backpack
            if (type == ItemType.Weapon || type == ItemType.None)
                for (int i = 0; i < iEmptyWeapons; i++)
                    backpack.Add(MM45Item.CreateRandom(ItemType.Weapon, bUsableOnly ? baseChar : null));
            if (type == ItemType.Armor || type == ItemType.None)
                for (int i = 0; i < iEmptyArmor; i++)
                    backpack.Add(MM45Item.CreateRandom(ItemType.Armor, bUsableOnly ? baseChar : null));
            if (type == ItemType.Accessory || type == ItemType.None)
                for (int i = 0; i < iEmptyAccessories; i++)
                    backpack.Add(MM45Item.CreateRandom(ItemType.Accessory, bUsableOnly ? baseChar : null));
            if (type == ItemType.Miscellaneous || type == ItemType.None)
                for (int i = 0; i < iEmptyMiscellaneous; i++)
                    backpack.Add(MM45Item.CreateRandom(ItemType.Miscellaneous, bUsableOnly ? baseChar : null));

            SetBackpack(baseChar.BasicAddress, backpack);
        }

        public List<Item> GetEquippedItems(int iCharAddress)
        {
            return GetBackpack(iCharAddress, BackpackType.EquippedOnly);
        }

        public List<Item> GetUnequippedItems(int iCharAddress)
        {
            return GetBackpack(iCharAddress, BackpackType.UnequippedOnly);
        }

        public override List<Item> GetBackpack(int iCharAddress)
        {
            return GetBackpack(iCharAddress, BackpackType.All);
        }

        public List<Item> GetBackpack(int iCharAddress, BackpackType type)
        {
            if (!IsValid)
                return null;

            List<Item> backpack = new List<Item>(36);

            long iAddress = Memory.PartyInfo + (iCharAddress * MM45Character.SizeInBytes);

            MemoryBytes bytes = ReadOffset(iAddress + MM45.Offsets.Inventory, 9 * 4 * 4);
            if (bytes == null)
                return null;

            MM45BackpackBytes bpBytes = new MM45BackpackBytes(bytes);

            int iMemoryIndex = 0;
            foreach(byte[,] itemType in bpBytes.All)
            {
                for (int i = 0; i < 9; i++)
                {
                    bool bEquipped = false;
                    if (itemType != bpBytes.Miscellaneous)
                        bEquipped = (itemType[i,3] != 0);

                    bool bAddItem = false;
                    switch(type)
                    {
                        case BackpackType.All:
                            bAddItem = true;
                            break;
                        case BackpackType.EquippedOnly:
                            bAddItem = bEquipped;
                            break;
                        case BackpackType.UnequippedOnly:
                            bAddItem = !bEquipped;
                            break;
                        default:
                            break;
                    }

                    if (bAddItem)
                    {
                        MM45Item item = MM45Item.Create(bytes, bpBytes.GetType(itemType), iMemoryIndex * 4);
                        if (item.Index != 0)
                        {
                            item.MemoryIndex = iMemoryIndex;
                            backpack.Add(item);
                        }
                    }

                    iMemoryIndex++;
                }
            }

            return backpack;
        }

        public override SetBackpackResult SetBackpack(int iCharAddress, List<Item> items, bool bRemoveEquipped = false)
        {
            if (!IsValid)
                return SetBackpackResult.InvalidHacker;

            if (iCharAddress < 0)
                return SetBackpackResult.InvalidPosition;

            long iAddress = Memory.PartyInfo + (iCharAddress * MM45Character.SizeInBytes);

            List<Item> equipped = bRemoveEquipped ? new List<Item>() : GetEquippedItems(iCharAddress);

            MM45BackpackBytes bpBytes = MM45BackpackBytes.Create(equipped);
            if (!bpBytes.Add(items))
                return SetBackpackResult.InsufficientSpace;

            WriteOffset(iAddress + MM45.Offsets.Inventory, bpBytes.GetBytes());

            return SetBackpackResult.Success;
        }

        public override bool SpellsUseLevelOnly { get { return true; } }

        public bool IsTown(int map, bool bWithInn = false)
        {
            return (map > 255 ? IsTown((MM5Map)(map % 256), bWithInn) : IsTown((MM4Map)map, bWithInn));
        }

        public bool IsTown(MM4Map map, bool bWithInn = false)
        {
            switch (map)
            {
                case MM4Map.F3Vertigo:
                case MM4Map.C3Rivercity:
                case MM4Map.E1ShangriLa:
                    return true;
                case MM4Map.A3Winterkill:
                case MM4Map.C2Asp:
                case MM4Map.D4Nightshadow:
                    return !bWithInn;
                default:
                    return false;
            }
        }

        public bool IsTown(MM5Map map, bool bWithInn = false)
        {
            switch (map)
            {
                case MM5Map.E3Sandcaster:
                case MM5Map.A4Castleview:
                case MM5Map.C2Olympus:
                    return true;
                case MM5Map.B2Necropolis:
                case MM5Map.F2Lakeside:
                    return !bWithInn;
                default:
                    return false;
            }
        }

        public bool IsInn(int map, Point pt)
        {
            return map > 255 ? IsInn((MM5Map)(map % 256), pt) : IsInn((MM4Map)map, pt);
        }

        public bool IsInn(MM4Map map, Point pt)
        {
            switch (map)
            {
                case MM4Map.F3Vertigo: return pt.X == 24 && pt.Y == 5;
                case MM4Map.C3Rivercity: return pt.X == 24 && pt.Y == 16;
                case MM4Map.E1ShangriLa: return pt.X == 14 && pt.Y == 13;
                default: return false;
            }
        }

        public bool IsInn(MM5Map map, Point pt)
        {
            switch (map)
            {
                case MM5Map.A4Castleview: return pt.X == 20 && pt.Y == 25;
                case MM5Map.E3Sandcaster: return pt.X == 9 && pt.Y == 13;
                case MM5Map.C2Olympus: return pt.X == 9 && pt.Y == 8;
                default: return false;
            }
        }

        public override bool IsSurface(int map)
        {
            return (map > 255 ? IsOutside((MM5Map)(map % 256)) : IsOutside((MM4Map)map));
        }

        public bool IsOutside(MM4Map map)
        {
            return (map >= MM4Map.A1Surface && map <= MM4Map.F4Surface);
        }

        public bool IsOutside(MM5Map map)
        {
            if (map >= MM5Map.A1Surface && map <= MM5Map.A4ElementalPlaneOfWater)
                return true;
            if (map >= MM5Map.A1SkyroadA1 && map <= MM5Map.F4SkyroadF4)
                return true;
            return false;
        }

        public MM45Side GetSide()
        {
            if (!IsValid)
                return MM45Side.Unknown;

            if (ReadByte(Memory.WorldSide) == 0)
                return MM45Side.Clouds;

            return MM45Side.Darkside;
        }

        public override int GetMonsterListIndex()
        {
            return GetMonsterSide() == MM45Side.Darkside ? 1 : 0;
        }

        public MM45Side GetMonsterSide()
        {
            if (!IsValid)
                return MM45Side.Unknown;

            if (ReadByte(Memory.MonsterSide) == 0)
                return MM45Side.Clouds;

            return MM45Side.Darkside;
        }

        public byte GetMapSide()
        {
            return ReadByte(Memory.WorldSide3);
        }

        public override int GetCurrentMapIndex()
        {
            if (!IsValid)
                return -1;

            byte bIndex = ReadByte(Memory.CurrentMapIndex);

            return GetMapSide() * 256 + bIndex;
        }

        public LocationInformation GetLocationForce()
        {
            if (!IsValid)
                return LocationInformation.Empty;

            MemoryBytes bytes = ReadOffset(Memory.FacingAndLocation, 3);
            if (bytes == null)
                return LocationInformation.Empty;

            LocationInformation info = new LocationInformation(new Point(bytes[1], bytes[2]));
            switch (bytes[0])
            {
                case 0:
                    info.Facing = Direction.Up;
                    break;
                case 1:
                    info.Facing = Direction.Right;
                    break;
                case 2:
                    info.Facing = Direction.Down;
                    break;
                case 3:
                    info.Facing = Direction.Left;
                    break;
                default:
                    info.Facing = Direction.Up;
                    break;
            }

            byte bKey = ReadByte(Memory.LastKeypress);
            switch (bKey)
            {
                case 0xF0: info.LastKeypress = Keys.Left; break;
                case 0xF1: info.LastKeypress = Keys.Right; break;
                case 0xF2: info.LastKeypress = Keys.Up; break;
                case 0xF3: info.LastKeypress = Keys.Down; break;
                case 42: info.LastKeypress = Keys.B; break;
                case 13: info.LastKeypress = Keys.Enter; break;
                default: info.LastKeypress = (Keys)bKey; break;
            }

            info.NumChars = ReadByte(Memory.NumChars);

            info.MapIndex = GetCurrentMapIndex();

            info.Town = IsTown(info.MapIndex);

            MainState main = GetMainState();

            info.InInn = (main == MainState.Inn);

            if (!info.InInn)
                info.InInn = IsInn(info.MapIndex, info.PrimaryCoordinates);

            info.CanUseBag = (IsTown(info.MapIndex, true) || Global.Cheats) && !info.InInn;  // Using the bag while in an Inn will result in deleted items!
            info.LightDistance = GetLightDistance(info.PrimaryCoordinates);

            return info;
        }

        public override string BagOfHoldingRequirement { get { return "in a town that has an Inn"; } }

        public override MapData GetMapData(bool bIncludeStrings, int iMapIndex)
        {
            if (!IsValid)
                return null;

            byte side = GetMapSide();

            if (iMapIndex == -1)
                iMapIndex = 0;

            LocationInformation location = GetLocation();

            MM45MapData data = new MM45MapData();

            data.FloorTileSet = ReadByte(Memory.FloorTileSet);

            byte[] bytesMap;
            GetMapInfoBytes(out bytesMap);
            data.MapBytes = new MM45MapBytes(bytesMap);
            data.VisibleObjects = GetVisibleObjects();
            data.SpecialSquares = GetSpecialSquares();
            data.ScriptInfo = GetScriptInfo() as MMScriptInfo;
            data.Index = location.MapIndex;
            data.Title = GetMapTitle(location.MapIndex);

            int iOffsetAppearance = 0;
            switch (iMapIndex)
            {
                case 0:
                    iOffsetAppearance = Memory.MapAppearance1;
                    break;
                case 1:
                    iOffsetAppearance = Memory.MapAppearance2;
                    break;
                case 2:
                    iOffsetAppearance = Memory.MapAppearance3;
                    break;
                case 3:
                    iOffsetAppearance = Memory.MapAppearance4;
                    break;
                default:
                    return null;
            }

            int iOffsetAttributes = iOffsetAppearance + 512;
            int iOffsetCartography = iOffsetAttributes + 256 + 92;

            data.Appearance = ReadOffset(iOffsetAppearance, 512);

            data.Cartography = new MapCartography();
            MemoryBytes bytesCart = ReadOffset(iOffsetCartography, 32);
            if (bytesCart == null)
                return null;
            data.Cartography.SetBytes(bytesCart, new Size(16, 16));

            data.Attributes = ReadOffset(iOffsetAttributes, 256);

            m_lastMapAppearance = new byte[512];
            Buffer.BlockCopy(data.Appearance, 0, m_lastMapAppearance, 0, 512);

            data.Outside = IsSurface(data.Index);
            data.Town = IsTown(data.Index);

            if (bIncludeStrings)
                data.ScriptInfo = GetScriptInfo() as MMScriptInfo;

            return data;
        }

        public override GameState GetGameState()
        {
            return ReadMM45GameState();
        }

        private MainState GetMainState()
        {
            UInt16 state1 = ReadUInt16(Memory.GameState1);
            UInt16 state2 = ReadUInt16(Memory.GameState2);
            UInt16 state3 = ReadUInt16(Memory.GameState3);
            MainState state = GetMainState(state1, state2, state3);
            return state;
        }

        private MainState GetMainState(UInt16 state1, UInt16 state2, UInt16 state3)
        {
            switch (state1)
            {
                case 0x19f6: return MainState.Adventuring;
                case 0x1b5c: return MainState.CharacterScreen;
                case 0x1bb8: return MainState.Inn;
                case 0x1bcc:
                    switch(state2)
                    {
                        case 0x48ca: return MainState.Inventory;
                        case 0x0000:
                            switch(state3)
                            {
                                case 0x000b: return MainState.SelectTownPortal;
                                case 0x078d:
                                case 0x0976: return MainState.SelectSpellTarget;
                                case 0x0639: return MainState.Shop;
                                case 0x2b3c: return MainState.Opening2;
                                default: return MainState.Unknown;
                            }
                        default: return MainState.Unknown;
                    }
                case 0x1ca4:
                case 0x1c96:
                    switch (state2)
                    {
                        case 0x0000: return MainState.Shop;
                        default: return MainState.Inventory;
                    }
                case 0x1c10:
                    switch (state2)
                    {
                        case 0x0000: return MainState.Shop;
                        case 0x48ca: return MainState.Inventory;
                        default: return MainState.Unknown;
                    }
                case 0x1e22: return MainState.Exchange;
                case 0x1a32:
                    switch (state2)
                    {
                        case 0x0000:
                            switch (state3)
                            {
                                case 0x03d7:
                                case 0x096a:
                                case 0x0496:
                                case 0x6b42:
                                case 0x0ca9:
                                case 0x0aba: return MainState.Bank;
                                default: return MainState.CastQuickspell;
                            }
                        default: return MainState.CastQuickspell;
                    }
                case 0x1a4a:
                    switch (state2)
                    {
                        case 0x0000:
                            switch (state3)
                            {
                                case 0x2ac6:
                                case 0x0b99:
                                case 0x5c57:
                                case 0x5fcc:
                                case 0x098b:
                                case 0x0496:
                                case 0x0be4:
                                case 0x0cb3: return MainState.Training;
                                default: return MainState.SelectSpellTarget;
                            }
                        default: return MainState.SelectSpellTarget;
                    }
                case 0x196c: return MainState.SelectSpell;
                case 0x1e4e: return MainState.QuestItems;
                case 0x1dea: return MainState.CreateExchangeStat;
                case 0x1db0:
                case 0x1da2: return MainState.CreateSelectClass;
                case 0x1eaa: return MainState.DiscardItem;
                case 0x1d0c: return MainState.EnchantItem;
                case 0x0000: return MainState.Opening;
                case 0x1902: return MainState.MainMenu;
                default: return MainState.Unknown;
            }
        }

        private Subscreen GetQuestSubscreen(byte b)
        {
            switch (b)
            {
                case 0: return Subscreen.QuestItems;
                case 1: return Subscreen.Quests;
                case 2: return Subscreen.AutoNotes;
                default: return Subscreen.Unknown;
            }
        }

        private Subscreen GetInventorySubscreen(byte b)
        {
            switch (b)
            {
                case 0: return Subscreen.Weapons;
                case 1: return Subscreen.Armor;
                case 2: return Subscreen.Accessories;
                case 3: return Subscreen.Miscellaneous;
                default: return Subscreen.Unknown;
            }
        }

        private MM45GameState ReadMM45GameState()
        {
            if (m_gsCurrent != null && m_gsCurrent.ReadTime.AddMilliseconds(50) > DateTime.Now)
                return m_gsCurrent as MM45GameState;     // Don't spam the game state from different windows

            MM45GameState state = new MM45GameState();
            state.Location = GetLocationForce();

            state.InShop = false;
            state.Inspecting = false;
            state.InCombat = false;

            state.Main = GetMainState();
            state.OriginalMain = state.Main;

            if (state.Location.NumChars == 0)
            {
                // Don't show various windows if there is no party
                if (!state.CharCreation)
                    state.Main = MainState.Unknown;
            }
            else
            {
                state.Inspecting = true;
                switch (state.Main)
                {
                    case MainState.SelectSpellTarget:
                    case MainState.SelectTownPortal:
                    case MainState.CharacterScreen:
                        break;
                    case MainState.DiscardItem:
                    case MainState.Inventory:
                        state.Subscreen = GetInventorySubscreen(ReadByte(Memory.InventorySubscreen));
                        break;
                    case MainState.EnchantItem:
                        state.ActingCharAddress = ReadByte(Memory.EnchantItemChar);
                        state.Subscreen = GetInventorySubscreen(ReadByte(Memory.EnchantItemSubscreen));
                        break;
                    case MainState.Shop:
                        state.Subscreen = GetInventorySubscreen(ReadByte(Memory.SellSubscreen));
                        state.InShop = true;
                        break;
                    case MainState.QuestItems:
                        state.Subscreen = GetQuestSubscreen(ReadByte(Memory.QuestSubscreen));
                        break;
                    default:
                        state.Inspecting = false;
                        break;
                }
            }

            state.InCombat = ReadByte(Memory.InCombat) == 1;

            if (LoadingMap)
                state.Main = MainState.LoadingMap;

            m_gsCurrent = state;
            return state;
        }

        public override bool LoadingMap { get { return ReadByte(Memory.LoadingMap1) == 111; } }

        public override Subscreen GetSubscreen()
        {
            return ReadMM45GameState().Subscreen;
        }

        public override bool SetLocation(Point ptLocation)
        {
            if (!IsValid)
                return false;

            byte[] bytes = new byte[2];
            bytes[0] = (byte)ptLocation.X;
            bytes[1] = (byte)ptLocation.Y;
            return WriteOffset(Memory.FacingAndLocation + 1, bytes);
        }

        private byte[] ReadMM45MovedThisRound()
        {
            byte[] bytes = new byte[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            if (!IsValid)
                return bytes;

            ReadOffset(Memory.MovedThisRound, bytes);
            return bytes;
        }

        public MMActiveEffects GetActiveEffects()
        {
            MMActiveEffects effects = new MMActiveEffects();

            if (!IsValid)
                return effects;

            return effects;
        }

        public override Point GetPartyPosition()
        {
            if (!IsValid)
                return Global.NullPoint;

            MemoryBytes bytes = ReadOffset(Memory.FacingAndLocation, 3);
            if (bytes == null)
                return Global.NullPoint;
            return new Point(bytes[1], bytes[2]);
        }

        public int GetMonsterCount()
        {
            return ReadByte(Memory.NumMonsters);
        }

        public override MonsterLocations GetMonsterLocations(bool bForceNew = false)
        {
            if (!IsValid)
                return null;

            int iNumMonsters = GetMonsterCount();

            byte[] bytesMonsters = new byte[20 * iNumMonsters];

            ReadOffset(Memory.MonsterPositions, bytesMonsters);
            for (int i = 0; i < bytesMonsters.Length; i += 20)
            {
                // Bytes 2, 9, 11, 12 and 16 are animation indices that change constantly and prevent change detection from working
                bytesMonsters[i + 2] = 0;
                bytesMonsters[i + 9] = 0;
                bytesMonsters[i + 11] = 0;
                bytesMonsters[i + 12] = 0;
                bytesMonsters[i + 16] = 0;
            }

            Point ptParty = GetPartyPosition();
            if (!bForceNew && m_lastMonsterPartyLocation == ptParty && Global.Compare(bytesMonsters, m_lastMonsterBytes))
                return m_lastMonsterLocations;

            m_lastMonsterBytes = bytesMonsters;
            m_lastMonsterPartyLocation = ptParty;

            m_lastMonsterLocations = new MonsterLocations(bytesMonsters, ptParty, GetMonsterSide());
            return m_lastMonsterLocations;
        }

        public override EncounterInfo GetEncounterInfo(bool bForceNew = false)
        {
            MM45Side side = GetMonsterSide();
            if (side != m_lastSide)
            {
                if (side == MM45Side.Clouds)
                    InitMM4MonsterList();
                else
                    InitMM5MonsterList();
                m_lastSide = side;
            }

            if (!IsValid)
                return null;

            MM45EncounterInfo info = new MM45EncounterInfo();
            if (info == null)
                return null;

            MonsterLocations locations = GetMonsterLocations(bForceNew);
            if (locations == null)
                return null;

            info.Party = ReadMM45PartyInfo();
            if (info.Party == null || info.Party.Bytes == null)
                return null;

            info.CharsMovedThisRound = ReadMM45MovedThisRound();
            info.MonstersMovedThisRound = info.CharsMovedThisRound.Skip(6).ToArray();
            info.ActiveEffects = GetActiveEffects();
            info.MaxEncounterIndex = locations.MaxEncounterIndex;
            info.SetMonsters(locations.Monsters);
            info.GameState = ReadMM45GameState();

            if (locations.MonsterPositions.ContainsKey(info.GameState.Location.PrimaryCoordinates))
                info.NumMeleeMonsters = locations.MonsterPositions[info.GameState.Location.PrimaryCoordinates].Monsters.Count;
            else
                info.NumMeleeMonsters = 0;

            MemoryStream stream = new MemoryStream();
            stream.Write(locations.RawBytes, 0, locations.RawBytes.Length);
            stream.Write(info.Party.Bytes, 0, info.Party.Bytes.Length);
            stream.Write(info.CharsMovedThisRound, 0, info.CharsMovedThisRound.Length);
            byte[] bytes = info.ActiveEffects.MM45Bytes;
            stream.Write(bytes, 0, bytes.Length);
            stream.WriteByte((byte) info.NumMeleeMonsters);
            info.PartyLocation = GetPartyPosition();
            stream.WriteByte((byte) info.PartyLocation.X);
            stream.WriteByte((byte) info.PartyLocation.Y);

            info.AllBytes = stream.ToArray();

            stream.Close();
            return info;
        }

        public override bool SetMonster(Monster monster) { return SetMonsterInfo(monster as MM45Monster); }

        public int GetMonsterDataOffset()
        {
            if (GetMonsterSide() == MM45Side.Clouds)
                return Memory.MM4MonsterData;
            return Memory.MM5MonsterData;
        }

        public bool SetMonsterInfo(MM45Monster monster)
        {
            if (monster == null)
                return false;

            if (!IsValid)
                return false;

            byte[] bytes = monster.GetBytes();

            WriteOffset(GetMonsterDataOffset() + (monster.Index * 60), bytes);

            long offset = Memory.MonsterPositions + (20 * monster.EncounterIndex);

            bytes = BitConverter.GetBytes((UInt16)monster.CurrentHP);
            WriteOffset(offset + MM45Memory.MonsterCurrentHP, bytes);
            WriteByte(offset + MM45Memory.MonsterCurrentIndex, (byte) monster.Index);
            WriteByte(offset + MM45Memory.MonsterCurrentX, (byte)monster.Position.X);
            WriteByte(offset + MM45Memory.MonsterCurrentY, (byte)monster.Position.Y);
            WriteByte(offset + MM45Memory.MonsterCondition, (byte) monster.MM345Condition);
            WriteByte(offset + MM45Memory.MonsterActivated, (byte)(monster.Active ? 1 : 0));

            // Reload the values into the main monster list
            if (GetMonsterSide() == MM45Side.Clouds)
                MM45.MM4MonsterList.Value.Reinitialize(this, true);
            else
                MM45.MM5MonsterList.Value.Reinitialize(this, true);
            return true;
        }

        public override bool EditMapCartography(MapCartography.EditAction action)
        {
            if (!IsValid)
                return false;

            byte[] bytes = new byte[32] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            switch (action)
            {
                case MapCartography.EditAction.FillSingle:
                case MapCartography.EditAction.FillAll:
                    for (int i = 0; i < bytes.Length; i++)
                        bytes[i] = 0xff;
                    break;
                default:
                    break;
            }

            switch (action)
            {
                case MapCartography.EditAction.FillAll:
                case MapCartography.EditAction.ClearAll:
                    WatchedFile fileXeen = new WatchedFile(MM45MemoryHacker.MM4CurrentDataFile, "Please select your XEEN.CUR file to edit the cartography data", false, true);
                    if (fileXeen.Length != MM4RosterFile.ExpectedLength)
                    {
                        MessageBox.Show(String.Format("The file \"{0}\" (length {1}) is not the expected size ({2} bytes for XEEN.CUR)",
                            fileXeen.FileName, fileXeen.Length, MM4RosterFile.ExpectedLength),
                            "Unable to process file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    WatchedFile fileDark = new WatchedFile(MM45MemoryHacker.MM5CurrentDataFile, "Please select your DARK.CUR file to edit the cartography data", false, true);
                    if (fileDark.Length != MM5RosterFile.ExpectedLength)
                    {
                        MessageBox.Show(String.Format("The file \"{0}\" (length {1}) is not the expected size ({2} bytes for DARK.CUR)",
                            fileDark.FileName, fileDark.Length, MM5RosterFile.ExpectedLength),
                            "Unable to process file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    if (fileXeen.IsValid && fileDark.IsValid)
                    {
                        for(MM4Map map = MM4Map.A1Surface; map < MM4Map.LastMain; map++)
                        {
                            foreach(uint offset in MM4FileOffsets.MapOffsets(map))
                            {
                                if (offset == 0)
                                    continue;
                                fileXeen.WriteBytes(offset + MM4FileOffsets.CartographyOffset, bytes);
                            }
                        }
                        fileXeen.Flush();
                        for(MM5Map map = MM5Map.A1Surface; map < MM5Map.LastMain; map++)
                        {
                            foreach(uint offset in MM5FileOffsets.MapOffsets(map))
                            {
                                if (offset == 0)
                                    continue;
                                fileDark.WriteBytes(offset + MM5FileOffsets.CartographyOffset, bytes);
                            }
                        }
                        fileDark.Flush();
                    }
                    goto case MapCartography.EditAction.ClearSingle;
                case MapCartography.EditAction.FillSingle:
                case MapCartography.EditAction.ClearSingle:
                    WriteOffset(Memory.MapCartography1, bytes);
                    WriteOffset(Memory.MapCartography2, bytes);
                    WriteOffset(Memory.MapCartography3, bytes);
                    WriteOffset(Memory.MapCartography4, bytes);
                    return true;
            }
            return false;
        }

        private UInt32 GetPointer(long offset)
        {
            return ReadUInt32(offset) * 16 + 32;
        }

        private MemoryBytes GetMemoryAtPointer(long offsetPointer, long offsetLength, long correction = 0)
        {
            if (!IsValid)
                return null;

            return ReadOffset(GetPointer(offsetPointer) + correction - m_offsetFoundBlock, ReadUInt16(offsetLength));
        }

        public override MemoryBytes GetScriptBytes()
        {
            if (m_offsetFoundBlock == Memory.MainSearchNonSVN1)
                return ReadOffset(Memory.ScriptsNonSVN1 - m_offsetFoundBlock, ReadUInt16(Memory.ScriptsLength));

            return ReadOffset(Memory.ScriptsSVN - m_offsetFoundBlock, ReadUInt16(Memory.ScriptsLength));
        }

        public List<ScriptString> MM45MazeStrings(byte[] bytes)
        {
            List<ScriptString> list = new List<ScriptString>();

            MM45String mm45String = null;

            int iNext = 0;
            do
            {
                mm45String = new MM45String();
                mm45String.SetFromBytes(bytes, ref iNext);
                if (mm45String.Valid)
                    list.Add(mm45String);
            } while (mm45String.Valid);

            return list;
        }

        public override string GetMapStrings(bool bRaw = false)
        {
            if (!IsValid)
                return null;

            List<ScriptString> mmStrings = GetScriptStrings();

            StringBuilder sb = new StringBuilder();
            foreach (MMString mm45String in mmStrings)
                sb.AppendLine(mm45String.ToString());

            return sb.ToString();
        }

        public override bool HasScripts { get { return true; } }

        public override List<int> GetMonsterIndices()
        {
            int iNumMonsters = GetMonsterCount();

            List<int> list = new List<int>(iNumMonsters);

            for (int i = 0; i < iNumMonsters; i++)
                list.Add(ReadByte(Memory.MonsterPositions + (i * 20) + MM45Memory.MonsterCurrentIndex));

            return list;
        }

        public override List<ScriptString> GetScriptStrings()
        {
            return MM45MazeStrings(GetMemoryAtPointer(Memory.StringsPointer, Memory.StringsLength));
        }

        public override List<ScriptString> GetScriptStrings(MemoryBytes mb)
        {
            // MM4/5 stores the strings in a completely different location than the scripts, so they must be re-read each time
            return GetScriptStrings();
        }

        public override GameScripts GetScripts(MemoryBytes bytes)
        {
            return new MM45Scripts(GetSpecialSquares(bytes));
        }

        public Dictionary<Point, List<MM345SpecialSquare>> GetSpecialSquares()
        {
            return GetSpecialSquares(null);
        }

        public byte[][] GetMapSquareFlags()
        {
            MemoryBytes b1 = ReadOffset(Memory.MapAttributes1, 256);
            MemoryBytes b2 = ReadOffset(Memory.MapAttributes2, 256);
            MemoryBytes b3 = ReadOffset(Memory.MapAttributes3, 256);
            MemoryBytes b4 = ReadOffset(Memory.MapAttributes4, 256);

            if (b1 == null || b2 == null || b3 == null || b4 == null)
                return null;

            return new byte[][] { b1.Bytes, b2.Bytes, b3.Bytes, b4.Bytes };
        }

        public Dictionary<Point, List<MM345SpecialSquare>> GetSpecialSquares(byte[] bytesActions)
        {
            if (!IsValid)
                return null;

            byte[] bytesEmpty = new byte[10] { 0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0x80 };

            Dictionary<Point, List<MM345SpecialSquare>> squares = new Dictionary<Point, List<MM345SpecialSquare>>();
            byte[][] bytesAttrib = GetMapSquareFlags();
            if (bytesAttrib == null)
                return squares;

            MemoryBytes bytes = ReadOffset(Memory.SpecialSquares, 17120);
            if (bytes == null)
                return squares;

            for (int i = 0; i < bytes.Length-9; i += 10)
            {
                if (Global.CompareBytes(bytes, bytesEmpty, i, 0, bytesEmpty.Length))
                    continue;
                MM45SpecialSquare ss = new MM45SpecialSquare(bytes, i, bytesActions);
                ss.AutoExecute = Global.IsMapByteFlagSet(bytesAttrib, ss.Location, 0x10);
                if (squares.ContainsKey(ss.Location))
                    squares[ss.Location].Add(ss);
                else
                {
                    List<MM345SpecialSquare> list = new List<MM345SpecialSquare>(1);
                    list.Add(ss);
                    squares.Add(ss.Location, list);
                }

            }

            return squares;
        }

        public override bool SetScriptLine(ScriptLine line)
        {
            if (!IsValid)
                return false;

            if (m_offsetFoundBlock == Memory.MainSearchNonSVN1)
                return WriteOffset(Memory.ScriptsNonSVN1 - m_offsetFoundBlock + line.Bytes.Offset + 1, line.Bytes);
            return WriteOffset(Memory.ScriptsSVN - m_offsetFoundBlock + line.Bytes.Offset + 1, line.Bytes);
        }

        public override bool SetPartyGold(UInt32 gold)
        {
            if (!IsValid)
                return false;

            byte[] bytes = BitConverter.GetBytes(gold);
            return WriteOffset(Memory.PartyGold, bytes);
        }

        public override bool SetPartyFood(UInt32 food)
        {
            if (!IsValid)
                return false;

            byte[] bytes = BitConverter.GetBytes((UInt16) food);
            return WriteOffset(Memory.PartyFood, bytes);
        }

        public override bool SetPartyGems(UInt32 gems)
        {
            if (!IsValid)
                return false;

            byte[] bytes = BitConverter.GetBytes(gems);
            return WriteOffset(Memory.PartyGems, bytes);
        }

        public override bool SetBankGold(UInt32 gold)
        {
            if (!IsValid)
                return false;

            byte[] bytes = BitConverter.GetBytes(gold);
            return WriteOffset(Memory.BankGold, bytes);
        }

        public override bool SetBankGems(UInt32 gems)
        {
            if (!IsValid)
                return false;

            byte[] bytes = BitConverter.GetBytes(gems);
            return WriteOffset(Memory.BankGems, bytes);
        }

        public int GetMapFlagsAddress()
        {
            return Memory.MapFlags1;
        }

        public override bool SetYear(UInt16 year)
        {
            if (!IsValid)
                return false;

            byte[] bytes = BitConverter.GetBytes(year);
            return WriteOffset(Memory.Year, bytes);
        }

        public override bool SetDay(Byte day)
        {
            if (!IsValid)
                return false;

            byte[] bytes = new byte[] { day };
            return WriteOffset(Memory.Day, bytes);
        }

        public override bool SetTime(UInt16 minutes)
        {
            if (!IsValid)
                return false;

            byte[] bytes = BitConverter.GetBytes(minutes);
            return WriteOffset(Memory.Minutes, bytes);
        }

        public override MemoryBytes GetPartyStaticBits()
        {
            if (!IsValid)
                return null;

            return ReadOffset(Memory.PartyStaticBits, 32);
        }

        public override bool SetPartyStaticBits(byte[] bytes)
        {
            if (!IsValid)
                return false;

            return WriteOffset(Memory.PartyStaticBits, bytes);
        }

        public override bool SetTime(UInt16 year, byte day, UInt16 minutes)
        {
            if (!IsValid)
                return false;

            byte[] bytesMinutes = BitConverter.GetBytes(minutes);
            byte[] bytesDay = new byte[] { day };
            byte[] bytesYear = BitConverter.GetBytes(year);
            WriteOffset(Memory.Minutes, bytesMinutes);
            WriteOffset(Memory.Year, bytesYear);
            return WriteOffset(Memory.Day, bytesDay);
        }

        private byte[] GetMapInfoBytes()
        {
            byte[] bytes;
            GetMapInfoBytes(out bytes);
            return bytes;
        }

        private bool GetMapInfoBytes(out byte[] bytesMapInfo)
        {
            if (!IsValid)
            {
                bytesMapInfo = null;
                return false;
            }

            bytesMapInfo = new byte[MM45MapBytes.Size];

            long pRead;
            ReadOffset(Memory.MapFlags1, bytesMapInfo, 124, out pRead);
            bytesMapInfo[124] = GetMapSide();
            bytesMapInfo[125] = (byte)GetCurrentMapQuad();
            return true;
        }

        private bool GetGameInfoBytes(out byte[] bytesMapInfo, out byte[] bytesPartyInfo)
        {
            if (!IsValid)
            {
                bytesMapInfo = null;
                bytesPartyInfo = null;
                return false;
            }

            GetMapInfoBytes(out bytesMapInfo);
            GetPartyBytes(out bytesPartyInfo);

            return true;
        }

        public bool GetPartyBytes(out byte[] bytesPartyInfo)
        {
            bytesPartyInfo = new byte[MM45PartyBytes.Size];
            return ReadOffset(Memory.PartyBytes, bytesPartyInfo);
        }

        public override GameInfo GetGameInfo()
        {
            if (!IsValid)
                return null;

            byte[] bytesMapInfo, bytesPartyInfo;

            if (!GetGameInfoBytes(out bytesMapInfo, out bytesPartyInfo))
                return null;

            MM45GameInfo info = new MM45GameInfo(this, bytesMapInfo, bytesPartyInfo);

            return info;
        }

        public override GameInfo GetGameInfo(GameInfo infoOld)
        {
            if (!(infoOld is MM45GameInfo))
                return GetGameInfo();

            byte[] bytesMapInfo, bytesPartyInfo;

            if (!GetGameInfoBytes(out bytesMapInfo, out bytesPartyInfo))
                return GetGameInfo();

            MM45GameInfo mm45Info = (MM45GameInfo)infoOld;

            if (Global.Compare(mm45Info.RawMap, bytesMapInfo) && Global.Compare(mm45Info.RawParty, bytesPartyInfo))
                return infoOld; // All the bytes are the same; don't bother creating a new object

            return new MM45GameInfo(this, bytesMapInfo, bytesPartyInfo);
        }

        public override List<MapTitleInfo> GetMapTitles()
        {
            List<MapTitleInfo> maps = new List<MapTitleInfo>((int)MM4Map.LastMain + (int)MM5Map.LastMain);
            for (int i = (int)MM4Map.A1Surface; i < (int)MM4Map.LastMain; i++)
            {
                MapTitleInfo pair = GetMapTitle(i);
                if (pair.Map != -1)
                    maps.Add(pair);
            }
            for (int i = (int)MM5Map.A1Surface; i < (int)MM5Map.LastMain; i++)
            {
                MapTitleInfo pair = GetMapTitle(256 + i);
                if (pair.Map != -1)
                    maps.Add(pair);
            }

            return maps;
        }

        public Dictionary<Point, List<MM345VisibleObject>> GetVisibleObjects()
        {
            if (!IsValid)
                return null;

            byte[] bytes = new byte[8 * 166];
            byte[] bytesNull = new byte[8] { 0, 0, 0, 0, 0, 0, 0, 0 };

            Dictionary<Point, List<MM345VisibleObject>> objects = new Dictionary<Point, List<MM345VisibleObject>>();

            if (!ReadOffset(Memory.VisibleObjects, bytes))
                return objects;

            for (int i = 0; i < 166 * 8; i += 8)
            {
                if (Global.CompareBytes(bytes, bytesNull, i, 0, 8))
                    continue;
                MM45VisibleObject vo = new MM45VisibleObject(bytes, i);
                if (objects.ContainsKey(vo.Location))
                    objects[vo.Location].Add(vo);
                else
                {
                    objects.Add(vo.Location, new List<MM345VisibleObject>(1));
                    objects[vo.Location].Add(vo);
                }
            }

            return objects;
        }

        public static string FacingString(int i, bool bAbbrev = false)
        {
            switch (i)
            {
                case 0: return "North";
                case 1: return "East";
                case 2: return "South";
                case 3: return "West";
                case 4: return "All";
                default: return bAbbrev ? "?" : "Unknown";
            }
        }

        public override bool SetBeacon(Point ptLocation, int iMap)
        {
            if (!IsValid)
                return false;

            // Set the first sorcerer/archer/ranger/druid beacon

            MM45PartyInfo party = ReadMM45PartyInfo();
            for (int i = 0; i < party.NumChars; i++)
            {
                MM45Character mmChar = MM45Character.Create(party.Bytes, i * MM45Character.SizeInBytes, GetGameInfo());
                if (mmChar == null)
                    continue;

                switch (mmChar.Class)
                {
                    case MM345Class.Sorcerer:
                    case MM345Class.Archer:
                    case MM345Class.Druid:
                    case MM345Class.Ranger:
                        break;
                    default:
                        continue;
                }

                MMBeacon beacon = new MMBeacon(iMap, ptLocation);

                byte[] bytes = beacon.GetBytes();
                WriteByte(Memory.PartyInfo + i * MM45Character.SizeInBytes + MM45.Offsets.BeaconSide, (byte)beacon.Side);
                return WriteOffset(Memory.PartyInfo + i * MM45Character.SizeInBytes + MM45.Offsets.Beacon, bytes);
            }

            return false;
        }

        public override bool SetReadySpell(int iChar, int iSpell)
        {
            MM45PartyInfo party = ReadMM45PartyInfo();
            if (party == null)
                return false;

            MM45Character mmChar = MM45Character.Create(party.Bytes, iChar * party.CharacterSize, GetGameInfo());
            if (mmChar == null)
                return false;

            if (iSpell > (int) MM45InternalSpellIndex.Last)
                iSpell = (int)MM45InternalSpellIndex.None;
            else if (!Global.Cheats)
            {
                // Make sure this character can cast this spell
                if (!mmChar.Spells.IsKnown(iSpell, mmChar.BasicClass))
                    return false;
            }

            if (mmChar == null || mmChar.Spells == null)
                return false;

            byte[] bytes = new byte[1] { (byte) ((MM45KnownSpells)mmChar.Spells).RawByteIndex((MM45InternalSpellIndex) iSpell, mmChar.CasterType) };

            return WriteOffset(Memory.PartyInfo + iChar * MM45Character.SizeInBytes + MM45.Offsets.ReadySpell, bytes);
        }

        public override bool SetReadySpells(int iSpell, SpellType type)
        {
            if (iSpell > (int)MM45InternalSpellIndex.Last)
                iSpell = (int)MM45InternalSpellIndex.None;

            // Make sure this character can cast this spell
            MM45PartyInfo party = ReadMM45PartyInfo();

            for (int iChar = 0; iChar < party.NumChars; iChar++)
            {
                MM45Character mmChar = MM45Character.Create(party.Bytes, iChar * party.CharacterSize, GetGameInfo());
                if (!Global.Cheats)
                {
                    if (!mmChar.Spells.IsKnown(iSpell, mmChar.BasicClass))
                        continue;
                }

                // This will convert the spell to whichever index is appropriate for the character's class
                int iTypeIndex = ((MM45KnownSpells)mmChar.Spells).RawByteIndex((MM45InternalSpellIndex)iSpell, mmChar.CasterType);

                if (iTypeIndex == -1)
                    continue;

                WriteByte(Memory.PartyInfo + iChar * MM45Character.SizeInBytes + MM45.Offsets.ReadySpell, (byte)iTypeIndex);
            }

            return true;
        }

        public override bool HasBeacon { get { return true; } }

        public override CharCreationInfo GetCharCreationInfo()
        {
            if (!IsValid)
                return null;

            MM345CharCreationInfo info = new MM345CharCreationInfo();
            info.AttributesModified = new byte[7] { 0, 0, 0, 0, 0, 0, 0 };
            info.AttributesOriginal = new byte[7] { 0, 0, 0, 0, 0, 0, 0 };
            info.State = ReadMM45GameState();

            if (info.State.CharCreation)
            {
                ReadOffset(Memory.CreationStats, info.AttributesModified);
                Buffer.BlockCopy(info.AttributesModified, 0, info.AttributesOriginal, 0, 7);
            }
            return info;
        }

        public override bool SetCharCreationInfo(CharCreationInfo info)
        {
            if (!IsValid)
                return false;

            if (info is MM345CharCreationInfo)
            {
                if (((MM345CharCreationInfo)info).SwapMightIntellect)
                {
                    byte b = info.AttributesOriginal[0];
                    info.AttributesOriginal[0] = info.AttributesOriginal[1];
                    info.AttributesOriginal[1] = b;
                }

                WriteOffset(Memory.CreationStats, info.AttributesOriginal);
                return true;
            }

            return false;
        }

        public override void RefreshRollScreen()
        {
            // Exchange Might with Intellect.  This takes a while, so swap the values in memory first to save a swap
            byte[] bytes = new byte[2];
            ReadOffset(Memory.CreationStats, bytes);
            byte byte1 = bytes[1];
            bytes[1] = bytes[0];
            bytes[0] = byte1;
            WriteOffset(Memory.CreationStats, bytes);
            SendKeysToDOSBox(new Keys[] { Keys.M, Keys.I });
        }

        public override string GetDebugMemoryInfo()
        {
            if (!IsValid)
                return "<no info available; game program may not be running>";

            StringBuilder sb = new StringBuilder();

            MM45GameState state = GetGameState() as MM45GameState;

            byte[] bytesState1 = new byte[2];
            ReadOffset(Memory.GameState1, bytesState1);

            sb.AppendFormat("State1: 0x{0:X4}", BitConverter.ToUInt16(bytesState1, 0));
            sb.AppendLine();

            //MMScripts scripts = GetScripts();

            //MemoryBytes bytesQuests = ReadOffset(Memory.QuestStrings, 0x2b5e);
            //List<MMString> listQuests = MM45MazeStrings(bytesQuests);
            //foreach (MMString str in listQuests)
            //    sb.AppendLine(str.Basic);

            return sb.ToString();
        }

        public override SpellInfo GetSpellInfo()
        {
            if (!IsValid)
                return null;

            MM45SpellInfo info = new MM45SpellInfo();
            byte[] temp = new byte[2];
            IntPtr pRead = IntPtr.Zero;
            info.Game = ReadMM45GameState();

            if (!info.Game.Casting)
                return info;

            info.Party = ReadMM45PartyInfo();

            info.Game.ActingCharAddress = info.Party.ActingChar;
            if (info.Game.InCombat)
                info.Game.ActingCaster = info.Party.ActingCombatChar;
            else
                info.Game.ActingCaster = info.Party.ActingCaster;
            return info;
        }

        public override bool CreateSuperCharacter(int iAddress)
        {
            if (!IsValid)
                return false;

            int offset = iAddress * MM45Character.SizeInBytes;
            CharacterOffsets offsets = MM45.Offsets;

            PartyInfo info = GetPartyInfo();
            if (offset + MM45Character.SizeInBytes > info.Bytes.Length + 1)
                return false;

            // Single-byte 255 values:
            foreach (int lOffset in new int[] { 
                offsets.Might, offsets.MightMod,
                offsets.Intellect, offsets.IntellectMod,
                offsets.Personality, offsets.PersonalityMod, 
                offsets.Endurance, offsets.EnduranceMod,
                offsets.Speed, offsets.SpeedMod,
                offsets.Accuracy, offsets.AccuracyMod, 
                offsets.Luck, offsets.LuckMod, 
                offsets.FireResist, offsets.FireResistMod, 
                offsets.ColdResist, offsets.ColdResistMod, 
                offsets.ElecResist, offsets.ElecResistMod,
                offsets.EnergyResist, offsets.EnergyResistMod,
                offsets.PoisonResist, offsets.PoisonResistMod,
                offsets.MagicResist, offsets.MagicResistMod,
                offsets.ArmorClassMod, offsets.Level, offsets.LevelMod} )
                info.Bytes[offset + lOffset] = 255;

            // A Spell Points value of 0xffff shows in the UI as 65535, but causes "not enough spell points to cast", so limit to 32767
            Buffer.BlockCopy(Global.MaxInt16, 0, info.Bytes, offset + offsets.CurrentSP, Global.MaxInt16.Length);
            Buffer.BlockCopy(Global.MaxInt16, 0, info.Bytes, offset + offsets.CurrentHP, Global.MaxInt16.Length);
            Buffer.BlockCopy(Global.MaxUInt32, 0, info.Bytes, offset + offsets.Experience, Global.MaxUInt32.Length);

            MM345Class mmClass = (MM345Class)info.Bytes[offset + offsets.Class];

            switch (mmClass)
            {
                case MM345Class.Knight:
                case MM345Class.Barbarian:
                case MM345Class.Robber:
                case MM345Class.Ninja:
                    Global.SetBytes(info.Bytes, offset + offsets.Spells, offsets.SpellsLength, 0);
                    break;
                default:
                    Global.SetBytes(info.Bytes, offset + offsets.Spells, offsets.SpellsLength, 1);
                    break;
            }

            Global.SetBytes(info.Bytes, offset + offsets.Skills, offsets.SkillsLength, 1);
            Global.SetBytes(info.Bytes, offset + offsets.Condition, offsets.ConditionLength, 0);
            info.Bytes[offset + offsets.AgeModifier] = 0;

            WriteOffset(Memory.PartyInfo, info.Bytes);

            byte obsidian = (byte) MM45ItemPrefixIndex.Obsidian;
            byte divine = (byte)MM45ItemPrefixIndex.Divine;
            byte masher = (byte)MM45WeaponSuffixIndex.MonsterMasher;
            byte jewel = (byte) MM45MiscItemIndex.Jewel;

            List<Item> items = new List<Item>(23);

            switch (mmClass)
            {
                case MM345Class.Knight:
                case MM345Class.Paladin:
                case MM345Class.Archer:
                case MM345Class.Ranger:
                    items.Add(MM45Item.Create(new byte[] { obsidian, (byte) MM45WeaponIndex.Flamberge, masher, 0 }, ItemType.Weapon, 0));
                    break;
                case MM345Class.Robber:
                case MM345Class.Barbarian:
                    items.Add(MM45Item.Create(new byte[] { obsidian, (byte) MM45WeaponIndex.GreatAxe, masher, 0 }, ItemType.Weapon, 0));
                    break;
                case MM345Class.Cleric:
                case MM345Class.Druid:
                    items.Add(MM45Item.Create(new byte[] { obsidian, (byte) MM45WeaponIndex.Hammer, masher, 0 }, ItemType.Weapon, 0));
                    break;
                case MM345Class.Sorcerer:
                    items.Add(MM45Item.Create(new byte[] { obsidian, (byte) MM45WeaponIndex.Staff, masher, 0 }, ItemType.Weapon, 0));
                    break;
                case MM345Class.Ninja:
                    items.Add(MM45Item.Create(new byte[] { obsidian, (byte) MM45WeaponIndex.Halberd, masher, 0 }, ItemType.Weapon, 0));
                    break;
                default:
                    break;
            }

            switch (mmClass)
            {
                case MM345Class.Knight:
                case MM345Class.Paladin:
                case MM345Class.Archer:
                case MM345Class.Robber:
                case MM345Class.Ranger:
                case MM345Class.Barbarian:
                case MM345Class.Ninja:
                    items.Add(MM45Item.Create(new byte[] { obsidian, (byte)MM45WeaponIndex.LongBow, masher, 0 }, ItemType.Weapon, 0));
                    break;
                default:
                    break;
            }

            switch (mmClass)
            {
                case MM345Class.Knight:
                case MM345Class.Paladin:
                    items.Add(MM45Item.Create(new byte[] { obsidian, (byte)MM45ArmorIndex.PlateArmor, 0, 0 }, ItemType.Armor, 0));
                    break;
                case MM345Class.Archer:
                case MM345Class.Robber:
                    items.Add(MM45Item.Create(new byte[] { obsidian, (byte)MM45ArmorIndex.ChainMail, 0, 0 }, ItemType.Armor, 0));
                    break;
                case MM345Class.Cleric:
                case MM345Class.Ranger:
                    items.Add(MM45Item.Create(new byte[] { obsidian, (byte)MM45ArmorIndex.SplintMail, 0, 0 }, ItemType.Armor, 0));
                    break;
                case MM345Class.Sorcerer:
                case MM345Class.Druid:
                    items.Add(MM45Item.Create(new byte[] { obsidian, (byte)MM45ArmorIndex.Robes, 0, 0 }, ItemType.Armor, 0));
                    break;
                case MM345Class.Barbarian:
                    items.Add(MM45Item.Create(new byte[] { obsidian, (byte)MM45ArmorIndex.ScaleArmor, 0, 0 }, ItemType.Armor, 0));
                    break;
                case MM345Class.Ninja:
                    items.Add(MM45Item.Create(new byte[] { obsidian, (byte)MM45ArmorIndex.RingMail, 0, 0 }, ItemType.Armor, 0));
                    break;
                default:
                    break;
            }


            items.Add(MM45Item.Create(new byte[] { obsidian, (byte)MM45ArmorIndex.Cloak, 0, 0 }, ItemType.Armor, 0));
            items.Add(MM45Item.Create(new byte[] { obsidian, (byte)MM45ArmorIndex.Boots, 0, 0 }, ItemType.Armor, 0));
            items.Add(MM45Item.Create(new byte[] { obsidian, (byte)MM45ArmorIndex.Helm, 0, 0 }, ItemType.Armor, 0));
            items.Add(MM45Item.Create(new byte[] { obsidian, (byte)MM45ArmorIndex.Gauntlets, 0, 0 }, ItemType.Armor, 0));
            items.Add(MM45Item.Create(new byte[] { divine, (byte)MM45AccessoryIndex.Belt, 0, 0 }, ItemType.Accessory, 0));
            items.Add(MM45Item.Create(new byte[] { divine, (byte)MM45AccessoryIndex.Amulet, 0, 0 }, ItemType.Accessory, 0));
            items.Add(MM45Item.Create(new byte[] { divine, (byte)MM45AccessoryIndex.Broach, 0, 0 }, ItemType.Accessory, 0));
            items.Add(MM45Item.Create(new byte[] { divine, (byte)MM45AccessoryIndex.Cameo, 0, 0 }, ItemType.Accessory, 0));
            items.Add(MM45Item.Create(new byte[] { divine, (byte)MM45AccessoryIndex.Ring, 0, 0 }, ItemType.Accessory, 0));
            items.Add(MM45Item.Create(new byte[] { divine, (byte)MM45AccessoryIndex.Ring, 0, 0 }, ItemType.Accessory, 0));
            items.Add(MM45Item.Create(new byte[] { jewel, (byte)MM45ItemSuffixIndex.TheGODS, 63, 0 }, ItemType.Miscellaneous, 0));
            items.Add(MM45Item.Create(new byte[] { jewel, (byte)MM45ItemSuffixIndex.Teleportation, 63, 0 }, ItemType.Miscellaneous, 0));
            items.Add(MM45Item.Create(new byte[] { jewel, (byte)MM45ItemSuffixIndex.TownPortals, 63, 0 }, ItemType.Miscellaneous, 0));
            items.Add(MM45Item.Create(new byte[] { jewel, (byte)MM45ItemSuffixIndex.Resurrection, 63, 0 }, ItemType.Miscellaneous, 0));
            items.Add(MM45Item.Create(new byte[] { jewel, (byte)MM45ItemSuffixIndex.Etherealization, 63, 0 }, ItemType.Miscellaneous, 0));
            items.Add(MM45Item.Create(new byte[] { jewel, (byte)MM45ItemSuffixIndex.MassDistortion, 63, 0 }, ItemType.Miscellaneous, 0));
            items.Add(MM45Item.Create(new byte[] { jewel, (byte)MM45ItemSuffixIndex.DancingSwords, 63, 0 }, ItemType.Miscellaneous, 0));
            items.Add(MM45Item.Create(new byte[] { jewel, (byte)MM45ItemSuffixIndex.Implosions, 63, 0 }, ItemType.Miscellaneous, 0));
            items.Add(MM45Item.Create(new byte[] { jewel, (byte)MM45ItemSuffixIndex.HolyWords, 63, 0 }, ItemType.Miscellaneous, 0));

            AddItemsToBackpack(iAddress, items);

            return true;
        }

        public void AddItemsToBackpack(int iAddress, List<Item> items)
        {
            if (items == null)
                return;

            List<Item> equipped = GetEquippedItems(iAddress);

            MM45BackpackBytes bpBytes = MM45BackpackBytes.Create(equipped);
            bpBytes.Add(items);

            WriteOffset(Memory.PartyInfo + (iAddress * MM45Character.SizeInBytes) + MM45.Offsets.Inventory, bpBytes.GetBytes());
        }

        public override Size GetCurrentMapDimensions()
        {
            byte side = GetMapSide();
            int mapNorth = ReadUInt16(Memory.Map1North) | (side << 8);
            int mapEast = ReadUInt16(Memory.Map1East) | (side << 8);
            MM45MapFlags flags = (MM45MapFlags) ReadUInt32(Memory.Map1Flags);

            if (flags.HasFlag(MM45MapFlags.Outdoor))
                return new Size(16, 16);    // Outdoor maps have a roving quadrant, so 16x16 makes the most sense here
            if ((mapNorth % 256) != 0 && (mapEast % 256) != 0)
                return new Size(32, 32);
            if ((mapNorth % 256) != 0 && (mapEast % 256) == 0)
                return new Size(16, 32);
            if ((mapNorth % 256) == 0 && (mapEast % 256) != 0)
                return new Size(32, 16);
            return new Size(16, 16);
        }

        public static DirectionFlags FacingDirection(int i)
        {
            switch (i)
            {
                case 0: return DirectionFlags.North;
                case 1: return DirectionFlags.East;
                case 2: return DirectionFlags.South;
                case 3: return DirectionFlags.West;
                case 4: return DirectionFlags.All;
                default: return DirectionFlags.None;
            }
        }

        public override bool KillAllMonsters()
        {
            if (!IsValid)
                return false;

            int iNumMonsters = GetMonsterCount();

            byte[] bytes = Global.NullBytes(iNumMonsters * 20);
            return WriteOffset(Memory.MonsterPositions, bytes);
        }

        public override string[] CurrentDataFiles
        {
            get
            {
                return new string[] { MM4CurrentDataFile, MM5CurrentDataFile };
            }
            set
            {
                if (value == null || value.Length < 2)
                {
                    SetMM4CurrentDataFile(String.Empty);
                    SetMM5CurrentDataFile(String.Empty);
                    return;
                }
                SetMM4CurrentDataFile(value[0]);
                SetMM5CurrentDataFile(value[1]);
            }
        }

        public void SetMM5CurrentDataFile(string strFile)
        {
            if (String.IsNullOrWhiteSpace(strFile))
            {
                Games.SetRosterPath(GameNames.MightAndMagic5, String.Empty);
                Games.SetRosterFile(GameNames.MightAndMagic5, "DARK.CUR");
                return;
            }
            Games.SetRosterPath(GameNames.MightAndMagic5, Path.GetDirectoryName(strFile));
            Games.SetRosterFile(GameNames.MightAndMagic5, Path.GetFileName(strFile));
        }

        public void SetMM4CurrentDataFile(string strFile)
        {
            if (String.IsNullOrWhiteSpace(strFile))
            {
                Games.SetRosterPath(GameNames.MightAndMagic4, String.Empty);
                Games.SetRosterFile(GameNames.MightAndMagic4, "XEEN.CUR");
                return;
            }
            Games.SetRosterPath(GameNames.MightAndMagic4, Path.GetDirectoryName(strFile));
            Games.SetRosterFile(GameNames.MightAndMagic4, Path.GetFileName(strFile));
        }

        public override bool ResetMonsters()
        {
            if (!IsValid)
                return false;

            MM45Side side = GetSide();
            byte[] defaults = side == MM45Side.Clouds ? Global.Uncompress(Properties.Resources.MM4MonsterDefaults) : Global.Uncompress(Properties.Resources.MM5MonsterDefaults);

            int iMap = ReadOffset(Memory.CurrentMapIndex, 1)[0];

            byte[] bytesMonsters = null;

            List<MMMonster> monsters = GetMonsterSide() == MM45Side.Clouds ? MM45.MM4Monsters : MM45.MM5Monsters;

            int iMapOffset = 0;
            int iMonsterCount = 0;

            while (iMapOffset < defaults.Length - 2)
            {
                if (defaults[iMapOffset] != iMap)
                {
                    iMapOffset += (2 + defaults[iMapOffset + 1] * 4);
                    continue;
                }

                iMonsterCount = defaults[iMapOffset + 1];
                bytesMonsters = new byte[iMonsterCount * 4];
                Buffer.BlockCopy(defaults, iMapOffset + 2, bytesMonsters, 0, bytesMonsters.Length);
                break;
            }

            if (bytesMonsters == null)
                return false;

            byte[] bytesMemory = Global.NullBytes(iMonsterCount * 20);

            for (int i = 0; i < iMonsterCount; i++)
            {
                bytesMemory[i * 20 + MM45Memory.MonsterCurrentX] = bytesMonsters[i * 4];
                bytesMemory[i * 20 + MM45Memory.MonsterCurrentY] = bytesMonsters[i * 4 + 1];
                bytesMemory[i * 20 + MM45Memory.MonsterCondition] = 0;
                bytesMemory[i * 20 + MM45Memory.MonsterActivated] = 0;
                int iIndex = bytesMonsters[i * 4 + 2];
                if (iIndex < MM45.MM4Monsters.Count)
                {
                    byte[] bytesHP = BitConverter.GetBytes((UInt16)monsters[iIndex].HP);
                    Buffer.BlockCopy(bytesHP, 0, bytesMemory, i * 20 + MM45Memory.MonsterCurrentHP, bytesHP.Length);
                }
                bytesMemory[i * 20 + MM45Memory.MonsterCurrentIndex] = bytesMonsters[i * 4 + 2];
                bytesMemory[i * 20 + MM45Memory.MonsterLocalIndex] = bytesMonsters[i * 4 + 3];
            }

            WriteOffset(Memory.MonsterPositions, bytesMemory);
            return true;
        }

        public static string MM4CurrentDataFile
        {
            get
            {
                return Global.CombineRoster(GameNames.MightAndMagic4);
            }
        }

        public static string MM5CurrentDataFile
        {
            get
            {
                return Global.CombineRoster(GameNames.MightAndMagic5);
            }
        }

        public override bool ValidateRosterFile(bool bAlwaysReload = true)
        {
            // Reload the roster file by default, in case the player deleted characters or otherwise putzed with the file
            if (m_mm4Roster != null)
            {
                m_mm5Roster = null; // Only one active roster at a time
                if (bAlwaysReload)
                    m_mm4Roster = new MM4RosterFile(Global.CombineRoster(GameNames.MightAndMagic4), true);
            }
            else if (bAlwaysReload || m_mm5Roster == null)
                m_mm5Roster = new MM5RosterFile(Global.CombineRoster(GameNames.MightAndMagic5), true);

            if (CurrentRoster == null || !CurrentRoster.Valid)
                BrowseRosterFile();

            if (CurrentRoster == null)
                return false;

            if (CurrentRoster.Valid)
            {
                if (CurrentRoster == m_mm4Roster)
                {
                    Games.SetRosterFile(GameNames.MightAndMagic4, Path.GetFileName(m_mm4Roster.FileName));
                    Games.SetRosterPath(GameNames.MightAndMagic4, Path.GetDirectoryName(m_mm4Roster.FileName));
                }
                else if (CurrentRoster == m_mm5Roster)
                {
                    Games.SetRosterFile(GameNames.MightAndMagic5, Path.GetFileName(m_mm5Roster.FileName));
                    Games.SetRosterPath(GameNames.MightAndMagic5, Path.GetDirectoryName(m_mm5Roster.FileName));
                    Games.SetRosterPath(GameNames.MightAndMagic45, Path.GetDirectoryName(m_mm5Roster.FileName));
                }
            }

            return CurrentRoster.Valid;
        }

        public override bool BrowseRosterFile(string strTitle = null)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = Games.RosterFile(GameNames.MightAndMagic4);
            ofd.InitialDirectory = Games.RosterPath(GameNames.MightAndMagic4);
            ofd.Filter = "Current Files|*.CUR|Saved Files|*.SAV|All Files|*.*";
            if (String.IsNullOrWhiteSpace(strTitle))
                ofd.Title = "You must select your XEEN.CUR or DARK.CUR file in order to use the Bag of Holding";
            else
                ofd.Title = strTitle;
            while (true)
            {
                if (ofd.ShowDialog() == DialogResult.Cancel)
                    return false;

                if (MM5RosterFile.AcceptableSize(new FileInfo(ofd.FileName).Length))
                {
                    m_mm4Roster = null;
                    m_mm5Roster = new MM5RosterFile(ofd.FileName, false);
                    if (m_mm5Roster.Valid)
                    {
                        Games.SetRosterFile(GameNames.MightAndMagic5, Path.GetFileName(m_mm5Roster.FileName));
                        Games.SetRosterPath(GameNames.MightAndMagic5, Path.GetDirectoryName(m_mm5Roster.FileName));
                        Games.SetRosterPath(GameNames.MightAndMagic45, Path.GetDirectoryName(m_mm5Roster.FileName));
                        break;
                    }
                }
                else
                {
                    m_mm5Roster = null;
                    m_mm4Roster = new MM4RosterFile(ofd.FileName, false);
                    if (m_mm4Roster.Valid)
                    {
                        Games.SetRosterFile(GameNames.MightAndMagic4, Path.GetFileName(m_mm4Roster.FileName));
                        Games.SetRosterPath(GameNames.MightAndMagic4, Path.GetDirectoryName(m_mm4Roster.FileName));
                        break;
                    }
                }
            }
            return true;
        }

        public override RosterFile CurrentRoster
        {
            get
            {
                if (m_mm4Roster == null || !m_mm4Roster.Valid)
                    return m_mm5Roster;
                return m_mm4Roster;
            }
        }

        public override List<Item> GetBackpackFromRoster(int iRosterPosition)
        {
            if (!ValidateRosterFile())
                return null;

            if (iRosterPosition < 0 || iRosterPosition >= CurrentRoster.Chars.Count)
                return null;

            byte[] bytesChar = CurrentRoster.LoadCharBytes(iRosterPosition);
            if (bytesChar == null)
                return null;

            if (!IsValid)
                return null;

            List<Item> backpack = new List<Item>(36);

            byte[] bytes = new byte[9 * 4 * 4];
            Buffer.BlockCopy(bytesChar, MM45.Offsets.Inventory, bytes, 0, bytes.Length);

            MM45BackpackBytes bpBytes = new MM45BackpackBytes(bytes);

            int iMemoryIndex = 0;
            foreach (byte[,] itemType in bpBytes.All)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (itemType[i, 0] != 0 || itemType[i, 1] != 0)
                    {
                        bool bEquipped = false;
                        if (itemType != bpBytes.Miscellaneous)
                            bEquipped = (itemType[i, 3] != 0);

                        MM45Item item = MM45Item.Create(bytes, bpBytes.GetType(itemType), iMemoryIndex * 4);
                        item.MemoryIndex = iMemoryIndex;
                        backpack.Add(item);
                    }

                    iMemoryIndex++;
                }
            }

            return backpack;
        }

        protected override int FindNextInventoryChar(int iStart, InventoryCharAction action, bool bForceRosterLoad = true)
        {
            if (!ValidateRosterFile(bForceRosterLoad))
                return -1;

            byte[] bytes = new byte[MM45Character.SizeInBytes];
            while (iStart >= 0)
            {
                MM45Character mm4Char = null;
                if (iStart < CurrentRoster.Chars.Count)
                    mm4Char = MM45Character.Create(CurrentRoster.Chars[iStart].Bytes, 0, null, true);

                switch (action)
                {
                    case InventoryCharAction.FindExisting:
                        if (mm4Char != null && mm4Char.Name == "Inventory")
                            return iStart;
                        break;
                    case InventoryCharAction.FindOrCreate:
                        if (mm4Char != null && mm4Char.Name == "Inventory")
                            return iStart;
                        if (mm4Char == null || String.IsNullOrWhiteSpace(mm4Char.Name))
                        {
                            // No character in the roster at this position; make a new one;
                            CurrentRoster.Chars[iStart].Bytes = Properties.Resources.MM45InventoryChar;
                            CurrentRoster.Chars[iStart].Town = 1;
                            CurrentRoster.SaveCharBytes(iStart);
                            return iStart;
                        }
                        break;
                    case InventoryCharAction.FindPotential:
                        if (mm4Char == null || mm4Char.Name == "" || mm4Char.Name == "Inventory")
                            return iStart;
                        break;
                    default:
                        break;
                }
                iStart--;
            }

            return -1;
        }

        public override SetBackpackResult SetBackpackInRoster(int iRosterPosition, List<Item> items)
        {
            // Completely overwrites a backpack, including equipped items
            // Also removes the "equipped" status of all items

            if (iRosterPosition < 0 || iRosterPosition > 19)
                return SetBackpackResult.InvalidPosition;

            if (!ValidateRosterFile())
                return SetBackpackResult.InvalidFile;

            if (iRosterPosition >= CurrentRoster.Chars.Count)
                return SetBackpackResult.InvalidPosition;

            MM45BackpackBytes bpBytes = MM45BackpackBytes.Create(items);
            if (bpBytes.ItemCount != items.Count)
                return SetBackpackResult.InsufficientSpace;

            byte[] bytesChar = CurrentRoster.LoadCharBytes(iRosterPosition);
            if (bytesChar == null)
                return SetBackpackResult.LoadCharFailure;

            byte[] bytesNull = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            byte[] bytesBackpack = bpBytes.GetBytes();

            Buffer.BlockCopy(bytesBackpack, 0, bytesChar, MM45.Offsets.Inventory, bytesBackpack.Length);

            CurrentRoster.SaveCharBytes(iRosterPosition, 1, bytesChar);

            return SetBackpackResult.Success;
        }

        public override int StoreBagInRoster(ItemBag bag)
        {
            int iCharacter = MaxInventoryChar;    // last character in the roster
            int iItemsStored = 0;

            // Might and Magic 4/5 stores four different types of item in four different arrays,
            // so we have to fill them up individually in order to store in inventory efficiently
            Queue<Item> armor = new Queue<Item>();
            Queue<Item> weapons = new Queue<Item>();
            Queue<Item> accessories = new Queue<Item>();
            Queue<Item> misc = new Queue<Item>();

            foreach (MM45Item item in bag.Items)
            {
                switch (item.Base.Type)
                {
                    case ItemType.Armor:
                        armor.Enqueue(item);
                        break;
                    case ItemType.Weapon:
                        weapons.Enqueue(item);
                        break;
                    case ItemType.Accessory:
                        accessories.Enqueue(item);
                        break;
                    case ItemType.Miscellaneous:
                        misc.Enqueue(item);
                        break;
                    default:
                        break;
                }
            }

            List<Item> backpack = new List<Item>();

            do
            {
                // Create an inventory character if one doesn't exist
                iCharacter = FindNextInventoryChar(iCharacter - 1, InventoryCharAction.FindOrCreate);

                for (int i = 0; i < 9; i++)
                {
                    if (armor.Count > 0)
                        backpack.Add(armor.Dequeue());
                    if (weapons.Count > 0)
                        backpack.Add(weapons.Dequeue());
                    if (accessories.Count > 0)
                        backpack.Add(accessories.Dequeue());
                    if (misc.Count > 0)
                        backpack.Add(misc.Dequeue());
                }

                if (iCharacter == -1)
                    return iItemsStored;    // No more available inventory characters

                if (SetBackpackInRoster(iCharacter, backpack) == SetBackpackResult.Success)
                    iItemsStored += backpack.Count;
                else
                    return iItemsStored;    // Error saving one of the backpacks

                backpack.Clear();
            } while (armor.Count > 0 || weapons.Count > 0 || accessories.Count > 0 || misc.Count > 0);

            // Any remaining Inventory characters' backpacks must be cleared
            backpack.Clear();
            iCharacter--;
            while (iCharacter >= 0)
            {
                iCharacter = FindNextInventoryChar(iCharacter, InventoryCharAction.FindExisting);
                SetBackpackInRoster(iCharacter, backpack);
                iCharacter--;
            }

            return iItemsStored;
        }

        public override int MaxInventoryChar { get { return 20; } }

        public override MapCartography GetCartography()
        {
            if (!IsValid)
                return null;

            MapCartography cart = new MM45MapCartography();

            byte[] bytes = new byte[32];

            cart.MapIndex = GetCurrentMapIndex();
            Size sz = GetCurrentMapDimensions();

            if (sz.Height == 16 && sz.Width == 16)
            {
                ReadOffset(Memory.MapCartography1, bytes);
                cart.SetBytes(bytes, new Size(16, 16));
                return cart;
            }

            byte[] bytes2 = new byte[32];
            byte[] bytes3 = new byte[32];
            byte[] bytes4 = new byte[32];
            ReadOffset(Memory.MapCartography1, bytes);
            ReadOffset(Memory.MapCartography2, bytes2);
            ReadOffset(Memory.MapCartography3, bytes3);
            ReadOffset(Memory.MapCartography4, bytes4);
            cart.SetFromQuad(bytes, bytes2, bytes3, bytes4);
            return cart;
        }

        public int GetNumChars()
        {
            if (!IsValid)
                return 0;

            byte[] bytes = new byte[1];
            if (!ReadOffset(Memory.NumChars, bytes))
                return 0;
            if (bytes[0] > 6)
                return 0;
            return bytes[0];
        }

        public override bool HasParty { get { return GetNumChars() > 0; } }

        private void CureCondition(MM45CureAllInfo info, int iAddress, ref byte condition, MM45InternalSpellIndex spell, bool bOneHP, ref bool bNoGems, ref bool bNoSP, ref bool bNotKnown)
        {
            if (condition > 0)
            {
                if (info.CasterSpells.IsKnown((int)spell, info.CasterClass))
                {
                    int iSP = MM45.SpellList.Value.GetSpell(spell).Cost.SpellPoints;
                    if (info.CasterSpellPoints >= iSP)
                    {
                        int iGems = MM45.SpellList.Value.GetSpell(spell).Cost.Gems;
                        if (info.CasterGems >= iGems)
                        {
                            info.CasterSpellPoints -= (ushort)iSP;
                            info.CasterGems -= (uint)iGems;
                            condition = 0;
                            if (bOneHP)
                                info.HitPoints[iAddress] = 1;
                        }
                        else
                            bNoGems = true;
                    }
                    else
                        bNoSP = true;
                }
                else
                    bNotKnown = true;
            }
        }

        public override CureAllResult CureAll(CureAllInfo cureAllInfo)
        {
            bool bSpellNotKnown = false;
            bool bInsufficientSP = false;
            bool bInsufficientGems = false;

            if (!(cureAllInfo is MM45CureAllInfo))
                return CureAllResult.Error;

            if (cureAllInfo.MonstersNearby)
                return CureAllResult.MonstersNearby;

            MM45CureAllInfo info = cureAllInfo as MM45CureAllInfo;

            // Cure Disease        10 SP
            // Cure Paralysis      12 SP
            // Cure Poison         8 SP
            // Awaken              1 SP
            // Divine Intervention 200 SP, 20 Gems, 5 Years
            // First Aid           1 SP  (+6 HP)
            // Cure Wounds         3 SP  (+15 HP)
            // Nature's Cure       6 SP  (+25 HP)
            // Raise Dead          50 SP, 10 Gems
            // Resurrection        125 SP, 20 Gems, 5 Years
            // Revitalize          2 SP
            // Stone to Flesh      35 SP, 5 Gems

            // Okay, let's start curing!
            for (int i = 0; i < info.Conditions.Length; i++)
            {
                // First skip Eradicated (whether to age the target should be more in the player's control)
                if (info.Conditions[i].Eradicated > 0)
                    continue;

                CureCondition(info, i, ref info.Conditions[i].Stone, MM45InternalSpellIndex.StoneToFlesh, true, ref bInsufficientGems, ref bInsufficientSP, ref bSpellNotKnown);
                CureCondition(info, i, ref info.Conditions[i].Paralyzed, MM45InternalSpellIndex.CureParalysis, false, ref bInsufficientGems, ref bInsufficientSP, ref bSpellNotKnown);
                CureCondition(info, i, ref info.Conditions[i].Weak, MM45InternalSpellIndex.Revitalize, false, ref bInsufficientGems, ref bInsufficientSP, ref bSpellNotKnown);
                CureCondition(info, i, ref info.Conditions[i].Poisoned, MM45InternalSpellIndex.CurePoison, false, ref bInsufficientGems, ref bInsufficientSP, ref bSpellNotKnown);
                CureCondition(info, i, ref info.Conditions[i].Diseased, MM45InternalSpellIndex.CureDisease, false, ref bInsufficientGems, ref bInsufficientSP, ref bSpellNotKnown);

            }

            bool bAnySleep = false;
            for (int i = 0; i < info.Conditions.Length; i++)
            {
                if (info.Conditions[i].Asleep > 0)
                {
                    if (info.CasterSpells.IsKnown((int)MM45InternalSpellIndex.Awaken, info.CasterClass))
                    {
                        if (info.CasterSpellPoints >= 1)
                        {
                            bAnySleep = true;
                            info.Conditions[i].Asleep = 0;
                        }
                        else
                            bInsufficientSP = true;
                    }
                    else
                        bSpellNotKnown = true;
                }
            }
            if (bAnySleep && info.CasterSpellPoints > 0)
                info.CasterSpellPoints -= 1;

            if (Properties.Settings.Default.CureAllHPWithConditions)
            {
                for (int i = 0; i < info.HitPoints.Length; i++)
                {
                    if (info.CasterSpells.IsKnown((int)MM45InternalSpellIndex.FirstAid, info.CasterClass))
                    {
                        while(info.HitPoints[i] < info.HitPointsMax[i] - 5)
                        {
                            if (info.CasterSpellPoints >= 1)
                            {
                                info.CasterSpellPoints--;
                                info.HitPoints[i] += 6;
                            }
                            else
                            {
                                bInsufficientSP = true;
                                break;
                            }
                        }
                        if (info.HitPoints[i] > 0)
                            info.Conditions[i].Unconscious = 0;
                    }
                    else
                        bSpellNotKnown = true;
                }
            }

            if (bSpellNotKnown)
                return CureAllResult.SpellNotKnown;
            if (bInsufficientSP)
                return CureAllResult.NoSpellPoints;
            if (bInsufficientGems)
                return CureAllResult.NoGems;
            return CureAllResult.Success;
        }

        public override CureAllInfo GetCureAllInfo(int iCasterAddress, int[] partyAddresses)
        {
            if (!IsValid)
                return null;

            if (iCasterAddress >= partyAddresses.Length)
                return null;

            MM45CureAllInfo info = new MM45CureAllInfo();
            MM45PartyInfo party = ReadMM45PartyInfo();

            byte[] temp = new byte[2];
            IntPtr pRead = IntPtr.Zero;

            info.GameState = ReadMM45GameState();
            info.InCombat = info.GameState.InCombat;

            MM45Character caster = null;

            info.Conditions = new MM45Condition[partyAddresses.Length];
            info.HitPoints = new Int16[partyAddresses.Length];
            info.HitPointsMax = new int[partyAddresses.Length];
            for (int i = 0; i < partyAddresses.Length; i++)
            {
                MM45Character mm45Char = MM45Character.Create(party.Bytes, party.CharacterSize * i, GetGameInfo());
                if (i == partyAddresses[iCasterAddress])
                    caster = mm45Char;
                info.Conditions[i] = mm45Char.Condition;
                info.HitPoints[i] = mm45Char.CurrentHP;
                info.HitPointsMax[i] = mm45Char.MaxHP;
            }

            MM45GameInfo gameInfo = GetGameInfo() as MM45GameInfo;
            info.CasterGems = gameInfo.Party.Gems;
            info.CasterSpellPoints = caster.CurrentSP;
            info.CasterSpells = caster.Spells;
            info.CasterClass = caster.BasicClass;
            info.CasterCondition = caster.Condition;
            info.MonstersNearby = ActiveMonstersNearby;

            return info;
        }

        public override void SetCureAllInfo(CureAllInfo cureAll, int iCasterAddress, int[] partyAddresses)
        {
            if (!IsValid)
                return;

            if (iCasterAddress >= partyAddresses.Length)
                return;

            MM45CureAllInfo info = cureAll as MM45CureAllInfo;

            for (int i = 0; i < info.Conditions.Length; i++)
            {
                WriteOffset(Memory.PartyInfo + i * MM45Character.SizeInBytes + MM45.Offsets.Condition, info.Conditions[i].GetBytes());
                WriteOffset(Memory.PartyInfo + i * MM45Character.SizeInBytes + MM45.Offsets.CurrentHP, BitConverter.GetBytes((Int16)info.HitPoints[i]));
            }

            WriteOffset(Memory.PartyGems, BitConverter.GetBytes(info.CasterGems));
            WriteOffset(Memory.PartyInfo + partyAddresses[iCasterAddress] * MM45Character.SizeInBytes + MM45.Offsets.CurrentSP, BitConverter.GetBytes((UInt16)info.CasterSpellPoints));
        }

        public override bool RefreshConditions()
        {
            MM45GameState state = ReadMM45GameState();
            switch (state.Main)
            {
                case MainState.Adventuring:
                    SendKeysToDOSBox(new Keys[] { Keys.D, Keys.Escape }, true);
                    break;
                case MainState.Inventory:
                    SendKeysToDOSBox(new Keys[] { Keys.Escape }, true);
                    break;
                case MainState.CharacterScreen:
                    SendKeysToDOSBox(new Keys[] { Keys.E, Keys.Escape }, true);
                    break;
                case MainState.QuickRef:
                    SendKeysToDOSBox(new Keys[] { Keys.Escape, Keys.E, Keys.Escape }, true);
                    break;
                default:
                    SendKeysToDOSBox(new Keys[] { Keys.D, Keys.Escape }, true);
                    break;
            }
            return true;
        }

        public override string GetMapEnum(int index)
        {
            if (index > 255)
                return String.Format("MM5Map.{0}", Enum.GetName(typeof(MM5Map), (MM5Map)(index % 256)));
            return String.Format("MM4Map.{0}", Enum.GetName(typeof(MM4Map), (MM4Map)(index)));
        }

        public override Shops GetShopInfo()
        {
            Shops shops = GetShopItems() as Shops;
            if (shops == null)
                return shops;

            MM45GameState state = ReadMM45GameState();
            shops.Screen = state.Subscreen;
            shops.InShop = state.InShop;
            return shops;
        }

        public MM45Shops GetShopItems()
        {
            if (!IsValid)
                return null;

            // Each item has 4 1-byte properties that differ based on the item type
            // Each shop has 4 categories (weapons, armor, accessories, misc)
            // Each category has 9 items
            // There are 4 shops on each side of the world
            byte[] bytesCloud = new byte[4 * 4 * 9 * 4];
            if (!ReadOffset(Memory.CloudsideStore1, bytesCloud))
                return null;

            byte[] bytesDark = new byte[4 * 4 * 9 * 4];
            if (!ReadOffset(Memory.DarksideStore1, bytesDark))
                return null;

            byte[] bytesCurrent = new byte[4 * 4 * 9];
            if (!ReadOffset(Memory.CurrentStoreWeapons, bytesCurrent))
                return null;

            if (m_lastShops != null && m_lastShops.CompareBytes(bytesCloud, bytesDark, bytesCurrent))
                return m_lastShops;

            m_lastShops = new MM45Shops(Memory.CloudsideStore1, bytesCloud, Memory.DarksideStore1, bytesDark, Memory.CurrentStoreWeapons, bytesCurrent);
            return m_lastShops;
        }

        public override bool SetShopItem(ShopItem item)
        {
            if (!(item.Item is MM45Item))
                return false;

            MM45Item mm45Item = item.Item as MM45Item;

            WriteOffset(item.Offset, mm45Item.GetMemoryBytes());

            return true;
        }

        public override List<BaseCharacter> GetCharacters()
        {
            PartyInfo pi = GetPartyInfo();
            if (pi == null)
                return null;

            List<BaseCharacter> chars = new List<BaseCharacter>(pi.NumChars);
            MM45GameInfo gi = GetGameInfo() as MM45GameInfo;
            for (int i = 0; i < pi.NumChars; i++)
                chars.Add(MM45Character.Create(pi.Bytes, MM45Character.SizeInBytes * i, gi));

            return chars;
        }

        public override void SelectGameFiles()
        {
            SelectGameFilesForm form = new SelectGameFilesForm(Game);
            if (form.ShowDialog() == DialogResult.OK)
            {
                m_fileDarkCur = new WatchedFile(form.File2, "Please select your DARK.CUR file", false);
                if (m_fileDarkCur.IsValid)
                    SetMM5CurrentDataFile(m_fileDarkCur.FileName);
                m_fileXeenCur = new WatchedFile(form.File1, "Please select your XEEN.CUR file", false);
                if (m_fileXeenCur.IsValid)
                    SetMM4CurrentDataFile(m_fileXeenCur.FileName);
            }
        }

        public override QuestInfoBase GetQuestInfo(QuestInfoBase lastInfo = null, int iOverrideCharAddress = -1, bool bAllowSelectionDialog = false)
        {
            if (!IsValid)
                return null;

            MM45QuestInfo info = new MM45QuestInfo();

            MM45PartyInfo party = ReadMM45PartyInfo();
            if (party == null)
                return null;

            MM45GameInfo gameInfo = GetGameInfo() as MM45GameInfo;
            if (gameInfo == null)
                return null;

            int iMapIndex = GetCurrentMapIndex();

            MemoryStream stream = new MemoryStream();

            if (m_fileDarkCur == null)
                m_fileDarkCur = new WatchedFile(MM5CurrentDataFile);
            if (m_fileXeenCur == null)
                m_fileXeenCur = new WatchedFile(MM4CurrentDataFile);

            if (bAllowSelectionDialog && (m_fileDarkCur == null || m_fileXeenCur == null))
            {
                if (MessageBox.Show("The quest information will be more complete if you specify your DARK.CUR and XEEN.CUR files.  Would you like to do this now?",
                    "Locate your DARK.CUR and XEEN.CUR files?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SelectGameFiles();
                }
            }

            if (iMapIndex != m_lastQuestMap && m_fileXeenCur != null && m_fileDarkCur != null)
            {
                m_fileXeenCur.ForceRead();
                m_fileDarkCur.ForceRead();
                m_lastQuestMap = iMapIndex;
            }

            if (m_fileXeenCur != null && !m_fileXeenCur.UserCanceled && m_fileXeenCur.IsValid &&
                m_fileDarkCur != null && !m_fileDarkCur.UserCanceled && m_fileDarkCur.IsValid)
                m_fileQuestInfo.SetInfo(m_fileXeenCur.GetBytes(), m_fileDarkCur.GetBytes(), GetScriptBytes(), GetMonsterBytes(), GetMapBytes(), iMapIndex, IsVoiceVersion());
            else
                m_fileQuestInfo.Valid = false;

            byte[] questBytes = party.QuestBytes;
            if (questBytes == null)
                return null;
            stream.Write(questBytes, 0, questBytes.Length);
            stream.WriteByte((byte)iOverrideCharAddress);
            Global.WriteInt32(stream, iMapIndex);
            if (m_fileQuestInfo != null)
            {
                byte[] bytesFQI = m_fileQuestInfo.GetBytes();
                stream.Write(bytesFQI, 0, bytesFQI.Length);
            }

            questBytes = gameInfo.Party.GetQuestRelatedBytes();
            stream.Write(questBytes, 0, questBytes.Length);

            byte[] newBytes = stream.ToArray();

            bool bSameChar = (lastInfo != null && (newBytes[0] == lastInfo.Bytes[0] || newBytes[0] == 255));

            if (lastInfo != null && Global.CompareBytes(lastInfo.Bytes, newBytes, 1, 1) && bSameChar)
                return lastInfo;    // Don't bother going through the lengthy SetQuests routine if nothing has changed

            info.SetQuests(new MM45QuestData(party, gameInfo, m_fileQuestInfo), iOverrideCharAddress);
            info.MapIndex = iMapIndex;
            info.Bytes = newBytes;

            return info;
        }

        public List<MemoryBytes> GetMapBytes()
        {
            if (!IsValid)
                return null;

            List<MemoryBytes> list = new List<MemoryBytes>(4);

            try
            {
                Size sz = GetCurrentMapDimensions();
                if (sz.Width > 16 && sz.Height > 16)
                {
                    list.Add(ReadOffset(Memory.MapAppearance1, 0x300));
                    list.Add(ReadOffset(Memory.MapAppearance2, 0x300));
                    list.Add(ReadOffset(Memory.MapAppearance3, 0x300));
                    list.Add(ReadOffset(Memory.MapAppearance4, 0x300));
                }
                else
                {
                    switch (GetCurrentMapQuad())
                    {
                        case 0: list.Add(ReadOffset(Memory.MapAppearance1, 0x300)); break;
                        case 1: list.Add(ReadOffset(Memory.MapAppearance2, 0x300)); break;
                        case 2: list.Add(ReadOffset(Memory.MapAppearance3, 0x300)); break;
                        case 3: list.Add(ReadOffset(Memory.MapAppearance4, 0x300)); break;
                        default: break;
                    }
                }
            }
            catch (Exception)
            {
                // Probably shutting down
                return list;
            }

            return list;
        }

        public override byte[] GetMonsterBytes()
        {
            MemoryStream ms = new MemoryStream();
            int iNumMonsters = GetMonsterCount();
            MemoryBytes monsters = ReadOffset(Memory.MonsterPositions, iNumMonsters * 20);
            if (monsters == null)
                return null;
            for (int i = 0; i < iNumMonsters; i++)
            {
                ms.WriteByte(monsters[i * 20 + MM45Memory.MonsterCurrentX]);
                ms.WriteByte(monsters[i * 20 + MM45Memory.MonsterCurrentY]);
                ms.WriteByte(monsters[i * 20 + MM45Memory.MonsterLocalIndex]);
                ms.WriteByte(0);
            }
            return ms.ToArray();
        }

        public override int CorrectMapIndex(int iMap)
        {
            if (GetMapSide() == 1)
                return iMap | 256;
            return iMap % 256;
        }
        public override GameInformationControl CreateGameInfoControl(IMain main) { return new MM45GameInformationControl(main); }

        public override int GetBlessValue()
        {
            if (!IsValid)
                return 0;

            return ReadByte(Memory.PartyBytes + MM45PartyBytes.OffsetBlessed);
        }


        public override string GetRaceDescription(GenericRace race)
        {
            switch (race)
            {
                case GenericRace.Human: return "All Resistances 7, Swimming";
                case GenericRace.Elf: return "Energy/Magic 5, Thievery 10, -2HP,+2MP/Lv  (Sorc/Arc)";
                case GenericRace.Gnome: return "Fire/Elec/Cold/Energy 5, Poison 20, Thievery 10, -1HP,+1SP/Lv, Spot Secret Doors";
                case GenericRace.Dwarf: return "Fire/Elec/Cold/Poison/Energy 2, Magic 20, Thievery 5, +1HP,-1SP/Lv, Danger Sense";
                case GenericRace.HalfOrc: return "Fire/Elec/Cold 10, Thievery -10, +2HP,-2SP/Lv";
                default: return "Unknown";
            }
        }

        public override MMMonster GetDefaultMonster(int iIndex, int iSide = 0)
        {
            if (iSide == (int)MM45Side.Clouds)
            {
                MM4MonsterList mm4List = new MM4MonsterList();
                if (iIndex >= mm4List.Monsters.Count)
                    return null;
                return mm4List.Monsters[iIndex];
            }
            MM5MonsterList mm5List = new MM5MonsterList();
            if (iIndex >= mm5List.Monsters.Count)
                return null;
            return mm5List.Monsters[iIndex];
        }

        public override string StatToolTip(int iIndex, int iValue)
        {
            PrimaryStat[] order = StatOrder;
            if (order[iIndex] == PrimaryStat.Might)
                return String.Format("Might gives a bonus ({0}: {1}) to damage inflicted with melee weapons in combat.  Penalties cannot drop damage per attack below 1.", iValue, GetStatModString(iValue, PrimaryStat.Might));
            if (order[iIndex] == PrimaryStat.Intellect)
                return String.Format("Intellect gives a bonus ({0}: {1}) to maximum spell points.  The formulas are Sorcerer/Druid:Lev*(3+Bonus), Archer/Ranger:(Lev*(3+Bonus))/2.  For Druids and Rangers the Bonus is the average of the Int and Per bonuses.", iValue, GetStatModString(iValue, PrimaryStat.Intellect));
            if (order[iIndex] == PrimaryStat.Personality)
                return String.Format("Personality gives a bonus ({0}: {1})  to maximum spell points.  The formulas are Cleric/Druid:Lev*(3+Bonus), Paladin/Ranger:(Lev*(3+Bonus))/2.  For Druids and Rangers the Bonus is the average of the Int and Per bonuses.", iValue, GetStatModString(iValue, PrimaryStat.Personality));
            if (order[iIndex] == PrimaryStat.Endurance)
                return String.Format("Endurance gives a bonus ({0}: {1}) to maximum hit points.  The formula is Level*(Base*Bonus).  The base values are Barbarian:12, Knight:10, Ranger:9, Paladin/Robber:8, Archer/Ninja:7, Druid:6, Cleric:5, Sorcerer:4", iValue, GetStatModString(iValue, PrimaryStat.Endurance));
            if (order[iIndex] == PrimaryStat.Speed)
                return String.Format("Speed gives a bonus ({0}: {1}) to armor class.  Speed also determines the order of actions in combat.", iValue, GetStatModString(iValue, PrimaryStat.Speed));
            if (order[iIndex] == PrimaryStat.Accuracy)
                return String.Format("Accuracy gives a bonus ({0}: {1}) to hit in combat.", iValue, GetStatModString(iValue, PrimaryStat.Accuracy));
            if (order[iIndex] == PrimaryStat.Luck)
                return String.Format("Luck gives a bonus ({0}: {1}) to resistance rolls, both damage resistance and status condition resistance.  You must have at least 1 resistance point for the Luck bonus to apply at all.", iValue, GetStatModString(iValue, PrimaryStat.Luck));
            return "";
        }

        public override int GetCurrentMapQuad()
        {
            return 0;
        }

        public bool SetAwards(int iCharIndex, byte[] awards)
        {
            if (!IsValid)
                return false;

            if (awards == null || awards.Length < MM45.Offsets.AwardsLength)
                return false;

            if (iCharIndex < 0 || iCharIndex > GetNumChars())
                return false;

            return WriteOffset(Memory.PartyInfo + (iCharIndex * MM45Character.SizeInBytes) + MM45.Offsets.Awards, awards);
        }

        public override bool HasCartography { get { return true; } }

        private int GetCartographyAddress(int x, int y)
        {
            switch (x / 16 + ((y / 16) * 2))
            {
                case 0: return Memory.MapCartography1;
                case 1: return Memory.MapCartography2;
                case 2: return Memory.MapCartography3;
                case 3: return Memory.MapCartography4;
                default: return Memory.MapCartography1;
            }
        }

        public override bool IsExplored(int x, int y)
        {
            // Should be faster than GetCartography().IsBitSet() for a single square

            if (!IsValid)
                return true;

            if (!PointInMap(new Point(x,y)))
                return false;

            int offset = GetCartographyAddress(x, y);
            byte bCartography = ReadByte(offset + ((y % 16) * 2) + ((x % 16) / 8));
            return (bCartography >> (7 - ((x % 8))) & 1) == 1;
        }

        public override bool ToggleCartography(Point pt, Toggle toggle)
        {
            if (!IsValid)
                return false;

            if (!PointInMap(pt))
                return false;

            int offset = GetCartographyAddress(pt.X, pt.Y);
            int iDelta = ((pt.Y % 16) * 2) + ((pt.X % 16) / 8);
            byte bCartography = ReadByte(offset + iDelta);
            byte bit = (byte)(1 << (7 - (pt.X % 8)));
            switch (toggle)
            {
                case Toggle.Toggle:
                    bCartography ^= bit;
                    break;
                case Toggle.Reset:
                    bCartography &= (byte) ~bit;
                    break;
                case Toggle.Set:
                    bCartography |= bit;
                    break;
            }
            return WriteByte(offset + iDelta, bCartography);
        }

        public override String GetGameTimeString(bool bFull)
        {
            if (!IsValid)
                return String.Empty;

            GameTime gt = GetGameTime();
            return MM345GameInfo.GetGameTimeString(gt, bFull);
        }

        public override long GetGameTimeLong()
        {
            GameTime gt = GetGameTime();
            return (gt.Hour * 60 + gt.Minute) | (gt.Day << 16) | (gt.Year << 24);
        }

        public override GameTime GetGameTime()
        {
            if (!IsValid)
                return GameTime.Empty;
            return GameTime.FromMM345Values(ReadUInt16(Memory.Year), ReadByte(Memory.Day), ReadUInt16(Memory.Minutes));
        }

        public override bool ActiveMonstersNearby
        {
            get
            {
                Point ptParty = GetPartyPosition();
                MonsterLocations monsters = GetMonsterLocations();
                foreach (MonsterPosition pos in monsters.MonsterPositions.Values)
                {
                    Proximity prox = new Proximity(pos.Position, ptParty);
                    if (prox.Simple < 4 && pos.Monsters.Any(m => m.Active))
                        return true;
                }
                return false;
            }
        }

        public override bool IsScriptBitMonster(int iMonster, int iMapIndex = -1)
        {
            if (iMapIndex == -1)
                iMapIndex = GetCurrentMapIndex();

            // Some monsters are manipulated solely for the purpose storing a bit of information
            // such as whether a lever is pulled or not.  This function will return true if this is one of them.
            if (iMapIndex < 256)
                return false;   // Nothing on Cloudside does this

            switch ((MM5Map)(iMapIndex % 256))
            {
                case MM5Map.A4Castleview: return (iMonster >= 10 && iMonster <= 37);
                case MM5Map.A1CastleAlamarLevel2: return (iMonster >= 0 && iMonster <= 49);
                case MM5Map.A4EllingersTowerLevel2: return (iMonster >= 0 && iMonster <= 4);
                case MM5Map.A4EllingersTowerLevel3: return (iMonster >= 0 && iMonster <= 2);
                case MM5Map.D4SouthernTowerLevel1: return (iMonster >= 4 && iMonster <= 5) || iMonster == 23;
                case MM5Map.C4TempleOfBarkLevel1: return (iMonster >= 0 && iMonster <= 1);
                case MM5Map.C4TempleOfBarkLevel4: return (iMonster >= 0 && iMonster <= 24);
                case MM5Map.C4TempleOfBarkLevel5: return (iMonster >= 1 && iMonster <= 8);
                case MM5Map.F2LostSoulsDungeonLevel2: return (iMonster >= 0 && iMonster <= 18);
                case MM5Map.F2LostSoulsDungeonLevel4: return (iMonster >= 0 && iMonster <= 6);
                case MM5Map.D2TheGreatPyramidLevel1: return (iMonster >= 0 && iMonster <= 13);
                case MM5Map.B2EscapePod1: return (iMonster >= 0 && iMonster <= 1);
                case MM5Map.B3DarkstoneTowerLevel1: return (iMonster >= 0 && iMonster <= 3);
                case MM5Map.B3DarkstoneTowerLevel2: return (iMonster >= 0 && iMonster <= 2);
                case MM5Map.B3DarkstoneTowerLevel3: return (iMonster >= 0 && iMonster <= 7);
                case MM5Map.E3DungeonOfDeathLevel1: return (iMonster >= 0 && iMonster <= 89);
                case MM5Map.E3DungeonOfDeathLevel2: return (iMonster >= 0 && iMonster <= 3) || (iMonster >= 9 && iMonster <= 56);
                case MM5Map.E3DungeonOfDeathLevel3: return (iMonster >= 0 && iMonster <= 4);
                case MM5Map.A2SouthernSphinxLevel1: return (iMonster >= 7 && iMonster <= 8);
                case MM5Map.E3Sandcaster:
                case MM5Map.C2Olympus:
                case MM5Map.C2OlympusSewer:
                case MM5Map.A4CastleKalindraLevel1:
                case MM5Map.A4CastleKalindraLevel2:
                case MM5Map.A4CastleKalindraLevel3:
                case MM5Map.A1CastleAlamarLevel3:
                case MM5Map.A4EllingersTowerLevel1:
                case MM5Map.A4EllingersTowerLevel4:
                case MM5Map.D4SouthernTowerLevel3:
                case MM5Map.D2TheGreatPyramidLevel2:
                case MM5Map.D2TheGreatPyramidLevel3:
                case MM5Map.D2TheGreatPyramidLevel4:
                case MM5Map.B1EscapePod2:
                case MM5Map.B2SkyroadB2:
                case MM5Map.C3SkyroadC3:
                case MM5Map.D3SkyroadD3:
                case MM5Map.E3SkyroadE3:
                case MM5Map.B3DarkstoneTowerLevel4:
                case MM5Map.B3CloudsOfTheAncients: return iMonster == 0;
                default: return false;
            }
        }

        public override bool TradeBackpacks(int iCharAddress1, int iCharAddress2)
        {
            // Backpacks can only be traded if both characters have enough capacity to make the trade
            // This includes all four item types (weapons, armor, accessories, misc)

            List<Item> equipped1 = GetEquippedItems(iCharAddress1);
            List<Item> equipped2 = GetEquippedItems(iCharAddress2);
            List<Item> backpack1 = GetBackpack(iCharAddress1, BackpackType.UnequippedOnly);
            List<Item> backpack2 = GetBackpack(iCharAddress2, BackpackType.UnequippedOnly);

            InventoryItemCounts countEquip1 = new InventoryItemCounts(equipped1);
            InventoryItemCounts countEquip2 = new InventoryItemCounts(equipped2);
            InventoryItemCounts countPack1 = new InventoryItemCounts(backpack1);
            InventoryItemCounts countPack2 = new InventoryItemCounts(backpack2);

            if (countPack1.Weapons > 9 - countEquip2.Weapons ||
                countPack2.Weapons > 9 - countEquip1.Weapons ||
                countPack1.Armor > 9 - countEquip2.Armor ||
                countPack2.Armor > 9 - countEquip1.Armor ||
                countPack1.Accessories > 9 - countEquip2.Accessories ||
                countPack2.Accessories > 9 - countEquip1.Accessories ||
                countPack1.Miscellaneous > 9 - countEquip2.Miscellaneous ||
                countPack2.Miscellaneous > 9 - countEquip1.Miscellaneous)
                return false;

            SetBackpackResult result1 = SetBackpack(iCharAddress1, backpack2);
            SetBackpackResult result2 = SetBackpack(iCharAddress2, backpack1);

            return (result1 == SetBackpackResult.Success && result2 == SetBackpackResult.Success);
        }

        public override Point GetSurfaceSector(int iMap)
        {
            if (iMap / 256 == 0)
            {
                // Cloudside surface maps
                switch ((MM4Map)iMap)
                {
                    case MM4Map.A1Surface: return new Point(0, 3);
                    case MM4Map.A2Surface: return new Point(0, 2);
                    case MM4Map.A3Surface: return new Point(0, 1);
                    case MM4Map.A4Surface: return new Point(0, 0);
                    case MM4Map.B1Surface: return new Point(1, 3);
                    case MM4Map.B2Surface: return new Point(1, 2);
                    case MM4Map.B3Surface: return new Point(1, 1);
                    case MM4Map.B4Surface: return new Point(1, 0);
                    case MM4Map.C1Surface: return new Point(2, 3);
                    case MM4Map.C2Surface: return new Point(2, 2);
                    case MM4Map.C3Surface: return new Point(2, 1);
                    case MM4Map.C4Surface: return new Point(2, 0);
                    case MM4Map.D1Surface: return new Point(3, 3);
                    case MM4Map.D2Surface: return new Point(3, 2);
                    case MM4Map.D3Surface: return new Point(3, 1);
                    case MM4Map.D4Surface: return new Point(3, 0);
                    case MM4Map.E1Surface: return new Point(4, 3);
                    case MM4Map.E2Surface: return new Point(4, 2);
                    case MM4Map.E3Surface: return new Point(4, 1);
                    case MM4Map.E4Surface: return new Point(4, 0);
                    case MM4Map.F1Surface: return new Point(5, 3);
                    case MM4Map.F2Surface: return new Point(5, 2);
                    case MM4Map.F3Surface: return new Point(5, 1);
                    case MM4Map.F4Surface: return new Point(5, 0);
                    default: return new Point(-1, -1);
                }
            }
            else
            {
                // Darkside surface maps
                switch ((MM5Map)(iMap % 256))
                {
                    case MM5Map.A1Surface: return new Point(0, 3);
                    case MM5Map.A2Surface: return new Point(0, 2);
                    case MM5Map.A3Surface: return new Point(0, 1);
                    case MM5Map.A4Surface: return new Point(0, 0);
                    case MM5Map.B1Surface: return new Point(1, 3);
                    case MM5Map.B2Surface: return new Point(1, 2);
                    case MM5Map.B3Surface: return new Point(1, 1);
                    case MM5Map.B4Surface: return new Point(1, 0);
                    case MM5Map.C1Surface: return new Point(2, 3);
                    case MM5Map.C2Surface: return new Point(2, 2);
                    case MM5Map.C3Surface: return new Point(2, 1);
                    case MM5Map.C4Surface: return new Point(2, 0);
                    case MM5Map.D1Surface: return new Point(3, 3);
                    case MM5Map.D2Surface: return new Point(3, 2);
                    case MM5Map.D3Surface: return new Point(3, 1);
                    case MM5Map.D4Surface: return new Point(3, 0);
                    case MM5Map.E1Surface: return new Point(4, 3);
                    case MM5Map.E2Surface: return new Point(4, 2);
                    case MM5Map.E3Surface: return new Point(4, 1);
                    case MM5Map.E4Surface: return new Point(4, 0);
                    case MM5Map.F1Surface: return new Point(5, 3);
                    case MM5Map.F2Surface: return new Point(5, 2);
                    case MM5Map.F3Surface: return new Point(5, 1);
                    case MM5Map.F4Surface: return new Point(5, 0);
                    default: return new Point(-1, -1);
                }
            }
        }

        public override bool SameSurfaceMaps(int iMap1, int iMap2) { return iMap1 / 256 == iMap2 / 256; }

        public override bool SkipIntroductions(int iTimeout = 5000, bool bPreLaunch = false)
        {
            DateTime dtStart = DateTime.Now;
            while ((DateTime.Now - dtStart).TotalMilliseconds < iTimeout)
            {
                MM45GameState state = ReadMM45GameState();
                if (state != null)
                {
                    switch (state.OriginalMain)
                    {
                        case MainState.Opening:
                        case MainState.Opening2:
                            TweakSleep(10);
                            SendKeysToDOSBox(new Keys[] { Keys.Escape }, true);
                            TweakSleep(300);
                            break;
                        case MainState.MainMenu:
                            TweakSleep(10);
                            SendKeysToDOSBox(new Keys[] { Keys.L }, true);
                            TweakSleep(200);
                            SendKeysToDOSBox(new Keys[] { Keys.L }, true);
                            TweakSleep(200);
                            break;
                        case MainState.SelectSpell:
                            TweakSleep(10);
                            SendKeysToDOSBox(new Keys[] { Keys.D1, Keys.Enter }, true);
                            TweakSleep(500);
                            break;
                        case MainState.Adventuring:
                            return true;
                        default:
                            break;
                    }
                }
                Thread.Sleep(10);
            }
            return false;
        }

        public override IEnumerable<Monster> GetMonsterList()
        {
            return MM45.MM4Monsters.Concat(MM45.MM5Monsters);
        }

        public int GetCurrentMapQuadMemoryOffset()
        {
            switch (ReadByte(Memory.CurrentMapQuad))
            {
                case 1: return Memory.MapAppearance2;
                case 2: return Memory.MapAppearance3;
                case 3: return Memory.MapAppearance4;
                default: return Memory.MapAppearance1;
            }
        }

        public override MapBytes GetCurrentMapBytes()
        {
            Size sz = GetCurrentMapDimensions();

            if (sz.Width == 32 && sz.Height == 32)
            {
                MemoryBytes appearance1 = ReadOffset(Memory.MapAppearance1, 512);
                MemoryBytes appearance2 = ReadOffset(Memory.MapAppearance2, 512);
                MemoryBytes appearance3 = ReadOffset(Memory.MapAppearance3, 512);
                MemoryBytes appearance4 = ReadOffset(Memory.MapAppearance4, 512);
                MemoryBytes attributes1 = ReadOffset(Memory.MapAttributes1, 512);
                MemoryBytes attributes2 = ReadOffset(Memory.MapAttributes2, 512);
                MemoryBytes attributes3 = ReadOffset(Memory.MapAttributes3, 512);
                MemoryBytes attributes4 = ReadOffset(Memory.MapAttributes4, 512);
                if (appearance1 == null || appearance2 == null || appearance3 == null || appearance4 == null || 
                    attributes1 == null || attributes2 == null || attributes3 == null || attributes4 == null)
                    return null;

                byte[] quadApp = MMMapData.CreateQuadBytes(32, appearance1.Bytes, appearance2.Bytes, appearance3.Bytes, appearance4.Bytes);
                byte[] quadAttrib = MMMapData.CreateQuadBytes(16, attributes1.Bytes, attributes2.Bytes, attributes3.Bytes, attributes4.Bytes);
                return new MapBytes(Global.Combine(quadApp, quadAttrib), 32, 32);
            }
            else if (sz.Width == 32)
            {
                MemoryBytes appearance1 = ReadOffset(Memory.MapAppearance1, 512);
                MemoryBytes appearance2 = ReadOffset(Memory.MapAppearance2, 512);
                MemoryBytes attributes1 = ReadOffset(Memory.MapAttributes1, 512);
                MemoryBytes attributes2 = ReadOffset(Memory.MapAttributes2, 512);
                if (appearance1 == null || appearance2 == null ||
                    attributes1 == null || attributes2 == null)
                    return null;

                byte[] pairApp = MMMapData.CreatePairBytes(32, appearance1.Bytes, appearance2.Bytes, Orientation.Horizontal);
                byte[] pairAttrib = MMMapData.CreatePairBytes(16, attributes1.Bytes, attributes2.Bytes, Orientation.Horizontal);
                return new MapBytes(Global.Combine(pairApp, pairApp), 32, 16);
            }
            else if (sz.Height == 32)
            {
                MemoryBytes appearance1 = ReadOffset(Memory.MapAppearance1, 512);
                MemoryBytes appearance2 = ReadOffset(Memory.MapAppearance2, 512);
                MemoryBytes attributes1 = ReadOffset(Memory.MapAttributes1, 512);
                MemoryBytes attributes2 = ReadOffset(Memory.MapAttributes2, 512);
                if (appearance1 == null || appearance2 == null ||
                    attributes1 == null || attributes2 == null)
                    return null;

                byte[] pairApp = MMMapData.CreatePairBytes(32, appearance1.Bytes, appearance2.Bytes, Orientation.Vertical);
                byte[] pairAttrib = MMMapData.CreatePairBytes(16, attributes1.Bytes, attributes2.Bytes, Orientation.Vertical);
                return new MapBytes(Global.Combine(pairApp, pairApp), 16, 32);
            }

            MemoryBytes app1 = ReadOffset(Memory.MapAppearance1, 512);
            MemoryBytes attr1 = ReadOffset(Memory.MapAttributes1, 512);
            if (app1 == null || attr1 == null)
                return null;

            return new MapBytes(Global.Combine(app1.Bytes, attr1.Bytes), 16, 16);
        }

        public override MapData CreateLiveMapData(MapBytes mb)
        {
            if (mb == null || mb.Bytes == null)
                return null;

            MM45MapData data = new MM45MapData();
            data.LiveOnly = true;
            data.MapBytes = new MM45MapBytes(GetMapInfoBytes());
            data.Bounds = new Rectangle(0, 0, mb.Size.Width, mb.Size.Height);
            data.Appearance = mb.Bytes;
            data.Attributes = Global.Subset(mb.Bytes, data.Width * data.Height * 2, data.Width * data.Height);
            data.FloorTileSet = ReadByte(Memory.FloorTileSet);
            return data;
        }
    }
}

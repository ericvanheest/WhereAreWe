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
    public class UltimaGameState : GameState
    {
        public GameNames UltimaGame = GameNames.BardsTale1;

        public override GameNames Game { get { return UltimaGame; } }
    }

    public class UltimaBackpackBytes
    {
        public byte[] Items;  // 46 Int16s

        public UltimaBackpackBytes()
        {
            Items = Global.NullBytes(94);
            foreach (int iDivider in new int[] { 8, 20, 52, 74, 92})
                Global.SetInt16(Items, iDivider, -1);
        }

        public UltimaBackpackBytes(byte[] bytes)
        {
            Items = bytes;
        }
    }

    public class UltimaPartyInfo : PartyInfo
    {
        public override int CharacterSize => Ultima1Character.SizeInBytes;
        public string CharacterName => Encoding.ASCII.GetString(Bytes, 0, Games.MaxNameLength(Game)).TrimEnd(new char[] { ' ', '\0' });

        public UltimaPartyInfo(byte[] bytes)
        {
            Bytes = bytes;
            State = new GameState();
            NumChars = 1;
            Positions = new byte[1] { 0 };
            Addresses = new int[1] { 0 };
        }

        public override byte[] QuestBytes
        {
            get
            {
                // Most quests are based on either the "quest ints" or the inventory, so return all of those 
                MemoryStream ms = new MemoryStream();
                ms.Write(Bytes, Ultima1.Offsets.Quests, Ultima1.Offsets.QuestsLength);
                ms.Write(Bytes, Ultima1.Offsets.Inventory, Ultima1.Offsets.InventoryLength);
                return ms.ToArray();
            }
        }
    }

    public class UltimaGameInfo : GameInfo
    {
    }

    public class UltimaMapData : MapData
    {
        public MapBytes RawBytes;
        public override int DefaultZoom => Index == 0 ? 50 : ((Index & 0xff) < 41) ? 100 : base.DefaultZoom;
        public Point OverworldLocation;
        public virtual UltimaTownTile GetTownValue(int horiz, int vert) => 0;
        public virtual UltimaOutdoorTile GetOverworldValue(int horiz, int vert) => 0;
        public virtual UltimaIndoorTile GetUnderworldValue(int horiz, int vert) => 0;
    }

    public abstract class UltimaMemoryHacker : MemoryHacker
    {
        public abstract UltimaMemory Memory { get; }

        public override byte[] MainSearch => Memory.MainSearch;
        public override MemoryGuess[] Guesses => Memory.Guesses;
        protected virtual UltimaGameState GetMainState() => null;
        public override GameState GetGameState() => ReadUltima123GameState();
        public override int CharacterSize => Ultima1Character.SizeInBytes;
        public abstract void SetInventory(UltimaInventory inv);
        public override string SpellType1 => "All";

        protected virtual int GetNumChars() => 1;

        protected LocationInformation GetLocationForce()
        {
            if (!IsValid)
                return LocationInformation.Empty;

            return GetLocation();
        }

        private UltimaGameState ReadUltima123GameState()
        {
            if (m_gsCurrent != null && m_gsCurrent.ReadTime.AddMilliseconds(50) > DateTime.Now)
                return m_gsCurrent as UltimaGameState;     // Don't spam the game state from different windows

            UltimaGameState state = GetMainState();
            if (state == null)
                return null;

            state.UltimaGame = Game;
            state.Location = GetLocationForce();
            state.InShop = false;

            m_gsCurrent = state;
            return state;
        }

        public override List<BaseCharacter> GetCharacters()
        {
            UltimaPartyInfo pi = GetPartyInfo() as UltimaPartyInfo;
            if (pi == null)
                return null;

            List<BaseCharacter> chars = new List<BaseCharacter>(pi.NumChars);
            for (int i = 0; i < pi.NumChars; i++)
            {
                UltimaCharacter ultimaChar = UltimaCharacter.Create(Game, pi.Bytes, i * CharacterSize);
                ultimaChar.Address = i;
                chars.Add(ultimaChar);
            }

            return chars;
        }

        public override string PleaseFormPartyString { get { return "Please create a character and enter the game."; } }
    }
}

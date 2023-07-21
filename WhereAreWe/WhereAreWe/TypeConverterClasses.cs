using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace WhereAreWe
{
    public class DrawColorTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                DrawColor dc = new DrawColor();

                string[] args = ((string)value).Split(',');
                if (args.Length < 1)
                    return dc;

                int iColor;
                KnownColor c;

                if (!Enum.TryParse(args[0].Trim(), true, out c))
                {
                    if (!Int32.TryParse(args[0], System.Globalization.NumberStyles.HexNumber, null, out iColor))
                        return dc;
                    dc.color = Color.FromArgb(iColor);
                }
                else
                    dc.color = Color.FromKnownColor(c);

                if (args.Length < 2)
                    return dc;

                DashStyle style = DashStyle.Solid;
                HatchStyle styleBlock = HatchStyle.Percent90;

                if (!Enum.TryParse(args[1].Trim(), true, out style))
                    return dc;

                dc.style = style;

                if (!Enum.TryParse(args[2].Trim(), true, out styleBlock))
                    return dc;

                dc.styleBlock = styleBlock;

                if (args.Length < 4)
                    return dc;

                byte width = 1;
                if (!Byte.TryParse(args[3].Trim(), out width))
                    return dc;

                dc.width = width;
                return dc;
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return ((DrawColor)value).ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    [TypeConverter(typeof(DrawColorTypeConverter))]
    public class DrawColor
    {
        public Color color;
        public DashStyle style;
        public HatchStyle styleBlock;
        public byte width;

        public DrawColor()
        {
            color = Color.Black;
            style = DashStyle.Solid;
            styleBlock = HatchStyle.Percent90;
            width = 1;
        }

        public DrawColor(Color c, DashStyle s)
        {
            color = c;
            style = s;
            styleBlock = HatchStyle.Percent90;
            width = 1;
        }

        public DrawColor(Color c)
        {
            color = c;
            style = DashStyle.Solid;
            styleBlock = HatchStyle.Percent90;
            width = 1;
        }

        public DrawColor(Color c, HatchStyle s)
        {
            color = c;
            style = DashStyle.Solid;
            styleBlock = s;
            width = 1;
        }

        public DrawColor(DrawColor copy)
        {
            color = copy.color;
            style = copy.style;
            width = copy.width;
            styleBlock = copy.styleBlock;
        }

        public DrawColor(Color c, DashStyle s, byte w)
        {
            color = c;
            style = s;
            width = w;
            styleBlock = HatchStyle.Percent90;
        }

        public DrawColor(MapLineInfo info)
        {
            color = info.Color;
            style = info.Pattern;
            width = (byte)info.Width;
            styleBlock = HatchStyle.Percent90;
        }

        public ColorPattern BlockStyle { get { return new ColorPattern(color, styleBlock); } }
        public MapLineInfo LineStyle { get { return new MapLineInfo(color, style, width); } }

        public override string ToString()
        {
            if (color.IsKnownColor)
                return String.Format("{0}, {1}, {2}, {3}", color.Name, style.ToString(), styleBlock.ToString(), width);
            else
                return String.Format("{0:X4}, {1}, {2}, {3}", color.ToArgb(), style.ToString(), styleBlock.ToString(), width);
        }
    }

    public class ShortcutKeys
    {
        public Keys[] Keys;

        public int Length
        {
            get { return Keys.Length; }
        }

        public ShortcutKeys()
        {
            Keys = new Keys[0];
        }

        public ShortcutKeys(Keys[] keys)
        {
            Keys = keys;
            Array.Sort(Keys);
        }

        public string KeyString
        {
            get
            {
                return Global.KeyString(Keys);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is ShortcutKeys)
            {
                ShortcutKeys keysIn = (ShortcutKeys)obj;
                if (keysIn.Length != Keys.Length)
                    return false;
                for (int i = 0; i < keysIn.Length; i++)
                    if (keysIn.Keys[i] != Keys[i])
                        return false;

                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int iHash = 0;
            for (int i = 0; i < 4; i++)
            {
                if (i >= Keys.Length)
                    return iHash;
                iHash = iHash | (((int)Keys[i]) << (i * 8));
            }
            return iHash;
        }

        public override string ToString()
        {
            return KeyString;
        }

        public static ShortcutKeys[] GetCombinations(Keys keyLast, Keys[] keys)
        {
            // Return keys.Length choose (keys.Length-1), but only the
            // combinations that contain the last-pressed key
            // (also returns the original array for convenience)

            List<ShortcutKeys> list = new List<ShortcutKeys>();
            list.Add(new ShortcutKeys(keys));

            for (int iSkip = 0; iSkip < keys.Length; iSkip++)
            {
                if (keys[iSkip] == keyLast)
                    continue;

                List<Keys> combination = new List<Keys>(keys);
                combination.RemoveAt(iSkip);
                list.Add(new ShortcutKeys(combination.ToArray()));
            }

            return list.ToArray();
        }

        public static string SKArrayString(List<ShortcutKeys> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach (ShortcutKeys sk in list)
                sb.AppendFormat("{0} ", sk.KeyString);

            return sb.ToString();
        }
    }

    [TypeConverter(typeof(NotificationsTypeConverter))]
    public class Notifications
    {
        public bool Enabled = true;
        public Dictionary<Action, Notification> Alerts;

        public Notifications()
        {
            Alerts = new Dictionary<Action, Notification>();
        }

        public static Notifications Defaults { get { return new Notifications(GetDefaults(), true); } }

        private static Dictionary<Action, Notification> GetDefaults()
        {
            Dictionary<Action, Notification> alerts = new Dictionary<Action, Notification>();
            for (Action action = Action.SpellHotkey1; action <= Action.SpellHotkey10; action++)
                alerts.Add(action, new Notification("$ActionChar: $SpellAction $SpellName $successState"));
            alerts.Add(Action.CureAllSilent, new Notification("$curChar: Cure-All $successState"));
            for (Action action = Action.CureAll1; action <= Action.CureAll8; action++)
                alerts.Add(action, new Notification("$ActionChar: Cure-All $successState"));
            for (Action action = Action.TradeBackpack1; action <= Action.TradeBackpack8; action++)
                alerts.Add(action, new Notification("$curChar traded with $actionChar: $successState"));
            return alerts;
        }

        public Notifications(Dictionary<Action, Notification> alerts, bool bEnabled)
        {
            Alerts = alerts;
            Enabled = bEnabled;
        }

        public bool Contains(Action action) { return Alerts != null && Alerts.ContainsKey(action); }
        public Notification GetAlert(Action action) { return Contains(action) ? Alerts[action] : null; }

        public Notifications Clone()
        {
            return Notifications.FromString(ToString());
        }

        public void SetAlert(Action action, Notification alert)
        {
            if (Alerts == null)
                Alerts = new Dictionary<Action, Notification>();
            if (!Alerts.ContainsKey(action))
                Alerts.Add(action, alert);
            else
                Alerts[action] = alert;
        }

        public static Notifications FromString(string strXML)
        {
            Dictionary<Action, Notification> alerts = new Dictionary<Action, Notification>();
            bool bEnabled = true;

            try
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(strXML);
                XmlElement eRoot = xml.SelectSingleNode("/Notifications") as XmlElement;
                if (eRoot.HasAttribute("enable") && eRoot.GetAttribute("enable") == "0")
                    bEnabled = false;
                foreach (XmlElement elemNote in xml.SelectNodes("/Notifications/Note"))
                {
                    int iType = Convert.ToInt32(elemNote.Attributes["type"].Value);
                    int iAction = Convert.ToInt32(elemNote.Attributes["action"].Value);
                    string strMessage = elemNote.SelectSingleNode("Message").InnerText;
                    string strAudioFile = elemNote.SelectSingleNode("Audio").InnerText;
                    alerts.Add((Action)iAction, new Notification((Notification.AlertType)iType, strMessage, strAudioFile));
                }
            }
            catch (Exception)
            {
                alerts = GetDefaults();
            }

            return new Notifications(alerts, bEnabled);
        }

        public override string ToString() { return ToString(true); }

        public string ToString(bool bFullDocument)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = bFullDocument ? new XmlWriterSettings() : new XmlWriterSettings { ConformanceLevel = ConformanceLevel.Fragment };

            using (XmlWriter writer = XmlWriter.Create(sb, settings))
            {
                if (bFullDocument)
                    writer.WriteStartDocument();

                writer.WriteStartElement("Notifications");
                writer.WriteAttributeString("enable", Enabled ? "1" : "0");
                foreach (Action action in Alerts.Keys)
                {
                    Notification note = Alerts[action];
                    if (!note.Any)
                        continue;   // Don't write out notifications with no alert types
                    writer.WriteStartElement("Note");
                    writer.WriteAttributeString("type", String.Format("{0}", (int)note.Type));
                    writer.WriteAttributeString("action", String.Format("{0}", (int)action));
                    writer.WriteStartElement("Message");
                    writer.WriteString(note.Message);
                    writer.WriteEndElement();  // message
                    writer.WriteStartElement("Audio");
                    writer.WriteString(note.AudioFile);
                    writer.WriteEndElement();  // audio
                    writer.WriteEndElement();  // note
                }
                writer.WriteEndElement(); // notifications
                if (bFullDocument)
                    writer.WriteEndDocument();
            }
            return sb.ToString();
        }
    }

    public class NotificationsTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
                return Notifications.FromString(value as string);
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return ((Notifications)value).ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    public class MemoryGuess
    {
        public ulong BlockLength;
        public uint Index;

        public MemoryGuess(ulong length, uint index)
        {
            BlockLength = length;
            Index = index;
        }

        public MemoryGuess(long length, uint index)
        {
            BlockLength = (ulong)length;
            Index = index;
        }

        public override int GetHashCode()
        {
            return (int) BlockLength ^ (int) Index;
        }

        public override bool Equals(object obj)
        {
            MemoryGuess mg = obj as MemoryGuess;
            if (mg == null)
                return false;
            return mg.BlockLength == BlockLength && mg.Index == Index;
        }

        public override string ToString()
        {
            return String.Format("Length=0x{0:X}, Index=0x{1:X}", BlockLength, Index);
        }
    }

    [TypeConverter(typeof(MemoryGuessesTypeConverter))]
    public class MemoryGuesses
    {
        public Dictionary<GameNames, MemoryGuess[]> Guesses;

        public MemoryGuesses()
        {
            Guesses = GetDefaults();
        }

        public MemoryGuesses(Dictionary<GameNames, MemoryGuess[]> guesses)
        {
            Guesses = guesses;
        }

        public static Dictionary<GameNames, MemoryGuess[]> GetDefaults()
        {
            Dictionary<GameNames, MemoryGuess[]> guesses = new Dictionary<GameNames, MemoryGuess[]>();

            guesses.Add(GameNames.MightAndMagic1, new MemoryGuess[] {
                    new MemoryGuess(4000000, 89113),
                    new MemoryGuess(4000000, 89481),
                    new MemoryGuess(4000000, 89513)
                });
            guesses.Add(GameNames.MightAndMagic2, new MemoryGuess[] {
                    new MemoryGuess(4000000, 72528),
                    new MemoryGuess(4000000, 72496),
                    new MemoryGuess(4000000, 72464),
                    new MemoryGuess(4000000, 72096),
                });
            guesses.Add(GameNames.MightAndMagic3, new MemoryGuess[] {
                    new MemoryGuess(16781312, 131401),
                    new MemoryGuess(16781312, 131369),
                    new MemoryGuess(16781312, 131337),
                    new MemoryGuess(16781312, 130969),
                });
            guesses.Add(GameNames.MightAndMagic45, new MemoryGuess[] {
                    new MemoryGuess(16781312, 7984),
                    new MemoryGuess(16781312, 7552),
                    new MemoryGuess(16781312, 8352),
                    new MemoryGuess(16781312, 7920),
                    new MemoryGuess(16781312, 8320),
                    new MemoryGuess(16781312, 7888),
                    new MemoryGuess(16781312, 7952),
                });
            return guesses;
        }

        public static MemoryGuesses FromString(string str)
        {
            Dictionary<GameNames, MemoryGuess[]> guesses = new Dictionary<GameNames, MemoryGuess[]>();

            try
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(str);
                foreach (XmlElement elemGame in xml.SelectNodes("/Guesses/Game"))
                {
                    List<MemoryGuess> list = new List<MemoryGuess>();
                    foreach (XmlElement elemGuess in elemGame.SelectNodes("Guess"))
                    {
                        MemoryGuess guess = new MemoryGuess(
                            Convert.ToInt64(elemGuess.Attributes["length"].Value),
                            Convert.ToUInt32(elemGuess.Attributes["index"].Value));
                        list.Add(guess);
                    }
                    guesses.Add((GameNames)Convert.ToInt32(elemGame.Attributes["index"].Value), list.ToArray());
                }
            }
            catch (Exception)
            {
                guesses = GetDefaults();
            }

            return new MemoryGuesses(guesses);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(sb))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Guesses");
                foreach (GameNames game in Guesses.Keys)
                {
                    writer.WriteStartElement("Game");
                    writer.WriteAttributeString("index", String.Format("{0}", (int)game));
                    foreach (MemoryGuess guess in Guesses[game])
                    {
                        writer.WriteStartElement("Guess");
                        writer.WriteAttributeString("length", String.Format("{0}", guess.BlockLength));
                        writer.WriteAttributeString("index", String.Format("{0}", guess.Index));
                        writer.WriteEndElement(); // guess
                    }
                    writer.WriteEndElement();  // game
                }
                writer.WriteEndElement(); // guesses
                writer.WriteEndDocument();
            }
            return sb.ToString();
        }
    }

    public class MemoryGuessesTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
                return MemoryGuesses.FromString(value as string);
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return ((MemoryGuesses)value).ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    public enum WindowType
    {
        None,
        Main,
        Party,
        GameInfo,
        Encounter,
        CreationAssistant,
        TrainingAssistant,
        About,
        Colors,
        DoNotShowAgain,
        DropTrash,
        ColorPicker,
        Icons,
        MapInfo,
        Items,
        MapSelection,
        SpellSelection,
        Monsters,
        Options,
        Quests,
        QuickRef,
        Scripts,
        Search,
        SelectGameFiles,
        SheetExpand,
        ShopInventory,
        SpellReference,
        StringsView,
        Unicode,
        Wait,
        Wizard,
        AskValue,
        AttributeEdit,
        BasicCharInfo,
        ColorPattern,
        ConditionEdit,
        EditBits,
        EditBytes,
        EditBytesSmall,
        EditCartography,
        EditLabels,
        EditRoster,
        EraEdit,
        ForbiddenSpellsEdit,
        GameShortcutsEditor,
        InventoryManipulator,
        ItemEditor,
        LineStyle,
        Effects,
        Skills,
        NoteTemplates,
        SelectImageRect,
        SheetOrganizer,
        SheetPathEdit,
        SheetSelector,
        TimeEdit,
        MM2KnownSPells,
        MM2MonsterEdit,
        MM2SecondarySkills,
        MM345KnownSPells,
        MM345MonsterEdit,
        MM3ConditionEdit,
        MM3ItemEdit,
        MM45ItemEdit,
        DebugConsole,
        EditZOrder,
        EditTriggerList
    }

    [TypeConverter(typeof(WindowInfoListTypeConverter))]
    public class WindowInfoList
    {
        public Dictionary<WindowType, WindowInfo> Info;

        public WindowInfoList()
        {
            Info = new Dictionary<WindowType, WindowInfo>();
        }

        public void Set(WindowType type, WindowInfo wi)
        {
            if (Info == null)
                Info = new Dictionary<WindowType, WindowInfo>();
            if (!Info.ContainsKey(type))
                Info.Add(type, wi);
            else
                Info[type] = wi;
        }

        private void EnsureKeyExists(WindowType type)
        {
            if (Info == null)
                Info = new Dictionary<WindowType, WindowInfo>();
            if (!Info.ContainsKey(type))
                Info.Add(type, WindowInfo.Empty);
        }

        public void SetNormalSize(WindowType type, Rectangle rc)
        {
            EnsureKeyExists(type);
            Info[type].NormalSize = rc;
        }

        public void SetAutoShow(WindowType type, bool bAuto)
        {
            EnsureKeyExists(type);
            Info[type].AutoShow = bAuto;
        }

        public void Set(WindowType type, Form form) { Set(type, new WindowInfo(form)); }

        public WindowInfo Get(WindowType type)
        {
            if (Info == null || !Info.ContainsKey(type))
                return WindowInfo.Empty;
            return Info[type];
        }

        public bool AutoShow(WindowType type) { return (Info == null || !Info.ContainsKey(type)) ? false : Info[type].AutoShow; }
        public bool Maximized(WindowType type) { return (Info == null || !Info.ContainsKey(type)) ? false : Info[type].Maximized; }
        public Rectangle NormalSize(WindowType type) { return (Info == null || !Info.ContainsKey(type)) ? Rectangle.Empty : Info[type].NormalSize; }
        public int[] SplitPositions(WindowType type) { return (Info == null || !Info.ContainsKey(type)) ? new int[0] : Info[type].SplitPositions; }
        public bool IsEmpty(WindowType type) { return (Info == null || !Info.ContainsKey(type)) ? true : Info[type].IsEmpty; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(sb))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Windows");
                foreach (WindowType type in Info.Keys)
                {
                    writer.WriteRaw(Info[type].ToString(type));
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            return sb.ToString();
        }
    }

    public class WindowInfoListTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                XmlDocument doc = new XmlDocument();
                WindowInfoList list = new WindowInfoList();
                try
                {
                    WindowInfoTypeConverter converter = new WindowInfoTypeConverter();
                    doc.LoadXml(value as string);
                    XmlNodeList nodes = doc.SelectNodes("/Windows/Info");
                    if (nodes == null)
                        return list;
                    foreach (XmlElement element in nodes)
                    {
                        try
                        {
                            WindowInfo wi = converter.ConvertFrom(element.OuterXml) as WindowInfo;
                            if (element.HasAttribute("window"))
                                list.Info.Add((WindowType)Convert.ToUInt32(element.GetAttribute("window")), wi);
                        }
                        catch (Exception)
                        {
                            // Ignore any values that are causing trouble
                        }
                    }
                }
                catch (Exception)
                {
                    // return anything we found
                }
                return list;
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return ((WindowInfoList)value).ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    public class WindowInfoTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                Rectangle rcSize = new Rectangle(0, 0, 1024, 768);
                bool bMaximized = false;
                bool bAutoShow = false;
                int[] splitPositions = null;

                XmlDocument doc = new XmlDocument();
                try
                {
                    doc.LoadXml(value as string);
                    XmlNode node = doc.SelectSingleNode("/Info/Size");
                    if (node != null)
                        rcSize = (Rectangle)TypeDescriptor.GetConverter(typeof(Rectangle)).ConvertFromString(node.InnerText);
                    node = doc.SelectSingleNode("/Info/Max");
                    if (node != null)
                        bMaximized = Convert.ToBoolean(node.InnerText);
                    node = doc.SelectSingleNode("/Info/Auto");
                    if (node != null)
                        bAutoShow = Convert.ToBoolean(node.InnerText);
                    XmlNodeList ints = doc.SelectNodes("/Info/Split/Num");
                    if (ints != null)
                    {
                        splitPositions = new int[ints.Count];
                        for (int i = 0; i < ints.Count; i++)
                            splitPositions[i] = Convert.ToInt32(ints[i].InnerText);
                    }
                }
                catch (Exception)
                {
                    // Ignore any values that are causing trouble
                }
                return new WindowInfo(rcSize, bMaximized, bAutoShow, splitPositions);
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return ((WindowInfo)value).ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    public class ShortcutsTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                Shortcuts shortcuts = new Shortcuts();

                XmlDocument doc = new XmlDocument();
                try
                {
                    shortcuts.ShortcutDict = new Dictionary<ShortcutKeys, InputOption>();

                    doc.LoadXml((string)value);
                    foreach (XmlElement eShortcut in doc.SelectNodes("/shortcuts/action"))
                    {
                        Action action = (Action)Convert.ToInt32(eShortcut.GetAttribute("n"));
                        InputOption input = new InputOption(action);
                        if (eShortcut.HasAttribute("g"))
                            input.Global = (eShortcut.GetAttribute("g") == "1");
                        else
                            input.Global = false;
                        List<ShortcutKeys> skList = new List<ShortcutKeys>();
                        foreach (XmlNode nodeKeys in eShortcut.SelectNodes("keys"))
                        {
                            List<Keys> keys = new List<Keys>();
                            foreach (string strKey in nodeKeys.InnerText.Split(','))
                                keys.Add((Keys)Convert.ToInt32(strKey));
                            skList.Add(new ShortcutKeys(keys.ToArray()));
                        }
                        foreach (ShortcutKeys sk in skList)
                        {
                            if (!shortcuts.ShortcutDict.ContainsKey(sk))
                                shortcuts.ShortcutDict.Add(sk, input);
                        }
                        input.Input = skList.ToArray();
                    }
                    return shortcuts;
                }
                catch (Exception)
                {
                    return shortcuts;
                }

            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return ((Shortcuts)value).ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    public class MRUFileListTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                string[] args = ((string)value).Split('|');
                MRUFileList list = new MRUFileList(args.Length);

                foreach (string s in args)
                    list.Paths.Add(s);

                return list;
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return ((MRUFileList)value).ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    [TypeConverter(typeof(MRUFileListTypeConverter))]
    public class MRUFileList
    {
        public List<string> Paths;

        public MRUFileList()
        {
            Paths = new List<string>();
        }

        public MRUFileList(int iSize)
        {
            Paths = new List<string>(iSize);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            string strPipe = "";
            foreach (string strPath in Paths)
            {
                sb.AppendFormat("{0}{1}", strPipe, strPath);
                strPipe = "|";
            }

            return sb.ToString();
        }
    }

    public class InputOptionTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                return InputOption.FromString((string)value);
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return ((InputOption)value).ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    [TypeConverter(typeof(InputOptionTypeConverter))]
    public class InputOption
    {
        public Action Action;
        public ShortcutKeys[] Input;
        public bool Global;

        public InputOption()
        {
            Action = WhereAreWe.Action.None;
            Global = false;
            ClearKeys();
        }

        public InputOption(Action action)
        {
            Action = action;
            Global = false;
            ClearKeys();
        }

        public bool HasKeys
        {
            get
            {
                if (Input == null)
                    return false;
                foreach (ShortcutKeys keys in Input)
                {
                    if (keys != null && keys.Length > 0)
                        return true;
                }
                return false;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}", (int)Action);
            foreach (ShortcutKeys keys in Input)
            {
                sb.Append(":");
                string strComma = "";
                foreach (Keys key in keys.Keys)
                {
                    sb.AppendFormat("{0}{1}", strComma, (int)key);
                    strComma = ",";
                }
            }
            if (Global)
                sb.Append(":G");
            return sb.ToString();
        }

        public void ClearKeys()
        {
            Input = new ShortcutKeys[2];
            Input[0] = new ShortcutKeys();
            Input[1] = new ShortcutKeys();
        }

        public void ClearKey(int iIndex)
        {
            if (Input == null)
                return;
            if (iIndex >= Input.Length)
                return;
            Input[iIndex] = new ShortcutKeys();
        }

        public void ClearKeys(int iIndex)
        {
            if (iIndex < Input.Length)
                Input[iIndex] = new ShortcutKeys();
        }

        public static InputOption FromString(string str)
        {
            // Example:  "1:65,66:67,68:G"
            InputOption input = new InputOption();

            string[] args = ((string)str).Split(':');
            if (args.Length < 1)
                return input;

            int iConvert;

            if (!Int32.TryParse(args[0], out iConvert))
                return input;

            input.Action = (Action)iConvert;

            int iKeyList = 1;
            int iMaxPerAction = 2;
            List<ShortcutKeys> shortcuts = new List<ShortcutKeys>(iMaxPerAction);

            while (iKeyList < args.Length)
            {
                if (args[iKeyList].ToUpper() == "G")
                {
                    input.Global = true;
                    iKeyList++;
                    continue;
                }
                if (shortcuts.Count >= iMaxPerAction)
                {
                    iKeyList++;
                    continue;
                }
                string[] strKeys = args[iKeyList].Split(',');
                List<Keys> keys = new List<Keys>(strKeys.Length);
                foreach (string strKey in strKeys)
                {
                    if (Int32.TryParse(strKey, out iConvert))
                        keys.Add((Keys)iConvert);
                }
                shortcuts.Add(new ShortcutKeys(keys.ToArray()));
                iKeyList++;
            }

            if (shortcuts.Count < 2)
                shortcuts.Add(new ShortcutKeys());
            input.Input = shortcuts.ToArray();

            return input;
        }

        public bool MatchesKeysDown(bool[] keysDown)
        {
            foreach (ShortcutKeys keys in Input)
            {
                bool bMatch = true;
                for (int i = 0; i < keysDown.Length; i++)
                {
                    if (keys.Keys.Contains((Keys)i))
                    {
                        if (keysDown[i])
                            continue;
                        else
                        {
                            bMatch = false;
                            break;
                        }
                    }
                    else
                    {
                        if (!keysDown[i])
                            continue;
                        else
                        {
                            bMatch = false;
                            break;
                        }
                    }
                }
                if (bMatch)
                    return true;
            }
            return false;
        }
    }

    [TypeConverter(typeof(WindowInfoTypeConverter))]
    public class WindowInfo
    {
        public Rectangle NormalSize;
        public bool Maximized;
        public bool AutoShow;
        public int[] SplitPositions;

        public static WindowInfo Empty { get { return new WindowInfo(Rectangle.Empty); } }
        public bool IsEmpty { get { return NormalSize == Rectangle.Empty; } }

        public WindowInfo(Rectangle rcNormal, bool bMaximized = false, bool bAutoShow = false, int[] splitPositions = null)
        {
            NormalSize = rcNormal;
            Maximized = bMaximized;
            AutoShow = bAutoShow;
            SplitPositions = splitPositions;
        }

        public WindowInfo(Form form, bool bAutoShow = false, params int[] positions)
        {
            SetFromForm(form, bAutoShow, positions);
        }

        public WindowInfo Clone()
        {
            return new WindowInfo(NormalSize, Maximized, AutoShow, SplitPositions);
        }

        public void SetFromForm(Form form, bool bAutoShow = false, params int[] positions)
        {
            Maximized = form.WindowState == FormWindowState.Maximized;
            NormalSize = Maximized ? form.RestoreBounds : new Rectangle(form.Location, form.Size);
            AutoShow = bAutoShow;
            if ((positions == null || positions.Length == 0) && form is HackerBasedForm)
                SplitPositions = ((HackerBasedForm)form).Splitters;
            else
                SplitPositions = positions;
        }

        public string ToString(WindowType type)
        {
            XElement element = GetElement();
            element.Add(new XAttribute("window", (int)type));
            return element.ToString(SaveOptions.DisableFormatting);
        }

        public override string ToString()
        {
            return GetElement().ToString(SaveOptions.DisableFormatting);
        }

        private XElement GetElement()
        {
            XElement ints = new XElement("Split");
            if (SplitPositions != null)
                foreach (int i in SplitPositions)
                    ints.Add(new XElement("Num", i));

            XElement xml = new XElement("Info", new XElement("Size", TypeDescriptor.GetConverter(typeof(Rectangle)).ConvertToString(NormalSize)),
                new XElement("Max", Convert.ToString(Maximized)),
                new XElement("Auto", Convert.ToString(AutoShow)),
                ints);

            return xml;
        }
    }

    [Serializable, TypeConverter(typeof(ExpandSizesTypeConverter))]
    public struct ExpandSizes
    {
        public int Top;
        public int Bottom;
        public int Left;
        public int Right;

        public ExpandSizes(int top, int bottom, int left, int right)
        {
            Top = top;
            Bottom = bottom;
            Left = left;
            Right = right;
        }

        public ExpandSizes(ExpandSizes copy)
        {
            Top = copy.Top;
            Bottom = copy.Bottom;
            Left = copy.Left;
            Right = copy.Right;
        }

        public int WidthDelta
        {
            get { return Right + Left; }
        }

        public int HeightDelta
        {
            get { return Bottom + Top; }
        }

        public bool IsEmpty
        {
            get { return (Top == 0 && Bottom == 0 && Left == 0 && Right == 0); }
        }

        public bool AllEqual
        {
            get { return Top == Bottom && Top == Left && Top == Right; }
        }

        public override string ToString()
        {
            return String.Format("{0},{1},{2},{3}", Top, Bottom, Left, Right);
        }

        public static ExpandSizes FromString(string str)
        {
            ExpandSizes sizes = new ExpandSizes(0, 0, 0, 0);

            if (str == null)
                return sizes;

            string[] numbers = str.Split(',');
            if (numbers.Length < 4)
                return sizes;

            int iTemp = 0;
            if (Int32.TryParse(numbers[0], out iTemp))
                sizes.Top = iTemp;
            if (Int32.TryParse(numbers[1], out iTemp))
                sizes.Bottom = iTemp;
            if (Int32.TryParse(numbers[2], out iTemp))
                sizes.Left = iTemp;
            if (Int32.TryParse(numbers[3], out iTemp))
                sizes.Right = iTemp;

            return sizes;
        }
    }

    public class ExpandSizesTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
                return ExpandSizes.FromString(value as string);
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return ((ExpandSizes)value).ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    [TypeConverter(typeof(ShortcutsTypeConverter))]
    public class Shortcuts
    {
        public Dictionary<ShortcutKeys, InputOption> ShortcutDict;

        public InputOption[] Commands
        {
            get
            {
                List<InputOption> cmds = new List<InputOption>();
                for (Action action = Action.FirstMenuItem; action < Action.Last; action++)
                {
                    if (action == Action.None || action == Action.LastMenuItem)
                        continue;
                    bool bExists = false;
                    foreach (InputOption existingIO in ShortcutDict.Values)
                    {
                        if (existingIO.Action == action)
                        {
                            cmds.Add(existingIO);
                            bExists = true;
                            break;
                        }
                    }
                    if (!bExists)
                        cmds.Add(new InputOption(action));
                }
                return cmds.ToArray();
            }
        }

        public Shortcuts()
        {
            ShortcutDict = new Dictionary<ShortcutKeys, InputOption>();
        }

        public bool IsEmpty { get { return Commands == null || Commands.Length == 0 || ShortcutDict == null || ShortcutDict.Values.Count == 0; } }

        public void Add(InputOption input)
        {
            foreach (ShortcutKeys keys in input.Input)
                ShortcutDict[keys] = input;
        }

        public void Add(Action action, params Keys[] keys)
        {
            InputOption input = new InputOption(action);
            input.Global = false;
            input.Input = new ShortcutKeys[2];
            input.Input[0] = new ShortcutKeys(keys);
            input.Input[1] = new ShortcutKeys();
            Add(input);
        }

        public void AddTwo(Action action, Keys[] keys1, Keys[] keys2)
        {
            InputOption input = new InputOption(action);
            input.Global = false;
            input.Input = new ShortcutKeys[2];
            input.Input[0] = new ShortcutKeys(keys1);
            input.Input[1] = new ShortcutKeys(keys2);
            Add(input);
        }

        public override string ToString() { return ToString(true); }

        public string ToString(bool bFullDocument)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = bFullDocument ? new XmlWriterSettings() : new XmlWriterSettings { ConformanceLevel = ConformanceLevel.Fragment };

            using (XmlWriter writer = XmlWriter.Create(sb, settings))
            {
                if (bFullDocument)
                    writer.WriteStartDocument();

                writer.WriteStartElement("shortcuts");

                foreach (InputOption input in Commands)
                {
                    if (input.Input != null && input.HasKeys)
                    {
                        writer.WriteStartElement("action");
                        writer.WriteAttributeString("n", ((int)input.Action).ToString());
                        if (input.Global)
                            writer.WriteAttributeString("g", "1");
                        foreach (ShortcutKeys skKeys in input.Input)
                        {
                            if (skKeys != null && skKeys.Keys != null && skKeys.Keys.Length > 0)
                            {
                                writer.WriteStartElement("keys");
                                StringBuilder sbKeys = new StringBuilder();
                                foreach (Keys skKey in skKeys.Keys)
                                    sbKeys.AppendFormat("{0},", (int)skKey);
                                if (sbKeys.Length > 0)
                                    sbKeys.Remove(sbKeys.Length - 1, 1);
                                writer.WriteString(sbKeys.ToString());
                                writer.WriteEndElement();
                            }
                        }
                        writer.WriteEndElement();
                    }
                }

                if (bFullDocument)
                    writer.WriteEndDocument();
            }
            return sb.ToString();
        }

        public InputOption ActionForDialogKey(Keys key)
        {
            Keys keyLast = (Keys)((int)key & 0xff);     // The key minus any of the Control/Win/Shift/Alt modifiers
            List<Keys> keys = new List<Keys>(4);
            keys.Add(keyLast);
            if ((key & Keys.Control) == Keys.Control)
                keys.Add(Keys.LControlKey);
            if ((key & Keys.Shift) == Keys.Shift)
                keys.Add(Keys.LShiftKey);
            if ((key & Keys.Alt) == Keys.Alt)
                keys.Add(Keys.LMenu);

            ShortcutKeys sc = new ShortcutKeys(keys.ToArray());
            if (ShortcutDict.ContainsKey(sc))
                return ShortcutDict[sc];
            return null;
        }

        public InputOption InputOptionForKeys(Keys keyLast, bool[] keysDown)
        {
            List<Keys> keys = new List<Keys>();
            for (int i = 0; i < keysDown.Length; i++)
                if (keysDown[i])
                    keys.Add((Keys)i);

            return InputOptionForKeys(keyLast, keys.ToArray());
        }

        public InputOption InputOptionForKeys(Keys keyLast, Keys[] keys)
        {
            foreach (ShortcutKeys scKeys in ShortcutKeys.GetCombinations(keyLast, keys))
            {
                if (ShortcutDict.ContainsKey(scKeys))
                {
                    if (ModifiersMatch(scKeys))
                        return ShortcutDict[scKeys];
                }
            }

            return null;
        }

        private bool ModifiersMatch(ShortcutKeys sk)
        {
            // Make sure the shortcut for, e.g. "F4" wasn't hit by pushing "Alt+F4" or similar
            bool bAlt = false;
            bool bControl = false;
            bool bShift = false;
            bool bWin = false;
            foreach (Keys key in sk.Keys)
            {
                switch (key)
                {
                    case Keys.LMenu:
                    case Keys.RMenu:
                    case Keys.Menu:
                        bAlt = true;
                        break;
                    case Keys.LControlKey:
                    case Keys.RControlKey:
                    case Keys.Control:
                        bControl = true;
                        break;
                    case Keys.LShiftKey:
                    case Keys.RShiftKey:
                    case Keys.Shift:
                        bShift = true;
                        break;
                    case Keys.LWin:
                    case Keys.RWin:
                        bWin = true;
                        break;
                }
            }
            if (!bAlt && NativeMethods.IsAltDown())
                return false;
            if (!bShift && NativeMethods.IsShiftDown())
                return false;
            if (!bControl && NativeMethods.IsControlDown())
                return false;
            if (!bWin && NativeMethods.IsWinDown())
                return false;

            return true;
        }

        public Keys[] KeysWanted
        {
            get
            {
                List<Keys> keysWanted = new List<Keys>();
                foreach (ShortcutKeys keys in ShortcutDict.Keys)
                {
                    foreach (Keys key in keys.Keys)
                        if (!keysWanted.Contains(key))
                            keysWanted.Add(key);
                }

                return keysWanted.ToArray();
            }
        }
    }

    public class ColumnHeaderListTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                ColumnHeaderList list = new ColumnHeaderList(value as string);
                return list;
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return ((ColumnHeaderList)value).ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    [TypeConverter(typeof(ColumnHeaderListTypeConverter))]
    public class ColumnHeaderList
    {
        public List<int> Widths;
        public List<int> Order;

        public ColumnHeaderList(ListView lv)
        {
            Widths = new List<int>(lv.Columns.Count);
            Order = new List<int>(lv.Columns.Count);
            for (int iCol = 0; iCol < lv.Columns.Count; iCol++)
            {
                Widths.Add(lv.Columns[iCol].Width);
                Order.Add(lv.Columns[iCol].DisplayIndex);
            }
        }

        public ColumnHeaderList(string str)
        {
            string[] args = str.Split(',');
            Widths = new List<int>(args.Length);
            Order = new List<int>(args.Length);
            for (int i = 0; i < args.Length; i += 2)
            {
                int iTest = 0;
                if (Int32.TryParse(args[i], out iTest))
                    Widths.Add(iTest);
                if (Int32.TryParse(args[i + 1], out iTest))
                    Order.Add(iTest);
            }
        }

        public ColumnHeaderList(int i)
        {
            Widths = new List<int>(i);
            Order = new List<int>(i);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            int iOrderDefault = 0;
            for (int i = 0; i < Widths.Count; i++)
            {
                if (i >= Order.Count)
                    sb.AppendFormat("{0},{1},", Widths[i], iOrderDefault);
                else
                    sb.AppendFormat("{0},{1},", Widths[i], Order[i]);
                iOrderDefault++;
            }
            return Global.Trim(sb).ToString();
        }
    }

    public class SpellHotkeyCollectionTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                SpellHotkeyCollection list = new SpellHotkeyCollection(value as string);
                return list;
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return ((SpellHotkeyCollection)value).ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    public class SpellHotkeyListTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                SpellHotkeyList list = new SpellHotkeyList(value as string);
                return list;
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return ((SpellHotkeyList)value).ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    public class SpellHotkey
    {
        public enum HKCharacter
        {
            Unknown = -1,
            None = 0,
            First = None,
            Character1,
            Character2,
            Character3,
            Character4,
            Character5,
            Character6,
            Character7,
            Character8,
            AllArcane,
            AllCleric,
            AllDruid,
            AllCharacters,
            CurrentCharacter,
            Last = CurrentCharacter
        }

        public static string[] CharacterStrings(HKCharacter hkcFirst, HKCharacter hkcLast)
        {
            string[] result = new string[hkcLast - hkcFirst + 1];
            for (HKCharacter hkc = hkcFirst; hkc <= hkcLast; hkc++)
                result[hkc - hkcFirst] = CharacterString(hkc);
            return result;
        }

        public static string CharacterString(HKCharacter hkc)
        {
            switch (hkc)
            {
                case HKCharacter.None: return "Do Nothing";
                case HKCharacter.Character1: return "Set Character 1 Ready Spell";
                case HKCharacter.Character2: return "Set Character 2 Ready Spell";
                case HKCharacter.Character3: return "Set Character 3 Ready Spell";
                case HKCharacter.Character4: return "Set Character 4 Ready Spell";
                case HKCharacter.Character5: return "Set Character 5 Ready Spell";
                case HKCharacter.Character6: return "Set Character 6 Ready Spell";
                case HKCharacter.Character7: return "Set Character 7 Ready Spell";
                case HKCharacter.Character8: return "Set Character 8 Ready Spell";
                case HKCharacter.AllArcane: return "Set All Arcane Casters' Ready Spells";
                case HKCharacter.AllCleric: return "Set All Cleric Casters' Ready Spells";
                case HKCharacter.AllDruid: return "Set All Druid Casters' Ready Spells";
                case HKCharacter.AllCharacters: return "Set All Characters' Ready Spells";
                case HKCharacter.CurrentCharacter: return "Cast with Current Character";
                default: return "Unknown";
            }
        }

        public int Key;
        public HKCharacter Character;
        public int SpellIndex;

        public SpellHotkey()
        {
            Key = -1;
            Character = HKCharacter.None;
            SpellIndex = -1;
        }

        public SpellHotkey(int key, HKCharacter character, int spell)
        {
            Key = key;
            Character = character;
            SpellIndex = spell;
        }

        public SpellHotkey(int key, ComboBox comboType, ComboBox comboSpell)
        {
            Key = key;
            Character = comboType.SelectedItem == null ? HKCharacter.None : (comboType.SelectedItem as HKCharTag).HKChar;
            SpellIndex = (comboSpell == null || comboSpell.SelectedItem == null ? -1 : (comboSpell.SelectedItem as HKSpellTag).HKSpell.BasicIndex);
        }
    }

    [TypeConverter(typeof(SpellHotkeyCollectionTypeConverter))]
    public class SpellHotkeyCollection
    {
        public Dictionary<GameNames, SpellHotkeyList> Hotkeys;

        public SpellHotkeyCollection()
        {
            Hotkeys = new Dictionary<GameNames, SpellHotkeyList>();
        }

        public static Dictionary<GameNames, SpellHotkeyList> FromXml(XmlDocument xml)
        {
            Dictionary<GameNames, SpellHotkeyList> keys = new Dictionary<GameNames, SpellHotkeyList>();
            XmlNodeList listGames = xml.SelectNodes("/SpellHotkeys/hotkeys");
            foreach (XmlElement eGame in listGames)
            {
                SpellHotkeyList shkList = new SpellHotkeyList(eGame.InnerText);
                keys.Add(shkList.SelectedGame, shkList);
            }
            return keys;
        }

        public SpellHotkeyCollection(string strXml)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(strXml);
                Hotkeys = FromXml(xml);
            }
            catch (Exception ex)
            {
                Hotkeys = new Dictionary<GameNames, SpellHotkeyList>();
                Global.LogError("Could not load SpellHotkeyCollection: {0}", ex.Message);
            }
        }

        public string GetXml()
        {
            StringBuilder sb = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sb);
            writer.WriteStartDocument();
            writer.WriteStartElement("SpellHotkeys");
            foreach (GameNames game in Hotkeys.Keys)
            {
                writer.WriteStartElement("hotkeys");
                writer.WriteAttributeString("game", ((int)game).ToString());
                writer.WriteString(Hotkeys[game].ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
            return sb.ToString();
        }

        public override string ToString()
        {
            return GetXml();
        }
    }

    [TypeConverter(typeof(SpellHotkeyListTypeConverter))]
    public class SpellHotkeyList
    {
        public GameNames SelectedGame;
        public Dictionary<int, SpellHotkey> Hotkeys;

        public SpellHotkeyList()
        {
            SelectedGame = GameNames.None;
            Hotkeys = new Dictionary<int, SpellHotkey>();
        }

        public SpellHotkey GetHotkey(int iKey) { return Hotkeys.ContainsKey(iKey) ? Hotkeys[iKey] : null; }

        public SpellHotkeyList(string str)
        {
            SelectedGame = GameNames.None;
            Hotkeys = new Dictionary<int, SpellHotkey>();

            string[] args = str.Split(',');
            if (args.Length < 1)
                return;

            int iTemp;
            if (Int32.TryParse(args[0], out iTemp))
                SelectedGame = (GameNames)iTemp;

            for (int i = 1; i < args.Length; i += 3)
            {
                SpellHotkey hk = new SpellHotkey();

                if (i > args.Length - 3)
                    return;

                if (Int32.TryParse(args[i], out iTemp))
                    hk.Key = iTemp;
                if (Int32.TryParse(args[i + 1], out iTemp))
                    hk.Character = (SpellHotkey.HKCharacter)iTemp;
                if (Int32.TryParse(args[i + 2], out iTemp))
                    hk.SpellIndex = iTemp;

                if (!Hotkeys.ContainsKey(hk.Key))
                    Hotkeys.Add(hk.Key, hk);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0},", (int)SelectedGame);
            foreach (SpellHotkey hk in Hotkeys.Values)
            {
                sb.AppendFormat("{0},{1},{2},", hk.Key, (int)hk.Character, hk.SpellIndex);
            }
            return Global.Trim(sb).ToString();
        }
    }

    [TypeConverter(typeof(WindowTypeListTypeConverter))]
    public class WindowTypeList
    {
        public WindowType[] Types;

        public WindowTypeList()
        {
        }

        public WindowTypeList(WindowType[] types)
        {
            Types = types;
        }

        public WindowTypeList(string types)
        {
            string[] values = types.Split(',');
            Types = new WindowType[values.Length];
            for(int i = 0; i < values.Length; i++)
            {
                int iType = (int) WindowType.None;
                Int32.TryParse(values[i], out iType);
                Types[i] = (WindowType) iType;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(WindowType type in Types)
                sb.AppendFormat("{0},", (int)type);
            return Global.Trim(sb).ToString();
        }
    }

    public class WindowTypeListTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return value is string ? new WindowTypeList(value as string) : base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return destinationType == typeof(string) ? ((WindowTypeList)value).ToString() : base.ConvertTo(context, culture, value, destinationType);
        }
    }

    [TypeConverter(typeof(SquareStyleListTypeConverter))]
    public class SquareStyleList
    {
        public static ColorPattern DefaultSolid = new ColorPattern(Color.MidnightBlue, HatchStyle.Percent50);
        public static ColorPattern DefaultSpace = new ColorPattern(Color.MediumPurple, HatchStyle.Percent75);
        public static ColorPattern DefaultBorder = new ColorPattern(Color.DarkGray, HatchStyle.Percent90);
        public static ColorPattern DefaultSky = new ColorPattern(Color.LightSkyBlue, HatchStyle.Percent90);
        public static ColorPattern DefaultDark = new ColorPattern(Color.Black, HatchStyle.Percent25);
        public static ColorPattern DefaultDim = new ColorPattern(Color.DarkGray, HatchStyle.Percent20);
        public static ColorPattern DefaultDangerous = new ColorPattern(Color.Yellow, HatchStyle.Percent50);
        public static ColorPattern DefaultDangerousDark = new ColorPattern(Color.Goldenrod, HatchStyle.Percent50);
        public static ColorPattern DefaultAntiMagic = new ColorPattern(Color.Red, HatchStyle.Percent25);
        public static ColorPattern DefaultAntiMagicDark = new ColorPattern(Color.Red, HatchStyle.Percent50);
        public static ColorPattern DefaultNoTeleport = new ColorPattern(Color.DeepSkyBlue, HatchStyle.Percent25);
        public static ColorPattern DefaultNoTeleportDark = new ColorPattern(Color.DeepSkyBlue, HatchStyle.Percent50);
        public static ColorPattern DefaultRegen = new ColorPattern(Color.SpringGreen, HatchStyle.Percent25);
        public static ColorPattern DefaultRegenDark = new ColorPattern(Color.SpringGreen, HatchStyle.Percent50);
        public static ColorPattern DefaultInaccessible = new ColorPattern(Color.FromArgb(192, 192, 192), HatchStyle.Percent50);

        public static bool IsDark(ColorPattern pattern)
        {
            return pattern.Equals(DefaultDark) || pattern.Equals(DefaultAntiMagicDark);
        }

        public enum Name
        {
            None,
            Solid,
            Sky,
            Space,
            Border
        }

        public Dictionary<Name, ColorPattern> List;

        public SquareStyleList()
        {
            List = new Dictionary<Name, ColorPattern>();
        }

        private void WriteIfExists(XmlWriter writer, Name name, string strName)
        {
            if (List.ContainsKey(name))
            {
                writer.WriteStartElement(strName);
                ColorPattern info = List[name];
                writer.WriteAttributeString("color", String.Format("{0:X8}", info.Color.ToArgb()));
                if (info.Pattern != HatchStyle.Percent90)
                    writer.WriteAttributeString("style", String.Format("{0}", (int)info.Pattern));
                writer.WriteEndElement();
            }
        }

        public override string ToString()
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter writer = new XmlTextWriter(sw);
            writer.WriteStartDocument();
            writer.WriteStartElement("Items");
            foreach (KeyValuePair<Name, ColorPattern> pair in List)
            {
                WriteIfExists(writer, Name.Solid, "Solid");
                WriteIfExists(writer, Name.Sky, "Sky");
                WriteIfExists(writer, Name.Space, "Space");
                WriteIfExists(writer, Name.Border, "Border");
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
            return sw.ToString();
        }

        public bool IsMatch(MapSquare square)
        {
            int color = square.Colors.Background.ToArgb();
            foreach (ColorPattern info in List.Values)
            {
                if (info.Color.ToArgb() == color && info.Pattern == square.Colors.BackgroundStyle)
                    return true;
            }
            return false;
        }

        public bool IsMatch(Name name, Color color, HatchStyle style)
        {
            if (!List.ContainsKey(name))
                return false;
            return (List[name].Color.ToArgb() == color.ToArgb() && List[name].Pattern == style);
        }

        public bool IsSolid(MapSquare square)
        {
            if (!List.ContainsKey(Name.Solid))
                return false;
            return (List[Name.Solid].Color.ToArgb() == square.Colors.Background.ToArgb() && List[Name.Solid].Pattern == square.Colors.BackgroundStyle);
        }

        public Color SolidColor { get { return List[Name.Solid].Color; } }
        public HatchStyle SolidPattern { get { return List[Name.Solid].Pattern; } }
        public Color SkyColor { get { return List[Name.Sky].Color; } }
        public HatchStyle SkyPattern { get { return List[Name.Sky].Pattern; } }
        public Color SpaceColor { get { return List[Name.Space].Color; } }
        public HatchStyle SpacePattern { get { return List[Name.Space].Pattern; } }
        public Color BorderColor { get { return List[Name.Border].Color; } }
        public HatchStyle BorderPattern { get { return List[Name.Border].Pattern; } }

        public static SquareStyleList Default
        {
            get
            {
                SquareStyleList list = new SquareStyleList();
                list.List.Add(Name.Border, DefaultBorder);
                list.List.Add(Name.Solid, DefaultSolid);
                list.List.Add(Name.Sky, DefaultSky);
                list.List.Add(Name.Space, DefaultSpace);
                return list;
            }
        }
    }

    public class SquareStyleListTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        private void AddFromNode(XmlDocument doc, SquareStyleList list, string strName, SquareStyleList.Name name)
        {
            XmlElement node = doc.SelectSingleNode("/Items/" + strName) as XmlElement;

            if (node != null)
            {
                HatchStyle style = HatchStyle.Percent90;
                if (node.HasAttribute("style"))
                    style = (HatchStyle)Convert.ToInt32(node.Attributes["style"].Value);
                list.List.Add(name, new ColorPattern(Color.FromArgb(Convert.ToInt32(node.Attributes["color"].Value, 16)), style));
            }
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                if ((string)value == "Default")
                    return SquareStyleList.Default;

                SquareStyleList list = new SquareStyleList();

                XmlDocument doc = new XmlDocument();
                try
                {
                    doc.LoadXml(value as string);
                    AddFromNode(doc, list, "Solid", SquareStyleList.Name.Solid);
                    AddFromNode(doc, list, "Sky", SquareStyleList.Name.Sky);
                    AddFromNode(doc, list, "Space", SquareStyleList.Name.Space);
                    AddFromNode(doc, list, "Border", SquareStyleList.Name.Border);
                }
                catch (Exception)
                {
                    return SquareStyleList.Default;
                }
                return list;
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return ((SquareStyleList)value).ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    [TypeConverter(typeof(DrawColorsTypeConverter))]
    public class DrawColors
    {
        public List<DrawColor> Blocks;
        public List<DrawColor> Lines;
        public List<DrawColor> Notes;

        public DrawColors()
        {
            Blocks = new List<DrawColor>(20);
            Lines = new List<DrawColor>(20);
            Notes = new List<DrawColor>(20);
        }

        public override string ToString()
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter writer = new XmlTextWriter(sw);
            writer.WriteStartDocument();
            writer.WriteStartElement("Colors");
            writer.WriteStartElement("Blocks");
            foreach (DrawColor dc in Blocks)
            {
                writer.WriteStartElement("Block");
                writer.WriteAttributeString("color", String.Format("{0:X8}", dc.color.ToArgb()));
                writer.WriteAttributeString("style", String.Format("{0}", (byte)dc.styleBlock));
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteStartElement("Lines");
            foreach (DrawColor dc in Lines)
            {
                writer.WriteStartElement("Line");
                writer.WriteAttributeString("color", String.Format("{0:X8}", dc.color.ToArgb()));
                writer.WriteAttributeString("style", String.Format("{0}", (byte)dc.style));
                writer.WriteAttributeString("width", String.Format("{0}", (byte)dc.width));
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteStartElement("Notes");
            foreach (DrawColor dc in Notes)
            {
                writer.WriteStartElement("Note");
                writer.WriteAttributeString("color", String.Format("{0:X8}", dc.color.ToArgb()));
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
            return sw.ToString();
        }

        public static DrawColors Default
        {
            get
            {
                DrawColors list = new DrawColors();
                list.Blocks.Add(new DrawColor(Color.Black, HatchStyle.Percent90));
                list.Blocks.Add(new DrawColor(Color.Red, HatchStyle.Percent90));
                list.Blocks.Add(new DrawColor(Color.Green, HatchStyle.Percent90));
                list.Blocks.Add(new DrawColor(Color.Blue, HatchStyle.Percent90));
                list.Blocks.Add(new DrawColor(Color.Gray, HatchStyle.Percent90));
                list.Blocks.Add(new DrawColor(Color.Orange, HatchStyle.Percent90));
                list.Blocks.Add(new DrawColor(Color.Purple, HatchStyle.Percent90));
                list.Blocks.Add(new DrawColor(Color.Cyan, HatchStyle.Percent90));
                list.Blocks.Add(new DrawColor(Color.Magenta, HatchStyle.Percent90));
                list.Blocks.Add(new DrawColor(Color.Yellow, HatchStyle.Percent90));
                list.Blocks.Add(new DrawColor(Color.Black, HatchStyle.Percent50));
                list.Blocks.Add(new DrawColor(Color.Red, HatchStyle.Percent50));
                list.Blocks.Add(new DrawColor(Color.Green, HatchStyle.Percent50));
                list.Blocks.Add(new DrawColor(Color.Blue, HatchStyle.Percent50));
                list.Blocks.Add(new DrawColor(Color.Gray, HatchStyle.Percent50));
                list.Blocks.Add(new DrawColor(Color.Orange, HatchStyle.Percent50));
                list.Blocks.Add(new DrawColor(Color.Purple, HatchStyle.Percent50));
                list.Blocks.Add(new DrawColor(Color.Cyan, HatchStyle.Percent50));
                list.Blocks.Add(new DrawColor(Color.Magenta, HatchStyle.Percent50));
                list.Blocks.Add(new DrawColor(Color.Yellow, HatchStyle.Percent50));
                list.Lines.Add(new DrawColor(Color.Black, DashStyle.Solid, 2));
                list.Lines.Add(new DrawColor(Color.Red, DashStyle.Solid, 2));
                list.Lines.Add(new DrawColor(Color.Green, DashStyle.Solid, 2));
                list.Lines.Add(new DrawColor(Color.Blue, DashStyle.Solid, 2));
                list.Lines.Add(new DrawColor(Color.Gray, DashStyle.Solid, 2));
                list.Lines.Add(new DrawColor(Color.Orange, DashStyle.Solid, 2));
                list.Lines.Add(new DrawColor(Color.Purple, DashStyle.Solid, 2));
                list.Lines.Add(new DrawColor(Color.Cyan, DashStyle.Solid, 2));
                list.Lines.Add(new DrawColor(Color.Magenta, DashStyle.Solid, 2));
                list.Lines.Add(new DrawColor(Color.Yellow, DashStyle.Solid, 2));
                list.Lines.Add(new DrawColor(Color.Black, DashStyle.Dot, 2));
                list.Lines.Add(new DrawColor(Color.Red, DashStyle.Dot, 2));
                list.Lines.Add(new DrawColor(Color.Green, DashStyle.Dot, 2));
                list.Lines.Add(new DrawColor(Color.Blue, DashStyle.Dot, 2));
                list.Lines.Add(new DrawColor(Color.Gray, DashStyle.Dot, 2));
                list.Lines.Add(new DrawColor(Color.Orange, DashStyle.Dot, 2));
                list.Lines.Add(new DrawColor(Color.Purple, DashStyle.Dot, 2));
                list.Lines.Add(new DrawColor(Color.Cyan, DashStyle.Dot, 2));
                list.Lines.Add(new DrawColor(Color.Magenta, DashStyle.Dot, 2));
                list.Lines.Add(new DrawColor(Color.Yellow, DashStyle.Dot, 2));
                list.Notes.Add(new DrawColor(Color.Black));
                list.Notes.Add(new DrawColor(Color.Red));
                list.Notes.Add(new DrawColor(Color.Green));
                list.Notes.Add(new DrawColor(Color.Wheat));
                list.Notes.Add(new DrawColor(Color.Tan));
                list.Notes.Add(new DrawColor(Color.Silver));
                list.Notes.Add(new DrawColor(Color.Blue));
                list.Notes.Add(new DrawColor(Color.CornflowerBlue));
                list.Notes.Add(new DrawColor(Color.Aquamarine));
                list.Notes.Add(new DrawColor(Color.Teal));
                list.Notes.Add(new DrawColor(Color.Chartreuse));
                list.Notes.Add(new DrawColor(Color.Olive));
                list.Notes.Add(new DrawColor(Color.Yellow));
                list.Notes.Add(new DrawColor(Color.Goldenrod));
                list.Notes.Add(new DrawColor(Color.Gray));
                list.Notes.Add(new DrawColor(Color.HotPink));
                list.Notes.Add(new DrawColor(Color.DarkViolet));
                list.Notes.Add(new DrawColor(Color.Orange));
                list.Notes.Add(new DrawColor(Color.Plum));
                list.Notes.Add(new DrawColor(Color.Maroon));
                return list;
            }
        }
    }

    public class DrawColorsTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                try
                {
                    if ((string)value == "Default")
                        return DrawColors.Default;

                    DrawColors colors = new DrawColors();

                    XmlDocument doc = new XmlDocument();
                    try
                    {
                        doc.LoadXml(value as string);
                        XmlNodeList blocks = doc.SelectNodes("/Colors/Blocks/Block");
                        foreach (XmlElement node in blocks)
                        {
                            if (node.HasAttribute("color") && node.HasAttribute("style"))
                                colors.Blocks.Add(new DrawColor(Color.FromArgb(Convert.ToInt32(node.Attributes["color"].Value, 16)),
                                    (HatchStyle)Convert.ToInt32(node.Attributes["style"].Value)));
                        }
                        XmlNodeList lines = doc.SelectNodes("/Colors/Lines/Line");
                        foreach (XmlElement node in lines)
                        {
                            if (node.HasAttribute("color") && node.HasAttribute("style") && node.HasAttribute("width"))
                                colors.Lines.Add(new DrawColor(Color.FromArgb(Convert.ToInt32(node.Attributes["color"].Value, 16)),
                                    (DashStyle)Convert.ToInt32(node.Attributes["style"].Value), Convert.ToByte(node.Attributes["width"].Value)));
                        }
                        XmlNodeList notes = doc.SelectNodes("/Colors/Notes/Note");
                        foreach (XmlElement node in notes)
                        {
                            if (node.HasAttribute("color"))
                                colors.Notes.Add(new DrawColor(Color.FromArgb(Convert.ToInt32(node.Attributes["color"].Value, 16))));
                        }
                    }
                    catch (Exception)
                    {
                        return SquareStyleList.Default;
                    }
                    return colors;
                }
                catch (Exception)
                {
                    return DrawColors.Default;
                }
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return ((DrawColors)value).ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    [TypeConverter(typeof(GameStringsTypeConverter))]
    public class GameStrings
    {
        protected Dictionary<GameNames, string> m_strings;

        public Dictionary<GameNames, string> Strings { get { return m_strings; } }

        public GameStrings()
        {
            m_strings = new Dictionary<GameNames, string>();
        }

        public static GameStrings Clone(GameStrings gsCopy)
        {
            return new GameStringsTypeConverter().ConvertFrom(gsCopy.ToString()) as GameStrings;
        }

        public void Set(GameNames game, string str)
        {
            if (m_strings.ContainsKey(game))
                m_strings[game] = str;
            else
                m_strings.Add(game, str);
        }

        public void Remove(GameNames game)
        {
            if (m_strings.ContainsKey(game))
                m_strings.Remove(game);
        }

        public bool ContainsKey(GameNames game) { return m_strings != null && m_strings.ContainsKey(game); }

        public string Get(GameNames game, string strDefault = null)
        {
            if (m_strings.ContainsKey(game))
                return m_strings[game];
            return strDefault;
        }

        public string Combine(GameNames game, string strOverrideDir, string strFile)
        {
            if (String.IsNullOrWhiteSpace(strOverrideDir))
                return Path.Combine(Get(game, String.Empty), strFile);
            return Path.Combine(strOverrideDir, strFile);
        }

        public int Count { get { return m_strings == null ? 0 : m_strings.Count; } }

        public IEnumerable<string> Values { get { return m_strings == null ? null : m_strings.Values; } }

        public static GameStrings Default
        {
            get
            {
                GameStrings gs = new GameStrings();
                return gs;
            }
        }

        public override string ToString()
        {
            if (m_strings == null)
                return String.Empty;

            StringBuilder sb = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sb);
            writer.WriteStartDocument();
            writer.WriteStartElement("Strings");
            foreach (GameNames game in m_strings.Keys)
            {
                writer.WriteStartElement("String");
                writer.WriteStartAttribute("game");
                writer.WriteValue(Games.GameEnumString(game));
                writer.WriteEndAttribute();
                writer.WriteString(m_strings[game]);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();

            writer.Close();

            return sb.ToString();
        }
    }

    public class GameStringsTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                try
                {
                    if ((string)value == "Default")
                        return GameStrings.Default;

                    GameStrings gs = new GameStrings();

                    XmlDocument doc = new XmlDocument();
                    try
                    {
                        doc.LoadXml(value as string);
                        XmlNodeList strings = doc.SelectNodes("/Strings/String");
                        foreach (XmlElement node in strings)
                        {
                            if (node.HasAttribute("game"))
                                gs.Set(Games.GameEnum(node.Attributes["game"].Value), node.InnerText);
                        }
                    }
                    catch (Exception)
                    {
                        return GameStrings.Default;
                    }
                    return gs;
                }
                catch (Exception)
                {
                    return GameStrings.Default;
                }
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return ((GameStrings)value).ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    [TypeConverter(typeof(DropTrashOptionsTypeConverter))]
    public class DropTrashOptions
    {
        public TrashCriteria.DropIf Criteria;
        public int Gold;
        public int Bonus;
        public int Class;
        public int ItemType;
        public int Material;
        public bool AllCharacters;

        public Dictionary<GameNames, HashSet<string>> CustomTrash;

        public DropTrashOptions()
        {
            Criteria = TrashCriteria.DropIf.LessThanGold;
            Gold = 500;
            Bonus = 0;
            Class = 0;
            ItemType = 0;
            Material = 0;
            AllCharacters = true;
            CustomTrash = new Dictionary<GameNames, HashSet<string>>();
        }

        public DropTrashOptions(TrashCriteria.DropIf criteria, int iGold, int iBonus, int iClass, int iType, int iMaterial, bool bAll, Dictionary<GameNames, HashSet<string>> custom)
        {
            Criteria = criteria;
            Gold = iGold;
            Bonus = iBonus;
            Class = iClass;
            ItemType = iType;
            Material = iMaterial;
            AllCharacters = bAll;
            CustomTrash = custom;
        }

        public bool IsCustomTrash(GameNames game, Item item)
        {
            if (CustomTrash == null || !CustomTrash.ContainsKey(game))
                return false;
            return CustomTrash[game].Contains(item.TrashIndex);
        }

        public static DropTrashOptions FromXml(string xml)
        {
            DropTrashOptions dt = new DropTrashOptions();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                XmlElement element = doc.SelectSingleNode("/DropTrash") as XmlElement;
                dt.Criteria = (TrashCriteria.DropIf)Convert.ToInt32(element.Attributes["criteria"].Value);
                dt.Gold = Convert.ToInt32(element.Attributes["gold"].Value);
                dt.Bonus = Convert.ToInt32(element.Attributes["bonus"].Value);
                dt.Class = Convert.ToInt32(element.Attributes["class"].Value);
                dt.ItemType = Convert.ToInt32(element.Attributes["itemtype"].Value);
                dt.Material = Convert.ToInt32(element.Attributes["material"].Value);
                dt.AllCharacters = Convert.ToInt32(element.Attributes["allchars"].Value) != 0;
                dt.CustomTrash = new Dictionary<GameNames, HashSet<string>>();
                XmlNodeList listGames = element.SelectNodes("custom");
                foreach (XmlElement eGame in listGames)
                {
                    int iGame = 0;
                    GameNames game = GameNames.None;
                    if (Int32.TryParse(eGame.GetAttribute("game"), out iGame))
                        game = (GameNames)iGame;

                    HashSet<string> items = new HashSet<string>();
                    XmlNodeList listItems = eGame.SelectNodes("item");
                    foreach (XmlElement eItem in listItems)
                        items.Add(eItem.GetAttribute("id"));
                    dt.CustomTrash.Add(game, items);
                }
            }
            catch (Exception ex)
            {
                Global.LogError("Invalid DropTrashOptions from string \"{0}\": {1}", xml, ex.Message);
            }
            return dt;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sb);
            writer.WriteStartDocument();
            writer.WriteStartElement("DropTrash");
            writer.WriteAttributeString("criteria", ((int)Criteria).ToString());
            writer.WriteAttributeString("gold", Gold.ToString());
            writer.WriteAttributeString("bonus", Bonus.ToString());
            writer.WriteAttributeString("class", Class.ToString());
            writer.WriteAttributeString("itemtype", ItemType.ToString());
            writer.WriteAttributeString("material", Material.ToString());
            writer.WriteAttributeString("allchars", AllCharacters ? "1" : "0");
            foreach (GameNames game in CustomTrash.Keys)
            {
                writer.WriteStartElement("custom");
                writer.WriteAttributeString("game", ((int)game).ToString());
                foreach (string str in CustomTrash[game])
                {
                    writer.WriteStartElement("item");
                    writer.WriteAttributeString("id", str);
                    writer.WriteEndElement();
                }
            }
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
            return sb.ToString();
        }
    }

    public class DropTrashOptionsTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                if ((string)value == "Default")
                    return new DropTrashOptions();

                return DropTrashOptions.FromXml(value as string);
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return ((DropTrashOptions)value).ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    [TypeConverter(typeof(FavoriteSpellsTypeConverter))]
    public class FavoriteSpells
    {
        public class GameChar
        {
            public GameNames Game;
            public string CharName;

            public GameChar(GameNames game, string charName)
            {
                Game = game;
                CharName = charName;
            }

            public override bool Equals(object obj)
            {
                GameChar gc = obj as GameChar;
                if (gc == null)
                    return false;

                return (gc.Game == Game && CharName == gc.CharName);
            }

            public override int GetHashCode()
            {
                return CharName.GetHashCode() ^ (int)Game;
            }

            public override string ToString()
            {
                return String.Format("{0},{1}", Games.ShortName(Game), CharName);
            }
        }

        public bool ShowFavorites;
        public Dictionary<GameChar, List<int>> Favorites;

        public FavoriteSpells()
        {
            ShowFavorites = true;
            Favorites = new Dictionary<GameChar, List<int>>();
        }

        public bool IsFavorite(GameNames game, string charName, int spell)
        {
            GameChar gc = new GameChar(game, charName);
            if (Favorites == null || !Favorites.ContainsKey(gc))
                return false;
            return Favorites[gc].Contains(spell);
        }

        public void AddFavorite(GameNames game, string charName, int spell)
        {
            GameChar gc = new GameChar(game, charName);
            if (!Favorites.ContainsKey(gc))
                Favorites.Add(gc, new List<int>());
            if (!Favorites[gc].Contains(spell))
                Favorites[gc].Add(spell);
        }

        public void SetFavorites(GameNames game, string charName, List<int> spells)
        {
            GameChar gc = new GameChar(game, charName);
            if (!Favorites.ContainsKey(gc))
                Favorites.Add(gc, spells);
            else
                Favorites[gc] = spells;
        }

        public List<int> GetFavorites(GameNames game, string charName)
        {
            GameChar gc = new GameChar(game, charName);
            if (charName == null || !Favorites.ContainsKey(gc))
                return new List<int>(0);
            return Favorites[gc];
        }

        public void RemoveFavorite(GameNames game, string charName, int spell)
        {
            GameChar gc = new GameChar(game, charName);
            if (!Favorites.ContainsKey(gc))
                return;
            if (!Favorites[gc].Contains(spell))
                return;
            Favorites[gc].Remove(spell);
        }

        public void RemoveFavorites(GameNames game, string charName)
        {
            GameChar gc = new GameChar(game, charName);
            if (!Favorites.ContainsKey(gc))
                return;
            Favorites[gc].Clear();
        }

        public static FavoriteSpells FromXml(string xml)
        {
            FavoriteSpells fs = new FavoriteSpells();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                XmlElement element = doc.SelectSingleNode("/Favorites") as XmlElement;
                fs.ShowFavorites = element.GetAttribute("show") != "0";
                fs.Favorites = new Dictionary<GameChar, List<int>>();
                XmlNodeList list = element.SelectNodes("list");
                foreach (XmlElement eGameChar in list)
                {
                    int iGame = 0;
                    GameNames game = GameNames.None;
                    if (Int32.TryParse(eGameChar.GetAttribute("game"), out iGame))
                        game = (GameNames)iGame;

                    string strName = eGameChar.GetAttribute("char");

                    if (game == GameNames.None || String.IsNullOrWhiteSpace(strName))
                        continue;

                    List<int> spells = new List<int>();
                    XmlNodeList listSpells = eGameChar.SelectNodes("spell");
                    foreach (XmlElement eSpell in listSpells)
                    {
                        int iSpell = -1;
                        if (Int32.TryParse(eSpell.GetAttribute("id"), out iSpell))
                        {
                            if (!spells.Contains(iSpell))
                                spells.Add(iSpell);
                        }
                    }
                    fs.Favorites.Add(new GameChar(game, strName), spells);
                }
            }
            catch (Exception ex)
            {
                Global.LogError("Invalid FavoriteSpells from string \"{0}\": {1}", xml, ex.Message);
            }
            return fs;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sb);
            writer.WriteStartDocument();
            writer.WriteStartElement("Favorites");
            writer.WriteAttributeString("show", ShowFavorites ? "1" : "0");
            foreach (GameChar gc in Favorites.Keys)
            {
                writer.WriteStartElement("list");
                writer.WriteAttributeString("game", ((int)gc.Game).ToString());
                writer.WriteAttributeString("char", gc.CharName);
                foreach (int spell in Favorites[gc])
                {
                    writer.WriteStartElement("spell");
                    writer.WriteAttributeString("id", spell.ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
            return sb.ToString();
        }
    }

    public class FavoriteSpellsTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                if ((string)value == "Default")
                    return new FavoriteSpells();

                return FavoriteSpells.FromXml(value as string);
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return ((FavoriteSpells)value).ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    public class TriggerListTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                if ((string)value == "Default")
                    return new TriggerList();

                return new TriggerList(value as string);
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return ((TriggerList)value).GetXml();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
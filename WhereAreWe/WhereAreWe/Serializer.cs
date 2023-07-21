using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.IO.Compression;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WhereAreWe
{
    public class Serializer
    {
        public Serializer()
        {
        }

        public void Save(MapBook book, string strFile)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<MapBook version=\"1.0\"/>");
            XmlNode root = doc.SelectSingleNode("/MapBook");
            root.Attributes.Append(doc.CreateAttribute("title")).Value = String.Format("{0}", book.Title);
            root.Attributes.Append(doc.CreateAttribute("IncreaseX")).Value = ((int) book.Location.IncreaseX).ToString();
            root.Attributes.Append(doc.CreateAttribute("IncreaseY")).Value = ((int) book.Location.IncreaseY).ToString();
            root.Attributes.Append(doc.CreateAttribute("OffsetX")).Value = book.Location.OffsetX.ToString();
            root.Attributes.Append(doc.CreateAttribute("OffsetY")).Value = book.Location.OffsetY.ToString();
            root.Attributes.Append(doc.CreateAttribute("QuickDoor")).Value = ((int)book.QuickDoor).ToString();
            if (!String.IsNullOrWhiteSpace(book.BookNote))
            {
                XmlNode nodeBookNote = root.AppendChild(doc.CreateElement("Note"));
                nodeBookNote.InnerText = book.BookNote;
            }
            XmlNode nodeBag = root.AppendChild(doc.CreateElement("Bag"));
            nodeBag.InnerText = book.BagOfHolding.SerializeToString();
            XmlElement nodeGridLines = root.AppendChild(doc.CreateElement("GridLines")) as XmlElement;
            nodeGridLines.SetAttribute("color", String.Format("{0:X8}", book.GridLines.Color.ToArgb()));
            nodeGridLines.SetAttribute("style", String.Format("{0}", (int) book.GridLines.Pattern));
            nodeGridLines.SetAttribute("width", String.Format("{0}", book.GridLines.Width));

            XmlElement nodeUnvisited = root.AppendChild(doc.CreateElement("Unvisited")) as XmlElement;
            nodeUnvisited.SetAttribute("fore", String.Format("{0:X8}", book.UnvisitedPattern.Color.ToArgb()));
            nodeUnvisited.SetAttribute("back", String.Format("{0:X8}", book.UnvisitedPattern.BackColor.ToArgb()));
            nodeUnvisited.SetAttribute("style", String.Format("{0}", (int) book.UnvisitedPattern.Pattern));

            XmlNode nodeFlagged = root.AppendChild(doc.CreateElement("FlaggedQuests"));
            if (book.FlaggedQuests != null)
            {
                foreach (string strQuest in book.FlaggedQuests)
                {
                    XmlNode nodeQuest = nodeFlagged.AppendChild(doc.CreateElement("Quest"));
                    nodeQuest.InnerText = strQuest;
                }
            }
            XmlNode nodeCompleted = root.AppendChild(doc.CreateElement("CompletedQuests"));
            if (book.ManualCompletedQuests != null)
            {
                foreach (string strQuest in book.ManualCompletedQuests)
                {
                    XmlNode nodeQuest = nodeCompleted.AppendChild(doc.CreateElement("Quest"));
                    nodeQuest.InnerText = strQuest;
                }
            }
            if (book.ManualCompletedTasks != null)
            {
                foreach (string strQuest in book.ManualCompletedTasks)
                {
                    XmlNode nodeTask = nodeCompleted.AppendChild(doc.CreateElement("Task"));
                    nodeTask.InnerText = strQuest;
                }
            }
            foreach (MapSheet sheet in book.Sheets)
            {
                XmlNode nodeSheet = root.AppendChild(doc.CreateElement("Sheet"));
                nodeSheet.Attributes.Append(doc.CreateAttribute("width")).Value = String.Format("{0}", sheet.GridWidth);
                nodeSheet.Attributes.Append(doc.CreateAttribute("height")).Value = String.Format("{0}", sheet.GridHeight);
                nodeSheet.Attributes.Append(doc.CreateAttribute("title")).Value = sheet.Title;
                nodeSheet.Attributes.Append(doc.CreateAttribute("path")).Value = sheet.MenuPath;
                nodeSheet.Attributes.Append(doc.CreateAttribute("zoom")).Value = String.Format("{0}", sheet.DefaultZoom);
                nodeSheet.Attributes.Append(doc.CreateAttribute("gameIndex")).Value = String.Format("{0}", sheet.GameMapIndex);

                if (sheet.Sections != null && sheet.Sections.Length > 0)
                {
                    XmlNode nodeSections = nodeSheet.AppendChild(doc.CreateElement("Sections"));
                    foreach (MapSection section in sheet.Sections)
                    {
                        XmlNode nodeSection = nodeSections.AppendChild(doc.CreateElement("Section"));
                        nodeSection.Attributes.Append(doc.CreateAttribute("x")).Value = String.Format("{0}", section.Source.X);
                        nodeSection.Attributes.Append(doc.CreateAttribute("y")).Value = String.Format("{0}", section.Source.Y);
                        nodeSection.Attributes.Append(doc.CreateAttribute("w")).Value = String.Format("{0}", section.Source.Width);
                        nodeSection.Attributes.Append(doc.CreateAttribute("h")).Value = String.Format("{0}", section.Source.Height);
                        nodeSection.Attributes.Append(doc.CreateAttribute("tx")).Value = String.Format("{0}", section.Target.X);
                        nodeSection.Attributes.Append(doc.CreateAttribute("ty")).Value = String.Format("{0}", section.Target.Y);
                    }
                }

                if (!String.IsNullOrWhiteSpace(sheet.UnvisitedBitmapFile))
                {
                    XmlNode nodeCustom = nodeSheet.AppendChild(doc.CreateElement("Unvisited"));
                    nodeCustom.Attributes.Append(doc.CreateAttribute("use")).Value = sheet.UseUnvisitedBitmap ? "1" : "0";
                    nodeCustom.Attributes.Append(doc.CreateAttribute("left")).Value = String.Format("{0}", sheet.UnvisitedCrop.Left);
                    nodeCustom.Attributes.Append(doc.CreateAttribute("top")).Value = String.Format("{0}", sheet.UnvisitedCrop.Top);
                    nodeCustom.Attributes.Append(doc.CreateAttribute("right")).Value = String.Format("{0}", sheet.UnvisitedCrop.Right);
                    nodeCustom.Attributes.Append(doc.CreateAttribute("bottom")).Value = String.Format("{0}", sheet.UnvisitedCrop.Bottom);
                    nodeCustom.Attributes.Append(doc.CreateAttribute("gridleft")).Value = String.Format("{0}", sheet.UnvisitedGrid.Left);
                    nodeCustom.Attributes.Append(doc.CreateAttribute("gridtop")).Value = String.Format("{0}", sheet.UnvisitedGrid.Top);
                    nodeCustom.Attributes.Append(doc.CreateAttribute("gridright")).Value = String.Format("{0}", sheet.UnvisitedGrid.Right);
                    nodeCustom.Attributes.Append(doc.CreateAttribute("gridbottom")).Value = String.Format("{0}", sheet.UnvisitedGrid.Bottom);

                    // If the image file is in the same directory with the .WAW file, remove the path.  This allows
                    // the user to move the .WAW and image file together.
                    if (Path.GetDirectoryName(sheet.UnvisitedBitmapFile) == Path.GetDirectoryName(strFile))
                        sheet.UnvisitedBitmapFile = Path.GetFileName(sheet.UnvisitedBitmapFile);
                    nodeCustom.InnerText = sheet.UnvisitedBitmapFile;
                }

                if (!String.IsNullOrWhiteSpace(sheet.SheetNote))
                {
                    XmlNode nodeMapNote = nodeSheet.AppendChild(doc.CreateElement("Note"));
                    nodeMapNote.InnerText = sheet.SheetNote;
                }

                XmlNode nodeGrid = nodeSheet.AppendChild(doc.CreateElement("Grid"));
                string strGridStream;
                if (sheet.GridStreamChanged)
                {
                    MemoryStream ms = new MemoryStream(sheet.Grid.Length * MapSquare.SizeHint);
                    GZipStream gz = new GZipStream(ms, CompressionMode.Compress);
                    sheet.Serialize(gz);
                    gz.Close();
                    strGridStream = Convert.ToBase64String(ms.ToArray());
                    sheet.GridStreamCache = strGridStream;
                }
                else
                    strGridStream = sheet.GridStreamCache;

                nodeGrid.InnerText = strGridStream;

                sheet.GridStreamChanged = false;

                XmlNode nodeNotes = nodeSheet.AppendChild(doc.CreateElement("Notes"));
                foreach (MapNote note in sheet.GetAllNotes())
                {
                    XmlElement elemNote = (XmlElement) nodeNotes.AppendChild(doc.CreateElement("Note"));
                    elemNote.Attributes.Append(doc.CreateAttribute("x")).Value = String.Format("{0}", note.Location.X);
                    elemNote.Attributes.Append(doc.CreateAttribute("y")).Value = String.Format("{0}", note.Location.Y);
                    if (note.Color != Color.Black)
                        elemNote.Attributes.Append(doc.CreateAttribute("color")).Value = String.Format("{0:X8}", note.Color.ToArgb());
                    elemNote.Attributes.Append(doc.CreateAttribute("symbol")).Value = note.Symbol;
                    elemNote.InnerText = note.Text;
                }
                XmlNode nodeIcons = nodeSheet.AppendChild(doc.CreateElement("Icons"));
                foreach (MapIcon icon in sheet.GetAllIcons())
                {
                    XmlElement elemIcon = (XmlElement)nodeIcons.AppendChild(doc.CreateElement("Icon"));
                    elemIcon.Attributes.Append(doc.CreateAttribute("x")).Value = String.Format("{0}", icon.Location.X);
                    elemIcon.Attributes.Append(doc.CreateAttribute("y")).Value = String.Format("{0}", icon.Location.Y);
                    elemIcon.Attributes.Append(doc.CreateAttribute("id")).Value = String.Format("{0}", (int) icon.Name);
                    elemIcon.Attributes.Append(doc.CreateAttribute("dir")).Value = String.Format("{0}", (int) icon.Orientation);
                    if (icon.Color.ToArgb() != Color.Black.ToArgb())
                        elemIcon.Attributes.Append(doc.CreateAttribute("color")).Value = String.Format("{0:X8}", icon.Color.ToArgb());
                }
                if (sheet.Labels != null && sheet.Labels.Count > 0)
                {
                    XmlNode nodeLabels = nodeSheet.AppendChild(doc.CreateElement("Labels"));
                    foreach (MapLabel label in sheet.Labels.Values)
                    {
                        if (String.IsNullOrWhiteSpace(label.Text))
                            continue;
                        XmlElement elemLabel = (XmlElement)nodeLabels.AppendChild(doc.CreateElement("Label"));
                        elemLabel.Attributes.Append(doc.CreateAttribute("text")).Value = label.Text;
                        elemLabel.Attributes.Append(doc.CreateAttribute("x")).Value = String.Format("{0:F2}", label.Location.X);
                        elemLabel.Attributes.Append(doc.CreateAttribute("y")).Value = String.Format("{0:F2}", label.Location.Y);
                        if (label.Anchors.Length == 1 && label.Anchors[0].Width == 1 && label.Anchors[0].Height == 1)
                        {
                            elemLabel.Attributes.Append(doc.CreateAttribute("ax")).Value = String.Format("{0}", label.Anchors[0].X);
                            elemLabel.Attributes.Append(doc.CreateAttribute("ay")).Value = String.Format("{0}", label.Anchors[0].Y);
                        }
                        else
                        {
                            foreach (Rectangle rc in label.Anchors)
                            {
                                XmlElement elemAnchor = (XmlElement)elemLabel.AppendChild(doc.CreateElement("Anchor"));
                                elemAnchor.Attributes.Append(doc.CreateAttribute("x")).Value = String.Format("{0}", rc.X);
                                elemAnchor.Attributes.Append(doc.CreateAttribute("y")).Value = String.Format("{0}", rc.Y);
                                elemAnchor.Attributes.Append(doc.CreateAttribute("w")).Value = String.Format("{0}", rc.Width);
                                elemAnchor.Attributes.Append(doc.CreateAttribute("h")).Value = String.Format("{0}", rc.Height);
                            }
                        }
                        if (label.Size != 100)
                            elemLabel.Attributes.Append(doc.CreateAttribute("size")).Value = String.Format("{0}", label.Size);
                        if (label.ForeColor.ToArgb() != MapLabel.DefaultForeColor.ToArgb())
                            elemLabel.Attributes.Append(doc.CreateAttribute("fore")).Value = String.Format("{0:X8}", label.ForeColor.ToArgb());
                        if (label.BackColor.ToArgb() != MapLabel.DefaultBackColor.ToArgb())
                            elemLabel.Attributes.Append(doc.CreateAttribute("back")).Value = String.Format("{0:X8}", label.BackColor.ToArgb());
                        if (label.BorderColor.ToArgb() != MapLabel.DefaultBorderColor.ToArgb())
                            elemLabel.Attributes.Append(doc.CreateAttribute("border")).Value = String.Format("{0:X8}", label.BorderColor.ToArgb());
                    }
                }
            }
            doc.Save(strFile);
        }

        public MapBook Load(string strFile)
        {
            List<MapSheet> maps = new List<MapSheet>();
            XmlDocument doc = new XmlDocument();
            ItemBag bag = new ItemBag();
            string strMapNote = String.Empty;
            string[] flagged = null;
            string[] completedQuests = null;
            string[] completedTasks = null;

            if (strFile == Global.InternalMapString)
                strFile = Games.MapForGame(Properties.Settings.Default.Game);

            if (String.IsNullOrWhiteSpace(strFile))
                return Global.CreateNewMapBook();

            if (strFile.StartsWith(":"))
                doc.LoadXml(Games.GetInternalMap(strFile.Substring(1)));
            else
                doc.Load(strFile);

            XmlNode root = doc.SelectSingleNode("/MapBook");
            if (root == null)
                return Global.CreateNewMapBook();   // Error

            XmlNode nodeMapNote = root.SelectSingleNode("Note");
            if (nodeMapNote != null)
                strMapNote = nodeMapNote.InnerText;

            XmlNode nodeBag = root.SelectSingleNode("Bag");
            if (nodeBag != null)
                bag = new ItemBag(nodeBag.InnerText);

            MapLineInfo infoGrid = Global.DefaultGridLineInfo;
            XmlNode nodeGridLines = root.SelectSingleNode("GridLines");
            if (nodeGridLines != null)
            {
                infoGrid = new MapLineInfo(
                    Color.FromArgb(Convert.ToInt32(nodeGridLines.Attributes["color"].Value, 16)),
                    (DashStyle) Convert.ToInt32(nodeGridLines.Attributes["style"].Value),
                    Convert.ToInt32(nodeGridLines.Attributes["width"].Value));
            }

            ColorPattern cpUnvisited = MapBook.DefaultUnvisitedPattern;
            XmlElement nodeUnvisited = root.SelectSingleNode("Unvisited") as XmlElement;
            if (nodeUnvisited != null)
            {
                if (nodeUnvisited.HasAttribute("fore"))
                {
                    cpUnvisited = new ColorPattern(
                       Color.FromArgb(Convert.ToInt32(nodeUnvisited.Attributes["fore"].Value, 16)),
                       (HatchStyle)Convert.ToInt32(nodeUnvisited.Attributes["style"].Value),
                       Color.FromArgb(Convert.ToInt32(nodeUnvisited.Attributes["back"].Value, 16)));
                }
            }

            XmlNodeList listFlagged = root.SelectNodes("FlaggedQuests/Quest");
            if (listFlagged != null && listFlagged.Count > 0)
            {
                flagged = new string[listFlagged.Count];
                for (int i = 0; i < flagged.Length; i++)
                    flagged[i] = listFlagged[i].InnerText;
            }

            XmlNodeList listCompletedQuests = root.SelectNodes("CompletedQuests/Quest");
            if (listCompletedQuests != null && listCompletedQuests.Count > 0)
            {
                completedQuests = new string[listCompletedQuests.Count];
                for (int i = 0; i < completedQuests.Length; i++)
                    completedQuests[i] = listCompletedQuests[i].InnerText;
            }
            XmlNodeList listCompletedTasks = root.SelectNodes("CompletedQuests/Task");
            if (listCompletedTasks != null && listCompletedTasks.Count > 0)
            {
                completedTasks = new string[listCompletedTasks.Count];
                for (int i = 0; i < completedTasks.Length; i++)
                    completedTasks[i] = listCompletedTasks[i].InnerText;
            }

            foreach (XmlNode nodeSheet in root.SelectNodes("Sheet"))
            {
                if (nodeSheet is XmlElement)
                {
                    string strSheetNote = String.Empty;
                    string strCustomUnvisited = String.Empty;
                    bool bUseCustomBitmap = false;
                    Rectangle rcCustomCrop = Rectangle.Empty;
                    Rectangle rcCustomGrid = Rectangle.Empty;

                    XmlElement e = (XmlElement)nodeSheet;
                    int iWidth = 0;
                    if (!Int32.TryParse(e.Attributes["width"].Value, out iWidth))
                        return Global.CreateNewMapBook();    // Error
                    int iHeight = 0;
                    if (!Int32.TryParse(e.Attributes["height"].Value, out iHeight))
                        return Global.CreateNewMapBook();    // Error

                    XmlNode nodeSheetNote = nodeSheet.SelectSingleNode("Note");
                    if (nodeSheetNote != null)
                        strSheetNote = nodeSheetNote.InnerText;

                    List<MapSection> sections = null;
                    XmlNode nodeSections = nodeSheet.SelectSingleNode("Sections");
                    if (nodeSections != null)
                    {
                        sections = new List<MapSection>(nodeSections.ChildNodes.Count);
                        foreach (XmlElement eSection in nodeSections.ChildNodes)
                        {
                            Rectangle rc = new Rectangle(GetIntAttr(eSection, "x", 0), GetIntAttr(eSection, "y", 0),
                                GetIntAttr(eSection, "w", 0), GetIntAttr(eSection, "h", 0));
                            Point pt = new Point(GetIntAttr(eSection, "tx", 0), GetIntAttr(eSection, "ty", 0));
                            sections.Add(new MapSection(rc, pt));
                        }
                    }

                    XmlElement nodeCustom = nodeSheet.SelectSingleNode("Unvisited") as XmlElement;
                    if (nodeCustom != null)
                    {
                        try
                        {
                            bUseCustomBitmap = nodeCustom.HasAttribute("use") ? nodeCustom.Attributes["use"].Value == "1" : false;
                            rcCustomCrop = RectFromLTRB(nodeCustom, "left", "top", "right", "bottom");
                            rcCustomGrid = RectFromLTRB(nodeCustom, "gridleft", "gridtop", "gridright", "gridbottom");
                            strCustomUnvisited = nodeCustom.InnerText;
                        }
                        catch (Exception)
                        {
                            // Do not use a custom bitmap if the parameters are invalid
                            bUseCustomBitmap = false;
                        }
                    }

                    XmlNode nodeGrid = e.SelectSingleNode("Grid");
                    if (nodeGrid == null)
                        return Global.CreateNewMapBook();   // Error

                    MapSheet sheet;

                    MemoryStream ms = new MemoryStream(Convert.FromBase64String(nodeGrid.InnerText));
                    GZipStream gz = new GZipStream(ms, CompressionMode.Decompress);
                    sheet = new MapSheet(infoGrid, iWidth, iHeight, gz);
                    sheet.GridStreamCache = nodeGrid.InnerText;
                    sheet.GridStreamChanged = false;
                    maps.Add(sheet);
                    gz.Close();

                    sheet.Title = e.Attributes["title"].Value;
                    sheet.SheetNote = strSheetNote;
                    sheet.UnvisitedBitmapFile = strCustomUnvisited;
                    sheet.UnvisitedGrid = rcCustomGrid;
                    sheet.UnvisitedCrop = rcCustomCrop;
                    sheet.UseUnvisitedBitmap = bUseCustomBitmap;

                    if (sections != null)
                        sheet.Sections = sections.ToArray();

                    int iGameMapIndex = -1;
                    if (e.HasAttribute("gameIndex"))
                        if (!Int32.TryParse(e.Attributes["gameIndex"].Value, out iGameMapIndex))
                            iGameMapIndex = -1;
                    sheet.GameMapIndex = iGameMapIndex;

                    int iZoom = 100;
                    if (e.HasAttribute("zoom"))
                        if (!Int32.TryParse(e.Attributes["zoom"].Value, out iZoom))
                            iZoom = 100;
                    sheet.DefaultZoom = iZoom;

                    if (e.HasAttribute("path"))
                        sheet.MenuPath = e.Attributes["path"].Value;

                    List<MapNote> notes = new List<MapNote>();
                    XmlNode nodeNotes = nodeSheet.SelectSingleNode("Notes");
                    if (nodeNotes != null)
                    {
                        foreach (XmlElement elemNote in nodeNotes.SelectNodes("Note"))
                        {
                            MapNote note = new MapNote();
                            if (elemNote.HasAttribute("color"))
                            {
                                int iColor = 0;
                                if (Int32.TryParse(elemNote.Attributes["color"].Value, System.Globalization.NumberStyles.HexNumber, null, out iColor))
                                    note.Color = Color.FromArgb(iColor);
                            }
                            else
                                note.Color = Color.Black;
                            int iX = 0, iY = 0;
                            if (Int32.TryParse(elemNote.Attributes["x"].Value, out iX) && Int32.TryParse(elemNote.Attributes["y"].Value, out iY))
                                note.Location = new Point(iX, iY);
                            note.Symbol = elemNote.Attributes["symbol"].Value;
                            note.Text = elemNote.InnerText;
                            notes.Add(note);
                        }

                        sheet.SetAllNotes(notes.ToArray());
                    }

                    List<MapIcon> icons = new List<MapIcon>();
                    XmlNode nodeIcons = nodeSheet.SelectSingleNode("Icons");
                    if (nodeIcons != null)
                    {
                        foreach (XmlElement elemIcon in nodeIcons.SelectNodes("Icon"))
                        {
                            MapIcon icon = new MapIcon();
                            int iX = 0, iY = 0;
                            if (Int32.TryParse(elemIcon.Attributes["x"].Value, out iX) && Int32.TryParse(elemIcon.Attributes["y"].Value, out iY))
                                icon.Location = new Point(iX, iY);
                            int iID = 0, iDir = 0;
                            if (Int32.TryParse(elemIcon.Attributes["id"].Value, out iID))
                                icon.Name = (IconName) iID;
                            if (Int32.TryParse(elemIcon.Attributes["dir"].Value, out iDir))
                                icon.Orientation = (Direction) iDir;
                            if (elemIcon.HasAttribute("color"))
                            {
                                int iColor = 0;
                                if (Int32.TryParse(elemIcon.Attributes["color"].Value, System.Globalization.NumberStyles.HexNumber, null, out iColor))
                                    icon.Color = Color.FromArgb(iColor);
                            }
                            else
                                icon.Color = Color.Black;
                            icons.Add(icon);
                        }

                        sheet.SetAllIcons(icons.ToArray());
                    }

                    XmlNode nodeLabels = nodeSheet.SelectSingleNode("Labels");
                    if (nodeLabels != null)
                    {
                        MapLabels labels = new MapLabels();
                        foreach (XmlElement elemLabel in nodeLabels.SelectNodes("Label"))
                        {
                            try
                            {
                                MapLabel label = null;
                                float fX = 0, fY = 0;
                                PointF ptLocation = Point.Empty;
                                int iX = 0, iY = 0;
                                string strText = String.Empty;
                                if (elemLabel.HasAttribute("text"))
                                    strText = elemLabel.Attributes["text"].Value;
                                else
                                    strText = elemLabel.InnerText;
                                if (elemLabel.HasAttribute("x") && elemLabel.HasAttribute("y") && 
                                    float.TryParse(elemLabel.Attributes["x"].Value, out fX) && float.TryParse(elemLabel.Attributes["y"].Value, out fY))
                                    ptLocation = new PointF(fX, fY);
                                if (elemLabel.HasAttribute("ax") && elemLabel.HasAttribute("ay") &&
                                    Int32.TryParse(elemLabel.Attributes["ax"].Value, out iX) && Int32.TryParse(elemLabel.Attributes["ay"].Value, out iY))
                                {
                                    label = new MapLabel(ptLocation, new Rectangle[] { new Rectangle(iX, iY, 1, 1) }, strText);
                                }
                                else
                                {
                                    XmlNodeList anchors = elemLabel.SelectNodes("Anchor");
                                    List<Rectangle> listRects = new List<Rectangle>(anchors.Count);
                                    foreach (XmlElement elemAnchor in anchors)
                                    {
                                        int x, y, width, height;
                                        if (elemAnchor.HasAttribute("x") && elemAnchor.HasAttribute("y") &&
                                            Int32.TryParse(elemAnchor.Attributes["x"].Value, out x) && Int32.TryParse(elemAnchor.Attributes["y"].Value, out y))
                                        {
                                            Rectangle rc = new Rectangle(x, y, 1, 1);
                                            if (elemAnchor.HasAttribute("w") && elemAnchor.HasAttribute("h") &&
                                                Int32.TryParse(elemAnchor.Attributes["w"].Value, out width) && Int32.TryParse(elemAnchor.Attributes["h"].Value, out height))
                                            {
                                                rc.Width = width;
                                                rc.Height = height;
                                            }
                                            listRects.Add(rc);
                                        }
                                    }
                                    label = new MapLabel(ptLocation, listRects.ToArray(), strText);
                                }
                                if (elemLabel.HasAttribute("size"))
                                    label.Size = Convert.ToInt32(elemLabel.Attributes["size"].Value);
                                if (elemLabel.HasAttribute("fore"))
                                    label.ForeColor = Color.FromArgb(Convert.ToInt32(elemLabel.Attributes["fore"].Value, 16));
                                if (elemLabel.HasAttribute("back"))
                                    label.BackColor = Color.FromArgb(Convert.ToInt32(elemLabel.Attributes["back"].Value, 16));
                                if (elemLabel.HasAttribute("border"))
                                    label.BorderColor = Color.FromArgb(Convert.ToInt32(elemLabel.Attributes["border"].Value, 16));
                                if (!labels.ContainsKey(label.Location))
                                    labels.Add(label);
                            }
                            catch(Exception)
                            {
                                // Invalid label; do not add
                            }
                        }
                        sheet.Labels = labels;
                    }
                }
            }

            MapBook book = Global.CreateNewMapBook(maps, strFile);
            book.Title = root.Attributes["title"].Value;
            Int32 parse;
            if (root.Attributes.GetNamedItem("IncreaseX") != null)
            {
                if (Int32.TryParse(root.Attributes["IncreaseX"].Value, out parse))
                    book.Location.IncreaseX = (AxisIncreaseX)parse;
                if (Int32.TryParse(root.Attributes["IncreaseY"].Value, out parse))
                    book.Location.IncreaseY = (AxisIncreaseY)parse;
                if (Int32.TryParse(root.Attributes["OffsetX"].Value, out parse))
                    book.Location.OffsetX = parse;
                if (Int32.TryParse(root.Attributes["OffsetY"].Value, out parse))
                    book.Location.OffsetY = parse;
            }
            if (root.Attributes.GetNamedItem("QuickDoor") != null)
            {
                if (Int32.TryParse(root.Attributes["QuickDoor"].Value, out parse))
                    book.QuickDoor = (DoorType) parse;
            }
            book.GridLines = infoGrid;
            book.BagOfHolding = bag;
            book.FlaggedQuests = flagged;
            book.ManualCompletedQuests = completedQuests;
            book.ManualCompletedTasks = completedTasks;
            book.BookNote = strMapNote;

            return book;
        }

        private int GetIntAttr(XmlElement e, string name, int iDefault)
        {
            if (!e.HasAttribute(name))
                return iDefault;
            int iVal = iDefault;
            if (!Int32.TryParse(e.Attributes[name].Value, out iVal))
                return iDefault;
            return iVal;
        }

        private Rectangle RectFromLTRB(XmlNode node, string left, string top, string right, string bottom)
        {
            int iLeft = Convert.ToInt32(node.Attributes[left].Value);
            int iTop = Convert.ToInt32(node.Attributes[top].Value);
            int iRight = Convert.ToInt32(node.Attributes[right].Value);
            int iBottom = Convert.ToInt32(node.Attributes[bottom].Value);
            return new Rectangle(iLeft, iTop, iRight - iLeft, iBottom - iTop);
        }
    }
}

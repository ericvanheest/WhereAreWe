using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;

namespace WhereAreWe
{
    public partial class MainForm
    {
        private void MenuEditUndo()
        {
            if (CurrentSheet.UndoContainer.UndoAvailable < 1)
                return;

            CancelSelection();

            UndoItem item = CurrentSheet.UndoContainer.UndoOneAction();

            switch (item.Action)
            {
                case UndoAction.MapSizeChange:
                    CurrentSheet.AddRedoSheet();
                    break;
                case UndoAction.MapDataChange:
                    CurrentSheet.AddRedoBlocks(item.Squares);
                    break;
                case UndoAction.NotesChange:
                    CurrentSheet.AddRedoNotes();
                    break;
                case UndoAction.IconsChange:
                    CurrentSheet.AddRedoIcons();
                    break;
                case UndoAction.LabelsChange:
                    CurrentSheet.AddRedoLabels();
                    break;
                case UndoAction.NoteChange:
                    CurrentSheet.AddRedoNote(item.Note.Location);
                    break;
                case UndoAction.IconChange:
                    break;
                case UndoAction.VisitedChange:
                    CurrentSheet.AddRedoVisited();
                    break;
                default:
                    break;
            }

            ProcessUndoItem(item);
            if (Mode == BlockMode.Keyboard)
                SetNoteText(CurrentSheet.Cursor);
        }

        private void ProcessUndoItem(UndoItem item)
        {
            if (item == null)
                return;

            switch (item.Action)
            {
                case UndoAction.MapDataChange:
                    CurrentSheet.SetSquaresFromUndo(item.Squares);
                    foreach (UndoItem otherItem in item.Squares.Other)
                        ProcessUndoItem(otherItem);
                    SetDirtyUnsaved();
                    break;
                case UndoAction.MapSizeChange:
                    Size sz = CurrentSheet.SquareSize;
                    Undo undoHold = CurrentSheet.UndoContainer;
                    CurrentSheet.SetGridData(item.MapSheetSize.Width, item.MapSheetSize.Height, item.MapSheetStream);
                    item.MapSheetStream.Seek(0, SeekOrigin.Begin);
                    CurrentSheet.SetAllNotes(item.Notes);
                    CurrentSheet.SetAllIcons(item.Icons);
                    CurrentSheet.SetAllLabels(item.Labels);
                    CurrentSheet.UndoContainer = undoHold;
                    CurrentSheet.SquareSize = sz;
                    CurrentSheet.SetYouAreHere(item.YouAreHere, this, IgnoreInaccessible);
                    SetDirtyUnsaved();
                    break;
                case UndoAction.NotesChange:
                    CurrentSheet.SetAllNotes(item.Notes);
                    SetDirtyUnsaved();
                    break;
                case UndoAction.LabelsChange:
                    CurrentSheet.SetAllLabels(item.Labels);
                    SetDirtyUnsaved();
                    break;
                case UndoAction.IconsChange:
                    CurrentSheet.SetAllIcons(item.Icons);
                    SetDirtyUnsaved();
                    break;
                case UndoAction.NoteChange:
                    CurrentSheet.SetNote(item.Note);
                    SetDirtyUnsaved();
                    break;
                case UndoAction.IconChange:
                    CurrentSheet.SetIcon(item.Icon);
                    SetDirtyUnsaved();
                    break;
                case UndoAction.VisitedChange:
                    item.Visited.SetVisited(CurrentSheet);
                    SetDirtyUnsaved();
                    break;
                default:
                    break;
            }
        }

        private void MenuEditRedo()
        {
            if (CurrentSheet.UndoContainer.RedoAvailable < 1)
                return;

            UndoItem item = CurrentSheet.UndoContainer.GetRedoAction();

            switch (item.Action)
            {
                case UndoAction.MapSizeChange:
                    CurrentSheet.AddUndoSheet();
                    break;
                case UndoAction.NotesChange:
                    CurrentSheet.AddUndoNotes();
                    break;
                case UndoAction.LabelsChange:
                    CurrentSheet.AddUndoLabels();
                    break;
                case UndoAction.IconsChange:
                    CurrentSheet.AddUndoIcons();
                    break;
                case UndoAction.NoteChange:
                    CurrentSheet.AddUndoNote(item.Note.Location);
                    break;
                case UndoAction.LabelChange:
                    CurrentSheet.AddUndoLabel(item.Label);
                    break;
                case UndoAction.IconChange:
                    // CurrentSheet.AddUndoIcon(item.Icon.Location);
                    break;
                case UndoAction.MapDataChange:
                    CurrentSheet.AddUndoBlocks(item.Squares);
                    break;
                case UndoAction.VisitedChange:
                    CurrentSheet.AddUndoVisited();
                    break;
                default:
                    break;
            }

            CurrentSheet.UndoContainer.IncrementRedoCounter();

            ProcessUndoItem(item);
        }
    }
}

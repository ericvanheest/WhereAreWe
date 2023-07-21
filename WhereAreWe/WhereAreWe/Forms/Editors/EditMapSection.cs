using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class EditMapSectionForm : Form
    {
        public EditMapSectionForm()
        {
            InitializeComponent();
        }

        public EditMapSectionForm(Rectangle rc, Point pt)
        {
            InitializeComponent();
            SourceRect = rc;
            TargetPoint = pt;
        }

        public EditMapSectionForm(MapSection section)
        {
            InitializeComponent();
            Section = section;
        }

        public MapSection Section
        {
            get
            {
                return new MapSection(SourceRect, TargetPoint);
            }

            set
            {
                SourceRect = value.Source;
                TargetPoint = value.Target;
            }
        }

        public Rectangle SourceRect
        {
            get
            {
                return new Rectangle((int)nudX.Value, (int)nudY.Value, (int)nudWidth.Value, (int)nudHeight.Value);
            }

            set
            {
                Global.SetNud(nudX, value.X);
                Global.SetNud(nudY, value.Y);
                Global.SetNud(nudWidth, value.Width);
                Global.SetNud(nudHeight, value.Height);
            }
        }

        public Point TargetPoint
        {
            get
            {
                return new Point((int)nudNewX.Value, (int)nudNewY.Value);
            }

            set
            {
                Global.SetNud(nudNewX, value.X);
                Global.SetNud(nudNewY, value.Y);
            }
        }
    }

    public class MapSection
    {
        public Rectangle Source;
        public Point Target;
        public bool External = false;

        public MapSection(Rectangle rcSource, Point ptTarget)
        {
            Source = rcSource;
            Target = ptTarget;
        }

        public Rectangle GetTargetRect(LocationSettings settings)
        {
            if (settings.IncreaseX == AxisIncreaseX.RightToLeft)
            {
                if (settings.IncreaseY == AxisIncreaseY.BottomToTop)
                    return new Rectangle(Target.X - (Source.Width - 1), Target.Y - (Source.Height - 1), Source.Width, Source.Height);
                else
                    return new Rectangle(Target.X - (Source.Width - 1), Target.Y, Source.Width, Source.Height);
            }
            else
            {
                if (settings.IncreaseY == AxisIncreaseY.BottomToTop)
                    return new Rectangle(Target.X, Target.Y - (Source.Height - 1), Source.Width, Source.Height);
                else
                    return new Rectangle(Target.X, Target.Y, Source.Width, Source.Height);
            }
        }

        public MapSection Clone()
        {
            return new MapSection(Source, Target);
        }

        public string SourceString { get { return String.Format("({0},{1}) {2}x{3}", Source.X, Source.Y, Source.Width, Source.Height); } }
        public string TargetString { get { return String.Format("{0},{1}", Target.X, Target.Y); } }
    }
}

using GameEditor.ContentInfoClasses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GameEditor.MyControls
{
    class WaypointBeacon : PictureBox
    {
        public WayPoint WaypointLink { get; set; }
        public int waypointIndex;

        public WaypointBeacon()
            : base()
        {
            //this.DoubleBuffered = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.SizeMode = PictureBoxSizeMode.Zoom;
            this.Cursor = Cursors.Hand;
            this.WaypointLink = new WayPoint();
            this.Paint += WaypointBeacon_Paint;
        }

        void WaypointBeacon_Paint(object sender, PaintEventArgs e)
        {
            Brush brush = new SolidBrush(Color.Yellow);
            Bitmap bm = new Bitmap(this.Width, this.Height);

            Graphics bmGraphics = Graphics.FromImage(bm);
            bmGraphics.DrawImage(GameEditor.Properties.Resources.WayPoint, new Rectangle(0, 0, this.Width, this.Height));
            bmGraphics.DrawString(waypointIndex.ToString(), new Font("Arial", 15, FontStyle.Bold), brush, 1, 1);

            brush.Dispose();
            bmGraphics.Dispose();

            e.Graphics.DrawImage(bm, Point.Empty);
        }
    }
}

﻿using GameEditor.ContentInfoClasses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GameEditor.MyControls
{
    public class WaypointBeacon : PictureBox
    {
        private WayPoint waypointLink;
        public WayPoint WaypointLink
        {
            get
            {
                return this.waypointLink;
            }
            set
            {
                this.waypointLink = value;
                this.Location = value.Position;
            }
        }
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
            this.LocationChanged += WaypointBeacon_LocationChanged;
        }

        void WaypointBeacon_LocationChanged(object sender, EventArgs e)
        {
            form_mapEdit.Instance.SetWaypointPosition(this, Location);
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

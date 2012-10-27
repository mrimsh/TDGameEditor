using System.Drawing;
using System.Windows.Forms;
using System;

namespace GameEditor
{
    public partial class form_mapEdit : Form
    {
        Image img_Map;
        bool isMiniMapDrag;
        Point miniMapDragMouseStartPosition;

        public form_mapEdit()
        {
            InitializeComponent();

            img_Map = GameEditor.Properties.Resources.Island_TD;

            splitContainer_panel.Panel1.AutoScroll = true;
            pctBox_mapImage.Location = Point.Empty;
            DrawMiniMapVeiwZone();
        }

        private void splitContainer1_Panel1_Scroll(object sender, ScrollEventArgs e)
        {
            DrawMiniMapVeiwZone();
            pctBox_mapImage.Refresh();
        }

        private void splitContainer_panel_SplitterMoved(object sender, SplitterEventArgs e)
        {
            DrawMiniMapVeiwZone();
        }

        private void DrawMiniMapVeiwZone()
        {
            Pen pen_miniMap = new Pen(Color.Red);
            Bitmap bm = new Bitmap(pctBox_MiniMap.Width, pctBox_MiniMap.Height);
            Graphics bmGraphics = Graphics.FromImage(bm);
            bmGraphics.DrawImage(img_Map, new Rectangle(0, 0, pctBox_MiniMap.Width, pctBox_MiniMap.Height));
            bmGraphics.DrawRectangle(pen_miniMap, new Rectangle(
                 (int)((float)-pctBox_mapImage.Location.X * (float)pctBox_MiniMap.Width / (float)pctBox_mapImage.Image.Width), // x
                (int)((float)-pctBox_mapImage.Location.Y * ((float)pctBox_MiniMap.Height / (float)pctBox_mapImage.Image.Height)), // y
                pctBox_MiniMap.Width * splitContainer_panel.Panel1.Width / pctBox_mapImage.Width, // width
                pctBox_MiniMap.Height * splitContainer_panel.Panel1.Height / pctBox_mapImage.Height // height
            ));

            pen_miniMap.Dispose();
            bmGraphics.Dispose();

            pctBox_MiniMap.Image = bm;
            pctBox_MiniMap.Refresh();

        }

        private void form_mapEdit_Resize(object sender, System.EventArgs e)
        {
            DrawMiniMapVeiwZone();
        }

        private void pctBox_MiniMap_MouseDown(object sender, MouseEventArgs e)
        {
            miniMapDragMouseStartPosition = e.Location;
            isMiniMapDrag = true;
        }

        private void pctBox_MiniMap_MouseUp(object sender, MouseEventArgs e)
        {
            isMiniMapDrag = false;
        }

        private void pctBox_MiniMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMiniMapDrag)
            {
                Point mouseOffset = new Point(e.Location.X - miniMapDragMouseStartPosition.X, e.Location.Y - miniMapDragMouseStartPosition.Y);

                splitContainer_panel.Panel1.HorizontalScroll.Value = LimitInt(
                    splitContainer_panel.Panel1.HorizontalScroll.Value + (int)Math.Ceiling((double)mouseOffset.X * (double)pctBox_mapImage.Image.Width / (double)pctBox_MiniMap.Width),
                    splitContainer_panel.Panel1.HorizontalScroll.Minimum,
                    splitContainer_panel.Panel1.HorizontalScroll.Maximum);
                splitContainer_panel.Panel1.VerticalScroll.Value = LimitInt(
                    splitContainer_panel.Panel1.VerticalScroll.Value + (int)Math.Ceiling(((double)mouseOffset.Y * (double)pctBox_mapImage.Image.Height / (double)pctBox_MiniMap.Height)),
                    splitContainer_panel.Panel1.VerticalScroll.Minimum,
                    splitContainer_panel.Panel1.VerticalScroll.Maximum);
                splitContainer_panel.Panel1.PerformLayout(); // removes trembling of scroll bar
                pctBox_mapImage.Refresh(); // removes trembling of image

                System.Diagnostics.Trace.WriteLine((double)mouseOffset.Y * (double)pctBox_mapImage.Image.Height / (double)pctBox_MiniMap.Height);

                miniMapDragMouseStartPosition = e.Location;

                DrawMiniMapVeiwZone();
            }
        }

        private int LimitInt(int i, int down, int up)
        {
            if (i < down)
            {
                return down;
            }
            else if (i > up)
            {
                return up;
            }
            else { return i; }
        }

        private void waypointToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

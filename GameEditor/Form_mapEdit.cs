using System.Drawing;
using System.Windows.Forms;
using System;
using GameEditor.ContentInfoClasses;
using System.Collections.Generic;
using GameEditor.MyControls;

namespace GameEditor
{
    public partial class form_mapEdit : Form
    {
        #region Singleton
        private static form_mapEdit instance;
        public static form_mapEdit Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        Image img_Map;
        bool isMiniMapDrag;
        Point dragStart;
        MapInfo currentMap = new MapInfo();
        private Control selectedObject;
        private List<WaypointBeacon> waypointBeacons = new List<WaypointBeacon>();
        private int selectedWay;

        public form_mapEdit()
        {
            instance = this;

            InitializeComponent();

            this.SetStyle(System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true);

            img_Map = GameEditor.Properties.Resources.Island_TD;

            splitContainer_panel.Panel1.AutoScroll = true;
            pctBox_mapImage.Location = Point.Empty;
            DrawMiniMapVeiwZone();

            XMLManager.Instance.SetStoragePath("C:\\Users\\Murad\\Desktop\\temp\\");
            selectedObject = this;

            RefreshSelectedWayCombobox();
            combobox_SelectedWay.SelectedIndex = selectedWay;

            propGrid_properties.SelectedObject = currentMap;
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
            Brush brush = new SolidBrush(Color.Yellow);
            int imageSize = Math.Min(pctBox_MiniMap.Width, pctBox_MiniMap.Height);
            Bitmap bm = new Bitmap(imageSize, imageSize);
            Graphics bmGraphics = Graphics.FromImage(bm);
            bmGraphics.DrawImage(img_Map, new Rectangle(0, 0, imageSize, imageSize));
            for (int i = 0; i < waypointBeacons.Count; i++)
            {
                bmGraphics.DrawImage(
                    GameEditor.Properties.Resources.WayPoint,
                    (int)((float)waypointBeacons[i].Left * (float)imageSize / (float)pctBox_mapImage.Image.Width),
                    (int)((float)waypointBeacons[i].Bottom * (float)imageSize / (float)pctBox_mapImage.Image.Height - 16),
                    16,
                    16);
                bmGraphics.DrawString(
                    waypointBeacons[i].waypointIndex.ToString(),
                    new Font("Arial", 7, FontStyle.Bold),
                    brush,
                    (int)((float)waypointBeacons[i].Left * (float)imageSize / (float)pctBox_mapImage.Image.Width),
                    (int)((float)waypointBeacons[i].Bottom * (float)imageSize / (float)pctBox_mapImage.Image.Height)
                    );
                if (i + 1 < waypointBeacons.Count)
                {
                    bmGraphics.DrawLine(
                        pen_miniMap,
                    (int)((float)waypointBeacons[i].Left * (float)imageSize / (float)pctBox_mapImage.Image.Width),
                    (int)((float)waypointBeacons[i].Bottom * (float)imageSize / (float)pctBox_mapImage.Image.Height),
                    (int)((float)waypointBeacons[i + 1].Left * (float)imageSize / (float)pctBox_mapImage.Image.Width),
                    (int)((float)waypointBeacons[i + 1].Bottom * (float)imageSize / (float)pctBox_mapImage.Image.Height)
                    );
                }
            }
            bmGraphics.DrawRectangle(pen_miniMap, new Rectangle(
                 (int)((float)-pctBox_mapImage.Location.X * (float)imageSize / (float)pctBox_mapImage.Image.Width) + 2, // x
                (int)((float)-pctBox_mapImage.Location.Y * ((float)imageSize / (float)pctBox_mapImage.Image.Height)) + 2, // y
                imageSize * splitContainer_panel.Panel1.Width / pctBox_mapImage.Width - 6, // width
                imageSize * splitContainer_panel.Panel1.Height / pctBox_mapImage.Height - 6 // height
            ));

            pen_miniMap.Dispose();
            brush.Dispose();
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
            this.dragStart = e.Location;
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
                Point mouseOffset = new Point(e.Location.X - this.dragStart.X, e.Location.Y - this.dragStart.Y);

                int imageSize = Math.Min(pctBox_MiniMap.Width, pctBox_MiniMap.Height);
                splitContainer_panel.Panel1.HorizontalScroll.Value = LimitInt(
                    splitContainer_panel.Panel1.HorizontalScroll.Value + (int)Math.Ceiling((double)mouseOffset.X * (double)pctBox_mapImage.Image.Width / (double)imageSize),
                    splitContainer_panel.Panel1.HorizontalScroll.Minimum,
                    splitContainer_panel.Panel1.HorizontalScroll.Maximum);
                splitContainer_panel.Panel1.VerticalScroll.Value = LimitInt(
                    splitContainer_panel.Panel1.VerticalScroll.Value + (int)Math.Ceiling(((double)mouseOffset.Y * (double)pctBox_mapImage.Image.Height / (double)imageSize)),
                    splitContainer_panel.Panel1.VerticalScroll.Minimum,
                    splitContainer_panel.Panel1.VerticalScroll.Maximum);
                splitContainer_panel.Panel1.PerformLayout(); // removes trembling of scroll bar
                pctBox_mapImage.Refresh(); // removes trembling of image

                this.dragStart = e.Location;

                DrawMiniMapVeiwZone();
            }
        }

        private void сохранитьToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SaveCurrentMap();
        }

        private void spawnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void toolstrip_btn_addSpawner_Click(object sender, EventArgs e)
        {

        }

        private void towerPadToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolstrip_btn_addTowerPad_Click(object sender, EventArgs e)
        {

        }

        private void wayToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void waypointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddWayPoint();
        }

        private void toolstrip_btn_addWayPoint_Click(object sender, EventArgs e)
        {
            AddWayPoint();
        }

        private void AddWayPoint()
        {
            WaypointBeacon wb = new WaypointBeacon();
            waypointBeacons.Add(wb);
            this.pctBox_mapImage.Controls.Add(wb);
            wb.Size = new Size(64, 64);
            wb.Move += wb_Move;
            wb.MouseDown += wb_MouseDown;
            wb.Location = new Point(
                splitContainer_panel.Panel1.HorizontalScroll.Value + (int)(splitContainer_panel.Panel1.Width * 0.5f),
                splitContainer_panel.Panel1.VerticalScroll.Value + (int)(splitContainer_panel.Panel1.Height * 0.5f)
            );
            currentMap.Ways[combobox_SelectedWay.SelectedIndex].waypoints.Add(wb.WaypointLink);
            wb.waypointIndex = currentMap.Ways[combobox_SelectedWay.SelectedIndex].Waypoints.Count;
            propGrid_properties.SelectedObject = wb.WaypointLink;
            ControlMover.Init(wb, ControlMover.Direction.Any);
            DrawMiniMapVeiwZone();
            wb.Refresh();
            pctBox_mapImage.Refresh();
        }

        void wb_Move(object sender, EventArgs e)
        {
            //WaypointBeacon senderBeacon = sender as WaypointBeacon;
            //senderBeacon.WaypointLink.Position = new Point(senderBeacon.Right, senderBeacon.Bottom);
            DrawMiniMapVeiwZone();
            propGrid_properties.Refresh();
            pctBox_mapImage.Refresh();
        }

        void wb_MouseDown(object sender, MouseEventArgs e)
        {
            propGrid_properties.SelectedObject = (sender as WaypointBeacon).WaypointLink;
        }

        private void combobox_SelectedWay_SelectedIndexChanged(object sender, EventArgs e)
        {
            Way selectedWay = currentMap.Ways.Find(
                delegate(Way way)
                {
                    return way.name == ((ToolStripComboBox)sender).Text;
                }
            );

            RefreshWaypointBeacons();

            propGrid_properties.SelectedObject = selectedWay;
        }

        void RefreshWaypointBeacons()
        {
            for (int i = 0; i < waypointBeacons.Count; i++)
            {
                pctBox_mapImage.Controls.Remove(waypointBeacons[i]);
            }
            waypointBeacons.Clear();

            for (int i = 0; i < currentMap.Ways[combobox_SelectedWay.SelectedIndex].waypoints.Count; i++)
            {
                WaypointBeacon wb = new WaypointBeacon();
                waypointBeacons.Add(wb);
                this.pctBox_mapImage.Controls.Add(wb);
                wb.WaypointLink = currentMap.Ways[combobox_SelectedWay.SelectedIndex].waypoints[i];
                wb.Size = new Size(64, 64);
                wb.Move += wb_Move;
                wb.MouseDown += wb_MouseDown;
                wb.waypointIndex = i;
                ControlMover.Init(wb, ControlMover.Direction.Any);
                wb.Refresh();
            }
            DrawMiniMapVeiwZone();
            pctBox_mapImage.Refresh();
        }

        public void SetWaypointPosition(WaypointBeacon wb, Point wpLocation)
        {
            wb.Location = wpLocation;
            wb.WaypointLink.Position = wpLocation;
        }

        public void SetWaypointPosition(WayPoint wp, Point wpLocation)
        {
            WaypointBeacon linkedWB;
            linkedWB = waypointBeacons.Find(
                 delegate(WaypointBeacon wb)
                 {
                     return wb.WaypointLink == wp;
                 });
            if (linkedWB != null)
            {
                SetWaypointPosition(linkedWB, wpLocation);
            }
        }

        private void btn_addWay_Click(object sender, EventArgs e)
        {
            AddWay();
        }

        private void btn_removeWay_Click(object sender, EventArgs e)
        {
            RemoveWay();
        }

        void AddWay()
        {
            Way newWay = new Way();
            newWay.name = "New Way " + new Random().Next(1000, 9999).ToString();
            currentMap.Ways.Add(newWay);
            RefreshSelectedWayCombobox();
            combobox_SelectedWay.SelectedIndex = combobox_SelectedWay.Items.Count - 1;
        }

        void RemoveWay()
        {
            if (currentMap.Ways.Count > 1)
            {
                int lastSelectedIndex = combobox_SelectedWay.SelectedIndex;
                currentMap.Ways.RemoveAt(lastSelectedIndex);
                RefreshSelectedWayCombobox();
                if (lastSelectedIndex < currentMap.Ways.Count)
                {
                    combobox_SelectedWay.SelectedIndex = lastSelectedIndex;
                }
                else
                {
                    combobox_SelectedWay.SelectedIndex = lastSelectedIndex - 1;
                }
            }
        }

        private void combobox_SelectedWay_Click(object sender, EventArgs e)
        {
            int lastSelectedIndex = combobox_SelectedWay.SelectedIndex;
            RefreshSelectedWayCombobox();
            combobox_SelectedWay.SelectedIndex = lastSelectedIndex;
        }

        public void RefreshSelectedWayCombobox()
        {
            combobox_SelectedWay.Items.Clear();
            for (int i = 0; i < currentMap.Ways.Count; i++)
            {
                combobox_SelectedWay.Items.Add(currentMap.Ways[i].name);
            }
        }

        private void pctBox_mapImage_MouseDown(object sender, MouseEventArgs e)
        {
            propGrid_properties.SelectedObject = currentMap;
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

        public List<string> GetWays()
        {
            List<string> ret = new List<string>();

            for (int i = 0; i < currentMap.Ways.Count; i++)
            {
                ret.Add(currentMap.Ways[i].name);
            }

            return ret;
        }

        public void SaveCurrentMap()
        {
            MapsCollection mapsCollection = Form_selectAction.Instance.loadedMaps;

            if (!mapsCollection.maps.Contains(currentMap))
            {
                mapsCollection.maps.Add(currentMap);
            }
            XMLManager.Instance.Save("maps.xml", mapsCollection, typeof(MapsCollection));
        }
    }

    class ControlMover
    {
        public enum Direction
        {
            Any,
            Horizontal,
            Vertical
        }

        public static void Init(Control control)
        {
            Init(control, Direction.Any);
        }

        public static void Init(Control control, Direction direction)
        {
            Init(control, control, direction);
        }

        public static void Init(Control control, Control container, Direction direction)
        {
            Init(control, container, control.Parent, direction);
        }

        public static void Init(Control control, Control container, Control bounds, Direction direction)
        {
            bool Dragging = false;
            Point DragStart = Point.Empty;
            control.MouseDown += delegate(object sender, MouseEventArgs e)
            {
                Dragging = true;
                DragStart = new Point(e.X, e.Y);
                control.Capture = true;
            };
            control.MouseUp += delegate(object sender, MouseEventArgs e)
            {
                Dragging = false;
                control.Capture = false;
            };
            control.MouseMove += delegate(object sender, MouseEventArgs e)
            {
                if (Dragging)
                {

                    int x = container.Location.X;
                    if (direction != Direction.Vertical)
                    {
                        x = Math.Max(0, e.X + container.Location.X - DragStart.X);
                        if (x > bounds.Size.Width - container.Width)
                        {
                            x = bounds.Size.Width - container.Width;
                        }
                    }
                    int y = container.Location.Y;
                    if (direction != Direction.Horizontal)
                    {
                        y = Math.Max(0, e.Y + container.Location.Y - DragStart.Y);
                        if (y > bounds.Size.Height - container.Height)
                        {
                            y = bounds.Size.Height - container.Height;
                        }
                    }
                    container.Location = new Point(x, y);

                    /*
                    if (direction != Direction.Vertical)
                    {
                        int left;
                        left = Math.Max(0, e.X + container.Left - DragStart.X);
                        if (left > bounds.Size.Width - container.Width)
                        {
                            left = bounds.Size.Width - container.Width;
                        }
                        container.Left = left;
                    }
                    if (direction != Direction.Horizontal)
                    {
                        int top;
                        top = Math.Max(0, e.Y + container.Top - DragStart.Y);
                        if (top > bounds.Size.Height - container.Height)
                        {
                            top = bounds.Size.Height - container.Height;
                        }
                        container.Top = top;
                    }
                    */
                }
            };
        }
    }
}

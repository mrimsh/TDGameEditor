using GameEditor.ContentInfoClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GameEditor
{
    public partial class Form_selectAction : Form
    {
        #region Singleton
        private static Form_selectAction instance;
        public static Form_selectAction Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public MapsCollection loadedMaps = new MapsCollection();

        public Form_selectAction()
        {
            instance = this;
            InitializeComponent();
        }

        private void btn_editMap_Click(object sender, EventArgs e)
        {
            new form_mapEdit().Show();
        }
    }
}

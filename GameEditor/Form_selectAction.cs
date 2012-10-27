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
        public Form_selectAction()
        {
            InitializeComponent();
        }

        private void btn_editMap_Click(object sender, EventArgs e)
        {
            new form_mapEdit().Show();
        }
    }
}

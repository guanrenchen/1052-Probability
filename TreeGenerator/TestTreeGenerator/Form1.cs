using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TreeGenerator;
using System.Xml;

namespace TestTreeGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            numericUpDown2.ValueChanged += numericUpDown1_ValueChanged;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            TreeData.TreeDataTableDataTable dt = new TreeData.TreeDataTableDataTable();
            int height = (int)numericUpDown1.Value,
                degree = (int)numericUpDown2.Value;
            try
            {
                if((int)Math.Pow(degree, height) > 2048 &&
                    DialogResult.No == MessageBox.Show("This might take a while, Continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    return;
                }
            }catch(Exception ex){
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            dt.AddTreeDataTableRow("0", "", "", "");
            for (int i = 0, id = 1, nodeNum = degree; i < height; ++i, nodeNum*=degree)
                for (int j = 0; j < nodeNum; ++j, ++id)
                    dt.AddTreeDataTableRow(id.ToString(), ((id - 1) / degree).ToString(), "", "");

            TreeBuilder myTree = new TreeBuilder(dt);
            myTree.BoxWidth = 50;
            myTree.BoxHeight = 50;
            myTree.HorizontalSpace = 10;
            myTree.VerticalSpace = 10;
            myTree.LineWidth = 2.5f;
            myTree.Margin = 5;
            BackgroundImage = Image.FromStream(myTree.GenerateTree(-1, -1, "0", System.Drawing.Imaging.ImageFormat.Bmp));
        }
    }
}

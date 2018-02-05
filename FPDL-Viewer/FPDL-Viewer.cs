using FPDL;
using System;
using System.Windows.Forms;

namespace FpdlViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "FPDL files | *.xml";
            openFileDialog1.InitialDirectory="\\";
            openFileDialog1.FileName = "*.xml";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK && openFileDialog1.CheckFileExists)
            {
                IFpdlObject fpdlObj = FpdlReader.Parse(openFileDialog1.FileName);
                richTextBox1.Text = fpdlObj.ToString();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("FPDL Viewer\nNiteworks CDS\nVersion 0.2", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

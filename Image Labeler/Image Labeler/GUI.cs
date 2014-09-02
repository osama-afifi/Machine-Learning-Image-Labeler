using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_Labeler
{
    public partial class GUI : Form
    {
        public GUI()
        {

            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

        }



        private string SelectFolder(string initialDirectory = "C:\\")
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            return (dialog.ShowDialog() == DialogResult.OK) ? dialog.SelectedPath : null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = SelectFolder();

            string[] fileList = Directory.GetFiles(textBox1.Text, "*.jpg", SearchOption.TopDirectoryOnly);
            foreach (string file in fileList)
                listBox1.Items.Add(file);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string []fileList = Directory.GetFiles(textBox1.Text, "*.jpg", SearchOption.TopDirectoryOnly);
            foreach (string file in fileList)
                listBox1.Items.Add(file);
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
          pictureBox1.Image = Image.FromFile(listBox1.SelectedItem.ToString());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void GUI_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox2.Text = SelectFolder();
        }
    }
}

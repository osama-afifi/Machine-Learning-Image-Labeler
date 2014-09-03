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
using dotnetthoughts;

namespace Image_Labeler
{
    public partial class GUI : Form
    {
        TrainItem[] trainArray;
        List<TrainItem> trainList;

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
            try
            {
                textBox1.Text = SelectFolder();

                string[] fileList = Directory.GetFiles(textBox1.Text, "*.jpg", SearchOption.TopDirectoryOnly);
                foreach (string file in fileList)
                    listBox1.Items.Add(file);

                trainArray = new TrainItem[listBox1.Items.Count];
                for (int i = 0; i < trainArray.Length; i++)
                    trainArray[i] = new TrainItem();
            }
            catch
            {
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string[] fileList = Directory.GetFiles(textBox1.Text, "*.jpg", SearchOption.TopDirectoryOnly);
                foreach (string file in fileList)
                    listBox1.Items.Add(file);
                trainArray = new TrainItem[listBox1.Items.Count];
                for (int i = 0; i < trainArray.Length; i++)
                    trainArray[i] = new TrainItem();
            }
            catch
            { }
        }

        void updateItem(int i)
        {

            textBox3.Text = trainArray[i].rect.Width.ToString();
            textBox4.Text = trainArray[i].rect.Height.ToString();

            textBox5.Text = trainArray[i].rect.Y.ToString();
            textBox6.Text = trainArray[i].rect.X.ToString();

            checkBox1.Checked = trainArray[i].positive;
            checkBox2.Checked = trainArray[i].take;
        }


        private void listBox1_Click(object sender, EventArgs e)
        {
            Image img = Image.FromFile(listBox1.SelectedItem.ToString());
            pictureBox1.Image = img;
            updateItem(listBox1.SelectedIndex);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void GUI_Load(object sender, EventArgs e)
        {

        }

        // Save Results
        private void button3_Click(object sender, EventArgs e)
        {
            // if not cropped take all
            for (int i = 0; i < trainArray.Length; i++)
            {
                if (trainArray[i].rect == new Rectangle())
                {
                    Image img = Image.FromFile(listBox1.Items[i].ToString());
                    trainArray[i].rect = new Rectangle(0, 0, img.Width, img.Height);
                }
                trainArray[i].imgDir = listBox1.Items[i].ToString();
            }

            trainList = new List<TrainItem>();
            for (int i = 0; i < trainArray.Length; i++)
                if (trainArray[i].take)
                {
                    trainList.Add(trainArray[i]);
                }

            try
            {
                saveResults();
                MessageBox.Show("Results saved successfully");
            }
            catch {
                MessageBox.Show("Error Saving Results");
            }
        }

        private static Image cropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            return bmpImage.Clone(cropArea, bmpImage.PixelFormat);
        }

        void saveResults()
        {
            String dest = textBox2.Text;

            var csv = new StringBuilder();

            for (int i = 0; i < trainList.Count; i++)
            {
                TrainItem t = trainList[i];
                // labeling
                int label = (t.positive) ? 1 : 0;
                var newLine = string.Format(label.ToString());
                csv.Append(newLine);
                // cropping
                Image src = Image.FromFile(t.imgDir);
                Image target = cropImage(src, t.rect);
                target.Save(Path.Combine(dest, "train_" + i.ToString()));
            }

            // label file writing
            File.WriteAllText(Path.Combine(dest, "label.txt"), csv.ToString());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox2.Text = SelectFolder();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            trainArray[listBox1.SelectedIndex].rect = pictureBox1.getRect();
            trainArray[listBox1.SelectedIndex].positive = checkBox1.Checked;
            trainArray[listBox1.SelectedIndex].take = checkBox2.Checked;
            updateItem(listBox1.SelectedIndex);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile(listBox1.SelectedItem.ToString());
            updateItem(listBox1.SelectedIndex);
        }
    }
}

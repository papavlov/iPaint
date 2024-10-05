using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iPaint
{
    public partial class StartScreen : Form
    {
        public StartScreen()
        {
            InitializeComponent();
        }

        Random random = new Random();
        private void button1_Click(object sender, EventArgs e)
        {
            int chance = random.Next(0, 100);
            var fileName = Microsoft.VisualBasic.Interaction.InputBox("Enter file name", "iPaint");

            if (!string.IsNullOrWhiteSpace(fileName))
            {
                DrawingForm drawingForm = new DrawingForm(this, fileName + ".drawing", new ShapesManager());

                drawingForm.Show();
                this.Hide();
             
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = ".";
                openFileDialog.Filter = "Drawing files (*.drawing)|*.drawing";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (var reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }

            var shapesManager = new ShapesManager();

            if (!string.IsNullOrWhiteSpace(filePath))
            {
                shapesManager.ReadShapes(filePath);

                DrawingForm drawingForm = new DrawingForm(this, filePath, shapesManager);

                drawingForm.Show();
               this.Hide();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Environment.Exit(0);
            }
        }
    }
}

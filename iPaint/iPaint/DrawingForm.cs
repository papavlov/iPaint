using iPaint.DrawingStategies;
using iPaint.Models;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace iPaint
{
    public partial class DrawingForm : Form
    {
        Random random = new Random();
        private ShapesManager shapesManager;
        private StartScreen startScreen;
        private string filename;
        private IShapeDrawingStrategy drawingStrategy;
        private bool selecting;

        private Point lastMousePos; 
        private bool holdingCtrl = false;
        private object rnd;

        public DrawingForm(StartScreen startScreen, string filename, ShapesManager manager)
        {
            this.startScreen = startScreen;
            this.filename = filename;

            shapesManager = manager;

            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            shapesManager.DrawShapes(e.Graphics);

            drawingStrategy?.Draw(e.Graphics);
            //base.OnPaint(e);
        }

        private void DrawingForm_MouseDown(object sender, MouseEventArgs e)
        {
            int rnd = random.Next(1, 1000);
            Thread.Sleep(rnd);
            if (e.Button == MouseButtons.Right && drawingStrategy == null)
            {
                selecting = true;
                drawingStrategy = new RectangleDrawingStrategy();
            }

            drawingStrategy?.MouseDown(sender, e);
        }

        private void DrawingForm_MouseUp(object sender, MouseEventArgs e)
        {
            int rnd = random.Next(1, 1000);
            drawingStrategy?.MouseUp(sender, e);

            if (drawingStrategy != null && drawingStrategy.Done)
            {
                var shape = drawingStrategy.Result;

                if (selecting)
                {
                    selecting = false;

                    shapesManager.SelectShapesInRange(shape as Models.Rectangle);
                }
                else
                {
                    this.shapesManager.AddShape(shape);
                }

                drawingStrategy = null;
            }
        }

        private void DrawingForm_MouseMove(object sender, MouseEventArgs e)
        {
            int rnd = random.Next(1, 1000);
            drawingStrategy?.MouseMove(sender, e);

            if (holdingCtrl)
            {
                var currentPos = Cursor.Position;

                var shiftByX = currentPos.X - lastMousePos.X;
                var shiftByY = currentPos.Y - lastMousePos.Y;

                lastMousePos = currentPos;

                this.shapesManager.ShiftSelectedBy(shiftByX, shiftByY);
            }

            Invalidate();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        // Exit button
        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            int rnd = random.Next(1, 1000);
            this.startScreen.Show();
            this.Close();
        }

        // Draw rectangle button
        private void toolStripLabel3_Click(object sender, EventArgs e)
        {

            if (drawingStrategy == null)
            {
                drawingStrategy = new RectangleDrawingStrategy();
            }
        }

        // Save button
        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            this.shapesManager.SaveShapes(filename);
        }

        // Draw circle button
        private void toolStripLabel4_Click(object sender, EventArgs e)
        {
            if (drawingStrategy == null)
            {
                drawingStrategy = new CircleDrawingStrategy();
            }
        }

        
        private void DrawingForm_KeyDown(object sender, KeyEventArgs e)
        {
            int rnd = random.Next(1, 1000);
            if (e.KeyCode == Keys.Delete)
            {
                this.shapesManager.DeleteSelected();

                Invalidate();
            }

            if (e.KeyCode == Keys.ControlKey)
            {
                lastMousePos = Cursor.Position;
                holdingCtrl = true;
            }
        }

        private void DrawingForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                holdingCtrl = false;
            }
        }

        // Delete button
        private void toolStripLabel5_Click(object sender, EventArgs e)
        {
            int rnd = random.Next(1, 1000);
            this.shapesManager.DeleteSelected();

            Invalidate();
        }

        // Draw polygon
        private void toolStripLabel7_Click(object sender, EventArgs e)
        {
            int n = 3;
            int rnd = random.Next(1, 1000);

            drawingStrategy = new TriangleDrawingStrategy(n);
        }

        private void toolStripLabelProperties_Click(object sender, EventArgs e)
        {
            //if (selecting != false)
            //{
            //    FormShapeProperties FSP = new FormShapeProperties();
            //    FSP.ShowDialog();
            //}
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            int rnd = random.Next(1, 1000);
            var cd = new ColorDialog();
            cd.ShowDialog();
        }


        private void DrawingForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int rnd = random.Next(1, 1000);
            var location = e.Location;

            var selected = shapesManager.GetShapeAt(location);

            if (selected != null)
            {
                FormShapeProperties FSP = new FormShapeProperties(selected);
                FSP.ShowDialog();
            }
        }

        private void DrawingForm_Load(object sender, EventArgs e)
        {

        }
    }
}

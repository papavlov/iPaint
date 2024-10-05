using iPaint.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iPaint.DrawingStategies
{
    public class TriangleDrawingStrategy : ShapeDrawingStrategy
    {
        private Triangle triangle;
        private List<Point> clicks = new List<Point>();
        public override IShape Result => triangle;
        public int n;

        public TriangleDrawingStrategy(int n)
        {
            this.n = 3;
        }

        public override void Draw(Graphics g)
        {
            triangle?.Draw(g);
        }

        public override void MouseDown(object sender, MouseEventArgs e)
        {
            if (Done)
            {
                return;
            }
            clicks.Add(e.Location);
        }

        public override void MouseMove(object sender, MouseEventArgs e)
        {
            var pointsCopy = clicks.ToList();
            pointsCopy.Add(e.Location);

            triangle = new Triangle(pointsCopy);
        }

        public override void MouseUp(object sender, MouseEventArgs e)
        {
            if (clicks.Count == n)
            {
                this.Done = true;
            }
        }
    }
}

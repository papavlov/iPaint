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
    public class CircleDrawingStrategy : ShapeDrawingStrategy
    {
        private bool selectedStart = false;

        private Point centre;
        private Point end;

        private Circle circle;

        public override IShape Result => circle;

        public override void Draw(Graphics g)
        {
            circle?.Draw(g);
        }

        public override void MouseDown(object sender, MouseEventArgs e)
        {
            if (Done)
            {
                return;
            }
            if (!selectedStart)
            {
                centre = e.Location;
                selectedStart = true;
                circle = new Circle(new Point(centre.X, centre.Y), 1);
                return;
            }
            end = e.Location;
        }

        public override void MouseMove(object sender, MouseEventArgs e)
        {
            if (Done || !selectedStart)
            {
                return;
            }

            this.end = e.Location;

            var x1 = centre.X;
            var x2 = end.X;
            var y1 = centre.Y;
            var y2 = end.Y;

            var distance = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
            

            circle = new Circle(new Point(centre.X, centre.Y), distance);
        }

        public override void MouseUp(object sender, MouseEventArgs e)
        {
            Done = true;
        }
    }
}

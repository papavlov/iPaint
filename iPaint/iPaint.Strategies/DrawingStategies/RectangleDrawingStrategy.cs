using iPaint.Models;
using System;
using System.Drawing;
using System.Windows.Forms;
using Rectangle = iPaint.Models.Rectangle;

namespace iPaint.DrawingStategies
{
    public class RectangleDrawingStrategy : ShapeDrawingStrategy
    {
        private bool started = false;

        private Point selectStartPoint;
        private Point selectEndPoint;

        private Rectangle rectangle;

        public override IShape Result => rectangle;

        public override void Draw(Graphics g)
        {
            rectangle?.Draw(g);
        }

        public override void MouseDown(object sender, MouseEventArgs e)
        {
            if (Done)
            {
                return;
            }
            selectStartPoint = e.Location;
            started = true;
        }

        public override void MouseMove(object sender, MouseEventArgs e)
        {
            if (Done || !started)
            {
                return;
            }

            this.selectEndPoint = e.Location;

            var width = selectEndPoint.X - selectStartPoint.X;
            var height = selectEndPoint.Y - selectStartPoint.Y;
            var startPoint = new Point(selectStartPoint.X, selectStartPoint.Y);

            if (width < 0)
            {
                startPoint.X = selectEndPoint.X;
            }
            if (height < 0)
            {
                startPoint.Y = selectEndPoint.Y;
            }

            width = Math.Abs(width);
            height = Math.Abs(height);

            rectangle = new Rectangle(startPoint, width, height);
        }

        public override void MouseUp(object sender, MouseEventArgs e)
        {
            this.Done = true;
        }
    }
}

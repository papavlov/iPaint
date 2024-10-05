using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPaint.Models
{
    [Serializable]
    public class Rectangle : Shape
    {
        private Point startLocation;
        private int width;
        private int height;

        public Rectangle(Point startLocation, int width, int height)
        {
            this.startLocation = startLocation;
            this.width = width;
            this.height = height;
        }

        public override double Area => this.width * this.height;

        public override Point Center => new Point(startLocation.X + width / 2, startLocation.Y + height / 2);

        public override void Draw(Graphics g)
        {
            using (var brush = new SolidBrush(this.Fill))
            {
                g.FillRectangle(brush, startLocation.X, startLocation.Y, width, height);
            }
            using (var pen = new Pen(this.Border, 5))
            {
                g.DrawRectangle(pen, startLocation.X, startLocation.Y, width, height);
            }
        }

        public override bool Includes(Point p)
        {
            return p.X >= startLocation.X && p.X <= startLocation.X + width && p.Y >= startLocation.Y && p.Y <= startLocation.Y + height;
        }

        public override void ShiftBy(int xAmount, int yAmount)
        {
            this.startLocation.X += xAmount;
            this.startLocation.Y += yAmount;
        }
    }
}

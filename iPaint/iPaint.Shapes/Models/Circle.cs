using System;
using System.Drawing;

namespace iPaint.Models
{
    [Serializable]
    public class Circle : Shape
    {
        private double radius;

        public Circle(Point location, double radius)
        {
            this.Center = location;
            this.radius = radius;
        }

        public override double Area => Math.PI * Math.Pow(radius, 2);

        public override void Draw(Graphics g)
        {
            using (var brush = new SolidBrush(this.Fill))
            {
                g.FillEllipse(brush, Center.X - (int)radius, Center.Y - (int)radius, (float)radius * 2, (float)radius * 2);
            }
            using (var pen = new Pen(this.Border, 3))
            {
                g.DrawEllipse(pen, Center.X - (int)radius, Center.Y - (int)radius, (float)radius * 2, (float)radius * 2);
            }
        }

        public override bool Includes(Point p)
        {
            var distance = Math.Sqrt(Math.Pow(this.Center.X - p.X, 2) + Math.Pow(this.Center.Y - p.Y, 2));

            return distance <= this.radius;
        }

        public override void ShiftBy(int xAmount, int yAmount)
        {
            this.Center = new Point(this.Center.X + xAmount, this.Center.Y + yAmount);
        }
    }
}

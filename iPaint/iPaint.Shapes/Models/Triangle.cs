using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace iPaint.Models
{
    [Serializable]
    public class Triangle : Shape
    {
        private List<Point> points;

        public override double Area
        {
            get
            {
                var p1 = points[0];
                var p2 = points[1];
                var p3 = points[2];

                var s1 = Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
                var s2 = Math.Sqrt(Math.Pow(p3.X - p2.X, 2) + Math.Pow(p3.Y - p2.Y, 2));
                var s3 = Math.Sqrt(Math.Pow(p1.X - p3.X, 2) + Math.Pow(p1.Y - p3.Y, 2));

                var p = (s1 + s2 + s3) / 2;

                var result = Math.Sqrt(p * (p - s1) * (p - s2) * (p - s3));

                return result;
            }
        }

        public override Point Center => new Point((int)points.Average(p => p.X), (int)points.Average(p => p.Y));

        public Triangle(List<Point> points)
        {
            this.points = points;
        }

        public override void Draw(Graphics g)
        {
            using (var brush = new SolidBrush(this.Fill))
            {
                g.FillPolygon(brush, this.points.ToArray());
            }
            using (var pen = new Pen(this.Border, 3))
            {
                for (int i = 0; i < points.Count - 1; i++)
                {
                    g.DrawLine(pen, points[i], points[i + 1]);
                }

                g.DrawLine(pen, points[points.Count - 1], points[0]);
            }
        }

        public override void ShiftBy(int xAmount, int yAmount)
        {
            points = points.Select(p => new Point(p.X + xAmount, p.Y + yAmount)).ToList();
        }

        // https://stackoverflow.com/questions/2049582/how-to-determine-if-a-point-is-in-a-2d-triangle
        public override bool Includes(Point p)
        {
            var d1 = Sign(p, points[0], points[1]);
            var d2 = Sign(p, points[1], points[2]);
            var d3 = Sign(p, points[2], points[0]);

            var has_neg = (d1 < 0) || (d2 < 0) || (d3 < 0);
            var has_pos = (d1 > 0) || (d2 > 0) || (d3 > 0);

            return !(has_neg && has_pos);
        }

        private int Sign(Point p1, Point p2, Point p3)
        {
            return (p1.X - p3.X) * (p2.Y - p3.Y) - (p2.X - p3.X) * (p1.Y - p3.Y);
        }
    }
}

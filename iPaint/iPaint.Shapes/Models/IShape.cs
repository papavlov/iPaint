using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace iPaint.Models
{
    public interface IShape
    {
        Point Center { get; }

        bool Selected { get; set; }

        double Area { get; }

        Color Border { get; }

        Color Fill { get; set; }

        void Draw(Graphics g);

        void ShiftBy(int xAmount, int yAmount);

        bool Includes(Point p);
    }
}

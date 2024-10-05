using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPaint.Models
{
    [Serializable]
    public abstract class Shape : IShape
    {
        public virtual Point Center { get; protected set; }

        public bool Selected { get; set; }

        public abstract double Area { get; }
        public Color Border => this.Selected ? (this.Fill == Color.Red ? Color.Green : Color.Red) : this.Fill;

        public Color Fill { get; set; } = Color.DarkGray;

        public abstract void Draw(Graphics g);

        public abstract bool Includes(Point p);

        public abstract void ShiftBy(int xAmount, int yAmount);
    }
}

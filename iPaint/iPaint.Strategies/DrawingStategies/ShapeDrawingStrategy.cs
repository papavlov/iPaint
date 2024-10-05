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
    public abstract class ShapeDrawingStrategy : IShapeDrawingStrategy
    {
        public abstract IShape Result { get; }

        public bool Done { get; protected set; }

        public abstract void Draw(Graphics g);

        public abstract void MouseDown(object sender, MouseEventArgs e);

        public abstract void MouseMove(object sender, MouseEventArgs e);

        public abstract void MouseUp(object sender, MouseEventArgs e);
    }
}

using iPaint.Models;
using System.Drawing;
using System.Windows.Forms;

namespace iPaint.DrawingStategies
{
    public interface IShapeDrawingStrategy
    {
        void MouseUp(object sender, MouseEventArgs e);

        void MouseDown(object sender, MouseEventArgs e);

        void MouseMove(object sender, MouseEventArgs e);

        void Draw(Graphics g);

        IShape Result { get; }

        bool Done { get; }

    }
}

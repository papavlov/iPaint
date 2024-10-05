using iPaint.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Rectangle = iPaint.Models.Rectangle;

namespace iPaint
{
    public class ShapesManager
    {
        private List<IShape> shapes = new List<IShape>();

        public void DrawShapes(Graphics g)
        {
            foreach (var shape in shapes)
            {
                shape.Draw(g);
            }
        }

        public void AddShape(IShape shape)
        {
            this.shapes.Add(shape);
        }

        public void SaveShapes(string filename)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new FileStream(filename, FileMode.Create))
            {
                formatter.Serialize(stream, shapes);
            }
        }

        public void ReadShapes(string filename)
        {
            if (!File.Exists(filename))
            {
                return;
            }

            var formatter = new BinaryFormatter();
            using (var stream = new FileStream(filename, FileMode.Open))
            {
                shapes = (List<IShape>)formatter.Deserialize(stream);
            }
        }

        public void SelectShapesInRange(Rectangle rect)
        {
            shapes.ForEach(s =>
            {
                s.Selected = false;
            });

            if (rect != null)
            {
                var shapesInRect = shapes.Where(s => rect.Includes(s.Center)).ToList();

                foreach (var shape in shapesInRect)
                {
                    shape.Selected = true;
                }
            }
        }

        public void ShiftSelectedBy(int xAmount, int yAmount)
        {
            var selected = shapes.Where(s => s.Selected).ToList();

            selected.ForEach(s =>
            {
                s.ShiftBy(xAmount, yAmount);
            });
        }

        public void DeleteSelected()
        {
            var selected = shapes.Where(s => s.Selected).ToList();

            shapes = shapes.Except(selected).ToList();
        }

        public IShape GetShapeAt(Point location)
        {
            return this.shapes.FirstOrDefault(s => s.Includes(location));
        }
    }
}

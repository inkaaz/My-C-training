using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Figures
{
    class Program
    {
        static void Main(string[] args)
        {
            var canvas = new ObjectCanvas();

            canvas.PrintAllFigures();

            canvas.AddFigure(new Rectangle(5, 10));
            canvas.AddFigure(new Ellipse(5, 10));
            canvas.AddFigure(new LineSegment(5));

            canvas.PrintAllFigures();

            canvas.Move(1, 40, 20);
            canvas.Remove(0);
            canvas.Resize(2, 10);

            canvas.PrintAllFigures();
            canvas.AddFigure(new Ellipse(5, 10));
            
            canvas.Undo(2);

            canvas.AddFigure(new LineSegment(5));
            canvas.PrintAllFigures();

        }
    }
}

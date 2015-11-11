using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Figures
{
    public interface IFigure
    {
        int ID {get; set;}
        double XCoordinate { get; set; }
        double YCoordinate { get; set; }
        double Angle { get; set; }

        void RotateClockwize(double angle);
        void Move(double x, double y);
        void Resize(double k);

        void Print();
    }
}

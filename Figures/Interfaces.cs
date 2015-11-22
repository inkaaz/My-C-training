using System.Collections.Generic;
namespace Figures
{


    public interface IFigure
    {
        int ID {get; set;}
        double CenterX { get; }
        double CenterY { get; }
        Orientation FigureOrientation { get; }

        void RotateClockwise();
        void RotateAntiClockwise();
        void Move(double x, double y);
        void Resize(double k);

        void Print();
    }

    public interface ICanvas 
    {
        Dictionary<int, IFigure> FigureDic { get; set; }
        void DecreaseMaxId();
    }
}

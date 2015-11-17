namespace Figures
{
    public interface IFigure
    {
        int ID {get; set;}
        double CenterX { get; }
        double CenterY { get; }
        Orientation FigureOrientation { get; }

        void RotateClockwise();
        void Move(double x, double y);
        void Resize(double k);

        void Print();
    }
}

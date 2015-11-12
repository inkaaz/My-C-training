using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Figures
{
    public abstract class Figure: IFigure
    {
        public int ID { get; set; }
        public double XCoordinate { get; set; }
        public double YCoordinate { get; set; }
        public double Angle { get; set; }

        public abstract void RotateClockwise(double angle);

        public void Move(double x, double y)
        {
            this.XCoordinate += x;
            this.YCoordinate += y;
        }

        public abstract void Resize(double k);

        public virtual void Print()
        {
            Console.WriteLine();
            Console.WriteLine("I am {0}, ID = {1}", this.GetType(),ID);
            Console.WriteLine("XCoordinate is {0}", XCoordinate);
            Console.WriteLine("YCoordinate is {0}", YCoordinate);
            Console.WriteLine("Angle is {0}", Angle);
        }
    }

    public class Rectangle : Figure
    {
        public Rectangle(double x, double y, int a, int b)
        {
            XCoordinate = x;
            YCoordinate = y;
            SideA = a;
            SideB = b;
        }

        public Rectangle(int a, int b) : this(0, 0, a, b) { }

        public double SideA { get; set; }
        public double SideB { get; set; }

        public override void RotateClockwise(double angle)
        {
            double halfDiagonal = Math.Sqrt(Math.Pow(SideA / 2, 2) + Math.Pow(SideB / 2, 2));
            this.Angle += angle;
            this.XCoordinate = XCoordinate - Math.Cos(angle) * halfDiagonal / 2;
            this.XCoordinate = XCoordinate - Math.Sin(angle) * halfDiagonal / 2;
        }

        public override void Resize(double k)
        {
            SideA = SideA * k;
            SideB = SideB * k;
        }

        public override void Print()
        {
            base.Print();
            Console.WriteLine("SideA is {0}", SideA);
            Console.WriteLine("SideB is {0}", SideB);
        }
    }

    public class Ellipse : Figure
    {
        public Ellipse(double x, double y, int a, int b)
        {
            XCoordinate = x;
            YCoordinate = y;
            RadiusA = a;
            RadiusA = b;
        }

        public override void RotateClockwise(double angle) { }

        public Ellipse(int a, int b) : this(0, 0, a, b) { }

        public double RadiusA { get; set; }
        public double RadiusB { get; set; }

        public override void Resize(double k)
        {
            RadiusA = RadiusA * k;
            RadiusB = RadiusB * k;
        }

        public override void Print()
        {
            base.Print();
            Console.WriteLine("RadiusA is {0}", RadiusA);
            Console.WriteLine("RadiusB is {0}", RadiusB);
        }
    }

    public class LineSegment : Figure
    {
        public LineSegment(double x, double y, int a)
        {
            XCoordinate = x;
            YCoordinate = y;
            Length = a;
        }

        public LineSegment(int a) : this(0, 0, a) { }

        public double Length { get; set; }
        public override void Resize(double k)
        {
            Length = Length * k;
        }

        public override void RotateClockwise(double angle)
        {
            this.Angle += angle;
            this.XCoordinate = XCoordinate - Math.Cos(angle) * Length / 2;
            this.XCoordinate = XCoordinate - Math.Sin(angle) * Length / 2;
        }

        public override void Print()
        {
            base.Print();
            Console.WriteLine("Length is {0}", Length);
        }

    }
}

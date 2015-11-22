using System;

namespace Figures
{
    public abstract class Figure: IFigure
    {
#region properties
		
        public int ID { get; set; }
        protected double centerX;
        protected double centerY;

        protected Orientation orientation;
        
        public double CenterX
        {
            get { return centerX; }
        }

        public double CenterY
        {
            get { return centerY; }
        }

        public Orientation FigureOrientation
        {
            get { return orientation; }
        }
 
	#endregion

        public void RotateClockwise()
        {      
            if (orientation == Orientation.Horizontal)
            {
                orientation = Orientation.Vertical;
            }
            else
            {
                orientation = Orientation.Horizontal;
            }        
        }

        public void RotateAntiClockwise()
        {
            RotateClockwise();
        }

        public void Move(double x, double y)
        {
            this.centerX += x;
            this.centerY += y;
        }

        public abstract void Resize(double k);

        public virtual void Print()
        {
            Console.WriteLine();
            Console.WriteLine("I am {0}, ID = {1}", this.GetType(),ID);
            Console.WriteLine("CenterX is {0}", centerX);
            Console.WriteLine("CenterY is {0}", centerY);
            Console.WriteLine("Orientation is {0}", orientation);
        }
    }

    public class Rectangle : Figure
    {
        public Rectangle(double x, double y, int width, int length)
        {
            centerX = x;
            centerY = y;
            sideA = width;
            sideB = length;
            if (sideA > sideB)
            { 
                orientation = Orientation.Horizontal;
            }
            else
            {
                orientation = Orientation.Vertical;
            }

        }

        public Rectangle(int width, int length) : this(0, 0, width, length) { }

        private double sideA;
        private double sideB;        

        public override void Resize(double k)
        {
            sideA = sideA * k;
            sideB = sideB * k;
        }

        public override void Print()
        {
            base.Print();
            Console.WriteLine("SideA is {0}", sideA);
            Console.WriteLine("SideB is {0}", sideB);
        }
    }

    public class Ellipse : Figure
    {
        public Ellipse(double x, double y, int width, int length)
        {
            centerX = x;
            centerY = y;
            radiusA = width;
            radiusB = length;
            if (radiusA > radiusB)
            {
                orientation = Orientation.Horizontal;
            }
            else
            {
                orientation = Orientation.Vertical;
            }
        }

        //public override void RotateClockwise(double angle) { }

        public Ellipse(int a, int b) : this(0, 0, a, b) { }

        private double radiusA;
        private double radiusB;

        public override void Resize(double k)
        {
            radiusA = radiusA * k;
            radiusB = radiusB * k;
        }

        public override void Print()
        {
            base.Print();
            Console.WriteLine("RadiusA is {0}", radiusA);
            Console.WriteLine("RadiusB is {0}", radiusB);
        }
    }

    public class LineSegment : Figure
    {
       

        public LineSegment(double x, double y, int length)
        {
            centerX = x;
            centerY = y;
            this.length = length;
            orientation = Orientation.Horizontal;
        }

        public LineSegment(int a) : this(0, 0, a) { }

        private double length;
        public override void Resize(double k)
        {
            length = length * k;
        }
        
        public override void Print()
        {
            base.Print();
            Console.WriteLine("Length is {0}", length);
        }

    }
}

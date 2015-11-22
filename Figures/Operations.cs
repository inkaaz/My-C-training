using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Figures
{
    public interface IOperation 
    {
        IOperation GetRevertOperation();
        // void Apply(ICanvas canvas);
    }

    public interface ICanvasOperation : IOperation
    {
        void ApplyToCanvas(ICanvas canvas);
    }

    public interface IFigureOperation : IOperation
    {
        void ApplyToFigure(IFigure figure);
    }

    public class ResizeOperation : IFigureOperation
    {
        double k;
        public ResizeOperation(double k)
        {
            this.k = k;
        }

        public void ApplyToFigure(IFigure figure)
        {
            figure.Resize(k);
        }

        public IOperation GetRevertOperation()
        {
            return new ResizeOperation(1 / k);
        }
    }

    public class MoveOperation : IFigureOperation
    {
        double x;
        double y;
        public MoveOperation(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public void ApplyToFigure(IFigure figure)
        {
            figure.Move(x, y);
        }

        public IOperation GetRevertOperation()
        {
            return new MoveOperation(-x, -y);
        }
    }

    public class RotateClockwizeOperation : IFigureOperation
    {
        public void ApplyToFigure(IFigure figure)
        {
            figure.RotateClockwise();
        }

        public IOperation GetRevertOperation()
        {
            return new RotateAntiClockwizeOperation();
        }
    }

    public class RotateAntiClockwizeOperation : IFigureOperation
    {
        public void ApplyToFigure(IFigure figure)
        {
            figure.RotateAntiClockwise();
        }

        public IOperation GetRevertOperation()
        {
            return new RotateClockwizeOperation();
        }
    }


    public class AddFigureOperation : ICanvasOperation
    {
        IFigure figure;
        public AddFigureOperation(IFigure figure)
        {
            this.figure = figure;            
        }

        public void ApplyToCanvas(ICanvas canvas)
        {
            if (!canvas.FigureDic.ContainsKey(figure.ID))
            {
                canvas.FigureDic.Add(figure.ID, figure);
            }
            else 
            {
                throw new ArgumentException(string.Format("The figure with ID = {0} already exists", figure.ID));
            }
        }

        public IOperation GetRevertOperation()
        {
            return new RemoveFigureOperation(figure);
        }
    }

    public class RemoveFigureOperation : ICanvasOperation
    {
        IFigure figure;
        public RemoveFigureOperation(IFigure figure)
        {
            this.figure = figure;
        }

        public void ApplyToCanvas(ICanvas canvas)
        {
            if (canvas.FigureDic.ContainsKey(figure.ID))
            {
                canvas.FigureDic.Remove(figure.ID);
            }
            else
            {
                throw new ArgumentOutOfRangeException(string.Format("There is no figure with ID {0}", figure.ID)); 
            }
        }

        public IOperation GetRevertOperation()
        {
            return new AddFigureOperation(figure);
        }
    }
}

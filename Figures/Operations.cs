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
        void Apply(ObjectCanvas canvas, IFigure figure);
    }

    //public interface ICanvasOperation : IOperation
    //{
    //    void ApplyToCanvas(ICanvas canvas);
    //}

    //public interface IFigureOperation : IOperation
    //{
    //    void ApplyToFigure(IFigure figure);
    //}

    public class ResizeOperation : IOperation
    {
        double k;
        public ResizeOperation(double k)
        {
            this.k = k;
        }

        public void Apply(ObjectCanvas canvas, IFigure figure)
        {
            figure.Resize(k);
        }

        public IOperation GetRevertOperation()
        {
            return new ResizeOperation(1 / k);
        }
    }

    public class MoveOperation : IOperation
    {
        double x;
        double y;
        public MoveOperation(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public void Apply(ObjectCanvas canvas, IFigure figure)
        {
            figure.Move(x, y);
        }

        public IOperation GetRevertOperation()
        {
            return new MoveOperation(-x, -y);
        }
    }

    public class RotateClockwizeOperation : IOperation
    {
        public void Apply(ObjectCanvas canvas, IFigure figure)
        {
            figure.RotateClockwise();
        }

        public IOperation GetRevertOperation()
        {
            return new RotateAntiClockwizeOperation();
        }
    }

    public class RotateAntiClockwizeOperation : IOperation
    {
        public void Apply(ObjectCanvas canvas, IFigure figure)
        {
            figure.RotateAntiClockwise();
        }

        public IOperation GetRevertOperation()
        {
            return new RotateClockwizeOperation();
        }
    }


    public class AddFigureOperation : IOperation
    {
        public void Apply(ObjectCanvas canvas, IFigure figure)
        {
            if (figure.ID == 0 && canvas.FigureDic.Count > 0)
            {
                figure.ID = canvas.FigureDic.Max(v => v.Key) + 1;
            }
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
            return new RemoveFigureOperation();
        }
    }

    public class RemoveFigureOperation : IOperation
    {        
        public void Apply(ObjectCanvas canvas, IFigure figure)
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
            return new AddFigureOperation();
        }
    }
}

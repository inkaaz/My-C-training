using System;
using System.Collections.Generic;
using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace Figures
{
    public class ObjectCanvas : ICanvas
    {
        public Dictionary<int, IFigure> FigureDic { get; set; }
        
        private Stack<Tuple<IOperation, IFigure>> undoStack = new Stack<Tuple<IOperation, IFigure>>();
        private int maxID = 0;
      
        public ObjectCanvas()
        {
            FigureDic = new Dictionary<int, IFigure>();
        }

        public void DecreaseMaxId()
        {
            maxID--;
        }

        public void Undo(int times)
        {
            for (int i = 0; i < times; i++)
            {
                var currentUndoTuple = undoStack.Pop();
                var figure = currentUndoTuple.Item2 as IFigure;

                if (currentUndoTuple is IFigureOperation)
                {
                    var undoFigureOperation = currentUndoTuple.Item1.GetRevertOperation() as IFigureOperation;
                    undoFigureOperation.ApplyToFigure(figure);
                }
                else
                {
                    var undoCanvasOperation = currentUndoTuple.Item1.GetRevertOperation() as ICanvasOperation;
                    undoCanvasOperation.ApplyToCanvas(this);
                }
            }        
        }

        public void Resize(int i, double k)
        {
            IFigure figure;
            if (FigureDic.TryGetValue(i, out figure))
            {
                var resizeOperation = new ResizeOperation(k);
                resizeOperation.ApplyToFigure(figure);
                undoStack.Push(new Tuple<IOperation, IFigure>(resizeOperation, figure));
            }
            else 
            {
                throw new ArgumentOutOfRangeException(string.Format("There is no figure with ID {0}", i));            
            }
        }

        public void Move(int i, double deltaX, double deltaY)
        {
            IFigure figure;
            if (FigureDic.TryGetValue(i, out figure))
            {
                var moveOperation = new MoveOperation(deltaX, deltaY);
                moveOperation.ApplyToFigure(figure);
                undoStack.Push(new Tuple<IOperation, IFigure>(moveOperation, figure));
            }
            else
            {
                throw new OutOfMemoryException(string.Format("There is no figure with ID {0}", i));
            }
        }

        public void RotateClockwize(int i)
        {
            IFigure figure;
            if (FigureDic.TryGetValue(i, out figure))
            {
                var rotateClockwizeOperation = new RotateClockwizeOperation();
                rotateClockwizeOperation.ApplyToFigure(figure);
                undoStack.Push(new Tuple<IOperation, IFigure>(rotateClockwizeOperation, figure));
            }
            else
            {
                throw new OutOfMemoryException(string.Format("There is no figure with ID {0}", i));
            }
        }       
        
        public void AddFigure(IFigure figure)
        {
            figure.ID = maxID++;
            var addOperation = new AddFigureOperation(figure);
            addOperation.ApplyToCanvas(this);
            undoStack.Push(new Tuple<IOperation,IFigure>(addOperation, figure));
        }

        public void Remove(int i)
        {
            IFigure figure;
            if (FigureDic.TryGetValue(i, out figure))
            {
                var removeFigureOperation = new RemoveFigureOperation(figure);
                removeFigureOperation.ApplyToCanvas(this);
                if (i == maxID - 1)
                {
                    maxID--;
                }
                undoStack.Push(new Tuple<IOperation, IFigure>(removeFigureOperation, figure));
            }
            else
            {
                throw new OutOfMemoryException(string.Format("There is no figure with ID {0}", i));
            }
        }

      

        public void PrintAllFigures()
        {
            if (FigureDic.Count > 0)
            {
                foreach (var f in FigureDic)
                {
                    f.Value.Print();
                }
            }
            else
            {
                Console.WriteLine("Dictionary is empty");
            }
            
            Console.ReadKey();
        }

    }
}

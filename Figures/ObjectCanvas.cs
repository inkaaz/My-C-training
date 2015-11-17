using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace Figures
{
    public class ObjectCanvas
    {
        private Dictionary<int, IFigure> figureDic;

        public Dictionary<int, IFigure> FigureDic
        {
            get { return figureDic; }
        }

        Stack<ActionWithFigure> undoStack;       

        private int maxID;
        public ObjectCanvas()
        {
            figureDic = new Dictionary<int, IFigure>();
            undoStack = new Stack<ActionWithFigure>();
            maxID = 0;
        }

        public void Undo(int times)
        {
            ActionWithFigure a;
            for (int i = 0; i < times; i++)
            {
                a = undoStack.Pop();
                a.RunFunction();
            }        
        }

        public int? Resize(int i, double k)
        {
            if (!figureDic.ContainsKey(i))
            {
                Console.WriteLine("There is no  figure with id {0}", i);
                return null;
            }
            return ResizeFigure(figureDic[i], k, false);
        }

        private int ResizeFigure(IFigure figure, double k, bool isUndoAction = false)
        {
            if (!isUndoAction)
            {
                ActionWithFigure undoAction = new ActionWithFigure(figure, ResizeFigure, 1/k);
                undoStack.Push(undoAction);
            }

            figure.Resize(k);
            Console.WriteLine("Resize Figure {0}", figure.ID);
            return figure.ID;
        }

        public int? Move(int i, double deltaX, double deltaY)
        {
            if (!figureDic.ContainsKey(i))
            {
                Console.WriteLine("There is no  figure with id {0}", i);
                return null;
            }
            return MoveFigure(figureDic[i], deltaX, deltaY, false);
        }

        private int MoveFigure(IFigure figure, double deltaX, double deltaY, bool isUndoAction = false)
        {
            if (!isUndoAction)
            {
                ActionWithFigure undoAction = new ActionWithFigure(figure, MoveFigure, -deltaX, -deltaY);
                undoStack.Push(undoAction);
            }
            figure.Move(deltaX, deltaY);
            Console.WriteLine("Move Figure {0}", figure.ID);
            return figure.ID;
        }

        public int? RotateClockwize(int i)
        {
            if (!figureDic.ContainsKey(i))
            {
                Console.WriteLine("There is no  figure with id {0}", i);
                return null;
            }
            return RotateClockwize(figureDic[i],  false);
        }       

        private int RotateClockwize(IFigure figure, bool isUndoAction = false)
        {
            if (!isUndoAction)
            {
                ActionWithFigure undoAction = new ActionWithFigure(figure, RotateClockwize);
                undoStack.Push(undoAction);
            }
            figure.RotateClockwise();
            Console.WriteLine("Rotate Figure {0}", figure.ID);
            return figure.ID;
        }
        
        public int AddFigure(IFigure figure, bool isUndoAction = false)
        {            
            if (!isUndoAction)
            {
                figure.ID = maxID++;
                ActionWithFigure undoAction = new ActionWithFigure(figure, RemoveFigure);
                undoStack.Push(undoAction);
                Console.WriteLine("Add Figure {0}", figure.ID);
            }            
            if (!figureDic.ContainsKey(figure.ID))
            {
                figureDic.Add(figure.ID, figure);                
            }
            else
            {
                Console.WriteLine("Error on adding, a figure with ID {0} already exists", figure.ID);
            }
            return figure.ID;
        }

        public int? Remove(int i)
        {
            if (!figureDic.ContainsKey(i))
            {
                Console.WriteLine("There is no  figure with id {0}", i);
                return null;
            }
            return RemoveFigure(figureDic[i], false);
        }

        private int RemoveFigure(IFigure figure, bool isUndoAction = false)
        {
            if (figureDic.ContainsKey(figure.ID))
            {
                figureDic.Remove(figure.ID);
                if (figure.ID == maxID - 1)
                {
                    maxID--;
                }
            }
            if (!isUndoAction)
            {
                ActionWithFigure undoAction = new ActionWithFigure(figure, AddFigure);
                undoStack.Push(undoAction);
                Console.WriteLine("Remove Figure {0}", figure.ID);
            }

            return figure.ID;
        }

        public void PrintAllFigures()
        {
            if (figureDic.Count > 0)
            {
                foreach (var f in figureDic)
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

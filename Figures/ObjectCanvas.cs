using System;
using System.Collections.Generic;
using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace Figures
{
    public class ObjectCanvas
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
                var figure = currentUndoTuple.Item2;
                var undoOperation = currentUndoTuple.Item1.GetRevertOperation();

                undoOperation.Apply(this, figure);
            }
        }

        public void ApplyOperation(IOperation operation, IFigure figure) 
        {
            operation.Apply(this, figure);
            undoStack.Push(new Tuple<IOperation, IFigure>(operation, figure));
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

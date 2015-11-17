
namespace Figures
{
    public class ActionWithFigure
    {        
        private ActionWithoutParams actionWithoutParams;
        private double param1;
        private double param2;
        private ActionWithOneParam oneParamAction;
        private ActionWithTwoParams twoParamsAction;

        public IFigure FigureToAction { get; set; }
        public delegate int ActionWithoutParams(IFigure f, bool isUndoAction);
        public delegate int ActionWithOneParam(IFigure f, double param, bool isUndoAction);
        public delegate int ActionWithTwoParams(IFigure f, double param1, double param2, bool isUndoAction);

        public ActionWithFigure(IFigure figure, ActionWithoutParams action)
        {
            FigureToAction = figure;
            actionWithoutParams = action;
        }

        public ActionWithFigure(IFigure figure, ActionWithOneParam action, double param)
        {
            FigureToAction = figure;
            oneParamAction = action;
            param1 = param;  
        }

        public ActionWithFigure(IFigure figure, ActionWithTwoParams action, double param1, double param2)
        {
            FigureToAction = figure;
            twoParamsAction = action;
            this.param1 = param1;
            this.param2 = param2;
        }

        public void RunFunction()
        {
            if (actionWithoutParams != null)
            {
                actionWithoutParams(FigureToAction, true);
            }
            else if (oneParamAction != null)
            {
                oneParamAction(FigureToAction, param1, true);
            }
            else
            {
                twoParamsAction(FigureToAction, param1, param2, true);
            }
        }
    }
}

using NUnit.Framework;

namespace Figures
{
    [TestFixture]
    public class ObjectCanvasTests
    {
        [Test]
        public void AddFiveFiguresTest()
        {
            var canvas = new ObjectCanvas();

            var addOperation = new AddFigureOperation();

            canvas.ApplyOperation(addOperation, new Rectangle(5, 10));
            canvas.ApplyOperation(addOperation, new Ellipse(5, 10));
            canvas.ApplyOperation(addOperation, new LineSegment(5));
            canvas.ApplyOperation(addOperation, new Rectangle(5, 10, 40, 50));
            canvas.ApplyOperation(addOperation, new Ellipse(5, 10, 40, 20));

            Assert.True(canvas.FigureDic.Count == 5);  
        }

        [Test]
        public void AddFiguresAndUndoTest()
        {
            var canvas = new ObjectCanvas();

            var addOperation = new AddFigureOperation();

            canvas.ApplyOperation(addOperation, new Rectangle(5, 10));
            canvas.ApplyOperation(addOperation, new Ellipse(5, 10));
            canvas.ApplyOperation(addOperation, new LineSegment(5));

            canvas.Undo(2);

            Assert.True(canvas.FigureDic.Count == 1);
        }

        [Test]
        public void AddingAfterUndoTest()
        {
            var canvas = new ObjectCanvas();

            var addOperation = new AddFigureOperation();
            canvas.ApplyOperation(addOperation, new Rectangle(5, 10));
            canvas.ApplyOperation(addOperation, new Ellipse(5, 10));
            canvas.ApplyOperation(addOperation, new LineSegment(5));

            canvas.Undo(2);

            canvas.ApplyOperation(addOperation, new Ellipse(5, 10, 40, 20));

            Assert.True(canvas.FigureDic.ContainsKey(1) && canvas.FigureDic[1] is Ellipse);
        }

        [Test]
        public void LineSegmentRotation90Test()
        {
            var canvas = new ObjectCanvas();

            int startX = 0;
            int startY = 0;

            var addOperation = new AddFigureOperation();
            canvas.ApplyOperation(addOperation, new LineSegment(6));

            var rotateClockwizeOperation = new RotateClockwizeOperation();
            canvas.ApplyOperation(rotateClockwizeOperation, canvas.FigureDic[0]);

            Assert.True(canvas.FigureDic[0].FigureOrientation == Orientation.Vertical
                        && canvas.FigureDic[0].CenterX == startX && canvas.FigureDic[0].CenterY == startY);
        }

        [Test]
        public void EllipseRotation90Test()
        {
            var canvas = new ObjectCanvas();

            int startX = 0;
            int startY = 0;
            var addOperation = new AddFigureOperation();
            canvas.ApplyOperation(addOperation, new Ellipse(6, 2));

            var rotateClockwizeOperation = new RotateClockwizeOperation();
            canvas.ApplyOperation(rotateClockwizeOperation, canvas.FigureDic[0]);

            Assert.True(canvas.FigureDic[0].FigureOrientation == Orientation.Vertical
                        && canvas.FigureDic[0].CenterX == startX && canvas.FigureDic[0].CenterY == startY);
        }

        [Test]
        public void RectangleRotation90Test()
        {
            var canvas = new ObjectCanvas();

            int startX = 6;
            int startY = 4;
            
            var addOperation = new AddFigureOperation();
            canvas.ApplyOperation(addOperation, new Rectangle(startX, startY, 20, 10));

            var rotateClockwizeOperation = new RotateClockwizeOperation();
            canvas.ApplyOperation(rotateClockwizeOperation, canvas.FigureDic[0]);

            Assert.True(canvas.FigureDic[0].FigureOrientation == Orientation.Vertical
                        && canvas.FigureDic[0].CenterX == startX && canvas.FigureDic[0].CenterY == startY);
        }

        [Test]
        public void RectangleRotation90UndoTest()
        {
            var canvas = new ObjectCanvas();
            
            int startX = 6;
            int startY = 4;
            
            var addOperation = new AddFigureOperation();
            canvas.ApplyOperation(addOperation, new Rectangle(startX, startY, 20, 10));

            var rotateClockwizeOperation = new RotateClockwizeOperation();
            canvas.ApplyOperation(rotateClockwizeOperation, canvas.FigureDic[0]);
            canvas.Undo(1);

            Assert.True(canvas.FigureDic[0].FigureOrientation == Orientation.Horizontal
                        && canvas.FigureDic[0].CenterX == startX && canvas.FigureDic[0].CenterY == startY);
        }

        //[Test]
        //public void 
    }
}

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

            canvas.AddFigure(new Rectangle(5, 10));
            canvas.AddFigure(new Ellipse(5, 10));
            canvas.AddFigure(new LineSegment(5));
            canvas.AddFigure(new Rectangle(5, 10, 40, 50));
            canvas.AddFigure(new Ellipse(5, 10, 40, 20));

            Assert.True(canvas.FigureDic.Count == 5);        
        }

        [Test]
        public void AddFiveFiguresAndUndoTest()
        {
            var canvas = new ObjectCanvas();

            canvas.AddFigure(new Rectangle(5, 10));
            canvas.AddFigure(new Ellipse(5, 10));
            canvas.AddFigure(new LineSegment(5));
            canvas.AddFigure(new Rectangle(5, 10, 40, 50));
            canvas.AddFigure(new Ellipse(5, 10, 40, 20));

            canvas.Undo(2);

            Assert.True(canvas.FigureDic.Count == 3);
        }

        [Test]
        public void AddingAfterUndoTest()
        {
            var canvas = new ObjectCanvas();
                        
            canvas.AddFigure(new LineSegment(5));
            canvas.AddFigure(new Rectangle(5, 10, 40, 50));
            canvas.AddFigure(new Ellipse(5, 10, 40, 20));

            canvas.Undo(2);

            canvas.AddFigure(new Ellipse(5, 10, 40, 20));

            Assert.True(canvas.FigureDic.ContainsKey(1) && canvas.FigureDic[1] is Ellipse);
        }

        [Test]
        public void LineSegmentRotation90Test()
        {
            var canvas = new ObjectCanvas();

            int startX = 0;
            int startY = 0;
            canvas.AddFigure(new LineSegment(6));
            canvas.RotateClockwize(0);

            Assert.True(canvas.FigureDic[0].FigureOrientation == Orientation.Vertical
                        && canvas.FigureDic[0].CenterX == startX && canvas.FigureDic[0].CenterY == startY);
        }

        [Test]
        public void EllipseRotation90Test()
        {
            var canvas = new ObjectCanvas();

            int startX = 0;
            int startY = 0;
            canvas.AddFigure(new Ellipse(6, 2));
            canvas.RotateClockwize(0);

            Assert.True(canvas.FigureDic[0].FigureOrientation == Orientation.Vertical
                        && canvas.FigureDic[0].CenterX == startX && canvas.FigureDic[0].CenterY == startY);
        }

        [Test]
        public void RectangleRotation90Test()
        {
            var canvas = new ObjectCanvas();

            int startX = 6;
            int startY = 4;
            canvas.AddFigure(new Rectangle(startX, startY, 20, 10));
            canvas.RotateClockwize(0);

            Assert.True(canvas.FigureDic[0].FigureOrientation == Orientation.Vertical
                        && canvas.FigureDic[0].CenterX == startX && canvas.FigureDic[0].CenterY == startY);
        }

        [Test]
        public void RectangleRotation90UndoTest()
        {
            var canvas = new ObjectCanvas();

            int startX = 6;
            int startY = 4;
            canvas.AddFigure(new Rectangle(startX, startY, 20, 10));
            canvas.RotateClockwize(0);
            canvas.Undo(1);

            Assert.True(canvas.FigureDic[0].FigureOrientation == Orientation.Horizontal
                        && canvas.FigureDic[0].CenterX == startX && canvas.FigureDic[0].CenterY == startY);
        }

        //[Test]
        //public void 
    }
}

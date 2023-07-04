using System.Drawing;

namespace GeometricFiguresCalculator.Service
{
    public static class PointExtensions
    {
        public static Point Subtract (this Point initPoint, Point point)
        {
            return new Point(initPoint.X - point.X, initPoint.Y - point.Y);
        }
    }
}

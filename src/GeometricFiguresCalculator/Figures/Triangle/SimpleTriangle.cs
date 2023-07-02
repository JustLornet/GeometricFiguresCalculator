using GeometricFiguresCalculator.Factory;
using GeometricFiguresCalculator.Service;
using System.Drawing;

namespace GeometricFiguresCalculator.Figures.Triangle
{
    [Figure("simple-triangle")]
    public sealed class SimpleTriangle : TriangleBase
    {
        internal SimpleTriangle(Point vertexA, Point vertexB, Point vertexC) : base(vertexA, vertexB, vertexC)
        {
            SendConsoleMessage.DefaultNotification(Name);
        }
    }
}

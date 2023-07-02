using GeometricFiguresCalculator.Figures.Triangle.Service;
using GeometricFiguresCalculator.Service;
using System.Drawing;

namespace GeometricFiguresCalculator.Figures.Triangle
{
    public abstract class TriangleBase : FigureBase
    {
        private readonly Point _vertexA;
        private readonly Point _vertexB;
        private readonly Point _vertexC;

        private bool? _isRightangled = null;

        public TriangleBase(Point vertexA, Point vertexB, Point vertexC)
        {
            _vertexA = vertexA;
            _vertexB = vertexB;
            _vertexC = vertexC;

            // проверка, что треугольник создан верно
            if (!this.IsTriangleValid(out Exception exception))
                throw exception;
        }

        public virtual Point VertexA => _vertexA;
        public virtual Point VertexB => _vertexB;
        public virtual Point VertexC => _vertexC;

        public bool IsRightangled
        {
            get
            {
                if (_isRightangled is not null)
                    return (bool)_isRightangled;

                _isRightangled = this.CalculateIsTriangleRightangled();

                return (bool)_isRightangled;
            }
        }

        public override double CalculateArea(int dimension = Constants.Dimension)
        {
            double majorLine = (VertexB.X - VertexA.X) * (VertexC.Y - VertexA.Y);
            double secondaryLine = (VertexC.X - VertexA.X) * (VertexB.Y - VertexA.Y);

            double areaNotRounded = 0.5f * Math.Abs(majorLine - secondaryLine);
            var areaRounded = Math.Round(areaNotRounded, dimension);

            return areaRounded;
        }
    }
}

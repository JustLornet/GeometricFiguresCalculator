using GeometricFiguresCalculator.Service;

namespace GeometricFiguresCalculator.Figures.Circle
{
    public abstract class CircleBase : FigureBase
    {
        private readonly double _radius;

        public CircleBase(double radius)
        {
            if (radius <= 0)
                throw new ArgumentException("Радиус круга должен быть больше 0");

            _radius = radius;
        }

        public virtual double Radius => _radius;

        public override double CalculateArea(int dimension = Constants.Dimension)
        {
            double areaNotRounded = Math.PI * Math.Pow(_radius, 2);
            var areaRounded = Math.Round(areaNotRounded, dimension);

            return areaRounded;
        }
    }
}

namespace GeometricFiguresCalculator.ExternalDllSample
{
    [FigureAttribute("square-sample")]
    public sealed class Square : FigureBase
    {
        private readonly int _length;

        public Square(int length)
        {
            _length = length;
        }

        public override double CalculateArea(int dimension = 2)
        {
            var area = Math.Pow(_length, 2);

            return Math.Round(area, dimension);
        }
    }
}
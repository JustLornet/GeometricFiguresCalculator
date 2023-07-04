using System.Drawing;

namespace GeometricFiguresCalculator.Figures.Triangle.Service
{
    public sealed class InvalidTriangleException : Exception
    {
        public InvalidTriangleException(string message) : base(message) { }

        /// <summary>
        /// Совпадение вершин треугольника
        /// </summary>
        public InvalidTriangleException(Point FirstVertex) :
            base($"Две совпадащие вершины с координатасм {FirstVertex}")
        { }
    }
}

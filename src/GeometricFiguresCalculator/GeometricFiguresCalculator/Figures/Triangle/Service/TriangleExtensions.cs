using GeometricFiguresCalculator.ValueObjects;

namespace GeometricFiguresCalculator.Figures.Triangle.Service
{
    public static class TriangleExtensions
    {
        /// <summary>
        /// Проверяет треугольник на корректность, возможность сущестования в евклидовой геометрии
        /// </summary>
        /// <param name="triangle">Проверямеый треугольник</param>
        /// <param name="exception">Ошибка в случае, если треугольник некорректен</param>
        /// <returns>true - треугольник корректен</returns>
        public static bool IsTriangleValid(this TriangleBase triangle, out Exception exception)
        {
            exception = null!;

            if (triangle.VertexA == triangle.VertexB)
                exception = new InvalidTriangleException(triangle.VertexA);
            else if (triangle.VertexA == triangle.VertexC)
                exception = new InvalidTriangleException(triangle.VertexA);
            else if (triangle.VertexB == triangle.VertexC)
                exception = new InvalidTriangleException(triangle.VertexB);

            if(exception is not null) return false;

            return true;
        }

        /// <summary>
        /// Расчет того, является ли треугольник прямоугольным
        /// </summary>
        /// <param name="triangle"></param>
        /// <returns>true - Треугольник прямоугольный</returns>
        public static bool CalculateIsTriangleRightangled (this TriangleBase triangle)
        {
            var angleA = new Angle(triangle.VertexA, triangle.VertexB, triangle.VertexC);

            if (angleA.IsRightangled) return true;

            var angleB = new Angle(triangle.VertexB, triangle.VertexA, triangle.VertexC);
            
            if (angleB.IsRightangled) return true;
            
            var angleC = new Angle(triangle.VertexC, triangle.VertexB, triangle.VertexA);
            
            return angleC.IsRightangled;
        }
    }
}

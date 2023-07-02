using GeometricFiguresCalculator.Factory;
using GeometricFiguresCalculator.Figures.Circle;
using GeometricFiguresCalculator.Figures.Triangle;
using GeometricFiguresCalculator.Service;
using System.Drawing;
using System.Reflection;

namespace GeometricFiguresCalculator
{
    public sealed class AreaCalculator
    {
        private readonly FiguresFactory _factory;

        /// <summary>
        /// Тип для получение площади геометрических фигур
        /// При инициализации через рефлексию происходит поиск всех имеющихся фигур
        /// </summary>
        public AreaCalculator()
        {
            _factory = new FiguresFactory();
        }

        /// <summary>
        /// Получение площади фигуры через название фигуры
        /// </summary>
        /// <param name="figureName">Имя фигуры</param>
        /// <param name="arguments">Аргументы для инициализации фигуры</param>
        /// <param name="dimension">Кол-во знаков после запятой</param>
        /// <returns></returns>
        public double GetFigureAreaViaName(string figureName, int dimension = Constants.Dimension, params object[] arguments)
        {
            var figure = _factory.GetFigure(figureName, arguments);

            if(figure is null) throw new ArgumentException($"Фигура с именем {nameof(figure)} отсутствует");

            return figure.CalculateArea(dimension);
        }
        
        /// <summary>
        /// ПОлучение площади круга
        /// </summary>
        /// <param name="radius">Радиус круга</param>
        /// <param name="dimension">Кол-во знаков после запятой</param>
        /// <returns>Площадь круга</returns>
        public static double GetCircleArea(double radius, int dimension = Constants.Dimension)
        {
            var figure = new SimpleCircle(radius);

            return figure.CalculateArea(dimension);
        }

        /// <summary>
        /// Получение площади треугольника
        /// </summary>
        /// <param name="vertexA">Вершина треугольника</param>
        /// <param name="vertexB">Вершина треугольника</param>
        /// <param name="vertexC">Вершина треугольника</param>
        /// <param name="isRightanled">true - треугольник является прямоугольным</param>
        /// <param name="dimension">Кол-во знаков после запятой</param>
        /// <returns>Площадь треугольника</returns>
        public static double GetTriangleArea(Point vertexA, Point vertexB, Point vertexC, out bool isRightanled, int dimension = Constants.Dimension)
        {
            var figure = new SimpleTriangle(vertexA, vertexB, vertexC);
            isRightanled = figure.IsRightangled;

            return figure.CalculateArea(dimension);
        }

        public static double GetFromExternalDll(string dllName, string figureName, int dimension = Constants.Dimension, params object?[]? parameters)
        {
            // путь до новой библиотеки
            var dir = Directory.GetCurrentDirectory();
            var dllFullPath = Path.Combine(dir, dllName);

            // загрузка новой библиотеки
            var asm = Assembly.LoadFile(dllFullPath);

            // поиск фигуры в новой библиотеке
            var asmTypes = asm.GetTypes();
            var newFigureType = asmTypes.FirstOrDefault(t =>
            {
                if (!t.IsSubclassOf(typeof(FigureBase)) || t.IsAbstract)
                    return false;

                var newFigureName = t.GetCustomAttribute<FigureAttribute>()?.Name;

                if (string.IsNullOrEmpty(newFigureName)) return false;

                return newFigureName == figureName;
            });

            if (newFigureType is null)
                throw new InvalidOperationException($"Фигура с именем {figureName} в данной библиотеке не была найдена");

            var newFigureNotParsed = Activator.CreateInstance(newFigureType, parameters);

            if(newFigureNotParsed is null)
                throw new InvalidOperationException($"Неудачное создание экземпляра фигуры с именем {figureName}");

            var newFigure = (FigureBase)newFigureNotParsed;

            return newFigure.CalculateArea(dimension);
        }
    }
}
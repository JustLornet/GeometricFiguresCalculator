using GeometricFiguresCalculator.Factory;
using GeometricFiguresCalculator.Figures.Circle;
using GeometricFiguresCalculator.Figures.Triangle;
using GeometricFiguresCalculator.Service;
using System.Collections;
using System.Drawing;
using System.Reflection;
using System.Linq;

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
        public double GetFigureAreaViaName(string figureName, IEnumerable<int> arguments, int dimension = Constants.Dimension)
        {
            try
            {
                var parsedArguments = arguments.Select(arg => (object)arg).ToArray();

                var area = this.GetFigureAreaViaName(figureName, dimension, parsedArguments);

                return area;
            }
            catch (Exception ex)
            {
                SendConsoleMessage.SendMessageAboutError(ex);

                return -1;
            }
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
            try
            {
                // приведение имени фигуры
                var parsedFigureName = figureName.Trim();

                var figure = _factory.GetFigure(parsedFigureName, arguments);

                if (figure is null) throw new ArgumentException($"Фигура с именем {nameof(figure)} отсутствует");

                return figure.CalculateArea(dimension);
            }
            catch (Exception ex)
            {
                SendConsoleMessage.SendMessageAboutError(ex);

                return -1;
            }
        }

        public IEnumerable<string> GetAllFiguresNames()
        {
            return _factory.GetAllFiguresNames();
        }

        /// <summary>
        /// ПОлучение площади круга
        /// </summary>
        /// <param name="radius">Радиус круга</param>
        /// <param name="dimension">Кол-во знаков после запятой</param>
        /// <param name="isEnableErrors">true - позволить приложению выкидывать ошибку</param>
        /// <returns>Площадь круга</returns>
        public static double GetCircleArea(double radius, int dimension = Constants.Dimension, bool isEnableErrors = false)
        {
            try
            {
                var figure = new SimpleCircle(radius);

                return figure.CalculateArea(dimension);
            }
            catch (Exception ex)
            {
                SendConsoleMessage.SendMessageAboutError(ex);

                if (isEnableErrors)
                    throw;

                return -1;
            }
        }

        /// <summary>
        /// Получение площади треугольника
        /// </summary>
        /// <param name="vertexA">Вершина треугольника</param>
        /// <param name="vertexB">Вершина треугольника</param>
        /// <param name="vertexC">Вершина треугольника</param>
        /// <param name="isRightanled">true - треугольник является прямоугольным</param>
        /// <param name="dimension">Кол-во знаков после запятой</param>
        /// <param name="isEnableErrors">true - позволить приложению выкидывать ошибку</param>
        /// <returns>Площадь треугольника</returns>
        public static double GetTriangleArea(Point vertexA, Point vertexB, Point vertexC, out bool? isRightanled, int dimension = Constants.Dimension, bool isEnableErrors = false)
        {
            try
            {
                var figure = new SimpleTriangle(vertexA, vertexB, vertexC);
                isRightanled = figure.IsRightangled;

                return figure.CalculateArea(dimension);
            }
            catch (Exception ex)
            {
                SendConsoleMessage.SendMessageAboutError(ex);
                isRightanled = null;

                if(isEnableErrors)
                    throw;

                return -1;
            }
        }

        /// <summary>
        /// Расчет площади фигуры из внешней библиотеки с поздним связыванием. Имя должно задаваться либо как относительный путь, либо как полный путь
        /// </summary>
        /// <param name="dllName">Имя внешней библиотеки: либо относительное, либо полное</param>
        /// <param name="figureName">Имя фигуры из этой библиотеки</param>
        /// <param name="dimension">Количество знаков после запятой в площади</param>
        /// <param name="parameters">Параметры для данной фигуры</param>
        /// <returns>Площадь заданной фигуры с установленными параметрами</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static double GetFromExternalDll(string dllName, string figureName, int dimension = Constants.Dimension, params object?[]? parameters)
        {
            try
            {
                // сначала происходит поиск данного файла в той же директории, что и настоящий проект
                // путь до новой библиотеки
                var dir = Directory.GetCurrentDirectory();
                var dllFullPath = Path.Combine(dir, dllName);

                var isDllInSameDirectory = File.Exists(dllFullPath);

                // если dll отсутствует в директории этого проекта, пытаемся найти её, представив dllName как полный путь
                if (!isDllInSameDirectory)
                {
                    dllFullPath = dllName;

                    if (!File.Exists(dllFullPath))
                        throw new FileNotFoundException(dllFullPath);
                }

                // загрузка новой библиотеки
                var asm = Assembly.LoadFile(dllFullPath);

                var calculatedArea = AreaCalculator.CalculateAreaFromExternallDll(asm, figureName, dimension, parameters);

                return calculatedArea;
            }
            catch (Exception ex)
            {
                SendConsoleMessage.SendMessageAboutError(ex);

                return -1;
            }
        }

        private static double CalculateAreaFromExternallDll(Assembly externalAsm, string figureName, int dimension = Constants.Dimension, params object?[]? parameters)
        {
            // поиск фигуры в новой библиотеке
            var asmTypes = externalAsm.GetTypes();
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

            if (newFigureNotParsed is null)
                throw new InvalidOperationException($"Неудачное создание экземпляра фигуры с именем {figureName}");

            var newFigure = (FigureBase)newFigureNotParsed;

            return newFigure.CalculateArea(dimension);
        }
    }
}
using GeometricFiguresCalculator.Figures.Circle;
using System.Reflection;

namespace GeometricFiguresCalculator.Factory
{
    public sealed class FiguresFactory
    {
        private IDictionary<string, Type> _figures;

        /// <summary>
        /// Фабрика для фигур. При инициализации порседством рефлексии находит классы, наследующие базовый класс фигуры
        /// </summary>
        public FiguresFactory()
        {
            var currentAssemblyTypes = Assembly.GetExecutingAssembly().GetTypes();

            // TODO: возможна ошибка добавления фигур с одинаковыми именами, добавить обработку
            var figuresTypes = currentAssemblyTypes.AsParallel().Where(t =>
            {
                if (!t.IsSubclassOf(typeof(FigureBase)) || t.IsAbstract)
                    return false;

                return t.GetCustomAttribute<FigureAttribute>() is not null;
            }).ToDictionary((t) => t.GetCustomAttribute<FigureAttribute>()!.Name);

            _figures = figuresTypes;
        }

        /// <summary>
        /// ПОлучение имен всех имеющихся фигур
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetAllFiguresNames()
        {
            return _figures.Keys;
        }

        /// <summary>
        /// Получение фигуры по имени
        /// </summary>
        /// <param name="name">Имя фигуры</param>
        /// <param name="arguments">Аргументы для инициализации фигуры</param>
        public FigureBase? GetFigure(string name, params object[] arguments)
        {
            if (!_figures.ContainsKey(name)) return null;

            var figure = Activator.CreateInstance(_figures[name], arguments) as FigureBase;

            return figure;
        }
    }
}

using GeometricFiguresCalculator.Service;
using System.Reflection;

namespace GeometricFiguresCalculator
{
    public abstract class FigureBase
    {
        private readonly FigureAttribute _attribute;
        private readonly string _name;

        public FigureBase()
        {
            var attribute = this.GetType().GetCustomAttribute<FigureAttribute>();
            
            if (attribute is null)
                throw new CustomAttributeFormatException($"Отсутствует имплментация атрибута {nameof(FigureAttribute)}");
        
            _attribute = attribute;
            _name = attribute.Name;
        }

        public virtual string Name => _name;

        /// <summary>
        /// Расчет площади фигуры
        /// </summary>
        /// <param name="dimension">Число знаков после запятой</param>
        /// <returns>Площадь фигуры</returns>
        public abstract double CalculateArea(int dimension = Constants.Dimension);

        public override string ToString()
        {
            return $"Фигура - {Name}";
        }
    }
}
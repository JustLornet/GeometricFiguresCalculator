namespace GeometricFiguresCalculator
{
    /// <summary>
    /// Атрибут для фигур
    /// </summary>
    public sealed class FigureAttribute : Attribute
    {
        private readonly string _name;

        public FigureAttribute(string figureName)
        {
            _name = figureName;
        }

        public string Name => _name;
    }
}

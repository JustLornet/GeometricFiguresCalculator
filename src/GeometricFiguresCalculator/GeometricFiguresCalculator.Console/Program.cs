using GeometricFiguresCalculator;

string CircleSample = "simple-circle";

while (true)
{
    try
    {
        Console.WriteLine($"Базовые доступные для консоли фигуры:\n\t{CircleSample}");
        Console.WriteLine("Введите название фигуры:");

        var figureName = Console.ReadLine();

        if (string.IsNullOrEmpty(figureName))
        {
            Console.WriteLine("Название фигуры не может быть пустым");
            continue;
        }

        var calculator = new AreaCalculator();
        var isFigureExists = calculator.GetAllFiguresNames().Contains(figureName);

        if (!isFigureExists)
        {
            Console.WriteLine($"Фигура с именем {figureName} не найдена");
            continue;
        }

        Console.Write("Введите параметры для данной фигуры через пробел");

        var trimmedName = figureName.Trim();

        if (trimmedName == CircleSample)
        {
            Console.WriteLine(" в виде целого числа (радиус круга), например: 10");
        }
        else
        {
            Console.WriteLine();
        }

        var arguments = Console.ReadLine();

        if (arguments is null)
        {
            Console.Write("Фигура без параметров невозможна!");
            continue;
        }

        // парсинг параметров
        var parsedArguments = arguments!.Split(" ").Where(arg => Int32.TryParse(arg, out _)).Select(arg => Int32.Parse(arg)).ToArray();

        var area = calculator.GetFigureAreaViaName(figureName!, arguments: parsedArguments!);

        Console.WriteLine($"Площадь выбранной фигуры: {area}");
        Console.WriteLine("------");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Во время выполнения операции возникла ошибка:\n\t{ex.Message}");
    }
    
}
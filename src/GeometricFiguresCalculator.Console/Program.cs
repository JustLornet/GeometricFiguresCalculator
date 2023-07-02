using GeometricFiguresCalculator.Console.Service;

System.Reflection.Assembly geometryDll = System.Reflection.Assembly.LoadFile(GeometryDllInfo.DllName);

if(geometryDll is null)
{
    Console.WriteLine($"Бибилоетка с именем {GeometryDllInfo.DllName} не была найдена");
    Environment.Exit(0);
}

Console.WriteLine($"Бибилоетка с именем {GeometryDllInfo.DllName} загружена");
Console.WriteLine("Введите название фигуры:");
var figureName = Console.ReadLine();

if(figureName is not null)
{
    var calcType = geometryDll.GetType(GeometryDllInfo.Calculator);
}
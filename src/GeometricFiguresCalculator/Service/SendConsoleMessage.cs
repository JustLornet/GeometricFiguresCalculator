namespace GeometricFiguresCalculator.Service
{
    internal static class SendConsoleMessage
    {
        internal static void DefaultNotification(string figureName)
        {
            Console.WriteLine($"{figureName} был создан. Данный тип является встроенным в библиотеку");
        }
    }
}

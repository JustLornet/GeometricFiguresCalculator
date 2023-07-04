namespace GeometricFiguresCalculator.Service
{
    public static class SendConsoleMessage
    {
        public static void DefaultNotification(string figureName)
        {
            Console.WriteLine($"{figureName} был создан. Данный тип является встроенным в библиотеку");
        }

        public static void SendMessageAboutError(Exception exc)
        {
            Console.WriteLine($"Во время выполнения возникла ошибка:\n\t{exc.Message}");
        }
    }
}

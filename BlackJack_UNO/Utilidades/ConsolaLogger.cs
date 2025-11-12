namespace BlackJack_1.Utilidades;

public static class ConsolaLogger
{
    // Muestra un mensaje del tiempo 
    public static void Mostrar(string mensaje)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {mensaje}");
        Console.ResetColor();
    }

    // Variante para mostrar errores
    public static void Error(string mensaje)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[ERROR {DateTime.Now:HH:mm:ss}] {mensaje}");
        Console.ResetColor();
    }

    // Variante para mostrar advertencias
    public static void Advertencia(string mensaje)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[ADVERTENCIA {DateTime.Now:HH:mm:ss}] {mensaje}");
        Console.ResetColor();
    }
}

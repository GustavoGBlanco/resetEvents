
using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.WriteLine("🧪 Ejecutando ejemplos de ResetEvents en C#...");

        Console.WriteLine("----------Ejemplo 1----------");
        new Thread(ResetEventsExamples.EsperarSenal).Start();
        new Thread(ResetEventsExamples.EnviarSenal).Start();
        Thread.Sleep(800);

        Console.WriteLine("----------Ejemplo 2----------");
        new Thread(ResetEventsExamples.Parte1).Start();
        new Thread(ResetEventsExamples.Parte2).Start();
        new Thread(ResetEventsExamples.Parte3).Start();
        Thread.Sleep(1000);

        Console.WriteLine("----------Ejemplo 3----------");
        for (int i = 1; i <= 2; i++)
            new Thread(() => ResetEventsExamples.HiloEsperador($"Hilo{i}")).Start();
        Thread.Sleep(300);
        new Thread(ResetEventsExamples.Señalar).Start();
        Thread.Sleep(1000);

        Console.WriteLine("----------Ejemplo 4----------");
        for (int i = 1; i <= 3; i++)
            new Thread(() => ResetEventsExamples.EsperarGrupo($"Hilo{i}")).Start();
        new Thread(ResetEventsExamples.DesbloquearGrupo).Start();
        Thread.Sleep(1000);

        Console.WriteLine("----------Ejemplo 5----------");
        for (int i = 1; i <= 2; i++)
            new Thread(() => ResetEventsExamples.Trabajador($"Trabajador{i}")).Start();
        new Thread(ResetEventsExamples.IniciarTodos).Start();
        Thread.Sleep(800);

        Console.WriteLine("----------Ejemplo 6----------");
        new Thread(ResetEventsExamples.Consumidor).Start();
        new Thread(ResetEventsExamples.Productor).Start();
        Thread.Sleep(1000);

        Console.WriteLine("----------Ejemplo 7----------");
        new Thread(ResetEventsExamples.Esperar).Start();
        new Thread(ResetEventsExamples.LiberarYResetear).Start();
        Thread.Sleep(800);

        Console.WriteLine("----------Ejemplo 8----------");
        new Thread(ResetEventsExamples.EsperarSlim).Start();
        new Thread(ResetEventsExamples.SeñalarSlim).Start();
        Thread.Sleep(800);

        Console.WriteLine("----------Ejemplo 9----------");
        new Thread(ResetEventsExamples.HiloA).Start();
        new Thread(ResetEventsExamples.HiloB).Start();
        Thread.Sleep(800);

        Console.WriteLine("----------Ejemplo 10----------");
        new Thread(ResetEventsExamples.Procesar).Start();
        new Thread(ResetEventsExamples.EsperarFin).Start();
        Thread.Sleep(1000);

        Console.WriteLine("✅ Fin de los ejemplos.");
    }
}

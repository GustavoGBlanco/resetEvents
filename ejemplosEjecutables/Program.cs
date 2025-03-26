
using System;
using System.Threading;

class ResetEventExamplesApp
{
    static void Main()
    {
        Console.WriteLine("----------Ejemplo 1: Sincronización simple con AutoResetEvent----------");
        Thread t1 = new Thread(SincronizacionSimple.EsperarSenal);
        t1.Start();
        Thread.Sleep(500);
        SincronizacionSimple.EnviarSenal();
        t1.Join();
        Console.WriteLine();

        Console.WriteLine("----------Ejemplo 2: Flujo secuencial con AutoResetEvent----------");
        Thread p1 = new Thread(FlujoSecuencial.EjecutarPaso1);
        Thread p2 = new Thread(FlujoSecuencial.EjecutarPaso2);
        Thread p3 = new Thread(FlujoSecuencial.EjecutarPaso3);
        p2.Start();
        p3.Start();
        Thread.Sleep(300);
        p1.Start();
        p1.Join(); p2.Join(); p3.Join();
        Console.WriteLine();

        Console.WriteLine("----------Ejemplo 3: Despertar individual con AutoResetEvent----------");
        for (int i = 1; i <= 3; i++)
        {
            string nombre = $"Hilo{i}";
            new Thread(() => EsperaIndividual.Esperar(nombre)).Start();
        }
        Thread.Sleep(500);
        for (int i = 0; i < 3; i++)
        {
            EsperaIndividual.SeñalIndividual();
            Thread.Sleep(200);
        }
        Thread.Sleep(1000);
        Console.WriteLine();

        Console.WriteLine("----------Ejemplo 4: Liberación global con ManualResetEvent----------");
        for (int i = 1; i <= 3; i++)
        {
            string nombre = $"Trabajador{i}";
            new Thread(() => LiberacionGlobal.Esperar(nombre)).Start();
        }
        Thread.Sleep(500);
        LiberacionGlobal.LiberarTodos();
        Thread.Sleep(500);
        Console.WriteLine();

        Console.WriteLine("----------Ejemplo 5: Sincronizador de inicio global----------");
        for (int i = 1; i <= 3; i++)
        {
            string nombre = $"Hilo{i}";
            new Thread(() => SincronizadorDeInicio.Trabajador(nombre)).Start();
        }
        Thread.Sleep(500);
        SincronizadorDeInicio.IniciarTodos();
        Thread.Sleep(500);
        Console.WriteLine();

        Console.WriteLine("----------Ejemplo 6: Productor y consumidor con AutoResetEvent----------");
        Thread consumidor = new Thread(ConsumidorEsperandoDato.Consumidor);
        consumidor.Start();
        Thread.Sleep(500);
        ConsumidorEsperandoDato.Productor();
        consumidor.Join();
        Console.WriteLine();

        Console.WriteLine("----------Ejemplo 7: Ciclo de reset manual----------");
        Thread hiloCiclo1 = new Thread(CicloDeResetManual.EsperarCiclo);
        Thread hiloCiclo2 = new Thread(CicloDeResetManual.EsperarCiclo);
        hiloCiclo1.Start();
        hiloCiclo2.Start();
        Thread.Sleep(300);
        CicloDeResetManual.LiberarYResetear();
        Thread.Sleep(500);
        Console.WriteLine();

        Console.WriteLine("----------Ejemplo 8: Uso de ManualResetEventSlim----------");
        Thread slim = new Thread(SincronizadorSlim.EsperarSlim);
        slim.Start();
        Thread.Sleep(300);
        SincronizadorSlim.SeñalarSlim();
        slim.Join();
        Console.WriteLine();

        Console.WriteLine("----------Ejemplo 9: Turnos alternados entre hilos----------");
        Thread hilo1 = new Thread(TurnoAlternado.EjecutarHilo1);
        Thread hilo2 = new Thread(TurnoAlternado.EjecutarHilo2);
        hilo1.Start();
        Thread.Sleep(200);
        hilo2.Start();
        hilo1.Join();
        hilo2.Join();
        Console.WriteLine();

        Console.WriteLine("----------Ejemplo 10: Esperar finalización de tarea----------");
        Thread trabajador = new Thread(DetectorDeFinalizacion.Procesar);
        Thread monitor = new Thread(DetectorDeFinalizacion.EsperarFin);
        monitor.Start();
        Thread.Sleep(300);
        trabajador.Start();
        trabajador.Join();
        monitor.Join();
        Console.WriteLine();
    }
}


using System;
using System.Threading;

public static class ResetEventsExamples
{
    private static AutoResetEvent _evento = new(false);
    private static AutoResetEvent _evento1 = new(false);
    private static AutoResetEvent _evento2 = new(false);
    private static ManualResetEvent _manualGrupo = new(false);
    private static ManualResetEvent _inicio = new(false);
    private static AutoResetEvent _datoDisponible = new(false);
    private static int _dato;
    private static ManualResetEvent _eventoReset = new(false);
    private static ManualResetEventSlim _eventoSlim = new(false);
    private static AutoResetEvent _turno1 = new(true);
    private static AutoResetEvent _turno2 = new(false);
    private static ManualResetEvent _finalizado = new(false);

    public static void EsperarSenal()
    {
        Console.WriteLine("Esperando señal...");
        _evento.WaitOne();
        Console.WriteLine("¡Señal recibida!");
    }

    public static void EnviarSenal()
    {
        Thread.Sleep(500);
        Console.WriteLine("Enviando señal.");
        _evento.Set();
    }

    public static void Parte1()
    {
        Console.WriteLine("Parte 1 ejecutada");
        _evento1.Set();
    }

    public static void Parte2()
    {
        _evento1.WaitOne();
        Console.WriteLine("Parte 2 ejecutada");
        _evento2.Set();
    }

    public static void Parte3()
    {
        _evento2.WaitOne();
        Console.WriteLine("Parte 3 ejecutada");
    }

    public static void HiloEsperador(string nombre)
    {
        Console.WriteLine($"{nombre} esperando señal...");
        _evento.WaitOne();
        Console.WriteLine($"{nombre} recibió la señal.");
    }

    public static void Señalar()
    {
        Console.WriteLine("Enviando señal.");
        _evento.Set();
    }

    public static void EsperarGrupo(string nombre)
    {
        Console.WriteLine($"{nombre} esperando...");
        _manualGrupo.WaitOne();
        Console.WriteLine($"{nombre} desbloqueado.");
    }

    public static void DesbloquearGrupo()
    {
        Thread.Sleep(500);
        Console.WriteLine("Liberando todos.");
        _manualGrupo.Set();
    }

    public static void Trabajador(string nombre)
    {
        Console.WriteLine($"{nombre} listo.");
        _inicio.WaitOne();
        Console.WriteLine($"{nombre} empezó a trabajar.");
    }

    public static void IniciarTodos()
    {
        Thread.Sleep(300);
        Console.WriteLine("¡Inicio global!");
        _inicio.Set();
    }

    public static void Consumidor()
    {
        Console.WriteLine("Esperando dato...");
        _datoDisponible.WaitOne();
        Console.WriteLine($"Dato recibido: {_dato}");
    }

    public static void Productor()
    {
        Thread.Sleep(300);
        _dato = 42;
        Console.WriteLine("Dato producido.");
        _datoDisponible.Set();
    }

    public static void Esperar()
    {
        Console.WriteLine("Esperando...");
        _eventoReset.WaitOne();
        Console.WriteLine("Desbloqueado.");
    }

    public static void LiberarYResetear()
    {
        _eventoReset.Set();
        Thread.Sleep(100);
        _eventoReset.Reset();
    }

    public static void EsperarSlim()
    {
        _eventoSlim.Wait();
        Console.WriteLine("Liberado por Slim.");
    }

    public static void SeñalarSlim()
    {
        Thread.Sleep(200);
        _eventoSlim.Set();
    }

    public static void HiloA()
    {
        _turno1.WaitOne();
        Console.WriteLine("Hilo A ejecutando.");
        _turno2.Set();
    }

    public static void HiloB()
    {
        _turno2.WaitOne();
        Console.WriteLine("Hilo B ejecutando.");
        _turno1.Set();
    }

    public static void Procesar()
    {
        Console.WriteLine("Procesando...");
        Thread.Sleep(800);
        _finalizado.Set();
    }

    public static void EsperarFin()
    {
        Console.WriteLine("Esperando finalización...");
        _finalizado.WaitOne();
        Console.WriteLine("Finalización detectada.");
    }
}

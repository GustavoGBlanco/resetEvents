using System;
using System.Threading;

// 🔄 Sincronización entre hilos donde un hilo espera a que otro le dé paso
public static class SincronizacionSimple
{
    private static AutoResetEvent _evento = new(false);

    public static void EsperarSenal()
    {
        Console.WriteLine("[Hilo] Esperando señal...");
        _evento.WaitOne();
        Console.WriteLine("[Hilo] Señal recibida, continuando ejecución.");
    }

    public static void EnviarSenal()
    {
        _evento.Set();
    }
}

// 🧭 Secuencia controlada entre partes que deben ejecutarse en orden
public static class FlujoSecuencial
{
    private static AutoResetEvent _paso1 = new(false);
    private static AutoResetEvent _paso2 = new(false);

    public static void EjecutarPaso1()
    {
        Console.WriteLine("Paso 1 ejecutado");
        _paso1.Set();
    }

    public static void EjecutarPaso2()
    {
        _paso1.WaitOne();
        Console.WriteLine("Paso 2 ejecutado");
        _paso2.Set();
    }

    public static void EjecutarPaso3()
    {
        _paso2.WaitOne();
        Console.WriteLine("Paso 3 ejecutado");
    }
}

// 🎯 Escenario donde múltiples consumidores esperan pero solo uno es atendido por vez
public static class EsperaIndividual
{
    private static AutoResetEvent _evento = new(false);

    public static void Esperar(string nombre)
    {
        Console.WriteLine($"[{nombre}] esperando señal...");
        _evento.WaitOne();
        Console.WriteLine($"[{nombre}] recibió la señal.");
    }

    public static void SeñalIndividual()
    {
        _evento.Set();
    }
}

// 🚀 Liberación de múltiples tareas en espera tras cumplirse una condición común
public static class LiberacionGlobal
{
    private static ManualResetEvent _evento = new(false);

    public static void Esperar(string nombre)
    {
        Console.WriteLine($"[{nombre}] esperando...");
        _evento.WaitOne();
        Console.WriteLine($"[{nombre}] liberado.");
    }

    public static void LiberarTodos()
    {
        _evento.Set();
    }
}

// 🏁 Coordinador de múltiples hilos que deben comenzar al mismo tiempo
public static class SincronizadorDeInicio
{
    private static ManualResetEvent _inicio = new(false);

    public static void Trabajador(string nombre)
    {
        Console.WriteLine($"[{nombre}] listo para comenzar.");
        _inicio.WaitOne();
        Console.WriteLine($"[{nombre}] trabajando.");
    }

    public static void IniciarTodos()
    {
        _inicio.Set();
    }
}

// 📦 Patrón clásico de productor-consumidor con señal de disponibilidad
public static class ConsumidorEsperandoDato
{
    private static AutoResetEvent _datoDisponible = new(false);
    private static int _dato;

    public static void Consumidor()
    {
        Console.WriteLine("Esperando dato...");
        _datoDisponible.WaitOne();
        Console.WriteLine("Dato recibido: " + _dato);
    }

    public static void Productor()
    {
        _dato = 42;
        _datoDisponible.Set();
    }
}

// 🔁 Liberación y reinicio de evento en ciclos sucesivos controlados
public static class CicloDeResetManual
{
    private static ManualResetEvent _evento = new(false);

    public static void EsperarCiclo()
    {
        Console.WriteLine("Esperando evento...");
        _evento.WaitOne();
        Console.WriteLine("Evento recibido");
    }

    public static void LiberarYResetear()
    {
        _evento.Set();
        Thread.Sleep(100);
        _evento.Reset();
    }
}

// 🧪 Variante ligera para escenarios de sincronización donde no se necesita entre procesos
public static class SincronizadorSlim
{
    private static ManualResetEventSlim _evento = new(false);

    public static void EsperarSlim()
    {
        _evento.Wait();
        Console.WriteLine("Desbloqueado con Slim");
    }

    public static void SeñalarSlim()
    {
        _evento.Set();
    }
}

// 🔁 Dos tareas que deben ejecutarse en estricto orden alternado
public static class TurnoAlternado
{
    private static AutoResetEvent _turno1 = new(true);
    private static AutoResetEvent _turno2 = new(false);

    public static void EjecutarHilo1()
    {
        _turno1.WaitOne();
        Console.WriteLine("[Hilo1] Ejecutando turno");
        _turno2.Set();
    }

    public static void EjecutarHilo2()
    {
        _turno2.WaitOne();
        Console.WriteLine("[Hilo2] Ejecutando turno");
        _turno1.Set();
    }
}

// 📢 Señalización de fin de tarea para hilos observadores
public static class DetectorDeFinalizacion
{
    private static ManualResetEvent _finalizado = new(false);

    public static void Procesar()
    {
        Console.WriteLine("Procesando...");
        Thread.Sleep(1000);
        _finalizado.Set();
    }

    public static void EsperarFin()
    {
        Console.WriteLine("Esperando finalización...");
        _finalizado.WaitOne();
        Console.WriteLine("Finalización detectada.");
    }
}

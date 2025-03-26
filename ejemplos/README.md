# Ejemplos de `AutoResetEvent` y `ManualResetEvent` en C#

Este documento contiene 10 ejemplos prácticos para comprender el uso de `AutoResetEvent` y `ManualResetEvent` en escenarios multihilo. Cada ejemplo está contextualizado con su utilidad real y su propósito técnico.

---

## 🧪 Ejemplo 1: AutoResetEvent - sincronización simple

```csharp
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
```

🔍 Un hilo espera a que otro le dé paso mediante una señal.

✅ **¿Por qué AutoResetEvent?**  
Se resetea automáticamente después de liberar un solo hilo, ideal para sincronización uno a uno.

---

## 🧪 Ejemplo 2: AutoResetEvent - pasos en orden estricto

```csharp
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
```

🔍 Fuerza la ejecución ordenada entre tres pasos dependientes.

✅ **¿Por qué AutoResetEvent?**  
Perfecto cuando cada paso debe desbloquear exclusivamente al siguiente.

---

## 🧪 Ejemplo 3: AutoResetEvent - múltiples hilos esperando

```csharp
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
```

🔍 Cada señal desbloquea exactamente un hilo, útil para control selectivo.

✅ **¿Por qué AutoResetEvent?**  
Evita que todos los hilos avancen a la vez. Precisión en la sincronización.

---

## 🧪 Ejemplo 4: ManualResetEvent - liberación en grupo

```csharp
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
```

🔍 Todos los hilos esperando avanzan a la vez al recibir la señal.

✅ **¿Por qué ManualResetEvent?**  
Permite sincronizar eventos que deben desbloquear a múltiples hilos simultáneamente.

---

## 🧪 Ejemplo 5: ManualResetEvent - comienzo simultáneo de múltiples hilos

```csharp
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
```

🔍 Coordinación de inicio entre muchos hilos que deben arrancar al mismo tiempo.

✅ **¿Por qué ManualResetEvent?**  
Ideal para simulaciones, benchmarks o juegos donde varios elementos deben comenzar sincronizados.

---

## 🧪 Ejemplo 6: AutoResetEvent - productor y consumidor

```csharp
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
```

🔍 Modelo clásico donde el consumidor espera que el productor termine su trabajo.

✅ **¿Por qué AutoResetEvent?**  
Sincroniza perfectamente una única entrega de datos.

---

## 🧪 Ejemplo 7: ManualResetEvent - reinicio manual para ciclos

```csharp
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
    Thread.Sleep(100); // permitir salida
    _evento.Reset();
}
```

🔍 Permite emitir una señal, esperar que se complete, y volver a esperar en el próximo ciclo.

✅ **¿Por qué ManualResetEvent?**  
Da control total sobre el momento de liberación y de volver a bloquear.

---

## 🧪 Ejemplo 8: ManualResetEventSlim - versión optimizada

```csharp
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
```

🔍 Alternativa más ligera cuando no se necesita interoperabilidad con procesos externos.

✅ **¿Por qué ManualResetEventSlim?**  
Menor consumo de recursos. Ideal para escenarios de sincronización interna.

---

## 🧪 Ejemplo 9: AutoResetEvent - turnos alternos entre tareas

```csharp
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
```

🔍 Simula una secuencia por turnos entre dos actores.

✅ **¿Por qué AutoResetEvent?**  
Permite alternar hilos de forma estrictamente ordenada.

---

## 🧪 Ejemplo 10: ManualResetEvent - esperar a que termine una operación

```csharp
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
```

🔍 Un hilo espera que otro finalice antes de continuar.

✅ **¿Por qué ManualResetEvent?**  
Ideal para tareas dependientes o múltiples observadores.

---
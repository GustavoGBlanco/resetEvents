# Ejemplos prácticos y profesionales de `AutoResetEvent` y `ManualResetEvent` en C#

Este documento presenta 10 ejemplos realistas y técnicamente justificados del uso de `AutoResetEvent` y `ManualResetEvent` en C#, todos diseñados con hilos (`Thread`) para ilustrar cómo se utilizan para coordinar ejecución entre múltiples hilos mediante señales.

---

## 🧪 Ejemplo 1: Sincronización simple con AutoResetEvent

```csharp
private static AutoResetEvent _evento = new(false);

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
```

🔍 Un hilo espera hasta que otro lo libere.

✅ **¿Por qué `AutoResetEvent`?**  
Automáticamente se resetea después de liberar un hilo. Ideal para pasar control entre hilos.

📊 **Comparación con otros mecanismos:**
- 🔐 `lock`, `Monitor`: no permiten coordinación por señales.
- 🧵 `Mutex`: es para exclusión mutua, no señalización.
- 🔁 `SemaphoreSlim`: se puede usar, pero no reinicia automáticamente.
- 🔄 `Barrier`: no aplica.

---

## 🧪 Ejemplo 2: Ejecución secuencial entre hilos

```csharp
private static AutoResetEvent _evento1 = new(false);
private static AutoResetEvent _evento2 = new(false);

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
```

🔍 Obliga a que las partes se ejecuten en orden: Parte1 → Parte2 → Parte3.

✅ **¿Por qué `AutoResetEvent`?**  
Permite una cadena de ejecución paso a paso.

📊 **Comparación con otros mecanismos:**
- 🔐 `lock`: no asegura orden.
- 🔁 `SemaphoreSlim`: requiere lógica extra.
- 🔄 `Barrier`: coordina fases, no pasos secuenciales.

---

## 🧪 Ejemplo 3: Coordinación uno a uno con múltiples hilos

```csharp
private static AutoResetEvent _evento = new(false);

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
```

🔍 Solo un hilo avanza por señal enviada.

✅ **¿Por qué `AutoResetEvent`?**  
Liberación de un hilo por vez. Perfecto para escenarios tipo “turno”.

📊 **Comparación con otros mecanismos:**
- 🔁 `SemaphoreSlim`: podría liberar varios.
- 🔄 `Barrier`: no controla señales explícitas.

---

## 🧪 Ejemplo 4: ManualResetEvent desbloqueando en grupo

```csharp
private static ManualResetEvent _evento = new(false);

public static void EsperarGrupo(string nombre)
{
    Console.WriteLine($"{nombre} esperando...");
    _evento.WaitOne();
    Console.WriteLine($"{nombre} desbloqueado.");
}

public static void DesbloquearGrupo()
{
    Thread.Sleep(500);
    Console.WriteLine("Liberando todos.");
    _evento.Set();
}
```

🔍 Varios hilos desbloquean al mismo tiempo.

✅ **¿Por qué `ManualResetEvent`?**  
Mantiene la señal activa hasta que se resetea manualmente.

📊 **Comparación con otros mecanismos:**
- 🔐 `lock`: no sirve para esperar múltiples.
- 🔁 `SemaphoreSlim`: requiere lógica extra para liberar a todos.

---

## 🧪 Ejemplo 5: ManualResetEvent para inicio sincronizado

```csharp
private static ManualResetEvent _inicio = new(false);

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
```

🔍 Todos los hilos comienzan al mismo tiempo.

✅ **¿Por qué `ManualResetEvent`?**  
Permite mantener la señal activa, liberando a todos simultáneamente.

📊 **Comparación con otros mecanismos:**
- 🔁 `SemaphoreSlim`: útil, pero no sincroniza arranque conjunto.
- 🔄 `Barrier`: más compleja para esta necesidad.

---

## 🧪 Ejemplo 6: Productor-consumidor simple con AutoResetEvent

```csharp
private static AutoResetEvent _datoDisponible = new(false);
private static int _dato;

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
```

🔍 El consumidor espera hasta que el productor tenga algo disponible.

✅ **¿Por qué `AutoResetEvent`?**  
Sincroniza flujo de datos entre hilos sin variables compartidas explícitas.

📊 **Comparación con otros mecanismos:**
- 🔁 `SemaphoreSlim`: puede funcionar pero permite múltiples accesos.
- 🔄 `Barrier`: no aplica para esta relación uno-a-uno.

---

## 🧪 Ejemplo 7: Reset manual entre ciclos

```csharp
private static ManualResetEvent _evento = new(false);

public static void Esperar()
{
    Console.WriteLine("Esperando...");
    _evento.WaitOne();
    Console.WriteLine("Desbloqueado.");
}

public static void LiberarYResetear()
{
    _evento.Set();
    Thread.Sleep(100); // permitir avanzar
    _evento.Reset();
}
```

🔍 Controla manualmente la señal entre ciclos.

✅ **¿Por qué `ManualResetEvent`?**  
Permite señal persistente que luego se corta.

📊 **Comparación con otros mecanismos:**
- 🔁 `SemaphoreSlim`: no tiene reset explícito.
- 🔄 `Barrier`: no diseñado para ciclos con espera activa.

---

## 🧪 Ejemplo 8: ManualResetEventSlim como alternativa optimizada

```csharp
private static ManualResetEventSlim _evento = new(false);

public static void EsperarSlim()
{
    _evento.Wait();
    Console.WriteLine("Liberado por Slim.");
}

public static void SeñalarSlim()
{
    Thread.Sleep(200);
    _evento.Set();
}
```

🔍 Optimiza uso cuando no se requiere compatibilidad con handles del sistema operativo.

✅ **¿Por qué `ManualResetEventSlim`?**  
Más eficiente cuando no se necesita interoperabilidad con OS.

📊 **Comparación con otros mecanismos:**
- 🔁 `ManualResetEvent`: más pesado si no necesitás interoperabilidad.
- 🔄 `Barrier`: no aplica.

---

## 🧪 Ejemplo 9: Turnos entre hilos con AutoResetEvent

```csharp
private static AutoResetEvent _turno1 = new(true);
private static AutoResetEvent _turno2 = new(false);

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
```

🔍 Alternancia de ejecución entre dos hilos.

✅ **¿Por qué `AutoResetEvent`?**  
Facilita control de turnos con señal uno a uno.

📊 **Comparación con otros mecanismos:**
- 🔁 `lock`: no implementa coordinación de turnos.
- 🔄 `Barrier`: sincroniza fases, no alternancia.

---

## 🧪 Ejemplo 10: Esperar finalización de tarea con ManualResetEvent

```csharp
private static ManualResetEvent _finalizado = new(false);

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
```

🔍 Un hilo espera que otro termine su trabajo.

✅ **¿Por qué `ManualResetEvent`?**  
Permite que varios observadores esperen un mismo evento.

📊 **Comparación con otros mecanismos:**
- 🔁 `lock`, `Monitor`: no funcionan para esta señalización unidireccional.
- 🔄 `Barrier`: no es su propósito.

---
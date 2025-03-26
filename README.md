# Módulo 6: `AutoResetEvent` y `ManualResetEvent` en C#

## ⚠️ ¿Qué son `AutoResetEvent` y `ManualResetEvent`?
Son mecanismos de **señalización entre hilos** que permiten que un hilo "espere" hasta que otro lo despierte.

- `AutoResetEvent` reinicia su estado automáticamente después de liberar un hilo.
- `ManualResetEvent` permanece en señalizado hasta que se reinicie manualmente.

---

## 📊 Diferencias clave

| Característica       | `AutoResetEvent`         | `ManualResetEvent`           |
|-----------------------|---------------------------|-------------------------------|
| Libera un hilo a la vez | ✅ Sí                   | ❌ No, libera a todos los que esperan |
| Estado vuelve a no señalado | Automáticamente | Manualmente con `Reset()`     |
| Ideal para...         | Eventos puntuales         | Sincronización por lotes       |

---

## 🏠 Escenario práctico: **Inicialización de sistema y arranque de módulos**

### Objetivo:
- Simular que el sistema principal inicializa recursos.
- Los módulos esperan hasta que el sistema esté listo para arrancar.

### Archivos

#### `StartupManager.cs`
```csharp
using System;
using System.Threading;

public class StartupManager
{
    private readonly ManualResetEvent _sistemaListo;

    public StartupManager(ManualResetEvent sistemaListo)
    {
        _sistemaListo = sistemaListo;
    }

    public void EsperarYArrancar(string modulo)
    {
        Console.WriteLine($"{modulo} esperando sistema...");
        _sistemaListo.WaitOne();
        Console.WriteLine($"{modulo} arrancando.");
    }
}
```

#### `Program.cs`
```csharp
using System;
using System.Threading;

class Program
{
    static void Main()
    {
        ManualResetEvent sistemaListo = new(false);
        var moduloA = new StartupManager(sistemaListo);
        var moduloB = new StartupManager(sistemaListo);

        new Thread(() => moduloA.EsperarYArrancar("Módulo A")).Start();
        new Thread(() => moduloB.EsperarYArrancar("Módulo B")).Start();

        Console.WriteLine("Inicializando sistema...");
        Thread.Sleep(3000); // Simula trabajo
        Console.WriteLine("Sistema listo!");
        sistemaListo.Set();
    }
}
```

---

## 📈 Alternativa: `AutoResetEvent`
Si usáramos `AutoResetEvent`, solo **un hilo** se liberaría con cada `Set()`.

### Ejemplo simple:
```csharp
AutoResetEvent semaforo = new(false);

new Thread(() => {
    semaforo.WaitOne();
    Console.WriteLine("Hilo 1 liberado");
}).Start();

new Thread(() => {
    semaforo.WaitOne();
    Console.WriteLine("Hilo 2 liberado");
}).Start();

Thread.Sleep(1000);
semaforo.Set(); // Solo libera uno
semaforo.Set(); // Luego el otro
```

---

## 🧼 Buenas prácticas

| Regla | Motivo |
|-------|--------|
| ✅ Usá `ManualResetEvent` para esperar varios hilos a la vez | Ideal para sincronización en bloque |
| ✅ Usá `AutoResetEvent` para control de flujo uno a uno | Evita liberar más de un hilo por accidente |
| ✅ Siempre seteá en `false` al crearlo para iniciar en "espera" | Comportamiento predecible |

---

# Set 3 – Multithreading with the Thread Class

| Carpeta | Fichero | Tarea | Puntos |
|---|---|---|---|
| [Ej1](./Ej1) | `Task1.cs` | Basic Thread Creation and Coordination | 20 |
| [Ej2](./Ej2) | `Task2.cs` | Parallel Download Simulation | 20 |
| [Ej3](./Ej3) | `Task3.cs` | Race Condition and Thread Synchronization | 25 |
| [Ej4](./Ej4) | `Task4.cs` | Parallel Matrix Processing | 35 |

Cada carpeta es una aplicación de consola independiente (.NET 10). Para ejecutarla:

```bash
cd Task3/Ej1
dotnet run
```

## Task 1 – Basic Thread Creation and Coordination
Genera un array de 100 enteros aleatorios y lanza tres hilos con nombre
(`SumCalculatorThread`, `MinMaxCalculatorThread`, `AverageCalculatorThread`).
El hilo principal espera con `Join()` y muestra suma, mínimo, máximo y media.

## Task 2 – Parallel Download Simulation
Simula 5 descargas (2 s a 6 s con `Thread.Sleep()`), un hilo con nombre por fichero.
Cada 500 ms el hilo principal consulta `IsAlive` y muestra los hilos activos.
La suma de duraciones es 20 s, mientras que la ejecución concurrente tarda ~6 s
(la descarga más larga).

## Task 3 – Race Condition and Thread Synchronization
20 hilos incrementan un contador compartido 100 000 veces cada uno.
Valor esperado: 20 × 100 000 = **2 000 000**.

- **Parte A (sin sincronización):** `counter++` no es atómico (leer–modificar–escribir),
  por lo que se pierden incrementos y el resultado suele ser menor (p. ej. 1 958 283).
- **Parte B (con `lock`):** el incremento se protege con `lock (counterLock)` y el
  resultado coincide siempre con 2 000 000.

## Task 4 – Parallel Matrix Processing
Suma de dos matrices 6000 × 6000 de enteros, secuencial y paralela con 4 hilos
(cada hilo procesa un rango contiguo de filas; el último recoge el resto si la
división no es exacta). Los tiempos se miden con `Stopwatch`, tras una ejecución
de calentamiento y promediando 5 repeticiones. Se comprueba que ambos resultados
son idénticos y se calcula `Speedup = T_sequential / T_parallel`.

Resultados de ejemplo (12 núcleos lógicos):

| Configuración | T secuencial | T paralelo | Speedup |
|---|---|---|---|
| Debug (`dotnet run`) | ~133 ms | ~55 ms | ~2.4x |
| Release (`dotnet run -c Release`) | ~43 ms | ~43 ms | ~1.0x |

El speedup es menor que 4 porque la suma de matrices es una operación
**limitada por el ancho de banda de memoria** (una suma por cada 12 bytes leídos/escritos).
En Release el compilador JIT vectoriza el bucle y un solo hilo ya satura la memoria,
así que añadir hilos apenas ayuda. También influyen el coste de crear los hilos,
la latencia de memoria y la planificación del sistema operativo.

## Documentación
Para la entrega, añadir una captura de pantalla de la ejecución de cada tarea.

# ⚡ Distributed Processing

> **Universidad:** WSB Merito Gdańsk (Programa Erasmus+)  
> **Grado:** Ingeniería Informática de Software  
> **Tecnologías clave:** Apache Spark, Apache Kafka, PySpark / Scala, Docker, Hadoop HDFS, Distributed Systems, C#, .NET

---

## 📌 Descripción del Repositorio

Este repositorio reúne las prácticas, proyectos y flujos de procesamiento masivo desarrollados en la asignatura **Distributed Processing**. El objetivo principal es dominar los conceptos clave de la computación distribuida, la tolerancia a fallos y la construcción de pipelines de datos tanto en lote (Batch) como en tiempo real (Event Streaming) utilizando el ecosistema de Big Data y bases sólidas de programación en C#.

---

## 🛠️ Herramientas y Stack Tecnológico

* **Procesamiento Distribuido:** Apache Spark (DataFrames, Spark SQL, Structured Streaming)
* **Ingesta y Event Streaming:** Apache Kafka (Producers, Consumers, Topics, Zookeeper / KRaft)
* **Lenguajes:** Python (PySpark) / Scala / Java / C#
* **Infraestructura & Contenedores:** Docker, Docker Compose, Hadoop HDFS
* **Formatos de Datos:** Parquet, Avro, JSON, CSV

---

## 🗂️ Estructura de Prácticas y Proyectos

### 📁 Task 1: C# Revision & Practical Tasks
La carpeta principal `Task1` contiene los siguientes proyectos de desarrollo en C# y .NET:
* 📂 **[AvaloniaCalculator](./Task1/AvaloniaCalculator)**: Aplicación de escritorio con interfaz gráfica basada en Avalonia UI.
* 📂 **[BIMCalculator](./Task1/BIMCalculator)**: Aplicación de consola para el cálculo del Índice de Masa Corporal.
* 📂 **[ElectricityConsumption](./Task1/ElectricityConsumption)**: Analizador en consola de consumo eléctrico semanal.
* 📂 **[EmployeeManagementSystem](./Task1/EmployeeManagementSystem)**: Sistema de gestión de empleados con persistencia en archivos de texto plano.
* 📂 **[SimpleCalculator](./Task1/SimpleCalculator)**: Calculadora gráfica desarrollada en Windows Forms.
* 📂 **[StudentGradeManager](./Task1/StudentGradeManager)**: Gestor de calificaciones de estudiantes basado en Programación Orientada a Objetos (OOP).
* 📂 **[VehicleRentalSystem](./Task1/VehicleRentalSystem)**: Sistema de gestión de alquiler de vehículos mediante colecciones y clases en C#.

### 📁 Task 2: Consumo de APIs REST y JSON en C#
La carpeta `Task2` contiene cuatro aplicaciones de consola que consumen la API pública [JSONPlaceholder](https://jsonplaceholder.typicode.com) mediante `WebClient` y `System.Text.Json`:
* 📂 **[Ej1](./Task2/Ej1)** (`Task1.cs`): Petición GET de un usuario y deserialización del JSON a un objeto `User`.
* 📂 **[Ej2](./Task2/Ej2)** (`Task2.cs`): Descarga de todos los posts y filtrado con LINQ de los de un usuario concreto.
* 📂 **[Ej3](./Task2/Ej3)** (`Task3.cs`): Creación de un post mediante una petición POST con datos introducidos y validados por consola.
* 📂 **[Ej4](./Task2/Ej4)** (`Task4.cs`): Sistema de gestión de posts con menú y operaciones CRUD completas (GET, POST, PUT, DELETE) encapsuladas en una clase `PostService`.

### 📁 Task 3: Multithreading con la clase Thread
La carpeta `Task3` contiene cuatro aplicaciones de consola sobre concurrencia con `System.Threading` y medición de rendimiento con `Stopwatch` (más detalles en su [README](./Task3/README.md)):
* 📂 **[Ej1](./Task3/Ej1)** (`Task1.cs`): Creación de tres hilos con nombre que calculan suma, mínimo/máximo y media de un array, sincronizados con `Join()`.
* 📂 **[Ej2](./Task3/Ej2)** (`Task2.cs`): Simulación de cinco descargas concurrentes, monitorizando los hilos activos cada 500 ms con `IsAlive`.
* 📂 **[Ej3](./Task3/Ej3)** (`Task3.cs`): Demostración de una condición de carrera con 20 hilos sobre un contador compartido y su corrección con `lock`.
* 📂 **[Ej4](./Task3/Ej4)** (`Task4.cs`): Suma de matrices secuencial frente a paralela con 4 hilos repartiendo filas, con cálculo del speedup.


---

## 🚀 Requisitos y Ejecución

1. **Clonar el repositorio:**
   ```bash
   git clone https://github.com/DaviidRicoo/distributed-processing.git
   ```

2. **Ejecutar un proyecto de consola de C#** (requiere el SDK de .NET 10):
   ```bash
   cd distributed-processing/Task3/Ej1
   dotnet run
   ```
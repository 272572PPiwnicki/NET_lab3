# 🧮 Matrix Multiplication Benchmark (.NET 8.0)

---

## 📌 Informacje

- **Autor:** *Patryk Piwnicki*
- **Prowadzący:** mgr inż. Michał Jaroszczuk
- **Grupa:** [SR][17:05]
- **Data:** 16 kwietnia 2025

---

## 🔧 Technologie

- C#
- .NET 8.0
- Visual Studio 2022
- `System.Threading`
- `System.Threading.Tasks`

---

## ⚙️ Opis działania

Program generuje losowe macierze o zadanym rozmiarze, a następnie dokonuje ich mnożenia trzema różnymi metodami:
1. **Sekwencyjnie** – klasyczna pętla `for`.
2. **Równolegle (`Parallel`)** – z użyciem `Parallel.For` oraz `ParallelOptions` z określoną liczbą wątków.
3. **Wielowątkowo (`Thread`)** – z użyciem klasy `Thread`, ręcznego podziału pracy i synchronizacji z `lock`.

Dla każdego podejścia mierzony jest czas wykonania. Program umożliwia przetestowanie działania algorytmu z różną liczbą wątków oraz dla różnych rozmiarów macierzy.

---

## 🌲 Drzewo projektu

![image](https://github.com/user-attachments/assets/05ac89a5-9c32-460c-a183-0e0c7ed05868)

---

## 📐 Parametry wejściowe

- Rozmiar macierzy (`size`)
- Liczba wątków (`1`, `2`, `4`, `8`)

---

## 🧩 Kluczowe klasy

- **Matrix** – struktura macierzy (losowe generowanie, indeksowanie, `ToString()`).
- **MatrixMultiplier** – zawiera metody:
  - `MultiplySequential(Matrix A, Matrix B)`
  - `MultiplyParallel(Matrix A, Matrix B, int threads)`

![image](https://github.com/user-attachments/assets/8bd0ec39-1e08-43f3-be5a-0ce404559b06)

- **MatrixMultiplierThread** – metoda `MultiplyWithThreads(...)` implementuje równoległe mnożenie z `Thread`, z użyciem `lock` do zabezpieczenia danych.

![image](https://github.com/user-attachments/assets/d70d3758-7f4f-4cd2-b87f-b82ae2c350da)

- **Program** – główna logika aplikacji: generowanie danych, pomiar czasu, wyświetlanie wyników.

---

## 📈 Wyniki badań

Badania zostały przeprowadzone dla macierzy o rozmiarach:
- `250x250`
- `500x500`
- `750x750`
- `1000x1000`

Dla każdej metody i liczby wątków (1, 2, 4, 8) wykonano kilka pomiarów, a wyniki zostały uśrednione.

---

## 📄 Graficzne przedstawienie wyników

![image](https://github.com/user-attachments/assets/fadbe41b-3131-46c3-9ec2-e50c36a87b2d)

![image](https://github.com/user-attachments/assets/82b69376-c871-4ef9-8b12-d211ef4084b2)

![image](https://github.com/user-attachments/assets/10aa1870-9282-4d5d-b730-7423c104bc0d)

![image](https://github.com/user-attachments/assets/c9517583-5794-4a90-90c7-51834b902b74)

## 🔬 Wnioski:

- Wzrost liczby wątków skutkuje skróceniem czasu wykonania dla metod `Parallel` i `Thread`, szczególnie przy większych rozmiarach macierzy.
- W przypadku `Parallel`, największe przyspieszenie obserwowane jest przy macierzach `750x750` i `1000x1000`, co potwierdza dobrą skalowalność algorytmu.
- Podejście `Thread` daje zbliżone wyniki, ale generuje większy narzut przy mniejszych rozmiarach.
- Dla małych macierzy różnice między metodami są niewielkie – narzut wielowątkowości czasem przewyższa zysk z równoległości.
- Użycie `lock` zabezpiecza dane przed wyścigami przy dostępie do współdzielonej macierzy wynikowej w wersji `Thread`.

---

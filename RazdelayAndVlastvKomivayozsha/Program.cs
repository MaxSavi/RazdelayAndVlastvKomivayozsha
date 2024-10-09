using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        int[,] graph = 
        {
      //     A   B  C   D
      /*A*/{ 0, 29, 20, 21 },
      /*B*/{ 29, 0, 15, 17 },
      /*C*/{ 20, 15, 0, 28 },
      /*D*/{ 21, 17, 28, 0 }
        };


        Dictionary<int, string> cityNames = new Dictionary<int, string>
        {
            { 0, "A" },
            { 1, "B" },
            { 2, "C" },
            { 3, "D" }
        };

        int startCity = 0; // Начальный город

        // получение кратчайшего пути
        List<int> path = TravelingSalesman(graph, startCity);

        // Вывод пути
        Console.WriteLine("Оптимальный путь:");
        foreach (int city in path)
        {
            Console.Write(cityNames[city] + " ");
        }
        Console.WriteLine(cityNames[path[0]]); // Возвращение в начальный город

        // Вывод стоимости
        Console.WriteLine("\nМинимальная стоимость: " + CalculatePathCost(graph, path));
        Console.ReadLine();
    }

    static List<int> TravelingSalesman(int[,] graph, int startCity)
    {
        int n = graph.GetLength(0);
        List<int> cities = new List<int>();
        for (int i = 0; i < n; i++)
        {
            if (i != startCity)
                cities.Add(i);
        }

        List<int> bestPath = null;
        int minCost = int.MaxValue;

        // перебор всех возможных перестановок городов
        foreach (var perm in GetPermutations(cities, cities.Count))
        {
            List<int> path = new List<int> { startCity };
            path.AddRange(perm);
            path.Add(startCity);

            int cost = CalculatePathCost(graph, path);
            if (cost < minCost)
            {
                minCost = cost;
                bestPath = path;
            }
        }

        return bestPath;
    }

    static IEnumerable<List<T>> GetPermutations<T>(List<T> list, int length)
    {
        if (length == 1) return list.Select(t => new List<T> { t });

        return GetPermutations(list, length - 1)
            .SelectMany(t => list.Where(e => !t.Contains(e)),
                        (t1, t2) => t1.Concat(new List<T> { t2 }).ToList());
    }

    static int CalculatePathCost(int[,] graph, List<int> path)
    {
        int cost = 0;
        for (int i = 0; i < path.Count - 1; i++)
        {
            cost += graph[path[i], path[i + 1]];
        }
        return cost;
    }

}

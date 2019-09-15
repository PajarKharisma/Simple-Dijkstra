using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra
{
    class Program
    {
        private static readonly int NO_PARENT = -1;

        private static void dijkstra(int[,] adjacencyMatrix, int startVertex)
        {
            int nVertices = adjacencyMatrix.GetLength(0);
            int[] shortestDistances = new int[nVertices];
            bool[] added = new bool[nVertices];
            for (int vertexIndex = 0; vertexIndex < nVertices; vertexIndex++)
            {
                shortestDistances[vertexIndex] = int.MaxValue;
                added[vertexIndex] = false;
            }

            shortestDistances[startVertex] = 0;

            int[] parents = new int[nVertices];
            parents[startVertex] = NO_PARENT;
            for (int i = 1; i < nVertices; i++)
            {
                int nearestVertex = -1;
                int shortestDistance = int.MaxValue;
                for (int vertexIndex = 0; vertexIndex < nVertices; vertexIndex++)
                {
                    if (!added[vertexIndex] && shortestDistances[vertexIndex] < shortestDistance)
                    {
                        nearestVertex = vertexIndex;
                        shortestDistance = shortestDistances[vertexIndex];
                    }
                }

                added[nearestVertex] = true; 
                for (int vertexIndex = 0; vertexIndex < nVertices; vertexIndex++)
                {
                    int edgeDistance = adjacencyMatrix[nearestVertex, vertexIndex];

                    if (edgeDistance > 0 && ((shortestDistance + edgeDistance) < shortestDistances[vertexIndex]))
                    {
                        parents[vertexIndex] = nearestVertex;
                        shortestDistances[vertexIndex] = shortestDistance + edgeDistance;
                    }
                }
            }

            printSolution(startVertex, shortestDistances, parents);
        }

        private static void printSolution(int startVertex, int[] distances, int[] parents)
        {
            int nVertices = distances.Length;
            Console.Write("Awal -> Tujuan \t Jarak \t Titik yang dilewati");

            for (int vertexIndex = 0;
                    vertexIndex < nVertices;
                    vertexIndex++)
            {
                if (vertexIndex != startVertex)
                {
                    Console.Write("\n" + (startVertex+1) + " -> ");
                    Console.Write((vertexIndex+1) + " \t\t ");
                    Console.Write(distances[vertexIndex] + "\t\t");
                    printPath(vertexIndex, parents);
                }
            }
        }

        private static void printPath(int currentVertex, int[] parents)
        {
            if (currentVertex == NO_PARENT)
            {
                return;
            }
            printPath(parents[currentVertex], parents);
            Console.Write((currentVertex+1) + " ");
        }
        static void Main(string[] args)
        {
            int[,] graph = {
                { 0, 1, 0, 3, 100},
                { 1, 0, 5, 0, 0},
                { 0, 5, 0, 2, 1},
                { 3, 0, 2, 0, 6},
                { 100, 0, 1, 6, 0},
            };

            int titikAwal = 1;
            dijkstra(graph, titikAwal-1);
            Console.ReadLine();
        }
    }
}

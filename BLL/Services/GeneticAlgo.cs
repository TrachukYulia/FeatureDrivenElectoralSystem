using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class GeneticAlgo
    {
        private int[,] _matrixOfFeaturesAndItems;
        private int[,] _populationMatrix;
        private int[,] _validMatrix;
        public int[,] Population => _populationMatrix;
        public int[,] VakidMatrix => _validMatrix;
        public GeneticAlgo(int[,] matrixOfFeaturesAndItems) {
            _matrixOfFeaturesAndItems = matrixOfFeaturesAndItems;
            _validMatrix = CheckValid(_matrixOfFeaturesAndItems);
            PrintMatrix(_validMatrix);
            _populationMatrix = CreatePopulation(_validMatrix);
            Console.WriteLine("Our population");
            PrintMatrix(_populationMatrix);
            Console.WriteLine();
        }
       public List<int> GetSolution(int[,] matrix, int[,] population)
        {
            int[] firstParent, secondParent;
            int[] firstChild, secondChild;
            var newPopulation = new int[population.GetLength(0), population.GetLength(1)];
            int iteration = 0;
            int numberOfIterationToStop = 0;
            int numberToStop = 10;
            var prevMinUseful = CounterOfUsefulPopulation(population).Min();
            while (true)
            {
                SelectParents(population, out firstParent, out secondParent);
                CrossingOver(matrix, firstParent, secondParent, out firstChild, out secondChild);
                (firstChild, secondChild) = Mutation(matrix, firstChild, secondChild);
                population = UpdatePopulation(population, firstChild, secondChild);
                var currentMinUseful = CounterOfUsefulPopulation(population).Min();
                if (currentMinUseful == prevMinUseful)
                    numberOfIterationToStop++;
                else
                    prevMinUseful = currentMinUseful;
                if (numberOfIterationToStop == numberToStop)
                    break;
                iteration++;
            }
            int min_index = IndexOfMin(CounterOfUsefulPopulation(population));
            int[] the_best_individ = GetRow(population, min_index);
            List<int> indexOfItems = new List<int>();

            for (int i = 0; i < the_best_individ.Length; i++)
            {
                if (the_best_individ[i] == 1)
                {
                    indexOfItems.Add(i);
                }
            }

            return indexOfItems;
        }
        public List<int> GetSolutionByGeneticAlgWithPrint(int[,] matrix, int[,] population)
        {
            int[] firstParent, secondParent;
            int[] firstChild, secondChild;
            var newPopulation = new int[population.GetLength(0), population.GetLength(1)];
            int iteration = 0;
            int numberOfIterationToStop = 0;
            int numberToStop = 10;
            var prevMinUseful = CounterOfUsefulPopulation(population).Min();
            while (true)
            {
                Console.WriteLine($"Iteration: {iteration} ");
                Console.WriteLine("-----------------------------");
                Console.WriteLine("Selected parents:");
                SelectParents(population, out firstParent, out secondParent);
                Console.WriteLine("First parent");
                PrintArray(firstParent);
                Console.WriteLine("Second parent");
                PrintArray(secondParent);
                Console.WriteLine();

                CrossingOver(matrix, firstParent, secondParent, out firstChild, out secondChild);
                Console.WriteLine("Get a childs:");
                Console.WriteLine("First child");
                PrintArray(firstChild);
                Console.WriteLine("Second child");
                PrintArray(secondChild);
                Console.WriteLine();

                Console.WriteLine("Mutation:");
                (firstChild, secondChild) = Mutation(matrix, firstChild, secondChild);
                Console.WriteLine("Get a childs:");
                Console.WriteLine("First child");
                PrintArray(firstChild);
                Console.WriteLine("Second child");
                PrintArray(secondChild);
                Console.WriteLine("Update Population:");
                Console.WriteLine("New Population:");
                newPopulation = UpdatePopulation(population, firstChild, secondChild);
                PrintMatrix(newPopulation);
                Console.WriteLine("Useful:");
                PrintArray(CounterOfUsefulPopulation(newPopulation));

                var currentMinUseful = CounterOfUsefulPopulation(newPopulation).Min();
                if (currentMinUseful == prevMinUseful)
                    numberOfIterationToStop++;
                else
                    prevMinUseful = currentMinUseful;
                if (numberOfIterationToStop == numberToStop)
                    break;
                iteration++;
            }
            int min_index = IndexOfMin(CounterOfUsefulPopulation(population));
            int[] the_best_individ = GetRow(population, min_index);
            List<int> indexOfItems = new List<int>();
            Console.WriteLine("The best individ");
            PrintArray(the_best_individ);
            Console.WriteLine($"The best individ GF: {CounterOfUsefulOfIndivid(the_best_individ)}");
            for (int i = 0; i < the_best_individ.Length; i++)
            {
                if (the_best_individ[i] == 1)
                {
                    indexOfItems.Add(i);
                }
            }

            Console.WriteLine("Indexec of the items");
            foreach (int i in indexOfItems)
                Console.WriteLine(i);
            return indexOfItems;
        }

     

        static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j]);
                    Console.Write(' ');
                }
                Console.WriteLine();
            }
        }

        static void PrintArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i]);
                Console.Write(' ');

            }
            Console.WriteLine();

        }
        static void SelectParents(int[,] population, out int[] firstParent, out int[] secondParent)
        {
            Random random = new Random();

            // Выбор первого родителя
            int firstParentIndex = random.Next(population.GetLength(0));
            firstParent = GetRow(population, firstParentIndex);
            // Выбор второго родителя
            int secondParentIndex;
            do
            {
                secondParentIndex = random.Next(population.GetLength(0));
            } while (secondParentIndex == firstParentIndex); // Повторяем, пока индекс не будет отличаться от первого родителя

            secondParent = GetRow(population, secondParentIndex);
        }
        static (int[], int[]) Mutation(int[,] parlamenters, int[] first_child, int[] second_child)
        {
            Random random = new Random();

            // Выбор случайного ребенка для мутации
            int child_index = random.Next(2);
            int mutating_point = random.Next(first_child.Length);
            Console.WriteLine("Mutation  point" + mutating_point);

            if (child_index == 0)
            {
                Console.WriteLine("Mutation first child");
                first_child[mutating_point] = (first_child[mutating_point] == 1) ? 0 : 1;
                first_child = CheckCondition(parlamenters, first_child);
            }
            else
            {
                Console.WriteLine("Mutation second child");
                second_child[mutating_point] = (second_child[mutating_point] == 1) ? 0 : 1;
                second_child = CheckCondition(parlamenters, second_child);
            }

            return (first_child, second_child);
        }

        static void CrossingOver(int[,] matrix, int[] firstParent, int[] secondParent, out int[] firstChild, out int[] secondChild)
        {
            Random random = new Random();

            int crossingPoint = random.Next(0, firstParent.Length - 1);
            Console.WriteLine("Crossing point: " + crossingPoint);

            firstChild = new int[firstParent.Length];
            secondChild = new int[firstParent.Length];

            // Копируем элементы до точки пересечения
            for (int i = 0; i <= crossingPoint; i++)
            {
                firstChild[i] = firstParent[i];
                secondChild[i] = secondParent[i];
            }

            // Копируем элементы после точки пересечения
            for (int i = crossingPoint + 1; i < firstParent.Length; i++)
            {
                firstChild[i] = secondParent[i];
                secondChild[i] = firstParent[i];
            }

            Console.WriteLine("Child 1:");
            PrintArray(firstChild);
            Console.WriteLine("Child 2:");
            PrintArray(secondChild);

            firstChild = CheckCondition(matrix, firstChild);
            secondChild = CheckCondition(matrix, secondChild);
        }

        static int[,] CreatePopulation(int[,] matrix)
        {
            int rows = (int)Math.Ceiling((double)matrix.GetLength(0) / 2);
            int columns = matrix.GetLength(0);
            var population = new int[rows, columns];
            var tempIndivid = new int[columns];
            var tempCoveringSign = new int[matrix.GetLength(1)];
            bool flag;
            Console.WriteLine("Create Ppopulation");
            PrintMatrix(population);
            Console.WriteLine("GenerateRandomPopulation");
            for (int i = 0; i < rows; i++)
            {
                tempIndivid = GenerateRandomIndivid(columns);
                population = CopyArrayToMatrix(tempIndivid, population, i);
            }
            PrintMatrix(population);
            for (int i = 0; i < rows; i++)
            {
                //Console.WriteLine($"Check covering of sign per individ: {i}");
                //tempCoveringSign = GetCoveredSign(matrix, GetRow(population, i));
                //PrintArray(tempCoveringSign);
                Console.WriteLine($"Check condition and retrieve true individ: {i}");
                tempIndivid = CheckCondition(matrix, GetRow(population, i));
                PrintArray(tempIndivid);
                flag = false;
                //Проверяем, есть ли одинаковые индивиды
                while (!flag)
                {
                    for (int index = 0; index < population.GetLength(0); index++)
                    {
                        if (EnumerableSequenceEqual(tempIndivid, GetRow(population, index)))
                        {
                            int[] newRow = GenerateRandomIndivid(columns);
                            for (int j = 0; j < columns; j++)
                            {
                                population[i, j] = newRow[j];
                            }
                            tempIndivid = CheckCondition(matrix, GetRow(population, i));
                            flag = true;
                        }
                        else
                        {
                            flag = true;
                        }

                    }
                }
                Console.WriteLine($"Check condition and retrieve trnewnenwnewue individ: {i}");
                tempIndivid = CheckCondition(matrix, GetRow(population, i));
                PrintArray(tempIndivid);
                Console.WriteLine($"Check covering of sign per new individ: {i}");
                tempCoveringSign = GetCoveredSign(matrix, tempIndivid);
                PrintArray(tempCoveringSign);
                population = CopyArrayToMatrix(tempIndivid, population, i);
            }
            return population;
        }
        static bool EnumerableSequenceEqual(int[] first, int[] second)
        {
            if (first.Length != second.Length)
                return false;
            for (int i = 0; i < first.Length; i++)
            {
                if (first[i] != second[i])
                    return false;
            }
            return true;
        }
        static int IndexOfMin(int[] array)
        {
            int minIndex = 0;
            int minValue = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < minValue)
                {
                    minIndex = i;
                    minValue = array[i];
                }
            }

            return minIndex;
        }
        static int IndexOfMax(int[] array)
        {
            int maxIndex = 0;
            int maxValue = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > maxValue)
                {
                    maxIndex = i;
                    maxValue = array[i];
                }
            }

            return maxIndex;
        }
        static int[,] SetRow(int[,] array, int rowIndex, int[] newRow)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                array[rowIndex, j] = newRow[j];
            }
            return array;
        }
        static int[,] UpdatePopulation(int[,] population, int[] firstChild, int[] secondChild)
        {
            bool first_child_exists = false;
            bool second_child_exists = false;

            // Перевірка наявності нащадків у популяції
            for (int i = 0; i < population.GetLength(0); i++)
            {
                int[] current_individ = GetRow(population, i);

                if (current_individ.SequenceEqual(firstChild))
                {
                    first_child_exists = true;
                }
                else if (current_individ.SequenceEqual(secondChild))
                {
                    second_child_exists = true;
                }
            }

            // Якщо в популяції немає особин із такими ж значеннями як в нащадків
            if (!first_child_exists)
            {
                // Шукаємо особину з найбільшим значенням корисності
                int max_useful_index = IndexOfMax(CounterOfUsefulPopulation(population));
                int[] max_useful_individ = GetRow(population, max_useful_index);

                // Якщо є значення корисності більше ніж у нащадка
                if (CounterOfUsefulOfIndivid(max_useful_individ) > CounterOfUsefulOfIndivid(firstChild))
                {
                    // Додаємо нащадка до популяції замість особини із найбільшим значенням корисності
                    population = SetRow(population, max_useful_index, firstChild);
                }
            }

            if (!second_child_exists)
            {
                int max_useful_index = IndexOfMax(CounterOfUsefulPopulation(population));
                int[] max_useful_individ = GetRow(population, max_useful_index);

                if (CounterOfUsefulOfIndivid(max_useful_individ) > CounterOfUsefulOfIndivid(secondChild))
                {
                    population = SetRow(population, max_useful_index, secondChild);
                }
            }
            return population;
        }
        static int[] CounterOfUsefulPopulation(int[,] population)
        {
            int[] array_of_useful = new int[population.GetLength(0)];
            for (int i = 0; i < population.GetLength(0); i++)
            {
                int sum = 0;
                for (int j = 0; j < population.GetLength(1); j++)
                {
                    sum += population[i, j];
                }
                array_of_useful[i] = sum;
            }
            return array_of_useful;
        }
        static int CounterOfUsefulOfIndivid(int[] individ)
        {
            int useful = individ.Sum();
            return useful;
        }

        static int[] CheckCondition(int[,] matrix, int[] individ)
        {
            Random random = new Random();
            var coveringSign = GetCoveredSign(matrix, individ);

            while (true)
            {
                Console.WriteLine("Covering sign: " + string.Join(" ", coveringSign));
                if (coveringSign.Sum() == matrix.GetLength(1))
                {
                    Console.WriteLine("All conditions are covered.");
                    break;
                }
                else
                {
                    var zeroIndices = individ
            .Select((value, index) => new { Value = value, Index = index })
            .Where(item => item.Value == 0)
            .Select(item => item.Index)
            .ToList();
                    int randomIndex;
                    if (zeroIndices.Count() != 0)
                    {
                        randomIndex = zeroIndices[random.Next(zeroIndices.Count - 1)];
                    }
                    else
                    {
                        randomIndex = Array.IndexOf(individ, 0);
                    }
                    individ[randomIndex] = 1;
                    coveringSign = GetCoveredSign(matrix, individ);
                }
            }

            if (individ.Sum() == individ.Length)
            {
                individ = RecreateTheWorstIndivid(matrix, individ);
            }

            return individ;

        }
        static int[] RecreateTheWorstIndivid(int[,] matrix, int[] individ)
        {
            Random random = new Random();
            int[] tempArray = new int[individ.Length];
            Array.Copy(individ, tempArray, individ.Length);

            int iteration = 0;
            while (true)
            {
                int index = random.Next(0, individ.Length);
                tempArray[index] = 0;
                int[] coveringSign = GetCoveredSign(matrix, tempArray);

                if (coveringSign.Sum() == matrix.GetLength(1))
                {
                    individ[index] = 0;
                    break;
                }
                else
                {
                    tempArray[index] = 1;
                    iteration++;

                    if (iteration == (individ.Length))
                    {
                        break;
                    }
                }
            }

            return individ;
        }
        static int[] GetCoveredSign(int[,] matrix, int[] individ)
        {

            int[] array_of_sign = new int[matrix.GetLength(1)];

            if (individ.All(x => x == 1))
            {
                // Заполнить array_of_sign единицами
                for (int i = 0; i < array_of_sign.Length; i++)
                {
                    array_of_sign[i] = 1;
                }
            }
            else
            {
                int counter = 0;
                // Заполнить array_of_sign в соответствии с текущим алгоритмом
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        if (individ[i] == 1 && matrix[i, j] == 1)
                        {
                            array_of_sign[j] = 1;
                            break;
                        }
                    }
                }
            }
            return array_of_sign;
        }

        static int[] GetRow(int[,] matrix, int index)
        {
            var newArray = new int[matrix.GetLength(1)];
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                newArray[j] = matrix[index, j];
            }
            return newArray;
        }
        static int[,] CopyArrayToMatrix(int[] array, int[,] matrix, int index)
        {
            for (int j = 0; j < array.Length; j++)
            {
                matrix[index, j] = array[j]; // Присваиваем каждый элемент новой строки в матрицу
            }
            return matrix;
        }
        static int[] GenerateRandomIndivid(int columns)
        {
            int[] newIndivid = new int[columns];
            Random random = new Random();
            for (int i = 0; i < columns; i++)
            {
                bool randomBool = random.Next(2) == 0; // True или False с равной вероятностью
                newIndivid[i] = randomBool ? 1 : 0;

            }
            return newIndivid;
        }
        static int[,] CheckValid(int[,] matrix)
        {
            int[] columnSums = new int[matrix.GetLength(1)];
            int sum;
            List<int> columnsIndexToDelete = new List<int>();
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                sum = 0;
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    sum += matrix[j, i];
                }
                columnSums[i] = sum;
                if (sum == 0)
                    columnsIndexToDelete.Add(i);
            }
            Console.WriteLine("Sum per col:");
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.WriteLine($"Column {j + 1}: {columnSums[j]}");
            }


            if (columnsIndexToDelete.Count > 0)
            {
                int newCountOfColumn = matrix.GetLength(1) - columnsIndexToDelete.Count;
                var newMatrix = new int[matrix.GetLength(0), newCountOfColumn];

                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    int targetColumn = 0;
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (!columnsIndexToDelete.Contains(j))
                        {
                            newMatrix[i, targetColumn] = matrix[i, j];
                            targetColumn++;
                        }
                    }
                }

                return newMatrix;
            }


            return matrix;
        }
        static int[,] CreateMatrix(int rows, int cols)
        {
            //int rows = matrix.GetLength(0);
            //int cols = matrix.GetLength(1);
            int k = 0;
            int[,] array = new int[,]
            {
        { 1,0,0,1,0,1,0 },
        { 0,1,0,0,1,0,1 },
        { 1,0,1,0,0,1,0 },
        { 0,1,0,1,0,0,1 },
        { 1,0,0,1,0,1,0 },
        { 1,0,0,1,1,0,0},
            };

            //for (int i = 0; i < array.GetLength(0); i++)
            //{
            //    for (int j = 0; j < array.GetLength(1); j++)
            //    {
            //        array[i, j] = k;
            //    }
            //}
            return array;
        }

    }
}

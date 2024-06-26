﻿using AutoMapper.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
public class GreedyAlg
    {
        private int[,] _matrix;
        private int[] _features;
        private int[] _items;
        private int[] _result;

        public GreedyAlg(int[,] matrix)
        {
            _matrix = CheckValid(matrix);
            _features = Enumerable.Range(0, _matrix.GetLength(1)).ToArray();
            _items = Enumerable.Range(0, _matrix.GetLength(0)).ToArray();
            _result = new int[_matrix.GetLength(0)];
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
        public int[] GreedySolution()
        {
            //first item choosed by random
            Random random = new Random();
            int itemIndex = random.Next(_items.Length);
            AddItem(_items[itemIndex]);

            while (_features.Length != 0) // untill all features wont be cover
            {
                
                int[] featureNums = new int[_items.Length]; 
                //count of all features, that covered every item in array
                for (int i = 0; i < _items.Length; i++)
                {
                    int[] f = CheckFeatures(_items[i]); 
                    featureNums[i] = CountFeaturesToCover(f);
                }
                // choose item, that cover the most amount of feature
                int itemNum = Array.IndexOf(featureNums, featureNums.Max());
                AddItem(_items[itemNum]);
            }
            //select Items
            int maxResult = _result.Max();
            int[] selectedItems = Enumerable.Range(0, _result.Length)
                                             .Where(i => _result[i] == maxResult)
                                             .Select(i => i)
                                             .ToArray();
            return selectedItems;
        }

        private int[] CheckFeatures(int item)
        {
            return Enumerable.Range(0, _matrix.GetLength(1))
                             .Where(i => _matrix[item, i] == 1)
                             .ToArray();
        }

        private int CountFeaturesToCover(int[] fList)
        {
            int num = 0;
            foreach (var i in fList)
            {
                if (_features.Contains(i))
                {
                    num++;
                }
            }
            return num;
        }

        private void AddItem(int item)
        {
            int[] featuresList = CheckFeatures(item);
            _features = _features.Where(f => !featuresList.Contains(f)).ToArray();
            this._items = this._items.Where(i => i != item).ToArray();
            _result[item] = 1;
        }
    }

}

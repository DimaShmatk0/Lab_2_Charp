using System;
using System.Threading;

namespace Lab_2_Charp
{
    class RangeMinFinder
    {
        private readonly int startIndex, finishIndex;
        private readonly int[] arr;
        private int minValue;
        private int minIndex;
        private readonly ParallelMinController controller;

        public RangeMinFinder(int start, int finish, int[] array, ParallelMinController ctrl)
        {
            startIndex = start;
            finishIndex = finish;
            arr = array;
            controller = ctrl;
        }

        public int MinValue => minValue;
        public int MinIndex => minIndex;

        public void Run()
        {
            minValue = arr[startIndex];
            minIndex = startIndex;
            for (int i = startIndex + 1; i < finishIndex; i++)
            {
                if (arr[i] < minValue)
                {
                    minValue = arr[i];
                    minIndex = i;
                }
            }
            controller.IncrementFinishedThread();
        }
    }
}

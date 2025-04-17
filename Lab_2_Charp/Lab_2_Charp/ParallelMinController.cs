using System;

namespace Lab_2_Charp
{
    class ParallelMinController
    {
        private readonly int length;
        private readonly int threadNum;
        private readonly int[] arr;
        private readonly RangeMinFinder[] arrayRangeMinFinder;
                
        public ParallelMinController(int[] arr,int length, int threadNum)
        {
            this.length = length;
            this.threadNum = threadNum;
            this.arr = arr;
            arrayRangeMinFinder = new RangeMinFinder[threadNum];
        }

        

        private int[,] GetSegmentRange()
        {
            var ranges = new int[threadNum, 2];
            int baseSize = length / threadNum;
            int remainder = length % threadNum;
            int start = 0;

            for (int i = 0; i < threadNum; i++)
            {
                int segmentSize = baseSize + (i < remainder ? 1 : 0);
                int end = start + segmentSize;
                ranges[i, 0] = start;
                ranges[i, 1] = end;
                start = end;
            }

            return ranges;
        }

        public void RunController()
        {
            int[,] arrSegmentRange = GetSegmentRange();

            for (int i = 0; i < threadNum; i++)
            {
                arrayRangeMinFinder[i] = new RangeMinFinder(
                    arrSegmentRange[i, 0],
                    arrSegmentRange[i, 1],
                    arr);
                arrayRangeMinFinder[i].Start();
            }

            foreach (var finder in arrayRangeMinFinder)
            {
                finder.Join();
            }

            int globalMin = arrayRangeMinFinder[0].MinValue;
            int globalIndex = arrayRangeMinFinder[0].MinIndex;

            for (int i = 1; i < threadNum; i++)
            {
                if (arrayRangeMinFinder[i].MinValue < globalMin)
                {
                    globalMin = arrayRangeMinFinder[i].MinValue;
                    globalIndex = arrayRangeMinFinder[i].MinIndex;
                }
            }
            Console.WriteLine();
            Console.WriteLine($"Мінімальне значення: {globalMin}");
            Console.WriteLine($"Індекс мінімального значення: {globalIndex}");
        }
    }
}

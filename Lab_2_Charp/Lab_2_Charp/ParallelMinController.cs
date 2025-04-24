using System;

namespace Lab_2_Charp
{
    class ParallelMinController
    {
        private readonly int length;
        private readonly int threadNum;
        private readonly int[] arr;
        private readonly RangeMinFinder[] finders;
        private int countFinishedThread = 0;
        private readonly object locker = new object();

        public ParallelMinController(int[] array, int length, int threadNum)
        {
            this.length = length;
            this.threadNum = threadNum;
            this.arr = array;
            this.finders = new RangeMinFinder[threadNum];
        }

        private (int, int)[] GetSegmentRanges()
        {
            var ranges = new (int, int)[threadNum];
            int baseSize = length / threadNum;
            int remainder = length % threadNum;
            int start = 0;

            for (int i = 0; i < threadNum; i++)
            {
                int segmentSize = baseSize + (i < remainder ? 1 : 0);
                int end = start + segmentSize;
                ranges[i] = (start, end);
                start = end;
            }
            return ranges;
        }

        public void RunController()
        {
            var ranges = GetSegmentRanges();
            Thread[] threads = new Thread[threadNum];

            for (int i = 0; i < threadNum; i++)
            {
                var finder = new RangeMinFinder(ranges[i].Item1, ranges[i].Item2, arr, this);
                finders[i] = finder;
                threads[i] = new Thread(finder.Run);
                threads[i].Start();
            }

            lock (locker)
            {
                while (countFinishedThread < threadNum)
                {
                    Monitor.Wait(locker);
                }
            }

            int globalMin = finders[0].MinValue;
            int globalIndex = finders[0].MinIndex;
            for (int i = 1; i < threadNum; i++)
            {
                if (finders[i].MinValue < globalMin)
                {
                    globalMin = finders[i].MinValue;
                    globalIndex = finders[i].MinIndex;
                }
            }

            Console.WriteLine($"Мінімальне значення: {globalMin}");
            Console.WriteLine($"Індекс мінімального значення: {globalIndex}");
        }

        public void IncrementFinishedThread()
        {
            lock (locker)
            {
                countFinishedThread++;
                if (countFinishedThread == threadNum)
                {
                    Monitor.PulseAll(locker);
                }
            }
        }
    }
}

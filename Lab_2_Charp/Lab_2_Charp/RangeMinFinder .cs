using System;
using System.Threading;

namespace Lab_2_Charp
{
    class RangeMinFinder
    {
        private readonly int startIndex;
        private readonly int finishIndex;
        private readonly int[] arr;
        private Thread thread;

        public int MinValue { get; private set; }
        public int MinIndex { get; private set; }

        public RangeMinFinder(int startIndex, int finishIndex, int[] arr)
        {
            this.startIndex = startIndex;
            this.finishIndex = finishIndex;
            this.arr = arr;
        }

        public void Start()
        {
            thread = new Thread(FindMin);
            thread.Start();
        }

        public void Join()
        {
            thread.Join();
        }

        private void FindMin()
        {
            MinValue = arr[startIndex];
            MinIndex = startIndex;

            for (int i = startIndex + 1; i < finishIndex; i++)
            {
                if (arr[i] < MinValue)
                {
                    MinValue = arr[i];
                    MinIndex = i;
                }
            }
        }
    }
}

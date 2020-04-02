using System;

namespace knn
{
    static class KNN
    {
        public static string Classify(double[] unknown, double[][] trainData, int numClasses, int k)
        {
            int n = trainData.Length;
            IndexAndDistance[] info = new IndexAndDistance[n];
            for (int i = 0; i < n; i++)
            {
                IndexAndDistance curr = new IndexAndDistance();
                double dist = Distance(unknown, trainData[i]);
                curr.idx = i;
                curr.dist = dist;
                info[i] = curr;
            }
            Array.Sort(info);
            for (int i = 0; i < k; ++i)
            {
                string c = trainData[info[i].idx][trainData[0].Length - 1] == 1 ? "Iris-virginica" : (trainData[info[i].idx][trainData[0].Length - 2] == 1 ? "Iris - versicolor" : "Iris - setosa");
                double dist = info[i].dist;
                Console.WriteLine($"Nearest: ({trainData[info[i].idx][0]}, {trainData[info[i].idx][1]}, {trainData[info[i].idx][2]}, {trainData[info[i].idx][3]}) Distance: {dist} Class: {c}");
            }
            string result = Vote(info, trainData, numClasses, k);
            return result;
        }
        
        private static double Distance(double[] unknown, double[] data)
        {
            double sum = 0;
            for (int i = 0; i < unknown.Length; i++)
                sum += (unknown[i] - data[i]) * (unknown[i] - data[i]);
            return Math.Sqrt(sum);
        }

        static string Vote(IndexAndDistance[] info, double[][] trainData, int numClasses, int k)
        {
            int[] votes = new int[numClasses];
            for (int i = 0; i < k; i++)
            {
                int idx = info[i].idx;
                int c = trainData[idx][trainData[0].Length - 1] == 1 ? 2 : (trainData[idx][trainData[0].Length - 2] == 1 ? 1 : 0);
                votes[c]++;
            }
            int mostVotes = 0;
            string classWithMostVotes = "";
            for (int i = 0; i < numClasses; i++)
            {
                if (votes[i] > mostVotes)
                {
                    mostVotes = votes[i];
                    classWithMostVotes = i == 0 ? "Iris-setosa" : (i == 1 ? "Iris-versicolor" : "Iris-virginica");
                }
            }
            return classWithMostVotes;
        }

        private class IndexAndDistance : IComparable<IndexAndDistance>
        {
            public int idx;
            public double dist;
            public int CompareTo(IndexAndDistance other)
            {
                if (this.dist < other.dist) return -1;
                else if (this.dist > other.dist) return 1;
                else return 0;
            }
        }
    }
}

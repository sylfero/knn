using System;

namespace knn
{
    class Program
    {
        static void Main(string[] args)
        {
            double[][] trainData = Data.Get("baza.txt");
            trainData.Normalize();
            trainData.Shuffle();
            double[] test = { 6.2, 1.5, 1.2, 0.13 };
            test.Normalize();
            Console.WriteLine($"Predicted class: {KNN.Classify(test, trainData, 3, 1)}");
            Console.ReadKey();
        }
    }
}

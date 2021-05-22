using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Linq;

namespace BenchTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<BenchTest>();
        }
    }

    public class BenchTest
    {
        private BenchTestCheck _benchTest;
        private string word;

        public BenchTest()
        {
            _benchTest = new BenchTestCheck();
            word = RandomString(5);
        }

        private static Random random = new Random();
        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [Benchmark]
        public bool FirstMethod() => _benchTest.BenchTestCheck1(word);

        [Benchmark]
        public bool SecondMethod() => _benchTest.BenchTestCheck2(word);
    }

    public class BenchTestCheck
    {
        public bool BenchTestCheck1(string word)
        {
            for (int i = 0; i < word.Length; i++)
                if (word[i] != word[word.Length - i - 1]) return false;
            return true;
        }
        public bool BenchTestCheck2(string word)
        {
            var reversedWord = new string(word.Reverse().ToArray());
            return reversedWord.Equals(word);
        }
    }
}

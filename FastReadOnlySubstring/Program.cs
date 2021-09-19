using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace FastReadOnlySubstring
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<BenchmarkClass>();
        }
    }
    [MemoryDiagnoser]
    public class BenchmarkClass
    {
        const string countryAndCapital = "Columbia - Washington";

        [Benchmark]
        public string Substring()
        {
            return countryAndCapital.Substring(countryAndCapital.IndexOf("-") + 1);
        }
        [Benchmark]
        public string CharSpan()
        {
            return string.Create(countryAndCapital.Length, countryAndCapital, (span, value) =>
            {
                value.AsSpan().Slice(span.IndexOf('-') + 1).CopyTo(span);
            });
        }
    }
}

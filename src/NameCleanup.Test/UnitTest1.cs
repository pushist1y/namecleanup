using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using CsvHelper;
using Microsoft.Extensions.PlatformAbstractions;
using Xunit;

namespace NameCleanup.Test
{
    public class UnitTest1
    {
        [Fact]
        public void TestEmpty()
        {
            var str1 = "Robijn Wasmiddel Klein & Krachtig Stralend Wit 2 x 1, 47L";
            var str2 = str1.NameCleanup();

            Assert.True(true);
        }

        [Fact]
        public void TestData()
        {
            var inputFilePath = FindInput();
            Assert.NotNull(inputFilePath);
            Assert.True(File.Exists(inputFilePath));
            var outputFile = Path.Combine(Path.GetDirectoryName(inputFilePath), "errors.csv");
            var sb = new StringBuilder();

            using (var reader = File.OpenText(inputFilePath))
            {
                var records = GetRecordsFromCsv(reader);
                var errors = new Dictionary<string, string>();
                foreach (var record in records)
                {
                    string name = (string) Convert.ToString(record.name);
                    string expected = (string) Convert.ToString(record.expected);

                    Assert.NotNull(name);
                    Assert.NotNull(expected);

                    var cleaned = name.NameCleanup();

                    if (cleaned != expected)
                    {
                        errors[name] = expected;
                        sb.AppendLine($@"""{name}"";""{expected}"";""{cleaned}""");
                    }
                }

                File.WriteAllText(outputFile, sb.ToString());
                Assert.Empty(errors);
            }
        }

        private List<dynamic> GetRecordsFromCsv(StreamReader reader)
        {
            var csv = new CsvReader(reader);
            csv.Configuration.BadDataFound = null;
            csv.Configuration.Delimiter = ",";
            return csv.GetRecords<dynamic>().ToList();
        }

        private string FindInput()
        {
            var dllPath = PlatformServices.Default.Application.ApplicationBasePath;
            var dir = new DirectoryInfo(Path.GetDirectoryName(dllPath));
            while (true)
            {
                if ((dir?.EnumerateFiles("input.csv").Any()) ?? false)
                {
                    return Path.Combine(dir.FullName, "input.csv");
                }

                dir = dir?.Parent;
                if (dir == null)
                {
                    return null;
                }
            }
        }
    }
}

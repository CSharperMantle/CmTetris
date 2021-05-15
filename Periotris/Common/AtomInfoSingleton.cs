using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace Periotris.Common
{
    public sealed class AtomInfoSingleton
    {
        private readonly JObject periodicTableRoot;

        private AtomInfoSingleton()
        {
            var uri = new Uri(TetrisConst.PeriodicTableJsonFileName); ;

            using (var resourceStream = Application.GetResourceStream(uri).Stream)
            using (var reader = new StreamReader(resourceStream))
            {
                string content = reader.ReadToEnd();
                periodicTableRoot = JObject.Parse(content);
            }
        }

        private static readonly Lazy<AtomInfoSingleton> instance
            = new Lazy<AtomInfoSingleton>(() => new AtomInfoSingleton());

        public static AtomInfoSingleton Instance
        {
            get
            {
                return instance.Value;
            }
        }

        public string GetElementSymbolByAtomicNumber(int atomicNumber)
        {
            return (
                from element in periodicTableRoot["elements"]
                where (int)element["number"] == atomicNumber
                select (string)element["symbol"]
                ).First();
        }
    }
}

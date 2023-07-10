using System;
using Newtonsoft.Json;

namespace LINQInManhattan
{
    class Program
    {
        static void Main()
        {
            string jsonPath = "../../../data.json";
            string jsonData = File.ReadAllText(jsonPath);
            var rootObject = JsonConvert.DeserializeObject<RootObject>(jsonData);  //deserialized json data(new var)
            var neighborhoods = rootObject.features.Select(item => item.properties.neighborhood);

            //////////////////////////////////////////////////////////////////////////////////////////////////////////

            Console.WriteLine("neighborhoods: ");
            Console.WriteLine(string.Join("- ", neighborhoods));
            Console.WriteLine("Total: " + neighborhoods.Count() + " neighborhoods");

            //////////////////////////////////////////////////////////////////////////////////////////////////////////

            var namedNeighborhoods = neighborhoods.Where(n => !string.IsNullOrEmpty(n));
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("neighborhoods that have a name: ");
            Console.WriteLine(string.Join("- ", namedNeighborhoods));
            Console.WriteLine("Total: " + namedNeighborhoods.Count() + " neighborhoods");

            //////////////////////////////////////////////////////////////////////////////////////////////////////////

            var distinctNeighborhoods = namedNeighborhoods.Distinct();
            Console.WriteLine();
            Console.WriteLine("Neighborhoods without repetition:");
            Console.WriteLine(string.Join("- ", distinctNeighborhoods));
            Console.WriteLine("Total: " + distinctNeighborhoods.Count() + " neighborhoods");

            //////////////////////////////////////////////////////////////////////////////////////////////////////////

            var consolidatedNeighborhoods = rootObject.features
                .Select(f => f.properties.neighborhood)
                .Where(n => !string.IsNullOrEmpty(n))
                .Distinct();
            Console.WriteLine();
            Console.WriteLine("Consolidated Neighborhoods using a single query: ");
            Console.WriteLine(string.Join("- ", consolidatedNeighborhoods));
            Console.WriteLine("Total: " + consolidatedNeighborhoods.Count() + " neighborhoods");

            var consolidatedSingleQuery = (from i in rootObject.features
                                                        let n = i.properties.neighborhood
                                                        where !string.IsNullOrEmpty(n)
                                                        select n).Distinct();
            Console.WriteLine();
            Console.WriteLine("Consolidated neighborhoods using LINQ query syntax:");
            Console.WriteLine(string.Join("- ", consolidatedSingleQuery));

            Console.WriteLine("Total: " + consolidatedSingleQuery.Count() + " neighborhoods");
        }
    }

    class RootObject
    {
        public string type { get; set; }
        public List<Feature> features { get; set; }
    }

    class Properties
    {
        public string zip { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string address { get; set; }
        public string borough { get; set; }
        public string neighborhood { get; set; }
        public string county { get; set; }
    }
    class Geometry
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }
    class Feature
    {
        public string type { get; set; }
        public Geometry geometry { get; set; }
        public Properties properties { get; set; }
    }
}

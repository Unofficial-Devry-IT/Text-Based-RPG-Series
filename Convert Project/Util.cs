using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
namespace Convert_Project
{
    public static class Util
    {
        public static List<T> LoadFromFile<T>()
        {
            string filename = $"{typeof(T).Name}s.json";

            return JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(filename));
        }

        public static void SaveFile<T>(IEnumerable<T> collection)
        {
            string filename = $"{typeof(T).Name}s.json";

            File.WriteAllText(filename, JsonConvert.SerializeObject(collection, Formatting.Indented));
        }
        
        public static T GetUserInput<T>(string message, string errorMessage = null)
        {
            do
            {
                Console.WriteLine(message);
                string input = Console.ReadLine();
                
                try
                {
                    return (T) Convert.ChangeType(input, typeof(T));
                }
                catch
                {
                    Console.WriteLine(errorMessage ?? $"Expected a value of type '{typeof(T).Name}'");
                }
            } while (true);
        }

        public static T GetUserInput<T>(string message, Predicate<T> criteria, string errorMessage = null)
        {
            T value = GetUserInput<T>(message, errorMessage);

            while (!criteria(value))
            {
                if(errorMessage != null)
                    Console.WriteLine(errorMessage);
                
                value = GetUserInput<T>(message, errorMessage);
            }

            return value;
        }

        public static T GetUserInput<T>(string message, T min, T max, string errorMessage = null)
            where T : IComparable, IConvertible, IFormattable
        {
            T value = GetUserInput<T>(message, errorMessage);

            while (Comparer.Default.Compare(value, min) < 0 || Comparer.Default.Compare(value, max) > 0)
            {
                Console.WriteLine($"Invalid input. You chose: '{value}'. Must be in range of {min} - {max}");
                return GetUserInput(message, min, max, errorMessage);
            }
            
            return value;
        }
    }
}
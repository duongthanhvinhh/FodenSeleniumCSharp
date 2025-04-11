using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace FodenApp.utilities
{
    public class JsonReader
    {
        private string _filePath;

        public JsonReader(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
            }

            _filePath = filePath;
        }

        public JObject ReadJson()
        {
            if (!File.Exists(_filePath))
            {
                throw new FileNotFoundException($"The file at {_filePath} does not exist.");
            }

            string jsonContent = File.ReadAllText(_filePath);
            return JObject.Parse(jsonContent);
        }

        public T GetValue<T>(string key)
        {
            JObject jsonObject = ReadJson();
            JToken? token = jsonObject.SelectToken(key);

            if (token == null)
            {
                throw new ArgumentException($"Key '{key}' not found in the JSON file.");
            }

            if (token == null)
            {
                throw new ArgumentException($"Key '{key}' not found in the JSON file.");
            }

            return token.ToObject<T>();
        }

        /*How to use:
        JsonReader reader = new JsonReader("path/to/json/file.json");
        string name = reader.GetValue<string>("person.name"); // Returns "John"
        int age = reader.GetValue<int>("person.age"); // Returns 30
        */
    }
}
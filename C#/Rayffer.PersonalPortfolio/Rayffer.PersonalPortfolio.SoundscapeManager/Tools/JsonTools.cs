using System.IO;
using Newtonsoft.Json;


namespace Rayffer.PersonalPortfolio.SoundscapeManager.Tools
{
    public static class JsonTools
    {
        public static void WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite, Formatting.Indented);
                writer = new StreamWriter(filePath, append);
                writer.Write(contentsToWriteToFile);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        public static T ReadFromJsonFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                reader = new StreamReader(filePath);
                var fileContents = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(fileContents, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore  });
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

    }
}

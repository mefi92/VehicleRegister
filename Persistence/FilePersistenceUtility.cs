using Newtonsoft.Json;
using Persistence.Exceptions;

namespace Persistence
{  
    public static class FilePersistenceUtility
    {
        public static void SaveObjectToTextFile<T>(string filePath, T inputObject)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
            }

            try
            {
                string jsonData = JsonConvert.SerializeObject(inputObject);
                File.WriteAllText(filePath, jsonData);
            }
            catch (Exception ex)
            {                 
                throw new FilePersistenceException("Error saving object to file.", ex);
            }
        }

        public static T LoadJsonDataFromFile<T>(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
            }

            try
            {
                string jsonData = File.ReadAllText(filePath);
                T result = JsonConvert.DeserializeObject<T>(jsonData);
                return result;
            }
            catch (Exception ex)
            {                
                throw new FilePersistenceException("Error loading object from file.", ex);
            }
        }
    }
}

using Microsoft.Extensions.Localization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace RealEstate.PL.Services.Localization
{
    public class JsonStringLocalization : IStringLocalizer
    {
        public LocalizedString this[string name]
        {
            get
            {
                var value = GetString(name);
                return new LocalizedString(name, value);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var acttualValue = this[name];
                return !acttualValue.ResourceNotFound ? new LocalizedString(name, string.Format(acttualValue, arguments)) : acttualValue;
            }
        }
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new NotImplementedException();
        }
        public string GetString(string key)
        {
            try
            {
                var filePath = $"/Resourse/{Thread.CurrentThread.CurrentCulture.Name}.json";
                var fullFilePath = Path.GetFullPath(filePath);

                if (File.Exists(fullFilePath))
                {
                    return GetValueFromJson(key, fullFilePath);
                }
                else
                {
                    throw new FileNotFoundException($"JSON file '{fullFilePath}' not found.");
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return string.Empty;
            }
            catch (JsonReaderException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return string.Empty;
            }
        }
        private string GetValueFromJson(string property, string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"JSON file '{filePath}' not found.");
                }

                string jsonText = File.ReadAllText(filePath);

                JObject json = JObject.Parse(jsonText);

                if (json.TryGetValue(property, StringComparison.OrdinalIgnoreCase, out JToken valueToken))
                {
                    return valueToken.ToString();
                }
                else
                {
                    throw new ArgumentException($"Property '{property}' not found in JSON file.");
                }
            }
            catch (JsonReaderException ex)
            {
                throw new JsonReaderException($"Invalid JSON format in file '{filePath}': {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading JSON file '{filePath}': {ex.Message}");
            }
        }

    }
}

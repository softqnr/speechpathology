using Plugin.Multilingual;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace SpeechPathology.Utils
{
    public class ResourceHelper
    {
        const string ResourceId = "SpeechPathology.Resources.AppResources";

        static readonly Lazy<ResourceManager> resourceManager = new Lazy<ResourceManager>(() => new ResourceManager(ResourceId,
            typeof(ResourceHelper).GetTypeInfo().Assembly));

        public static string GetResourceNameByValue(string value)
        {
            var entry = resourceManager.Value.GetResourceSet(CrossMultilingual.Current.CurrentCultureInfo, true, true)
                  .OfType<DictionaryEntry>()
                  .FirstOrDefault(e => e.Value.ToString() == value);

            var key = entry.Key.ToString();
            return key;
        }

        public static string[] TranslateArray(string[] array)
        {
            var ci = CrossMultilingual.Current.CurrentCultureInfo;
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = resourceManager.Value.GetString(array[i], ci);
            }
            return array;
        }
    }
}

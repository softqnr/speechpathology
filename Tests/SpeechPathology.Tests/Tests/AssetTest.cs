using SpeechPathology;
using SpeechPathology.DataServices.Flashcard;
using System.Reflection;
using Xamarin.Forms;
using Xunit;

namespace SpeechPathology.Tests
{
    public class AssetTest
    {
        private readonly FlashcardService _flashcardService;
        public AssetTest()
        {
            //_flashcardService = new FlashcardService();
        }
        [Fact]
        public void CheckImagesExistInResources()
        {
            // Display names of embedded resources
            //var assembly = typeof(App).GetTypeInfo().Assembly;
            //foreach (var res in assembly.GetManifestResourceNames())
            //{
            //    System.Diagnostics.Debug.WriteLine(">>> " + res);
            //}
            var img = ImageSource.FromResource("SpeechPathology.Assets.Images.lemon.jpg");
            Assert.NotNull(img);
        }
    }
}

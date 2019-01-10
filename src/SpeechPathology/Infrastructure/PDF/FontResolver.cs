using PdfSharpCore.Fonts;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SpeechPathology.Infrastructure.PDF
{
    internal class FontResolver : IFontResolver
    {
        public string DefaultFontName
        {
            get { return "OpenSans"; }
        }

        public static readonly string[] FontFiles = new string[]
            {
                "OpenSans-Regular.ttf",
                "OpenSans-Bold.ttf",
                "OpenSans-Italic.ttf",
                "OpenSans-BoldItalic.ttf",
            };

        public byte[] GetFont(string faceName)
        {
            if (FontFiles.Contains(faceName)) {
                var assembly = Assembly.GetExecutingAssembly();
                Stream stream = assembly.GetManifestResourceStream($"SpeechPathology.Assets.Fonts.{faceName}");
                using (var reader = new StreamReader(stream))
                {
                    var bytes = default(byte[]);
                    using (var memstream = new MemoryStream())
                    {
                        reader.BaseStream.CopyTo(memstream);
                        bytes = memstream.ToArray();
                    }
                    return bytes;
                }
            }
            throw new ArgumentException($"Invalid typeface name '{faceName}'");
        }

        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            familyName = familyName.Replace(" ", "");
            string fontName = string.Empty;
            if (familyName == DefaultFontName)
            {
                fontName = FontFiles[Convert.ToInt32(isBold) + 2 * Convert.ToInt32(isItalic)];
            }
            //else
            //{
            //    fontName = _fontProvider.ProvideFont(familyName, isBold, isItalic);
            //}
            return new FontResolverInfo(fontName);
        }
    }
}
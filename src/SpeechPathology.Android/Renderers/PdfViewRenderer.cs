using System;
using System.ComponentModel;
using System.Net;
using Android.Content;
using Android.Print;
using Android.Webkit;
using SpeechPathology.Controls;
using SpeechPathology.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using WebView = Xamarin.Forms.WebView;

[assembly: ExportRenderer(typeof(PdfView), typeof(PdfViewRenderer))]
namespace SpeechPathology.Droid
{
    public class PdfViewRenderer : WebViewRenderer
    {
        Context _context;
        public PdfViewRenderer(Context context) : base(context)
        {
            _context = context;
        }
        internal class PdfWebChromeClient : WebChromeClient
        {
            Context _context;
            public PdfWebChromeClient(Context context)
            {
                _context = context;
            }
            public override bool OnJsAlert(Android.Webkit.WebView view, string url, string message, JsResult result)
            {
                if (message != "PdfViewer_app_scheme:print")
                {
                    return base.OnJsAlert(view, url, message, result);
                }

                using (var printManager = _context.GetSystemService(Android.Content.Context.PrintService) as PrintManager)
                {
                    printManager?.Print(FileName, new FilePrintDocumentAdapter(_context, FileName, Uri), null);
                }

                return true;
            }

            public string Uri { private get; set; }

            public string FileName { private get; set; }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
            {
                return;
            }

            var pdfView = Element as PdfView;

            if (pdfView == null)
            {
                return;
            }
           
            if (string.IsNullOrWhiteSpace(pdfView.Uri) == false)
            {
                Control.SetWebChromeClient(new PdfWebChromeClient(_context)
                {
                    Uri = pdfView.Uri,
                    FileName = GetFileNameFromUri(pdfView.Uri)
                });
            }
            // WebChromeClient has JS enabled by default
            Control.Settings.JavaScriptEnabled = true;
            Control.Settings.AllowFileAccess = true;
            Control.Settings.AllowUniversalAccessFromFileURLs = true;

            LoadFile(pdfView.Uri);
        }

        private static string GetFileNameFromUri(string uri)
        {
            var lastIndexOf = uri?.LastIndexOf("/", StringComparison.InvariantCultureIgnoreCase);
            return lastIndexOf > 0 ? uri.Substring(lastIndexOf.Value, uri.Length - lastIndexOf.Value) : string.Empty;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName != PdfView.UriProperty.PropertyName)
            {
                return;
            }

            var pdfView = Element as PdfView;

            if (pdfView == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(pdfView.Uri) == false)
            {
                Control.SetWebChromeClient(new PdfWebChromeClient(_context)
                {
                    Uri = pdfView.Uri,
                    FileName = GetFileNameFromUri(pdfView.Uri)
                });
            }

            LoadFile(pdfView.Uri);
        }

        private void LoadFile(string uri)
        {
            if (string.IsNullOrWhiteSpace(uri))
            {
                return;
            }
            Control.LoadUrl(string.Format("file:///android_asset/pdfjs/web/viewer.html?file={0}",
                WebUtility.UrlEncode("file:///android_asset/" + uri)));
        }
    }
}
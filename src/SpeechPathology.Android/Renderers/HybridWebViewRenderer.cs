using Android.Content;
using SpeechPathology.Controls;
using SpeechPathology.Droid.Renderers;
using System.ComponentModel;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]
namespace SpeechPathology.Droid.Renderers
{
    public class HybridWebViewRenderer : ViewRenderer<HybridWebView, Android.Webkit.WebView>
    {
        const string JavascriptFunction = "function invokeCSharpAction(data){jsBridge.invokeAction(data);}";
        Context _context;

        public HybridWebViewRenderer(Context context) : base(context)
        {
            _context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<HybridWebView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                var webView = new Android.Webkit.WebView(_context);
                webView.Settings.JavaScriptEnabled = true;
                webView.SetWebViewClient(new JavascriptWebViewClient($"javascript: {JavascriptFunction}"));
                SetNativeControl(webView);
            }
            if (e.OldElement != null)
            {
                Control.RemoveJavascriptInterface("jsBridge");
                var hybridWebView = e.OldElement as HybridWebView;
                hybridWebView.Cleanup();
            }
            if (e.NewElement != null)
            {
                ////Control.AddJavascriptInterface(new JSBridge(this), "jsBridge");
                // Control.Settings.AllowUniversalAccessFromFileURLs = true;
                ////Control.LoadUrl($"file:///android_asset/Content/{Element.Uri}");
                //Control.LoadUrl(string.Format("file:///android_asset/pdfjs/web/viewer.js?file={0}", 
                //    string.Format("file:///android_asset/Content/{0}", WebUtility.UrlEncode(Element.Uri))));
            }
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == nameof(HybridWebView.Uri))
            {
                //Control.AddJavascriptInterface(new JSBridge(this), "jsBridge");
                Control.Settings.AllowUniversalAccessFromFileURLs = true;
                //Control.LoadUrl($"file:///android_asset/Content/{Element.Uri}");
                Control.LoadUrl(string.Format("file:///android_asset/pdfjs/web/viewer.html?file={0}",
                    string.Format("file:///android_asset/Content/{0}", WebUtility.UrlEncode(Element.Uri))));
            }
        }
    }
}
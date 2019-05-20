using SpeechPathology.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(SpeechPathology.Droid.AppVersionProvider))]
namespace SpeechPathology.Droid
{
    public class AppVersionProvider : IAppVersionProvider
    {
        public string AppVersion
        {
            get
            {
                var context = Android.App.Application.Context;
                var info = context.PackageManager.GetPackageInfo(context.PackageName, 0);

                return $"{info.VersionName}";

                //return $"{info.VersionName}.{info.VersionCode.ToString()}";
            }
        }
    }
}
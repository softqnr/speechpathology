using SpeechPathology.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(SpeechPathology.Droid.Services.AppVersionProvider))]
namespace SpeechPathology.Droid.Services
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
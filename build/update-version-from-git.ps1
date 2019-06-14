Write-Host "Updating AndroidManifest version information"
$ManifestPath = (resolve-path .\..\..\Properties\AndroidManifest.xml)
[xml] $xdoc = Get-Content $ManifestPath
$versionName = (git describe --tags).ToString()
$tag = (git describe --abbrev=0).ToString()
$commitCount = (git rev-list --count HEAD).ToString()
$versionCode = $commitCount
$xdoc.manifest.versionCode = $versionCode
$xdoc.manifest.versionName = $versionName
$xdoc.Save($ManifestPath)
Write-Host "VersionCode set to $versionCode"
Write-Host "VersionName set to $versionName"
param ([string] $ProjectDir, [string] $Language)
Write-Host "ProjectDir: $ProjectDir"
Write-Host "Language: $Language"

$ManifestPath = $ProjectDir + "Properties\AndroidManifest.xml"

Write-Host "ManifestPath: $ManifestPath"

[xml] $xdoc = Get-Content $ManifestPath

$package = "com.softqnr.SpeechPathology" + $Language.ToUpper()

If ($package -ne $xdoc.manifest.package) 
{
    $xdoc.manifest.package = $package
    $xdoc.Save($ManifestPath)
    Write-Host "AndroidManifest.xml package name updated to $package"
}else{
    Write-Host "AndroidManifest.xml package name not changed"
}
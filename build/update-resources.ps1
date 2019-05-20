param ([string] $ProjectDir, [string] $Language, [string] $LanguageList)
Write-Host "ProjectDir: $ProjectDir"
Write-Host "Language: $Language"
Write-Host "Languages: $LanguageList"

$Languages = $LanguageList.Split("|")

$ProjectFile = $ProjectDir + "SpeechPathology.Android.csproj"

Write-Host "Project file: $ProjectFile"

[xml] $xmlDoc = Get-Content $ProjectFile

$ns = New-Object System.Xml.XmlNamespaceManager($xmlDoc.NameTable)
$ns.AddNamespace("ns", $xmlDoc.DocumentElement.NamespaceURI)

$nodelist = $xmlDoc.ChildNodes.SelectNodes("//ns:AndroidAsset|//ns:AndroidResource|//ns:None", $ns)

Write-Host "$($nodelist.count) found"
$ProjectFileChanged = $false
$RegexPatterns = @("^Resources\\drawable\\(?<lang>({0}))_(?<filename>.+)$" -f ($Languages -join '|')
    , "^Assets\\Sounds\\(?<lang>({0}))\\(?<filename>.+)$" -f ($Languages -join '|')
    , "^Assets\\Worksheets\\(?<lang>({0}))\\(?<filename>.+)$" -f ($Languages -join '|')
    , "^Assets\\LanguageSkills\\(?<lang>({0}))\\(?<filename>.+)$" -f ($Languages -join '|')
)
foreach ($RegexPattern in $RegexPatterns){
    Write-Host "Testing $RegexPattern pattern" 
    foreach ($node in $nodelist) {
	    if ($node.Include){
		    if ($node.Include -match $RegexPattern){
			    $nodeName = $node.Name
			    if ($Matches.lang -eq $Language){
                    # Activate
				    if ($nodeName -eq "None"){
                        # Detect element type name
                        $ElementTypeName = "AndroidResource"
                        if ($node.Include.StartsWith("Assets")){
                            $ElementTypeName = "AndroidAsset"
                        }
					    Write-Host "$nodeName > $($node.Include) activating $ElementTypeName"
					    $ProjectFileChanged = $true
					    # Create None node
					    $noneNode = $xmlDoc.CreateElement($ElementTypeName, $xmlDoc.DocumentElement.NamespaceURI)
					    $noneNode.SetAttribute("Include", $node.Include)
					    # Replace node
					    $parent = $node.ParentNode
					    [void] $parent.RemoveChild($node)
					    [void] $parent.AppendChild($noneNode)
				    }else{
					    Write-Host "$nodeName > $($node.Include) OK."
				    }
			    }elseif ($Matches.lang -in $Languages){
                    # Deactivate
				    if ($nodeName -eq "AndroidResource" -or $nodeName -eq "AndroidAsset"){
					    Write-Host "$nodeName > $($node.Include) is from $($Matches.lang) deactivating"
					    $ProjectFileChanged = $true
					    # Create None node
					    $noneNode = $xmlDoc.CreateElement('None', $xmlDoc.DocumentElement.NamespaceURI)
					    $noneNode.SetAttribute("Include", $node.Include)
					    # Replace node
					    $parent = $node.ParentNode
					    [void] $parent.RemoveChild($node)
					    [void] $parent.AppendChild($noneNode)
				    }
			    }
		    }
	    }
    }
}
if ($ProjectFileChanged){
	Copy-Item $ProjectFile -Destination "$ProjectFile.backup"

	$xmlDoc.Save($ProjectFile)

	$ResourceFile = $ProjectDir + "Resources\Resource.designer.cs"

	Copy-Item $ResourceFile -Destination "$ResourceFile.backup"
    # Force rebuild android resource file by clearing its contents
	Clear-Content $ResourceFile
    
    # Stop build process
    # This will ensure that user will rebuild the project since you cannot modify
    # in pre-build event because project file is allready loaded
    exit 1
}
param ([string] $DatabasePath, [string] $Language)
Write-Host "ProjectDir: $ProjectDir"
Write-Host "Language: $Language"

Write-Host "DBPath: $DatabasePath"
Write-Host "PSScriptRoot: $PSScriptRoot"
# Note: Permissions needed
try{
	#Add-Type -Path "$PSScriptRoot\System.Data.SQLite.dll"
	$assembly = [Reflection.Assembly]::LoadFile("$PSScriptRoot\System.Data.SQLite.dll")
}
catch [System.Reflection.ReflectionTypeLoadException]
{
   Write-Host "Message: $($_.Exception.Message)"
   Write-Host "StackTrace: $($_.Exception.StackTrace)"
   Write-Host "LoaderExceptions: $($_.Exception.LoaderExceptions)"
}

$con = New-Object -TypeName System.Data.SQLite.SQLiteConnection
$con.ConnectionString = "Data Source=$DatabasePath" 
# Open the connection
$con.Open()
# Create command
$sql = $con.CreateCommand()
$sql.CommandText = "UPDATE Settings SET value='$($Language.ToUpper())' WHERE name='language';"
# Execute update SQL 
$success = $sql.ExecuteNonQuery()

If ($success -eq "1")
{
	Write-Host "Language updated to $($Language.ToUpper())"
}
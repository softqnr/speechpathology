param ([string] $ProjectDir, [string] $Language)
Write-Host "ProjectDir: $ProjectDir"
Write-Host "Language: $Language"

$DatabasePath = $ProjectDir + "Assets\sp.db"

Write-Host "DBPath: $DatabasePath"

# Note: Permissions needed
Add-Type -Path "$PSScriptRoot\System.Data.SQLite.dll"
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
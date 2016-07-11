param($installPath, $toolsPath, $package, $project)

$appPluginsFolder = $project.ProjectItems | Where-Object { $_.Name -eq "App_Plugins" }
$nugetPackageDashboardFolder = $appPluginsFolder.ProjectItems | Where-Object { $_.Name -eq "NugetPackageDashboard" }

if (!$nugetPackageDashboardFolder)
{
	$newPackageFiles = "$installPath\Content\App_Plugins\NugetPackageDashboard"

	$projFile = Get-Item ($project.FullName)
	$projDirectory = $projFile.DirectoryName
	$projectPath = Join-Path $projDirectory -ChildPath "App_Plugins"
	$projectPathExists = Test-Path $projectPath

	if ($projectPathExists) {	
		Write-Host "Updating Nuget Package Dashboard App_Plugin files using PS as they have been excluded from the project"
		Copy-Item $newPackageFiles $projectPath -Recurse -Force
	}
}
param($installPath, $toolsPath, $package, $project)

Import-Module (Join-Path $toolsPath Utils.psm1)

$relativeInstallPath = Get-RelativePath ([System.Io.Path]::GetDirectoryName($dte.Solution.FullName)) $installPath

Add-ToolFolder (Join-Path (Join-Path $relativeInstallPath "lib")  "NET4")
Add-ToolFolder (Join-Path $relativeInstallPath "DesignTime")

$project.DTE.ItemOperations.Navigate('http://entlib.codeplex.com/wikipage?title=TopazTplSep2012UpdateReleaseNotes')
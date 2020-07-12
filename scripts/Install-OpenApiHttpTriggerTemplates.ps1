Param(
    [string] [Parameter(Mandatory=$true)] $ProjectPath,
    [string] [Parameter(Mandatory=$true)] $Namespace,
    [switch] [Parameter(Mandatory=$false)] $IsVersion1
)

# Forces TLS 1.2 for GitHub download.
[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12

# Downloads contents
$interface = (Invoke-WebRequest `
    -Uri "https://raw.githubusercontent.com/aliencube/AzureFunctions.Extensions/dev/templates/OpenApiTriggers/IOpenApiHttpTriggerContext.cs" `
    -UseBasicParsing).Content

$context = (Invoke-WebRequest `
    -Uri "https://raw.githubusercontent.com/aliencube/AzureFunctions.Extensions/dev/templates/OpenApiTriggers/OpenApiHttpTriggerContext.cs" `
    -UseBasicParsing).Content

$triggerUri = "https://raw.githubusercontent.com/aliencube/AzureFunctions.Extensions/dev/templates/OpenApiTriggers/OpenApiHttpTrigger.cs"
if ($IsVersion1 -eq $true)
{
    $triggerUri = "https://raw.githubusercontent.com/aliencube/AzureFunctions.Extensions/dev/templates/OpenApiTriggers/OpenApiHttpTriggerV1.cs"
}

$trigger = (Invoke-WebRequest `
    -Uri $triggerUri `
    -UseBasicParsing).Content

# Replaces namespace
$interface = $interface.Replace("<# NAMESPACE #>", $Namespace)
$context = $context.Replace("<# NAMESPACE #>", $Namespace)
$trigger = $trigger.Replace("<# NAMESPACE #>", $Namespace)

# Save templates
if (!(Test-Path "$ProjectPath/OpenApi" -PathType Container)) {
    $result = New-Item -ItemType Directory -Force -Path "$ProjectPath/OpenApi"
}

$interface | Out-File "$ProjectPath/OpenApi/IOpenApiHttpTriggerContext.cs"
$context | Out-File "$ProjectPath/OpenApi/OpenApiHttpTriggerContext.cs"
$trigger | Out-File "$ProjectPath/OpenApi/OpenApiHttpTrigger.cs"

Remove-Variable result
Remove-Variable interface
Remove-Variable context
Remove-Variable trigger
Remove-Variable triggerUri

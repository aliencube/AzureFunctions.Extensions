Param(
    [string] [Parameter(Mandatory=$true)] $ProjectPath,
    [string] [Parameter(Mandatory=$true)] $Namespace
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

$trigger = (Invoke-WebRequest `
    -Uri "https://raw.githubusercontent.com/aliencube/AzureFunctions.Extensions/feature/openapi-templates/templates/OpenApiTriggers/OpenApiHttpTrigger.cs" `
    -UseBasicParsing).Content

# Replaces namespace
$interface = $interface.Replace("<# NAMESPACE #>", $Namespace)
$context = $context.Replace("<# NAMESPACE #>", $Namespace)
$trigger = $trigger.Replace("<# NAMESPACE #>", $Namespace)

# Save templates
$interface | Out-File "$ProjectPath/IOpenApiHttpTriggerContext.cs"
$context | Out-File "$ProjectPath/OpenApiHttpTriggerContext.cs"
$trigger | Out-File "$ProjectPath/OpenApiHttpTrigger.cs"

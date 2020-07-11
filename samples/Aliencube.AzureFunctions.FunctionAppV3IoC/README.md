# Aliencube.AzureFunctions.FunctionAppV3IoC #

## Getting Started ##

### Open API Integration ###

Run the following script to enable Open API integration:

```powershell
# PowerShell
../../scripts/Download-OpenApiHttpTriggerTemplates.ps1 -ProjectPath ./ -Namespace Aliencube.AzureFunctions.FunctionAppV3Static
```

```bash
# Bash script
../../scripts/Download-OpenApiHttpTriggerTemplates.sh -ProjectPath ./ -Namespace Aliencube.AzureFunctions.FunctionAppV3Static
```

### Run Function App ###

Run the Function app locally.

```bash
func start
```

Open a web browser and visit the page.

```txt
http://localhost:7071/api/swagger/ui
```

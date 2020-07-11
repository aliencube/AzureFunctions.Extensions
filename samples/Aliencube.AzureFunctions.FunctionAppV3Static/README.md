# Aliencube.AzureFunctions.FunctionAppV3Static #

## Getting Started ##

### Install Open API Templates ###

Run the following script to install the Open API templates:

```powershell
# PowerShell
../../scripts/Download-OpenApiHttpTriggerTemplates.ps1 -ProjectPath ./ -Namespace Aliencube.AzureFunctions.FunctionAppV3Static
```

```bash
# Bash script
../../scripts/Download-OpenApiHttpTriggerTemplates.sh . Aliencube.AzureFunctions.FunctionAppV3Static
```

> You may need to run `chmod +x ../../scripts/Download-OpenApiHttpTriggerTemplates.sh` command.


### Run Function App ###

Run the Function app locally.

```bash
func start
```

Open a web browser and visit the page.

```txt
http://localhost:7071/api/swagger/ui
```

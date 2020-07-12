# Aliencube.AzureFunctions.FunctionAppV1Static #

## Getting Started ##

### Install Open API Templates ###

Run the following script to install the Open API templates:

```powershell
# PowerShell
../../scripts/Install-OpenApiHttpTriggerTemplates.ps1 `
    -ProjectPath ./ `
    -Namespace Aliencube.AzureFunctions.FunctionAppV3Static `
    -IsVersion1
```


### Run Function App ###

Run the Function app locally.

```powershell
func start
```

Open a web browser and visit the page.

```txt
http://localhost:7071/api/swagger/ui
```

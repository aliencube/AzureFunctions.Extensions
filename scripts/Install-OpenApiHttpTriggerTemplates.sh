#!/bin/bash

path=$1
namespace=$2

# Checks if directory exists
[ ! -d "$path/OpenApi" ] && mkdir -p "$path/OpenApi"

# Save templates
curl -X GET "https://raw.githubusercontent.com/aliencube/AzureFunctions.Extensions/dev/templates/OpenApiTriggers/IOpenApiHttpTriggerContext.cs" > "$path/OpenApi/IOpenApiHttpTriggerContext.cs"
curl -X GET "https://raw.githubusercontent.com/aliencube/AzureFunctions.Extensions/dev/templates/OpenApiTriggers/OpenApiHttpTriggerContext.cs" > "$path/OpenApi/OpenApiHttpTriggerContext.cs"
curl -X GET "https://raw.githubusercontent.com/aliencube/AzureFunctions.Extensions/dev/templates/OpenApiTriggers/OpenApiHttpTrigger.cs" > "$path/OpenApi/OpenApiHttpTrigger.cs"

# Replaces namespace
sed -i '' "s!\<\# NAMESPACE \#\>!$namespace!g" "$path/OpenApi/IOpenApiHttpTriggerContext.cs"
sed -i '' "s!\<\# NAMESPACE \#\>!$namespace!g" "$path/OpenApi/OpenApiHttpTriggerContext.cs"
sed -i '' "s!\<\# NAMESPACE \#\>!$namespace!g" "$path/OpenApi/OpenApiHttpTrigger.cs"

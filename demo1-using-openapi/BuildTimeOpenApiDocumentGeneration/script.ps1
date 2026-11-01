$ErrorActionPreference = "Stop"

# Check if jq is installed
winget list -q jqlang.jq | out-null

if ($? -eq $false) {
    winget install -e --id jqlang.jq
}

dotnet build

cat openapi-spec.json | jq

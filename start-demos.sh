#!/usr/bin/env bash
# Starts every app the demo tours run live. Press Ctrl+C to stop them all.
#
# Usage: ./start-demos.sh [--all]
#   --all  also start MinimalApi (5302) and AheadOfTime (5303), which the tours only show as code
set -euo pipefail

cd "$(dirname "$0")"

projects=(
  "demo1-using-openapi/OpenApiInDotnet8:5300:swagger"
  "demo1-using-openapi/Controllers:5301:scalar"
  "demo2-customizing-and-whats-new-in-dotnet11/CustomizingOpenApi:5305:scalar"
  "demo2-customizing-and-whats-new-in-dotnet11/WhatsNewInDotnet11:5306:scalar"
)

if [[ "${1:-}" == "--all" ]]; then
  projects+=(
    "demo1-using-openapi/MinimalApi:5302:scalar"
    "demo1-using-openapi/AheadOfTime:5303:scalar"
  )
fi

echo "Building..."
dotnet build OpenApiInDotnet.slnx --nologo -v quiet

trap 'echo; echo "Stopping demos..."; kill $(jobs -p) 2>/dev/null || true; wait 2>/dev/null || true' EXIT INT TERM

for entry in "${projects[@]}"; do
  IFS=: read -r project port ui <<< "$entry"
  dotnet run --no-build --launch-profile http --project "$project" > >(sed "s|^|[$(basename "$project")] |") 2>&1 &
done

echo
echo "Demos running:"
for entry in "${projects[@]}"; do
  IFS=: read -r project port ui <<< "$entry"
  printf "  %-22s http://localhost:%s/%s\n" "$(basename "$project")" "$port" "$ui"
done
echo
echo "Press Ctrl+C to stop."

wait

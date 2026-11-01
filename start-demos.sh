#!/usr/bin/env bash
# Starts every app the demo tours run live and opens the demo pages in the browser,
# in the order the tours use them. Press Ctrl+C to stop the apps.
#
# Usage: ./start-demos.sh [--all] [--no-browser]
#   --all         also start MinimalApi (5302) and AheadOfTime (5303), which the tours only show as code
#   --no-browser  only start the apps
set -euo pipefail

cd "$(dirname "$0")"

start_all=false
open_browser=true
for arg in "$@"; do
  case "$arg" in
    --all) start_all=true ;;
    --no-browser) open_browser=false ;;
    *) echo "Unknown option: $arg" >&2; exit 1 ;;
  esac
done

# project:port
projects=(
  "demo1-using-openapi/OpenApiInDotnet8:5300"
  "demo1-using-openapi/Controllers:5301"
  "demo2-customizing-and-whats-new-in-dotnet11/CustomizingOpenApi:5305"
  "demo2-customizing-and-whats-new-in-dotnet11/WhatsNewInDotnet11:5306"
)

if $start_all; then
  projects+=(
    "demo1-using-openapi/MinimalApi:5302"
    "demo1-using-openapi/AheadOfTime:5303"
  )
fi

# Pages in tour order: port|path. See .tours/demo-1.tour and .tours/demo-2.tour.
pages=(
  # Demo 1, step 4: Swagger UI on the .NET 8 app
  "5300|/swagger"
  # Demo 1, step 9: the .NET 11 controllers app
  "5301|/openapi/v1.json"
  "5301|/openapi/v1.yaml"
  "5301|/scalar/#tag/talks/GET/api/talks"
  "5301|/swagger"
  # Demo 2, step 9: QUERY in Scalar, then the nested tags and the deprecated endpoint
  "5306|/scalar/#tag/talks/QUERY/api/talks/search"
  "5305|/scalar/#tag/weather"
  "5305|/scalar/#tag/forecasts/GET/old-endpoint"
  # Demo 2, step 11: Server-Sent Events
  "5306|/scalar/#tag/talks/GET/api/talks/live"
  # Demo 2, step 13: union
  "5306|/scalar/#tag/talks/GET/api/talks/{id}/session"
  # Demo 2, step 15: JSON and CSV content types
  "5306|/scalar/#tag/talks/GET/api/talks/{id}/export"
)

if [[ -n "${BROWSER_CMD:-}" ]]; then
  opener="$BROWSER_CMD"
elif command -v open >/dev/null; then
  opener=open
elif command -v xdg-open >/dev/null; then
  opener=xdg-open
else
  echo "No browser opener found (open, xdg-open); use --no-browser or set BROWSER_CMD." >&2
  exit 1
fi

echo "Building..."
dotnet build OpenApiInDotnet.slnx --nologo -v quiet

trap 'echo; echo "Stopping demos..."; kill $(jobs -p) 2>/dev/null || true; wait 2>/dev/null || true' EXIT INT TERM

# dotnet run would open its own browser tabs for the launch profiles with launchBrowser,
# which breaks the order below. Run without the profile and pass the same settings instead.
export ASPNETCORE_ENVIRONMENT=Development

for entry in "${projects[@]}"; do
  IFS=: read -r project port <<< "$entry"
  dotnet run --no-build --no-launch-profile --project "$project" -- --urls "http://localhost:$port" \
    > >(sed "s|^|[$(basename "$project")] |") 2>&1 &
done

echo
echo "Demos running:"
for entry in "${projects[@]}"; do
  IFS=: read -r project port <<< "$entry"
  printf "  %-22s http://localhost:%s\n" "$(basename "$project")" "$port"
done

if $open_browser; then
  for entry in "${projects[@]}"; do
    IFS=: read -r project port <<< "$entry"
    printf "Waiting for %s..." "$(basename "$project")"
    until curl -s -o /dev/null "http://localhost:$port/"; do sleep 0.5; done
    echo " ready"
  done

  echo
  echo "Opening the demo pages in tour order..."
  for page in "${pages[@]}"; do
    IFS='|' read -r port path <<< "$page"
    url="http://localhost:$port$path"
    echo "  $url"
    "$opener" "$url" >/dev/null 2>&1
    sleep 1
  done
fi

echo
echo "Press Ctrl+C to stop."

wait

#!/bin/bash

set -e

dotnet tool install --version 7.0.15 --global dotnet-ef

cat << \EOF
# Add .NET Core SDK tools
export PATH="$PATH:/root/.dotnet/tools"
EOF

export PATH="$PATH:/root/.dotnet/tools"

ls /root/.dotnet/tools

# Run EF Core scaffold command
dotnet-ef database update -p /src/Marketplace.Infrastructure/Marketplace.Infrastructure.csproj -s /src/Marketplace.API/Marketplace.API.csproj  -- --environment Development

# Start the application
#exec dotnet Marketplace.API.dll

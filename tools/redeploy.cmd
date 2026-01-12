@echo off
echo Building the project in Release configuration...
dotnet build ..\shell-chat.sln -c Release

echo Packing the tool in Release configuration...
dotnet pack ..\shell-chat.sln -c Release --no-build

echo Uninstalling the existing global tool...
dotnet tool uninstall --global shell-chat

echo Installing the new version of the global tool...
dotnet tool install --global --add-source ../nupkg shell-chat

echo Redeployment complete. You can now use the 'shc' command.

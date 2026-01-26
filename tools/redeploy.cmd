@echo off
:: Navigate to project root (one level up from tools folder)
pushd %~dp0..

echo ========================================
echo  Rebuilding and Redeploying shell-chat
echo ========================================
echo.

:: Clean previous build
echo [1/4] Cleaning previous build...
dotnet clean --configuration Release --verbosity quiet
if %ERRORLEVEL% neq 0 (
    echo ERROR: Clean failed!
    popd
    pause
    exit /b 1
)

:: Build and pack
echo [2/4] Building and packing...
dotnet pack --configuration Release --verbosity quiet
if %ERRORLEVEL% neq 0 (
    echo ERROR: Pack failed!
    popd
    pause
    exit /b 1
)

:: Uninstall existing tool
echo [3/4] Uninstalling existing tool...
dotnet tool uninstall -g shell-chat 2>nul

:: Install as global tool
echo [4/4] Installing as global tool...
dotnet tool install -g --add-source ./nupkg shell-chat
if %ERRORLEVEL% neq 0 (
    echo ERROR: Tool installation failed!
    popd
    pause
    exit /b 1
)

:: Return to original directory
popd

echo.
echo ========================================
echo  Deployment complete!
echo  Run 'shc --help' to verify
echo ========================================
pause

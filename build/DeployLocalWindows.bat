CD ..
CD source
RD ..\artifacts /s /q
dotnet publish %product% -c Release -r win-x64 --self-contained -o ../artifacts/publish/windows /p:DebugType=None /p:DebugSymbols=false

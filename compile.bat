set msbuild4=%systemroot%\Microsoft.Net\FrameWork\v4.0.30319\msbuild.exe

set nameofproject=QuestorLauncher
set csproj=.\%nameofproject%.sln
"%msbuild4%" "%csproj%" /p:configuration="Debug" /target:Clean;Build
Echo Done building [ %nameofproject% ] - see above for any errors - 1 of 6 builds
%pause%
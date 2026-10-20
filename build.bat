@echo off

rmdir /s /q Hua.Sort\bin
rmdir /s /q Hua.Sort\obj

dotnet build Hua.Sort\Hua.Sort.csproj -c Release
@echo off

rmdir /s /q Hua.Sort\bin
rmdir /s /q Hua.Sort\obj

dotnet build Hua.Sort\Hua.Sort.csproj -c Release

del Hua.Sort\bin\Release\net8.0\Hua.Sort.deps.json
del Hua.Sort\bin\Release\net8.0\Hua.Sort.pdb
del Hua.Sort\bin\Release\net9.0\Hua.Sort.deps.json
del Hua.Sort\bin\Release\net9.0\Hua.Sort.pdb
del Hua.Sort\bin\Release\net10.0\Hua.Sort.deps.json
del Hua.Sort\bin\Release\net10.0\Hua.Sort.pdb
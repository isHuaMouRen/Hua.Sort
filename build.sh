#!/bin/bash

cd Hua.Sort

rm -r bin && rm -r obj

mkdir -p bin/Release

cd bin/Release

dotnet build ../../Hua.Sort.csproj -c Release

rm -f net*/Hua.Sort.deps.json
rm -f net*/Hua.Sort.pdb

version=$(grep -oP '(?<=<Version>).*?(?=</Version>)' ../../Hua.Sort.csproj)

zip -r "Hua.Sort.$version.zip" net*
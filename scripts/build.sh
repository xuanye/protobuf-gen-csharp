set -ex

cd $(dirname $0)/../src/

distFolder="../dist/"

if [ -d $distFolder ]; then
  rm -R $distFolder
fi

mkdir -p $distFolder

dotnet restore ./Protobuf.Gen.sln

dotnet build ./Protobuf.Gen/Protobuf.Gen.csproj -c Release


# publish for windows
dotnet publish ./Protobuf.Gen/Protobuf.Gen.csproj -r win10-x64 --output bin/dist/win10x64
dotnet publish ./Protobuf.Gen/Protobuf.Gen.csproj -r win7-x86 --output bin/dist/win7x86
dotnet publish ./Protobuf.Gen/Protobuf.Gen.csproj -r win7-x64 --output bin/dist/win7x64

# publish for macos
dotnet publish ./Protobuf.Gen/Protobuf.Gen.csproj -r osx.10.11-x64 --output bin/dist/osx

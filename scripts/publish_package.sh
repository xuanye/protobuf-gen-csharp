set -ex

cd $(dirname $0)/../src/

artifactsFolder="../artifacts"

if [ -d $artifactsFolder ]; then
  rm -R $artifactsFolder
fi

mkdir -p $artifactsFolder

dotnet restore ./Protobuf.Gen.sln


dotnet build ./Protobuf.Gen.Core/Protobuf.Gen.Core.csproj -c Release


versionNumber="1.0.1"

dotnet pack ./Protobuf.Gen.Core/Protobuf.Gen.Core.csproj -c Release -o ../$artifactsFolder  --version-suffix=$versionNumber

if [ "$TRAVIS_BRANCH" == "master" ]; then
    #dotnet nuget push ./$artifactsFolder/Protobuf.Gen.Core.${versionNumber}.nupkg -k $NUGET_KEY -s https://www.nuget.org
    echo "DON'T PUSH"
fi

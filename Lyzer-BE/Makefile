release:
	dotnet publish --configuration Release --output dist/

up:
	dotnet run Project --environment Development --launch-profile https

build:
	dotnet build

clean:
	rm -rf bin/ obj/ dist/

test:
	dotnet test --collect:"XPlat Code Coverage" -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=opencover

format:
	dotnet format Lyzer-BE.csproj
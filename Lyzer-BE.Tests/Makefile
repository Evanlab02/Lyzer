clean:
	rm -rf TestResults/ bin/ obj/

test:
	dotnet test --collect:"XPlat Code Coverage" -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=opencover

coverage:
	reportgenerator "-reports:TestResults/**/*.xml" "-targetdir:Coverage/HTMLReport" -reporttypes:HTML
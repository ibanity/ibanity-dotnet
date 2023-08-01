sources := $(shell find . \( -name '*.csproj' -o -name '*.cs' -o -name '*.cshtml' \) -not -path '*/obj/*')
lib = src/Client/bin/Debug/netstandard2.0/Ibanity.dll

.PHONY: docker

$(lib): $(sources)
	dotnet build .

docker:
	docker run --rm -v $(CURDIR):/app -w /app mcr.microsoft.com/dotnet/sdk:6.0 dotnet build .

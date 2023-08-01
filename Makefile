sources := $(shell find . \( -name '*.csproj' -o -name '*.cs' -o -name '*.cshtml' \) -not -path '*obj*')
lib = src/Client/bin/Debug/netstandard2.0/Ibanity.dll

$(lib): $(sources)
	dotnet build .

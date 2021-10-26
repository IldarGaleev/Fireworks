
# Fireworks

Example of using [factory method](https://refactoring.guru/design-patterns/factory-method)


## Table of content

- [Quick start](#Quick-start)
- [From sourse](#From-sourse)
- [How to use](#How-to-use)
- [Files](#Files)

## Quick start

1. Install [.Net Core v3.1](https://dotnet.microsoft.com/download/dotnet/3.1) runtime
	- [Windows](https://docs.microsoft.com/dotnet/core/install/windows)
	- [Linux](https://docs.microsoft.com/dotnet/core/install/linux)
	- [Mac](https://docs.microsoft.com/dotnet/core/install/macos)
2. Download [latest release](/release/latest)
3. Unzip files
	- **Windows** powershell
		``` powershell
		Expand-Archive -Force .\FactoryPattern_R*.zip
		```
	- **Linux/Mac**

		``` bash
		unzip -f FactoryPattern_R*.zip 
		```
5. Enter to the dir
	- **Windows/Linux/Mac**

		``` bash
		cd FactoryPattern_R*/
		```
4. Run
	- **Windows** powershell
		``` powershell
		.\FactoryPattern.exe
		```
	- **Linux/Mac**

		``` bash
		dotnet ./FactoryPattern.dll
		```
5. Enjoy ([how to use](#How-to-use))

## From source

1. Install  [.Net Core v3.1](https://dotnet.microsoft.com/download/dotnet/3.1) SDK
2. Clone repository
	``` bash
	git clone https://github.com/IldarGaleev/Fireworks.git
	```
3. Enter project directory 
	``` bash
	cd .\FactoryPattern\
	```
4. Build and run
	``` bash
	dotnet run .\FactoryPattern.csproj
	```

## How to use

- `Esc` - exiting the application 
- `F` - fireworks launch

## Files

- **DrawPrimitives** - Namespace contains primitives for draw
	- **[Coordinate.cs](DrawPrimitives/Coordinate.cs)** - Coordinate info
	- **[Pixel.cs](DrawPrimitives/Pixel.cs)** - Pixel info
	- **[PixelList.cs](DrawPrimitives/PixelList.cs)** - Pixel dictionary
- **FireworkGuns** - Namespace contains implemented firework guns
	- **[ConfettiGun.cs](FireworkGuns/ConfettiGun.cs)** - Confetti
	- **[RedFireGun.cs](FireworkGuns/RedFireGun.cs)** - Gun shot red fires

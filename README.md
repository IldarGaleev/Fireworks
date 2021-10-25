
# Fireworks

Example of using [factory method](https://refactoring.guru/design-patterns/factory-method)


## Quick start

1. Download binary
2. Install [.Net Core v3.1](https://dotnet.microsoft.com/download/dotnet/3.1) runtime
3. Run
	* **Windows**
	``` bash
	.\FactoryPattern.exe
	```
	* **Linux**
	``` bash
	chmod +x FactoryPattern
	./FactoryPattern
	```
4. [Use](#How-to-use) 

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

* **Esc** - exiting the application 
* **F** - fireworks launch

## Files

* **DrawPrimitives** - Namespace contains primitives for draw
	* **[Coordinate.cs](DrawPrimitives/Coordinate.cs)** - Coordinate info
	* **[Pixel.cs](DrawPrimitives/Pixel.cs)** - Pixel info
	* **[PixelList.cs](DrawPrimitives/PixelList.cs)** - Pixel dictionary
* **FireworkGuns** - Namespace contains implemented firework guns
	* **[ConfettiGun.cs](FireworkGuns/ConfettiGun.cs)** - Confetti
	* **[RedFireGun.cs](FireworkGuns/RedFireGun.cs)** - Gun shot red fires

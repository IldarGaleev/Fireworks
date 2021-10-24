
# Fireworks

This code demonstrate "Abstract fabric" pattern

## Build

1. Install  [.Net Core v3.1](https://dotnet.microsoft.com/download/dotnet/3.1)
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

## How use

* Press **Esc** button - exit program
* Press **F** button - fire gun

## Files

* **DrawPrimitives** - Namespace contains primitives for draw
	* **[Coordinate.cs](DrawPrimitives/Coordinate.cs)** - Coordinate info
	* **[Pixel.cs](DrawPrimitives/Pixel.cs)** - Pixel info
	* **[PixelList.cs](DrawPrimitives/PixelList.cs)** - Pixel dictionary
* **FireworkGuns** - Namespace contains implemented firework guns
	* **[ConfettiGun.cs](FireworkGuns/ConfettiGun.cs)** - Confetti
	* **[RedFireGun.cs](FireworkGuns/RedFireGun.cs)** - Gun shot red fires

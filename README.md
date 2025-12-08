# HotelTimeSolution

Repository with three console apps and shared libraries:
- `Fujtajbl` (`Fujtajbl.Core`): calculator with `+ - * /`, input validation, and guards against divide-by-zero or overflow.
- `IPMasking` (`IPMasking.Core`): checks whether IP addresses are in the same subnet (CIDR input, prefix/mask helpers).
- `Maze` (`Maze.Core`): console visualizer that lets four different "midgets" solve a maze and draws their progress.
- `UnitTests`: xUnit tests for the above logic (IP masking, calculator, maps and movement in the maze).

## Solution structure
- `Fujtajbl/` and `Fujtajbl.Core/` – calculator and operation strategies.
- `IPMasking/` and `IPMasking.Core/` – subnet validation and IP utilities.
- `Maze/` and `Maze.Core/` – map loading, path validation, simulation, and rendering.
- `UnitTests/` – xUnit tests; maze maps live in `UnitTests/Maze/TestMaps`.


### Fujtajbl (calculator)
```pwsh
dotnet run --project Fujtajbl/Fujtajbl.csproj
```
Enter numbers, choose an operation (`+ - * /`), `exit` quits.

### IPMasking (subnet check)
```pwsh
dotnet run --project IPMasking/IPMasking.csproj
```
1) Enter a base address in CIDR form `X.X.X.X/Y`.  
2) The app computes the mask and repeatedly compares more IPs to see if they are in the same subnet.  
`exit` ends the run.

### Maze (maze visualizer)
```pwsh
dotnet run --project Maze/Maze.csproj
```
On start, provide a path to a map file, e.g.:
```
d:\src\HotelTimeSolution\UnitTests\Maze\TestMaps\ValidSingleEnd.dat
```
Legend: `#`=wall, space=path, `S`=start, `F`=finish. Console resizes to the map; four midgets draw their progress in real time.

## Tests
```pwsh
cd d:\src\HotelTimeSolution
dotnet test UnitTests/UnitTests.csproj
```
Coverage includes mask computation and subnet comparison, the calculator, and maze validation/traversal including map files in `UnitTests/Maze/TestMaps`.

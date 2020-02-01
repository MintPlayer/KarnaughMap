# QuineMcCluskey
The Quine McCluskey algorithm in c#
## NuGet package
https://www.nuget.org/packages/QuineMcCluskey/

https://www.nuget.org/packages/KarnaughMap/
## Installation
### NuGet package manager
Open the NuGet package manager and install the **QuineMcCluskey** package in the project
### Package manager console
Install-Package QuineMcCluskey
## Usage
Simply call the following method with the required minterms and don't-cares:

    var loops = QuineMcCluskeySolver.QMC_Solve(minterms, dontcares);

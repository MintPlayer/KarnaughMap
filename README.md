# Karnaugh-Map
## Version info

| License                                                                                                               | Build status                                                                                                                                                                                                  |
|-----------------------------------------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [![License](https://img.shields.io/badge/License-Apache%202.0-green.svg)](https://opensource.org/licenses/Apache-2.0) | [![master](https://github.com/MintPlayer/KarnaughMap/actions/workflows/dotnet-core.yml/badge.svg)](https://github.com/MintPlayer/KarnaughMap/actions/workflows/dotnet-core.yml) |

| Package        | Version                                                                                                                         |
|----------------|---------------------------------------------------------------------------------------------------------------------------------|
| QuineMcCluskey | [![NuGet Version](https://img.shields.io/nuget/v/QuineMcCluskey.svg?style=flat)](https://www.nuget.org/packages/QuineMcCluskey) |
| KarnaughMap    | [![NuGet Version](https://img.shields.io/nuget/v/KarnaughMap.svg?style=flat)](https://www.nuget.org/packages/KarnaughMap)       |

## Installation
### NuGet package manager
Open the NuGet package manager and install the **QuineMcCluskey** package in the project
### Package manager console

    Install-Package QuineMcCluskey
    Install-Package KarnaughMap

## Usage
Simply call the following method with the required minterms and don't-cares:

    var loops = QuineMcCluskeySolver.QMC_Solve(minterms, dontcares);

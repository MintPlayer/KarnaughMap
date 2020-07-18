# QuineMcCluskey
[![NuGet Version](https://img.shields.io/nuget/v/QuineMcCluskey.svg?style=flat)](https://www.nuget.org/packages/QuineMcCluskey)
[![NuGet](https://img.shields.io/nuget/dt/QuineMcCluskey.svg?style=flat)](https://www.nuget.org/packages/QuineMcCluskey)
[![Build Status](https://travis-ci.org/MintPlayer/QuineMcCluskey.svg?branch=master)](https://travis-ci.org/MintPlayer/QuineMcCluskey)

The Quine McCluskey algorithm in c#
## NuGet package
https://www.nuget.org/packages/QuineMcCluskey/
## Installation
### NuGet package manager
Open the NuGet package manager and install the **QuineMcCluskey** package in the project
### Package manager console
Install-Package QuineMcCluskey
## Usage
Simply call the following method with the required minterms and don't-cares:

    var loops = QuineMcCluskeySolver.QMC_Solve(minterms, dontcares);

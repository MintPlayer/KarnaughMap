# QuineMcCluskey
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/b119ef33dd60435eb5fb9c3451d4c3f1)](https://app.codacy.com/gh/MintPlayer/QuineMcCluskey?utm_source=github.com&utm_medium=referral&utm_content=MintPlayer/QuineMcCluskey&utm_campaign=Badge_Grade_Dashboard)
[![NuGet Version](https://img.shields.io/nuget/v/QuineMcCluskey.svg?style=flat)](https://www.nuget.org/packages/QuineMcCluskey)
[![NuGet](https://img.shields.io/nuget/dt/QuineMcCluskey.svg?style=flat)](https://www.nuget.org/packages/QuineMcCluskey)
[![Build Status](https://travis-ci.org/MintPlayer/QuineMcCluskey.svg?branch=master)](https://travis-ci.org/MintPlayer/QuineMcCluskey)
[![License](https://img.shields.io/badge/License-Apache%202.0-green.svg)](https://opensource.org/licenses/Apache-2.0)

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

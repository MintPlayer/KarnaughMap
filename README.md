# QuineMcCluskey
[![NuGet Version](https://img.shields.io/nuget/v/QuineMcCluskey.svg?style=flat)](https://www.nuget.org/packages/QuineMcCluskey)
[![NuGet](https://img.shields.io/nuget/dt/QuineMcCluskey.svg?style=flat)](https://www.nuget.org/packages/QuineMcCluskey)
[![Build Status](https://travis-ci.org/MintPlayer/QuineMcCluskey.svg?branch=master)](https://travis-ci.org/MintPlayer/QuineMcCluskey)
[![License](https://img.shields.io/badge/License-Apache%202.0-green.svg)](https://opensource.org/licenses/Apache-2.0)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/49681d22d4af4272974ed403a806e1e8)](https://www.codacy.com/gh/MintPlayer/QuineMcCluskey?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=MintPlayer/QuineMcCluskey&amp;utm_campaign=Badge_Grade)

The Quine McCluskey algorithm in c#
## Installation
### NuGet package manager
Open the NuGet package manager and install the **QuineMcCluskey** package in the project
### Package manager console
Install-Package QuineMcCluskey
## Usage
Simply call the following method with the required minterms and don't-cares:

    var loops = QuineMcCluskeySolver.QMC_Solve(minterms, dontcares);

# FileNameCounter

## Assumptions

1. The file is a clear-text file without any strange encoding (text, csv, xlm etc)
2. Line-breaks in the search word is a none-match


## Description

There are 3 versions of the algorithm that looks for matches:

1. Basic/First version - Worst Case => O(n^2)
2. Turbo version - Worst Case => O(n)
3. Case Insensitive version based on the basic algorithm.

## Observations

There is no significant difference on version 1 and 2 while testing on a 1GB file, which leads me to think that the StreamReader is the primary bottleneck.

In a file where there's is higher percentage of matches it might make a difference but it should be relativly small.

## Running the Code

### Prerequisites

Ensure that DotNet Core 6.0 SDK is installed.

Unzip the DataFiles.zip file or prepare your own text files.

### Running

From the terminal:
1. Navigate to the App.Console folder
2. Run `dotnet run .\DataFiles\TestFile-02.txt`
3. Voila!

## Project Structure

**App.Console** - Entry point for the solution.

**App.Core** - Project/Class Library containing all Services/Extensions/Models etc required.

**Tests/App.Core.Tests** - Tests for the classes/methods in App.Core.

## Potential Improvements

1. Multithreaded mode for large (3 GB+) utilizing multithreaded processing.
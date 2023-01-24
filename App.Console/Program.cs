using App.Core.Exceptions;
using App.Core.Extensions;
using App.Core.Services;

try
{
    var cmdArgs = Environment.GetCommandLineArgs();
    string? filePath = cmdArgs.Length >= 2 ? Environment.GetCommandLineArgs()[1] : null;

    if (string.IsNullOrWhiteSpace(filePath)) throw new ParameterRequiredException($"A FilePath is required as the first parameter.");

    Console.WriteLine("Validating that File Exists..");
    var fileExists = File.Exists(filePath);
    if (!fileExists) throw new AppFileNotFoundException($"Cannot find file with path '{filePath}'.");

    Console.WriteLine("Getting file name from path..");
    var fileName = Path.GetFileNameWithoutExtension(filePath);

    var _occurrenceService = new OccurrenceService();

    // ############## Basic Case Sensitive Section ########################

    Console.WriteLine("Running with Case Sensitive matching..");

    Console.WriteLine(" - Opening file..");
    var textFileRegular = File.Open(filePath, FileMode.Open);
    System.IO.StreamReader fileStreamRegular = new System.IO.StreamReader(textFileRegular);

    Console.WriteLine(" - Counting occurrences..");
    var watch = new System.Diagnostics.Stopwatch();

    watch.Start();
    var count = _occurrenceService.CountOccurrences(fileStreamRegular, fileName);
    watch.Stop();

    textFileRegular.Close();
    Console.WriteLine($" - Found '{count.ToString("N0")}' matches in file '{filePath}', in {watch.ElapsedMilliseconds.ToString("N0")} ms!");


    // ############## Turbo Case Sensitive Section ########################

    Console.WriteLine("Running Turbo Mode with Case Sensitive matching..");

    Console.WriteLine(" - Opening file..");
    var textFileTurbo = File.Open(filePath, FileMode.Open);
    System.IO.StreamReader fileStreamTurbo = new System.IO.StreamReader(textFileTurbo);

    Console.WriteLine(" - Counting occurrences..");
    var watchTurbo = new System.Diagnostics.Stopwatch();

    watchTurbo.Start();
    var countTurbo = _occurrenceService.CountOccurrences(fileStreamTurbo, fileName);
    watchTurbo.Stop();

    textFileTurbo.Close();
    Console.WriteLine($" - Found '{countTurbo.ToString("N0")}' matches in file '{filePath}', in {watchTurbo.ElapsedMilliseconds.ToString("N0")} ms!");

    // ############## Case Insensitive Section ########################

    Console.WriteLine("Running with Case Insensitive matching..");


    Console.WriteLine(" - Opening file..");
    var textFileCaseInsensitive = File.Open(filePath, FileMode.Open);
    System.IO.StreamReader fileStreamCaseInsensitive = new System.IO.StreamReader(textFileCaseInsensitive);

    var watchCaseInsensitive = new System.Diagnostics.Stopwatch();

    Console.WriteLine(" - Counting occurrences..");
    watchCaseInsensitive.Start();
    var countInsensitive = _occurrenceService.CountOccurrencesCaseInsensitive(fileStreamCaseInsensitive, fileName);
    watchCaseInsensitive.Stop();

    textFileCaseInsensitive.Close();
    Console.WriteLine($" - Found '{countInsensitive.ToString("N0")}' Case Insensitive matches in file '{filePath}', in {watchCaseInsensitive.ElapsedMilliseconds.ToString("N0")} ms!");

    
    // ############## Summary ########################


    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("---------------------------------------------------------------------------------------------");
    Console.WriteLine($"Found '{count.ToString("N0")}' exact matches and '{countInsensitive.ToString("N0")}' matches ignoring casing.");
    Console.WriteLine("---------------------------------------------------------------------------------------------");
}
catch (System.Exception e)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Error occurred: {e.GetType().ToString()} - {e.Message}");
}

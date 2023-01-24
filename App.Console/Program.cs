using App.Core.Exceptions;
using App.Core.Extensions;
using App.Core.Services;

string filePath = Environment.GetCommandLineArgs()[1];

if(string.IsNullOrWhiteSpace(filePath)) throw new ParameterRequiredException($"A FilePath is required as the first parameter.");

Console.WriteLine("Validating that File Exists..");
var fileExists = File.Exists(filePath);
if(!fileExists) throw new AppFileNotFoundException($"Cannot find file with path '{filePath}'.");

Console.WriteLine("Getting file name from path..");
var fileName = Path.GetFileNameWithoutExtension(filePath);

var _occurrenceService = new OccurrenceService();

// Case Sensitive Section

Console.WriteLine("Opening file..");
var textFile = File.Open(filePath, FileMode.Open);
System.IO.StreamReader fileStream = new System.IO.StreamReader(textFile);

Console.WriteLine("Counting occurrences..");
var watch = new System.Diagnostics.Stopwatch();
            
watch.Start();
var count = _occurrenceService.CountOccurrences(fileStream, fileName);
watch.Stop();

textFile.Close();
Console.WriteLine($"Found '{count.ToString("N0")}' matches in file '{filePath}', in {watch.ElapsedMilliseconds.ToString("N0")} ms!");

// Case Insensitive Section

Console.WriteLine("Running again with Case Insensitive matching..");


Console.WriteLine("Opening file..");
var textFileCaseInsensitive = File.Open(filePath, FileMode.Open);
System.IO.StreamReader fileStreamCaseInsensitive = new System.IO.StreamReader(textFileCaseInsensitive);

var watchCaseInsensitive = new System.Diagnostics.Stopwatch();

Console.WriteLine("Counting occurrences..");
watchCaseInsensitive.Start();
var countInsensitive = _occurrenceService.CountOccurrencesCaseInsensitive(fileStreamCaseInsensitive, fileName);
watchCaseInsensitive.Stop();

textFile.Close();
Console.WriteLine($"Found '{countInsensitive.ToString("N0")}' Case Insensitive matches in file '{filePath}', in {watchCaseInsensitive.ElapsedMilliseconds.ToString("N0")} ms!");
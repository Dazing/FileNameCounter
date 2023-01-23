using App.Core.Exceptions;
using App.Core.Extensions;
using App.Core.Services;

string filePath = Environment.GetCommandLineArgs()[1];

Console.WriteLine("Validating that File Exists..");
var fileExists = File.Exists(filePath);
if(!fileExists) throw new AppFileNotFoundException($"Cannot find file with path '{filePath}'");

Console.WriteLine("Getting file name from path..");
var fileName = Path.GetFileNameWithoutExtension(filePath);

Console.WriteLine("Opening file..");
var textFile = File.Open(filePath, FileMode.Open);
System.IO.StreamReader fileStream = new System.IO.StreamReader(textFile);

Console.WriteLine("Counting occurrences..");
var watch = new System.Diagnostics.Stopwatch();
var _occurrenceService = new OccurrenceService();
            
watch.Start();
var count = _occurrenceService.CountOccurrences(fileStream, fileName);
watch.Stop();

Console.WriteLine($"Found '{count.ToString("N0")}' matches in file '{filePath}', in {watch.ElapsedMilliseconds.ToString("N0")} ms!");
using App.Core.Services;
using App.Tests.Common.Attributes;
using App.Core.Tests.TestFiles;
using System.Text;

namespace App.Core.Tests;

public class OccurrenceServiceTests
{
    private const string _fileBasePath = "./Files";
    private readonly IOccurrenceService _occurrenceService;

    public OccurrenceServiceTests()
    {
       _occurrenceService = new OccurrenceService();
    }

    [Theory]
    [EmbeddedResourceData(typeof(TestFile), "TestFile_EmptyFile.txt")]
    public void CountOccurrences_EmptyString_ShouldReturn0(string data)
    {
        var streamReader = GetReaderFromTestData(data);
        
        var count = _occurrenceService.CountOccurrences(streamReader, "TestFile_EmptyFile");

        Assert.Equal(0, count);
    }

    [Theory]
    [EmbeddedResourceData(typeof(TestFile), "TestFile_NoMatches.txt")]
    public void CountOccurrences_NoMatches_ShouldReturn0(string data)
    {
        var streamReader = GetReaderFromTestData(data);
        
        var count = _occurrenceService.CountOccurrences(streamReader, "TestFile_EmptyFile");

        Assert.Equal(0, count);
    }

    [Theory]
    [EmbeddedResourceData(typeof(TestFile), "TestFile_SomeMatches.txt")]
    public void CountOccurrences_SomeMatches_ShouldReturnMatches(string data)
    {
        var streamReader = GetReaderFromTestData(data);
        
        var count = _occurrenceService.CountOccurrences(streamReader, "TestFile_SomeMatches");

        Assert.Equal(7, count);
    }

    private StreamReader GetReaderFromTestData(string data) {
        byte[] byteArray = Encoding.ASCII.GetBytes(data);
        MemoryStream ms = new MemoryStream(byteArray);
        return new StreamReader(ms, System.Text.Encoding.UTF8, true);
    }
}
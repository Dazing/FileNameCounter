namespace App.Core.Services;

public interface IOccurrenceService
{
    int CountOccurrences(StreamReader stream, string searchTerm);
    int CountOccurrencesCaseInsensitive(StreamReader stream, string searchTerm);
}

public class OccurrenceService : IOccurrenceService
{
    public OccurrenceService() { }

    public int CountOccurrences(StreamReader stream, string searchTerm)
    {
        int counter = 0;

        string line;
        bool hasIncremented = false;
        while (!stream.EndOfStream)
        {
            line = stream.ReadLine();

            if (string.IsNullOrWhiteSpace(line)) continue;

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] != searchTerm[0]) continue;
                for (int j = 1; j < searchTerm.Length; j++)
                {
                    if (line[i+j] != searchTerm[j]) break;
                    else if (j == searchTerm.Length - 1) {
                        counter++;
                        hasIncremented = true;
                    };
                }
                
                if (hasIncremented)
                {
                    i += searchTerm.Length;
                    hasIncremented = false;
                }
            }
        }

        return counter;
    }
    
    public int CountOccurrencesCaseInsensitive(StreamReader stream, string searchTerm)
    {
        int counter = 0;

        string line;
        bool hasIncremented = false;
        bool firstCharMatch = false;
        bool charMatch = false;

        while (!stream.EndOfStream)
        {
            line = stream.ReadLine();

            if (string.IsNullOrWhiteSpace(line)) continue;

            for (int i = 0; i < line.Length; i++)
            {
                firstCharMatch = char.ToLowerInvariant(line[i]) == char.ToLowerInvariant(searchTerm[0]);
                if (!firstCharMatch) continue;

                for (int j = 1; j < searchTerm.Length; j++)
                {
                    
                    charMatch = char.ToLowerInvariant(line[i+j]) == char.ToLowerInvariant(searchTerm[j]);
                    if (!charMatch) break;
                    else if (j == searchTerm.Length - 1) {
                        counter++;
                        hasIncremented = true;
                    };
                }
                
                if (hasIncremented)
                {
                    i += searchTerm.Length;
                    hasIncremented = false;
                }
            }
        }

        return counter;
    }
}

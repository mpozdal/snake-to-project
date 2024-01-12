using System.Collections.Generic;

namespace SnakeGame;

public sealed class ResultsCollection
{
    private static List<Result> _results = new List<Result>();
        
    public static void AddResult(Result res)
    {
        _results.Add(res);
    }

    public static List<Result> GetResults()
    {
        return _results;
    }

    public static Result GetResult(int index)
    {
        return _results[index];
    }

    public static int CountOfResults()
    {
        return _results.Count;
    }
    
}
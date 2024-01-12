namespace SnakeGame;

public class Result
{
    private string _name;
    private int _result;
    private string _level;

    public Result(string name, int result, string level)
    {
        _name = name;
        _result = result;
        _level = level;
    }

    public string GetName()
    {
        return _name;
    }

    public int GetResult()
    {
        return _result;
    }
    public string GetLevel()
    {
        return _level;
    }
}
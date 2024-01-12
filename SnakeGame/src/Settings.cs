using System;

namespace SnakeGame;

public sealed class Settings
{
    private string _nickname;
    private int _cols;
    private int _rows;
    private int _level;
    private static Settings _instance;

    private Settings(string nickname, int rows, int cols, int level)
    {
        _nickname = nickname;
        _cols = cols;
        _rows = rows;
        _level = level;
    }

    public static Settings GetInstance()
    {
        return _instance;
    }

    public static Settings Instance(string nickname, int rows, int cols, int level)
    {
        if (_instance == null)
        {
            _instance = new Settings( nickname,  rows,  cols, level);
        }

        return _instance;
    }
    public static void ChangeSettings(string nickname, int rows, int cols, int level)
    {
        _instance = null;

        _instance = new Settings( nickname,  rows,  cols, level);
    }

    public string Nickname
    {
        get => _nickname;
        set => _nickname = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int Cols
    {
        get => _cols;
        set => _cols = value;
    }

    public int Rows
    {
        get => _rows;
        set => _rows = value;
    }
    public int Level
    {
        get => _level;
        set => _level = value;
    }
}

using System;
using System.Collections.Generic;
using Avalonia.Remote.Protocol.Designer;

namespace SnakeGame;

public class GameState
{
    public int Rows { get; }
    public int Cols { get; }
    public GridValue[,] Grid { get; }
    public int Score { get; private set; }
    public bool GameOver { get; private set; }
    private Snake _snake;
    private Food _food;
    private string _name;
    private string _level;

    public GameState(int rows, int cols)
    {
        Rows = rows;
        Cols = cols;
        Grid = new GridValue[rows, cols];

        _snake = new Snake(Grid, rows, cols);
        
        _food = new Food(Grid, rows, cols);
        
        _name = Settings.GetInstance().Nickname;
        if (Settings.GetInstance().Level == 0) _level = "EASY";
        else if (Settings.GetInstance().Level == 1) _level = "MEDIUM";
        else _level = "HARD";

    }
    public Snake Snake
    {
        get => _snake;
    }
    private bool OutsideGrid(Position pos)
    {
        return pos.Row < 0 || pos.Row >= Rows || pos.Col < 0 || pos.Col >= Cols;
    }

    private GridValue WillHit(Position newHeadPos)
    {
        if (OutsideGrid(newHeadPos))
        {
            return GridValue.Outside;
        }

        if (newHeadPos == _snake.TailPosition())
        {
            return GridValue.Empty;
        }
        
        return Grid[newHeadPos.Row, newHeadPos.Col];
    }

    public void Move()
    {
        if (_snake.DirChanges().Count > 0)
        {
            _snake.Dir = _snake.DirChanges().First.Value;
            _snake.DirChanges().RemoveFirst();
        }
        Position newHeadPos = _snake.HeadPosition().Translate(_snake.Dir);
        GridValue hit = WillHit(newHeadPos);

        if (hit == GridValue.Outside || hit == GridValue.Snake)
        {
            GameOver = true;
            ResultsCollection.AddResult(new Result(_name, Score,_level ));
        } 
        else if (hit == GridValue.Empty)
        {
            _snake.RemoveTail();
            _snake.AddHead(newHeadPos);
        }
        else if (hit == GridValue.Food)
        {
            _snake.AddHead(newHeadPos);
            Score++;
            _food.AddFood();
        }
        
    }
    
    
}
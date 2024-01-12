namespace SnakeGame;
using System;
using System.Collections.Generic;
public class Snake
{
    private readonly LinkedList<Direction> dirChanges = new LinkedList<Direction>();

    private readonly LinkedList<Position> _snakePositions = new LinkedList<Position>();
    public Direction Dir { get; set; }

    private GridValue[,] Grid;

    public LinkedList<Direction> DirChanges()
    {
        return dirChanges;
    }

    public Snake(GridValue[,] grid, int rows, int cols)
    {
        Dir = Direction.Right;
        Grid = grid;
        
        AddSnake(rows, cols);
    }
    private void AddSnake(int rows, int cols)
    {
        int r = rows / 2;

        for (int c = 1; c <= 3; c++)
        {
            Grid[r, c] = GridValue.Snake;
            _snakePositions.AddFirst((new Position(r, c)));
        }
    }
    public LinkedList<Position> SnakePositions()
    {
        return _snakePositions;
    }
    
    public Position HeadPosition()
    {
        return _snakePositions.First.Value;
    }

    public Position TailPosition()
    {
        return _snakePositions.Last.Value;
    }
    public void AddHead(Position pos)
    {
        _snakePositions.AddFirst(pos);
        Grid[pos.Row, pos.Col] = GridValue.Snake;
    }

    public void RemoveTail()
    {
        Position tail = _snakePositions.Last.Value;
        Grid[tail.Row, tail.Col] = GridValue.Empty;
        _snakePositions.RemoveLast();
    }

    public Direction GetLastDirection()
    {
        if (dirChanges.Count == 0)
        {
            return Dir;
        }

        return dirChanges.Last.Value;
    }

    public bool CanChangeDirection(Direction newDir)
    {
        if (dirChanges.Count == 2)
        {
            return false;
        }

        Direction lastDir = GetLastDirection();

        return newDir != lastDir && newDir != lastDir.Opposite();
    }

    public void ChangeDirection(Direction dir)
    {
        if (CanChangeDirection(dir))
        {
            dirChanges.AddLast(dir);
        }
        
    }
}
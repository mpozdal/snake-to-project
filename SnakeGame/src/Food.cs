namespace SnakeGame;
using System;
using System.Collections.Generic;
public class Food
{
    private GridValue[,] Grid;
    private int Rows;
    private int Cols;
    
    public Food(GridValue[,] grid, int rows, int cols)
    {
        Grid = grid;
        Rows = rows;
        Cols = cols;
        AddFood();
    }
    
    public void AddFood()
    {
        List<Position> empty = new List<Position>(EmptyPositions());
        if (empty.Count == 0)
        {
            return;
        }

        Position pos = empty[new Random().Next(empty.Count)];
        Grid[pos.Row, pos.Col] = GridValue.Food;
    }
    private IEnumerable<Position> EmptyPositions()
    {
        for(int r = 0; r < Rows; r++)
        {
            for(int c = 0; c < Cols; c++)
            {
                if (Grid[r, c] == GridValue.Empty)
                {
                    yield return new Position(r, c);
                }
            }
        }
    }
}
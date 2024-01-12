using Avalonia.Controls.Primitives;

namespace SnakeGame;
using System.Collections.Generic;

using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Bitmap = Avalonia.Media.Imaging.Bitmap;
using Image = Avalonia.Controls.Image;
public sealed class Game
{
    private readonly Dictionary<GridValue, Bitmap> _gridValToImage = new()
    {
        { GridValue.Empty, Images.Empty },
        { GridValue.Snake, Images.Body },
        { GridValue.Food, Images.Food }
    };

    private readonly Dictionary<Direction, int> dirToRotation = new()
    {
        { Direction.Up, 0 },
        { Direction.Right, 90 },
        { Direction.Down, 180 },
        { Direction.Left, 270 }
    };
    
    private readonly int _rows, _cols;
    private readonly Image[,] _gridImages;
    private GameState _gameState;
    private bool _gameRunning;
    private Border Overlay;
    private UniformGrid GameGrid;
    private TextBlock OverlayText;
    private TextBlock ScoreText;
    private static Game _instance = null;
    private Settings _settings;
    private int level;
    private Game(bool gameRunning, Border overlay, UniformGrid gameGrid, TextBlock overlayText, TextBlock scoreText)
    {
        _settings = Settings.GetInstance();
        _cols = _settings.Cols;
        _rows = _settings.Rows;
         GameGrid = gameGrid;
         OverlayText = overlayText;
         ScoreText = scoreText;
         Overlay = overlay;
        _gridImages = SetupGrid();
        if (_settings.Level == 0) level = 120;
        if (_settings.Level == 1) level = 90;
        if (_settings.Level == 2) level = 60;
        
        _gameState = new GameState(_rows, _cols);
        _gameRunning = gameRunning;
     
    }

    public static void RemoveInstance()
    {
        _instance = null;
    }

    public static Game Instance(bool gameRunning, Border overlay, UniformGrid gameGrid, TextBlock overlayText, TextBlock scoreText)
    {
            if (_instance == null)
            {
                _instance = new Game( gameRunning,  overlay,  gameGrid,  overlayText,  scoreText);
            }
            return _instance;
    }
    private async Task RunGame()
    {
        Draw();
        await ShowCountDown();
       Overlay.IsVisible = false;
        await GameLoop();
        await ShowGameOver();
        _gameState = new GameState(_rows, _cols);
    }
    public async void Window_KeyDown( KeyEventArgs e)
    {
        if (Overlay.IsVisible == true)
        {
            e.Handled = true;
           
        }
        
        if (!_gameRunning)
        {
            _gameRunning = true;
            await RunGame();
            _gameRunning = false;
        }
        if (_gameState.GameOver)
        {
            return;
        }

        switch(e.Key)
        {
            case Key.Left:
                _gameState.Snake.ChangeDirection(Direction.Left);
                break; 
            case Key.Right:
                _gameState.Snake.ChangeDirection(Direction.Right);
                break;
            case Key.Up:
                _gameState.Snake.ChangeDirection(Direction.Up);
                break;
            case Key.Down:
                _gameState.Snake.ChangeDirection(Direction.Down);
                break;
        }
    }

    private async Task GameLoop()
    {
        while (!_gameState.GameOver)
        {
            await Task.Delay(level);
            _gameState.Move();
            Draw();
        }
    }
    private Image[,] SetupGrid()
    {
        Image[,] images = new Image[_rows, _cols];
        GameGrid.Rows = _rows;
        GameGrid.Columns = _cols;
        GameGrid.Width = GameGrid.Height * (_cols / (double)_rows);
    
        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _cols; c++)
            {
                Image image = new Image
                {
                    Source = Images.Empty,
                    RenderTransformOrigin = new RelativePoint(0.5,0.5, 0)
                };
                images[r, c] = image;
                GameGrid.Children.Add(image);
            }
        }
    
        return images;
    }

    private void Draw()
    {
        DrawGrid();
        DrawSnakeHead();
        ScoreText.Text = $"SCORE {_gameState.Score}";
    }
    private void DrawGrid()
    {
        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _cols; c++)
            {
                GridValue gridVal = _gameState.Grid[r, c];
                _gridImages[r, c].Source = _gridValToImage[gridVal];
            }
        }
    }

    private async Task ShowCountDown()
    {
        for(int i = 3; i >= 1; i--)
        {
            OverlayText.Text = i.ToString();
            await Task.Delay(500);
        }
    }

    private async Task ShowGameOver()
    {
        await DrawDeadSnake();
        await Task.Delay(500);
        Overlay.IsVisible = true;
        OverlayText.Text = "PRESS ANY KEY TO START";
    }

    private void DrawSnakeHead()
    {
        Position headPos = _gameState.Snake.HeadPosition();
        Image image = _gridImages[headPos.Row, headPos.Col];
        image.Source = Images.Head;

        int rotation = dirToRotation[_gameState.Snake.Dir];
        image.RenderTransform = new RotateTransform(rotation);
    }

    private async Task DrawDeadSnake()
    {
        List<Position> positions = new List<Position>(_gameState.Snake.SnakePositions());
        for (int i = 0; i < positions.Count; i++)
        {
            Position pos = positions[i];
            Bitmap source = (i == 0) ? Images.DeadHead : Images.DeadBody;
            _gridImages[pos.Row, pos.Col].Source = source;
            await Task.Delay(50);
        }
        Position headPos = _gameState.Snake.HeadPosition();
        Image image = _gridImages[headPos.Row, headPos.Col];
        image.Source = Images.DeadHead;
        

    }
}
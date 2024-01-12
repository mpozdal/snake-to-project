using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace SnakeGame;

public partial class GameScreen : UserControl
{
    private ContentControl _content;
    private bool _gameRunning;
    private Game _game;

    public GameScreen(ContentControl content)
    {
        InitializeComponent();
        _content = content;
        
        _gameRunning = false;
        _game = Game.Instance(_gameRunning, Overlay, GameGrid, OverlayText, ScoreText);
    }
    private void InputElement_OnKeyDown(object? sender, KeyEventArgs e)
    {
        
        _game.Window_KeyDown(e);
    }

    private void GoBackToMenu(object? sender, RoutedEventArgs e)
    {
        Game.RemoveInstance();
        _content.Content = new MenuScreen(_content);
       
    }
    
}
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.CodeAnalysis;

namespace SnakeGame;

public partial class MenuScreen : UserControl
{
    private ContentControl _content;
    public MenuScreen(ContentControl content)
    {
        InitializeComponent();
        Logo.Source = Images.Logo;
        _content = content;
    }

    private void StartGame_OnClick(object? sender, RoutedEventArgs e)
    {
        _content.Content = new GameScreen(_content);
    }
    private void Settings_OnClick(object? sender, RoutedEventArgs e)
    {
        _content.Content = new SettingsScreen(_content);
    }
    private void Results_OnClick(object? sender, RoutedEventArgs e)
    {
        _content.Content = new GameScreen(_content);
    }
    
}
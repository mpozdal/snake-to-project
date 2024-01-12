using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace SnakeGame;

public partial class SettingsScreen : UserControl
{
    private ContentControl _content;
    private Settings _settings;
    public SettingsScreen(ContentControl content)
    {
        InitializeComponent();
        _content = content;
        _settings = Settings.GetInstance();
        Nickname.Text = _settings.Nickname;
        Cols.Text = _settings.Cols.ToString();
        Rows.Text = _settings.Rows.ToString();
        Level.SelectedIndex = _settings.Level;
    }

    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        
        Settings.ChangeSettings(Nickname.Text, Int32.Parse(Rows.Text), Int32.Parse(Cols.Text), Level.SelectedIndex);
        _content.Content = new MenuScreen(_content);
    }
}
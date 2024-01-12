
using System;
using System.Collections.Generic;

using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Bitmap = Avalonia.Media.Imaging.Bitmap;
using Image = Avalonia.Controls.Image;

namespace SnakeGame;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        contentControl.Content = new MenuScreen(contentControl);
        Settings settings =  Settings.Instance("Guest", 15, 15, 0);
        ResultsCollection results = new ResultsCollection();
    }
    
}
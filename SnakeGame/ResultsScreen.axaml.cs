using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace SnakeGame;

public partial class ResultsScreen : UserControl
{
    private ContentControl _content;
    public ResultsScreen(ContentControl content)
    {
        InitializeComponent();
        _content = content;
        CreateDynamicRows();
    }

    private void GoBackToMenu_OnClick(object? sender, RoutedEventArgs e)
    {
        _content.Content = new MenuScreen(_content);
    }

    private void CreateDynamicRows()
    {
        for (int i = 0; i < ResultsCollection.CountOfResults(); i++)
        {
            RowDefinition rowDefinition = new RowDefinition();
            ResultsGrid.RowDefinitions.Add(rowDefinition);
            
            Grid rowGrid = new Grid();
            rowGrid.Width = 300;
            
            rowGrid.HorizontalAlignment = HorizontalAlignment.Center;
            rowGrid.Margin = new Thickness(0,5,0,5);
            
            ResultsGrid.Children.Add(rowGrid);
            Grid.SetRow(rowGrid, i); 
            
            rowGrid.ColumnDefinitions.Add(new ColumnDefinition());
            rowGrid.ColumnDefinitions.Add(new ColumnDefinition());  
            rowGrid.ColumnDefinitions.Add(new ColumnDefinition());  
            
            TextBlock nameTextBlock = new TextBlock();
            nameTextBlock.Text = ResultsCollection.GetResult(i).GetName();
           
            nameTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
            nameTextBlock.FontSize = 20;
            rowGrid.Children.Add(nameTextBlock);
            Grid.SetColumn(nameTextBlock, 0);
            
            TextBlock levelTextBlock = new TextBlock();
            levelTextBlock.Text = ResultsCollection.GetResult(i).GetLevel();
           
            levelTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
            levelTextBlock.FontSize = 20;
            rowGrid.Children.Add(levelTextBlock);
            Grid.SetColumn(levelTextBlock, 2);
            
            TextBlock resultTextBlock = new TextBlock();
            resultTextBlock.Text = ResultsCollection.GetResult(i).GetResult().ToString();
            
            resultTextBlock.FontSize = 20;
            resultTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
            rowGrid.Children.Add(resultTextBlock);
            Grid.SetColumn(resultTextBlock, 1); 
        }
    }
}
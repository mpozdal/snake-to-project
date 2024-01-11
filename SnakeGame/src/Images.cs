using System;
using System.Drawing.Imaging;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace SnakeGame;

public static class Images
{
    public readonly static Bitmap Empty = LoadImage("Empty.png");
    public readonly static Bitmap Body = LoadImage("Body.png");
    public readonly static Bitmap Head = LoadImage("Head.png");
    public readonly static Bitmap Food = LoadImage("Food.png");
    public readonly static Bitmap DeadBody = LoadImage("DeadBody.png");
    public readonly static Bitmap DeadHead = LoadImage("DeadHead.png");
    
    private static Bitmap LoadImage(string fileName)
    {
        const string URL = "/Users/michalpozdal/Desktop/studia/3 rok/TO/projekt/SnakeGame/SnakeGame/Assets/";
        return new Bitmap($"{URL}{fileName}");
    }
}
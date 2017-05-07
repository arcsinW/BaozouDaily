using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace BaoZouRiBao.Views
{ 
    public sealed partial class DefaultPage : Page
    {
        public DefaultPage()
        {
            this.InitializeComponent();

            canvas.TargetElapsedTime = TimeSpan.FromMilliseconds(100);

            Unloaded += DefaultPage_Unloaded;
        }

        private void DefaultPage_Unloaded(object sender, RoutedEventArgs e)
        {
            canvas.RemoveFromVisualTree();
            canvas = null;
        }

        private CanvasBitmap bitmap;

        private List<Uri> Items = new List<Uri>();
        private int currentIndex = 0;

        async Task Canvas_CreateResourcesAsync(CanvasAnimatedControl sender)
        {
            bitmap = await CanvasBitmap.LoadAsync(sender, Items[currentIndex]);
        }

        private void CanvasAnimatedControl_Draw(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedDrawEventArgs args)
        {
            args.DrawingSession.Clear(Colors.Transparent);
            args.DrawingSession.DrawImage(bitmap);
            //args.DrawingSession.DrawImage(bitmap, new System.Numerics.Vector2(200));
            canvas.Invalidate();
        }

        private async void CanvasAnimatedControl_Update(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedUpdateEventArgs args)
        {
            bitmap = await CanvasBitmap.LoadAsync(sender, Items[currentIndex]);
            currentIndex++;
            if(currentIndex >= Items.Count)
            {
                currentIndex = 0;
            }
        }

        private void CanvasAnimatedControl_CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            for (int i = 0; i < 6; i++)
            {
                Items.Add(new Uri($"ms-appx:///Assets/Images/loadinganim{i}.png"));
            }
            args.TrackAsyncAction(Canvas_CreateResourcesAsync(sender).AsAsyncAction());
        }
    }
}

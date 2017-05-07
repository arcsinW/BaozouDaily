using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace BaoZouRiBao.Controls
{
    public sealed class GifAnimControl : Control
    {
        public GifAnimControl()
        {
            this.DefaultStyleKey = typeof(GifAnimControl);
        }

        private const string CanvasName = "animatedControl";

        private CanvasAnimatedControl canvas;
        private CanvasBitmap bitmap;

        private List<Uri> Items = new List<Uri>();
        private int currentIndex = 0;

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            canvas = (CanvasAnimatedControl)GetTemplateChild(CanvasName);
            canvas.TargetElapsedTime = TimeSpan.FromMilliseconds(100);
            canvas.CreateResources += Canvas_CreateResources;
            canvas.Draw += Canvas_Draw;
            canvas.Update += Canvas_Update;
        }

        async Task Canvas_CreateResourcesAsync(CanvasAnimatedControl sender)
        {
            bitmap = await CanvasBitmap.LoadAsync(sender, Items[currentIndex]);
        }

        private void Canvas_CreateResources(CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            for (int i = 0; i < 6; i++)
            {
                Items.Add(new Uri($"ms-appx:///Assets/Images/loadinganim{i}.png"));
            }
            args.TrackAsyncAction(Canvas_CreateResourcesAsync(sender).AsAsyncAction());
        }

        private async void Canvas_Update(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {
            bitmap = await CanvasBitmap.LoadAsync(sender, Items[currentIndex]);
            currentIndex = (currentIndex + 1 >= Items.Count) ? 0 : ++currentIndex;
        }

        private void Canvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            args.DrawingSession.Clear(Colors.White);
            args.DrawingSession.DrawImage(bitmap, new System.Numerics.Vector2(200));
            canvas.Invalidate();
        }
    }
}

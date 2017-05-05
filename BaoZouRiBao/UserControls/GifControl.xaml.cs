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

namespace BaoZouRiBao.UserControls
{
    public sealed partial class GifControl : UserControl
    {
        public GifControl()
        {
            this.InitializeComponent();
            animatedControl.TargetElapsedTime = TimeSpan.FromMilliseconds(100);
            Unloaded += GifControl_Unloaded;
        }

        private void GifControl_Unloaded(object sender, RoutedEventArgs e)
        {
            animatedControl.RemoveFromVisualTree();
            animatedControl = null;
        }

        private void animatedControl_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            args.DrawingSession.Clear(Colors.White);

            args.DrawingSession.DrawImage(bitmap, new System.Numerics.Vector2(200));
            animatedControl.Invalidate();
        }

        private void animatedControl_CreateResources(CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(Canvas_CreateResourcesAsync(sender).AsAsyncAction());
        }
          
        public List<Uri> Items
        {
            get { return (List<Uri>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(List<Uri>), typeof(GifControl), new PropertyMetadata(0));
         
        int i = 0;

        private async void animatedControl_Update(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {
            bitmap = await CanvasBitmap.LoadAsync(sender, Items[i]);
            i++;
            if (i >= Items.Count)
            {
                i = 0;
            }
        }

        CanvasBitmap bitmap;

        public async Task Canvas_CreateResourcesAsync(CanvasAnimatedControl sender)
        {
            bitmap = await CanvasBitmap.LoadAsync(sender, Items[i]);
        }
    }
}

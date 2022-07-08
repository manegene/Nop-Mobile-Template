using CoreGraphics;
using habahabamall.Controls;
using habahabamall.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomShadowFrame), typeof(FrameShadowRenderer))]

namespace habahabamall.iOS
{
    /// <summary>
    /// Customize the shadow effects of the Frame control in iOS to make the shadow effects looks similar to Android
    /// </summary>
    public class FrameShadowRenderer : FrameRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> element)
        {
            base.OnElementChanged(element);
            var customShadowFrame = (CustomShadowFrame)Element;
            if (customShadowFrame != null)
            {
                Layer.CornerRadius = customShadowFrame.Radius;
                Layer.ShadowOpacity = customShadowFrame.ShadowOpacity;
                Layer.ShadowOffset =
                    new CGSize(customShadowFrame.ShadowOffsetWidth, customShadowFrame.ShadowOffSetHeight);
                Layer.Bounds.Inset(customShadowFrame.BorderWidth, customShadowFrame.BorderWidth);
                Layer.BorderColor = customShadowFrame.CustomBorderColor.ToCGColor();
                Layer.BorderWidth = (float)customShadowFrame.BorderWidth;
            }
        }
    }
}
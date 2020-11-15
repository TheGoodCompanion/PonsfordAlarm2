using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SkiaSharp.Views.Forms;
using SkiaSharp;

namespace PonsfordAlarm2
{
    public partial class MainPage : ContentPage
    {
        private SKPath _path;
        float arcLength = 105;

        private DateTime _alarmDate;
        
        public MainPage()
        {
            InitializeComponent();
            _path = new SKPath();
            _alarmDate = GetNextAlarmDate();

            Device.StartTimer(TimeSpan.FromSeconds(1 / 60f), () =>
            {
                canvasView.InvalidateSurface();
                return true;
            });

            Device.StartTimer(TimeSpan.FromMilliseconds(1000), () =>
            {
                slider.TranslationX = -80;
                slider.TranslateTo(80, 0, 800, Easing.Linear);

                return true;
            });
        }

        private static DateTime GetNextAlarmDate()
        {
            DateTime today = DateTime.Today;
            DateTime possibleDate = new DateTime(today.Year, today.Month, today.Day, 20, 15, 00);

            if (DateTime.Now > possibleDate)
                return possibleDate.AddDays(1);
            return possibleDate;
        }

        private void canvas_PaintSurface(
            object sender, 
            SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            SKPaint strokePaint = GetPaintColor(SKPaintStyle.Stroke, null, 10, SKStrokeCap.Square);
            SKPaint dotPaint = GetPaintColor(SKPaintStyle.Fill, "#DE0469");
            SKPaint hrPaint = GetPaintColor(SKPaintStyle.Stroke, "#262626", 4, SKStrokeCap.Square);
            SKPaint blackPaint= GetPaintColor(SKPaintStyle.Fill, "#262626");

            SKPaint minPaint = GetPaintColor(SKPaintStyle.Stroke, "#DE0469", 2, SKStrokeCap.Square);
            SKPaint bgPaint = GetPaintColor(SKPaintStyle.Stroke, "#FFFFFF");

            canvas.Clear();

            SKRect arcRect = new SKRect(10, 10, info.Width - 10, info.Height - 10);
            SKRect bgRect = new SKRect(25, 25, info.Width - 25, info.Height - 25);

            canvas.DrawOval(bgRect, bgPaint);

            strokePaint.Shader = SKShader.CreateLinearGradient(
                new SKPoint(arcRect.Left, arcRect.Right),
                new SKPoint(arcRect.Right, arcRect.Left),
                new SKColor[] { SKColor.Parse("#DE0469"), SKColors.Transparent },
                new float[] { 0, 1 },
                SKShaderTileMode.Repeat);

            _path.ArcTo(arcRect, 45, arcLength, true);
            canvas.DrawPath(_path, strokePaint);

            canvas.Translate(info.Width / 2, info.Height / 2);
            canvas.Scale(info.Width / 200f);

            canvas.Save();
            canvas.RotateDegrees(240);
            canvas.DrawCircle(0, -75, 2, dotPaint);
            canvas.Restore();

            DateTime dateTime = DateTime.Now;

            //HourHand
            canvas.Save();
            canvas.RotateDegrees(30 * dateTime.Hour + (0.5f * dateTime.Minute) + (0.033f * dateTime.Second));
            canvas.DrawLine(0, 5, 0, -60, hrPaint);
            canvas.Restore();
            
            //MinuteHand
            canvas.Save();
            canvas.RotateDegrees(6 * dateTime.Minute + (0.1f * dateTime.Second));
            canvas.DrawLine(0, 20, 0, -90, hrPaint);
            canvas.Restore();
            
            //SecondHand
            canvas.Save();
            canvas.RotateDegrees(6 * dateTime.Second);
            canvas.DrawLine(0, 10, 0, -90, minPaint);
            canvas.Restore();

            canvas.DrawCircle(0, 0, 5, dotPaint);

            for(int angle = 0; angle < 360; angle += 6)
            {
                canvas.DrawCircle(0, -90, angle % 30 == 0 ? 1 : 0.5f, blackPaint);
                canvas.RotateDegrees(6);
            }

            secondsText.Text = dateTime.Second.ToString("00");
            timeText.Text = dateTime.ToString("hh:mm");
            periodText.Text = dateTime.Hour >= 12 ? "PM" : "AM";

            var alarmDiff = _alarmDate - dateTime;
            alarmText.Text = $"{alarmDiff.Hours}h {alarmDiff.Minutes}m until next alarm";
        }

        private SKPaint GetPaintColor(
            SKPaintStyle style, 
            string hexColor, 
            float strokeWidth = 0, 
            SKStrokeCap cap = SKStrokeCap.Round, 
            bool isAntialias = true)
        {
            return new SKPaint
            {
                Style = style,
                StrokeWidth = strokeWidth,
                Color = string.IsNullOrWhiteSpace(hexColor) ? SKColors.White : SKColor.Parse(hexColor),
                StrokeCap = cap,
                IsAntialias = isAntialias
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Felipecsl.GifImageViewLibrary;
using System.IO;
using System.Timers;

namespace PH01
{
    [Activity(Label = "Tom Jerry",MainLauncher = true, Icon = "@drawable/icon",Theme = "@style/Theme.AppCompat.Light.NoActionBar",  ConfigurationChanges = Android.Content.PM.ConfigChanges.ScreenSize | Android.Content.PM.ConfigChanges.Orientation, ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class SplashScreen : AppCompatActivity
    {
        private GifImageView gifImageView;
        private ProgressBar progressBar;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SplashScreen);
            gifImageView = FindViewById<GifImageView>(Resource.Id.gifImageView);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);

            Stream input = Assets.Open("mhcho.gif");
            byte[] bytes = ConvertFileToByteArray(input);
            gifImageView.SetBytes(bytes);
            gifImageView.StartAnimation();

            //Wait for 3 seconds and start new Activity
            Timer timer = new Timer();
            timer.Interval = 3000;
            timer.AutoReset = false;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            StartActivity(new Intent(this, typeof(MainActivity)));
        }

        private byte[] ConvertFileToByteArray(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                    ms.Write(buffer, 0, read);
                return ms.ToArray();
            }
            
        }
    }
}
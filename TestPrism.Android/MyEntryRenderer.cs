﻿using Android.Content;
using Android.Widget;
using TestPrism;
using TestPrism.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace TestPrism.Droid
{
    class MyEntryRenderer : EntryRenderer
    {
        public MyEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                //Control.SetTextColor(global::Android.Graphics.Color.LightGreen);
                //Control.SetBackgroundColor(global::Android.Graphics.Color.LightGreen);
                Control.Background = Context.GetDrawable(Resource.Drawable.EditTextStyle);
                Control.SetPadding(20, 20, 10, 20);
            }
        }
    }
}
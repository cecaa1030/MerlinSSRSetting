
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;


namespace MerlinSSRSettingAndroid
{
    [Activity(Label = "MerlinSSRSettingAndroid", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

            LinearLayout serverListView = FindViewById<LinearLayout>(Resource.Id.serverListView);
            
            LayoutInflater inflater = LayoutInflater.From(serverListView.Context);
            var entityView = inflater.Inflate(Resource.Layout.ServerEntity, null);            
            var entityServerName = entityView.FindViewById<TextView>(Resource.Id.textServerName);
            entityServerName.Text = "XXXX1";

            serverListView.AddView(entityView);
            
            entityView = inflater.Inflate(Resource.Layout.ServerEntity, null);
            entityServerName = entityView.FindViewById<TextView>(Resource.Id.textServerName);
            entityServerName.Text = "XXXX2";

            serverListView.AddView(entityView);
        }
    }
}


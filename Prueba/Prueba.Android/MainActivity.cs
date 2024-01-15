using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Content;
using System.Threading.Tasks;
using AndroidX.Core.App;
using static Prueba.HolaMundo;
using Xamarin.Forms;

namespace Prueba.Droid
{
    [Activity(Label = "Prueba", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            try
            {
                Xamarin.Essentials.Platform.Init(this, savedInstanceState);
                global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

                // Aquí registras la implementación de INotificationService
                DependencyService.Register<INotificationService, AndroidNotificationService>();
                DependencyService.Register<ICloseApp, CloseAppAndroid>();

                LoadApplication(new App());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en OnCreate: " + ex.Message);
            }
        }
        public class AndroidNotificationService : INotificationService
        {
            public Task ShowNotification(string title, string message)
            {
                // Lógica para mostrar notificaciones en Android
                // Puedes usar la clase NotificationCompat.Builder aquí
                // Asegúrate de devolver una tarea completada
                return Task.CompletedTask;
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public class CloseAppAndroid : ICloseApp
        {
            public void CloseApp()
            {
                // Cerrar la aplicación en Android
                Process.KillProcess(Process.MyPid());
            }
        }
        public async Task ShowNotification(string title, string message)
        {
            var context = Android.App.Application.Context; // Obtener el contexto de la aplicación directamente

            var intent = new Intent(context, typeof(MainActivity));
            var pendingIntent = PendingIntent.GetActivity(context, 0, intent, PendingIntentFlags.OneShot);

            string channelId = "channel_id";

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channelName = "Channel_Name";
                var channelDescription = "Channel_Description";
                var channel = new NotificationChannel(channelId, channelName, NotificationImportance.Default)
                {
                    Description = channelDescription
                };

                var notificationManager = (NotificationManager)context.GetSystemService(NotificationService);
                notificationManager.CreateNotificationChannel(channel);
            }

            var notificationBuilder = new NotificationCompat.Builder(context, channelId)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetSmallIcon(Resource.Mipmap.icon) // Reemplaza con el nombre real del recurso si es necesario
                .SetAutoCancel(true)
                .SetContentIntent(pendingIntent);

            var notificationManagerCompat = NotificationManagerCompat.From(context);
            notificationManagerCompat.Notify(0, notificationBuilder.Build());

            await Task.CompletedTask;
        }
        public static MainActivity Instance { get; private set; }



    }

}
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prueba
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HolaMundo : ContentPage
    {
        private const string ApiUrlEntrada = "https://app.ctrlit.cl/ctrl/dial/registrarweb/cEwcXjodiX?i=1&lat=&lng=&r=3{0}";
        private const string ApiUrlSalida = "https://app.ctrlit.cl/ctrl/dial/registrarweb/cEwcXjodiX?i=0&lat=&lng=&r=3{0}";
        private const string RutEntradaKey = "RutEntrada";
        private const string RutSalidaKey = "RutSalida";
        public HolaMundo()
        {
            InitializeComponent();
            CargarRutGuardado();
            // Configurar la tarea para ejecutarse todos los días a las 8 AM
            //ScheduleDailyTask();

        }

        public class CloseAppAndroid : ICloseApp
        {
            public void CloseApp()
            {
                // Cerrar la aplicación en Android
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }
        private async void OnEntradaClicked(object sender, EventArgs e)
        {
            string rutEntrada = entryRutEntrada.Text;
            if (ValidarRut(rutEntrada))
            {
                    if (!string.IsNullOrWhiteSpace(rutEntrada))
                {
                    // Guardar el RUT
                    Application.Current.Properties[RutEntradaKey] = rutEntrada;

                    string apiUrlEntrada = string.Format(ApiUrlEntrada, Uri.EscapeDataString(rutEntrada));

                    using (HttpClient client = new HttpClient())
                    {
                        try
                        {
                            string resultadoApi = await client.GetStringAsync(apiUrlEntrada);
                            labelResultadoEntrada.Text = "Respuesta de la API de Entrada: " + resultadoApi;
                            
                            // Configurar temporizador para limpiar el mensaje después de 7 segundos
                            Device.StartTimer(TimeSpan.FromSeconds(7), () =>
                            {
                                labelResultadoEntrada.Text = string.Empty;
                                return false; // El temporizador se detiene después de una ejecución
                            });
                            // Mostrar notificación
                            var notificationService = DependencyService.Get<INotificationService>();
                            if (notificationService != null)
                            {
                                await notificationService.ShowNotification("Título", "Mensaje de la notificación");
                            }
                            else
                            {
                                Console.WriteLine("INotificationService no está registrado.");
                            }

                            //DependencyService.Get<INotificationService>().ShowNotification("Ejecucion Corectaa Salida", "La tarea automática se ejecutó correctamente.");
                            //ScheduleDailyTask();
                        }
                        catch (Exception ex)
                        {
                            labelResultadoEntrada.Text = "Error al llamar a la API de Entrada: " + ex.Message;
                            Device.StartTimer(TimeSpan.FromSeconds(7), () =>
                            {
                                labelResultadoEntrada.Text = string.Empty;
                                return false; // El temporizador se detiene después de una ejecución
                            });
                        }
                    }
                }
                else
                {
                    labelResultadoEntrada.Text = "Por favor, ingrese un RUT de entrada.";
                    Device.StartTimer(TimeSpan.FromSeconds(7), () =>
                    {
                        labelResultadoEntrada.Text = string.Empty;
                        return false; // El temporizador se detiene después de una ejecución
                    });
                }
            }
            else
            {
                labelResultadoEntrada.Text = "RUT de entrada no válido. Por favor, ingrese un RUT válido.";
                Device.StartTimer(TimeSpan.FromSeconds(7), () =>
                {
                    labelResultadoEntrada.Text = string.Empty;
                    return false; // El temporizador se detiene después de una ejecución
                });
            }
        }

        private async void OnSalidaClicked(object sender, EventArgs e)
        {
            string rutSalida = entryRutSalida.Text;
            if (ValidarRut(rutSalida))
            {
                    if (!string.IsNullOrWhiteSpace(rutSalida))
                    {
                        // Guardar el RUT
                        Application.Current.Properties[RutSalidaKey] = rutSalida;

                        string apiUrlSalida = string.Format(ApiUrlSalida, Uri.EscapeDataString(rutSalida));

                        using (HttpClient client = new HttpClient())
                        {
                            try
                            {
                                string resultadoApi = await client.GetStringAsync(apiUrlSalida);
                                labelResultadoSalida.Text = "Respuesta de la API de Salida: " + resultadoApi;

                                // Configurar temporizador para limpiar el mensaje después de 7 segundos
                                Device.StartTimer(TimeSpan.FromSeconds(7), () =>
                                {
                                    labelResultadoSalida.Text = string.Empty;
                                    return false; // El temporizador se detiene después de una ejecución
                                    
                                });
                            // Mostrar notificación
                            var notificationService = DependencyService.Get<INotificationService>();
                            if (notificationService != null)
                            {
                                await notificationService.ShowNotification("Título", "Mensaje de la notificación");
                            }
                            else
                            {
                                Console.WriteLine("INotificationService no está registrado.");
                            }

                            //DependencyService.Get<INotificationService>().ShowNotification("Ejecucion Corectaa Salida", "La tarea automática se ejecutó correctamente.");
                            //ScheduleDailyTask();
                        }
                        catch (Exception ex)
                            {
                                labelResultadoSalida.Text = "Error al llamar a la API de Salida: " + ex.Message;
                            Device.StartTimer(TimeSpan.FromSeconds(7), () =>
                            {
                                labelResultadoSalida.Text = string.Empty;
                                return false; // El temporizador se detiene después de una ejecución
                            });
                        }
                        }
                    }
                    else
                    {
                        labelResultadoSalida.Text = "Por favor, ingrese un RUT de salida.";
                    Device.StartTimer(TimeSpan.FromSeconds(7), () =>
                    {
                        labelResultadoSalida.Text = string.Empty;
                        return false; // El temporizador se detiene después de una ejecución
                    });
                }
                }
            else
            {
                labelResultadoSalida.Text = "RUT de Salida no válido. Por favor, ingrese un RUT válido.";
                Device.StartTimer(TimeSpan.FromSeconds(7), () =>
                {
                    labelResultadoSalida.Text = string.Empty;
                    return false; // El temporizador se detiene después de una ejecución
                });
            }
            }

        private void OnCerrarClicked(object sender, EventArgs e)
        {
            CloseApp();
        }
        public interface ICloseApp
        {
            void CloseApp();
        }
        private void CloseApp()
        {
            // Dependiendo de la plataforma, puedes usar diferentes métodos para cerrar la aplicación

            // Para Xamarin.Forms, puedes usar DependencyService
            var closeAppService = DependencyService.Get<ICloseApp>();
            if (closeAppService != null)
            {
                closeAppService.CloseApp();
            }
            else
            {
                Console.WriteLine("ICloseApp no está registrado.");
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            // Guardar las propiedades antes de que la página se cierre
            Application.Current.SavePropertiesAsync();
        }
        private void CargarRutGuardado()
        {
            if (Application.Current.Properties.ContainsKey(RutEntradaKey))
            {
                string rutGuardado = Application.Current.Properties[RutEntradaKey].ToString();
                entryRutEntrada.Text = rutGuardado;
                entryRutSalida.Text = rutGuardado;
            }
        }
        public static bool ValidarRut(string rut)
        {

            bool validacion = false;
            try
            {
                rut = LimpiaRut(rut).Replace("-", "");
                int rutAux = int.Parse(rut.Substring(0, rut.Length - 1));
                char dv = char.Parse(rut.Substring(rut.Length - 1, 1));
                int m = 0, s = 1;
                for (; rutAux != 0; rutAux /= 10)
                {
                    s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
                }
                if (dv == (char)(s != 0 ? s + 47 : 75))
                {
                    validacion = true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return validacion;
        }
        public static string LimpiaRut(string rut)
        {
            return rut.ToUpper().Trim().Replace(".", "");
        }


 
        private void ScheduleDailyTask()
        {
            DateTime now = DateTime.Now;
            DateTime scheduledTime;

            try
            {
                 // Lunes a viernes Entrada
                if (now.DayOfWeek >= DayOfWeek.Monday && now.DayOfWeek <= DayOfWeek.Friday)
                {
                    scheduledTime = new DateTime(now.Year, now.Month, now.Day, 7, 55, 0);
                }

                // Verificar si hoy es viernes y ajustar la hora de salida
                if (now.DayOfWeek == DayOfWeek.Friday)
                {
                    scheduledTime = new DateTime(now.Year, now.Month, now.Day, 15, 0, 0);
                }
                else
                {
                    //Horario de salida de lunes a jueves
                    scheduledTime = new DateTime(now.Year, now.Month, now.Day, 17, 40, 0);
                }

                // Si la hora programada ya pasó, programar para el próximo día
                if (now > scheduledTime)
                    scheduledTime = scheduledTime.AddDays(1);

                TimeSpan timeUntilScheduled = scheduledTime - now;

                // Programar la tarea para ejecutarse a la misma hora todos los días
                Device.StartTimer(timeUntilScheduled, () =>
                {
                    try
                    {                    
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            // Lógica para ejecutar la tarea automática
                            // Puedes llamar a un método específico o iniciar una nueva página, según tus necesidades.

                            // Ejemplo: Ejecutar la lógica para la entrada
                            await HandleAutomaticEntry();
                        });

                        // Programar la tarea para el próximo día
                        scheduledTime = scheduledTime.AddDays(1);
                        timeUntilScheduled = scheduledTime - DateTime.Now;

                        return true; // El temporizador se reinicia para ejecuciones futuras
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error en Device.StartTimer: " + ex.Message);
                        return false; // Detener el temporizador para evitar problemas continuos
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en ScheduleDailyTask: " + ex.Message);
            }
        }

        private async Task HandleAutomaticExit()
        {
            string savedRutSalida = Application.Current.Properties.ContainsKey(RutSalidaKey) ?
                Application.Current.Properties[RutSalidaKey].ToString() : string.Empty;

            if (!string.IsNullOrWhiteSpace(savedRutSalida))
            {
                string apiUrlSalida = string.Format(ApiUrlSalida, Uri.EscapeDataString(savedRutSalida));

                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        string resultadoApiSalida = await client.GetStringAsync(apiUrlSalida);
                        // Puedes mostrar el resultado o manejarlo según sea necesario
                        Console.WriteLine("Respuesta de la API de Salida: " + resultadoApiSalida);
                        //DependencyService.Get<INotificationService>().ShowNotification("Tarea completada", "La tarea automática de marca de asistencia se ejecutó correctamente.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al llamar a la API de Salida: " + ex.Message);
                        Console.WriteLine("Error al ejecutar la tarea automática de marca de asistencia: " + ex.Message);
                    }
                }
            }
        }
        private async Task HandleAutomaticEntry()
        {
            // Lógica para la entrada automática
            // Puedes adaptar esta lógica según tus necesidades
            string savedRutEntrada = Application.Current.Properties.ContainsKey(RutEntradaKey) ?
                Application.Current.Properties[RutEntradaKey].ToString() : string.Empty;

            if (!string.IsNullOrWhiteSpace(savedRutEntrada))
            {
                string apiUrlEntrada = string.Format(ApiUrlEntrada, Uri.EscapeDataString(savedRutEntrada));

                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        string resultadoApi = await client.GetStringAsync(apiUrlEntrada);
                        // Puedes mostrar el resultado o manejarlo según sea necesario
                        Console.WriteLine("Respuesta de la API de Entrada: " + resultadoApi);
                        //DependencyService.Get<INotificationService>().ShowNotification("Tarea completada", "La tarea automática se ejecutó correctamente.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al llamar a la API de Entrada: " + ex.Message);
                        Console.WriteLine("Error al ejecutar la tarea automática: " + ex.Message);
                    }
                }
            }
        }
        public interface INotificationService
        {
            Task ShowNotification(string title, string message);
        }



    }

}
using Estekhareh.Services;
using Estekhareh.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Estekhareh
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<EstekharehDatabase>();
            DependencyService.Register<EstekharehStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            WarmDbAndCache();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            WarmDbAndCache();
        }

        private static void WarmDbAndCache()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                _ = Task.Run(async () =>
                {
                    var database = DependencyService.Get<IEstekharehDatabase>();
                    await Task.WhenAll(
                        database.GetSetting(),
                        database.GetTranslators()
                    );
                });

                return false;
            });
        }
    }
}

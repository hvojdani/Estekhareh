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
            InitDbAndCache();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            InitDbAndCache();
        }

        private static void InitDbAndCache()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                _ = Task.Run(async () =>
                {
                    EstekharehDatabase.Init();
                    var database = DependencyService.Get<IEstekharehDatabase>();
                    _ = await database.GetSetting();
                    _ = await database.GetTranslators();
                });

                return false;
            });
        }
    }
}

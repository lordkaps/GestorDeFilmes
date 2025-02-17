namespace GestorDeFilmes
{
    public partial class AppShell : Shell
    {
        public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();
        public AppShell()
        {
            InitializeComponent();
            //RegisterRoutes();
        }

        //Devido ao TabbedPage e FlyoutPag, fez necessario o uso do NavigationPage no lugar do Shell

        //void RegisterRoutes()
        //{
        //    Routes.Add(nameof(MainPage), typeof(MainPage));
        //    Routes.Add(nameof(DetalheFilmePage), typeof(DetalheFilmePage));

        //    foreach (KeyValuePair<string, Type> item in Routes)
        //        Routing.RegisterRoute(item.Key, item.Value);
        //}
    }
}

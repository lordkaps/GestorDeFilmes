using GestorDeFilmes.Core.Services;

namespace GestorDeFilmes
{
    public partial class MainPage : ContentPage
    {
        private readonly TMDbService _movieService;

        public MainPage()
        {
            InitializeComponent();
            _movieService = new TMDbService();
        }

        private async void OnSearchPressed(object sender, EventArgs e)
        {
            string query = SearchBarMovies.Text;
            if (!string.IsNullOrWhiteSpace(query))
            {
                var movies = await _movieService.PesquisaFilmesAsync(query);
                MoviesCollectionView.ItemsSource = movies;
            }
        }
    }

}

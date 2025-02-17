namespace GestorDeFilmes.Models
{
    public static class TMDbSettings
    {
        public const string UrlBase = "https://api.themoviedb.org/3/";
        public const string SessionUrl = "https://api.themoviedb.org/3/authentication/session/new";
        public const string AccountUrl = "https://api.themoviedb.org/3/account";
        public const string LogoutUrl = "https://api.themoviedb.org/3/authentication/session";
        public const string ApiKey = "5eb58c1a2aaa5a2dfe76e2563e53fe4a";
        public const string UrlListaPopular = "movie/popular?language=pt-BR&page=1";
        public const string BuscaFilme = "search/movie?query=";
        public const string PadraoDeBusca = "&include_adult=false&language=pt-BR&page=1";

        public const string Bearer = "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI1ZWI1OGMxYTJhYWE1YTJkZmU3NmUyNTYzZTUzZmU0YSIsIm5iZiI6MTczOTM5Mjc2NC40MzgwMDAyLCJzdWIiOiI2N2FkMDZmY2Q2M2U5ZGVlZWEzNmQ1OWQiLCJzY29wZXMiOlsiYXBpX3JlYWQiXSwidmVyc2lvbiI6MX0.Jl2shUN7T8SRHuJMopNrVsOkGnmvxW395vpuQVmYD10";
    }
}

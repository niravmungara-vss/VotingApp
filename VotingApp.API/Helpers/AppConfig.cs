namespace VotingApp.API.Helpers
{
    public class AppConfig
    {
        public ConnectionStrings ConnectionStrings { get; set; }
    }

    public class ConnectionStrings
    {
        public string SQLdb { get; set; }
    }
}

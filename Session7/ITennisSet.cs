namespace Session7
{
    public interface ITennisSet
    {
        void Start();
        bool HasWinner { get; }
        IPlayer Winner { get; }
        void GameWonBy(IPlayer player);
        string ScoreFor(IPlayer player);
    }
}
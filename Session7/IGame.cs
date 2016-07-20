namespace Session7
{
    public interface IGame
    {
        void Start();
        void PointScoredBy(IPlayer player);
        bool HasWinner { get; }
        IPlayer Winner { get; }
        string ScoreFor(IPlayer player);
    }
}
namespace Session7
{
    using System.Collections.Generic;
    using System.Linq;

    public class TennisSet : ITennisSet
    {
        private IEnumerable<IPlayer> players;

        private IDictionary<IPlayer, int> scores;

        public TennisSet(IEnumerable<IPlayer> players)
        {
            this.players = players;
        }

        public bool HasWinner { get; private set; }
        public IPlayer Winner { get; internal set; }

        public void GameWonBy(IPlayer player)
        {
            scores[player]++;

            CheckForWinner();
        }

        private void CheckForWinner()
        {
            var topScore = scores.Max(score => score.Value);

            if (topScore < 6)
                return;

            var topScoringPlayer = scores.First(score => score.Value == topScore).Key;

            if (topScore == 7 ||
                scores.Where(score => score.Key != topScoringPlayer).All(score => score.Value <= 4))
            {
                HasWinner = true;
                Winner = topScoringPlayer;
            }
        }

        public string ScoreFor(IPlayer player)
        {
            return scores == null ? " " : scores[player].ToString();
        }

        public void Start()
        {
            scores = players
                .Select(player => new { Player = player, Score = 0 })
                .ToDictionary(p => p.Player, p => p.Score);

            HasWinner = false;
            Winner = null;
        }
    }
}

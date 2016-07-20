namespace Session7
{
    using System.Collections.Generic;
    using System.Linq;

    public class Game : IGame
    {
        static class PointNames
        {
            public static int Zero = 0;
            public static int Fifteen = 1;
            public static int Thirty = 2;
            public static int Forty = 3;
            public static int Advantage = 4;
            public static int Win = 5;
        }

        static string[] gameScores = new[] { "0", "15", "30", "40", "A", "Win" };

        IEnumerable<IPlayer> players;

        IDictionary<IPlayer, int> playerScores;

        public Game(IEnumerable<IPlayer> players)
        {
            this.players = players;
        }

        public bool HasWinner { get; private set; }

        public IPlayer Winner { get; private set; }

        public void PointScoredBy(IPlayer player)
        {
            if (playerScores.All(score => score.Value == PointNames.Forty))
            {
                playerScores[player] = PointNames.Advantage;
                return;
            }

            var playerScore = playerScores[player];

            var otherPlayer = playerScores.First(p => p.Key != player).Key;
            var otherPlayerScore = playerScores[otherPlayer];

            if(otherPlayerScore == PointNames.Advantage)
            {
                playerScores[otherPlayer] = PointNames.Forty;
                return;
            }

            if (playerScore == PointNames.Forty)
                playerScores[player]++;

            playerScores[player]++;

            CheckForWinner();
        }

        private void CheckForWinner()
        {
            var highestScore = playerScores.Max(score => score.Value);

            if (highestScore != PointNames.Win)
                    return;

            var highestScoringPlayer = playerScores.First(score => score.Value == highestScore).Key;

            HasWinner = true;
            Winner = highestScoringPlayer;
        }

        public string ScoreFor(IPlayer player)
        {
            return gameScores[playerScores[player]];
        }

        public void Start()
        {
            playerScores = players
                .Select(player => new { Player = player, Score = 0 })
                .ToDictionary(player => player.Player, player => player.Score);

            HasWinner = false;
            Winner = null;
        }
    }
}
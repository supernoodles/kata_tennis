namespace Session7
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    class Scoreboard : IScoreboard
    {
        IConsole Console;
        IEnumerable<ITennisSet> sets;
        IEnumerable<IPlayer> players;
        IGame game;

        public Scoreboard(IConsole console, IEnumerable<ITennisSet> sets, IEnumerable<IPlayer> players, IGame game)
        {
            Console = console;
            this.sets = sets;
            this.players = players;
            this.game = game;
        }

        public void DisplayScore()
        {
            DisplayHeader();
            DisplayPlayerScores();
        }

        public void DisplayFinalScore(IPlayer winner)
        {
            DisplayHeader();
            DisplayPlayerScores(showGameScore: false, winner: winner);
        }

        private void DisplayPlayerScores(bool showGameScore = true, IPlayer winner = null)
        {
            var builder = new StringBuilder();

            foreach (var player in players)
            {
                builder.Append(player.ToString())
                    .Append(player == winner ? "*" : " ");
                
                foreach (var set in sets)
                {
                    builder.Append("| ").Append(set.ScoreFor(player)).Append(" ");
                }

                builder.Append("|");
                if (showGameScore)
                {
                    builder.Append(" ").Append(game.ScoreFor(player));
                }

                Console.Output(builder.ToString());

                builder.Clear();
            }
        }

        void DisplayHeader()
        {
            var builder = new StringBuilder();

            builder.Append("Player   ");

            int count = 1;
            foreach (var set in sets)
            {
                builder.Append("| ").Append(count).Append(" ");
                ++count;
            }

            builder.Append("| Game");

            Console.Output(builder.ToString());
            Console.Output(new string('-', 15 + 4 * sets.Count()));
        }
    }
}

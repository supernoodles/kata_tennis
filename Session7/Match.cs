namespace Session7
{
    using System.Collections.Generic;
    using System.Linq;

    public class Match
    {
        private IConsole console;
        private IScoreboard scoreboard;
        private IEnumerable<ITennisSet> tennisSets;
        private IGame game;
        IEnumerable<IPlayer> players;

        public Match(IConsole console, IScoreboard scoreboard, IEnumerable<ITennisSet> tennisSets, IGame game, IEnumerable<IPlayer> players)
        {
            this.console = console;
            this.scoreboard = scoreboard;
            this.tennisSets = tennisSets;
            this.game = game;
            this.players = players;
        }

        public void Start()
        {
            foreach(var set in tennisSets)
            {
                set.Start();

                while (!set.HasWinner)
                {
                    game.Start();

                    while (!game.HasWinner)
                    {
                        scoreboard.DisplayScore();

                        var pointWinner = int.Parse(console.Input("Point won by:"));

                        game.PointScoredBy(players.First(p => p.Number == pointWinner));
                    }

                    set.GameWonBy(game.Winner);
                }
            }
            scoreboard.DisplayFinalScore(GetWinner());
            console.Input("");
        }

        private IPlayer GetWinner()
        {
            return tennisSets
                .GroupBy(s => s.Winner)
                .OrderByDescending(g => g.Count())
                .First().Key;
        }
    }
}

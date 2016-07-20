namespace Session7
{
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            var players = new List<IPlayer>
            {
                new Player(1),
                new Player(2)
            };

            var console = new Console();

            var sets = new List<ITennisSet>
            {
                new TennisSet(players),
                new TennisSet(players),
                new TennisSet(players)
            };

            var game = new Game(players);

            var scoreboard = new Scoreboard(console, sets, players, game);

            var match = new Match(console, scoreboard, sets, game, players);

            match.Start();
        }
    }
}

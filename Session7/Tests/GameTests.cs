namespace Session7.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class GameTests
    {
        List<Mock<IPlayer>> playerMocks;

        Game game;

        IPlayer player1, player2;

        [SetUp]
        public void SetUp()
        {
            var player1Mock = new Mock<IPlayer>();
            var player2Mock = new Mock<IPlayer>();

            playerMocks = new List<Mock<IPlayer>> { player1Mock, player2Mock };

            player1 = player1Mock.Object;
            player2 = player2Mock.Object;

            game = new Game(playerMocks.Select(p => p.Object));
            game.Start();
        }

        [Test]
        public void NewGameShouldHaveZeroScoreForPlayer1()
        {
            Assert.That(game.ScoreFor(player1), Is.EqualTo("0"));
        }

        [Test]
        public void NewGameShouldHaveZeroScoreForAllPlayers()
        {
            Assert.That(game.ScoreFor(player1), Is.EqualTo("0"));
            Assert.That(game.ScoreFor(player2), Is.EqualTo("0"));
        }

        [TestCase(0, "0")]
        [TestCase(1, "15")]
        [TestCase(2, "30")]
        [TestCase(3, "40")]
        public void ReportCorrectScoreForPlayer1(int pointsWon, string score)
        {
            for (var i = 0; i < pointsWon; ++i)
                game.PointScoredBy(player1);

            Assert.That(game.ScoreFor(player1), Is.EqualTo(score));
        }

        [Test]
        public void Player1WinsGameWith4consecutivePoints()
        {
            for (var i = 0; i < 4; ++i)
                game.PointScoredBy(player1);

            Assert.That(game.HasWinner, Is.True);
            Assert.That(game.Winner, Is.EqualTo(player1));
        }

        [Test]
        public void IfBothPlayersOn40NextScoreIsAdvantage()
        {
            for (var i = 0; i < 3; ++i)
            {
                game.PointScoredBy(player1);
                game.PointScoredBy(player2);
            }

            game.PointScoredBy(player1);

            Assert.That(game.ScoreFor(player1), Is.EqualTo("A"));
            Assert.That(game.HasWinner, Is.False);
        }

        [Test]
        public void WhenOnePlayerOnAdvantageTheirNextScoreWins()
        {
            for (var i = 0; i < 3; ++i)
            {
                game.PointScoredBy(player1);
                game.PointScoredBy(player2);
            }

            game.PointScoredBy(player1);
            game.PointScoredBy(player1);

            Assert.That(game.HasWinner, Is.True);
            Assert.That(game.Winner, Is.EqualTo(player1));
        }

        [Test]
        public void IfPlayer1OnAdvantgePlayer2ScoreReinstatesDeuce()
        {
            for (var i = 0; i < 3; ++i)
            {
                game.PointScoredBy(player1);
                game.PointScoredBy(player2);
            }

            game.PointScoredBy(player1);
            game.PointScoredBy(player2);

            Assert.That(game.ScoreFor(player1), Is.EqualTo("40"));
            Assert.That(game.ScoreFor(player2), Is.EqualTo("40"));
            Assert.That(game.HasWinner, Is.False);

        }
    }
}

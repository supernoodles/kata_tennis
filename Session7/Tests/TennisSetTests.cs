namespace Session7.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class TennisSetTests
    {
        List<Mock<IPlayer>> playerMocks;

        TennisSet set;

        [SetUp]
        public void SetUp()
        {
            var player1Mock = new Mock<IPlayer>();
            var player2Mock = new Mock<IPlayer>();

            playerMocks = new List<Mock<IPlayer>> { player1Mock, player2Mock };

            set = new TennisSet(playerMocks.Select(p => p.Object));
            set.Start();
        }

        [Test]
        public void NewSetShouldHaveZeroScoreForPlayer1()
        {
            Assert.That(set.ScoreFor(playerMocks[0].Object), Is.EqualTo("0"));
        }

        [Test]
        public void NewSetShouldHaveZeroScoreForAllPlayers()
        {
            Assert.That(set.ScoreFor(playerMocks[0].Object), Is.EqualTo("0"));
            Assert.That(set.ScoreFor(playerMocks[1].Object), Is.EqualTo("0"));
        }

        [Test]
        public void SetRecordsPlayer1WinningFirstGame()
        {
            set.GameWonBy(playerMocks[0].Object);

            Assert.That(set.ScoreFor(playerMocks[0].Object), Is.EqualTo("1"));
            Assert.That(set.ScoreFor(playerMocks[1].Object), Is.EqualTo("0"));
        }

        [Test]
        public void SetInProgressHasNoWinner()
        {
            set.GameWonBy(playerMocks[0].Object);
            set.GameWonBy(playerMocks[1].Object);
            set.GameWonBy(playerMocks[1].Object);

            Assert.That(set.HasWinner, Is.False);
        }

        [Test]
        public void Player1IsWinnerAfterWinning6GamesToLove()
        {
            for (var i = 0; i < 6; i++)
            {
                set.GameWonBy(playerMocks[0].Object);
            }

            Assert.That(set.HasWinner, Is.True);
        }

        [TestCase(6, 4, true)]
        [TestCase(7, 5, true)]
        [TestCase(3, 2, false)]
        [TestCase(6, 5, false)]
        public void IsPlayer1TheWinner(int player1Score, int player2Score, bool isPlayer1Winner)
        {
            for (var i = 0; i < player2Score; ++i)
            {
                set.GameWonBy(playerMocks[1].Object);
            }

            for (var i = 0; i < player1Score; ++i)
            {
                set.GameWonBy(playerMocks[0].Object);
            }

            Assert.That(set.HasWinner, Is.EqualTo(isPlayer1Winner));

            if (isPlayer1Winner)
                Assert.That(set.Winner, Is.EqualTo(playerMocks[0].Object));
        }
    }
}

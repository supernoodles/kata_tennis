namespace Session7.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class ScoreboardTests
    {
        Mock<IConsole> consoleMock;
        List<Mock<ITennisSet>> setsMock;
        Mock<IGame> gameMock;
        List<Mock<IPlayer>> playersMock;
        IPlayer player1, player2;
        Scoreboard scoreboard;

        [SetUp]
        public void SetUp()
        {
            consoleMock = new Mock<IConsole>();

            var setMock1 = new Mock<ITennisSet>();
            var setMock2 = new Mock<ITennisSet>();
            var setMock3 = new Mock<ITennisSet>();

            setsMock = new List<Mock<ITennisSet>> { setMock1, setMock2, setMock3 };

            gameMock = new Mock<IGame>();

            var player1Mock = new Mock<IPlayer>();
            player1Mock.Setup(p => p.ToString()).Returns("Player 1");
            var player2Mock = new Mock<IPlayer>();
            player2Mock.Setup(p => p.ToString()).Returns("Player 2");

            playersMock = new List<Mock<IPlayer>> { player1Mock, player2Mock };

            player1 = player1Mock.Object;
            player2 = player2Mock.Object;

            gameMock = new Mock<IGame>();

            scoreboard = new Scoreboard(consoleMock.Object, 
                setsMock.Select(set => set.Object),
                playersMock.Select(p => p.Object),
                gameMock.Object);
        }

        [Test]
        public void ShouldShowInitialHeaderLine()
        {
            scoreboard.DisplayScore();

            consoleMock.Verify(cm => cm.Output("Player   | 1 | 2 | 3 | Game"));
            consoleMock.Verify(cm => cm.Output("---------------------------"));
        }

        [Test]
        public void ShouldShowInitialScoreForPlayer1()
        {
            setsMock[0].Setup(sm => sm.ScoreFor(player1)).Returns("0");
            setsMock[1].Setup(sm => sm.ScoreFor(player1)).Returns(" ");
            setsMock[2].Setup(sm => sm.ScoreFor(player1)).Returns(" ");

            gameMock.Setup(gm => gm.ScoreFor(player1)).Returns("0");

            scoreboard.DisplayScore();

            consoleMock.Verify(cm => cm.Output("Player 1 | 0 |   |   | 0"), Times.AtLeastOnce);
        }

        [Test]
        public void ShouldShowScoreForAllPlayers()
        {
            setsMock[0].Setup(sm => sm.ScoreFor(player1)).Returns("0");
            setsMock[0].Setup(sm => sm.ScoreFor(player2)).Returns("0");
            setsMock[1].Setup(sm => sm.ScoreFor(player1)).Returns(" ");
            setsMock[2].Setup(sm => sm.ScoreFor(player1)).Returns(" ");
            setsMock[1].Setup(sm => sm.ScoreFor(player2)).Returns(" ");
            setsMock[2].Setup(sm => sm.ScoreFor(player2)).Returns(" ");

            gameMock.Setup(gm => gm.ScoreFor(player1)).Returns("0");
            gameMock.Setup(gm => gm.ScoreFor(player2)).Returns("0");

            scoreboard.DisplayScore();

            consoleMock.Verify(cm => cm.Output("Player 1 | 0 |   |   | 0"));
            consoleMock.Verify(cm => cm.Output("Player 2 | 0 |   |   | 0"));
        }

        [Test]
        public void ShouldShowFinalScore()
        {
            setsMock[0].Setup(sm => sm.ScoreFor(player1)).Returns("7");
            setsMock[1].Setup(sm => sm.ScoreFor(player1)).Returns("4");
            setsMock[2].Setup(sm => sm.ScoreFor(player1)).Returns("6");
            setsMock[0].Setup(sm => sm.ScoreFor(player2)).Returns("5");
            setsMock[1].Setup(sm => sm.ScoreFor(player2)).Returns("6");
            setsMock[2].Setup(sm => sm.ScoreFor(player2)).Returns("4");
            gameMock.Setup(gm => gm.ScoreFor(player1)).Returns("Win");
            gameMock.Setup(gm => gm.ScoreFor(player2)).Returns("40");

            scoreboard.DisplayFinalScore(player1);

            consoleMock.Verify(cm => cm.Output("Player 1*| 7 | 4 | 6 |"));
            consoleMock.Verify(cm => cm.Output("Player 2 | 5 | 6 | 4 |"));
        }
    }
}

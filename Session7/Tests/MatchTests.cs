namespace Session7.Tests
{
    using NUnit.Framework;
    using Moq;
    using System.Collections.Generic;
    using System.Linq;

    [TestFixture]
    public class MatchTests
    {
        Mock<IScoreboard> scoreboardMock;
        Mock<IGame> gameMock;
        List<Mock<ITennisSet>> setsMock;
        Mock<IConsole> consoleMock;
        Mock<IPlayer> player1Mock, player2Mock;

        List<Mock<IPlayer>> playersMock;

        Session7.Match match;

        [SetUp]
        public void SetUp()
        {
            scoreboardMock = new Mock<IScoreboard>();

            player1Mock = new Mock<IPlayer>();
            player2Mock = new Mock<IPlayer>();
            player1Mock.Setup(p => p.Number).Returns(1);
            player2Mock.Setup(p => p.Number).Returns(2);

            playersMock = new List<Mock<IPlayer>> { player1Mock, player2Mock };

            var setMock1 = new Mock<ITennisSet>();
            var setMock2 = new Mock<ITennisSet>();
            var setMock3 = new Mock<ITennisSet>();

            setMock1.SetupSequence(s => s.HasWinner)
                .Returns(false)
                .Returns(true);
            setMock2.Setup(s => s.HasWinner).Returns(true);
            setMock3.Setup(s => s.HasWinner).Returns(true);
            setMock1.Setup(s => s.Winner).Returns(player1Mock.Object);
            setMock2.Setup(s => s.Winner).Returns(player2Mock.Object);
            setMock3.Setup(s => s.Winner).Returns(player1Mock.Object);

            setsMock = new List<Mock<ITennisSet>> { setMock1, setMock2, setMock3 };

            gameMock = new Mock<IGame>();

            gameMock.SetupSequence(g => g.HasWinner)
                .Returns(false)
                .Returns(true);

            gameMock.Setup(g => g.Winner).Returns(player1Mock.Object);

            consoleMock = new Mock<IConsole>();
            consoleMock.Setup(c => c.Input(It.IsAny<string>())).Returns("1");

            match = new Session7.Match(consoleMock.Object,
                scoreboardMock.Object,
                setsMock.Select(sm => sm.Object),
                gameMock.Object,
                playersMock.Select(p => p.Object));

            match.Start();
        }

        [Test]
        public void MatchShouldShowInitialScoreBoardWhenGameStarts()
        {

            scoreboardMock.Verify(sb => sb.DisplayScore());
        }

        [Test]
        public void MatchShouldStartFirstSet()
        {
            setsMock[0].Verify(s => s.Start(), Times.Once);
        }

        [Test]
        public void MatchShouldStartSecondSetWhenFirstSetWon()
        {
            setsMock[1].Verify(s => s.Start(), Times.Once);
        }

        [Test]
        public void MatchStartsFirstGame()
        {
            gameMock.Verify(g => g.Start(), Times.AtLeastOnce);
        }

        [Test]
        public void FirstGameAsksForInput()
        {
            consoleMock.Verify(c => c.Input("Point won by:"), Times.AtLeastOnce);
            gameMock.Verify(g => g.PointScoredBy(It.IsAny<IPlayer>()), Times.AtLeastOnce);
        }

        [Test]
        public void SetGetsUpdatedWithWinnerOfGame()
        {
            setsMock[0].Verify(s => s.GameWonBy(It.IsAny<IPlayer>()));
        }

        [Test]
        public void ShowsFinalScoreWhenMatchWon()
        {
            scoreboardMock.Verify(sb => sb.DisplayFinalScore(It.IsAny<IPlayer>()), Times.Once);
        }

        [Test]
        public void SelectsTheCorrectWinnerAtEnd()
        {
            scoreboardMock.Verify(sb => sb.DisplayFinalScore(player1Mock.Object), Times.Once);
        }
    }
}

namespace Session7
{
    public class Player : IPlayer
    {
        public Player(int number)
        {
            Number = number;
        }

        public int Number { get; private set; }

        public override string ToString()
        {
            return string.Format("Player {0}", Number);
        }
    }
}

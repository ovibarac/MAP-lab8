namespace MAP_lab8
{
    public class ActivePlayer<ID> :Entity<ID> 
    {
        private ID idPlayer;
        private ID idGame;
        private int nbPointsScored;
        private PlayerType type;

        public ActivePlayer(ID id, ID idPlayer, ID idGame, int nbPointsScored, PlayerType type) : base(id)
        {
            this.idPlayer = idPlayer;
            this.idGame = idGame;
            this.nbPointsScored = nbPointsScored;
            this.type = type;
        }

        public ID IdPlayer
        {
            get => idPlayer;
            set => idPlayer = value;
        }

        public ID IdGame
        {
            get => idGame;
            set => idGame = value;
        }

        public int NbPointsScored
        {
            get => nbPointsScored;
            set => nbPointsScored = value;
        }

        public PlayerType Type
        {
            get => type;
            set => type = value;
        }

        public override string ToString()
        {
            return Id + "," + IdPlayer.ToString() + "," + IdGame.ToString() + "," + NbPointsScored.ToString() + "," + Type.ToString();
        }
    }
}
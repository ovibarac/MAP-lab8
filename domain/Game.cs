using System;

namespace MAP_lab8
{
    public class Game<ID> : Entity<ID>
    {
        private Team<ID> team1;
        private Team<ID> team2;
        private DateTime date;

        public Game(ID id, Team<ID> team1, Team<ID> team2, DateTime date) : base(id)
        {
            this.team1 = team1;
            this.team2 = team2;
            this.date = date;
        }

        public Team<ID> Team1
        {
            get => team1;
            set => team1 = value;
        }

        public Team<ID> Team2
        {
            get => team2;
            set => team2 = value;
        }

        public DateTime Date
        {
            get => date;
            set => date = value;
        }

        public override string ToString()
        {
            return Id + ",(" + Team1.ToString() + "),(" + Team2.ToString() + ")," + Date;
        }
    }
}
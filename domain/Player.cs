using System;

namespace MAP_lab8
{
    public class Player<ID> : Student<ID>
    {
        private Team<ID> team;

        public Player(ID id, string name, String school, Team<ID> team) : base(id, name, school)
        {
            this.team = team;
        }

        public Team<ID> Team
        {
            get => team;
            set => team = value;
        }

        public override string ToString()
        {
            return base.ToString() + ",(" + Team.ToString() + ")";
        }
    }
}
using System;

namespace MAP_lab8
{
    public class Team<ID> : Entity<ID>
    {
        private String name;

        public string Name
        {
            get => name;
            set => name = value;
        }

        public Team(ID id, string name) : base(id)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return base.ToString() + "," + Name;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
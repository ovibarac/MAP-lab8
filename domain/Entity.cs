namespace MAP_lab8
{
    public class Entity<ID>
    {
        private ID id;

        public ID Id
        {
            get => id;
            set => id = value;
        }

        public Entity(ID id)
        {
            this.id = id;
        }

        public override string ToString()
        {
            return Id.ToString();
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
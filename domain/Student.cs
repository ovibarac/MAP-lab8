using System;

namespace MAP_lab8
{
    public class Student<ID> : Entity<ID>
    {
        private String name;
        private String school;

        public Student(ID id, string name, String school) : base(id)
        {
            this.name = name;
            this.school = school;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public String School
        {
            get => school;
            set => school = value;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString() + "," + Name + "," + School;
        }
    }
}
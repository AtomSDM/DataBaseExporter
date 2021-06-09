using System;

namespace ConsoleApp1.DataMaster
{
    public class StructElement
    {
        public readonly int Id;
        public readonly string Name;
        public readonly Type Type;

        public StructElement(int id, string name, Type type)
        {
            Id = id;
            Name = name;
            Type = type;
        }
    }
}
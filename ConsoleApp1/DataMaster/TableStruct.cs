using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1.DataMaster
{
    public class TableStruct
    {
        public readonly List<StructElement> _elements;
        public TableStruct(List<StructElement> elements) => _elements = elements;
        public StructElement this[string name]
        {
            get
            {
                return _elements.FirstOrDefault(e => e.Name == name);
            }
        }
        public StructElement this[int id] => _elements[id];

        public int Count() => _elements.Count;
        
        //ToDo: Rewrite method ToString
        //
        //public override string ToString();
    }
}
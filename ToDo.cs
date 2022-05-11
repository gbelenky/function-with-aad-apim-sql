using System;

namespace gbelenky.ToDo
{
    public class ToDo
    {        
        public Guid id { get; set; }
        public string title { get; set; }
        public bool? completed { get; set; }
    }
}
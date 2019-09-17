using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestPrism.Entities
{
    /// <summary>
    /// Друг
    /// </summary>
    public class Friend : RealmObject
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Pol Pol { get; set; }
    }
}

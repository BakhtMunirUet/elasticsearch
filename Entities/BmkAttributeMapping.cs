using System;
using Nest;

namespace API.Entities
{
    [ElasticsearchType(RelationName = "bmk_attribute_mapping")]
    public class BmkAttributeMapping
    {
        [Text(Name = "first_name")]
        public string FirstName { get; set; }

        [Text(Name = "last_name")]
        public string LastName { get; set; }

        [Number()]
        public int Salary { get; set; }

        [Date(Format = "MMddyyyy")]
        public DateTime Birthday { get; set; }

    }
}
using System;
using Microsoft.AspNetCore.Mvc;

namespace API.Entities
{
    public class NewYork
    {
        public int availability_365 { get; set; }
        public double latitude { get; set; }
        public string neighbourhood_group { get; set; }
        public int number_of_reviews { get; set; }
        public int host_id { get; set; }
        public int minimum_nights { get; set; }
        public string last_review { get; set; }
        public string neighbourhood { get; set; }
        public double price { get; set; }
        public double reviews_per_month { get; set; }
        public int calculated_host_listings_count { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public int id { get; set; }
        public string host_name { get; set; }
        public double longitude { get; set; }
        public string room_type { get; set; }

    }
}
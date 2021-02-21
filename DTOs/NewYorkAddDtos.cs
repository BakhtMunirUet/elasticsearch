using Nest;

namespace API.DTOs
{
    public class NewYorkAddDtos
    {
        public string neighbourhood { get; set; }
        public double price { get; set; }
        public string name { get; set; }
        public int id { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}
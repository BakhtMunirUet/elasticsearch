using System;
using System.ComponentModel.DataAnnotations;

namespace API.Helpers
{
    public class SortParams
    {
        private const int MaxPageSize = 50;
        public int SkipCount { get; set; } = 0;
        private int _pageSize = 10;

        public int MaxResultCount
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        [Required]
        public string SortParameter { get; set; }

        public string SortBy { get; set; } = "des";

    }
}
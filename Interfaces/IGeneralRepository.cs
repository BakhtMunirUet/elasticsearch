using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IGeneralRepository
    {
        Task<ActionResult<IEnumerable<NewYork>>> GetAllDataSort(SortParams sortParams);
        Task<ActionResult<IEnumerable<NewYork>>> GetAllDataFilter();
        Task<ActionResult<IEnumerable<NewYork>>> GetDataFuzzy(string searchText);
        Task<ActionResult<IEnumerable<NewYork>>> GetDataWildcard(string seachText);
        Task<ActionResult<IEnumerable<NewYork>>> GetDataMatchPhrasePrefix(string seachText);
    }
}
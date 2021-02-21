using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class GeneralController : BaseApiController
    {

        private readonly IMapper _mapper;
        private readonly IGeneralRepository _generalRepository;

        public GeneralController(IGeneralRepository generalRepository, IMapper mapper)
        {
            _generalRepository = generalRepository;
            _mapper = mapper;
        }

        [HttpGet("sort")]
        public async Task<ActionResult<IEnumerable<NewYorkSecondDtos>>> GetSortData([FromQuery] SortParams sortParams)
        {

            var newYork = await _generalRepository.GetAllDataSort(sortParams);
            if (newYork != null)
            {
                return _mapper.Map<List<NewYork>, List<NewYorkSecondDtos>>(newYork.Value.ToList());
            }
            return BadRequest("Oops!, request not executed successfully");
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<NewYorkSecondDtos>>> GetFilterData()
        {

            var newYork = await _generalRepository.GetAllDataFilter();
            if (newYork != null)
            {
                return _mapper.Map<List<NewYork>, List<NewYorkSecondDtos>>(newYork.Value.ToList());
            }
            return BadRequest("Oops!, request not executed successfully");
        }

        [HttpGet("fuzzy/{searchText}")]
        public async Task<ActionResult<IEnumerable<NewYorkSecondDtos>>> GetFuzzyData(string searchText)
        {

            var newYork = await _generalRepository.GetDataFuzzy(searchText);
            if (newYork != null)
            {
                return _mapper.Map<List<NewYork>, List<NewYorkSecondDtos>>(newYork.Value.ToList());
            }
            return BadRequest("Oops!, request not executed successfully");
        }

        [HttpGet("wildcard/{searchText}")]
        public async Task<ActionResult<IEnumerable<NewYorkSecondDtos>>> GetWildcardData(string searchText)
        {

            var newYork = await _generalRepository.GetDataWildcard(searchText);
            if (newYork != null)
            {
                return _mapper.Map<List<NewYork>, List<NewYorkSecondDtos>>(newYork.Value.ToList());
            }
            return BadRequest("Oops!, request not executed successfully");
        }

        [HttpGet("matchPhrasePrefix/{searchText}")]
        public async Task<ActionResult<IEnumerable<NewYorkSecondDtos>>> GetDataMatchPhrasePrefixData(string searchText)
        {

            var newYork = await _generalRepository.GetDataMatchPhrasePrefix(searchText);
            if (newYork != null)
            {
                return _mapper.Map<List<NewYork>, List<NewYorkSecondDtos>>(newYork.Value.ToList());
            }
            return BadRequest("Oops!, request not executed successfully");
        }
    }
}
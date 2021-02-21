using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace API.Controllers
{
    public class NewYorkController : BaseApiController
    {

        private readonly IMapper _mapper;
        private readonly INewYorkRepository _newYorkRepository;

        public NewYorkController(INewYorkRepository newYorkRepository, IMapper mapper)
        {
            _newYorkRepository = newYorkRepository;
            _mapper = mapper;
        }


        [HttpPost("addData")]
        public async Task<ActionResult> AddData([FromBody] NewYorkAddDtos newYorkAddDtos)
        {
            bool result = await _newYorkRepository.AddData(newYorkAddDtos);
            if (result)
            {
                return NoContent();
            }

            return BadRequest("Failed to add data");
        }

        [HttpPost("addManyBulk")]
        public async Task<ActionResult> AddManyBulk([FromBody] List<NewYorkAddDtos> newYorkList)
        {
            if (newYorkList.Count > 50)
            {
                return BadRequest("Number of documents will not be greater than 50 per request");
            }
            else
            {
                bool result = await _newYorkRepository.AddManyBulk(newYorkList);
                if (result)
                {
                    return NoContent();
                }

                return BadRequest("Failed to add data");
            }


        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewYorkSecondDtos>>> GetData([FromQuery] UserParams userParams)
        {
            return await _newYorkRepository.GetNewYorkAllDataAsync(userParams);
        }

        [HttpGet("searchByName")]
        public async Task<ActionResult<IEnumerable<NewYorkSecondDtos>>> GetSpecificData(String name)
        {
            return await _newYorkRepository.GetNewYorkSpecificDataAsync(name);
        }

        [HttpGet("findById")]
        public async Task<ActionResult<NewYorkSecondDtos>> GetFindById(int id)
        {


            var newYork = await _newYorkRepository.GetFindById(id);

            if (newYork.Value != null)
            {
                return _mapper.Map<NewYorkSecondDtos>(newYork.Value);
            }
            return BadRequest("Failed to find the object");


        }


        [HttpPut("updateData")]
        public async Task<ActionResult> UpdateData(NewYorkUpdateDtos newYorkUpdateDtos, int id)
        {
            bool result = await _newYorkRepository.UpdateData(newYorkUpdateDtos, id);
            if (result)
            {
                return NoContent();
            }

            return BadRequest("Failed to update data");
        }


        [HttpDelete("deleteData")]
        public async Task<ActionResult> DeleteData(int id)
        {
            bool result = await _newYorkRepository.DeleteData(id);
            if (result)
            {
                return NoContent();
            }

            return BadRequest("Failed to delete data");
        }

    }
}
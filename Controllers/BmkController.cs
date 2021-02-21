using System.Threading.Tasks;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace API.Controllers
{
    public class BmkController : BaseApiController
    {
        private readonly IBmkRepository _bmkRepository;
        public BmkController(IBmkRepository bmkRepository)
        {
            _bmkRepository = bmkRepository;
        }

        [HttpGet("CreateAttributeIndex")]

        public async Task<ActionResult<string>> CreateIndex()
        {

            var result = await _bmkRepository.CreateIndex();
            if (result.Value == IndexNames.Bmk)
            {
                return "Succefully Created Index name: " + result.Value;
            }
            else if (result.Value == "resource_already_exists_exception")
            {
                return BadRequest(result.Value);
            }

            return BadRequest("Oh!, failed to create index");


        }


        [HttpGet("CreateManualMappingIndex")]

        public async Task<ActionResult<string>> CreateManualIndex()
        {

            var result = await _bmkRepository.CreateManualIndex();
            if (result.Value == IndexNames.BmkManual)
            {
                return "Succefully Created Index name: " + result.Value;
            }
            else if (result.Value == "resource_already_exists_exception")
            {
                return BadRequest(result.Value);
            }

            return BadRequest("Oh!, failed to create index");


        }

        [HttpGet("CreateNGramIndex")]

        public async Task<ActionResult<string>> CreateNGramIndex()
        {

            var result = await _bmkRepository.CreateNGramsSettingIndex();
            if (result.Value == IndexNames.BmkNGramIndex)
            {
                return "Succefully Created Index name: " + result.Value;
            }
            else if (result.Value == "resource_already_exists_exception")
            {
                return BadRequest(result.Value);
            }

            return BadRequest("Oh!, failed to create index");


        }
    }
}
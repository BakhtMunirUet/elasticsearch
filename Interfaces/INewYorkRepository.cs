using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface INewYorkRepository
    {
        Task<bool> AddData(NewYorkAddDtos ewYorkAddDtos);

        Task<bool> AddManyBulk(List<NewYorkAddDtos> newYorkList);
        Task<ActionResult<IEnumerable<NewYorkSecondDtos>>> GetNewYorkAllDataAsync(UserParams userParams);
        Task<ActionResult<IEnumerable<NewYorkSecondDtos>>> GetNewYorkSpecificDataAsync(string name);
        Task<ActionResult<NewYork>> GetFindById(int id);
        Task<bool> UpdateData(NewYorkUpdateDtos newYorkUpdateDtos, int id);
        Task<bool> DeleteData(int id);

    }
}
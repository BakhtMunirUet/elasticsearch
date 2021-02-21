using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IBmkRepository
    {
        Task<ActionResult<String>> CreateIndex();
        Task<ActionResult<String>> CreateManualIndex();

        Task<ActionResult<String>> CreateNGramsSettingIndex();
    }
}
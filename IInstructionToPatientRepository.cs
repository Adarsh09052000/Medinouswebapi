using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Medinous.WebApi.Core.Dtos;

namespace Medinous.WebApi.Core.Interface
{
    public interface IInstructionToPatientRepository
    {
        Task<HttpCustomResponseMessage> SaveInstructionDetails(SaveInstructionRequest saveInstructionRequest);
        string GetInstructionsToPatient(string CostcenterCode);
        Task<List<InstructionsList>> GetInstructionToPatientAsync(string CostcenterCode);
        Task<HttpCustomResponseMessage> SaveInstructionMaster(PopulateInstructionMasterData PopulateInstructionMasterDataList);
        string GetInstructionMaster();
    }
}

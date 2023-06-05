using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medinous.WebApi.Core.Interface;
using Medinous.WebApi.Businesslogic.Contract;
using Medinous.WebApi.Businesslogic;
using Medinous.WebApi.Core.Dtos;
using System.Data;

namespace Medinous.WebApi.Infrastructure.Repository
{
    public class InstructionToPatientRepository : IInstructionToPatientRepository
    {
        public InstructionToPatient _InstructionToPatientRepository;

        public async Task<HttpCustomResponseMessage> SaveInstructionDetails(SaveInstructionRequest saveInstructionRequest)
        {
            _InstructionToPatientRepository = new InstructionToPatient();
            return await _InstructionToPatientRepository.SaveInstructionDetails(saveInstructionRequest);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CostcenterCode"></param>
        /// <param name="LocationCode"></param>
        /// <returns></returns>
        public string GetInstructionsToPatient(string CostcenterCode)
        {
            _InstructionToPatientRepository = new InstructionToPatient();
            string str = _InstructionToPatientRepository.GetInstructionsToPatient(CostcenterCode);
            return str;
        }
       public async Task<List<InstructionsList>> GetInstructionToPatientAsync(string CostcenterCode)
        { 
            _InstructionToPatientRepository = new InstructionToPatient();
            return await _InstructionToPatientRepository.GetInstructionToPatientAsync(CostcenterCode);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PopulateInstructionMasterDataList"></param>
        /// <returns></returns>
        public async Task<HttpCustomResponseMessage> SaveInstructionMaster(PopulateInstructionMasterData PopulateInstructionMasterDataList)
        {
            _InstructionToPatientRepository = new InstructionToPatient();
            return await _InstructionToPatientRepository.SaveInstructionMaster(PopulateInstructionMasterDataList);
        }
        public string GetInstructionMaster()
        {
            _InstructionToPatientRepository = new InstructionToPatient();
            string str = _InstructionToPatientRepository.GetInstructionMaster();
            return str;
        }
    }
}

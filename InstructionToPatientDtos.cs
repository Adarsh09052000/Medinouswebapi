using System;
using System.Collections.Generic;


namespace Medinous.WebApi.Core.Dtos
{
    public class SaveInstructionRequest : CommonDto
    {
        public decimal Code { get; set; }
        public string Description { get; set; }
        public string InActive { get; set; }
        public string Plan { get; set; }
        public string CostcenterCode { get; set; }

    }
    public class PopulateInstructionToPatientData
    {
        public List<InstructionsList> PopulateInstructionsList { get; set; }
    }
    public class InstructionsList
    {
        public string SINO { get; set; }
        public string CODE { get; set; }
        public string DESCRIPTION { get; set; }
        public string INACTIVE { get; set; }
        public string USERIDM { get; set; }
        public string PLAN { get; set; }
        public string COSTCENTER_CODE { get; set; }
    }
    public class InstructionMasterData : CommonDto
    {
        public string INSTRUCTION_ID { get; set; }
        public string TEMPLATE_NAME { get; set; }
        public string TYPE { get; set; }
        public string INACTIVE { get; set; }
        public string NATIVE_TEMPLATENAME { get; set; }
        public string USERIDC { get; set; }

    }
    public class PopulateInstructionMasterData
    {
        public List<InstructionMasterData> PopulateInstructionMasterDataList { get; set; }
    }
}

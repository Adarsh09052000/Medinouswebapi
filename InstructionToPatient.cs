using System;
using Nous.MedinousNG.Enterprise.Data;
using Medinous.WebApi.Core.Dtos;
using Oracle.ManagedDataAccess.Client;
using System.Threading.Tasks;
using System.Data;
using System.Collections.Generic;

namespace Medinous.WebApi.Businesslogic
{

    public class InstructionToPatient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="saveInstructionRequest"></param>
        /// <returns></returns>
        public async Task<HttpCustomResponseMessage> SaveInstructionDetails(SaveInstructionRequest saveInstructionRequest)
        {
            OracleCommand dbCommand = new OracleCommand();
            try
            {

                Database database = Database.Create("defaultConnectionString");

                dbCommand = database.CreateStoreCommand("STP_SAVEINSTRUCTIONSTOPATIENT");
                database.AddInputParameter(dbCommand, "pCODE", OracleDbType.Decimal, saveInstructionRequest.Code);
                database.AddInputParameter(dbCommand, "pCOSTCENTER_CODE", OracleDbType.Varchar2, saveInstructionRequest.CostcenterCode);
                database.AddInputParameter(dbCommand, "pDESCRIPTION", OracleDbType.Varchar2, saveInstructionRequest.Description);
                database.AddInputParameter(dbCommand, "pINACTIVE", OracleDbType.Varchar2, saveInstructionRequest.InActive);
                database.AddInputParameter(dbCommand, "pUSERIDM", OracleDbType.Varchar2, saveInstructionRequest.ModifiedUser);
                database.AddInputParameter(dbCommand, "pPLAN", OracleDbType.Varchar2, saveInstructionRequest.Plan);

                await Task.Run(() => database.ExecuteNonQuery(dbCommand));

                HttpCustomResponseMessage response = new HttpCustomResponseMessage()
                {
                    Message = "Success",
                    HttpCode = 200

                };

                return response;
            }

            catch (Exception ex)
            {
                HttpCustomResponseMessage response = new HttpCustomResponseMessage()
                {
                    HttpCode = 400,
                    Message = ex.Message
                };

                return response;
            }
            finally
            {
                if (dbCommand != null)
                {
                    if (dbCommand.Connection != null)
                    {
                        dbCommand.Connection.Close();
                    }
                    dbCommand.Dispose();
                }
            }
        }
       
        public async Task<List<InstructionsList>> GetInstructionToPatientAsync(string CostcenterCode)
        {
            OracleCommand dbCommand = new OracleCommand();
            try
            {
                Database database = Database.Create("defaultConnectionString");

                dbCommand = database.CreateStoreCommand("STP_GETPATIENTINSTRUCTIONS");
                database.AddInputParameter(dbCommand, "pCostcenterCode", OracleDbType.Decimal, CostcenterCode);
                database.AddOutputParameter(dbCommand, "Instructions_cursor", OracleDbType.RefCursor);

                DataSet dataSet =  database.ExecuteDataSet<DataSet>(dbCommand, new string[] { "PopulateInstructionsList" });

                List<InstructionsList> dtoList = await PopulateInstructionToPatientDTOFromDataSet(dataSet);

                return dtoList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbCommand != null)
                {
                    if (dbCommand.Connection != null)
                    {
                        dbCommand.Connection.Close();
                    }
                    dbCommand.Dispose();
                }
            }
        }
        public async Task<List<InstructionsList>> PopulateInstructionToPatientDTOFromDataSet(DataSet dataSet)
        {
            DataTable dataTable = dataSet.Tables["PopulateInstructionsList"];

            List<InstructionsList> dtoList = new List<InstructionsList>();

            foreach (DataRow row in dataTable.Rows)
            {

                string SlNo = row["SINO"].ToString();
                string code = row["CODE"].ToString();
                string Description = row["DESCRIPTION"].ToString();
                string InActive = row["INACTIVE"].ToString();
                string ModifiedUser = row["USERIDM"].ToString();
                string Plan = row["PLAN"].ToString();
                string CostCenterCode = row["COSTCENTER_CODE"].ToString();

                InstructionsList dto = new InstructionsList();
                {
                    dto.SINO = SlNo;
                    dto.CODE = code;
                    dto.DESCRIPTION = Description;
                    dto.INACTIVE = InActive;
                    dto.USERIDM = ModifiedUser;
                    dto.PLAN = Plan;
                    dto.COSTCENTER_CODE = CostCenterCode;
                }
                dtoList.Add(dto);
            }

            return dtoList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CostcenterCode"></param>
        /// <param name="LocationCode"></param>
        /// <returns></returns>
        public string GetInstructionsToPatient(string CostcenterCode)
        {
            OracleCommand dbCommand = new OracleCommand();
            try
            {
                Database database = Database.Create("defaultConnectionString");

                dbCommand = database.CreateStoreCommand("STP_GETPATIENTINSTRUCTIONS");
                database.AddInputParameter(dbCommand, "pCostcenterCode", OracleDbType.Decimal, CostcenterCode);
                database.AddOutputParameter(dbCommand, "Instructions_cursor", OracleDbType.RefCursor);

                string str = Newtonsoft.Json.JsonConvert.SerializeObject(database.ExecuteDataSetForJsonString<DataSet>(dbCommand, new string[] { "PopulateInstructionsList" }));

                return str;

            }
            catch (Exception ex)
            {
                HttpCustomResponseMessage response = new HttpCustomResponseMessage()
                {
                    Message = ex.Message,
                    HttpCode = 400
                };

                return Newtonsoft.Json.JsonConvert.SerializeObject(response);
            }
            finally
            {
                if (dbCommand != null)
                {
                    if (dbCommand.Connection != null)
                    {
                        dbCommand.Connection.Close();
                    }
                    dbCommand.Dispose();
                }
            }

        }
        public async Task<HttpCustomResponseMessage> SaveInstructionMaster(PopulateInstructionMasterData PopulateInstructionMasterDataList)
        {
            OracleCommand dbCommand = new OracleCommand();
            try
            {
                string primaryKey = "";

                Database database = Database.Create("defaultConnectionString");
                DataTable dtTable = new DataTable();
                dtTable = MedinousClass.ToDataTable(PopulateInstructionMasterDataList.PopulateInstructionMasterDataList);

                dbCommand = database.CreateStoreCommand("STP_SAVEINSTRUCTIONMASTER");

                database.AddInputParameter(dbCommand, "pInstructionId", OracleDbType.Varchar2, "INSTRUCTION_ID", DataRowVersion.Current);
                database.AddInputParameter(dbCommand, "pTemplateName", OracleDbType.Varchar2, "TEMPLATE_NAME", DataRowVersion.Current);
                database.AddInputParameter(dbCommand, "pType", OracleDbType.Varchar2, "TYPE", DataRowVersion.Current);
                database.AddInputParameter(dbCommand, "PUser", OracleDbType.Varchar2, "USERIDC", DataRowVersion.Current);
                database.AddInputParameter(dbCommand, "pInActive", OracleDbType.Varchar2, "INACTIVE", DataRowVersion.Current);
                database.AddInputParameter(dbCommand, "pNativeTemplateName", OracleDbType.Varchar2, "NATIVE_TEMPLATENAME", DataRowVersion.Current);
                database.AddOutputParameter(dbCommand, "vInstructionId", OracleDbType.Decimal, 10);

                await Task.Run(() => database.UpdateDataTable<DataTable>(dtTable, dbCommand, dbCommand, null));

                {
                    primaryKey = dbCommand.Parameters["vInstructionId"].Value.ToString();
                }

                HttpCustomResponseMessage response = new HttpCustomResponseMessage()
                {
                    Message = "Success",
                    HttpCode = 200,
                    PrimaryKey = primaryKey
                };

                return response;
            }

            catch (Exception ex)
            {
                HttpCustomResponseMessage response = new HttpCustomResponseMessage()
                {
                    HttpCode = 400,
                    Message = ex.Message
                };

                return response;
            }
            finally
            {
                if (dbCommand != null)
                {
                    if (dbCommand.Connection != null)
                    {
                        dbCommand.Connection.Close();
                    }
                    dbCommand.Dispose();
                }
            }


        }
        public string GetInstructionMaster()
        {
            OracleCommand dbCommand = new OracleCommand();
            try
            {
                Database database = Database.Create("defaultConnectionString");

                dbCommand = database.CreateStoreCommand("STP_GETINSTRUCTIONMASTER");
                database.AddOutputParameter(dbCommand, "pOutputCursor", OracleDbType.RefCursor);

                string str = Newtonsoft.Json.JsonConvert.SerializeObject(database.ExecuteDataSetForJsonString<DataSet>(dbCommand, new string[] { "PopulateInstructionMasterDataList" }));

                return str;

            }
            catch (Exception ex)
            {
                HttpCustomResponseMessage response = new HttpCustomResponseMessage()
                {
                    Message = ex.Message,
                    HttpCode = 400
                };

                return Newtonsoft.Json.JsonConvert.SerializeObject(response);
            }
            finally
            {
                if (dbCommand != null)
                {
                    if (dbCommand.Connection != null)
                    {
                        dbCommand.Connection.Close();
                    }
                    dbCommand.Dispose();
                }
            }
        }
    }        
}

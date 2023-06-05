using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Medinous.WebApi.Core.Interface;
using Medinous.WebApi.Infrastructure;
using Medinous.WebApi.Core.Dtos;
using Microsoft.Extensions.Configuration;
using Medinous.WebApi.Core.Dtos.DWB;
using Medinous.WebApi.Infrastructure.Repository;

namespace Medinous.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class InpatientListController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly IInpatientListRepository InpatientListRepository;   // interface class --> Infrastructure (service contract -->  service provider)
        private readonly IConfiguration configuration;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_InpatientListRepository"></param>
        /// <param name="_configuration"></param>
        public InpatientListController(IInpatientListRepository _InpatientListRepository, IConfiguration _configuration)
        {
            InpatientListRepository = _InpatientListRepository;
            configuration = _configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DoctorUser"></param>
        /// <param name="DeptCode"></param>
        /// <param name="LocationCode"></param>
        /// <param name="AllInpatients"></param>
        /// <returns></returns>
        [HttpGet("GetDoctorsInpatientList")] 
        public ActionResult GetDoctorsInpatientList(string DoctorUser, string DeptCode, string LocationCode, string AllInpatients)
        {
            var newdata = InpatientListRepository.GetDoctorsInpatientList(DoctorUser, DeptCode, LocationCode, AllInpatients);
            if (newdata.Contains("HttpCode") == false)
            {
                var listData = Newtonsoft.Json.JsonConvert.DeserializeObject<PopulatePendingIPListdData>(newdata);
                return Ok(listData);
            }
            else
            {
                return Ok(newdata);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DoctorCode"></param>
        /// <param name="LocationCode"></param>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        [HttpGet("GetIPPendingWithDoctor")] 
        public ActionResult GetIPPendingWithDoctor(string DoctorCode, string LocationCode, string FromDate, string ToDate, string Type)
        {
            var newdata = InpatientListRepository.GetIPPendingWithDoctor(DoctorCode, LocationCode, FromDate, ToDate, Type);
            if (newdata.Contains("HttpCode") == false)
            {
                var listData = Newtonsoft.Json.JsonConvert.DeserializeObject<PopulateDoctorIPPending>(newdata);
                return Ok(listData);
            }
            else
            {
                return Ok(newdata);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DoctorUser"></param>
        /// <returns></returns>
        [HttpGet("GetIPPendingActivityCount")]
        public IActionResult GetIPPendingActivityCount(string DoctorUser)
        {
            var newdata = InpatientListRepository.GetIPPendingActivityCount(DoctorUser);
            return Ok(newdata);
        }
        
    }
}

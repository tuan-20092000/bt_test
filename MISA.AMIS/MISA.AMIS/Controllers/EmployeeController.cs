using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Ifarstructures;
using MISA.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.AMIS.Controllers
{
    [Route("api/v1/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        IEmployeeRepository _employeeRepository;
        IEmployeeService _employeeService;
        public EmployeeController(IEmployeeRepository employeeRepository, IEmployeeService employeeService)
        {
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public IActionResult Get()
        {
            var employees = _employeeRepository.GetAll();
            if(employees.Count > 0)
            {
                return Ok(employees);
            }
            return NoContent();
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var employee = _employeeRepository.GetById(id);
            if(employee != null)
            {
                return Ok(employee);
            }
            return NoContent();
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
            var rowAffect = _employeeService.Insert(employee);
            if(rowAffect > 0)
            {
                return Ok(rowAffect);
            }
            return BadRequest();
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Employee employee)
        {
            var rowAffects = _employeeService.Update(employee);
            if(rowAffects > 0)
            {
                return Ok(rowAffects);
            }
            return BadRequest();
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var rowAffects = _employeeService.Delete(id);
            if(rowAffects > 0)
            {
                return Ok(rowAffects);
            }
            return BadRequest();
        }

        [HttpGet("search")]
        public IActionResult Search (string s)
        {
            var res = _employeeRepository.Search(s);
            if(res.Count() > 0)
            {
                return Ok(res);
            }
            return NoContent();
        }

        [HttpGet("paging")]
        public IActionResult Paging(int indexStart, int count)
        {
            var res = _employeeRepository.Paging(indexStart, count);
            if (res.Count() > 0)
            {
                return Ok(res);
            }
            return NoContent();
        }
    }
}

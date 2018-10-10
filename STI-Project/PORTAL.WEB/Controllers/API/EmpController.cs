using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PORTAL.DAL.EF;
using PORTAL.DAL.EF.Helper;
using PORTAL.DAL.EF.Models;


namespace PORTAL.WEB.Controllers.API
{
    [Produces("application/json")]

    public class EmpController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmpController(ApplicationDbContext context)
        {
            _context = context;
        }

        //[Route("api/Emp/{name}/{position}/{department}/{division}")]
        [Route("api/Employee/search/{search}")]
        public IActionResult GetEmployees(string search)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var employee = from s in _context.Employee
                           where (EF.Functions.Like(s.Employee_Name_English, "%" + search + "%") 
                           || EF.Functions.Like(s.Position, "%" + search + "%")
                           || EF.Functions.Like(s.Department, "%" + search + "%")
                           || EF.Functions.Like(s.Division, "%" + search + "%")
                           || EF.Functions.Like(s.Emp_ID, "%" + search + "%")) 
                           //&& EF.Functions.Equals(s.Termination_Date).Equals(null)
                           select s;
            if (employee == null)
            {
                return NotFound();
            }

            return new ObjectResult(employee);
        }

        [Route("api/Employee/byname/{name}")]
        public IActionResult GetEmployee(string name)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var employee = from s in _context.Employee
                           where EF.Functions.Like(s.Employee_Name_English, "%" + name + "%")
                           select s;
            if (employee == null)
            {
                return NotFound();
            }

            return new ObjectResult(employee);
        }

    }
}
using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using OnlineCatalog.Dtos;
using OnlineCatalog.Dtos.StudentDtos;
using OnlineCatalog.Utils;

namespace OnlineCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly DataAccesLayer _dataAccesLayer;

        public StudentsController(DataAccesLayer dataAccesLayer)
            => _dataAccesLayer = dataAccesLayer;

        /// <summary>
        /// Initialize the database
        /// </summary>
        [HttpPost("seed")]
        public void Seed() 
            => _dataAccesLayer.Seed();

        /// <summary>
        /// Extract all students from database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IEnumerable<StudentDto> GetAllStudents()
            => _dataAccesLayer.GetStudents().ToDto();

        /// <summary>
        /// Extract student by id parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public StudentDto? GetStudentById(int id) 
            => _dataAccesLayer.GetStudentById(id)?.ToDto();

        /// <summary>
        /// Create a student
        /// </summary>
        /// <param name="studentToCreate">stunde to create</param>
        /// <returns>created student</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public StudentDto? AddStudent([FromBody] StudentCreateDto studentToCreate) 
            => _dataAccesLayer.CreateStudent(studentToCreate.ToEntity())?.ToDto();


        /// <summary>
        /// Update student details
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPatch]
        public StudentDto? UpdateStudent(StudentUpdateDto student)
            => _dataAccesLayer.UpdateStudent(student.ToEntity())?.ToDto();

        /// <summary>
        /// Update student address
        /// </summary>
        /// <param name="id"></param>
        /// <param name="addressToupdate"></param>
        [HttpPut("{id}")]
        public void UpdateStudentAddress([FromRoute] int id, [FromBody] AddressToupdateDto addressToupdate)
            => _dataAccesLayer.UpdateStudentAddress(id,addressToupdate.ToEntity<StudentAddress>());
        
        [HttpDelete("{id}")]
        public void RemoveStudent(int id) 
            => _dataAccesLayer.RemoveStudent(id);
    }
}

using Data;
using Microsoft.AspNetCore.Mvc;
using OnlineCatalog.Dtos;
using OnlineCatalog.Utils;

namespace OnlineCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        /// <summary>
        /// Initialize the database
        /// </summary>
        [HttpPost("seed")]
        public void Seed() 
            => DataAccesLayerSingleton.Instance.Seed();

        /// <summary>
        /// Extract all students from database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<StudentTogetDto> GetAllStudents() 
            => DataAccesLayerSingleton.Instance.GetStudents().Select(s => s.TogetDto()).ToList();

        /// <summary>
        /// Extract student by id parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/id/{id}")]
        public StudentTogetDto? GetStudentById(int id) 
            => DataAccesLayerSingleton.Instance.GetStudentById(id)?.TogetDto();

        /// <summary>
        /// Create a student
        /// </summary>
        /// <param name="studentToCreate">stunde to create</param>
        /// <returns>created student</returns>
        [HttpPost]
        public StudentTogetDto? AddStudent([FromBody] StudentToCreateDto studentToCreate) 
            => DataAccesLayerSingleton.Instance.CreateStudent(studentToCreate.ToEntity())?.TogetDto();


        /// <summary>
        /// Update student details
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPatch]
        public StudentTogetDto? UpdateStudent(StudentToUpdateDto student)
            => DataAccesLayerSingleton.Instance.UpdateStudent(student.ToEntity())?.TogetDto();

        /// <summary>
        /// Update student address
        /// </summary>
        /// <param name="id"></param>
        /// <param name="addressToupdate"></param>
        [HttpPut("{id}")]
        public void UpdateStudentAddress([FromRoute] int id, [FromBody] AddressToupdateDto addressToupdate)
            => DataAccesLayerSingleton.Instance.UpdateStudentAddress(id,addressToupdate.ToEntity());
        
        [HttpDelete("{id}")]
        public void RemoveStudent(int id) 
            => DataAccesLayerSingleton.Instance.RemoveStudent(id);
    }
}

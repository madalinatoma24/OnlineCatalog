using Data;
using Microsoft.AspNetCore.Mvc;
using OnlineCatalog.Dtos.CourseDtos;
using OnlineCatalog.Utils;

namespace OnlineCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly DataAccesLayer _dataAccesLayer;

        public CoursesController(DataAccesLayer dataAccesLayer)
            => _dataAccesLayer = dataAccesLayer;


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]

        
        public IEnumerable<CourseDto> GeCourses()
           => _dataAccesLayer.GetCourses().ToDto();

        /// <summary>
        /// Extract course by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public CourseDto? GetCourseById(int id)
            => _dataAccesLayer.GetCourseById(id)?.ToDto();

       
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public CourseDto? AddCours([FromBody] CoursCreateDto courseToCreate)
            => _dataAccesLayer.CreateCourse(courseToCreate.ToEntity())?.ToDto();

        [HttpDelete("coursId")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public void DeleteCourse(int courseId)
            => _dataAccesLayer.RemoveCourse(courseId);

    }
}

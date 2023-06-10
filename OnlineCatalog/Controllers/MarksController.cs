using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using OnlineCatalog.Dtos.MarkDtos;
using OnlineCatalog.Dtos.StudentDtos;
using OnlineCatalog.Utils;

namespace OnlineCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarksController : ControllerBase
    {
        private readonly DataAccesLayer _dataAccesLayer;

        public MarksController(DataAccesLayer dataAccesLayer)
            => _dataAccesLayer = dataAccesLayer;


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IEnumerable<MarkDto> GetMarks()
           => _dataAccesLayer.GetMarks().ToDto();

        [HttpGet("{studentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IEnumerable<MarkDto>? GetMarksByStudentId(int studentId)
            => _dataAccesLayer.GetMarksByStudentId(studentId).ToDto();

        [HttpGet("{studentId}/{coursId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IEnumerable<MarkDto>? GetMarksByCourseId(int studentId, int coursId)
           => _dataAccesLayer.GetMarksByStudentIdAndCourseId(studentId, coursId)?.ToDto();

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public MarkDto? AddMarks([FromBody] MarkCreateDto markCreate)
           => _dataAccesLayer.AddMark(markCreate.ToEntity())?.ToDto();

        [HttpGet("{studentId}/{coursId}/average")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public double GetAverageStudentCoursMarks(int studentId, int coursId)
            => _dataAccesLayer.GetAverageStudentCoursMarks(studentId, coursId);

        [HttpGet("average")]
        public IEnumerable<StudentMarksAverageDto> GetStudentOrderByMarksAverage([FromQuery] SortEnum sort)
            => _dataAccesLayer.GetStudentOrderByMarksAverage((student, average) => student.ToAverageDto(average), sort);
    }
}

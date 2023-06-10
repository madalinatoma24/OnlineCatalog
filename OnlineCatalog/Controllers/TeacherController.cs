using Data;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCatalog.Dtos;
using OnlineCatalog.Dtos.MarkDtos;
using OnlineCatalog.Dtos.TeacherDtos;
using OnlineCatalog.Utils;

namespace OnlineCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly DataAccesLayer _dataAccesLayer;

        public TeacherController(DataAccesLayer dataAccesLayer)
            => _dataAccesLayer = dataAccesLayer;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public TeacherDto AddTeacher(TeacherCreateDto teacher)
            => _dataAccesLayer.AddTeacher(teacher.ToEntity()).ToDto();


        [HttpGet("{id}/marks")]
        public IEnumerable<MarkDto> GetTeacherGivenMarks([FromRoute] int id)
            => _dataAccesLayer.GetTeacherGivenMarks(id).ToDto();


        [HttpGet("{id}")]
        public TeacherDto? GetTeacherById([FromRoute] int id)
            => _dataAccesLayer.GetTeacherById(id).ToDto();

        [HttpPut("{id}")]
        public void UpdateTeacherAddress([FromRoute] int id, AddressToupdateDto address)
            => _dataAccesLayer.UpdateTeacherAddress(id, address.ToTeacherEntity());

        [HttpPut("{id}/{courseId}")]
        public void AssignTeacherToCourse([FromRoute] int id, [FromRoute] int courseId)
            => _dataAccesLayer.AssignTeacherToCourse(id, courseId);

        [HttpPost("{id}/_promote")]
        public void PromoteTeacher([FromRoute] int id)
            => _dataAccesLayer.PromoteTeacher(id);

        [HttpDelete("{id}")]
        public void RemoveTeacher([FromRoute] int id)
            => _dataAccesLayer.RemoveTeacher(id);

    }
}

using DevStudyNotes.API.Entities;
using DevStudyNotes.API.Models;
using DevStudyNotes.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevStudyNotes.API.Controllers
{
    [ApiController]
    [Route("api/study-notes")]
    public class StudyNotesController : ControllerBase
    {
        private readonly StudyNoteDbContext _context;

        public StudyNotesController(StudyNoteDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var studyNotes = _context.StudyNotes.ToList();   

            return Ok(studyNotes);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var studyNote = _context.StudyNotes
                .Include(s => s.StudyNoteReactions)
                .FirstOrDefault(x => x.Id == id);

            if (studyNote == null)
            {
                return NotFound();
            }

            return Ok(studyNote);
        }

        [HttpPost]
        public IActionResult Post(AddStudyNotesInputModel model)
        {
            var studyNote = new StudyNote(model.Title, model.Description, model.IsPublic);

            _context.StudyNotes.Add(studyNote);
            _context.SaveChanges();

            return CreatedAtAction("GetById", new { id = studyNote.Id }, model);
        }

        [HttpPost("{id}/reactions")]
        public IActionResult PostReaction(int id, AddReactionsStudyInputModel model)
        {
            var studyNotes = _context.StudyNotes.SingleOrDefault(s => s.Id == id);

            if (studyNotes == null)
            {
                return BadRequest();
            }

            studyNotes.AddReaction(model.IsPositive);

            _context.SaveChanges();

            return NoContent();
        }
    }
}

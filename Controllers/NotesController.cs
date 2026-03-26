using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using notes_api_app.app.Data;
using notes_api_app.app.DTOs;
using notes_api_app.app.Models;

namespace notes_api_app.app.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NotesController : ControllerBase
{
    private readonly AppDbContext _context;

    public NotesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<Note>> CreateNote(CreateNoteDto dto)
    {
        var note = new Note
        {
            Title = dto.Title,
            Content = dto.Content,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Notes.Add(note);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetNoteById), new { id = note.Id }, note);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
    {
        var notes = await _context.Notes
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();

        return Ok(notes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Note>> GetNoteById(int id)
    {
        var note = await _context.Notes.FindAsync(id);

        if (note == null)
        {
            return NotFound(new { message = "Note not found" });
        }

        return Ok(note);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateNote(int id, UpdateNoteDto dto)
    {
        var note = await _context.Notes.FindAsync(id);

        if (note == null)
        {
            return NotFound(new { message = "Note not found" });
        }

        note.Title = dto.Title;
        note.Content = dto.Content;
        note.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteNote(int id)
    {
        var note = await _context.Notes.FindAsync(id);

        if (note == null)
        {
            return NotFound(new { message = "Note not found" });
        }

        _context.Notes.Remove(note);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
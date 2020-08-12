using ClassRoomManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassRoomManager.Repositories
{
    public interface INoteData
    {
        IEnumerable<Note> GetNotesByGroupId(int groupId);
        IEnumerable<Note> GetAllNotes();
        int AddNote(Note note);
        Note GetNoteById(int noteId);
        void UpdateNote(Note note);
        IEnumerable<Note> GetNotesByStudentId(int studentId);
    }
}

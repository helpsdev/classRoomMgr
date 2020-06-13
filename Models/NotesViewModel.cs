using System.Collections.Generic;
using ClassRoomManager.Models;

namespace ClassRoomManager.Models
{
    public class NotesViewModel
    {
        public IEnumerable<Note> Notes { get; set; }
        public int GroupId { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassRoomManager.Models
{
    public class Note
    {
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset ModificationDate { get; set; }
        public Student Student { get; set; }
        public string Description { get; set; }
        public int NoteId { get; set; }
    }
}

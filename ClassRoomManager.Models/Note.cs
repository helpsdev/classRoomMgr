using System;
using System.Collections.Generic;
using System.Text;

namespace ClassRoomManager.Models
{
    public class Note
    {
        public int GroupId { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
        public int StudentId { get; set; }
        public string Description { get; set; }
        public int NoteId { get; set; }
    }
}

using System.Collections.Generic;
using ClassRoomManager.Models;

namespace ClassRoomManager.Models
{
    public class CreateNoteViewModel
    {
        public IEnumerable<Student> Students { get; }
        public int GroupId { get; }

        public CreateNoteViewModel(IEnumerable<Student> students, int groupId)
        {
            Students = students;
            GroupId = groupId;
        }
    }
}
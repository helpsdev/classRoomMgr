using ClassRoomManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassRoomManager.Repositories
{
    public interface IClassRoomManagerData
    {
        IEnumerable<Group> GetAllGroups();
        Group GetGroupById(int groupId);
        IEnumerable<Student> GetStudentsByGroupId(int groupId);
        Student GetStudentById(int studentId);
        int AddTeam(Team team);
        Team GetTeamById(int teamId);
        IEnumerable<Team> GetTeamsByGroupId(int groupId);
        IEnumerable<Note> GetNotesByGroupId(int groupId);
        IEnumerable<Note> GetAllNotes();
    }
}

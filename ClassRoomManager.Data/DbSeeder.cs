using ClassRoomManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassRoomManager.Repositories
{
    public static class DbSeeder
    {
        
        public static void Seed(ClassRoomManagerContext classRoomManagerContext)
        {
            classRoomManagerContext.Database.Migrate();

            if (classRoomManagerContext.Groups.Any()) return; //DB has data

            var testGroups = new List<Group>
            {
                new Group()
                {
                    Name = "Grupo A"
                },
                new Group()
                {
                    Name = "Grupo B"
                },
                new Group()
                {
                    Name = "Grupo C"
                }
            };

            classRoomManagerContext.Groups.AddRange(testGroups);

            var testStudents = new List<Student>
            {
                new Student()
                {
                    Group = testGroups[0],
                    ListNumber = 1,
                    Name = "Edwin Perez"
                },
                new Student()
                {
                    Group = testGroups[0],
                    ListNumber = 1,
                    Name = "Alicia Paredes"
                },
                new Student()
                {
                    Group = testGroups[0],
                    ListNumber = 1,
                    Name = "Lorena Aragon"
                },
                new Student()
                {
                    Group = testGroups[1],
                    ListNumber = 1,
                    Name = "Ramona Macias"
                },
                new Student()
                {
                    Group = testGroups[1],
                    ListNumber = 1,
                    Name = "Yahir Aragon"
                },
                new Student()
                {
                    Group = testGroups[1],
                    ListNumber = 1,
                    Name = "Jesus Aragon"
                },
                new Student()
                {
                    Group = testGroups[2],
                    ListNumber = 1,
                    Name = "Seidy Perez"
                },
                new Student()
                {
                    Group = testGroups[2],
                    ListNumber = 1,
                    Name = "Elian Perez"
                }
            };

            classRoomManagerContext.Students.AddRange(testStudents);
            classRoomManagerContext.SaveChanges();
        }
    }
}

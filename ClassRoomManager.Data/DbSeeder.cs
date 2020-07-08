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

            var groups = new List<Group>
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

            classRoomManagerContext.Groups.AddRange(groups);
            classRoomManagerContext.SaveChanges();
        }
    }
}

using ClassRoomManager.Controllers;
using ClassRoomManager.Models;
using ClassRoomManager.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ClassRoomManager.Tests
{
    [TestClass]
    public class AdminControllerTest
    {
        [TestMethod]
        public void EditPeriod_ReturnsAViewResult_WithAPreriodAsModel()
        {
            //Arrange
            var fakePeriodData = new FakePeriodData();
            var controller = new AdminController(fakePeriodData, null);
            //Act
            var result = controller.EditPeriod(periodId: 1, saveErrors: false);
            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType((result as ViewResult).Model, typeof(Period));
        }

        [TestMethod]
        public void EditPeriod_ReturnsAViewResult_WithViewDataErrorMessageVariableSet()
        {
            //Arrange
            var fakePeriodData = new FakePeriodData();
            var controller = new AdminController(fakePeriodData, null);
            //Act
            var result = controller.EditPeriod(periodId: 1, saveErrors: true);
            //Assert
            Assert.AreEqual("Hubo un error al guardar la información. Intenta de nuevo, si el problema persiste llama al administrador del sitio.", 
                (result as ViewResult).ViewData["ErrorMessage"].ToString());
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }

    internal class FakePeriodData : IPeriodData
    {
        public int AddPeriod(Period period)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Period> GetAllPeriods()
        {
            throw new System.NotImplementedException();
        }

        public Period GetPeriodById(int periodId)
        {
            return new Period
            {
                PeriodId = 1,
                StartDate = new DateTime(2020,08,01,0,0,0),
                EndDate = new DateTime(2020, 08, 31, 0, 0, 0)
            };
        }

        public int UpdatePeriod(Period period)
        {
            throw new System.NotImplementedException();
        }
    }
}

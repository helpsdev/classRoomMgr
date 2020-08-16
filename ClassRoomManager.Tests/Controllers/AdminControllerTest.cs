using ClassRoomManager.Controllers;
using ClassRoomManager.Models;
using ClassRoomManager.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [TestMethod]
        public void EditPeriod_ReturnARedirectToActionResult_WhenDbUpdateExceptionOccurs()
        {
            //Arrange
            var fakePeriodData = new FakePeriodData();
            var controller = new AdminController(fakePeriodData, null);
            var period = new Period { PeriodId = 1 };
            //Act
            var result = controller.EditPeriod(period);
            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual(nameof(controller.EditPeriod), (result as RedirectToActionResult).ActionName);
            int periodIdRouteValue = int.Parse((result as RedirectToActionResult).RouteValues["PeriodId"].ToString());
            Assert.AreEqual(1, periodIdRouteValue);
            bool saveErrorsRouteValue = bool.Parse((result as RedirectToActionResult).RouteValues["saveErrors"].ToString());
            Assert.IsTrue(saveErrorsRouteValue);
        }

        [TestMethod]
        public void EditPeriod_ReturnAViewResult_WithPeriodAsModel_WhenUpdateIsSuccess()
        {
            //Arrange
            var fakePeriodData2 = new FakePeriodData2();
            var controller = new AdminController(fakePeriodData2, null);
            var testPeriod = new Period
            {
                PeriodId = 1,
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now
            };
            //Act
            var result = controller.EditPeriod(testPeriod);
            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType(((ViewResult)result).Model, typeof(Period));
            Assert.AreEqual(testPeriod, ((ViewResult)result).Model);
        }

        [TestMethod]
        public void DeletePeriod_ReturnsAViewResult_WithPeriodAsModel_WhenSaveErrorsIsFalse()
        {
            //Arrange
            var fakePeriodData = new FakePeriodData();
            var controller = new AdminController(fakePeriodData, null);
            var periodIdToDelete = 1;
            //Act
            var result = controller.Delete(periodIdToDelete, saveErrors:false);
            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType(((ViewResult)result).Model, typeof(Period));
        }

        [TestMethod]
        public void DeletePeriod_ReturnsAViewResult_WithPeriodAsModelAndErrorMessageSet_WhenSaveErrorIsTrue()
        {
            //Arrange
            var fakePeriodData = new FakePeriodData();
            var controller = new AdminController(fakePeriodData, null);
            var periodIdToDelete = 1;
            //Act
            var result = controller.Delete(periodIdToDelete, saveErrors: true);
            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual("Hubo un error al guardar la información. Intenta de nuevo, si el problema persiste llama al administrador del sitio.",
                ((ViewResult)result).ViewData["ErrorMessage"].ToString());
        }

        [TestMethod]
        public void DeletePeriod_ReturnsNotFoundResult_WhenPeriodNotExist()
        {
            //Arrange
            var fakePeriodData2 = new FakePeriodData2();
            var controller = new AdminController(fakePeriodData2, null);
            var periodIdToDelete = 1;
            //Act
            var result = controller.Delete(periodIdToDelete);
            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeletePeriod_ReturnsRedirectToActionResult_WhenSuccessfullyDeletePeriod()
        {
            //Arrange
            var fakePeriodData = new FakePeriodData();
            var controller = new AdminController(fakePeriodData, null);
            var periodToDelete = 1;
            //Act
            var result = controller.Delete(periodToDelete);
            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual(nameof(controller.PeriodsList), ((RedirectToActionResult)result).ActionName);
        }

        [TestMethod]
        public void DeletePeriod_ReturnsRedirectToActionResult_WhenDbUpdateExceptionOccurrs()
        {
            //Arrange
            var fakePeriodData2 = new FakePeriodData2();
            var controller = new AdminController(fakePeriodData2, null);
            var periodToDelete = 1;
            //Act
            var result = controller.Delete(periodToDelete);
            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual(nameof(controller.Delete), ((RedirectToActionResult)result).ActionName);
            int perioIdRouteValue = int.Parse(((RedirectToActionResult)result).RouteValues["periodId"].ToString());
            Assert.AreEqual(1, perioIdRouteValue);
            bool saveErrorsRouteValue = bool.Parse(((RedirectToActionResult)result).RouteValues["saveErrors"].ToString());
            Assert.IsTrue(saveErrorsRouteValue);
        }
    }
    internal class FakePeriodData2 : IPeriodData
    {
        public int AddPeriod(Period period)
        {
            throw new NotImplementedException();
        }

        public int DeletePeriod(Period period)
        {
            throw new DbUpdateException();
        }

        public IEnumerable<Period> GetAllPeriods()
        {
            throw new NotImplementedException();
        }

        public Period GetPeriodById(int periodId)
        {
            return null;
        }

        public int UpdatePeriod(Period period)
        {
            return 1;
        }
    }

    internal class FakePeriodData : IPeriodData
    {
        public int AddPeriod(Period period)
        {
            throw new System.NotImplementedException();
        }

        public int DeletePeriod(Period period)
        {
            return 1;
        }

        public IEnumerable<Period> GetAllPeriods()
        {
            throw new System.NotImplementedException();
        }

        public Period GetPeriodById(int periodId)
        {
            return new Period
            {
                PeriodId = periodId,
                StartDate = new DateTime(2020,08,01,0,0,0),
                EndDate = new DateTime(2020, 08, 31, 0, 0, 0)
            };
        }

        public int UpdatePeriod(Period period)
        {
            throw new DbUpdateException();
        }
    }
}

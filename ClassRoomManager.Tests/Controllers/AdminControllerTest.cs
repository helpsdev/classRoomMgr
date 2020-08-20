using ClassRoomManager.Controllers;
using ClassRoomManager.Models;
using ClassRoomManager.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassRoomManager.Tests
{
    [TestClass]
    public class AdminControllerTest
    {

        [TestMethod]
        public void CreatePeriod_ReturnsAViewResult()
        {
            //Arrange
            var controller = new AdminController(null, null);
            //Act
            var result = controller.CreatePeriod();
            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void CreatePeriod_ReturnsAViewResult_WithAPeriodAsModel_WhenModelStateIsInvalid()
        {
            //Arrange
            var fakePeriodData = new Mock<IPeriodData>();
            var controller = new AdminController(fakePeriodData.Object, null);
            var period = new Period
            {
                PeriodId = 1,
                StartDate = new DateTime(2020, 02, 01),
                EndDate = new DateTime(2020, 01, 01)
            };
            //Act
            controller.ModelState.AddModelError("Dates wrong", "The dates are wrong");
            var result = controller.CreatePeriod(period);
            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            fakePeriodData.Verify(p => p.AddPeriod(period), Times.Never());
            Assert.AreEqual(period, ((ViewResult)result).Model);
        }

        [TestMethod]
        public void CreatePeriod_ReturnsAViewResult_WithAPeriodAsModel_WhenModelStateIsValid()
        {
            //Arrange
            var fakePeriodData = new Mock<IPeriodData>();
            var controller = new AdminController(fakePeriodData.Object, null);
            var period = new Period
            {
                PeriodId = 1,
                EndDate = new DateTime(2020, 02, 01),
                StartDate = new DateTime(2020, 01, 01)
            };
            //Act
            var result = controller.CreatePeriod(period);
            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            fakePeriodData.Verify(p => p.AddPeriod(period), Times.Once());
            Assert.AreEqual(period, ((ViewResult)result).Model);
        }

        [TestMethod]
        public void EditPeriod_ReturnsAViewResult_WithAPreriodAsModel()
        {
            //Arrange
            var fakePeriodData = new Mock<IPeriodData>();
            fakePeriodData.Setup(p => p.GetPeriodById(1)).Returns(new Period { PeriodId = 1 });
            var controller = new AdminController(fakePeriodData.Object, null);
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
            var fakePeriodData = new Mock<IPeriodData>();
            fakePeriodData.Setup(p => p.GetPeriodById(1)).Returns(new Period { PeriodId = 1 });
            var controller = new AdminController(fakePeriodData.Object, null);
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
            var fakePeriodData = new Mock<IPeriodData>();
            var period = new Period { PeriodId = 1 };
            fakePeriodData.Setup(p => p.UpdatePeriod(period)).Throws(new DbUpdateException());
            var controller = new AdminController(fakePeriodData.Object, null);
            
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
            var fakePeriodData = new Mock<IPeriodData>();
            var period = new Period
            {
                PeriodId = 1,
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now
            };
            fakePeriodData.Setup(p => p.UpdatePeriod(period)).Returns(1);
            var controller = new AdminController(fakePeriodData.Object, null);
            //Act
            var result = controller.EditPeriod(period);
            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType(((ViewResult)result).Model, typeof(Period));
            Assert.AreEqual(period, ((ViewResult)result).Model);
        }

        [TestMethod]
        public void DeletePeriod_ReturnsAViewResult_WithPeriodAsModel_WhenSaveErrorsIsFalse()
        {
            //Arrange
            var fakePeriodData = new Mock<IPeriodData>();
            var period = new Period {PeriodId = 1 };
            fakePeriodData.Setup(p => p.GetPeriodById(period.PeriodId)).Returns(period);
            var controller = new AdminController(fakePeriodData.Object, null);
            var periodIdToDelete = 1;
            //Act
            var result = controller.DeletePeriod(periodIdToDelete, saveErrors:false);
            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType(((ViewResult)result).Model, typeof(Period));
        }

        [TestMethod]
        public void DeletePeriod_ReturnsAViewResult_WithPeriodAsModelAndErrorMessageSet_WhenSaveErrorIsTrue()
        {
            //Arrange
            var fakePeriodData = new Mock<IPeriodData>();
            var period = new Period { PeriodId = 1 };
            fakePeriodData.Setup(p => p.GetPeriodById(period.PeriodId)).Returns(period);
            var controller = new AdminController(fakePeriodData.Object, null);
            //Act
            var result = controller.DeletePeriod(period.PeriodId, saveErrors: true);
            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual("Hubo un error al guardar la información. Intenta de nuevo, si el problema persiste llama al administrador del sitio.",
                ((ViewResult)result).ViewData["ErrorMessage"].ToString());
        }

        [TestMethod]
        public void DeletePeriod_ReturnsNotFoundResult_WhenPeriodNotExist()
        {
            //Arrange
            var fakePeriodData = new Mock<IPeriodData>();
            var period = new Period { PeriodId = 1 };
            fakePeriodData.Setup(p => p.GetPeriodById(period.PeriodId));
            var controller = new AdminController(fakePeriodData.Object, null);
            //Act
            var result = controller.DeletePeriod(period.PeriodId);
            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeletePeriod_ReturnsRedirectToActionResult_WhenSuccessfullyDeletePeriod()
        {
            //Arrange
            var fakePeriodData = new Mock<IPeriodData>();
            var period = new Period { PeriodId = 1 };
            fakePeriodData.Setup(p => p.GetPeriodById(period.PeriodId)).Returns(period);
            fakePeriodData.Setup(p => p.DeletePeriod(period)).Returns(1);
            var controller = new AdminController(fakePeriodData.Object, null);
            //Act
            var result = controller.DeletePeriod(period.PeriodId);
            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual(nameof(controller.PeriodsList), ((RedirectToActionResult)result).ActionName);
        }

        [TestMethod]
        public void DeletePeriod_ReturnsRedirectToActionResult_WhenDbUpdateExceptionOccurrs()
        {
            //Arrange
            var fakePeriodData = new Mock<IPeriodData>();
            var period = new Period { PeriodId = 1 };
            fakePeriodData.Setup(p => p.GetPeriodById(period.PeriodId)).Returns(period);
            fakePeriodData.Setup(p => p.DeletePeriod(period)).Throws(new DbUpdateException());
            var controller = new AdminController(fakePeriodData.Object, null);
            //Act
            var result = controller.DeletePeriod(period.PeriodId);
            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual(nameof(controller.DeletePeriod), ((RedirectToActionResult)result).ActionName);
            int perioIdRouteValue = int.Parse(((RedirectToActionResult)result).RouteValues["periodId"].ToString());
            Assert.AreEqual(1, perioIdRouteValue);
            bool saveErrorsRouteValue = bool.Parse(((RedirectToActionResult)result).RouteValues["saveErrors"].ToString());
            Assert.IsTrue(saveErrorsRouteValue);
        }

        [TestMethod]
        public void ActivitiesList_ReturnsAViewResult_WithIEnumerableActivityAsModel()
        {
            //Arrange
            var fakeActivityData = new Mock<IActivityData>();
            fakeActivityData.Setup(a => a.GetAllActivities()).Returns(new List<Activity>());
            var controller = new AdminController(null, fakeActivityData.Object);
            //Act
            var result = controller.ActivitiesList();
            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType(((ViewResult)result).Model, typeof(IEnumerable<Activity>));
        }

        [TestMethod]
        public void CreateActivity_ReturnsAViewResult()
        {
            //Arrange
            var controller = new AdminController(null, null);
            //Act
            var result = controller.CreateActivity();
            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void CreateActivity_ReturnsAViewResultAndAddActivityIsNotCalled_WhenModelStateIsInvalid()
        {
            //Arrange
            var fakeActivityData = new Mock<IActivityData>();
            var controller = new AdminController(null, fakeActivityData.Object);
            var activity = new Activity
            {
                ActivityId = 1,
                CreationDate = DateTimeOffset.Now,
                FinalEvaluationValue = 4,
                ModificationDate = DateTimeOffset.Now,
                Name = "test name",
                Type = ActivityType.Grade
            };
            //Act
            controller.ModelState.AddModelError("ModelInvalid", "Some error in stage");
            var result = controller.CreateActivity(activity);
            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            fakeActivityData.Verify(a => a.AddActivity(activity), Times.Never);
            Assert.AreEqual(activity, ((ViewResult)result).Model);

        }

        [TestMethod]
        public void CreateActivity_ReturnsAViewResultAndAddActivityIsCalledOnce_WhenModelStateIsValid()
        {
            //Arrange
            var fakeActivityData = new Mock<IActivityData>();
            var activity = new Activity()
            {
                ActivityId = 1,
                CreationDate = DateTimeOffset.Now,
                ModificationDate = DateTimeOffset.Now,
                FinalEvaluationValue = 4,
                Name = "test activity name",
                Type = ActivityType.Grade
            };
            fakeActivityData.Setup(a => a.AddActivity(activity)).Returns(1);
            var controller = new AdminController(null, fakeActivityData.Object);
            //Act
            var result = controller.CreateActivity(activity);
            //Assert
            fakeActivityData.Verify(a => a.AddActivity(activity), Times.Once());
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(activity, ((ViewResult)result).Model);

        }

        [TestMethod]
        public void EditActivity_ReturnsAViewResult_WithActivityAsModel()
        {
            //Arrange
            var fakeActivityData = new Mock<IActivityData>();
            var activity = new Activity
            {
                ActivityId = 1
            };
            fakeActivityData.Setup(a => a.GetActivityById(activity.ActivityId)).Returns(activity);
            var controller = new AdminController(null, fakeActivityData.Object);
            //Act
            var result = controller.EditActivity(activity.ActivityId);
            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(activity, ((ViewResult)result).Model);

        }
    }
}

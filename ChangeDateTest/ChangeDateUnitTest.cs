using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChangeDate.Models;

namespace ChangeDateTest
{
    [TestClass]
    public class ChangeDateUnitTest
    {
        ChangeDateModel changeDateModel = new ChangeDateModel();

        [TestMethod]
        public void VerifyDefaultDate()
        {
            string expected = "01/01/1900 00:00";
            string result = changeDateModel.DateTime;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void SetNewDateMore4000()
        {
            string expected = "04/03/2010 17:40";
            string result = changeDateModel.ChangeDate("01/03/2010 23:00", '+', 4000);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void VerifyErrorCodeToInvalidDateFormat()
        {
            string expected = "500";
            string result = changeDateModel.ChangeDate("01-09-2017", '+', 4000);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void VerifyErrorCodeToInvalidOperatorFormat()
        {
            string expected = "500";
            string result = changeDateModel.ChangeDate("05-09-2017 23:00", '@', 5000);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void VerifyReturnToNegativeMinutes()
        {
            string expected = "12/05/2017 02:00";
            string result = changeDateModel.ChangeDate("10/05/2017 00:00", '+', -3000);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void SetNewDateSub4000()
        {
            string expected = "27/09/2017 23:00";
            string result = changeDateModel.ChangeDate("30/09/2017 17:40", '-', 4000);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void SetNewDateAddOneYearMinutes()
        {
            string expected = "20/02/2017 04:00";
            string result = changeDateModel.ChangeDate("20/02/2016 04:00", '+', 525600);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void SetNewDateAddOneMonthMinutes()
        {
            string expected = "25/03/2017 04:59";
            string result = changeDateModel.ChangeDate("25/02/2017 04:59", '+', 43800);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void SetNewDateAddOneDayMinutesChangeMonth()
        {
            string expected = "01/03/2016 04:00";
            string result = changeDateModel.ChangeDate("28/02/2016 04:00", '+', 1440);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void SetNewDateAddOneHourMinutesChangeDayAndMonth()
        {
            string expected = "01/10/2017 00:00";
            string result = changeDateModel.ChangeDate("30/09/2017 23:00", '+', 60);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void VerifyDateWhenDayMonthHourOrMinuteMoreThanAcceptValues()
        {
            string expected = "01/01/2017 00:01";
            string result = changeDateModel.ChangeDate("32/13/2017 24:61", '+', 1);
            Assert.AreEqual(expected, result);
        }
    }
}

using safnet.iba.Business.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace safnet.iba.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for tEvent and is intended
    ///to contain all tEvent Unit Tests
    ///</summary>
    [TestClass()]
    public class tEvent
    {
        
        //internal virtual Event CreateEvent()
        //{
        //    Event target = new safnet.iba.Business.Entities.Moles.SEvent();
        //    return target;
        //}

        ///// <summary>
        /////A test for Duration
        /////</summary>
        //[TestMethod()]
        //[HostType("Moles")]
        //public void t_Duration()
        //{
        //    Event target = CreateEvent();
        //    target.StartTimeStamp = DateTime.Now;
        //    target.EndTimeStamp = target.StartTimeStamp.Value.AddMinutes(5);
        //    TimeSpan? expected = new TimeSpan(0, 5, 0);

        //    TimeSpan? actual = target.Duration;
        //    Assert.AreEqual(expected, actual);
        //}

        ///// <summary>
        /////A test for EndTimeStamp
        /////</summary>
        //[TestMethod()]
        //public void t_EndTimeStamp()
        //{
        //    Event target = CreateEvent();
        //    Nullable<DateTime> expected = System.DateTime.Now;

        //    Nullable<DateTime> actual;
        //    target.EndTimeStamp = expected;
        //    actual = target.EndTimeStamp;
        //    Assert.AreEqual(expected, actual);
        //}


        ///// <summary>
        /////A test for LocationId
        /////</summary>
        //[TestMethod()]
        //public void t_LocationId()
        //{
        //    Event target = CreateEvent();
        //    Guid expected = LookupConstants.LocationTypePoint;

        //    Guid actual;
        //    target.LocationId = expected;
        //    actual = target.LocationId;
        //    Assert.AreEqual(expected, actual);
        //}

        ///// <summary>
        /////A test for StartTimeStamp
        /////</summary>
        //[TestMethod()]
        //public void t_StartTimeStamp()
        //{
        //    Event target = CreateEvent();
        //    Nullable<DateTime> expected = System.DateTime.Now;

        //    Nullable<DateTime> actual;
        //    target.StartTimeStamp = expected;
        //    actual = target.StartTimeStamp;
        //    Assert.AreEqual(expected, actual);
        //}
    }
}

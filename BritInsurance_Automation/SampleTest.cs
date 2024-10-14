using BritInsurance_Automation;
using NUnit.Framework;
using Custom;
using System.Collections.Generic;
using RestSharp;

namespace YourNamespace
{
    public class SampleTest : BaseTest
    {
        [Test]
        public void UIValidation()
        {
            List<string> data = new List<string> { "Financials", "Interim results for the six months ended 30 June 2022", "Results for the year ended 31 December 2023", "Interim Report 2023", "Kirstin Simon" };
            driver.Navigate().GoToUrl("https://www.britinsurance.com/ ");
            var actionHelper = new ActionHelper(driver);
            Assert.AreEqual("Brit Insurance | Writing the Future", actionHelper.GetTitle());
            actionHelper.EnterDataonHomePage("IFRS 17");
            Assert.IsTrue(actionHelper.ValidateSerachResult(data));
        }

        [Test]
        public void APIValidation()
        {
            var actionHelper = new ActionHelper(driver);
            var id = actionHelper.CreateIdWithPostRequest();
            var updatedNameForData = "QA Test 2024";
            var results = actionHelper.CreatePatchRequestForID(id, updatedNameForData);
            Assert.AreEqual(id, results.id);
            Assert.AreEqual(updatedNameForData, results.name);

        }


    }
}

using BritInsurance_Automation;
using OpenQA.Selenium;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;

namespace Custom
{
    public class ActionHelper
    {
        private IWebDriver driver;
        ExplicitWaitHelperClass explicitwaitObj;

        public ActionHelper(IWebDriver driver)
        {
            this.driver = driver;
            explicitwaitObj = new ExplicitWaitHelperClass(driver);
        }
        #region Elements
        private IWebElement SearchButton => explicitwaitObj.WaitForElement("button[aria-label='Search button']");

        private IWebElement SearchBar => explicitwaitObj.WaitForElement("input[type='search']");

        private IReadOnlyCollection<IWebElement> allmatchingRows => explicitwaitObj.WaitForElementXpathList("//div[@class='result']/a");
        #endregion

        #region Actions
        public string GetTitle()
        {
            return driver.Title;
        }

        public void EnterDataonHomePage(string inputData)
        {
            Thread.Sleep(5000);
            SearchButton.Click();
            SearchBar.SendKeys(inputData);
        }

        public bool ValidateSerachResult(IList<string> expectedData)
        {
            IList<string> actualData = new List<string>();
            bool result = allmatchingRows.Count == 5;
            foreach (var ele in allmatchingRows)
            {
                actualData.Add(ele.Text.Trim());
            }

            result &= actualData.SequenceEqual(expectedData);
            return result;
        }

        public string CreateIdWithPostRequest()
        {
            var jsonBody = @"{
                ""name"": ""Apple MacBook Pro 16"",
                ""data"": {
                    ""year"": 2019,
                    ""price"": 1849.99,
                    ""CPU model"": ""Intel Core i9"",
                    ""Hard disk size"": ""1 TB""
                }
            }";

            var client = new RestClient("https://api.restful-api.dev");
            var request = new RestRequest("/objects", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(jsonBody);
            var response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var id = JsonSerializer.Deserialize<Root>(response.Content).id;
                return id;
            }
            else
                return null;
        }

        public Root CreatePatchRequestForID(string id, string updatedName)
        {
            
            var client = new RestClient("https://api.restful-api.dev");
            var request = new RestRequest("/objects/"+id+"", Method.Patch);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { name = updatedName });
            var response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var data = JsonSerializer.Deserialize<Root>(response.Content);
                return data;
            }
            else
                return null;
        }

        #endregion

        #region API CLass
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data
        {
            public int year { get; set; }
            public double price { get; set; }

          
            public string CPUmodel { get; set; }
            public string Harddisksize { get; set; }
        }

        public class Root
        {
            public string id { get; set; }
            public string name { get; set; }
            public DateTime createdAt { get; set; }
            public Data data { get; set; }
        }


        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeSample.Models;
using SampleDataFactory.DataModels;

namespace CodeSample.Controllers
{
    /// <summary>
    /// Date: 01-22-2020
    /// This Controller contains methods that perform the following actions:
    ///     1- Return view/partial views that are used to render data.
    ///     2- Invokes the middle-tier to either get of save data.
    ///     2- Load models with data that  will then be used by views to reneder data.
    /// </summary>
     
    public class SampleCodeController : Controller
    {
        //This is the main method in my sample code application.
        //This method returns the main view.
        //The main view servers as a Parent View to all partial views in this controller
        public ActionResult Index(int Param1 = 0)
        {
            //This values will be passed over to the view by use of a view bag.
            ViewBag.ApplicationName = "Sample code application"; 
            ViewBag.PassValue1ToTheNextView = Param1;


            return View();
        }

        /// <summary>
        /// This method calls the data factory(middle-tier) to retrieve data from the DataBase.
        /// It loads the model with the data returned and passes it to the partial view for rendering.
        /// </summary>
        /// <returns>CodeSampleEmployeeView.cshtml</returns>
        public ActionResult CodeSampleEmployeeView()
        {
            //CHECK IF USER HAS ACCESS BY CALLING THE SECURITY CHECK METHOD BELOW. 
            //-------------------------------------------------------------------------------------------------------
            if (SecurityCheck() == false)
                return Content("<script type='text/javascript'>window.opener='blah';window.close();</script>");
            //-------------------------------------------------------------------------------------------------------

            //create an instace of CodeSampleEmployeeModel class
            CodeSampleEmployeeModel CodeSampleEmployeeModel = new CodeSampleEmployeeModel();

            //Build the reference to the datafactory (middle-tier) getdata class
            SampleDataFactory.GetData GD = new SampleDataFactory.GetData();

            //call middle-tier get data method and load model with data returned
            CodeSampleEmployeeModel.GetCodeSampleList = GD.GetCodeSampleList();

            //get a count of the records returned.
            ViewBag.count = CodeSampleEmployeeModel.GetCodeSampleList.Count;

            //If no records are returned,notify user.
            if (ViewBag.count == 0)
            {
                ViewBag.ShowMessage = "No results found";
            }

            return PartialView("~/Views/CodeSample/CodeSampleEmployeeView.cshtml", CodeSampleEmployeeModel);

        }
        /// <summary>
        /// This method will receive an employee object from javascript/Jquery ajax post funtion.
        /// It will then call the middle-tier and pass the object to sava/update employee information.
        /// </summary>
        /// <param name="NewEmployeeInformationList"></param>
        /// <returns></returns>

        public JsonResult SaveEmployeeData(SampleEmployeeDataModel NewEmployeeInformationList)
        {
            //Build the reference to the datafactory (middle-tier) save data class.
            SampleDataFactory.SaveData SaveData = new SampleDataFactory.SaveData();

            // Call datafactory (middle-tier) to save the data.
            var SavedID = SaveData.SaveEmployeeInformation(NewEmployeeInformationList);
            

            return Json(SavedID);
        }

        /// <summary>
        /// /// This routine makes sure that any calls to this controller from the outside world came from a legitimate users, and not some hacker sending random requests.
        /// </summary>
        /// <returns></returns>
        public bool SecurityCheck()
        {
            bool pass = true;
            string currentUser = User.Identity.Name;
            currentUser = currentUser.ToLower().Replace("companyname\\", "");

            //This method will be called by every method that returns a partialview. If a false value is returned,
            // access to that view will be denied.
            if (System.Configuration.ConfigurationManager.AppSettings["websiteSecurity"] == "TRUE")
            {
                SampleDataFactory.GetData SampleDFGetData  = new SampleDataFactory.GetData();
                if (SampleDFGetData.CheckuserSecurityAccess(currentUser) == "false") 
                    pass = false;
            }

            return pass;
        }
    }
}
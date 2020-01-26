using SampleDataFactory.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeSample.Models
{
    public class CodeSampleEmployeeModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string SSN { get; set; }

        //This list will be used to render data in code sample view
        public List<SampleEmployeeDataModel> GetCodeSampleList { get; set; }

    }
}
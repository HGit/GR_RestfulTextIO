using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using GR.TextIO;

namespace TextIOWebAPI.Controllers
{
	// HEG  - not now, keep simple...
    // [Authorize]	
    public class ValuesController : ApiController
    {

		//public ValuesController()
		//{
		//	textReader = new DelimitedTextReader(); //  CreateTextReader(args);
		//	textReader.FieldDelimiter = ",";
		//	textReader.RecordDelimiter = System.Environment.NewLine;
		//	textReader.HeaderRecordIndex = 0;
		//}



		protected DelimitedTextReader _textReader = null;


		public DelimitedTextReader textReader
		{
			get
			{
				if(_textReader == null)
				{
					_textReader = new DelimitedTextReader();
					//_textReader.FieldDelimiter = ","; // decault to comma
					//_textReader.RecordDelimiter = System.Environment.NewLine; // decault to NL
					//_textReader.HeaderRecordIndex = 0;
				}

				return _textReader;
			}

			//protected
			set
			{
				_textReader = value;
			}
		}


		[AcceptVerbs("GET")]
		[ActionName("DataTableRaw")]
		public DataTable DataTable()
        {
			if(this.textReader == null) return null;

			return this.textReader.CurrentDataTable;
        }

		[AcceptVerbs("GET")]
		[ActionName("DataTable")]
		//public string SerializedDataTable()
		public string GetDataTable()
        {
			string json = "";

			if(this.textReader == null) return json;

			if(this.textReader.CurrentDataTable != null)
			{
				json = this.textReader.SerializeToDataTable(this.textReader.CurrentDataTable);
			}
			//return this.textReader.SerializeToDataTable();
			return json;
        }


		 // POST api/values
		[AcceptVerbs("POST", "PUT")]
		[ActionName("DataTable")]
        public void SetDataTable([FromBody]string value) // string is JSON for Create DataTable DataRow and/or Model instance
        {
			if(value == null) return;

			if(value.Trim().Length <= 0) return;

			if(this.textReader == null) return;

			this.textReader.CurrentDataTable = this.textReader.DeserializeToDataTable(value);
			// 
			// Does DataRow exist?
        }

		////[AcceptVerbs("PUT")]
		////[ActionName("DataTable")]
		////public void SetDataTable([FromBody]string value) // string is JSON for Create DataTable DataRow and/or Model instance
		////{
		////	if(value == null) return;

		////	if(value.Trim().Length <= 0) return;

		////	this.textReader.CurrentDataTable = this.textReader.DeserializeToDataTable(value);
		////	// 
		////	// Does DataRow exist?
		////}

		[AcceptVerbs("DELETE")]
		[ActionName("DataTable")]
		public void DeleteDataTable()		
        {
			if(this.textReader == null) return;

			if(this.textReader.CurrentDataTable != null)
			{
				this.textReader.CurrentDataTable.Clear();
			}

			this.textReader.CurrentDataTable = null;
        }






		

		////[HttpGet]		
		//// OR...
		////[AcceptVerbs("GET", "POST", "PUT", "DELETE", "HEAD")] // If more than one and/or uncommon (other than HttpGet, HttpPut, HttpPost, or HttpDelete) verbs are needed...
		//[AcceptVerbs("GET")]
		////
		//[ActionName("SampleRecordValues")] // specific {action} name instead of the method itself?
		////
		//// http://localhost/PennyMac/api/Test/TestService2/333 - NOTE: {loanGuid} (in the route) must match (case-sensitive) the literal parameter in the method
		////[Route("api/Test/TestService2/{loanGuid}")]  // SPECIFIC ROUTE HERE - No use of {controller} OR {action}
		// // GET api/values/SampleRecordValues
  //      public IEnumerable<string> GetSampleRecordValues()
  //      {
  //          return new string[] { "value1", "value2" };
  //      }


		//[AcceptVerbs("GET")]
		//[ActionName("CurrentDataTableRows")]
		//public List<DataRow> GetCurrentDataTableRows()
  //      {
		//	List<DataRow> list = null; // = new List<DataRow>(
		//	if(this.textReader.CurrentDataTable != null)
		//	{
		//		list = new List<DataRow>(this.textReader.CurrentDataTable.Select());
		//	}
		//	//else
		//	//{
		//	//	list = new List<DataRow>(); // send an empty list... ???
		//	//}

		//	return list;
  //      }


		//[AcceptVerbs("GET")]
		////[ActionName("DataTableRaw")]
		//public DataTable InitializeTextReader()
  //      {
		//	if(this.textReader == null)
		//	{
		//		textReader = new DelimitedTextReader();
		//		textReader.FieldDelimiter = ","; // decault to comma
		//		textReader.RecordDelimiter = System.Environment.NewLine; // decault to NL
		//		textReader.HeaderRecordIndex = 0;
		//	}

		//	return this.textReader.CurrentDataTable;
  //      }

		




		////// GET api/values
		////public IEnumerable<string> Get()
		////{
		////	return new string[] { "value1", "value2" };
		////}

		////// GET api/values/5
		////public string Get(int id)
		////{
		////	return "value";
		////}

		////// POST api/values
		////public void Post([FromBody]string value) // string is JSON for Create DataTable DataRow and/or Model instance
		////{
		////	if(value == null) return;

		////	if(value.Trim().Length <= 0) return;

		////	// 
		////	// Does DataRow exist?
		////}

		////// PUT api/values/5
		////public void Put(int id, [FromBody]string value) // string is JSON for Updating the DataTable and/or Model instance
		////{
		////}

		////// DELETE api/values/5
		////public void Delete(int id)
		////{
		////	//DataTable dataTable = 
		////	//if(id < 0 || id > 
		////}
	}
}

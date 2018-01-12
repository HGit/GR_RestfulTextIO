using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextIOWebAPI;
using TextIOWebAPI.Controllers;
using System.Data;
using GR.TextIO;

namespace TextIOWebAPI.Tests.Controllers
{
	[TestClass]
	public class ValuesControllerTest
	{


		public ValuesControllerTest()
		{			
			Initialize();
		}


		ValuesController _controller = new ValuesController();

		protected ValuesController controller
		{
			get
			{
				if(_controller == null) _controller = new ValuesController();
				return _controller;
			}

			set
			{
				_controller = value;
			}
		}



		protected virtual void Initialize()
		{
			controller.textReader = new DelimitedTextReader();

			controller.textReader.FieldDelimiter = ","; // decault to comma
			controller.textReader.RecordDelimiter = System.Environment.NewLine; // decault to NL
			controller.textReader.HeaderRecordIndex = 0;

			string data =
			"LastName,FirstName,Gender,DOB,FavoriteColor" + System.Environment.NewLine
			+ "Doe,Jane,F,2/17/1991,red" + System.Environment.NewLine
			+ "Smith,John,M,4/20/1985,green" + System.Environment.NewLine
			+ "Taylor,Henry,M,1/13/1965,blue" + System.Environment.NewLine
			+ "Griffin,Jen,F,4/3/1979,cyan" + System.Environment.NewLine
			;

			try
			{
				System.IO.StringReader sreader = new System.IO.StringReader(data);
				controller.textReader.Read(sreader);

				controller.textReader.InitializeDataTable("TEST-TABLE");
			}
			catch(Exception e)
			{

			}
		}


		//[AcceptVerbs("GET")]
		//[ActionName("DataTableRaw")]
		[TestMethod]
		public void DataTable()
        {	
			//ValuesController controller = new ValuesController();

			controller.textReader = new DelimitedTextReader(0, System.Environment.NewLine, ",");
			DataTable dataTable = controller.DataTable(); 

			Assert.IsNotNull(dataTable);			
        }

		//[AcceptVerbs("GET")]
		//[ActionName("DataTable")]
		public void GetDataTable()
		{
			//ValuesController controller = new ValuesController();

			string json = controller.GetDataTable(); 

			//Assert.IsNotNull(json);	

			Assert.IsTrue((json != null && json.Trim().Length > 0));	
		}



		[TestMethod]
		public void SetDataTable(string value)
		{
			//ValuesController controller = new ValuesController();

			Assert.IsNotNull(value);

			Assert.IsTrue(value.Trim().Length > 0);

			controller.SetDataTable(value);

			Assert.IsNotNull(controller.textReader);

			Assert.IsNotNull(controller.textReader.CurrentDataTable);

		}


		//[AcceptVerbs("DELETE")]
		//[ActionName("DataTable")]
		[TestMethod]
		public void DeleteDataTable()
		{
			//ValuesController controller = new ValuesController();

			controller.DeleteDataTable();

			Assert.IsNotNull(controller.textReader);

			Assert.IsNull(controller.textReader.CurrentDataTable);
		}

		//[TestMethod]
		//public void DeleteDataTable()
		//{
		//	// Arrange
		//	ValuesController controller = new ValuesController();

		//	controller.DeleteDataTable();

		//}



		//[TestMethod]
		//public void GetById()
		//{
		//	// Arrange
		//	ValuesController controller = new ValuesController();

		//	// Act
		//	string result = controller.Get(5);

		//	// Assert
		//	Assert.AreEqual("value", result);
		//}

		////[TestMethod]
		////public void Post()
		////{
		////	// Arrange
		////	ValuesController controller = new ValuesController();

		////	// Act
		////	controller.Post("value");

		////	// Assert
		////}

		////[TestMethod]
		////public void Put()
		////{
		////	// Arrange
		////	ValuesController controller = new ValuesController();

		////	// Act
		////	controller.Put(5, "value");

		////	// Assert
		////}

		////[TestMethod]
		////public void Delete()
		////{
		////	// Arrange
		////	ValuesController controller = new ValuesController();

		////	// Act
		////	controller.Delete(5);

		////	// Assert
		////	//Assert.AreEqual(
		////}
	}
}

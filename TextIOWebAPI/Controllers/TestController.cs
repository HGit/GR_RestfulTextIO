using EllieMae.Encompass.BusinessObjects.Loans;
using EllieMae.Encompass.BusinessObjects.Loans.Templates;
using EllieMae.Encompass.Client;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace EncompWebAPI.Controllers
{
    public class TestController : ApiController
    {
        //[HttpGet]
        //[Route("register")]
	    //TestService
        public IHttpActionResult ORIGINALTest_Svc()
        {
            Dictionary<string, string> loanData = new Dictionary<string, string>();
            Session session = new Session();
            Loan loan = null;
            try
            {
                session.Start(@"https://TEBE11157523.ea.elliemae.net$TEBE11157523", "sysbrokerportal", "U7@ZPM5j?cKPg%");
                //Fetch the template based on the configured loan template in config.
                LoanTemplate template = (LoanTemplate)session.Loans.Templates.GetTemplate(TemplateType.LoanTemplate, @"public:\Companywide\Wholesale");
                //Creating a new loan with the selected template
                loan = template.NewLoan();
                if (loan != null)
                {
                    //setting the business rules to false, to save execution time.
                    loan.BusinessRulesEnabled = false;
                    //turning off the calculations, to save execution time.
                    loan.CalculationsEnabled = false;
                    // Set the loan folder for the loan
                    loan.LoanFolder = "Test Loans";

                    //Setting the field values to the loan, passed in the request.
                    string firstName = "BorrowerFirstName";
                    string lastname = "BorrowerLastName";

                    loan.Fields["36"].Value = firstName;
                    loan.Fields["37"].Value = lastname;

                    //setting the business rule, before commit.
                    loan.BusinessRulesEnabled = true;
                    //setting the calculation, before commit.
                    loan.CalculationsEnabled = true;
                    loan.Commit();
                    //Return loan number and GUID after the loan is created.
                    loanData.Add("Loan.LoanNumber", loan.LoanNumber);
                    loanData.Add("Loan.Guid", loan.Guid);
                    loanData.Add("Loan.4000", loan.Fields["4000"].UnformattedValue);
                    loanData.Add("Loan.4002", loan.Fields["4002"].UnformattedValue);
                }
            }
            catch (Exception ex)
            {
                // Catch all other exceptions and raise/throw new EncompassException with custom message after logging it
                loanData.Add("Exception Occured", ex.Message);
            }
            finally
            {
                loan?.Close();
            }
            return Ok(loanData);
        }




		//[HttpGet]
		// OR...
		//[AcceptVerbs("GET", "HEAD")] // If more than one and/or uncommon (other than HttpGet, HttpPut, HttpPost, or HttpDelete) verbs are needed...
		[AcceptVerbs("GET")] 
		// [Route("register")] // DEFAULT REOUTE: api/{controller}/{id}
		// 
		public string TestService()
		{
			string retval = "TestService()";
			//string loanGuid = "{6feacb2c-8dd7-4167-a047-af0927c66132}";
			//string str = TestService2(loanGuid);
			//if(str != null) retval += ", " + str;
			return retval;
		}



		//[HttpGet]
		// OR...
		//[AcceptVerbs("GET", "HEAD")] // If more than one and/or uncommon (other than HttpGet, HttpPut, HttpPost, or HttpDelete) verbs are needed...
		[AcceptVerbs("GET")] 
		// [Route("register")] // DEFAULT REOUTE: api/{controller}/{id}
		// 
		public string TestService(string loanGuid)
		{
			if(loanGuid == null) loanGuid = "";
			string retval = "TestService(string loanGuid)=" + loanGuid;
			//string loanGuid = "{6feacb2c-8dd7-4167-a047-af0927c66132}";
			//string str = TestService2(loanGuid);
			//if(str != null) retval += ", " + str;
			return retval;
		}



		//[HttpGet]
		// OR...
		//[AcceptVerbs("GET", "HEAD")] // If more than one and/or uncommon (other than HttpGet, HttpPut, HttpPost, or HttpDelete) verbs are needed...
		[AcceptVerbs("GET")] 
		//
		// http://localhost/PennyMac/api/Test/TestService2/333 - NOTE: {loanGuid} (in the route) must match the literal parameter in the method
		[Route("api/Test/TestService2/{loanGuid}")]  // SPECIFIC ROUTE HERE - No use of {controller} OR {action}
		//
		public string TestService2(string loanGuid)
		{
			if(loanGuid == null) loanGuid = "";
			return "TestService2(string loanGuid):" + loanGuid;
		}


		//[HttpGet]
		[AcceptVerbs("GET")] 
		// route: If registered in Uses default: "api/{controller}/{action}/{loanGuid}
		// 
		public string TestService3(string loanGuid)
		{
			if(loanGuid == null) loanGuid = "";
			return "TestService3(string loanGuid):" + loanGuid;
		}


		//[HttpGet]
		[AcceptVerbs("GET")] 
		// route: If registered in Uses default: "api/{controller}/{action}/{loanGuid}
		[ActionName("TestServiceA")]
		public string TestService4(string loanGuid)
		{
			if(loanGuid == null) loanGuid = "";
			return "A TestService4(string loanGuid):" + loanGuid;
		}


		//[HttpGet]
		[AcceptVerbs("GET")]		
		//[Route()]
		[ActionName("TestServiceB")]
		// URL:
		// http://localhost/PennyMac/api/register/Ludicrous/TestServiceFour
		// http://localhost/PennyMac/register/TestServiceFour
		public string TestService5(string loanGuid)
		{
			if(loanGuid == null) loanGuid = "";
			return "B TestService5(string loanGuid):" + loanGuid;
		}



		//[HttpGet]
		[AcceptVerbs("GET")] 
		// [Route("register")]
        // public IHttpActionResult TestService()
		public string TestPennyMacService(string loanGuid)
        {
			this.WriteToVariable = true;
			DebugPoint = 0;

			Dictionary<string, string> loanData = null;
            try
            {
				// EllieMae.EMLite.Common.EnGlobalSettings.get_HttpClientCustomHeaders()'. at EllieMae.Encompass.Client.Session.InitAppName() at EllieMae.Encompass.Client.Session..ctor() 
				//EllieMae.
				//loanData = TestPennyMac2();

				Session session = new Session();
				session.Start(@"https://TEBE11157523.ea.elliemae.net$TEBE11157523", "sysbrokerportal", "U7@ZPM5j?cKPg%");

				
				//Dictionary<string, string> loanData = null;

				//string loanGuid = ""; //  "{6feacb2c-8dd7-4167-a047-af0927c66132}"; // "{5079a630-ca13-43c7-8007-8ef8e442c402}"; //  "{344a86f2-7a3b-4beb-9d34-933e40669391}";
				bool UseLoanTemplateForNewLoanCreation = false; //  true; // if loanGuid is empty or invalid

				bool BusinessRulesEnabled = true; //  false; // false allowed new loan creation!!!
				bool CalculationsEnabled = true;

				// SUCCESSFUL VARIABLES
				//
				////loanGuid = "{6feacb2c-8dd7-4167-a047-af0927c66132}";
				////UseLoanTemplateForNewLoanCreation = true;	
				////BusinessRulesEnabled = true; //  false; // false allowed new loan creation!!!
				////CalculationsEnabled = true;


				//loanGuid = ""; //  "{6feacb2c-8dd7-4167-a047-af0927c66132}";
				UseLoanTemplateForNewLoanCreation = false;
				BusinessRulesEnabled = false; //  false; // false allowed new loan creation!!!
				CalculationsEnabled = false;

				loanData = TestPennyMac(session, loanGuid, UseLoanTemplateForNewLoanCreation, BusinessRulesEnabled, CalculationsEnabled);

				//foreach(string key in loanData.Keys)
				//{
				//	string val = loanData[key];
				//	if(val == null) val = "NULL";
				//	WriteLine("KEY[" + key + "]=[" + val + "]");
				//}

				//return loanData;


            }
            catch (Exception ex)
            {
				WriteLine("" + ex);
				////// Catch all other exceptions and raise/throw new EncompassException with custom message after logging it
				////loanData.Add("Exception Occured", ex.Message);
				////System.Web.Http.Results.ExceptionResult er = new System.Web.Http.Results.ExceptionResult(ex,this);
				////return er;
            }
            finally
            {
               // nothing here...
            }

			this.WriteToVariable = false;

			if(loanData == null) loanData = new Dictionary<string, string>();
			//return Ok(loanData);
			return "BEGIN-" + this.WriteVariable + "-DebugPoint-" + DebugPoint + "-END"; // Ok(this.WriteVariable);
        }
        


		//////static
		////public  Dictionary<string, string> TestPennyMac2()
		////{

		////	Session session = new Session();
  ////          session.Start(@"https://TEBE11157523.ea.elliemae.net$TEBE11157523", "sysbrokerportal", "U7@ZPM5j?cKPg%");

				
		////	Dictionary<string, string> loanData = null;

		////	string loanGuid = ""; //  "{6feacb2c-8dd7-4167-a047-af0927c66132}"; // "{5079a630-ca13-43c7-8007-8ef8e442c402}"; //  "{344a86f2-7a3b-4beb-9d34-933e40669391}";
		////	bool UseLoanTemplateForNewLoanCreation = false; //  true; // if loanGuid is empty or invalid

		////	bool BusinessRulesEnabled = true; //  false; // false allowed new loan creation!!!
		////	bool CalculationsEnabled = true;

		////	// SUCCESSFUL VARIABLES
		////	//
		////	////loanGuid = "{6feacb2c-8dd7-4167-a047-af0927c66132}";
		////	////UseLoanTemplateForNewLoanCreation = true;	
		////	////BusinessRulesEnabled = true; //  false; // false allowed new loan creation!!!
		////	////CalculationsEnabled = true;


		////	loanGuid = ""; //  "{6feacb2c-8dd7-4167-a047-af0927c66132}";
		////	UseLoanTemplateForNewLoanCreation = false;
		////	BusinessRulesEnabled = false; //  false; // false allowed new loan creation!!!
		////	CalculationsEnabled = false;

		////	loanData = TestPennyMac2(session, loanGuid, UseLoanTemplateForNewLoanCreation, BusinessRulesEnabled, CalculationsEnabled);

		////	//foreach(string key in loanData.Keys)
		////	//{
		////	//	string val = loanData[key];
		////	//	if(val == null) val = "NULL";
		////	//	WriteLine("KEY[" + key + "]=[" + val + "]");
		////	//}

		////	return loanData;

		////}


		//static
		public  Dictionary<string, string> TestPennyMac(Session session, string loanGuid
																, bool UseLoanTemplateForNewLoanCreation
																, bool BusinessRulesEnabled
																, bool CalculationsEnabled
															)
        {
            Dictionary<string, string> loanData = new Dictionary<string, string>();
			//Session session = new Session();

			if(session == null)
			{
				WriteLine("ERROR - session == null");
				return loanData;
			}

			if(!(session.IsConnected))
			{				
				WriteLine("ERROR - !(session.IsConnected)");
				return loanData;
			}
			
			
            Loan loan = null;
            try
            {
				////// Session session = new Session();
				//////session.Start(@"https://TEBE11157523.ea.elliemae.net$TEBE11157523", "sysbrokerportal", "U7@ZPM5j?cKPg%");

				//string line = "";
				//////Fetch the template based on the configured loan template in config.
				////LoanTemplate template = (LoanTemplate)session.Loans.Templates.GetTemplate(TemplateType.LoanTemplate, @"public:\Companywide\Wholesale");
				//////Creating a new loan with the selected template
				////loan = template.NewLoan();
				if(loanGuid != null && loanGuid.Trim().Length > 0)
				{
					WriteLine("Opening Loan using loanGuid [" + loanGuid + "]");

					loanGuid = loanGuid.Trim(); // Trim... just in case...
					try
					{
						loan = session.Loans.Open(loanGuid);

						if(loan != null)
						{
							if(loan.GetCurrentLocks().Count > 0)
							{
								throw new Exception("ERROR - loan.GetCurrentLocks().Count > 0");
							}
						}
					}
					catch(Exception e)
					{
						WriteLine("" + e);
						throw e;
					}
				}

				if(loan == null)
				{
					////WriteLine("Loan is null - Would you like to create a NEW LOAN? - If so, press 'Y' and <ENTER>");
					////line = Console.ReadLine();
					////if(line != null && line.Trim().Length > 0 && line.ToUpper().StartsWith("Y"))
					{
						WriteLine("Creating new loan...");
						
						//Fetch the template based on the configured loan template in config.
						LoanTemplate template = (LoanTemplate)session.Loans.Templates.GetTemplate(TemplateType.LoanTemplate, @"public:\Companywide\Wholesale");
						//Creating a new loan with the selected template
						if(UseLoanTemplateForNewLoanCreation && template != null)
						{
							loan = template.NewLoan();
						}
						else
						{
							loan = session.Loans.CreateNew();
						}

						if(loan != null)
						{
							loan.LoanFolder = "Test Loans"; // folder can only be set like this on NEW LOAN...
						}
					}
				}

                if (loan != null)
                {
					////if(loan.GetCurrentLocks().Count > 0)
					////{
					////	throw new Exception("");
					////}

					if(!(loan.Locked))
					{
						WriteLine("Locking loan for editing...");
						loan.Lock();
						WriteLine("Loan locked!");
					}

					//if(!(loan.LockedExclusive
					//////setting the business rules to false, to save execution time.
					if(loan.BusinessRulesEnabled != BusinessRulesEnabled) loan.BusinessRulesEnabled = BusinessRulesEnabled;
                    //////turning off the calculations, to save execution time.
                    if(loan.CalculationsEnabled != CalculationsEnabled) loan.CalculationsEnabled = CalculationsEnabled;

					WriteLine("");
					WriteLine("loan.BusinessRulesEnabled=" + loan.BusinessRulesEnabled);
					WriteLine("loan.CalculationsEnabled=" + loan.CalculationsEnabled);
					WriteLine("");


					DateTime NOW = DateTime.Now;
					//Setting the field values to the loan, passed in the request.
					string firstName = "FN" + NOW.Ticks;
					string lastname = "LN" + NOW.Ticks;

					// Set the loan folder for the loan
					////loan.LoanFolder = "Test Loans"; only on new loan
					loan.Fields["36"].Value = firstName;
					loan.Fields["37"].Value = lastname;

					////WriteLine("Would you like to set Loan Fields? - If so, press 'Y' and <ENTER>");
					////line = Console.ReadLine();
					////if(line != null && line.Trim().Length > 0 && line.ToUpper().StartsWith("Y"))
					////{
					////	// Set the loan folder for the loan
					////	loan.LoanFolder = "Test Loans";

						
					////	WriteLine("Would you like to set Loan Fields (4000, 4002)? - If so, press 'Y' and <ENTER>");
					////	line = Console.ReadLine();
					////	if(line != null && line.Trim().Length > 0 && line.ToUpper().StartsWith("Y"))
					////	{
					////		loan.Fields["4000"].Value = firstName;
					////		loan.Fields["4002"].Value = lastname;
					////	}

					////	WriteLine("Would you like to set Loan Fields (36, 37)? - If so, press 'Y' and <ENTER>");
					////	line = Console.ReadLine();
					////	if(line != null && line.Trim().Length > 0 && line.ToUpper().StartsWith("Y"))
					////	{
					////		loan.Fields["36"].Value = firstName;
					////		loan.Fields["37"].Value = lastname;
					////	}

					////}


					WriteLine("");
					WriteLine("BEFORE COMMIT - loan.Guid=" + (loan.Guid != null?loan.Guid:"null"));
					WriteLine("");

                    //////setting the business rule, before commit.
                    ////loan.BusinessRulesEnabled = true;
                    //////setting the calculation, before commit.
                    ////loan.CalculationsEnabled = true;

                    loan.Commit();

					
					WriteLine("");
					WriteLine("AFTER COMMIT - loan.Guid=" + (loan.Guid != null?loan.Guid:"null"));
					WriteLine("");
					WriteLine("loan.LoanNumber=" + loan.LoanNumber);
					WriteLine("loan.LoanFolder=" + loan.LoanFolder);
					WriteLine("");

					//Return loan number and GUID after the loan is created.
					loanData.Add("Loan.LoanNumber", loan.LoanNumber);
					loanData.Add("Loan.Guid", loan.Guid);
					loanData.Add("Loan.4000", loan.Fields["4000"].UnformattedValue);
					loanData.Add("Loan.4002", loan.Fields["4002"].UnformattedValue);
					loanData.Add("Loan.36", loan.Fields["36"].UnformattedValue);
					loanData.Add("Loan.37", loan.Fields["37"].UnformattedValue);

					WriteLine("Unlocking loan...");
					loan.Unlock();
					WriteLine("loan unlocked!");
					
				}
			}
            catch (Exception ex)
            {
				WriteLine("" + ex);
                // Catch all other exceptions and raise/throw new EncompassException with custom message after logging it
                loanData.Add("Exception Occured", ex.Message);

				throw ex;
            }
            finally
            {
                loan?.Close();
            }
			return loanData; //  Ok(loanData);
        }



		int DebugPoint = 0;

		string _WriteVariable = "";
		public string WriteVariable
		{
			get
			{
				if(_WriteVariable == null) _WriteVariable = "";
				return _WriteVariable;
			}

			set
			{
				_WriteVariable = value;
			}
		}

		bool WriteToVariable = false;
		
		public void WriteLine(string message)
		{
			if(message == null) return;

			message += System.Environment.NewLine;
			Write(message);			
		}


		public void Write(string message)
		{
			if(message == null) return;
			System.Console.Write(message);

			if(this.WriteToVariable)
			{
				++DebugPoint;
				WriteVariable += message;
			}
		}




    }
}

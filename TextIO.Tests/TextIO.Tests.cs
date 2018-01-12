using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace GR.TextIO.Tests
{
	[TestClass]
	public class TextIOTests
	{

		[TestMethod]
		public void Read(string file)
		{

			Assert.IsNotNull(file);

			Assert.IsTrue(file.Trim().Length <= 0);


			if(file == null)
			{				
				return;
			}

			if(file.Trim().Length <= 0)
			{				
				return;
			}

			DelimitedTextReader textReader = new DelimitedTextReader(); //  CreateTextReader(args);
			textReader.FieldDelimiter = ",";
			textReader.RecordDelimiter = System.Environment.NewLine;
			textReader.HeaderRecordIndex = 0;

			//if(textReader == null)
			//{
			//	WriteLine("ERROR - textReader == null");
			//	return;
			//}
						
			WriteLine("");
			WriteLine("textReader.FieldDelimiter=" + textReader.FieldDelimiter);
			WriteLine("textReader.RecordDelimiter=" + textReader.RecordDelimiter);
			WriteLine("textReader.HeaderRecordIndex=" + textReader.HeaderRecordIndex);			
			WriteLine("textReader.LeftEnclosure=" + textReader.LeftEnclosure);
			WriteLine("textReader.RightEnclosure=" + textReader.RightEnclosure);
			WriteLine("textReader.CurrentEncoding=" + textReader.CurrentEncoding);
			WriteLine("");

			string OrderBy = "Gender DESC"; //  GetSortOrderBy(args);
			//if(OrderBy == null) OrderBy = "";

			//string file = GetFile(args);
			//if(file == null) file = "";

			WriteLine("File=" + file);
			WriteLine("OrderBy=" + OrderBy);
			WriteLine("");

			////•	Output 1 – sorted by gender (females before males) then by last name ascending.
			////•	Output 2 – sorted by birth date, ascending.
			////•	Output 3 – sorted by last name, descending.
			////Display dates in the format M/D/YYYY

			if(file == null || file.Trim().Length <= 0)
			{
				WriteLine("ERROR file is null");
				return;
			}

			WriteLine("");
			WriteLine("textReader.SetFieldTypeMapping...");	
			WriteLine("");	
			textReader.SetFieldTypeMapping("DOB", typeof(DateTime));


			WriteLine("");
			WriteLine("Reading file:" + file);	
			WriteLine("");			
			textReader.Read(file);


			WriteLine("");
			WriteLine("textReader.GenerateDataTable...");
			WriteLine("");
			//
			// textReader.GenerateDataTable("Table 1");
			textReader.InitializeDataTable("CURRENT_TABLE");
			System.Data.DataTable dataTable = textReader.CurrentDataTable;

			Assert.IsNotNull(dataTable);

			WriteLine("");
			WriteLine("textReader.SortDataTable...");
			WriteLine("");
			////string OrderBy = GetSortOrderBy(args);
			dataTable = textReader.SortDataTable(dataTable, OrderBy);
			Assert.IsNotNull(dataTable);

			WriteLine("");
			WriteLine("textReader.PrintDataTable...");
			WriteLine("");
			//textReader.PrintDataTable(dataTable);
			textReader.PrintDataTable(dataTable, System.Environment.NewLine, "^", Console.Out);


		
			WriteLine("");
			WriteLine("END - MainBody");
			WriteLine("");
		}


		

		void WriteLine(string mesg)
		{
			if(mesg == null) return;  // mesg = ""; 			
			mesg += System.Environment.NewLine;
			Write(mesg);
		}

		void Write(string mesg)
		{
			if(mesg == null) return;

			System.Console.Write(mesg);

			//if(this.InvokeRequired)
			//{
			//	this.Invoke(new Action(() => { Write(mesg); }));
			//	return;
			//}

			//if(!(AllowWrite)) { return; }
			
			//if(richTextBoxMessages != null && !(richTextBoxMessages.IsDisposed))
			//{
			//	//richTextBoxMessages.Text += mesg; //  +System.Environment.NewLine;
			//	richTextBoxMessages.AppendText(mesg);
			//	richTextBoxMessages.ScrollToCaret();
			//}
						
		}
		

	}
}

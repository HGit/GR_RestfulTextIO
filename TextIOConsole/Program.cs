using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
//using GR.TextIO;


namespace GR.TextIO
{
	static class Program
	{

		
		static StringArraySearcher CommandlineParameterFinder = new StringArraySearcher();


		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			BeforeMainBody(args);

			MainBody(args);

			AfterMainBody(args);
		}


		static void BeforeMainBody(string[] args)
		{
			// actions...
		}

		static void AfterMainBody(string[] args)
		{
			// actions...
		}


		// Forms...
		////static void MainBody(string[] args)
		////{			
		////	//Application.EnableVisualStyles();
		////	//Application.SetCompatibleTextRenderingDefault(false);
			
		////	// 
		////	//string line = "";			
		////	//Console.WriteLine("Press <ENTER> To Continue...");
		////	//line = Console.ReadLine();
		////			TextReader
						
		////	//Form form = new Form1();
		////	//Application.Run(form);
		////}
		
		static void MainBody(string[] args)
		{
			WriteLine("");
			WriteLine("BEGIN - MainBody");
			WriteLine("");

			//string line = "";
			//Console.WriteLine("Press <ENTER> To Continue...");
			//line = Console.ReadLine();
			
			
			DelimitedTextReader textReader = CreateTextReader(args);
			if(textReader == null)
			{
				WriteLine("ERROR - textReader == null");
				return;
			}
						
			WriteLine("");
			WriteLine("textReader.FieldDelimiter=" + textReader.FieldDelimiter);
			WriteLine("textReader.RecordDelimiter=" + textReader.RecordDelimiter);
			WriteLine("textReader.HeaderRecordIndex=" + textReader.HeaderRecordIndex);			
			WriteLine("textReader.LeftEnclosure=" + textReader.LeftEnclosure);
			WriteLine("textReader.RightEnclosure=" + textReader.RightEnclosure);
			WriteLine("textReader.CurrentEncoding=" + textReader.CurrentEncoding);
			WriteLine("");

			string OrderBy = GetSortOrderBy(args);
			if(OrderBy == null) OrderBy = "";

			string file = GetFile(args);
			if(file == null) file = "";

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
			//DataTable dataTable = textReader.GenerateDataTable("Table 1");
			textReader.InitializeDataTable("CURRENT_TABLE");
			System.Data.DataTable dataTable = textReader.CurrentDataTable;

			WriteLine("");
			WriteLine("textReader.SortDataTable...");
			WriteLine("");
			////string OrderBy = GetSortOrderBy(args);
			dataTable = textReader.SortDataTable(dataTable, OrderBy);


			WriteLine("");
			WriteLine("textReader.PrintDataTable...");
			WriteLine("");
			//textReader.PrintDataTable(dataTable);
			textReader.PrintDataTable(dataTable, System.Environment.NewLine, "^", Console.Out);


		
			WriteLine("");
			WriteLine("END - MainBody");
			WriteLine("");
		}
		
		


		private static string GetFile(string[] args)
		{

			string file = "";

			if(args == null || args.Length <= 0)
			{
				return file;
			}

			//CommandlineParameterFinder.DefaultLogicMatchingType = StringArraySearcher.DefaultLogicMatchingTypes.Equals;
			//Func<string, string, bool> matchLogic = CommandlineParameterFinder.DefaultMatchLogic;

			bool caseSensitive = false;
			//int occurrence = 1;
			int indexOffset = 1;
			SearchDetails searchDetails = null;
			
			searchDetails = CommandlineParameterFinder.FindMatch(args, "--file", caseSensitive, indexOffset); //  indexOffset);
			if(searchDetails.Matched) file = searchDetails.Value;

			return file;
		}
				

		private static string GetSortOrderBy(string[] args)
		{

			string orderBy = "";

			if(args == null || args.Length <= 0)
			{
				return orderBy;
			}

			//CommandlineParameterFinder.DefaultLogicMatchingType = StringArraySearcher.DefaultLogicMatchingTypes.Equals;
			//Func<string, string, bool> matchLogic = CommandlineParameterFinder.DefaultMatchLogic;

			bool caseSensitive = false;
			//int occurrence = 1;
			int indexOffset = 1;
			SearchDetails searchDetails = null;
			
			searchDetails = CommandlineParameterFinder.FindMatch(args, "--order-by", caseSensitive, indexOffset); //  indexOffset);
			if(searchDetails.Matched) orderBy = searchDetails.Value;

			return orderBy;
		}



		private static DelimitedTextReader CreateTextReader(string[] args)
		{

			DelimitedTextReader textReader = new DelimitedTextReader();

			if(args == null || args.Length <= 0)
			{
				return textReader;
			}

						
			//CommandlineParameterFinder.DefaultLogicMatchingType = StringArraySearcher.DefaultLogicMatchingTypes.Equals;
			//Func<string, string, bool> matchLogic = CommandlineParameterFinder.DefaultMatchLogic;

			bool caseSensitive = false;
			//int occurrence = 1;
			int indexOffset = 1;
			SearchDetails searchDetails = null;
			
			
			//searchDetails = CommandlineParameterFinder.FindMatch(args, "--file", caseSensitive, indexOffset); //  indexOffset);
			//if(searchDetails.Matched) file = searchDetails.Value;
			

			searchDetails = CommandlineParameterFinder.FindMatch(args, "--field-delimiter", caseSensitive, indexOffset);
			if(searchDetails.Matched) textReader.FieldDelimiter = searchDetails.Value;

			textReader.FieldDelimiter = textReader.FieldDelimiter.Replace("\\r", "\r").Replace("\\n", "\n").Replace("\\t", "\t").Replace("\\s", " ");

			searchDetails = CommandlineParameterFinder.FindMatch(args, "--record-delimiter", caseSensitive, indexOffset);
			if(searchDetails.Matched) textReader.RecordDelimiter = searchDetails.Value;

			textReader.RecordDelimiter = textReader.RecordDelimiter.Replace("\\r", "\r").Replace("\\n", "\n").Replace("\\t", "\t").Replace("\\s", " ");

			searchDetails = CommandlineParameterFinder.FindMatch(args, "--header-index", caseSensitive, indexOffset);
			if(searchDetails.Matched)
			{
				string headerIndexStr = searchDetails.Value;
				if(headerIndexStr != null && headerIndexStr.Trim().Length > 0)
				{
					int headerIndex = -1;
					bool success = Int32.TryParse(headerIndexStr, out headerIndex);
					if(success) textReader.HeaderRecordIndex = headerIndex;		
				}
			}
						
			
			return textReader;
		}





		static void WriteLine(string mesg)
		{
			if(mesg == null) return;  // mesg = ""; 			
			mesg += System.Environment.NewLine;
			Write(mesg);
		}

		static void Write(string mesg)
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

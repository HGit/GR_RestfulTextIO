using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;


namespace GR.Text
{
	public class DelimitedTextReader : IDelimitedTextReader, IDisposable
	{
		
		// declare default CTOR for clarity - minimize assumptions
		public DelimitedTextReader()
		{
					
		}


		public DelimitedTextReader(int headerRecordIndex, string recordDelimiter, string fieldDelimiter)
		{
			this.HeaderRecordIndex = headerRecordIndex;
			this.RecordDelimiter = recordDelimiter;
			this.FieldDelimiter = fieldDelimiter;
		}


		public DelimitedTextReader(int headerRecordIndex, string recordDelimiter, string fieldDelimiter, string LeftRightEnclosure)
		{
			this.HeaderRecordIndex = headerRecordIndex;
			this.RecordDelimiter = recordDelimiter;
			this.FieldDelimiter = fieldDelimiter;
			this.LeftEnclosure = LeftRightEnclosure;
			this.RightEnclosure = LeftRightEnclosure;
		}


		public DelimitedTextReader(int headerRecordIndex, string recordDelimiter, string fieldDelimiter, string leftEnclosure, string rightEnclosure)
		{
			this.HeaderRecordIndex = headerRecordIndex;
			this.RecordDelimiter = recordDelimiter;
			this.FieldDelimiter = fieldDelimiter;
			this.LeftEnclosure = leftEnclosure;
			this.RightEnclosure = rightEnclosure;
		}

		Dictionary<string, Func<string,object,object>> _FieldOutputFormatter = new Dictionary<string, Func<string,object,object>>();	
		Dictionary<string, Type> _FieldTypeMappings = new Dictionary<string, Type>();		
		//protected string _FileName
		protected uint MaxColumns = 0;
		public bool _IncludeRecordDelimiter = false;
		public bool _IncludeFieldDelimiter = false;
		protected int _HeaderRecordIndex = -1; //   { get; set; }
		protected string _LeftEnclosure = "";
		protected string _RightEnclosure = "";
		protected string _FieldDelimiter = "";
		protected string _RecordDelimiter = "";
		protected Encoding _CurrentEncoding = Encoding.Default;
		protected BinaryReader _CurrentBinaryReader = null;
		List<string> _CurrentRecord = null; 
		List<List<string>> _Records = null;
		

		public Dictionary<string, Func<string,object,object>> FieldOutputFormatter
		{
			get
			{
				if(_FieldOutputFormatter == null) _FieldOutputFormatter = new Dictionary<string, Func<string,object,object>>();
				return _FieldOutputFormatter;
			}

			set
			{
				_FieldOutputFormatter = value;
			}
		}

		public Dictionary<string, Type> FieldTypeMappings
		{
			get
			{
				if(_FieldTypeMappings == null) _FieldTypeMappings = new Dictionary<string, Type>();
				return _FieldTypeMappings;
			}

			set
			{
				_FieldTypeMappings = value;
			}
		}

		public bool IncludeRecordDelimiter
		{
			get
			{
				return _IncludeRecordDelimiter;
			}

			set
			{
				_IncludeRecordDelimiter = value;
			}
		}

		public bool IncludeFieldDelimiter
		{
			get
			{
				return _IncludeFieldDelimiter;
			}

			set
			{
				_IncludeFieldDelimiter = value;
			}
		}

		public int HeaderRecordIndex
		{
			get
			{				
				return _HeaderRecordIndex;
			}

			set
			{
				_HeaderRecordIndex = value;
			}
		}

		public string LeftEnclosure  
		{
			get
			{
				if(_LeftEnclosure == null) _LeftEnclosure = "";
				return _LeftEnclosure;
			}

			set
			{
				_LeftEnclosure = value;
			}
		}

		public string RightEnclosure 
		{
			get
			{
				if(_RightEnclosure == null) _RightEnclosure = "";
				return _RightEnclosure;
			}

			set
			{
				_RightEnclosure = value;
			}
		}

		public string FieldDelimiter
		{
			get
			{
				if(_FieldDelimiter == null) _FieldDelimiter = "";
				return _FieldDelimiter;
			}

			set
			{
				_FieldDelimiter = value;
			}
		}
		
		public string RecordDelimiter
		{
			get
			{
				if(_RecordDelimiter == null) _RecordDelimiter = "";
				return _RecordDelimiter;
			}

			set
			{
				_RecordDelimiter = value;
			}
		}

		public Encoding CurrentEncoding
		{
			get
			{
				return _CurrentEncoding;
			}

			// protected
			set
			{
				_CurrentEncoding = value;
			}
		}

		protected BinaryReader CurrentBinaryReader
		{
			get
			{
				return _CurrentBinaryReader;
			}

			// protected
			set
			{
				_CurrentBinaryReader = value;
			}
		}
				
		protected List<string> CurrentRecord
		{
			get
			{
				if(_CurrentRecord == null) _CurrentRecord = new List<string>();
				return _CurrentRecord;
			}

			set
			{
				_CurrentRecord = value;
			}
		}

		public List<List<string>> Records
		{
			get
			{
				if(_Records == null) _Records = new List<List<string>>();
				return _Records;
			}

			protected
			set
			{
				_Records = value;
			}
		}

		
		
		#region IDisposable_impl

		// ***************************************************************************
		// BEGIN DISPOSE IMPLEMENTATION
		// ***************************************************************************

		// To keep track of Dispose calls in case it is called repeatedly for some reason...
		private bool AlreadyDisposed = false;


		//Implement IDisposable.
		public void Dispose()
		{
			Dispose(true);
			// Below causes the Finalize (~Destructor) method to be skipped!!!
			GC.SuppressFinalize(this); // Disallow GC to clean-up this because we just did it above using Dispose(true)...
		}

		protected virtual void Dispose(bool IsSuppressingFinalize)
		{

			if(AlreadyDisposed) return;

			try
			{
				if(IsSuppressingFinalize)
				{
					// TO DO: clean up managed objects OR objects that are expecting a Dispose(bool) call
				}

				// TO DO: clean up unmanaged objects or any other objects regardless of Dispose(bool) call
				//this.Disconnect();
				if(this.CurrentBinaryReader != null)
				{
					try
					{
						WriteLine("BEGIN - this.CurrentBinaryReader.Close()");
						this.CurrentBinaryReader.Close();
						WriteLine("END - this.CurrentBinaryReader.Close()");
					}
					catch(Exception e)
					{
						Console.WriteLine("" + e);
					}
				}
			}
			catch(Exception e)
			{
				Console.WriteLine("" + e);
			}

			AlreadyDisposed = true;		
		}

		
		//ONLY IF YOU MUST - If you want to implement Finalize method, it is recommended to use Finalize and Dispose method together as shown below...
		//
		//At runtime C# destructor is automatically Converted to Finalize method
		~DelimitedTextReader()
		{
			Dispose(false);
		}

		// ***************************************************************************
		// END DISPOSE IMPLEMENTATION
		// ***************************************************************************


		#endregion

			   

		
		



		public void Read(string filepath)
		{
			if(filepath == null || filepath.Trim().Length <= 0)
			{
				// Error Message? Log?
				return;
			}
			
			// FileInfo will provide absolute path if possible...
			FileInfo fileInfo = new FileInfo(filepath);
			Read(fileInfo);			
		}


		public void Read(FileInfo fileInfo)
		{
			if(fileInfo == null)
			{
				// Error Message? Log?
				WriteLine("ERROR - fileInfo == null");
				return;
			}

			WriteLine("fileInfo:" + fileInfo.FullName);

			// I prefer static checking (e.g. File.Exists) due to some previously expereinced side effects (latency) of FileInfo in some situations (Threading, etc.)
			bool fileExists = File.Exists(fileInfo.FullName);

			if(!fileExists)
			{
				// Error Message? Log?
				WriteLine("ERROR - !fileExists");
				return;
			}

			FileStream fileStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read);
			Read(fileStream);			
		}

		
		public void Read(Stream stream)
		{
			if(stream == null)
			{
				// Error Message? Log?
				return;
			}

			this.Read(new BinaryReader(stream, this.CurrentEncoding));				
		}


		

		public void Read(BinaryReader binaryReader)
		{

			if(binaryReader == null)
			{
				// Error Message? Log?
				return;
			}

			this.MaxColumns = 0;
			this.CurrentBinaryReader = binaryReader;
			this.Records.Clear();			
			string CurrentBuffer = ""; // new StringBuilder();
			
			try
			{
				int ich = this.CurrentBinaryReader.Read();
				
				while(ich >= 0)
				{
					char ch = (char)ich;
					CurrentBuffer += ch;

					//if(this.LeftEnclosure.Length > 0 && this.RightEnclosure.Length > 0 && CurrentBuffer.ToString().StartsWith(LeftEnclosure))
					//{

					//}
					//else
					{
						// EOL / End of Record ?
						if(this.RecordDelimiter.Length > 0 && CurrentBuffer.ToString().EndsWith(this.RecordDelimiter))
						{		

							//if(CurrentBuffer.Length > this.RecordDelimiter.Length)
							string Data = CurrentBuffer.ToString().Substring(0, CurrentBuffer.Length - this.RecordDelimiter.Length);
							CurrentRecord.Add(Data);

							// clear buffer...
							CurrentBuffer = "";

							// Add delimiter?
							if(IncludeRecordDelimiter) CurrentRecord.Add(this.RecordDelimiter);

							// Add before renewing...							
							if(CurrentRecord.Count > 0) // consider if empty...
							{
								if(!(this.Records.Contains(CurrentRecord))) this.Records.Add(CurrentRecord);
							}

							// Capture/Update MaxColumns...
							if(CurrentRecord.Count > MaxColumns) MaxColumns = (uint)CurrentRecord.Count;

							// renew record (new object, NOT simply clear)
							CurrentRecord = new List<string>();

						}
						// New Field?
						else if(this.FieldDelimiter.Length > 0 && CurrentBuffer.ToString().EndsWith(this.FieldDelimiter))
						{
							bool ConsumeFieldData = true;
							string Data = CurrentBuffer.ToString().Substring(0, CurrentBuffer.Length - this.FieldDelimiter.Length);

							// Only if using Enclosures...
							if(this.LeftEnclosure.Length > 0 && this.RightEnclosure.Length > 0)
							{
								// TODO - Need to Trim() before checking???
								if(Data.StartsWith(LeftEnclosure))
								{										
									if(!(Data.EndsWith(RightEnclosure))) // keep reading and appending to buffer...
									{
										ConsumeFieldData = false;
									}
								}
							}	

							if(ConsumeFieldData)
							{
								CurrentRecord.Add(Data);

								// clear buffer...
								CurrentBuffer = "";

								// Add delimiter?
								if(this.IncludeFieldDelimiter) CurrentRecord.Add(this.FieldDelimiter);
							}
						}

					}

					// finally, read for next iteration...
					ich = this.CurrentBinaryReader.Read();

				} // end read loop


				if(CurrentBuffer.Length > 0)
				{
					CurrentRecord.Add(CurrentBuffer);
					CurrentBuffer = ""; // clear
				}

				if(CurrentRecord.Count > 0)
				{
					if(!(this.Records.Contains(CurrentRecord))) this.Records.Add(CurrentRecord);
				}					
				
			}
			catch(Exception e)
			{
				WriteLine("" + e);
			}
			finally
			{
				if(this.CurrentBinaryReader != null)
				{
					try
					{
						this.CurrentBinaryReader.Close();
					}
					catch(Exception e)
					{
						WriteLine("" + e);
					}
				}
			}


			WriteLine("Records.Count: " + this.Records.Count);
			WriteLine("MaxColumns: " + this.MaxColumns);

		}






		
		public void SetFieldTypeMapping(string fieldName, Type typeMapping)
		{
			if(fieldName == null) return;

			fieldName = fieldName.ToUpper();

			if(this.FieldTypeMappings.ContainsKey(fieldName))
			{
				this.FieldTypeMappings[fieldName] = typeMapping;
			}
			else
			{
				this.FieldTypeMappings.Add(fieldName,typeMapping);
			}			
		}
				
		public Type GetFieldTypeMapping(string fieldName)
		{
			Type typeMapping = null;
			if(fieldName == null) return typeMapping;

			fieldName = fieldName.ToUpper();

			if(this.FieldTypeMappings.ContainsKey(fieldName))
			{
				typeMapping = this.FieldTypeMappings[fieldName];
			}
			
			return typeMapping;	
		}

		public void RemoveFieldTypeMapping(string fieldName)
		{
			if(fieldName == null) return;

			fieldName = fieldName.ToUpper();

			if(this.FieldTypeMappings.ContainsKey(fieldName))
			{
				this.FieldTypeMappings.Remove(fieldName);
			}					
		}



		
		
		public void SetFieldOutputFormatter(string fieldName, Func<string,object,object> formatterAction)
		{
			if(fieldName == null) return;

			fieldName = fieldName.ToUpper();

			if(this.FieldOutputFormatter.ContainsKey(fieldName))
			{
				this.FieldOutputFormatter[fieldName] = formatterAction;
			}
			else
			{
				this.FieldOutputFormatter.Add(fieldName,formatterAction);
			}			
		}
		
		public Func<string,object,object> GetFieldOutputFormatter(string fieldName)
		{
			Func<string,object,object> formatterAction = null;
			if(fieldName == null) return formatterAction;

			fieldName = fieldName.ToUpper();

			if(this.FieldOutputFormatter.ContainsKey(fieldName))
			{
				formatterAction = this.FieldOutputFormatter[fieldName];
			}
			
			return formatterAction;	
		}

		public void RemoveFieldOutputFormatter(string fieldName)
		{
			if(fieldName == null) return;

			fieldName = fieldName.ToUpper();

			if(this.FieldOutputFormatter.ContainsKey(fieldName))
			{
				this.FieldOutputFormatter.Remove(fieldName);
			}					
		}





		public System.Data.DataTable GenerateDataTable(string tableName)
		{
			
			System.Data.DataTable dataTable = null; // new System.Data.DataTable();

			if(this.Records.Count <= 0)
			{
				return dataTable;
			}

			dataTable = new System.Data.DataTable();

			if(tableName == null || tableName.Trim().Length <= 0) tableName = "TABLE_" + DateTime.Now.Ticks;
			
			dataTable.TableName = tableName;
			
			
			if(this.HeaderRecordIndex >= 0 && this.HeaderRecordIndex < this.Records.Count)
			{
				List<string> Record = this.Records[this.HeaderRecordIndex];
				if(Record != null && Record.Count > 0)
				{
					for(int h = 0; h < Record.Count; ++h)
					{
						string HeaderName = Record[h];
						if(HeaderName == null || HeaderName.Trim().Length <= 0) HeaderName = "Column" + h;

						Type dataType = GetFieldTypeMapping(HeaderName);
						if(dataType == null) dataType = typeof(string);
						System.Data.DataColumn dc = dataTable.Columns.Add(HeaderName, dataType);
					}
				}
			}

			if(this.MaxColumns > 0 && this.MaxColumns > dataTable.Columns.Count) // dataTable.Columns.Count <= 0 &&
			{
				uint Max = this.MaxColumns - ((uint)(dataTable.Columns.Count));
				for(uint c = 0; c < Max; ++c)
				{
					System.Data.DataColumn dc = dataTable.Columns.Add("Column" + c, typeof(string));
				}
			}

			dataTable.AcceptChanges();
			

			for(int r = 0; r < this.Records.Count; ++r)
			{
				if(this.HeaderRecordIndex == r) continue; // handled Headers above...

				List<string> Record = this.Records[r];
				if(Record == null || Record.Count <= 0) continue;

				List<object> dataRowValues = new List<object>();
				for(int f = 0; f < Record.Count; ++f)
				{
					string Field = Record[f];
					dataRowValues.Add(Field);
				}

				System.Data.DataRow dataRow = dataTable.NewRow();
				dataRow.ItemArray = dataRowValues.ToArray();
				dataTable.Rows.Add(dataRow);
			}


			dataTable.AcceptChanges();

			
			return dataTable;
		}





		public DataTable SortDataTable(System.Data.DataTable dataTable, string OrderBy)
		{
			DataTable sortedTable = dataTable;

			if(dataTable == null)
			{
				WriteLine("ERROR - dataTable == null");
				return sortedTable;
			}

			if(OrderBy == null || OrderBy.Trim().Length <= 0)
			{
				WriteLine("WARNING - OrderBy == null || OrderBy.Trim().Length <= 0");
				return sortedTable;
			}

			//table.DefaultView.Sort = "Town ASC, Cutomer ASC"
			//table.Select("", "Town ASC, Cutomer ASC")

			DataView dataView = new DataView(dataTable);
			dataView.Sort = OrderBy; //  " AutoID DESC, Name DESC";
			sortedTable = dataView.ToTable();

			return sortedTable;
		}
		
		public DataTable SortDataTable(System.Data.DataTable dataTable, SortComparer sortComparer)
		{
			DataTable sortedTable = dataTable;
			
			if(dataTable == null)
			{
				WriteLine("ERROR - dataTable == null");
				return sortedTable;
			}

			if(sortComparer == null)
			{
				WriteLine("WARNING - sortComparer == null");
				return sortedTable;
			}

			if(sortComparer.SortDescriptors.Count <= 0)
			{
				WriteLine("WARNING - sortComparer.SortDescriptors.Count <= 0");
				return sortedTable;
			}


			DataRow[] dataRows = dataTable.Select();
			List<DataRow> dataRowList = new List<DataRow>();
			dataRowList.Sort(sortComparer);

			sortedTable = new DataTable();
			foreach(DataRow drow in dataRowList)
			{
				sortedTable.Rows.Add(drow);
			}

			sortedTable.AcceptChanges();

			return sortedTable;
		}

		


		public void PrintDataTable(System.Data.DataTable dataTable)
		{
			string recordDelimiter = System.Environment.NewLine;
			string fieldDelimiter = "\t";
			System.IO.TextWriter Out = Console.Out;

			PrintDataTable(dataTable, recordDelimiter, fieldDelimiter, Out);
		}

		public void PrintDataTable(System.Data.DataTable dataTable, string recordDelimiter, string fieldDelimiter, System.IO.TextWriter Out)
		{

			if(dataTable == null)
			{
				WriteLine("ERROR - dataTable == null");
				return;
			}

			if(Out == null) Out = Console.Out;

			if(fieldDelimiter == null) fieldDelimiter = "";
			if(recordDelimiter == null) recordDelimiter = "";

			string headerBuffer = "";
			string dataBuffer = "";

			// Headers...			
			foreach(DataColumn dc in dataTable.Columns)
			{
				headerBuffer += (headerBuffer.Length > 0?fieldDelimiter:"") + dc.ColumnName;
			}
			
			// Data...
			foreach(DataRow drow in dataTable.Rows)
			{
				string rowBuffer = "";
				foreach(DataColumn dc in dataTable.Columns)
				{
					object fieldData = drow[dc];
					//if(fieldData == null) fieldData = "<NULL>";

					Func<string, object, object> formatterAction = GetFieldOutputFormatter(dc.ColumnName);
					if(formatterAction != null) fieldData = formatterAction.Invoke(dc.ColumnName, fieldData);

					if(fieldData == null) fieldData = "<NULL>";

					rowBuffer += (rowBuffer.Length > 0?fieldDelimiter:"") + fieldData;
				}

				dataBuffer += (dataBuffer.Length > 0?recordDelimiter:"") + rowBuffer;
			}
			
			Out.WriteLine(headerBuffer + System.Environment.NewLine + dataBuffer);
		}
		

		


		protected void WriteLine(string mesg)
		{
			if(mesg == null) return;  // mesg = ""; 			
			mesg += System.Environment.NewLine;
			Write(mesg);
		}

		protected void Write(string mesg)
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

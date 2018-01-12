using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GR.TextIO
{
	interface IDelimitedTextReader : ITextReader
	{	

		string FieldDelimiter { get; set; }
		string RecordDelimiter { get; set; }
		int HeaderRecordIndex  { get; set; }

		string LeftEnclosure  { get; set; }
		string RightEnclosure  { get; set; }
	}
}

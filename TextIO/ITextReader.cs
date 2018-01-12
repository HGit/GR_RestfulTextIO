using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace GR.TextIO
{
	public interface ITextReader
	{

		Encoding CurrentEncoding { get; set; }
		DataTable CurrentDataTable  { get; set; }

		void Read(string filepath);
		void Read(FileInfo fileInfo);
		void Read(Stream stream);
		void Read(TextReader stream);
		//void Read(string str);
		void Read(BinaryReader fileInfo);

	}

}

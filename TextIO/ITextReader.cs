using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace GR.Text
{
	public interface ITextReader
	{

		Encoding CurrentEncoding { get; set; }

		void Read(string filepath);
		void Read(FileInfo fileInfo);
		void Read(BinaryReader fileInfo);
	}

}

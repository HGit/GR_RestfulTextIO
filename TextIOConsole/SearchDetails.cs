using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GR.TextIO
{
	public class SearchDetails
	{

		public SearchDetails()
		{

		}


		protected int _Occurrence = 1; // 1st by default
		protected int _MatchIndex = -1; // -1 = not set
		protected string _MatchValue = "";
		protected int _MatchOffsetIndex = -1; // -1 = not set
		protected string _MatchOffsetValue = "";
		//protected string _Value = "";
		//protected bool _Success = false;
		

		
		public bool Matched 
		{
			get
			{				
				return (_MatchIndex >= 0);
			}			
		}


		public int Occurrence 
		{
			get
			{				
				return _Occurrence;
			}

			set
			{
				_Occurrence = value;
			}
		}
		

		public int MatchIndex 
		{
			get
			{				
				return _MatchIndex;
			}

			set
			{
				_MatchIndex = value;
			}
		}
		

		public string MatchValue
		{
			get
			{
				if(_MatchValue == null) _MatchValue = "";
				return _MatchValue;
			}

			set
			{
				_MatchValue = value;
			}
		}
				

		public int MatchOffsetIndex 
		{
			get
			{				
				return _MatchOffsetIndex;
			}

			set
			{
				_MatchOffsetIndex = value;
			}
		}


		public string MatchOffsetValue
		{
			get
			{
				if(_MatchOffsetValue == null) _MatchOffsetValue = "";
				return _MatchOffsetValue;
			}

			set
			{
				_MatchOffsetValue = value;
			}
		}


        // final value after considering match offset, etc, etc...
		public string Value
		{
			get
			{
				return (this.MatchOffsetIndex >= 0 ? this.MatchOffsetValue : this.MatchValue);				
			}

			//set
			//{
			//	_MatchOffsetValue = value;
			//}
		}



	}

}

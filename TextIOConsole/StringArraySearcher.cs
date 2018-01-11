using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GR.Text
{


	/*
	
		EXAMPLE:

			StringArraySearcher CommandlineParameterFinder = new StringArraySearcher();
			//Func<string, string, bool> matchLogic = CommandlineParameterFinder.DefaultMatchLogic;

			bool caseSensitive = false;
			//int occurrence = 1;
			int indexOffset = 1;

			SearchDetails searchDetails = null;
			
			//searchDetails = CommandlineParameterFinder.FindMatch(args, "--interactive", caseSensitive, 0);

			searchDetails = CommandlineParameterFinder.FindMatch(args, "--encompass_server", caseSensitive, indexOffset);
			if(searchDetails.Matched) serverURI = searchDetails.Value;


	*/
	public class StringArraySearcher // <string[]> where string[] : IList<string>, IList // , ICollection<string>, IEnumerable<string> 
												   // public abstract class Array : ICloneable, IList, ICollection, IEnumerable, IStructuralComparable, IStructuralEquatable
	{
		

		public StringArraySearcher()
		{
					
		}


		
		


        protected int _Occurrence = 1;
        protected int _IndexOffsetFromMatch = 0;
        protected bool _CaseSensitive = false;

		protected System.Text.RegularExpressions.RegexOptions _RegexOptions = System.Text.RegularExpressions.RegexOptions.None;			
		//protected bool _AsRegex = false;

		protected Func<string, string, bool> _DefaultMatchLogic = null;

		public enum DefaultLogicMatchingTypes : int
        {
             Equals = 1
            ,StartsWith = 2
            ,EndsWith = 3
        }


        DefaultLogicMatchingTypes _DefaultLogicMatchingType = DefaultLogicMatchingTypes.Equals;
        public DefaultLogicMatchingTypes DefaultLogicMatchingType
        {
            get
            {
                return _DefaultLogicMatchingType;
            }
            set
            {
                _DefaultLogicMatchingType = value;
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

        public int IndexOffsetFromMatch
        {
            get
            {
                return _IndexOffsetFromMatch;
            }
            set
            {
                _IndexOffsetFromMatch = value;
            }
        }
        
        public bool CaseSensitive
        {
            get
            {
                return _CaseSensitive;
            }
            set
            {
                _CaseSensitive = value;
            }
        }

        public bool UseEqualsInDefaultMatchLogic = true;

		public System.Text.RegularExpressions.RegexOptions RegexOptions
		{
			get
			{
				return _RegexOptions;
			}

			set
			{
				_RegexOptions = value;
			}
		}


		//public bool AsRegex
		//{
		//	get
		//	{
		//		return _AsRegex;
		//	}

		//	set
		//	{
		//		_AsRegex = value;
		//	}
		//}

        
		
		public virtual Func<string, string, bool> DefaultMatchLogic
		{
			get
			{
				if(_DefaultMatchLogic == null)
				{
					_DefaultMatchLogic = new Func<string, string, bool>((param, matchpattern)=>
					{
						bool matched = false;

						if(param == null && matchpattern == null)
						{
							return matched;
						}
						else if(param == null && matchpattern != null)
						{
							return matched;
						}
						else if(param != null && matchpattern == null)
						{
							return matched;
						}
                        

                        if (!this.CaseSensitive)
                        {
                            param = param.ToLower();
                            matchpattern = matchpattern.ToLower();
                        }

                        if(DefaultLogicMatchingType == DefaultLogicMatchingTypes.StartsWith)
                        {
                            matched = matchpattern.StartsWith(param);
                        }
                        else if(DefaultLogicMatchingType == DefaultLogicMatchingTypes.EndsWith)
                        {
                            matched = matchpattern.EndsWith(param);
                        }
                        else if(DefaultLogicMatchingType == DefaultLogicMatchingTypes.Equals)
                        {
                            matched = matchpattern.Equals(param);
                        }
                        else
                        {
                            // default is Equals
                             matched = matchpattern.Equals(param);
                        }

						return matched;
					}
					);
				}

				return _DefaultMatchLogic;
			}

			set
			{
				_DefaultMatchLogic = value;
			}
		}







		public SearchDetails FindMatch(string[] args, string paramName)
		{

			//bool caseSensitive = true;
            //this.CaseSensitive = true;
			//int occurrence = 1;
			//int indexOffsetFromMatch = 0;
			return FindMatch(args, paramName, this.CaseSensitive, this.Occurrence, this.IndexOffsetFromMatch);

		}


		public SearchDetails FindMatch(string[] args, string paramName, int indexOffsetFromMatch)
		{

            //bool caseSensitive = true;
            //int occurrence = 1;
            this.IndexOffsetFromMatch = indexOffsetFromMatch;
			return FindMatch(args, paramName, this.CaseSensitive, this.Occurrence, this.IndexOffsetFromMatch);

		}


		
		public SearchDetails FindMatch(string[] args, string paramName, bool caseSensitive)
		{

			this.CaseSensitive = caseSensitive;
			//int occurrence = 1;
			//int indexOffsetFromMatch = 0;
			//return FindMatch( args, caseSensitive, occurrence, paramName, 0 );
			return FindMatch(args, paramName, this.CaseSensitive, this.Occurrence, this.IndexOffsetFromMatch);

		}


		
		public SearchDetails FindMatch(string[] args, string paramName, bool caseSensitive, int indexOffsetFromMatch)
		{
            this.CaseSensitive = caseSensitive;
            this.IndexOffsetFromMatch = indexOffsetFromMatch;
			//int occurrence = 1;
			//return FindMatch( args, caseSensitive, occurrence, paramName, indexOffsetFromMatch );
			return FindMatch(args, paramName, this.CaseSensitive, this.Occurrence, this.IndexOffsetFromMatch);
		}

		
		
		public SearchDetails FindMatch(string[] args, string paramName, bool caseSensitive, int occurrence, int indexOffsetFromMatch)
		{

			// A default matching function...
			//Func<string, string, bool> matchLogic 

            this.CaseSensitive = caseSensitive;
            this.IndexOffsetFromMatch = indexOffsetFromMatch;
			this.Occurrence = occurrence;

			return FindMatch(args, paramName, this.CaseSensitive, this.Occurrence, this.IndexOffsetFromMatch
								, this.DefaultMatchLogic // null //  Func < string, string, bool > matchLogic
								);

		}





		// primary
		public SearchDetails FindMatch(string[] args, string paramName, bool _caseSensitive
										  , int _occurrence
										  , int _indexOffsetFromMatch
										  ,Func<string,string,bool> matchLogic
										 )
		{

			SearchDetails retval = new SearchDetails();

            this.CaseSensitive = _caseSensitive;
            this.Occurrence = _occurrence;
            this.IndexOffsetFromMatch = _indexOffsetFromMatch;



			if (paramName == null || "".Equals(paramName))
			{
				return retval;
			}

			if (args == null || args.Length <= 0)
			{
				return retval;
			}

			if(matchLogic == null) matchLogic = this.DefaultMatchLogic;

		
			//List<StringParameterDetails> parameters = this.FindMatchs(args, paramName, caseSensitive, occurrence, indexOffsetFromMatch, matchLogic);
			List<SearchDetails> parameters = this.FindMatches(args, paramName, this.CaseSensitive, this.IndexOffsetFromMatch, matchLogic);

			if(parameters != null && parameters.Count > 0)
			{

				if (this.Occurrence == 0) { this.Occurrence = 1; } // only if occurance == 0 then set to 1

				int occurrenceCounter = 0;				
                				
                // start from beginning (ASC order)
				if(this.Occurrence > 0)
				{
                    int x = 0;
					for(; x < parameters.Count; ++x)
					{
						++occurrenceCounter;
						if(occurrenceCounter == this.Occurrence)
						{
							retval = parameters[x];
							break;
						}
					}					
				}
				// else if occurrence < 0, start from end (DESC order) and work backwards...
				else
				{
					int x = parameters.Count - 1;
					for(; x >= 0; --x)
					{
						--occurrenceCounter;
						if(occurrenceCounter == this.Occurrence)
						{
							retval = parameters[x];
							break;
						}
					}
				}						

				//retval = parameters[0];
			}


			if(retval == null) retval = new SearchDetails();

			return retval;

		}






		// primary
		public List<SearchDetails> FindMatches(string[] args, string matchpattern, bool _caseSensitive 
											//, int occurrence
										  , int _indexOffsetFromMatch
										  , Func<string, string, bool> matchLogic
										 )
		{

            this.CaseSensitive = _caseSensitive;
            this.IndexOffsetFromMatch = _indexOffsetFromMatch;


			List<SearchDetails> parameters = new List<SearchDetails>();

			//string retval = "";

			if(matchpattern == null || "".Equals(matchpattern))
			{
				return parameters;
			}

			if(args == null || args.Length <= 0)
			{
				return parameters;
			}

			if(matchLogic == null) matchLogic = this.DefaultMatchLogic;

            // If still null, simply return an empty list...
            if(matchLogic == null) return parameters;
			
			
			int occurrenceCount = 0;

			for (int c = 0; c < args.Length; c++)
			{
				if (args[c] == null) { continue; }


				bool matched = false;


				string finalmatchpattern = matchpattern;
				string matchtext = args[c];
				

               
				////if(AsRegex)
				////{					
				////	if(!caseSensitive) this.RegexOptions |= System.Text.RegularExpressions.RegexOptions.IgnoreCase;

				////	System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(matchpattern, this.RegexOptions); // use original pattern w/out lowering, etc. 					
				////	matched = regex.IsMatch(matchtext); // use original text w/out lowering, etc. 

				////	//// TESTING
				////	//if(matched) System.Console.WriteLine("***** REGEX MATCHED! *****");					
				////}
				////else
				////{

				////	if (!caseSensitive)
				////	{
				////		finalmatchpattern = finalmatchpattern.ToLower();
				////		matchtext = matchtext.ToLower();
				////	}

				////	if(matchLogic != null)
				////	{
				////		matched = matchLogic.Invoke(finalmatchpattern, matchtext);
				////	}
				////	else
				////	{
				////		matched = matchtext.Equals(finalmatchpattern);
				////	}
				////}

                 matched =  matchLogic.Invoke(finalmatchpattern, matchtext);


				if(matched)
				{
					++occurrenceCount;

					SearchDetails stringParameterDetails = new SearchDetails();
					stringParameterDetails.Occurrence = occurrenceCount;
					stringParameterDetails.MatchValue = matchtext;
					stringParameterDetails.MatchIndex = c;
					parameters.Add(stringParameterDetails);

					if(this.IndexOffsetFromMatch != 0)
					{
						int offsetIndex = this.IndexOffsetFromMatch + c;
						if(offsetIndex >= 0 && offsetIndex < args.Length)
						{
							//retval = args[offsetIndex];
							stringParameterDetails.MatchOffsetValue = args[offsetIndex]; // retval;
							stringParameterDetails.MatchOffsetIndex = offsetIndex;
							//parameters.Add(stringParameterDetails); // when returning all matched elements
						}
						////else
						////{
						////	retval = String.Empty; // "ERROR: (indexOffsetFromMatch + c) -> (" + indexOffsetFromMatch + " + " + c + ")"; //  String.Empty; // otherwise, return empty string
						////	parameters.Add(stringParameterDetails); // when returning all matched elements
						////}
					}

					////if( occurrence >= 0 && occurrenceCount == occurrence) break; // skip this check when returning all matched elements											
				}


			} // loop
			
			

			return parameters;

		}
				

	}



	

}

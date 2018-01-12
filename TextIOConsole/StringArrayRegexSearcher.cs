using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GR.TextIO
{
    public class StringArrayRegexSearcher : StringArraySearcher
    {

        public StringArrayRegexSearcher() : base()
        {

        }




        
		public override Func<string, string, bool> DefaultMatchLogic
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


                        //System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(param);
                        //matched = regex.IsMatch(matchpattern);                            
                        if(!this.CaseSensitive) this.RegexOptions |= System.Text.RegularExpressions.RegexOptions.IgnoreCase;
                        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(matchpattern, this.RegexOptions); // use original pattern w/out lowering, etc. 					
                        matched = regex.IsMatch(matchpattern); // use original text w/out lowering, etc. 
                        
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


    }
}

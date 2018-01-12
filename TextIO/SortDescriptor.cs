using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace GR.TextIO
{

    public class SortDescriptor // <T> where T : IComparable<T>
    {



        public SortDescriptor(string sortPropertyName) 
        {
            this.SortPropertyName = sortPropertyName; 
        }



        public SortDescriptor(string sortPropertyName, System.ComponentModel.ListSortDirection sortDirection) 
        {
            this.SortPropertyName = sortPropertyName;
            this.SortDirection = sortDirection; 
        }


        


        string _SortPropertyName = null;
        ListSortDirection _SortDirection = ListSortDirection.Ascending; // SortOrder.Ascending;


       
        public string SortPropertyName
        {
            get
            {
                if (_SortPropertyName == null) { _SortPropertyName = ""; }
                return _SortPropertyName;
            }

            set
            {
                _SortPropertyName = value;
            }
        }

        
        public ListSortDirection SortDirection
        {
            get
            {
                return _SortDirection;
            }

            set
            {
                _SortDirection = value;
            }
        }



    }

}

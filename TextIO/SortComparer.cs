using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;

namespace GR.TextIO
{


    public class SortComparer : IComparer<DataRow>
    {

        public SortComparer()
        {

        }


		public SortComparer(ListSortDirection sortDirection)
        {			
			this.SortDirection = sortDirection;
        }


		public SortComparer(List<SortDescriptor> sortExpressions)
        {
			this.SortDescriptors = sortExpressions;
        }


		public SortComparer(List<SortDescriptor> sortExpressions, ListSortDirection sortDirection)
        {
			this.SortDescriptors = sortExpressions;
			this.SortDirection = sortDirection;
        }




        // HEG 
        ListSortDirection _SortDirection = ListSortDirection.Ascending; // SortOrder.Ascending;     
		// HEG 
        List<SortDescriptor> _SortDescriptors = null;



		// HEG 
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
      
		// HEG 
        public List<SortDescriptor> SortDescriptors
        {
            get
            {
                if (_SortDescriptors == null) { _SortDescriptors = new List<SortDescriptor>(); }
                return _SortDescriptors;
            }

            set
            {
                _SortDescriptors = value;
            }
        }




		// HEG 
        public void ClearSortExpressions()
        {
            this.SortDescriptors = null;
        }

        ////// HEG        
        ////public void SetSortExpressions(List<SortDescriptor> sortExpressions)
        ////{
        ////    //this.SortDescriptors.Clear();

        ////    //if (sortExpressions == null || sortExpressions.Count <= 0)
        ////    //{
        ////    //    return;
        ////    //}

        ////    this.SortDescriptors = sortExpressions; //.AddRange(sortExpressions);

        ////}

		


		// IComparer<T>
		//public int Compare(string left, string right)
        public virtual int Compare(DataRow left, DataRow right)      
        {
            int retval = 0;

            if (left == null && right == null)
            {
                retval = 0;
            }
            else if (left != null && right == null)
            {
                retval = 1;
                if (SortDirection == ListSortDirection.Descending) { retval *= -1; }
            }
            else if (left == null && right != null)
            {
                retval = -1;
                if (SortDirection == ListSortDirection.Descending) { retval *= -1; }
            }
            else
            {

                if (this.SortDescriptors.Count > 0)
                {
                    
                    for (int se = 0; se < this.SortDescriptors.Count; ++se)
                    {
                        SortDescriptor sortExpression = this.SortDescriptors[se];
                        if (sortExpression == null || sortExpression.SortPropertyName == null || sortExpression.SortPropertyName.Trim().Length <= 0)
                        {
                            continue;
                        }

                        ListSortDirection sortExpressionDirection = sortExpression.SortDirection; //  ListSortDirection.Ascending;

						//DynamicProperty leftdp = left.GetProperty(sortExpression.SortPropertyName);
						//DynamicProperty rightdp = right.GetProperty(sortExpression.SortPropertyName);
						//object leftValue = leftdp.Value; // SurfaceValue
						//object rightValue = rightdp.Value; // SurfaceValue
						object leftValue = left[sortExpression.SortPropertyName]; // .Value; // SurfaceValue
						object rightValue = right[sortExpression.SortPropertyName]; // rightdp.Value; // SurfaceValue


						if (leftValue == null && rightValue == null) { retval = 0; }
                        else if (leftValue != null && rightValue == null) { retval = 1; }
                        else if (leftValue == null && rightValue != null) { retval = -1; }
                        else if (leftValue is IComparable && rightValue is IComparable)
                        {
                            retval = ((IComparable)leftValue).CompareTo((IComparable)rightValue);
                        }

                        if (retval != 0)
                        {
                            //if (this.AllowSortExpressionDirections)
                            //{
                            //    //Console.WriteLine("1");
                            //    if (sortExpressionDirection == ListSortDirection.Descending) { retval *= -1; }
                            //}
                            //else
                            //{
                            //    if (this.SortDirection == ListSortDirection.Descending) { retval *= -1; }
                            //}
                            // Just keep it simple... if there are Expressions, THEN use the Expressions!
                            if (sortExpressionDirection == ListSortDirection.Descending) { retval *= -1; }

                            break;
                        }

                    } // end loop

                }
                //else
                //{
                //    retval = left.CompareTo(right);
                //    if (retval != 0)
                //    {
                //        if (SortDirection == ListSortDirection.Descending) { retval *= -1; }
                //    }
                //}

            }

            return retval;
        }

		



    }
}

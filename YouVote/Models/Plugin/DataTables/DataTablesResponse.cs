using System.Collections.Generic;
using System.Runtime.Serialization;

namespace YouVote.Models.Plugin.DataTables
{
	public class DataTablesResponse
	{
		public DataTablesResponse()
		{
			sEcho = sEcho;
			iTotalRecords = iTotalRecords;
			iTotalDisplayRecords = iTotalDisplayRecords;
			aaData = aaData;
		}

		/// <summary>
		/// An unaltered copy of sEcho sent from the client side. 
		/// This parameter will change with each draw (it is basically a draw count) - 
		/// so it is important that this is implemented. Note that it strongly recommended 
		/// for security reasons that you 'cast' this parameter to an integer 
		/// in order to prevent Cross Site Scripting (XSS) attacks.
		/// </summary>        
		[DataMember]
		public string sEcho { get; set; }

		/// <summary>
		/// Total records, before filtering (i.e. the total number of records in the database)
		/// </summary>
		[DataMember]
		public int iTotalRecords { get; set; }

		/// <summary>
		/// Total records, after filtering (i.e. the total number of records after filtering has been applied - 
		/// not just the number of records being returned in this result set)
		/// </summary>
		[DataMember]
		public int iTotalDisplayRecords { get; set; }

		/// <summary>
		/// Optional - this is a string of column names, comma separated (used in combination with sName) 
		/// which will allow DataTables to reorder data on the client-side if required for display
		/// </summary>
		//[DataMember]
		//public string sColumns { get; set; }

		/// <summary>
		/// The data in a 2D array
		/// Fill this structure with the plain table data
		/// represented as string.
		/// </summary>
		[DataMember]
		public IEnumerable<object> aaData { get; set; }
		//public List<List<string>> aaData { get; set; }
	}
}
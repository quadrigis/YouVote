using System.Collections.Generic;

namespace YouVote.Models.Plugin.DataTables
{
	/// <summary>
	/// This class represents an MVC Action result for
	/// a jquery.datatables response.
	/// </summary>
	public class DataTablesResult : JsonNetResult
	{
		public DataTablesResult(string sEcho, int iTotalRecords, int iTotalDisplayRecords, IEnumerable<object> aaData)
		{
			var dataTablesRespone = new DataTablesResponse();

			dataTablesRespone.sEcho = sEcho;
			dataTablesRespone.iTotalRecords = iTotalRecords;
			dataTablesRespone.iTotalDisplayRecords = iTotalDisplayRecords;
			dataTablesRespone.aaData = aaData;

			Data = dataTablesRespone;
		}

		public DataTablesResult(DataTablesRequest dataTable, int iTotalRecords, int iTotalDisplayRecords, IEnumerable<object> aaData)
			: this(dataTable.sEcho, iTotalRecords, iTotalDisplayRecords, aaData)
		{
		}

		public DataTablesResult(DataTablesRequest dataTable)
			: this(dataTable.sEcho, 0, 0, null)
		{
		}

		public DataTablesResult(DataTablesResponse response)
		{
			Data = response;
		}
	}
}
namespace YouVote.Model.Domain.Audyt
{
	public class AudytLogItem
	{
		public string PropertyName { get; set; }

		public string OldValue { get; set; }

		public string NewValue { get; set; }
	}
}
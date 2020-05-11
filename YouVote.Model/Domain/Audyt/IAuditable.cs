namespace YouVote.Model.Domain.Audyt
{
	public interface IAuditable
	{
		string IdString { get; }

		string IdType { get; }

		string ObjectType { get; }
	}
}
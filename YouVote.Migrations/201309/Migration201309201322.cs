using System.Data;
using MigSharp;

namespace YouVote.Migrations
{
    [MigrationExport(ModuleName = "YouVote", Tag = "Permission")]
    public class Migration201309201322 : IReversibleMigration
    {
        private const string Table = "Permission";

        public void Up(IDatabase db)
        {
            db.CreateTable(Table)
                .WithPrimaryKeyColumn("Id", DbType.Int32)
                .WithNotNullableColumn("Name", DbType.AnsiString)
                .WithNullableColumn("Describe", DbType.AnsiString);
        }

        public void Down(IDatabase db)
        {
            db.Tables[Table].Drop();
        }
    }
}

using System.Data;
using MigSharp;

namespace YouVote.Migrations
{
    [MigrationExport(ModuleName = "YouVote", Tag = "Initial migration")]
    public class Migration201309041303 : IReversibleMigration
    {
        private const string Table = "HiLo";

        public void Up(IDatabase db)
        {
            db.CreateTable(Table)
                .WithPrimaryKeyColumn("TableName", DbType.AnsiString).OfSize(30)
                .WithNotNullableColumn("NextValue", DbType.Int32);
        }

        public void Down(IDatabase db)
        {
            db.Tables[Table].Drop();
        }
    }
}

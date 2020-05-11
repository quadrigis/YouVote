using System.Data;
using MigSharp;

namespace YouVote.Migrations
{
    [MigrationExport(ModuleName = "YouVote", Tag = "AudytLog")]
    public class Migration201309250839 : IReversibleMigration
    {
        private const string Table = "AudytLog";

        public void Up(IDatabase db)
        {
            db.Execute(@"INSERT INTO HiLo VALUES ('"+Table+"',1)");

            db.CreateTable(Table)
                .WithPrimaryKeyColumn("Id", DbType.Int32)
                .WithNotNullableColumn("LogDateTime", DbType.DateTime)
                .WithNullableColumn("UserLogin", DbType.AnsiString)
                .WithNotNullableColumn("AudytLogType", DbType.Int32)
                .WithNullableColumn("ObjectType", DbType.AnsiString)
                .WithNullableColumn("ObjectId", DbType.AnsiString)
                .WithNullableColumn("ObjectIdType", DbType.AnsiString)
                .WithNullableColumn("ObjectValue", DbType.AnsiString);
        }

        public void Down(IDatabase db)
        {
            db.Tables[Table].Drop();
            db.Execute(@"DELETE HiLo WHERE Tablename = '"+Table+"'");
        }
    }
}

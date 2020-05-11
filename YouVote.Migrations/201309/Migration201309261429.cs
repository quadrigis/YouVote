using System.Data;
using MigSharp;

namespace YouVote.Migrations
{
    [MigrationExport(ModuleName = "YouVote", Tag = "SysUser")]
    public class Migration201309261429 : IReversibleMigration
    {
        private const string Table = "SysUser";
        private const string Table2 = "SysUserSysRole";

        public void Up(IDatabase db)
        {
            db.Execute(@"INSERT INTO HiLo VALUES ('" + Table + "',2)");

            db.CreateTable(Table)
                .WithPrimaryKeyColumn("Id", DbType.Int32)
                .WithNotNullableColumn("Email", DbType.AnsiString).OfSize(100).Unique()
                .WithNotNullableColumn("Password", DbType.AnsiString)

                .WithNullableColumn("FirstName", DbType.AnsiString).OfSize(200)
                .WithNullableColumn("LastName", DbType.AnsiString).OfSize(200)
                .WithNullableColumn("Address", DbType.AnsiString)
                .WithNullableColumn("Age", DbType.Int32)
                .WithNullableColumn("PersonalityNumber", DbType.AnsiString).OfSize(100)
                .WithNullableColumn("PersonalityNumberTypeId", DbType.Int32)

                .WithNotNullableColumn("RecordVersion", DbType.Int32)
                .WithNotNullableColumn("IsActive", DbType.Byte);

            db.CreateTable(Table2)
                .WithNotNullableColumn("SysUserId", DbType.Int32)
                .WithNotNullableColumn("SysRoleId", DbType.Int32);

            db.Tables[Table2].AddForeignKeyTo(Table, "FK_User_Role").Through("SysUserId", "Id");
            db.Tables[Table2].AddForeignKeyTo("SysRole", "FK_User_Role2").Through("SysRoleId", "Id");
            db.Tables[Table].AddForeignKeyTo("Dictionary", "FK_User_Dictionary").Through("PersonalityNumberTypeId", "Id");
        }

        public void Down(IDatabase db)
        {
            db.Tables[Table2].ForeignKeys["FK_User_Role"].Drop();
            db.Tables[Table2].ForeignKeys["FK_User_Role2"].Drop();
            db.Tables[Table].ForeignKeys["FK_User_Dictionary"].Drop();
            db.Tables[Table2].Drop();
            db.Tables[Table].Drop();
            db.Execute(@"DELETE HiLo WHERE Tablename = '" + Table + "'");
        }
    }
}

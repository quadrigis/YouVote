using System.Data;
using MigSharp;

namespace YouVote.Migrations
{
    [MigrationExport(ModuleName = "YouVote", Tag = "SysRole")]
    public class Migration201309251001 : IReversibleMigration
    {
        private const string Table = "SysRole";
        private const string Table2 = "SysRolePermission";

        public void Up(IDatabase db)
        {
            db.Execute(@"INSERT INTO HiLo VALUES ('"+Table+"',2)");
            db.CreateTable(Table)
                .WithPrimaryKeyColumn("Id", DbType.Int32)
                .WithNotNullableColumn("Name", DbType.AnsiString)
                .WithNullableColumn("Describe", DbType.AnsiString)
                .WithNullableColumn("RecordVersion", DbType.Int32);

            db.CreateTable(Table2)
                .WithNotNullableColumn("SysRoleId", DbType.Int32)
                .WithNotNullableColumn("PermissionId", DbType.Int32);

            db.Tables[Table2].AddForeignKeyTo(Table, "FK_Role_Permission1").Through("SysRoleId", "Id");
            db.Tables[Table2].AddForeignKeyTo("Permission", "FK_Role_Permission2").Through("PermissionId", "Id");
        }

        public void Down(IDatabase db)
        {
            db.Tables[Table2].ForeignKeys["FK_Role_Permission1"].Drop();
            db.Tables[Table2].ForeignKeys["FK_Role_Permission2"].Drop();
            db.Tables[Table2].Drop();

            db.Tables[Table].Drop();
            db.Execute(@"DELETE HiLo WHERE Tablename = '"+Table+"'");
        }
    }
}

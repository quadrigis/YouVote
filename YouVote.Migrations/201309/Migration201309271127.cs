using System.Data;
using MigSharp;

namespace YouVote.Migrations
{
    [MigrationExport(ModuleName = "YouVote", Tag = "Adding user and lang")]
    public class Migration201309271127 : IReversibleMigration
    {
        public void Up(IDatabase db)
        {
            db.Execute(@"INSERT INTO Permission VALUES (1, 'Full', 'Full access to the system')");
            db.Execute(@"INSERT INTO Language VALUES (1, 'PL')");
            db.Execute(@"INSERT INTO Language VALUES (2, 'ENG')");
            db.Execute(@"INSERT INTO SysRole VALUES (1, 'Admin', 'Full access to the system',1)");
            db.Execute(@"INSERT INTO SysRolePermission VALUES (1, 1)");
            db.Execute(@"INSERT INTO SysUser VALUES (1,'admin@admin.pl','zDF2QS/VNjU/3cvYnuQG64wYCkMIYo7u',NULL,NULL,NULL,NULL,NULL,NULL,1,1)");
            db.Execute(@"INSERT INTO SysUserSysRole VALUES (1, 1)");


            db.Execute(@"INSERT INTO DictionaryType VALUES (1, 'PersonalityNumberType')");
            db.Execute(@"INSERT INTO Dictionary VALUES (1, 'Pesel','Polish personality uniqe number',1,1,1,1)");
        }

        public void Down(IDatabase db)
        {
            db.Execute(@"DELETE SysUserSysRole");
            db.Execute(@"DELETE SysRolePermission");
            db.Execute(@"DELETE SysRole");
            db.Execute(@"DELETE SysUser");

            db.Execute(@"DELETE Dictionary");
            db.Execute(@"DELETE DictionaryType");

            db.Execute(@"DELETE Permission");
            db.Execute(@"DELETE Language");
        }
    }
}

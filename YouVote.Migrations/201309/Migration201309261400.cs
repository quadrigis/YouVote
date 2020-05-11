using System.Data;
using MigSharp;

namespace YouVote.Migrations
{
    [MigrationExport(ModuleName = "YouVote", Tag = "Dictionary")]
    public class Migration201309261400 : IReversibleMigration
    {
        private const string Table = "Dictionary";
        private const string Table2 = "DictionaryType";
        private const string Table3 = "Language";

        public void Up(IDatabase db)
        {
            db.CreateTable(Table)
                .WithPrimaryKeyColumn("Id", DbType.Int32)
                .WithNotNullableColumn("Value", DbType.AnsiString)
                .WithNotNullableColumn("ShortValue", DbType.AnsiString).OfSize(50)
                .WithNotNullableColumn("DictionaryTypeId", DbType.Int32)
                .WithNotNullableColumn("LanguageId", DbType.Int32)
                .WithNotNullableColumn("Order", DbType.Int32)
                .WithNotNullableColumn("IsActive", DbType.Byte);

            db.CreateTable(Table2)
                .WithPrimaryKeyColumn("Id", DbType.Int32)
                .WithNotNullableColumn("Name", DbType.AnsiString).OfSize(50);

            db.CreateTable(Table3)
                .WithPrimaryKeyColumn("Id", DbType.Int32)
                .WithNotNullableColumn("Name", DbType.AnsiString).OfSize(50);

            db.Tables[Table].AddForeignKeyTo(Table2, "FK_Dictionary_Type").Through("DictionaryTypeId", "Id");
            db.Tables[Table].AddForeignKeyTo(Table3, "FK_Dictionary_Lang").Through("LanguageId", "Id");
        }

        public void Down(IDatabase db)
        {
            db.Tables[Table].ForeignKeys["FK_Dictionary_Type"].Drop();
            db.Tables[Table].ForeignKeys["FK_Dictionary_Lang"].Drop();
            db.Tables[Table2].Drop();
            db.Tables[Table3].Drop();
            db.Tables[Table].Drop();
        }
    }
}

using YouVote.Model.Domain.Audyt;

namespace YouVote.Model.Domain.Dictionary
{
    public class Dictionary : Auditable
	{
        public virtual DictionaryType DictionaryType { get; set; }

        public virtual Language.Language Language { get; set; } 

		public virtual string Value { get; set; }

		public virtual string ShortValue { get; set; }

		public virtual int Order { get; set; }

		public virtual bool IsActive { get; set; }
	}
}
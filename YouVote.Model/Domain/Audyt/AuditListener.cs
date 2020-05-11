using System;
using System.Collections.Generic;
using NHibernate.Event;
using System.Collections;
using Newtonsoft.Json;
using YouVote.Common.DomainModel;
using YouVote.Common.NHibernate;

namespace YouVote.Model.Domain.Audyt
{
	public class AuditListener : IPostUpdateEventListener, IPostInsertEventListener, IPostDeleteEventListener
	{
	    public delegate string GetCurentUser();

	    public event GetCurentUser GetCurentUserId;

		public void OnPostUpdate(PostUpdateEvent @event)
		{
			var auditable = @event.Entity as IAuditable;
			if (auditable == null)
			{
				return;
			}

			var items = new List<AudytLogItem>();

			var dirtyPropertyIndexes = @event.Persister.FindDirty(@event.OldState, @event.State, @event.Entity, @event.Session);
			foreach (var index in dirtyPropertyIndexes)
			{
				items.Add(new AudytLogItem
				{
					NewValue = GetPropertValue(@event.State, index),
					OldValue = GetPropertValue(@event.OldState, index),
					PropertyName = @event.Persister.PropertyNames[index]
				});
			}

			SaveLogEntry(auditable, items, AudytLogType.Update);
		}

		public void OnPostInsert(PostInsertEvent @event)
		{
			var auditable = @event.Entity as IAuditable;
			if (auditable == null)
			{
				return;
			}

			var names = @event.Persister.PropertyNames;
			var items = new List<AudytLogItem>();

			for (int i = 0; i < names.Length; i++)
			{
				items.Add(new AudytLogItem
				{
					NewValue = GetPropertValue(@event.State ,i),
					PropertyName = names[i]
				});
			}

			SaveLogEntry(auditable, items, AudytLogType.Insert);
		}

		public void OnPostDelete(PostDeleteEvent @event)
		{
			var auditable = @event.Entity as IAuditable;
			if (auditable == null)
			{
				return;
			}

			var names = @event.Persister.PropertyNames;
			var items = new List<AudytLogItem>();

			for (int i = 0; i < names.Length; i++)
			{
				items.Add(new AudytLogItem
				{
					OldValue = GetPropertValue(@event.DeletedState, i),
					PropertyName = names[i]
				});
			}

			SaveLogEntry(auditable, items, AudytLogType.Delete);
		}

		private string GetPropertValue(object[] state, int index)
		{
			var properyValue = state[index];
			if (properyValue == null)
			{
				return string.Empty;
			}
			else if (properyValue is Entity)
			{
				return ((Entity)properyValue).Id.ToString("G");
			}
			else if (properyValue is ICollection)
			{
				return "kolekcja";
			}
			else
			{
				return properyValue.ToString();
			}
		}

		private void SaveLogEntry(IAuditable auditable, List<AudytLogItem> items, AudytLogType audytLogType)
		{
			using (var session = NHibernateSession.GetDefaultSessionFactory().OpenSession())
			{
				using (var tx = session.BeginTransaction())
				{
					var log = new AudytLog
					{
						AudytLogTypeEnum = audytLogType,
						LogDateTime = DateTime.Now,
						UserLogin = GetCurentUserId(),
						ObjectId = auditable.IdString,
						ObjectIdType = auditable.IdType,
						ObjectType = auditable.ObjectType,
						ObjectValue = JsonConvert.SerializeObject(items)
					};

					session.Save(log);

					tx.Commit();
				}
			}
		}
	}
}
using System.Collections.Generic;
using NHibernate.Event;
using YouVote.Common.NHibernate;
using YouVote.Model.Domain.Audyt;

namespace YouVote.Model
{
    public class NhConfig
    {
        public static void InitNhibernateSession(ISessionStorage webSessionStorage, AuditListener.GetCurentUser getCurentUser, bool generateStats, string[] paths, string connectionString)
        {
            var nhProperties = new Dictionary<string, string>
			{
				{ "connection.connection_string", connectionString }
			};

            if (generateStats)
            {
                nhProperties.Add("generate_statistics", "true");
            }
            
            var nhConfig = NHibernateSession.Init(
                webSessionStorage,
                new[] { paths[0] },
                null,
                paths[1],
                nhProperties,
                string.Empty);

            var auditListner = new AuditListener();
            auditListner.GetCurentUserId += getCurentUser;
            nhConfig.SetListener(ListenerType.PostUpdate, auditListner);
            nhConfig.SetListener(ListenerType.PostInsert, auditListner);
            nhConfig.SetListener(ListenerType.PostDelete, auditListner);
        }
    }
}

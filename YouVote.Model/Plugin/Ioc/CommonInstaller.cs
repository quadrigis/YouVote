using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using YouVote.Common.PersistenceSupport;

namespace YouVote.Model.Plugin.Ioc
{
	public class CommonInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Component.For<ITransactionProvider>().ImplementedBy<TransactionProvider>().LifeStyle.Transient);
            container.Register(Component.For<INewTransaction>().ImplementedBy<Transaction>().LifeStyle.Transient);
		}
	}
}
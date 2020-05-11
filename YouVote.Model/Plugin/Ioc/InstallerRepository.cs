using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace YouVote.Model.Plugin.Ioc
{
	public class InstallerRepository : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(AllTypes.FromThisAssembly()
								.Where(type => type.Name.EndsWith("Repository"))
								.WithServiceDefaultInterfaces()
								.LifestyleTransient());
		}
	}
}
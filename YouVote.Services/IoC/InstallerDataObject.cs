using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace YouVote.Services.IoC
{
	public class InstallerDataObject : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(AllTypes.FromThisAssembly()
								  .Where(type => type.Name.EndsWith("DataObject"))
								  .WithServiceDefaultInterfaces()
								  .LifestyleTransient());
		}
	}
}
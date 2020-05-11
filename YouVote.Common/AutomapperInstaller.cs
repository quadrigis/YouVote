using System;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace YouVote.Common
{
    public class AutomapperInstaller
    {
        public void Init()
        {
            var instances = from t in Assembly.GetExecutingAssembly().GetTypes()
                            where t.GetInterfaces().Contains(typeof(IAutomapperInstall))
                                     && t.GetConstructor(Type.EmptyTypes) != null
                            select Activator.CreateInstance(t) as IAutomapperInstall;

            foreach (var instance in instances)
            {
                instance.Install();
            }

            Mapper.AssertConfigurationIsValid();
        }
    }

    public interface IAutomapperInstall
    {
        void Install();
    }
}
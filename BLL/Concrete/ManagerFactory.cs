using System;
using BLL.Abstract;
using Entities.POCOEntities;

namespace BLL.Concrete
{
    public class ManagerFactory
    {
        private static ManagerFactory FactoryInstance { get; set; }

        private ManagerFactory(){}

        public static ManagerFactory GetInstance()
        {
            return FactoryInstance ?? (FactoryInstance = new ManagerFactory());
        }

        public IManager<TPoco> GetManagerFor<TPoco>() where TPoco : EntityPOCO
        {
            var entityType = typeof (TPoco);
            var entityName = entityType.Name.Replace("POCO", string.Empty);
            var managerClassName = string.Format("BLL.Concrete.{0}Manager", entityName);

            var managerType = Type.GetType(managerClassName);

            if (managerType == null)
                throw new Exception("Manager not found");

            return (IManager<TPoco>)Activator.CreateInstance(managerType);
        }
    }
}

using System.Data.Entity;

namespace System.Data.Entity
{
	public class EntityFrameworkConfiguration : DbConfiguration
	{
		public EntityFrameworkConfiguration()
		{
			AddInterceptor(new SoftDeleteInterceptor());
		}
	}
}
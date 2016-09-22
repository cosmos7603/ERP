using AM.DAL;
using AM.Services.Models;
using System.Runtime.Remoting.Messaging;

namespace AM.Services
{
	public class ServiceBase
	{
		#region Consts
		private const string DBContextName = "DBContext";
		private const string IdentityContextName = "Identity";
		private const string BrandContextName = "Brand";
		private const string StoreContextName = "Store";
		#endregion

		#region Properties
		protected static DBERP DBERP
		{
			get
			{
				// Find DB on this thread's bag
				DBERP db = CallContext.GetData(DBContextName) as DBERP;

				// If it doesn't exists, create it
				if (db == null)
				{
					db = new DBERP();

					// Save the context on the thread's bag
					CallContext.SetData(DBContextName, db);
				}

				return db;
			}
		}

		protected static DB DB
		{
			get
			{
				// Find DB on this thread's bag
				DB db = CallContext.GetData(DBContextName) as DB;

				// If it doesn't exists, create it
				if (db == null)
				{
					db = new DB();

					// Save the context on the thread's bag
					CallContext.SetData(DBContextName, db);
				}

				return db;
			}
		}

		protected static ServiceUserIdentity Identity => CallContext.GetData(IdentityContextName) as ServiceUserIdentity ?? new ServiceUserIdentity
		{
			AuditLogin = "system"
		};

		protected static ServiceStore Store => CallContext.GetData(StoreContextName) as ServiceStore ?? new ServiceStore();

		protected static ServiceBrand Brand => CallContext.GetData(BrandContextName) as ServiceBrand ?? new ServiceBrand();

		#endregion

		#region Service Entities
		public static void SetIdentity(ServiceUserIdentity identity)
		{
			CallContext.SetData(IdentityContextName, identity);
		}

		public static void SetBrand(ServiceBrand brand)
		{
			CallContext.SetData(BrandContextName, brand);
		}

		public static void SetStore(ServiceStore store)
		{
			CallContext.SetData(StoreContextName, store);
		}
		#endregion

		#region Context Management
		public static void ResetContext()
		{
			// Remove and dispose current context
			DisposeContext();

			// Create new context
			CallContext.SetData(DBContextName, new DBERP());
		}

		public static void DisposeContext()
		{
			// Read current context
			DBERP db = CallContext.GetData(DBContextName) as DBERP;

			// If there's a context
			if (db != null)
			{
				// Dispose it.. freeing any connection and resources used
				db.Dispose();

				// Remove context instance from the thread's bag
				CallContext.SetData(DBContextName, null);	
			}
		}
		#endregion
	}
}

using System.Data.Entity.Infrastructure;
using Corpnet.Profiling;

namespace Corpnet.Profiling.EF6
{
    /// <summary>
    /// Connection factory used for EF Code First <c>DbContext</c> API
    /// </summary>
    public class ProfiledDbConnectionFactory : IDbConnectionFactory
    {
        /// <summary>
        /// The wrapped connection factory.
        /// </summary>
        private readonly IDbConnectionFactory _wrapped;

        /// <summary>
        /// Initialises a new instance of the <see cref="ProfiledDbConnectionFactory"/> class. 
        /// Create a profiled connection factory
        /// </summary>
        /// <param name="wrapped">
        /// The underlying connection that needs to be profiled
        /// </param>
        public ProfiledDbConnectionFactory(IDbConnectionFactory wrapped)
        {
            _wrapped = wrapped;
        }

        /// <summary>
        /// Create a wrapped connection for profiling purposes 
        /// </summary>
        /// <param name="nameOrConnectionString">the name or connection string.</param>
        /// <returns>the connection</returns>
        public System.Data.Common.DbConnection CreateConnection(string nameOrConnectionString)
        {
			var profiler = MiniProfiler.Current;

			var connection = _wrapped.CreateConnection(nameOrConnectionString);

			return profiler != null ? new ProfiledDbConnection(connection, MiniProfiler.Current) : connection;
        }
    }
}

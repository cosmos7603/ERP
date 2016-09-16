using System;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Reflection.Emit;
using Corpnet.Profiling;

namespace Corpnet.Profiling.EF6
{
    [System.ComponentModel.DesignerCategory("")]
    public class ProfiledDbCommand : DbCommand, ICloneable
    {
		private const string EF_STRING = "Entity Framework";

        private DbCommand _command;
        private DbConnection _connection;
        private DbTransaction _transaction;
        private MiniProfiler _profiler;

		public ProfiledDbCommand(DbCommand command, DbConnection connection, MiniProfiler profiler)
        {
            if (command == null) throw new ArgumentNullException("command");

            _command = command;
            _connection = connection;

            if (profiler != null)
            {
                _profiler = profiler;
            }
        }

        public override string CommandText
        {
            get { return _command.CommandText; }
            set { _command.CommandText = value; }
        }

        public override int CommandTimeout
        {
            get { return _command.CommandTimeout; }
            set { _command.CommandTimeout = value; }
        }

        public override CommandType CommandType
        {
            get { return _command.CommandType; }
            set { _command.CommandType = value; }
        }

        protected override DbConnection DbConnection
        {
            get
            {
                return _connection;
            }
            set
            {
                if (MiniProfiler.Current != null)
                    _profiler = MiniProfiler.Current;

                _connection = value;

				var awesomeConn = value as ProfiledDbConnection;
                
				_command.Connection = awesomeConn == null ? value : awesomeConn.WrappedConnection;
            }
        }

        protected override DbParameterCollection DbParameterCollection
        {
            get { return _command.Parameters; }
        }

        protected override DbTransaction DbTransaction
        {
            get
            {
                return _transaction;
            }

            set
            {
                _transaction = value;
                var awesomeTran = value as ProfiledDbTransaction;
                _command.Transaction = awesomeTran == null ? value : awesomeTran.WrappedTransaction;
            }
        }

        public override bool DesignTimeVisible
        {
            get { return _command.DesignTimeVisible; }
            set { _command.DesignTimeVisible = value; }
        }

        /// <summary>
        /// Gets or sets the updated row source.
        /// </summary>
        public override UpdateRowSource UpdatedRowSource
        {
            get { return _command.UpdatedRowSource; }
            set { _command.UpdatedRowSource = value; }
        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            if (_profiler == null || !_profiler.IsActive)
                return _command.ExecuteReader(behavior);

            DbDataReader result = null;

			using (_profiler.Step(EF_STRING, EFProviderUtilities.GetFormattedSqlCommand(_command.CommandText, _command.Parameters)))
			{
				result = _command.ExecuteReader(behavior);
                result = new ProfiledDbDataReader(result, _connection, _profiler);
			}

            return result;
        }

        public override int ExecuteNonQuery()
        {
            if (_profiler == null || !_profiler.IsActive)
                return _command.ExecuteNonQuery();

            int result;

			using (_profiler.Step(EF_STRING, EFProviderUtilities.GetFormattedSqlCommand(_command.CommandText, _command.Parameters)))
			{
                result = _command.ExecuteNonQuery();
			}

            return result;
        }

        public override object ExecuteScalar()
        {
            if (_profiler == null || !_profiler.IsActive)
                return _command.ExecuteScalar();

            object result;

			using (_profiler.Step(EF_STRING, EFProviderUtilities.GetFormattedSqlCommand(_command.CommandText, _command.Parameters)))
			{
				result = _command.ExecuteScalar();
			}

            return result;
        }

        public override void Cancel()
        {
            _command.Cancel();
        }

        public override void Prepare()
        {
            _command.Prepare();
        }

        protected override DbParameter CreateDbParameter()
        {
            return _command.CreateParameter();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _command != null)
                _command.Dispose();

            _command = null;

			base.Dispose(disposing);
        }

        public DbCommand InternalCommand
        {
            get
            {
                return _command;
            }
        }

        public ProfiledDbCommand Clone()
        {
			// EF expects ICloneable
            var tail = _command as ICloneable;
            if (tail == null) throw new NotSupportedException("Underlying " + _command.GetType().Name + " is not cloneable");
            
			return new ProfiledDbCommand((DbCommand)tail.Clone(), _connection, _profiler);
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}

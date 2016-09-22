using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Linq;
using AM.Utils;
using System.Data.Common;

namespace AM.DAL
{
    public partial class BaseContext : DbContext
    {
        #region Properties
        public static string DatabaseConnectionString { get; set; }
        #endregion

        #region Constructor
        public BaseContext()
            : this(DatabaseConnectionString)
        {
        }

        public BaseContext(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer<BaseContext>(null);

			// Disable lazy loading
			Configuration.LazyLoadingEnabled = false;
        }
        #endregion

        #region Custom Events
        public class SpExecutionEventArgs : EventArgs
        {
            public string SpName { get; set; }
            public string SQL { get; set; }
            public object Bag { get; set; }
            public long DataSize { get; set; }
            public int Count { get; set; }

            public SpExecutionEventArgs()
                : base()
            { }

            public SpExecutionEventArgs(string spName, string sql)
                : this()
            {
                this.SpName = spName;
                this.SQL = sql;
            }

            public SpExecutionEventArgs(object bag)
                : this()
            {
                this.Bag = bag;
            }

            public SpExecutionEventArgs(object bag, long dataSize, int count)
                : this()
            {
                this.Bag = bag;
                this.DataSize = dataSize;
                this.Count = count;
            }
        }

        /// <summary>
        /// An event handler for stored procedure calls.
        /// </summary>
        public delegate void SpExecutionEventHandler(object sender, SpExecutionEventArgs e);
        #endregion

        #region Hooks Events
        /// <summary>
        /// Event that gets fired BEFORE any command is executed.
        /// </summary>
        public event SpExecutionEventHandler BeforeCommandExecution;

        /// <summary>
        /// Event that gets fired AFTER any command is executed.
        /// </summary>
        public event SpExecutionEventHandler AfterCommandExecution;

        /// <summary>
        /// Event that gets fired when there's an exception with the command execution
        /// </summary>
        public event SpExecutionEventHandler CommandExecutionError;
        #endregion

        #region Hook Invokes
        public void BeforeCommandExecutionInvoke(object sender, SpExecutionEventArgs e)
        {
            SpExecutionEventHandler handler = BeforeCommandExecution;
            if (handler != null) handler(sender, e);
        }

        public void AfterCommandExecutionInvoke(object sender, SpExecutionEventArgs e)
        {
            SpExecutionEventHandler handler = AfterCommandExecution;
            if (handler != null) handler(sender, e);
        }

        public void CommandExecutionErrorInvoke(object sender, SpExecutionEventArgs e)
        {
            SpExecutionEventHandler handler = CommandExecutionError;
            if (handler != null) handler(sender, e);
        }
        #endregion

        #region Events
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Do not pluralize relationships
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Stop using unicode for varchar strings comparisons (remove the N' in the comparisons)
            modelBuilder.Properties<string>().Configure(x => x.HasColumnType("VARCHAR"));
        }

        public int SaveChanges(string login)
        {
            // Get all Added/Deleted/Modified entities (not Unmodified or Detached)
            foreach (var e in ChangeTracker.Entries().Where(p => p.State == EntityState.Added || p.State == EntityState.Modified || p.Entity.GetType().GetProperty("RowVersion") != null))
            {
                if (e.State == EntityState.Added)
                {
                    if (e.Entity.GetType().GetProperty("CreateDate") != null)
                    {
                        DbPropertyEntry createDate = e.Property("CreateDate");
                        DbPropertyEntry createBy = e.Property("CreateBy");

                        createDate.CurrentValue = DateTime.Now;
                        createBy.CurrentValue = login;
                    }
                }
                else if (e.State == EntityState.Modified || (e.State == EntityState.Unchanged && e.Entity.GetType().GetProperty("RowVersion") != null))
                {
                    if (e.Entity.GetType().GetProperty("EditDate") != null)
                    {
                        DbPropertyEntry editDate = e.Property("EditDate");
                        DbPropertyEntry editBy = e.Property("EditBy");

                        editDate.CurrentValue = DateTime.Now;
                        editBy.CurrentValue = login;
                    }
                }
            }

            // Call the original SaveChanges(), which will save both the changes made and the audit records
            return base.SaveChanges();
        }

        public void UndoAllChanges()
        {
            foreach (var dbEntityEntry in ChangeTracker.Entries())
            {
                switch (dbEntityEntry.State)
                {
                    case EntityState.Modified:
                        dbEntityEntry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        dbEntityEntry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        dbEntityEntry.Reload();
                        break;
                }
            }
        }
        #endregion

        #region SP Execution
        /// <summary>
        /// Executes a Stored Procedure without parameters.
        /// </summary>
        /// <typeparam name="T">Type of the resulting objects</typeparam>
        /// <param name="spName">Name of the Stored Procedure.</param>
        /// <returns>A List of the resulting objects</returns>
        public virtual List<T> ExecuteSp<T>(string spName)
        {
            //return this.Database.SqlQuery<T>(spName).ToList();
            return ExecuteSp<T>(spName, new SqlParameter[] { });
        }

        /// <summary>
        /// Executes a Stored Procedure.
        /// </summary>
        /// <typeparam name="T">Type of the resulting objects</typeparam>
        /// <param name="spName">Name of the Stored Procedure.</param>
        /// <param name="sqlParameters">The input parameters.</param>
        /// <returns>A List of the resulting objects</returns>
        public virtual List<T> ExecuteSp<T>(string spName, params SqlParameter[] sqlParameters)
        {
            var parameters = new List<string>();

            foreach (var sqlParameter in sqlParameters)
            {
                var p = $"@{sqlParameter.ParameterName}{(sqlParameter.Direction == ParameterDirection.Output || sqlParameter.Direction == ParameterDirection.InputOutput ? " out" : "")}";
                parameters.Add(p);
            }

            var command = $"{spName} {string.Join(",", parameters.ToArray())}";

            SpExecutionEventArgs eBefore = new SpExecutionEventArgs(spName, GetSqlCall(command, sqlParameters));
            BeforeCommandExecutionInvoke(this, eBefore);

            List<T> result = Database.SqlQuery<T>(command, sqlParameters).ToList();

            SpExecutionEventArgs eAfter = new SpExecutionEventArgs(eBefore.Bag, GetListSize(result), result.Count);
            AfterCommandExecutionInvoke(this, eAfter);

            return result;
        }

        /// <summary>
        /// Executes a command, returning a data reader.
        /// </summary>
        /// <param name="spName">Name of the Stored Procedure.</param>
        /// <param name="sqlParameters">The input parameters.</param>
        /// <returns>A database reader, supporting multiple result sets.</returns>
        public virtual DbDataReader ExecuteReader(string spName, params SqlParameter[] sqlParameters)
        {
            var parameters = new List<string>();

            // Prepare parameters
            foreach (var sqlParameter in sqlParameters)
            {
                var p = $"@{sqlParameter.ParameterName}{(sqlParameter.Direction == ParameterDirection.Output || sqlParameter.Direction == ParameterDirection.InputOutput ? " out" : "")}";
                parameters.Add(p);
            }

            // Open connection
            Database.Connection.Open();

            // Create command
            var cmd = Database.Connection.CreateCommand();

            // Prepare command text
            cmd.CommandText = $"{spName} {string.Join(",", parameters.ToArray())}";

            // Add parameters
            cmd.Parameters.AddRange(sqlParameters);

            SpExecutionEventArgs eBefore = new SpExecutionEventArgs(spName, GetSqlCall(cmd.CommandText, sqlParameters));
            BeforeCommandExecutionInvoke(this, eBefore);

            // Execute reader
            var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            SpExecutionEventArgs eAfter = new SpExecutionEventArgs(eBefore.Bag);
            AfterCommandExecutionInvoke(this, eAfter);

            // Example on how to read multiple datasets from the same query
            // Read Entity1 from the first resultset
            // var objectContext = ((IObjectContextAdapter)myContext).ObjectContext;
            //result.Set1 = objectContext.Translate<Entity1>(reader, "Set1", MergeOptions.AppendOnly);
            // Read Entity2 from the second resultset
            //reader.NextResult();
            //result.Set2 = objectContext.Translate<Entity2>(reader, "Set2", MergeOptions.AppendOnly);

            return reader;
        }

        /// <summary>
        /// Gets the SP as close as possible as how it would be executed on the database,
        /// to be logged as such and if possible able to copy and paste to run directly on the database.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        private string GetSqlCall(string command, SqlParameter[] sqlParameters)
        {
            string sqlCall = (command + ',').Replace(" out,", ",").Replace(" output,", ",");
            string valueToPass;
            string outVariablesDeclaration = string.Empty, outVariablesForSelect = string.Empty;
            foreach (SqlParameter parameter in sqlParameters)
            {
                if (parameter.Direction == ParameterDirection.Output || parameter.Direction == ParameterDirection.InputOutput)
                {
                    valueToPass = '@' + parameter.ParameterName + "OutValue";
                    outVariablesDeclaration += valueToPass + ' ' + parameter.SqlDbType.ToString("g") //declaration with data type
                                             + (parameter.Size > 0 ? '(' + parameter.Size.ToString() + ')' : string.Empty) //data size if needed
                                             + '=' + GetFormattedValue(parameter.SqlDbType, parameter.Value) + ','; //value being sent
                    outVariablesForSelect += valueToPass + ',';
                    valueToPass += " out";
                }
                else
                {
                    valueToPass = GetFormattedValue(parameter.SqlDbType, parameter.Value);
                }
                sqlCall = sqlCall.Replace($"@{parameter.ParameterName},", $"@{parameter.ParameterName}={valueToPass},");
            }
            return (string.IsNullOrEmpty(outVariablesDeclaration) ? string.Empty : "declare " + outVariablesDeclaration.TrimEnd(',') + '\n')
                 + "exec " + sqlCall.TrimEnd(',', ' ')
                 + (string.IsNullOrEmpty(outVariablesForSelect) ? string.Empty : "\nselect " + outVariablesForSelect.TrimEnd(','));
        }

        /// <summary>
        /// Returns the value to pass for a parameter formatted according to the parameter data type
        /// </summary>
        /// <param name="type">The parameter type as a <c ref="SqlDbType"></c></param>
        /// <param name="value">the value of the parameter.</param>
        /// <returns></returns>
        private static string GetFormattedValue(SqlDbType type, object value)
        {
            string valueToPass;
            if (value == DBNull.Value || value == null)
            {
                valueToPass = "null";
            }
            else
            {
                switch (type)
                {
                    case SqlDbType.BigInt:
                    case SqlDbType.Decimal:
                    case SqlDbType.Float:
                    case SqlDbType.Int:
                    case SqlDbType.Money:
                    case SqlDbType.Real:
                    case SqlDbType.SmallInt:
                    case SqlDbType.SmallMoney:
                    case SqlDbType.TinyInt:
                    case SqlDbType.DateTimeOffset:
                    case SqlDbType.Timestamp:
                        valueToPass = value.ToString();
                        break;
                    case SqlDbType.Bit:
                        valueToPass = value.ToBool() ? "1" : "0";
                        break;
                    case SqlDbType.DateTime:
                    case SqlDbType.SmallDateTime:
                    case SqlDbType.Date:
                    case SqlDbType.Time:
                    case SqlDbType.DateTime2:
                        DateTime auxDt;
                        bool isDate = DateTime.TryParse(value.ToString(), out auxDt);
                        valueToPass = isDate ? ('\'' + auxDt.ToString("yyyy-MM-dd HH:mm:ss") + '\'') : "null";
                        break;
                    case SqlDbType.Image:
                        valueToPass = "(image)";
                        break;
                    case SqlDbType.VarBinary:
                        valueToPass = "(VarBinary)";
                        break;
                    case SqlDbType.Variant:
                        valueToPass = "(Variant)";
                        break;
                    case SqlDbType.Udt:
                        valueToPass = "(user-defined type)";
                        break;
                    case SqlDbType.Structured:
                        valueToPass = "(structured data)";
                        break;
                    case SqlDbType.NChar:
                    case SqlDbType.NText:
                    case SqlDbType.NVarChar:
                    case SqlDbType.UniqueIdentifier:
                    case SqlDbType.Text:
                    case SqlDbType.VarChar:
                    case SqlDbType.Xml:
                    default:
                        valueToPass = '\'' + value.ToString() + '\'';
                        break;
                }
            }
            return valueToPass;
        }

        /// <summary>
        /// NOT IMPLEMENTED YET. Calculates the size in bytes of the return value from the stored procedure execution.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns>Always returns zero until properly implemented.</returns>
        private static long GetListSize<T>(List<T> list)
        {
            /*
            //Need clases to be serializable to do this.
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter f = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

            f.Serialize(ms, list); //Need clases to be serializable to do this.
            long datasize = ms.Length;

            ms.Dispose();

            return datasize;
            */
            return 0;
        }
        #endregion
    }
}
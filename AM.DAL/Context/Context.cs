using System.Data.Entity;
using AM.DAL;
using AM.DAL.Entities.Business;
using Corpnet.Profiling;

namespace AM.DAL
{
	public partial class DB : BaseContext
	{
		#region Constructor
		public DB()
			: this(DatabaseConnectionString)
		{
		}

		public DB(string connectionString)
			: base(connectionString)
		{
			AttachEvents();
		}
		#endregion

		#region DB Sets
		public DbSet<Appointment> Appointment { get; set; }
		public DbSet<ConfigSetting> ConfigSetting { get; set; }
		public DbSet<ConfigGroup> ConfigGroup { get; set; }
		public DbSet<Email> Email { get; set; }
		public DbSet<EmailTemplate> EmailTemplate { get; set; }
		public DbSet<Report> Report { get; set; }
		public DbSet<ReportRun> ReportRun { get; set; }
		public DbSet<DataFile> DataFile { get; set; }
		public DbSet<EventLog> EventLog { get; set; }
		public DbSet<Module> Module { get; set; }
		public DbSet<Resource> Resource { get; set; }
		public DbSet<User> User { get; set; }
		public DbSet<State> State { get; set; }
		public DbSet<EmailAttach> EmailAttach { get; set; }
		public DbSet<PasswordHistory> PasswordHistory { get; set; }
		public DbSet<Permission> Permission { get; set; }
		public DbSet<HeadOffice> HeadOffice { get; set; }
		public DbSet<Brand> Brand { get; set; }
		public DbSet<Store> Store { get; set; }
		public DbSet<DestinationArea> DestinationArea { get; set; }
		public DbSet<Vendor> Vendor { get; set; }
		public DbSet<Destination> Destination { get; set; }
		public DbSet<StoreDestination> StoreDestination { get; set; }
		public DbSet<MktgSrc> MktgSrc { get; set; }
		public DbSet<MktgCat> MktgCat { get; set; }
		public DbSet<BrandMktgSrc> BrandMktgSrc { get; set; }
		public DbSet<InterestCat> InterestCat { get; set; }
		public DbSet<MktgActivity> MktgActivity { get; set; }
		public DbSet<Interest> Interest { get; set; }
		public DbSet<CEActivity> CEActivity { get; set; }
		public DbSet<Ad> Ad { get; set; }
		public DbSet<AdBrand> AdBrand { get; set; }
		public DbSet<AdLocation> AdLocation { get; set; }
		public DbSet<BrandGuestCommType> BrandGuestCommType { get; set; }
		public DbSet<GuestComm> GuestComm { get; set; }
		public DbSet<GuestCommAdj> GuestCommAdj { get; set; }
		public DbSet<GuestCommAdjType> GuestCommAdjType { get; set; }
		public DbSet<GuestCommHst> GuestCommHst { get; set; }
		public DbSet<GuestCommPermission> GuestCommPermission { get; set; }
		public DbSet<GuestCommType> GuestCommType { get; set; }
		public DbSet<Lead> Lead { get; set; }
		public DbSet<StoreNews> StoreNews { get; set; }
		public DbSet<Notification> Notification { get; set; }
		public DbSet<MktgActivityBrand> MktgActivityBrand { get; set; }
		public DbSet<MktgActivityPromotion> MktgActivityPromotion { get; set; }
		public DbSet<Promotion> Promotion { get; set; }
		public DbSet<MktgActivityStore> MktgActivityStore { get; set; }
        public DbSet<MktgActivityDelivery> MktgActivityDelivery { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<ResPass> ResPass { get; set; }
        public DbSet<Grp> Grp { get; set; }
		public DbSet<LineItem> LineItem { get; set; }    
        public DbSet<CorporateCharge> CorporateCharge { get; set; }
        public DbSet<CorporateChargeReason> CorporateChargeReason { get; set; }
        public DbSet<Branch> Branch { get; set; }
		public DbSet<GrpLinePricing> GrpLinePricing { get; set; }
		public DbSet<GrpSegment> GrpSegment { get; set; }
        public DbSet<LinePass> LinePass { get; set; }
        public DbSet<Itinerary> Itinerary { get; set; }
        public DbSet<ItineraryDay> ItineraryDay { get; set; }
        public DbSet<Cruise> Cruise { get; set; }

		#endregion

		#region Functions
		private void AttachEvents()
		{
			AfterCommandExecution += new SpExecutionEventHandler(DB_OnAfterExecution);
			BeforeCommandExecution += new SpExecutionEventHandler(DB_OnBeforeExecution);
		}
		#endregion

		#region Events
		private void DB_OnBeforeExecution(object sender, SpExecutionEventArgs e)
		{
			Timing timing = (Timing)MiniProfiler.Current.Step(e.SpName, e.SQL);
			e.Bag = timing;
		}

		private void DB_OnAfterExecution(object sender, SpExecutionEventArgs e)
		{
			if (e.Bag != null)
			{
				Timing timing = ((Timing)e.Bag);
				timing.Stop(e.DataSize, e.Count);
			}
		}
		#endregion
	}

	public partial class DBERP : BaseContext
	{

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Client>().ToTable("Client");
			modelBuilder.Entity<Provider>().ToTable("Provider");
			modelBuilder.Entity<Product>().ToTable("Product");
			modelBuilder.Entity<ProductFamily>().ToTable("ProductFamily");
		}

		#region Constructor
		public DBERP()
			: this(DatabaseConnectionString)
		{
		}

		public DBERP(string connectionString)
			: base(connectionString)
		{
			AttachEvents();
		}
		#endregion

		#region DB Sets

		public DbSet<Client> Client { get; set; }
		public DbSet<ClientType> ClientType { get; set; }

		public DbSet<Provider> Provider { get; set; }

		public DbSet<Product> Product { get; set; }

		public DbSet<ProductFamily> ProductFamily { get; set; }

		#endregion

		#region Functions
		private void AttachEvents()
		{
			AfterCommandExecution += new SpExecutionEventHandler(DB_OnAfterExecution);
			BeforeCommandExecution += new SpExecutionEventHandler(DB_OnBeforeExecution);
		}
		#endregion

		#region Events
		private void DB_OnBeforeExecution(object sender, SpExecutionEventArgs e)
		{
			Timing timing = (Timing)MiniProfiler.Current.Step(e.SpName, e.SQL);
			e.Bag = timing;
		}

		private void DB_OnAfterExecution(object sender, SpExecutionEventArgs e)
		{
			if (e.Bag != null)
			{
				Timing timing = ((Timing)e.Bag);
				timing.Stop(e.DataSize, e.Count);
			}
		}
		#endregion
	}
}
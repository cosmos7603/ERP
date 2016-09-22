namespace AM.Services
{
	public class Consts
	{
		public const int CORPORATE_STORE_ID = 0;
	}

	public class UserLevelCode
	{
		public const string C = "C";
		public const string A = "A";
		public const string S = "S";
		public const string B = "B";
		public const string H = "H";
	}

	public class LoginResultCode
	{
		public const string OK = "OK";
		public const string FAILED = "FAILED";
		public const string PASSWORD_EXPIRED = "PASSWORD_EXPIRED";
		public const string INACTIVE = "INACTIVE";
		public const string LOCKED = "LOCKED";
	}

	public class EnvironmentCode
	{
		public const string DEV = "DEV";
		public const string STG = "STG";
		public const string PRD = "PRD";
	}

	public class ImportTypeCode
	{
		public const string PUR = "PUR";
		public const string PRT = "PRT";
		public const string EIN = "EIN";
		public const string EAC = "EAC";
		public const string NGI = "NGI";
	}

	public class MktgSrcLevel
	{
		public const string CORP = "CORP";
		public const string HO = "HO";
		public const string STORE = "STORE";
		public const string USER = "USER";
	}

	public class NotificationTypeCode
	{
		public const string MKTG_ACT = "MKTG_ACT";
		public const string RPT_EMAIL = "RPT_EMAIL";
		public const string STORE_NEWS = "STORE_NEWS";
	}

	public class AppointmentTypeCode
	{
		public const string USR = "USR"; // User (custom)
		public const string NTS = "NTS"; // Note
		public const string VFP = "VFP"; // Vendor Final Payment Date
		public const string VDD = "VDD"; // Vendor Deposit Date
		public const string CFP = "CFP"; // Customer Final Payment Date
		public const string CDD = "CDD"; // Customer Deposit Date
		public const string CEX = "CEX"; // Customer Experience
		public const string FUP = "FUP"; // Note followup
		public const string DDD = "DDD"; // Documents Due Date
		public const string BVC = "BVC"; // Bon Boyage Call
		public const string PCC = "PCC"; // Post Cruise Call
		public const string ADC = "ADC"; // Air Deviation Call
		public const string GFT = "GFT"; // Gift Fulfilment Reminder
		public const string GAD = "GAD"; // Add Gift To Booking
	}

	public class ProductTypeCode
	{
		public const string AIRTR = "AIRTR";
		public const string BUSTR = "BUSTR";
		public const string CRUIS = "CRUIS";
		public const string DSCNT = "DSCNT";
		public const string GIFTO = "GIFTO";
		public const string HOTEL = "HOTEL";
		public const string INSUR = "INSUR";
		public const string OTHER = "OTHER";
		public const string TRNSF = "TRNSF";
		public const string TOURO = "TOURO";
		public const string TOURP = "TOURP";
		public const string SHORE = "SHORE";
		public const string CARRE = "CARRE";
		public const string CANCL = "CANCL";
	}

	public class ReservationStatusCode
	{
		public const string ARCHVD = "ARCHVD";
		public const string BOOKED = "BOOKED";
		public const string CANCEL = "CANCEL";
		public const string DEPRTD = "DEPRTD";
		public const string QUOTED = "QUOTED";
	}


	public class UserCommPymtRule
	{
		public const string CB0VB1 = "CB0VB1";
		public const string CB0VB1DP = "CB0VB1DP";
		public const string CB0VB0 = "CB0VB0";
		public const string CB0VB0DP = "CB0VB0DP";
		public const string DEP = "DEP";
	}

	public class UserCommType
	{
		public const string PROFIT = "PROFIT";
		public const string PRICE = "PRICE";
		public const string FLAT = "FLAT";
	}

	public class CodeLevelCode
	{
		public const string PUSHDOWN = "PUSHDOWN";
		public const string HEADOFFICE = "HEADOFFICE";
	}


	public class CodeTypeCode
	{
		public const string DEST = "DEST"; // Destination
		public const string VEND = "VEND"; // Vendor
		public const string BUDG = "BUDG"; // Budget
		public const string CARD = "CARD"; // Credit-Card
		public const string DIET = "DIET"; // Special diet
		public const string DINE = "DINE"; // Dining option
		public const string DURA = "DURA"; // Duration
		public const string INTR = "INTR"; // Interests
		public const string PHON = "PHON"; // Phone types
		public const string POST = "POST"; // Post title
		public const string PRET = "PRET"; // Pre title
		public const string SPEC = "SPEC"; // Marketing source
		public const string SRCE = "SRCE"; // Sepacial occasion
		public const string SEAT = "SEAT"; // Air seating
		public const string TABL = "TABL"; // Table sizes
		public const string CONF = "CONF"; // Confirmation Type
		public const string MESP = "MESP"; // Manage Doc Specials
		public const string STAT = "STAT"; // Stateroom Type
		public const string RELA = "RELA"; // Relationship type
		public const string ADDR = "ADDR"; // Address type
		public const string MAIL = "MAIL"; // Email Type
		public const string MARI = "MARI"; // Marital status
		public const string VNDT = "VNDT"; // Vendor Type
		public const string NOTE = "NOTE"; // Note type
		public const string CARS = "CARS"; // Car types
		public const string LETC = "LETC"; // Letter Template Category
		public const string AGCR = "AGCR"; // Agency cards
		public const string SHRT = "SHRT"; // Shirt Size
		public const string SEND = "SEND"; // Received and Sent Via Docs
		public const string PPIB = "PPIB"; // Passport Issues By
		public const string BRTH = "BRTH"; // Berthing
		public const string BUSS = "BUSS"; // Bus Stop
		public const string CHRG = "CHRG"; // Corporate Charge Reason
		public const string DSCT = "DSCT"; // Discount Type
		public const string GATE = "GATE"; // Gateway City
		public const string GLAC = "GLAC"; // GL Accounts (formerly known as NVOY - Non-Voyage Transfers)
		public const string BACC = "BACC"; // Bank Accounts
		public const string OTHE = "OTHE"; // Other Type
		public const string SACD = "SACD"; // Store Associate Consortium/Division
		public const string IEFT = "IEFT"; // Internal EFT Account
		public const string LINK = "LINK"; // Links (Store Level)
		public const string CARE = "CARE"; // Cancellation Reason
		public const string OPTO = "OPTO"; // Opt-Out Reason
		public const string CANP = "CANP"; // Canada Province
	}
}
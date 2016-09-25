using System.Web.Optimization;

namespace AM.WebSite
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			RegisterLoginBundles(bundles);
			RegisterMainBundles(bundles);

			// Force bundling, even on DEV, for testing
			 BundleTable.EnableOptimizations = false;
		}

		private static void RegisterLoginBundles(BundleCollection bundles)
		{
			// CSS Styles
			bundles.Add(new StyleBundle("~/bundles/login-css")
				.Include("~/Content/theme/css/bootstrap.css")
				.Include("~/Content/libs/fontawesome/css/font-awesome.css")
				.Include("~/Content/theme/css/plugins/iCheck/custom.css")
				.Include("~/Content/theme/css/plugins/awesome-bootstrap-checkbox/awesome-bootstrap-checkbox.css")
				.Include("~/Content/theme/css/plugins/toastr/toastr.min.css")
				.Include("~/Content/theme/overrides/login.css")
				.Include("~/Content/theme/css/animate.css")
				.Include("~/Content/theme/css/style.css")
			);

			// Javascript
			bundles.Add(new ScriptBundle("~/bundles/login-js")
				.Include("~/Content/libs/jquery/jQuery-2.1.4.min.js")
				.Include("~/Content/theme/js/bootstrap.js")
				.Include("~/Content/theme/js/plugins/iCheck/icheck.min.js")
				.Include("~/Content/libs/validate/jquery.validate.js")
				.Include("~/Content/libs/blockui/jquery.blockUI.js")
				.Include("~/Content/theme/js/plugins/toastr/toastr.min.js")
				.Include("~/Content/theme/js/plugins/toastr/toastr.config.js")
				.Include("~/Controls/Captcha/JS/Captcha.js")
				.Include("~/Shared/JS/Ajax.js")
				.Include("~/Shared/JS/Global.js")
				.Include("~/Shared/JS/BrowserDetection.js")
				.Include("~/Shared/JS/JQExtensions.js")
				.Include("~/Shared/JS/Shared.js")
				);
		}

		private static void RegisterMainBundles(BundleCollection bundles)
		{
			// CSS Styles
			bundles.Add(new StyleBundle("~/bundles/main-css")
				// Bootstrap
				.Include("~/Content/theme/css/bootstrap.css")

				// libs
				.Include("~/Content/libs/autocomplete/jquery.autocomplete.css")
				//.Include("~/Content/libs/datatables/dataTables.bootstrap.css")
				.Include("~/Content/libs/datepicker/datepicker3.css")
				.Include("~/Content/libs/select2/select2.min.css")
				.Include("~/Content/libs/timepicker/bootstrap-timepicker.css")
				.Include("~/Content/libs/fontawesome/css/font-awesome.css")
				.Include("~/Content/libs/qtip/jquery.qtip.css")

				// Theme
				.Include("~/Content/theme/css/plugins/ladda/ladda-themeless.min.css")
				.Include("~/Content/theme/css/plugins/dataTables/datatables.min.css")
				.Include("~/Content/theme/css/plugins/toastr/toastr.min.css")
				.Include("~/Content/theme/css/plugins/iCheck/custom.css")
				.Include("~/Content/theme/css/plugins/awesome-bootstrap-checkbox/awesome-bootstrap-checkbox.css")
				.Include("~/Content/theme/css/animate.css")
				.Include("~/Content/theme/css/style.css")

				// Overrides
				.Include("~/Content/theme/overrides/main.css")
				.Include("~/Content/theme/overrides/aspneterror.css")
				.Include("~/Content/theme/overrides/autocomplete.css")
				.Include("~/Content/theme/overrides/datatables.css")
				.Include("~/Content/theme/overrides/bootstrap.css")
				//.Include("~/Content/theme/overrides/style.css")
				//.Include("~/Content/theme/overrides/navbar.css")
				);

			// Javascript
			bundles.Add(new ScriptBundle("~/bundles/main-js")
				// JQuery
				.Include("~/Content/libs/jquery/jQuery-2.1.4.min.js")

				// libs
				.Include("~/Content/libs/datatables/jquery.dataTables.js")
				//.Include("~/Content/libs/datatables/jquery.dataTables.select.min.js")
				//.Include("~/Content/libs/datatables/dataTables.bootstrap.js")
				.Include("~/Content/libs/datepicker/bootstrap-datepicker.js")
				.Include("~/Content/libs/input-mask/jquery.inputmask.bundle.min.js")
				.Include("~/Content/libs/select2/select2.js")
				.Include("~/Content/libs/autocomplete/jquery.autocomplete.js")
				.Include("~/Content/libs/blockui/jquery.blockUI.js")
				.Include("~/Content/libs/form/jquery.form.js")
				.Include("~/Content/libs/format/jquery.format.js")
				.Include("~/Content/libs/validate/jquery.validate.js")
				.Include("~/Content/libs/qtip/jquery.qtip.js")
				.Include("~/Content/libs/tinymce/tinymce.min.js")
				.Include("~/Content/libs/multiselect/multiselect.js")

				// Bootstrap
				.Include("~/Content/theme/js/bootstrap.js")

				// Theme
				.Include("~/Content/theme/js/plugins/metisMenu/jquery.metisMenu.js")
				.Include("~/Content/theme/js/plugins/slimscroll/jquery.slimscroll.min.js")
				.Include("~/Content/theme/js/inspinia.js")
				.Include("~/Content/theme/js/plugins/pace/pace.min.js")
				.Include("~/Content/theme/js/plugins/ladda/spin.min.js")
				.Include("~/Content/theme/js/plugins/ladda/ladda.min.js")
				.Include("~/Content/theme/js/plugins/ladda/ladda.jquery.min.js")
				.Include("~/Content/theme/js/plugins/dataTables/datatables.min.js")
				.Include("~/Content/theme/js/plugins/toastr/toastr.min.js")
				.Include("~/Content/theme/js/plugins/toastr/toastr.config.js")
				.Include("~/Content/theme/js/plugins/iCheck/icheck.min.js")

				// Primero framework scripts
				.Include("~/Shared/JS/Ajax.js")
				.Include("~/Shared/JS/BrowserDetection.js")
				.Include("~/Shared/JS/Dialogs.js")
				.Include("~/Shared/JS/Formatting.js")
				.Include("~/Shared/JS/DataTables.js")
				.Include("~/Shared/JS/Lists.js")
				.Include("~/Shared/JS/Global.js")
				.Include("~/Shared/JS/Inspinia.js")
				.Include("~/Shared/JS/Controls.js")
				.Include("~/Shared/JS/UnsavedChanges.js")
				.Include("~/Shared/JS/MainLayout.js")
				.Include("~/Shared/JS/Menu.js")
				.Include("~/Shared/JS/JQExtensions.js")
				.Include("~/Shared/JS/Repeater.js")
				.Include("~/Shared/JS/Reports.js")
				.Include("~/Shared/JS/Shared.js")
				.Include("~/Shared/JS/SSO.js")
				.Include("~/Shared/JS/ValidationMsg.js")
				.Include("~/Shared/JS/Setup.js")

				// Shared business components
				.Include("~/Areas/Shared/Menu/JS/Menu.js")
				.Include("~/Areas/Shared/SessionInfo/JS/SessionInfo.js")
				.Include("~/Areas/Shared/ReportHistory/JS/ReportHistory.js")
				.Include("~/Areas/Shared/ClientLookup/JS/ClientLookup.js")
				.Include("~/Areas/Shared/GroupLookup/JS/GroupLookup.js")
				.Include("~/Areas/Reservation/Shared/GuestList/JS/GuestList.js")

				// Primero framework controls
				.Include("~/Controls/Auto/JS/Auto.js")
				.Include("~/Controls/Captcha/JS/Captcha.js")
				.Include("~/Controls/SearchHeader/JS/SearchHeader.js")
				.Include("~/Controls/Combo/JS/Combo.js")
				.Include("~/Controls/DataFileList/JS/DataFileList.js")
				.Include("~/Controls/DataFileUpload/JS/DataFileUpload.js")
				.Include("~/Controls/DateRange/JS/DateRange.js")
				.Include("~/Controls/MonthYear/JS/MonthYear.js")
				.Include("~/Controls/RichEditor/JS/RichEditor.js")
				.Include("~/Controls/MultiselectList/JS/MultiselectList.js"));
		}
	}
}
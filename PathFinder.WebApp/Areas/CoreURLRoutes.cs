namespace PathFinder.WebApp.Areas
{
    public static class CoreURLRoutes
    {
        public static class Account
        {
            public const string Register = "Account/Register";
            public const string GetUser = "Account/GetUser";
            public const string UpdateStatus = "Account/UpdateStatus";
            public const string UpdateUser = "Account/UpdateUser";
            public const string SuccessRegister = "Account/SuccessRegister";
            public const string AccountPending = "AccountPending";
            public const string AccountRejected = "AccountRejected";
            public const string Login = "account/Login";
            public const string Logout = "account/Logout/";
            public const string ForgetPassword = "account/ForgetPassword";
            public const string CheckCode = "account/CheckCode";
            public const string ResetPassword = "account/ResetPassword";
            public const string ChangePassword = "account/ChangePassword";
            public const string Add = "account/Add";
            public const string CheckAuthorize = "account/CheckAuthorize";
        }
        public static class Company
        {
            public const string GetCompanyPage = "company/GetPage";
            public const string IsCompanyExist = "company/iscompanyexist";
            public const string IsUserNameExist = "company/IsUserNameExist";
            public const string IsUpdatedUserNameExist = "company/IsUpdatedUserNameExist";
            public const string IsEmailExist = "company/IsEmailExist";
            public const string IsUpdatedEmailExist = "company/IsUpdatedEmailExist";
            public const string GetStatistics = "company/GetStatistics";
            public const string ChangeCompanyStatus = "company/ChangeCompanyStatus";
            public const string CreateAdminEmployee = "company/CreateAdminEmployee";
            public const string GetFeature = "company/GetFeatures";
        }

        public static class Role
        {
            public const string IsRoleExist = "role/isroleexist";
            public const string MakeDefaultRole = "role/makedefaultrole";
            public const string Claims = "role/RoleClaims";
            public const string GetPage = "role/getpage";
            public const string GetRoles = "role/GetLookUp";
            public const string GetPermissions = "role/Permissions";
        }

        public static class Settings
        {
            public const string GetEmailSettings = "settings/getemailsettings";
            public const string AddEmailSettings = "settings/addemailsettings";
            public const string UpdateEmailSettings = "settings/updateemailsettings";

            public const string GetTermsAndConditionsSettings = "settings/GetTermsAndConditions";
            public const string GetTermsAndConditionsForLogin = "settings/GetTermsAndConditionsForLogin";
            public const string AddTermsAndConditionsSettings = "settings/AddTermsAndConditions";
            public const string UpdateTermsAndConditionsSettings = "settings/UpdateTermsAndConditions";



            public const string GetCompanyGeneralSettings = "settings/getCompanyGeneralSettings";
            public const string AddCompanyGeneralSettings = "settings/addCompanyGeneralSettings";
            public const string UpdateCompanyGeneralSettings = "settings/updateCompanyGeneralSettings";

            public const string GetCompanyMobileHomeScreen = "settings/getCompanyMobileHomeScreen";
            public const string AddCompanyMobileHomeScreen = "settings/addCompanyMobileHomeScreen";
            public const string UpdateCompanyMobileHomeScreen = "settings/updateCompanyMobileHomeScreen";

            public const string GetCompanyMobileBannerAndLogo = "settings/GetCompanyMobileBannerAndLogo";
            public const string AddCompanyMobileBannerAndLogo = "settings/AddCompanyMobileBannerAndLogo";
            public const string UpdateCompanyMobileBannerAndLogo = "settings/UpdateCompanyMobileBannerAndLogo";
            public const string DeleteCompanyMobileBanner = "settings/DeleteCompanyMobileBanner?image=";
            public const string DeleteCompanyMobileLogo = "settings/DeleteCompanyMobileLogo?image=";

            public const string LoadCompanyAppearanceThemeDataTable = "settings/LoadCompanyAppearanceThemeDataTable";
            public const string GetAllCompanyAppearanceThemes = "settings/GetAllCompanyAppearanceThemes";
            public const string GetCompanyAppearanceTheme = "settings/getCompanyAppearanceTheme/";
            public const string AddCompanyAppearanceTheme = "settings/addCompanyAppearanceTheme";
            public const string UpdateCompanyAppearanceTheme = "settings/updateCompanyAppearanceTheme";
            public const string MakeCompanyThemeDefault = "settings/MakeCompanyThemeDefault";
            public const string DeleteCompanyTheme = "settings/DeleteCompanyTheme/";

            public const string GetCompanyBackendColorAndLogo = "settings/GetCompanyBackendColorAndLogo";
            public const string AddCompanyBackendColorAndLogo = "settings/AddCompanyBackendColorAndLogo";
            public const string UpdateCompanyBackendColorAndLogo = "settings/UpdateCompanyBackendColorAndLogo";
            public const string DeleteCompanyBackendLogo = "settings/DeleteCompanyBackendLogo?image=";
            public const string DeleteCompanyBackendFavicon = "settings/DeleteCompanyBackendFavicon?image=";
            public const string GetCompanyBackendEndDetails = "settings/GetCompanyBackEndDetailsBySubdomin";
        }
        public static class LookUp
        {
            public const string GetLookUpEmployee = "Employee/GetLookUp?empName=";
            public const string GetLookUpEmployeesList = "Employee/GetEmployeesByIds?csvIds=";
        }
        public static class News
        {
            public const string Default = "News";
            public const string Add = "News/Add";
            public const string Update = "News/Update/";
            public const string NewsPage = "News/GetPage";
            public const string NewsGrid = "News/GetGrid";
            public const string GetById = "News/GetById/";
            public const string Notify = "News/Notify/";
        }


        public static class Employee
        {
            public const string EmployeeDefault = "Account";
            public const string EmployeeGrid = "Account/GetGrid";
            public const string EmployeePage = "Account/GetPage";
            public const string EmployeeViewProfile = "Account/ViewProfile/";

            public const string GetEmployeeBankingInfo = "Employee/GetEmployeeBankingInfo/";
            public const string UpdateEmployeeBankingInfo = "Employee/UpdateEmployeeBankingInfo/";

            public const string DeactiveEmployee = "Employee/Deactive/";
            public const string AddEmployee = "Account/Register";
            public const string CheckEmployeeNationalIdIsUnique = "Employee/IsUniqueEmployeeNationalId";
            public const string CheckEmployeePassportNoIsUnique = "Employee/IsUniqueEmployeePassportNo";
            public const string CheckEmployeePhoneIsUnique = "Employee/IsUniqueEmployeePhone";
            public const string ExportEmployee = "Employee/Export";
            public const string ResetEmployeePassword = "Employee/ResetEmployeePassword";
            public const string GetEmployeeById = "Employee/GetEmloyeeById/";
            public const string RefreshQRCode = "Employee/RefreshQRCode/";
        }
        public static class EmployeeSetting
        {
            public const string GetEmployeeSetting = "EmployeeSetting/Get/";
            public const string SaveEmployeeSetting = "EmployeeSetting/Save";
        }
        public static class Notification
        {
            public const string SendNotification = "Notification/SendNotification/";
            public const string ControllerName = "Notifications";
            public const string HistoryAction = "History";
            public const string GetHistory = "Notification/GetHistory/";
            public const string GetById = "Notification/GetById/";
        }

        public static class Import
        {
            public const string ImportEmployeesDefault = "ImportEmployees";
            public const string Add = "ImportEmployees/add";
            public const string GetPage = "ImportEmployees/GetPage";
            public const string GetById = "ImportEmployees/GetById/";
        }

        public static class Dashboard
        {
            public const string ControllerName = "EmployeeDashboard";
            public const string GetAdminDashboard = "Dashboard/GetAdminDashboard";
            public const string GetHomeDashboard = "Dashboard/GetHomeDashboard";
            public const string GetUserStatisticsDashboard = "Dashboard/GetUserStatisticsDashboard";
            public const string GetIndustriesDashboard = "Dashboard/GetIndustriesDashboard";
            public const string GetContactUsDashboard = "Dashboard/GetContactUsDashboard";
            public const string GetUserLoginDashboard = "Dashboard/GetUserLoginDashboard";
            public const string GetCategoryWasteTransactionDashboard = "Dashboard/GetCategoryWasteTransactionDashboard";
            public const string GetWasteTransactionDashboard = "Dashboard/GetWasteTransactionDashboard";
        }

        public static class Version
        {
            public const string ControllerName = "Version";
            public const string Add = "Version/Add";
            public const string Update = "Version/Update/";
            public const string GetPage = "Version/GetPage";
            public const string GetById = "Version/GetById/";
            public const string IsMandatory = "Version/IsMandatory/";
            public const string GetWebsiteVersion = "Version/GetVersion/";
        }

        public static class Governorate
        {
            public const string GetLookUp = "Governorate/GetLookUp";
            public const string GetLookUpById = "Governorate/GetLookUpById/";

        }
        public static class Nationality
        {
            public const string GetLookUp = "Nationality/GetLookUp/";
            public const string GetLookUpById = "Nationality/GetLookUpById/";

        }
        public static class City
        {
            public const string GetLookUp = "City/GetLookUp/";
            public const string GetLookUpById = "City/GetLookUpById/";

        }
        public static class Center
        {
            public const string GetLookUp = "Center/GetLookUp/";
            public const string GetLookUpById = "Center/GetLookUpById/";
        }
    }
}

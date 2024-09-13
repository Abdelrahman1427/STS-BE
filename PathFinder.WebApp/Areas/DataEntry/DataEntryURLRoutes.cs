namespace PathFinder.WebApp.Areas.DataEntry
{
    public static class DataEntryURLRoutes
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

        public static class Beneficiaries
        {
            public const string ControllerName = "Beneficiaries";
            public const string AddBeneficiaries = "Beneficiaries/add";
            public const string GetBeneficiaries = "Beneficiaries/GetById/";
            public const string UpdateBeneficiaries = "Beneficiaries/update/";
            public const string GetPageBeneficiaries = "Beneficiaries/GetPage/";



        }

        public static class CourseTransaction
        {
            public const string ControllerName = "CourseTransaction";
            public const string AddCourseTransaction = "CourseTransaction/add";
            public const string GetCourseTransaction = "CourseTransaction/GetById/";
            public const string UpdateCourseTransaction = "CourseTransaction/update/";
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

using PathFinder.SharedKernel.Constants;

namespace PathFinder.SharedKernel.Polices.Mapping
{
    public static class GenericPermission
    {
        public static string GetDynamicPolicy(string controlllerName, string actionName)
        {
            List<string> controllersDefinition = new List<string>()
            {
                "Category"
            };

            bool isControllerNameInDefinitions = controllersDefinition.Exists(a => a == controlllerName);
            if (isControllerNameInDefinitions)
            {
                switch (actionName)
                {
                    case AuthoriationConstants.Index:
                        return PolicyConstants.CanViewDefinitionsPolicy;

                    case AuthoriationConstants.LoadDataTable:
                        return PolicyConstants.CanViewDefinitionsPolicy;

                    case AuthoriationConstants.GetPage:
                        return PolicyConstants.CanViewDefinitionsPolicy;

                    case AuthoriationConstants.Add:
                        return PolicyConstants.CanAddDefinitionsPolicy;

                    case AuthoriationConstants.Update:
                        return PolicyConstants.CanUpdateDefinitionsPolicy;

                    case AuthoriationConstants.GetById:
                        return PolicyConstants.CanUpdateDefinitionsPolicy;

                    case AuthoriationConstants.Delete:
                        return PolicyConstants.CanDeleteDefinitionsPolicy;

                    default:
                        return "";
                }

            }
            return "";

        }
    }
}

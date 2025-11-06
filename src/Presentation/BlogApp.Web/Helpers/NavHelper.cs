namespace BlogApp.Web.Helpers
{
    public static class NavHelper
    {
        public static string IsActive(HttpContext context, string targetPath, bool exactMatch = false)
        {
            if (context == null || string.IsNullOrEmpty(targetPath))
                return "text-gray-700 dark:text-gray-300 hover:text-primary font-semibold text-sm";

            var currentPath = context.Request.Path.Value ?? "";
            var currentPathLower = currentPath.ToLower();
            var targetPathLower = targetPath.ToLower();

            bool isActive;

            if (exactMatch)
            {
                isActive = currentPathLower == targetPathLower;
            }
            else
            {
                // Ana sayfa özel durumu
                if (targetPathLower == "/")
                {
                    isActive = currentPathLower == "/";
                }
                else
                {
                    isActive = currentPathLower.StartsWith(targetPathLower) &&
                               (currentPathLower == targetPathLower ||
                                currentPathLower[targetPathLower.Length] == '/');
                }
            }

            return isActive
                ? "text-primary dark:text-white font-bold text-sm"
                : "text-gray-700 dark:text-gray-300 hover:text-primary font-semibold text-sm";
        }
    }
}

using Microsoft.AspNetCore.Mvc.Filters;
namespace ASP_Lr11.Filters
{
    public class UniqueUserCounterFilter : IActionFilter
    {
        private static readonly object _lock = new object();
        private static HashSet<string> _uniqueUsers = new HashSet<string>();
        private static readonly string filePath = "UniqueUsersCount.txt";
        private static readonly string usersListPath = "UniqueUsersList.txt";

        static UniqueUserCounterFilter()
        {
            LoadUniqueUsers();
            UpdateUserCount();
        }

        private static void LoadUniqueUsers()
        {
            lock (_lock)
            {
                if (File.Exists(usersListPath))
                {
                    var users = File.ReadAllLines(usersListPath).ToList();
                    _uniqueUsers = new HashSet<string>(users);
                }
            }
        }

        private static void UpdateUserCount()
        {
            lock (_lock)
            {
                File.WriteAllText(filePath, $"Unique users count: {_uniqueUsers.Count}");
            }
        }

        private static void AddUniqueUser(string user)
        {
            lock (_lock)
            {
                if (_uniqueUsers.Add(user))
                {
                    File.AppendAllText(usersListPath, user + Environment.NewLine);
                    UpdateUserCount();
                }
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var userIdentifier = context.HttpContext.Request.Cookies["UserIdentifier"];
            if (string.IsNullOrEmpty(userIdentifier))
            {
                userIdentifier = Guid.NewGuid().ToString();
                context.HttpContext.Response.Cookies.Append("UserIdentifier", userIdentifier);
            }

            AddUniqueUser(userIdentifier);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
using Slugify;

namespace ContentVnptApplication.Helpers
{
    public class SlugHelperService
    {
        public static string Generate(string title)
        {
            var helper = new SlugHelper();
            return helper.GenerateSlug(title);
        }
    }
}
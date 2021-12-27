using DAL.EF;

namespace Tests.Integration_Tests.Util
{
    internal class FakeDbInitializer
    {
        public static void Initialize(BlogContext context)
        {
            var fakeData = new FakeData();

            context.Articles.AddRange(fakeData.Articles);
            context.Comments.AddRange(fakeData.Comments);
            context.Tags.AddRange(fakeData.Tags);
            context.Users.AddRange(fakeData.Users);

            context.SaveChanges();
        }
    }
}
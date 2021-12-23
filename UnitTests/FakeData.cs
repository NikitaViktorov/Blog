using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using DAL.Entities;

namespace UnitTests
{
    internal class FakeData
    {
        public FakeData()
        {
            var fixture = new Fixture();

            //Костыль под модели мыкыты
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            Articles = fixture.Build<Article>()
                .CreateMany(5)
                .ToList();

            Comments = fixture.CreateMany<Comment>(5)
                .ToList();

            Tags = fixture.Build<Tag>()
                .CreateMany(5)
                .ToList();

            Users = fixture.CreateMany<User>(5)
                .ToList();
        }

        public List<Article> Articles { get; }
        public List<Comment> Comments { get; }
        public List<Tag> Tags { get; }
        public List<User> Users { get; }
    }
}
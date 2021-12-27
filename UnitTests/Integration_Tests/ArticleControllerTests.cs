using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using Newtonsoft.Json;
using Tests.Integration_Tests.Util;
using Xunit;

namespace Tests.Integration_Tests
{
    public class ArticleControllerTests
    {
        private readonly BaseTestFixture _fixture;
        private readonly IMapper _mapper = AutoMapperProfile.InitializeAutoMapper().CreateMapper();

        public ArticleControllerTests()
        {
            _fixture = new BaseTestFixture();
        }

        [Fact]
        public async Task Get_ShouldReturnListResult()
        {
            // Act
            var response = await _fixture.Client.GetAsync("api/Article");
            response.EnsureSuccessStatusCode();
            var models =
                JsonConvert.DeserializeObject<ICollection<Article>>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.NotEmpty(models);
        }

        [Fact]
        public async Task Post_ShouldCreateNewArticle()
        {
            //Arrange
            var fixture = new FakeData();
            var fakeArticle = _mapper.Map<ArticleDto>(fixture.Articles.First());
            var fakeArticleJson = JsonConvert.SerializeObject(fakeArticle);
            var userId = _fixture.DbContext.Users.First().Id.ToString();
            var httpContent = new StringContent(fakeArticleJson, Encoding.UTF8, "application/json");

            //Act
            var response = await _fixture.Client.PostAsync("api/Article/CreateArticle/" + userId, httpContent);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.Contains(_fixture.DbContext.Articles, article => article.Text == fakeArticle.Text);
        }

        //[Fact]
        //public async Task Get_ShouldGetUserArticles()
        //{
        //    //Act
        //    var response = await _fixture.Client.GetAsync("api/Article/GetUserArticles");
        //    response.EnsureSuccessStatusCode();
        //    var models = JsonConvert.DeserializeObject<ICollection<ArticleDto>>(await response.Content.ReadAsStringAsync());

        //    // Assert
        //    Assert.NotEmpty(models);

        //}
    }
}
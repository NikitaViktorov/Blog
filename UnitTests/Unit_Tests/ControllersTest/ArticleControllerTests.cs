using System;
using System.Threading.Tasks;
using AutoMapper;
using BLL.AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using Blog.Controllers;
using Moq;
using Xunit;

namespace Tests.Unit_Tests.ControllersTest
{
    public class ArticleControllerTests
    {
        private readonly ArticleController _articleController;
        private readonly Mock<IArticleService> _articleServiceMock;
        private FakeData _fakeData;
        private IMapper _mapper = AutoMapperProfile.InitializeAutoMapper().CreateMapper();

        public ArticleControllerTests()
        {
            _articleServiceMock = new Mock<IArticleService>();
            _articleController = new ArticleController(_articleServiceMock.Object);
            _fakeData = new FakeData();
        }

        [Fact]
        public async Task Get_ServiceInvoke()
        {
            //Act
            await _articleController.GetArticle(It.IsAny<Guid>());

            //Assert
            _articleServiceMock.Verify(service => service.Get(It.IsAny<Guid>()));
        }

        [Fact]
        public async Task GetAll_ServiceInvoke()
        {
            //Act
            await _articleController.GetArticles();

            //Assert
            _articleServiceMock.Verify(service => service.GetAll());
        }

        [Fact]
        public async Task Create_ServiceInvoke()
        {
            //Act
            await _articleController.CreateArticle(It.IsAny<ArticleDto>(), It.IsAny<string>());

            //Assert
            _articleServiceMock.Verify(service => service.Create(It.IsAny<ArticleDto>(), It.IsAny<string>()));
        }
    }
}
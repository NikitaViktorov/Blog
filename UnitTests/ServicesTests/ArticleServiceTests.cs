using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTOs;
using BLL.Services;
using DAL.Entities;
using DAL.Interfaces;
using Moq;
using Xunit;

namespace UnitTests.ServicesTests
{
    public class ArticleServiceTests
    {
        private readonly ArticleService _articleService;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IArticleRepository> _articleRepositoryMock;

        public ArticleServiceTests()
        {
            var fakeData = new FakeData();

            _articleRepositoryMock = new Mock<IArticleRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();

            _articleRepositoryMock.Setup(r => r.Get(It.IsAny<Guid>()))
                .ReturnsAsync(fakeData.Articles.First());

            _unitOfWorkMock.Setup(u => u.Articles)
                .Returns(_articleRepositoryMock.Object);

            _articleService = new ArticleService(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Get_RepositoryInvokes()
        {
            //Arrange
            _mapperMock.Setup(mapper => mapper.Map<ArticleDto>(It.IsAny<Article>()))
                .Returns(new ArticleDto());

            //Act
            await _articleService.Get(It.IsAny<Guid>());

            //Assert
            _unitOfWorkMock.Verify(uow => uow.Articles);
            _articleRepositoryMock.Verify(r => r.Get(It.IsAny<Guid>()));
            _mapperMock.Verify(mapper => mapper.Map<ArticleDto>(It.IsAny<Article>()));
        }

        [Fact]
        public async Task GetAll_RepositoryInvokes()
        {
            //Arrange
            _mapperMock.Setup(mapper => mapper.Map<ICollection<ArticleDto>>(It.IsAny<ICollection<Article>>()))
                .Returns(new List<ArticleDto>());

            //Act
            await _articleService.GetAll();

            //Assert
            _unitOfWorkMock.Verify(uow => uow.Articles);
            _articleRepositoryMock.Verify(r => r.GetAll());
            _mapperMock.Verify(mapper => mapper.Map<ICollection<ArticleDto>>(It.IsAny<ICollection<Article>>()));
        }

        [Fact]
        public async Task Create_RepositoryInvokes()
        {
            //Arrange
            _mapperMock.Setup(mapper => mapper.Map<Article>(It.IsAny<ArticleDto>()))
                .Returns(new Article());

            //Act
            await _articleService.Create(It.IsAny<ArticleDto>());

            //Assert
            _unitOfWorkMock.Verify(uow => uow.Articles);
            _articleRepositoryMock.Verify(r => r.Create(It.IsAny<Article>()));
        }

        [Fact]
        public async Task Update_RepositoryInvokes()
        {
            //Arrange
            _mapperMock.Setup(mapper => mapper.Map<Article>(It.IsAny<ArticleDto>()))
                .Returns(new Article());

            //Act
            await _articleService.Update(It.IsAny<Guid>(), It.IsAny<ArticleDto>());

            //Assert
            _unitOfWorkMock.Verify(uow => uow.Articles);
            _articleRepositoryMock.Verify(r => r.Update(It.IsAny<Article>()));
        }

        [Fact]
        public async Task Delete_RepositoryInvokes()
        {
            //Act
            await _articleService.Delete(It.IsAny<Guid>());

            //Assert
            _unitOfWorkMock.Verify(uow => uow.Articles);
            _articleRepositoryMock.Verify(r => r.Delete(It.IsAny<Guid>()));
        }

        [Fact]
        public async Task AddTag_RepositoryInvokes()
        {
            //Act
            await _articleService.AddTag(It.IsAny<Guid>(), It.IsAny<TagDto>());

            //Assert
            _unitOfWorkMock.Verify(uow => uow.Articles);
            _articleRepositoryMock.Verify(r => r.Update(It.IsAny<Article>()));
        }

        [Fact]
        public async Task GetArticleByText_RepositoryInvokes()
        {
            //Arrange
            _mapperMock.Setup(mapper => mapper.Map<ArticleDto>(It.IsAny<Article>()))
                .Returns(new ArticleDto());

            //Act
            await _articleService.GetArticleByText(It.IsAny<string>());

            //Assert
            _unitOfWorkMock.Verify(uow => uow.Articles);
            _articleRepositoryMock.Verify(r => r.GetArticleByText(It.IsAny<string>()));
        }

        [Fact]
        public async Task GetArticlesByTag_RepositoryInvokes()
        {
            //Arrange
            _mapperMock.Setup(mapper => mapper.Map<ICollection<ArticleDto>>(It.IsAny<ICollection<Article>>()))
                .Returns(new List<ArticleDto>());

            //Act
            await _articleService.GetArticlesByTag(It.IsAny<Guid>());

            //Assert
            _unitOfWorkMock.Verify(uow => uow.Articles);
            _articleRepositoryMock.Verify(r => r.GetArticlesByTag(It.IsAny<Guid>()));
        }

        [Fact]
        public async Task GetUserArticles_RepositoryInvokes()
        {
            //Arrange
            _mapperMock.Setup(mapper => mapper.Map<ICollection<ArticleDto>>(It.IsAny<ICollection<Article>>()))
                .Returns(new List<ArticleDto>());

            //Act
            await _articleService.GetUserArticles(It.IsAny<Guid>());

            //Assert
            _unitOfWorkMock.Verify(uow => uow.Articles);
            _articleRepositoryMock.Verify(r => r.GetUserArticles(It.IsAny<Guid>()));
        }
    }
}
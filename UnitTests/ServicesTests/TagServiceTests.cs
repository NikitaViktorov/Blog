using System;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTOs;
using BLL.Services;
using DAL.Entities;
using DAL.Interfaces;
using Moq;
using Xunit;

namespace UnitTests.ServicesTests
{
    public class TagServiceTests
    {
        private readonly Mock<ITagRepository> _tagRepositoryMock;
        private readonly TagService _tagService;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public TagServiceTests()
        {
            var fakeData = new FakeData();
            _tagRepositoryMock = new Mock<ITagRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _tagRepositoryMock.Setup(r => r.Get(It.IsAny<Guid>()))
                .ReturnsAsync(fakeData.Tags.First());

            _unitOfWorkMock.Setup(u => u.Tags)
                .Returns(_tagRepositoryMock.Object);

            _tagService = new TagService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Get_RepositoryInvokes()
        {
            //Act
            await _tagService.Get(It.IsAny<Guid>());

            //Assert
            _unitOfWorkMock.Verify(uow => uow.Tags);
            _tagRepositoryMock.Verify(r => r.Get(It.IsAny<Guid>()));
        }

        [Fact]
        public async Task GetAll_RepositoryInvokes()
        {
            //Act
            await _tagService.GetAll();

            //Assert
            _unitOfWorkMock.Verify(uow => uow.Tags);
            _tagRepositoryMock.Verify(r => r.GetAll());
        }

        [Fact]
        public async Task Create_RepositoryInvokes()
        {
            //Act
            await _tagService.Create(It.IsAny<TagDto>());

            //Assert
            _unitOfWorkMock.Verify(uow => uow.Tags);
            _tagRepositoryMock.Verify(r => r.Create(It.IsAny<Tag>()));
        }

        [Fact]
        public async Task Update_RepositoryInvokes()
        {
            //Arrange
            var tagDto = new TagDto();

            //Act
            await _tagService.Update(It.IsAny<Guid>(), tagDto);

            //Assert
            _unitOfWorkMock.Verify(uow => uow.Tags);
            _tagRepositoryMock.Verify(r => r.Update(It.IsAny<Tag>()));
        }

        [Fact]
        public async Task Delete_RepositoryInvokes()
        {
            //Act
            await _tagService.Delete(It.IsAny<Guid>());

            //Assert
            _unitOfWorkMock.Verify(uow => uow.Tags);
            _tagRepositoryMock.Verify(r => r.Delete(It.IsAny<Guid>()));
        }
    }
}
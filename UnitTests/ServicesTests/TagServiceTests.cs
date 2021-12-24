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
    public class TagServiceTests
    {
        private readonly TagService _tagService;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ITagRepository> _tagRepositoryMock;

        public TagServiceTests()
        {
            var fakeData = new FakeData();
            _tagRepositoryMock = new Mock<ITagRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();

            _tagRepositoryMock.Setup(r => r.Get(It.IsAny<Guid>()))
                .ReturnsAsync(fakeData.Tags.First());

            _unitOfWorkMock.Setup(u => u.Tags)
                .Returns(_tagRepositoryMock.Object);

            _tagService = new TagService(_unitOfWorkMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Get_RepositoryInvokes()
        {
            //Arrange
            _mapperMock.Setup(mapper => mapper.Map<TagDto>(It.IsAny<Tag>()))
                .Returns(new TagDto());

            //Act
            await _tagService.Get(It.IsAny<Guid>());

            //Assert
            _unitOfWorkMock.Verify(uow => uow.Tags);
            _tagRepositoryMock.Verify(r => r.Get(It.IsAny<Guid>()));
            _mapperMock.Verify(mapper => mapper.Map<TagDto>(It.IsAny<Tag>()));
        }

        [Fact]
        public async Task GetAll_RepositoryInvokes()
        {
            //Arrange
            _mapperMock.Setup(mapper => mapper.Map<ICollection<TagDto>>(It.IsAny<ICollection<Tag>>()))
                .Returns(new List<TagDto>());

            //Act
            await _tagService.GetAll();

            //Assert
            _unitOfWorkMock.Verify(uow => uow.Tags);
            _tagRepositoryMock.Verify(r => r.GetAll());
            _mapperMock.Verify(mapper => mapper.Map<ICollection<TagDto>>(It.IsAny<ICollection<Tag>>()));
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
            //Act
            await _tagService.Update(It.IsAny<Guid>(), It.IsAny<TagDto>());

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
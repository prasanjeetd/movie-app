using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieApp.Controllers;
using MovieApp.Models;
using MovieApp.Services;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace MovieApp.UnitTests
{
    public class ControllerTests
    {
        IRepository repository;
        Mock<IMovieService> mockRepo;

        List<Movie> seed = new List<Movie>
            {
                new Movie { Title= "Swadesh", Year=2000 ,Actors = new string []  {"Sharukh" ,"Amrita"} },
                new Movie { Title= "Krish 3", Year=2007 ,Actors = new string []  {"Hritik" ,"Priyanka"} },
                 new Movie { Title= "Avenger", Year=2019 ,Actors = new string []  {"Robert" ,"Chris"} },
                new Movie { Title= "War", Year=2019 ,Actors = new string []  {"Hritik" ,"Tiger"} }
            };

        public ControllerTests()
        {
            this.repository = (new Mock<IRepository>()).Object;

            this.repository.movies = seed;

            mockRepo = new Mock<IMovieService>();

        }

        [Fact]
        public void Test_Controller_GetAll()
        {

            // Arrange
            int pageSize = 4;

            mockRepo.Setup(x => x.GetN(It.IsAny<int>()))
                .Returns(seed);

            var controller = new MovieController(mockRepo.Object);

            //Act
            var response = controller.GetAll();

            //Assert
            mockRepo.Verify(x => x.GetN(pageSize), Times.Once);
            var result = (Assert.IsType<ObjectResult>(response.Result)) as ObjectResult;

            var items = Assert.IsType<List<Movie>>(result.Value);
            Assert.Equal(pageSize, items.Count);

        }

        [Fact]
        public void Test_Controller_GetAll_WithParameter()
        {

            // Arrange
            int pageSize = 3;

            mockRepo.Setup(x => x.GetN(It.IsAny<int>()))
                .Returns(seed.Take(pageSize).ToList());

            var controller = new MovieController(mockRepo.Object);

            //Act
            var response = controller.GetAll(pageSize);

            //Assert
            mockRepo.Verify(x => x.GetN(pageSize), Times.Once);
            var result = (Assert.IsType<ObjectResult>(response.Result)) as ObjectResult;

            var items = Assert.IsType<List<Movie>>(result.Value);
            Assert.Equal(pageSize, items.Count);

        }

        [Theory]
        [InlineData("Billa")]
        public void Test_Controller_Get_NotFound(string title)
        {

            mockRepo.Setup(x => x.Get(It.IsAny<string>()))
                .Returns(seed.FirstOrDefault(x => x.Title == title));

            var controller = new MovieController(mockRepo.Object);

            //Act
            var response = controller.Get(title);

            //Assert
            mockRepo.Verify(x => x.Get(title), Times.Once);
            Assert.IsType<NotFoundObjectResult>(response.Result);

        }

        [Fact]
        public void Test_Controller_Get()
        {

            // Arrange
            string title = "Avenger";

            mockRepo.Setup(x => x.Get(It.IsAny<string>()))
                .Returns(seed.FirstOrDefault(x => x.Title == title));

            var controller = new MovieController(mockRepo.Object);

            //Act
            var response = controller.Get(title);

            //Assert
            mockRepo.Verify(x => x.Get(title), Times.Once);
            var result = (Assert.IsType<OkObjectResult>(response.Result)) as OkObjectResult;

            var movie = Assert.IsType<Movie>(result.Value);
            Assert.Equal(title, movie.Title);

        }
    }

}

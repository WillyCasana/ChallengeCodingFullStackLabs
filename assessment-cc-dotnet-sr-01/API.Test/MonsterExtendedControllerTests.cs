using System.Diagnostics;
using API.Controllers;
using API.Test.Fixtures;
using FluentAssertions;
using Lib.Repository.Entities;
using Lib.Repository.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace API.Test;

public class MonsterExtendedControllerTests
{
    private readonly Mock<IBattleOfMonstersRepository> _repository;

    public MonsterExtendedControllerTests()
    {
        _repository= new Mock<IBattleOfMonstersRepository> ();
    }


    [Fact]
    public async Task Post_OnSuccess_ImportCsvToMonster()
    {
        //arrange
        var fileMock= new Mock<IFormFile> ();
        var content = "name,attack,defense,speed,hp,imageUrl\nTest Monster,50,40,60,80,test.jpg";
        var fileName = "test.csv";
        var ms= new MemoryStream ();    
        var writer= new StreamWriter (ms);
        writer.Write (content);
        writer.Flush ();
        ms.Position = 0;
        fileMock.Setup(_ =>_.OpenReadStream()).Returns(ms);
        fileMock.Setup(_ =>_.FileName).Returns(fileName);
        fileMock.Setup(_ =>_.Length).Returns(ms.Length);

        _repository.Setup(x => x.Monsters.AddAsync(It.IsAny<IEnumerable<Monster>>())).Returns(Task.CompletedTask);
        _repository.Setup(x => x.Save()).ReturnsAsync(1);

        var controller= new MonsterController(_repository.Object);
       
        //act
        var result= await controller.ImportCsv(fileMock.Object);

        //assert
        result.Should().BeOfType<OkResult>();
    }

    [Fact]
    public async Task Post_BadRequest_ImportCsv_With_Nonexistent_Monster()
    {
        //arrange
        var fileMock = new Mock<IFormFile>();
        var content = "name,attack,defense,speed,hp,imageUrl\nNonexistent Monster,50,40,60,80,test.jpg";
        var fileName = "test.csv";
        var ms = new MemoryStream();
        var writer = new StreamWriter(ms);
        writer.Write(content);
        writer.Flush();
        ms.Position = 0;
        fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
        fileMock.Setup(_ => _.FileName).Returns(fileName);
        fileMock.Setup(_ => _.Length).Returns(ms.Length);

        var controller = new MonsterController(_repository.Object);
        //act
        var result = await controller.ImportCsv(fileMock.Object);


        //assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Post_BadRequest_ImportCsv_With_Nonexistent_Column()
    {
        //arrange
        var fileMock = new Mock<IFormFile>();
        var content = "wrongColumn,attack,defense,speed,hp,imageUrl\nNonexistent Monster,50,40,60,80,test.jpg";
        var fileName = "test.csv";
        var ms = new MemoryStream();
        var writer = new StreamWriter(ms);
        writer.Write(content);
        writer.Flush();
        ms.Position = 0;
        fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
        fileMock.Setup(_ => _.FileName).Returns(fileName);
        fileMock.Setup(_ => _.Length).Returns(ms.Length);

        var controller = new MonsterController(_repository.Object);


        //act
        var result = await controller.ImportCsv(fileMock.Object);

        //assert
        result.Should().BeOfType<BadRequestObjectResult>();
        var objectR = (BadRequestObjectResult)result;

        Assert.Equal("Wrong data mapping.", objectR.Value);
    }
}

using API.Controllers;
using API.Test.Fixtures;
using FluentAssertions;
using Lib.Repository.Entities;
using Lib.Repository.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace API.Test;

public class BattleExtendedControllerTests
{
    private readonly Mock<IBattleOfMonstersRepository> _repository;

    [Fact]
    public async Task Post_OnNoMonsterFound_When_StartBattle_With_NonexistentMonster()
    {
        // @TODO missing implementation
        Assert.True(true);
    }

    [Fact]
    public async Task Post_OnSuccess_Returns_With_MonsterAWinning()
    {
        // @TODO missing implementation
        Assert.True(true);
    }


    [Fact]
    public async Task Post_OnSuccess_Returns_With_MonsterBWinning()
    {
        // @TODO missing implementation
        Assert.True(true);
    }

    [Fact]
    public async Task Post_OnSuccess_Returns_With_MonsterAWinning_When_TheirSpeedsSame_And_MonsterA_Has_Higher_Attack()
    {
        // @TODO missing implementation
        Assert.True(true);
    }

    [Fact]
    public async Task Post_OnSuccess_Returns_With_MonsterBWinning_When_TheirSpeedsSame_And_MonsterB_Has_Higher_Attack()
    {
        // @TODO missing implementation
        Assert.True(true);
    }

    [Fact]
    public async Task Post_OnSuccess_Returns_With_MonsterAWinning_When_TheirDefensesSame_And_MonsterA_Has_Higher_Speed()
    {
        // @TODO missing implementation
        Assert.True(true);
    }

    [Fact]
    public async Task Delete_OnSuccess_RemoveBattle()
    {
        // @TODO missing implementation
        Assert.True(true);
    }

    [Fact]
    public async Task Delete_OnNoBattleFound_Returns404()
    {
        // @TODO missing implementation
        Assert.True(true);
    }
}

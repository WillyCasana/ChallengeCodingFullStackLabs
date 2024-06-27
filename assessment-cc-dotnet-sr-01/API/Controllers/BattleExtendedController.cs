using Lib.Repository.Entities;
using Lib.Repository.Repository;
using Lib.Repository.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


public class BattleExtendedController : BaseApiController
{
    private readonly IBattleOfMonstersRepository _repository;

    public BattleExtendedController(IBattleOfMonstersRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Add([FromBody] Battle battle)
    {

        var monsterA = await _repository.Monsters.FindAsync(battle.MonsterA);
        var monsterB = await _repository.Monsters.FindAsync(battle.MonsterB);

        if (monsterA == null)
        {
            return new BadRequestObjectResult("Missing ID");
        }


        await _repository.Battles.AddAsync(battle);
        await _repository.Save();
        return Ok(battle);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Remove(int id)
    {
        var existingBattle= _repository.Battles.FindAsync(id);
        if (existingBattle == null)
        {
            return NotFound($"The Battle with ID = {id} not found.");
        }
        await _repository.Battles.RemoveAsync(id);
        await _repository.Save();

        return Ok();
    }
}

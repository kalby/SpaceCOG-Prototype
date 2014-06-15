using System;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

[assembly: InternalsVisibleTo("PlayerController")]
public class TestNUnitExample: Attribute
    
{
    [Test]
    public void PlayerTakesDamage()
    {
        PlayerController playerTwo = new PlayerController();
        playerTwo.maxHealth = 100;
        playerTwo.Hit(20);
        Assert.That(playerTwo.currentHealth < 100);
        Assert.That(playerTwo.currentHealth >= 0);
    }

    [Test]
    public void AeonurTakesDamage()
    {
        AeonurReceiveDamage aeonur = new AeonurReceiveDamage();
        aeonur.maximumHealth = 1000;
        aeonur.SendMessage("Hit", 20);

        Assert.That(aeonur.currentHealth < 1000);
        Assert.That(aeonur.currentHealth >= 0);
    }
}

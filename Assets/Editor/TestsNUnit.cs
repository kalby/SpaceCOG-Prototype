using System;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

/**
 * This script is responsible for testing the code only 
 */
public class TestsNUnit: Attribute
    
{
    [Test]
    public void PlayerTakesDamage()
    {   // Create a new playerController
        PlayerController playerTwo = new PlayerController();
        // Set the health of the player
        playerTwo.maxHealth = 100;
        // Do damage to the player
        playerTwo.Hit(20);
        // Assert that damage has been dealt to the player
        Assert.That(playerTwo.currentHealth < 100);
        // Assert that the player is still alive
        Assert.That(playerTwo.currentHealth >= 0);
    }

    [Test]
    public void AeonurTakesDamage()
    {
        // Create a new AeonurReceiveDamage object 
        AeonurReceiveDamage aeonur = new AeonurReceiveDamage();
        // Set the health
        aeonur.maximumHealth = 1000;
        // Do damage to the object
        aeonur.SendMessage("Hit", 20);
        // Assert that damage has been dealth to the object
        Assert.That(aeonur.currentHealth < 1000);
        // Assert that the object is still alive
        Assert.That(aeonur.currentHealth >= 0);
    }
}

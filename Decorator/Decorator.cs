using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Decorator : MonoBehaviour
{
    public void Start()
    {

        Race orc = new Orc();
        Race elf = new Elves();

        Race farmerSoldierOrc = new ProfessionDecorator(new Soldier(),new ProfessionDecorator(new Farmer(), orc));
        farmerSoldierOrc.Render();

    }
}

public class ProfessionDecorator : Race {

    public Race decoratedRace;
    public Profession prof;
    public ProfessionDecorator(Profession prof, Race race)
    {
        decoratedRace = race;
        this.prof = prof;
    }
    public override void Render()
    {
        prof.Render();
        decoratedRace.Render();
    }
}

public abstract class Race
{
    public string name;
    public abstract void Render();
}

public class Elves : Race
{

    public Elves()
    {
        name = "Elf";
    }

    public override void Render()
    {
        Debug.Log(name);
    }
}

public class Orc : Race
{
    public Orc()
    {
        name = "Orc";
    }
    public override void Render()
    {
        Debug.Log(name);
    }
}

public abstract class Profession
{
    public string professionName;
    public abstract void Render();

}

public class Farmer : Profession
{
    public Farmer()
    {
        professionName = "Farmer";
    }
    public override void Render()
    {

        Debug.Log(professionName);

    }
}

public class Soldier : Profession
{
    public Soldier()
    {
        professionName = "Soldier";
    }
    public override void Render()
    {

        Debug.Log(professionName);

    }
}

public class Shaman : Profession
{
    public Shaman()
    {
        professionName = "Shaman";
    }
    public override void Render()
    {
        
       Debug.Log(professionName);
        
    }
}

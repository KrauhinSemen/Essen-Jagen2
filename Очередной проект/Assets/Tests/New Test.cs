using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NewTest
{
    [Test]
    public void SimpleTest1()
    {
        var eater = new Eating();
        eater.maxX = 100;
        eater.maxY = 100;
        eater.minX = 0;
        eater.minY = 0;
        eater.distanceFood = 5;
        Assert.AreEqual(eater.CheckingPoint(0, 0, 0, 0), false);
    }

    [Test]
    public void SimpleTest2()
    {
        var eater = new Eating();
        eater.maxX = 100;
        eater.maxY = 100;
        eater.minX = 0;
        eater.minY = 0;
        eater.distanceFood = 5;
        Assert.AreEqual(eater.CheckingPoint(0, 0, 5, 0), true);
    }

    [Test]
    public void SimpleTest3()
    {
        var eater = new Eating();
        eater.maxX = 100;
        eater.maxY = 100;
        eater.minX = 0;
        eater.minY = 0;
        eater.distanceFood = 5;
        Assert.AreEqual(eater.CheckingPoint(0, 0, 4.9f, 1), true);
    }

    [Test]
    public void SimpleTest4()
    {
        var eater = new Eating();
        eater.maxX = 100;
        eater.maxY = 100;
        eater.minX = 0;
        eater.minY = 0;
        eater.distanceFood = 5;
        Assert.AreEqual(eater.CheckingPoint(0, 0, 4.9f, 0), false);
    }

    [Test]
    public void SimpleTest5()
    {
        var eater = new Eating();
        eater.maxX = 100;
        eater.maxY = 100;
        eater.minX = 0;
        eater.minY = 0;
        eater.distanceFood = 5;
        Assert.AreEqual(eater.CheckingPoint(0, 0, 0, 5), true);
    }

    [Test]
    public void SimpleTest6()
    {
        var eater = new Eating();
        eater.maxX = 100;
        eater.maxY = 100;
        eater.minX = 0;
        eater.minY = 0;
        eater.distanceFood = 5;
        Assert.AreEqual(eater.CheckingPoint(0, 0, 5, 5), true);
    }

    [Test]
    public void SimpleTest7()
    {
        var eater = new Eating();
        eater.maxX = 100;
        eater.maxY = 100;
        eater.minX = 0;
        eater.minY = 0;
        eater.distanceFood = 5;
        Assert.AreEqual(eater.CheckingPoint(50, 50, 47.5f, 50), false);
    }

    [Test]
    public void SimpleTest8()
    {
        var eater = new Eating();
        eater.maxX = 100;
        eater.maxY = 100;
        eater.minX = 0;
        eater.minY = 0;
        eater.distanceFood = 5;
        Assert.AreEqual(eater.CheckingPoint(0, 100, 5, 0), true);
    }

    [Test]
    public void SimpleTest9()
    {
        var eater = new Eating();
        eater.maxX = 100;
        eater.maxY = 100;
        eater.minX = 0;
        eater.minY = 0;
        eater.distanceFood = 5;
        Assert.AreEqual(eater.CheckingPoint(0.7f, 7.8f, 5.9f, 48.8f), true);
    }
}

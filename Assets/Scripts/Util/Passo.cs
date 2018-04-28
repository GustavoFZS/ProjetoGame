﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passo : IComparable<Passo>
{
    public int x;
    public int y;
    public int peso;
    public Passo anterior;

    public Passo(int x, int y, int peso, Passo anterior)
    {
        this.x = x;
        this.y = y;
        this.peso = peso;
        this.anterior = anterior;
    }

    public int CompareTo(Passo obj)
    {
        if (obj == null) return 1;

        if (this.peso == obj.peso) return 1;

        return this.peso - obj.peso;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Passo p = (Passo)obj;

        return (x == p.x) && (y == p.y);
    }

    public override int GetHashCode()
    {
        return x^y;
    }

    public override string ToString()
    {
        return "X:" + x + " Y:" + y + " Peso:" + peso;
    }

}
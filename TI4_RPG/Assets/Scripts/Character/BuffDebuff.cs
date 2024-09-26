
using UnityEngine;

public class BuffDebuff
{
    public float timer;
    public string tipo;
    public int power;
    public Character alvo;

    public BuffDebuff(float timer, string tipo, Character alvo, int power)
    {
        this.timer = timer;
        this.tipo = tipo;
        this.alvo = alvo;
        this.power = power;
    }
}

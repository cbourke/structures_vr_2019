using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This needs a vector for position, direction. A magnitude, and string for type(dead, live, etc) */
public class Load
{
    private Vector3 position;
    private Vector3 direction;
    private float magnitude;
    private loadType type;

    // Start is called before the first frame update
    public Load()
    {
    }

    public Load(Vector3 position, Vector3 direction, float magnitude, loadType type = loadType.Dead)
    {
        this.position = position;
        this.direction = direction;
        this.magnitude = magnitude;
        this.type = type;
    }

    void setPosition(Vector3 position) {
        this.position = position;
    }

    Vector3 getPosition() {
        return this.position;
    }

    void setDirection(Vector3 direction) {
        this.direction = direction;
    }

    Vector3 getDirection() {
        return this.direction;
    }
    void setMagnitude(float magnitude) {
        this.magnitude = magnitude;
    }

    float getMagnitude() {
        return this.magnitude;
    }
    void setType(loadType type) {
        this.type = type;
    }

    loadType getType() {
        return this.type;
    }
}

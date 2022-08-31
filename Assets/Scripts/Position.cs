using System;
using System.Collections;
using System.Collections.Generic;

public class Position
{
    public int rows;
    public int cols;
    public int distribution;

    public Position(int rows, int cols, int distribution)
    {
        this.rows = rows;
        this.cols = cols;
        this.distribution = distribution;
    }
}

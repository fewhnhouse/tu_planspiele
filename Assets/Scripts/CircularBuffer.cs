using UnityEngine;
using System.Collections;

public class CircularBuffer
{

    private int[] internalArray;
    private int index = 0;
    private int size = 0;

    public CircularBuffer(int size)
    {
        this.size = size;
        this.internalArray = new int[size];

        Clear();
    }

    public void Push(int i)
    {
        internalArray[index] = i;
        index = (index + 1) % size;
    }

    public void Clear()
    {
        for (int i = 0; i < size; i++)
        {
            internalArray[i] = 0;
        }
    }

    //valid when all entries are the same and none are minvalue
    public bool Valid()
    {
        int lastValue = internalArray[0];

        for (int i = 1; i < size; i++)
        {
            if (internalArray[i] == 0 || internalArray[i] != lastValue)
                return false;
        }

        return true;
    }
}

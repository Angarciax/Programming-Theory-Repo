using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze 
{
    private int[,] mazeData;
    const int defaultSize = 16;
    const int defaultBlockLenght = 4;
    const int key = 5 , badEnemy = 4, goodEnemy = 3 ,player = 2 , wall = 1, empty = 0;
    public int size { get; } //ENCAPSULATION
    public int blockLenght { get; } //ENCAPSULATION
    public Maze( int requiredSize, int requiredBlockLenght) // POLIMORPHISM
    {
        size = requiredSize;
        blockLenght = requiredBlockLenght;
        CreateMaze(requiredSize);
    }
    public Maze( int requiredSize) // POLIMORPHISM
    {
        size = requiredSize;
        blockLenght = defaultBlockLenght;
        CreateMaze( requiredSize );
    }
    public Maze() // POLIMORPHISM
    {
        size = defaultSize ;
        blockLenght = defaultBlockLenght;
        CreateMaze(size);
    }

    public bool IsWall( int posY, int posX) // ABSTRACTION
    {
        if (mazeData[posY, posX] == wall) return true;
        return false;
    }

    public bool IsEmpty(int posY, int posX) // ABSTRACTION
    {
        if (mazeData[posY, posX] == empty) return true;
        return false;
    }

    private int SetObject( int objectType )
    {
        int siteY = 0;
        int siteX = 0;
        while( !IsEmpty(siteY, siteX))
        {
            siteY = Random.Range(1, size);
            siteX = Random.Range(1, size);
        }
        mazeData[siteY, siteX] = objectType;
        return (siteY * 100) + siteX; // Convention: maze position = Y*100 + X
    }
    public int SetKey() // ABSTRACTION
    {
        return SetObject(key);
    }

    public int SetPlayer()  // ABSTRACTION
    {
        return SetObject(player);
    }

    public int SetGoodEnemy() // ABSTRACTION
    {
        return SetObject(badEnemy);
    }

    public int SetBadEnemy() // ABSTRACTION
    {
        return SetObject(goodEnemy);
    }
    protected void CreateMaze( int rSize ) // 
    {
        mazeData = new int[rSize, rSize];
        int countLoop = 0, maxLoop = 1000 ; // Counter to prevent freezes

        // Paint perimeter and upper door
        for (int posX = 0; posX < rSize; posX++)
        {
            mazeData[0, posX] = wall;
            mazeData[rSize-1, posX] = wall;
        }
        mazeData[rSize - 1, rSize - 3] = empty;
        for (int posY = 1; posY < rSize; posY++)
        {
            mazeData[posY, 0] = wall;
            mazeData[posY, rSize - 1] = wall;
        }
        for (int posY = 1; posY < size - 2; posY++)
            for (int posX = 1; posX < size - 2; posX++)
                mazeData[posY, posX] = empty;
                // paint random walls

        for ( int bSize = blockLenght; bSize >= 1 ; bSize--)
        {
            for( int bLength = 1; bLength <= blockLenght-bSize+1 ; bLength++)
            {
                int siteY=-1, siteX=-1 ;
                // Horizontal
                bool fits = false;
                countLoop = 0;
                while( !fits && countLoop < maxLoop )
                {
                    countLoop++;
                    siteY = Random.Range(1, size);
                    siteX = Random.Range(1, size);
                    fits = fitsBlock(bSize, true, siteY, siteX);
                }
                if( countLoop < maxLoop) 
                    SetBlock(bSize, true, siteY, siteX);

                // Vertical
                fits = false;
                countLoop = 0;
                while (!fits && countLoop < maxLoop)
                {
                    countLoop++;
                    siteY = Random.Range(1, size);
                    siteX = Random.Range(1, size);
                    fits = fitsBlock(bSize, false, siteY, siteX);
                }
                if (countLoop < maxLoop) 
                    SetBlock(bSize, false, siteY, siteX );
            }
        }
        // Added one by the walls
        SetBlock(1, false, 1, Random.Range(2, size - 3));
        SetBlock(1, false, size-2, Random.Range(2, size - 4));
        SetBlock(1, false, Random.Range(2, size - 3), 1);
        SetBlock(1, false, Random.Range(2, size - 3), size-2);

        // Finally set Player
        mazeData[1, 1] = player;
    }

    private void SetBlock(int blockSize, bool horizontal, int posY, int posX)
    {
        if (horizontal)
        {
            for (int x = 0; x < blockSize; x++)
                mazeData[posY, x + posX] = wall;
        }
        else
        {
            for (int y = 0; y < blockSize; y++)
                mazeData[posY + y, posX] = wall;
        }
    }
    private bool fitsBlock( int blockSize, bool horizontal, int posY, int posX )
    {
        if (IsWall(posY, posX))
            return false;
        if( horizontal)
        {
            if (posX + blockSize >= size - 1) return false;
            for (int X = posX; X < posX + blockSize; X++)
                if (surrounded(posY, X)) return false;
        }
        else
        {
            if (posY + blockSize >= size - 1) return false;
            for (int Y = posY; Y < posY + blockSize; Y++)
                if (surrounded(Y,posX)) return false;
        }
        return true;
    }

    private bool surrounded ( int posY, int posX)
    {
        if (mazeData[posY - 1, posX - 1] == wall) return true;
        if (mazeData[posY - 1, posX] == wall) return true;
        if (mazeData[posY - 1, posX + 1] == wall) return true;
        if (mazeData[posY , posX + 1] == wall) return true;
        if (mazeData[posY , posX - 1] == wall) return true;
        if (mazeData[posY + 1, posX + 1] == wall) return true;
        if (mazeData[posY + 1, posX ] == wall) return true;
        if (mazeData[posY + 1, posX - 1] == wall) return true;

        return false;
    }
}

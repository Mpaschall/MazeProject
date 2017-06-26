using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    class Square
    {
        //updated 6/25/17 to make strings print correctly
        //variables for position
        int xPos, yPos, startX, startY, endX, endY;
        string sqstring, type;

        //default constructor
        public Square()
        {
        }

        //overloaded constructor
        public Square(int xPos, int yPos, string type)
        {
            this.xPos = xPos;
            this.yPos = yPos;
            this.type = type;
        }

        public string start()
        {
            sqstring = "....S....";
            return sqstring;
        }

        public string fin()
        {
            sqstring = "....F....";
            return sqstring;
        }

        public string opSq()
        {
            sqstring = ".........";
            return sqstring;
        }

        public string wall()
        {
            sqstring = "xxxxxxxxx";
            return sqstring;
        }

        public void moveRight()
        {
            xPos++;
        }

        public void moveLeft()
        {
            xPos--;
        }

        public void moveDown()
        {
            yPos++;
        }

        public void moveUp()
        {
            yPos--;
        }

        public void setX(int xPos)
        {
            this.xPos = xPos;
        }

        public int getX()
        {
            return xPos;
        }

        public void setY(int yPos)
        {
            this.yPos = yPos;
        }

        public int getY()
        {
            return yPos;
        }

        public void setType(string type)
        {
            this.type = type;
        }

        public string getType()
        {
            return type;
        }
    }
}

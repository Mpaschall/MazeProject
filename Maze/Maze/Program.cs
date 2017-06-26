using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Maze
{
    class Program
    {
        static void Main(string[] args)
        {
            //create variables to use with square
            int count2 = 0;
            int count = 0;
            int boardCount = 0;
            string startX, startY, endX, endY, coor, boardX, boardY, temp, temp2, wallX, wallY, fullMaze, curr;
            string line1 = "";
            string line2 = "";
            string line3 = "";
            string sqString = "";
            object test;

            //get input from file
            string[] input = System.IO.File.ReadAllLines(@"C:\Users\xdark\Documents\Visual Studio 2015\Projects\Maze\sqcoordinates.txt");
            string[] coord;
            object[,] squares = new object[10, 11];
            LLCreate linked = new LLCreate();
            Stack<string> stack = new Stack<string>();

            for (int i = 0; i < input.Length; i++)
            {

                //if input does not have parenthesis, contains sentinel, count is < 1,
                //get board size and create the array for the board
                if (!input[i].Contains('(') && !input[i].Contains(')') && count < 1 && input[i].Contains('.'))
                {
                    coord = input[i].Split(',');
                    coor = coord[i];
                    boardX = coor;
                    coor = coord[i + 1];
                    boardY = coor.Substring(1, 2);
                    count++;
                }
                //else if count >= 1 < 2 and contains sentinel this is the starting coordinate, get it
                else if (count >= 1 && count < 2 && input[i].Contains('.'))
                {
                    coord = input[i].Split(',');
                    coor = coord[0];
                    startX = (coor.Remove(0, 1));
                    coor = coord[1];
                    temp = (coor.Replace(')', ' '));
                    temp2 = (temp.Replace('.', ' '));
                    startY = temp2.Trim();
                    Square square = new Square(int.Parse(startX), int.Parse(startY), "start");
                    squares[int.Parse(startX), int.Parse(startY)] = square;
                    count++;
                }

                //else if count >= 2 < 3 and contains sentinel, get final square
                else if (count >= 2 && count < 3 && input[i].Contains('.'))
                {
                    coord = input[i].Split(',');
                    coor = coord[0];
                    endX = (coor.Remove(0, 1));
                    coor = coord[1];
                    temp = (coor.Replace(')', ' '));
                    temp2 = (temp.Replace('.', ' '));
                    endY = temp2.Trim();
                    Square square = new Square(int.Parse(endX), int.Parse(endY), "end");
                    squares[int.Parse(endX), int.Parse(endY)] = square;
                    linked.addToTail(square);
                    count++;
                }

                //else if count >= 3 and does not contain sentinel
                else if (count >= 3 && count < 42)
                {
                    coord = input[i].Split(',');
                    coor = coord[0];
                    wallX = (coor.Remove(0, 1));
                    coor = coord[1];
                    temp = (coor.Replace(')', ' '));
                    temp2 = (temp.Replace('.', ' '));
                    wallY = temp2.Trim();
                    Square square = new Square(int.Parse(wallX), int.Parse(wallY), "wall");
                    squares[int.Parse(wallX), int.Parse(wallY)] = square;
                    count++;
                }

                //add open cells to the array
                else
                {
                    int rowLength = squares.GetLength(0);
                    int colLength = squares.GetLength(1);

                    for (int h = 0; h < rowLength; h++)
                    {
                        for (int j = 0; j < colLength; j++)
                        {
                            if (squares[h, j] == null)
                            {
                                Square square = new Square(h, j, "open");
                                squares[h, j] = square;
                            }
                        }
                    }
                }

            }

            //edited 6/18/17 to add items from the array to the linked list and print those items

            foreach (Square square in squares)
            {
                if (square.getType() == "start")
                {
                    linked.addToTail(square.start());
                }
                else if (square.getType() == "end")
                {
                    linked.addToTail(square.fin());
                }
                else if (square.getType() == "open")
                {
                    linked.addToTail(square.opSq());
                }
                else if (square.getType() == "wall")
                {
                    linked.addToTail(square.wall());
                }

            }
            
            //updated 6/25/17 to print with streamwriter to mazePrint.txt
            //left linked list adding in to minimize changes

            StreamWriter writer = new StreamWriter("mazePrint.txt");

            foreach (Square square in squares)
            {
                if (count2 < 9)
                {
                    if (square.getType() == "start")
                    {
                        sqString = square.start();
                        line1 += sqString.Substring(0, 3);
                        line2 += sqString.Substring(3, 3);
                        line3 += sqString.Substring(6, 3);
                        linked.addToTail(square.start());
                        count2++;
                    }
                    else if (square.getType() == "end")
                    {
                        sqString = square.fin();
                        line1 += sqString.Substring(0, 3);
                        line2 += sqString.Substring(3, 3);
                        line3 += sqString.Substring(6, 3);
                        linked.addToTail(square.fin());
                        count2++;
                    }
                    else if (square.getType() == "open")
                    {
                        sqString = square.opSq();
                        line1 += sqString.Substring(0, 3);
                        line2 += sqString.Substring(3, 3);
                        line3 += sqString.Substring(6, 3);
                        linked.addToTail(square.opSq());
                        count2++;
                    }
                    else if (square.getType() == "wall")
                    {
                        sqString = square.wall();
                        line1 += sqString.Substring(0, 3);
                        line2 += sqString.Substring(3, 3);
                        line3 += sqString.Substring(6, 3);
                        linked.addToTail(square.wall());
                        count2++;
                    }
                }

                //added 6/25/17 this prints the maze in the console and file
                else
                {
                    writer.WriteLine(line1);
                    Console.WriteLine(line1);
                    writer.WriteLine(line2);
                    Console.WriteLine(line2);
                    writer.WriteLine(line3);
                    Console.WriteLine(line3);
                    line1 = "";
                    line2 = "";
                    line3 = "";
                    count2 = 0;
                }
             
            }

            writer.Close();

            //stack implementation added 6/25/17 
            //adds squares to the stack from squares array, goes back by popping if it encounters a wall 
            //break if encounters end
            foreach (Square square in squares)
            {
                    if (square.getType() == "start")
                    {
                        Console.WriteLine("\nThis is the start");
                        stack.Push(square.start());
                        Console.WriteLine("stack top is :" + stack.Peek());
                    }
                    else if (square.getType() == "end")
                    {
                        Console.WriteLine("This is the end");
                        break;
                    }
                    else if (square.getType() == "open")
                    {
                        Console.WriteLine("This is open");
                        stack.Push(square.opSq());
                        Console.WriteLine("stack top is : " + stack.Peek());

                    }
                    else if (square.getType() == "wall")
                    {
                        Console.WriteLine("This is a wall");
                        stack.Pop();
                        Console.WriteLine("stack top is : " + stack.Peek());

                    }
            }
            //prints linked list, commented out 6/25/17
            // linked.printList();
        }
    }
}

using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver
{
    internal class Tile
    {
        public int id;
        public int rotation;
        public int type;

        private static List<List<String>> tiles;

        // constructor

        public Tile(int Id, int Rotation)
        {
            this.id = Id;
            this.rotation = Rotation;

            if (this.id == 0) this.type = 0;
            else if (this.id <= 5) this.type = 1;
            else if (this.id == 7) this.type = 6 + this.rotation;
            else this.type = 2 + this.rotation;
        }

        // public methods

        public static void generateTiles()
        {
            tiles = new List<List<String>>();
            string[] lines = System.IO.File.ReadAllLines(@"..\..\..\Tiles.txt");

            for (int i = 0; i < 10 * 6; i += 6)
            {
                tiles.Add(new List<String>() { lines[i],
                                               lines[i + 1],
                                               lines[i + 2],
                                               lines[i + 3],
                                               lines[i + 4] });
            }
        }

        public void print()
        {
            for (int i = 0; i < 5; i++)
            {
                this.print(i);
                Console.WriteLine();
            }
        }

        public void print(int line)
        {
            String s = "";

            switch (this.rotation)
            {
                case 0:
                    for (int i = 0; i < 5; i++)
                        s += tiles[id][line][i] + " ";
                    break;

                case 1:
                    for (int i = 0; i < 5; i++)
                        s += tiles[id][4 - i][line] + " ";
                    break;

                case 2:
                    for (int i = 0; i < 5; i++)
                        s += tiles[id][4 - line][4 - i] + " ";
                    break;

                case 3:
                    for (int i = 0; i < 5; i++)
                        s += tiles[id][i][4 - line] + " ";
                    break;
            }

            Console.Write(s);
        }

        public void rotate(int r)
        {
            this.rotation += r;

            switch (this.id)
            {
                case 0: this.rotation  = 0; break;
                case 1: this.rotation  = 0; break;
                case 2: this.rotation %= 4; break;
                case 3: this.rotation %= 4; break;
                case 4: this.rotation %= 2; break;
                case 5: this.rotation %= 4; break;
                case 6: this.rotation %= 4; break;
                case 7: this.rotation %= 4; break;
                case 8: this.rotation %= 4; break;
                case 9: this.rotation %= 4; break;

            }
        }

        public void mirror()
        {
            if (this.id == 0 || this.id == 1 || this.id == 4) return;

            if (this.id == 2)
            {
                if (this.rotation == 0) { this.rotation = 2; return; }
                if (this.rotation == 2) { this.rotation = 0; return; }
                return;
            }

            if (this.id == 5)
            {
                if (this.rotation == 1) { this.rotation = 3; return; }
                if (this.rotation == 3) { this.rotation = 1; return; }
                return;
            }

            else if (this.rotation == 0) this.rotation = 1;
            else if (this.rotation == 1) this.rotation = 0;
            else if (this.rotation == 2) this.rotation = 3;
            else if (this.rotation == 3) this.rotation = 2;

            if (this.id == 8) { this.id = 9; return; }
            if (this.id == 9) { this.id = 8; return; }
        }

    }
}

/*  Piece tiles
 * 
 *  0:0
 *  _ _ _ _ _
 *  _ _ _ _ _
 *  _ _ _ _ _
 *  _ _ _ _ _
 *  _ _ _ _ _
 *
 *  1:0
 *  _ _ _ _ _
 *  _ _ _ _ _
 *  _ _ _ _ _
 *  _ _ _ _ _
 *  _ _ _ _ _
 *
 *  2:0        2:1          2:2         2:3
 *  x x x x x   _ _ _ _ x   _ _ _ _ _   x _ _ _ _
 *  _ _ _ _ _   _ _ _ _ x   _ _ _ _ _   x _ _ _ _
 *  _ _ _ _ _   _ _ _ _ x   _ _ _ _ _   x _ _ _ _
 *  _ _ _ _ _   _ _ _ _ x   _ _ _ _ _   x _ _ _ _
 *  _ _ _ _ _   _ _ _ _ x   x x x x x   x _ _ _ _
 *
 *  3:0         3:1         3:2         3:3
 *  x x x x x   _ _ _ _ x   x _ _ _ _   x x x x x
 *  _ _ _ _ x   _ _ _ _ x   x _ _ _ _   x _ _ _ _
 *  _ _ _ _ x   _ _ _ _ x   x _ _ _ _   x _ _ _ _
 *  _ _ _ _ x   _ _ _ _ x   x _ _ _ _   x _ _ _ _
 *  _ _ _ _ x   x x x x x   x x x x x   x _ _ _ _
 * 
 *  4:0         4:1
 *  x x x x x   x _ _ _ x
 *  _ _ _ _ _   x _ _ _ x
 *  _ _ _ _ _   x _ _ _ x
 *  _ _ _ _ _   x _ _ _ x
 *  x x x x x   x _ _ _ x
 * 
 *  5:0         5:1         5:2         5:3
 *  x x x x x   x _ _ _ x   x x x x x   x x x x x
 *  _ _ _ _ x   x _ _ _ x   x _ _ _ _   x _ _ _ x
 *  _ _ _ _ x   x _ _ _ x   x _ _ _ _   x _ _ _ x
 *  _ _ _ _ x   x _ _ _ x   x _ _ _ _   x _ _ _ x
 *  x x x x x   x x x x x   x x x x x   x _ _ _ x
 * 
 *  6:0         6:1         6:2         6:3
 *  x x x _ _   _ _ _ _ x   x _ _ _ _   _ _ x x x
 *  _ _ _ x _   _ _ _ _ x   x _ _ _ _   _ x _ _ _
 *  _ _ _ _ x   _ _ _ _ x   x _ _ _ _   x _ _ _ _
 *  _ _ _ _ x   _ _ _ x _   _ x _ _ _   x _ _ _ _
 *  _ _ _ _ x   x x x _ _   _ _ x x x   x _ _ _ _
 * 
 *  7:0         7:1         7:2         7:3
 *  _ _ _ x x   _ _ _ _ _   _ _ _ _ _   x x _ _ _
 *  _ _ _ _ x   _ _ _ _ _   _ _ _ _ _   x _ _ _ _
 *  _ _ _ _ _   _ _ _ _ _   _ _ _ _ _   _ _ _ _ _
 *  _ _ _ _ _   _ _ _ _ x   x _ _ _ _   _ _ _ _ _
 *  _ _ _ _ _   _ _ _ x x   x x _ _ _   _ _ _ _ _
 *  
 *  8:0         8:1         8:2         8:3
 *  x x x _ _   x x x x x   x _ _ _ x   _ _ x x x
 *  x _ _ x _   _ _ _ _ x   x _ _ _ x   _ x _ _ _
 *  x _ _ _ x   _ _ _ _ x   x _ _ _ x   x _ _ _ _
 *  x _ _ _ x   _ _ _ x _   _ x _ _ x   x _ _ _ _
 *  x _ _ _ x   x x x _ _   _ _ x x x   x x x x x
 * 
 *  9:0         9:1         9:2         9:3
 *  x x x _ _   x _ _ _ x   x x x x x   _ _ x x x
 *  _ _ _ x _   x _ _ _ x   x _ _ _ _   _ x _ _ x
 *  _ _ _ _ x   x _ _ _ x   x _ _ _ _   x _ _ _ x
 *  _ _ _ _ x   x _ _ x _   _ x _ _ _   x _ _ _ x
 *  x x x x x   x x x _ _   _ _ x x x   x _ _ _ x
 * 
 * 
 * 
 * 
 */

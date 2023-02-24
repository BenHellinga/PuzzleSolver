using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver
{
    internal class Board
    {
        public int width;
        public int height;
        public List<Piece> pieces;

        // constructor

        public Board(int Width, int Height)
        {
            this.width = Width;
            this.height = Height;

            this.pieces = new List<Piece>();
        }

        // public methods

        public bool place(Piece piece, int x, int y, List<List<int>> types)
        {
            if (!checkCollision(piece, x, y, types)) return false;

            this.pieces.Add(piece);

            piece.x = x;
            piece.y = y;

            return true;
        }

        public void print()
        {
            Console.Write("x");
            for (int i = 0; i < this.width * 5 + 3; i++)
                Console.Write(" x");
            Console.WriteLine();

            Console.Write("x ");
            for (int i = 0; i < this.width * 5 + 1; i++)
                Console.Write("  ");
            Console.WriteLine("  x");

            for (int i = 0; i < this.height * 5; i++)
            {
                Console.Write("x   ");

                int x = 0;
                bool piecePrinted;

                while (x < this.width)
                {
                    piecePrinted = false;
                    foreach (Piece piece in this.pieces)
                    {
                        if (piece.x <= x && piece.x + piece.width > x && piece.y <= i / 5 && piece.y + piece.height - 1 >= i / 5)
                        {
                            piecePrinted = piece.print(i - piece.y * 5, x - piece.x);
                            if (!piecePrinted) continue;
                            break;
                        }
                    }

                    if (!piecePrinted)
                        Console.Write("          ");

                    piecePrinted = false;
                    x++;
                }

                Console.WriteLine("  x");
            }

            Console.Write("x ");
            for (int i = 0; i < this.width * 5 + 1; i++)
                Console.Write("  ");
            Console.WriteLine("  x");

            Console.Write("x");
            for (int i = 0; i < this.width * 5 + 3; i++)
                Console.Write(" x");
            Console.WriteLine();
        }

        // private methods

        /*
        private bool checkCollision(Piece piece, int px, int py)
        {
            Tile testTile;
            Tile placeTile;

            int testType;
            int placeType;

            if (px + piece.width > this.width || py + piece.height > this.height)
                return false;

            foreach (Piece testPiece in this.pieces)
            {
                if (testPiece.x > px + piece.width - 1) continue;
                if (testPiece.x + testPiece.width - 1 < px) continue;

                if (testPiece.y > py + piece.height - 1) continue;
                if (testPiece.y + testPiece.height - 1 < py) continue;

                int ix = Math.Max(testPiece.x, px);
                int iy = Math.Max(testPiece.y, py);

                int ox = Math.Min(testPiece.x + testPiece.width - ix, px + piece.width - ix);
                int oy = Math.Min(testPiece.y + testPiece.height - iy, py + piece.height - iy);

                //Console.WriteLine(testPiece.id + " " + px + " " + py + " " + testPiece.x + " " + testPiece.y + " " + ix + " " + iy + " " + ox + " " + oy);

                for (int y = iy; y < iy + oy; y++)
                {
                    for (int x = ix; x < ix + ox; x++)
                    {
                        testTile = testPiece.tiles[(y - testPiece.y) * testPiece.width + (x - testPiece.x)];
                        placeTile = piece.tiles[(y - py) * piece.width + (x - px)];

                        if (testTile.id == 0) testType = 0;
                        else if (testTile.id <= 5) testType = 1;
                        else if (testTile.id == 7) testType = 3;
                        else testType = 2;

                        if (placeTile.id == 0) placeType = 0;
                        else if (placeTile.id <= 5) placeType = 1;
                        else if (placeTile.id == 7) placeType = 3;
                        else placeType = 2;

                        if (testType == 0 || placeType == 0) continue;
                        if (testType == 1 || placeType == 1) return false;
                        if (testType == 2 && placeType == 2) return false;
                        if (testTile.rotation == placeTile.rotation) continue;
                        return false;
                    }
                }
            }

            return true;
        }
        */

        private bool checkCollision(Piece piece, int px, int py, List<List<int>> types)
        {
            for (int y = 0; y < piece.height; y++)
            {
                for (int x = 0; x < piece.width; x++)
                {
                    if (types[py + y][px + x] == 0 || piece.tiles[y * piece.width + x].type == 0) continue;
                    if (types[py + y][px + x] == 1 || piece.tiles[y * piece.width + x].type == 1) return false;
                    if (types[py + y][px + x] >= 6 && piece.tiles[y * piece.width + x].type >= 6) return false;
                    
                    if (Math.Abs(types[py + y][px + x] - piece.tiles[y * piece.width + x].type) == 4) continue;
                    return false;
                }
            }
            return true;
        }
    }
}

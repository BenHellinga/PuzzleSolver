using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver
{
    internal class Solver
    {
        private List<Piece> pieces;
        private Board board;
        private char[] progress;

        static void Main(string[] args)
        {
            Solver solver = new Solver();

            List<List<int>> types;
            DateTime start;
            DateTime end;

            start = DateTime.Now;
            
            if (solver.solve1(0, solver.makeTypes()))
                solver.board.print();
            else
                Console.WriteLine("No solution found");
            

            end = DateTime.Now;

            Console.WriteLine("Finished in " + (end - start));
        }

        public Solver()
        {
            board = new Board(9, 6);
            pieces = new List<Piece>();
            progress = "0 0/0 0/0, 0 0/0 0/0".ToCharArray();

        Tile.generateTiles();
            generatePieces();
        }

        private void generatePieces()
        {
            pieces.Add(new Piece(3, 2, new List<int>() { 3, 2, 3, 3, 2, 6},  
                                       new List<int>() { 3, 0, 0, 2, 2, 1}, 0));

            pieces.Add(new Piece(4, 2, new List<int>() { 3, 2, 4, 5, 3, 6, 0, 0 },
                                       new List<int>() { 3, 0, 0, 0, 2, 1, 0, 0 }, 1));

            pieces.Add(new Piece(4, 2, new List<int>() { 5, 2, 3, 7, 0, 6, 2, 5},
                                       new List<int>() { 2, 0, 0, 2, 0, 2, 2, 0}, 2));

            pieces.Add(new Piece(4, 2, new List<int>() { 5, 2, 2, 8, 7, 3, 6, 0},
                                       new List<int>() { 2, 0, 0, 1, 0, 2, 1, 0}, 3));

            pieces.Add(new Piece(5, 2, new List<int>() { 5, 4, 2, 4, 8, 0, 7, 5, 0, 0},
                                       new List<int>() { 2, 0, 0, 0, 1, 0, 0, 1, 0, 0}, 4));

            pieces.Add(new Piece(3, 3, new List<int>() { 3, 2, 8, 3, 2, 0, 7, 5, 0},
                                       new List<int>() { 3, 0, 1, 2, 1, 0, 0, 1, 0}, 5));

            pieces.Add(new Piece(3, 3, new List<int>() { 3, 3, 7, 6, 2, 3, 0, 0, 9},
                                       new List<int>() { 3, 0, 2, 2, 2, 0, 0, 0, 1}, 6));

            pieces.Add(new Piece(3, 3, new List<int>() { 3, 3, 0, 6, 1, 5, 0, 8, 0},
                                       new List<int>() { 3, 0, 0, 2, 0, 0, 0, 2, 0}, 7));

            pieces.Add(new Piece(3, 3, new List<int>() { 3, 2, 8, 2, 3, 0, 9, 0, 0 },
                                       new List<int>() { 3, 0, 1, 3, 1, 0, 1, 0, 0 }, 8));
        }

        public void printPieces()
        {
            foreach (Piece piece in pieces)
            {
                piece.print();
            }
        }

        // private methods

        private List<List<int>> makeTypes()
        {
            List<List<int>> types = new List<List<int>>()
            {
                new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            };

            return types;
        }

        private List<List<int>> placePiece(List<List<int>> types, Piece piece)
        {
            List<List<int>> newTypes = new List<List<int>>();

            for (int y = 0; y < this.board.height; y++)
            {
                newTypes.Add(new List<int>());
                for (int x = 0; x < this.board.width; x++)
                {
                    newTypes[y].Add(types[y][x]);
                }
            }

            for (int y = 0; y < piece.height; y++)
            {
                for (int x = 0; x < piece.width; x++)
                {
                    if (piece.tiles[y * piece.width + x].type == 0) continue;

                    if (newTypes[y + piece.y][x + piece.x] == 0) newTypes[y + piece.y][x + piece.x] = piece.tiles[y * piece.width + x].type;
                    else newTypes[y + piece.y][x + piece.x] = 1;
                }
            }

            return newTypes;
        }

        private void printTypes(List<List<int>> types)
        {
            for (int y = 0; y < this.board.height; y++)
            {
                for (int x = 0; x < this.board.width; x++)
                {
                    Console.Write(types[y][x]);
                }
                Console.WriteLine();
            }
        }







        public bool solve(int piece, List<List<int>> types)
        {
            if (piece == 9) return true;

            
            Console.WriteLine();
            board.print();
            Console.WriteLine(piece);
            

            if (piece < 3) Console.WriteLine(this.progress);

            if (piece < 2)
            {
                this.progress[piece * 11] = '0';
                this.progress[piece * 11 + 4] = (char)(this.board.height - this.pieces[piece].height + 48);
                this.progress[piece * 11 + 8] = (char)(this.board.width - this.pieces[piece].width + 48);
            }

            for (int y = 0; y < this.board.height - this.pieces[piece].height + 1; y++)
            {
                for (int x = 0; x < this.board.width - this.pieces[piece].width + 1; x++)
                {
                    if (piece < 2)
                    {
                        this.progress[piece * 11 + 2] = (char)(y + 48);
                        this.progress[piece * 11 + 6] = (char)(x + 48);
                    }

                    if (this.board.place(this.pieces[piece], x, y, types))
                    {
                        if (solve(piece + 1, placePiece(types, this.pieces[piece]))) return true;
                        else this.board.pieces.RemoveAt(piece);
                    }
                }
            }

            this.pieces[piece].rotate(1);

            if (piece < 2)
            {
                this.progress[piece * 11] = '1';
                this.progress[piece * 11 + 4] = (char)(this.board.height - this.pieces[piece].height + 48);
                this.progress[piece * 11 + 8] = (char)(this.board.width - this.pieces[piece].width + 48);
            }

            for (int y = 0; y < this.board.height - this.pieces[piece].height + 1; y++)
            {
                for (int x = 0; x < this.board.width - this.pieces[piece].width + 1; x++)
                {
                    if (piece < 2)
                    {
                        this.progress[piece * 11 + 2] = (char)(y + 48);
                        this.progress[piece * 11 + 6] = (char)(x + 48);
                    }

                    if (this.board.place(this.pieces[piece], x, y, types))
                    {
                        if (solve(piece + 1, placePiece(types, this.pieces[piece]))) return true;
                        else this.board.pieces.RemoveAt(piece);
                    }
                }
            }

            this.pieces[piece].rotate(1);

            if (piece < 2)
            {
                this.progress[piece * 11] = '2';
                this.progress[piece * 11 + 4] = (char)(this.board.height - this.pieces[piece].height + 48);
                this.progress[piece * 11 + 8] = (char)(this.board.width - this.pieces[piece].width + 48);
            }

            for (int y = 0; y < this.board.height - this.pieces[piece].height + 1; y++)
            {
                for (int x = 0; x < this.board.width - this.pieces[piece].width + 1; x++)
                {
                    if (piece < 2)
                    {
                        this.progress[piece * 11 + 2] = (char)(y + 48);
                        this.progress[piece * 11 + 6] = (char)(x + 48);
                    }

                    if (this.board.place(this.pieces[piece], x, y, types))
                    {
                        if (solve(piece + 1, placePiece(types, this.pieces[piece]))) return true;
                        else this.board.pieces.RemoveAt(piece);
                    }
                }
            }

            this.pieces[piece].rotate(1);

            if (piece < 2)
            {
                this.progress[piece * 11] = '3';
                this.progress[piece * 11 + 4] = (char)(this.board.height - this.pieces[piece].height + 48);
                this.progress[piece * 11 + 8] = (char)(this.board.width - this.pieces[piece].width + 48);
            }

            for (int y = 0; y < this.board.height - this.pieces[piece].height + 1; y++)
            {
                for (int x = 0; x < this.board.width - this.pieces[piece].width + 1; x++)
                {
                    if (piece < 2)
                    {
                        this.progress[piece * 11 + 2] = (char)(y + 48);
                        this.progress[piece * 11 + 6] = (char)(x + 48);
                    }

                    if (this.board.place(this.pieces[piece], x, y, types))
                    {
                        if (solve(piece + 1, placePiece(types, this.pieces[piece]))) return true;
                        else this.board.pieces.RemoveAt(piece);
                    }
                }
            }

            this.pieces[piece].rotate(1);
            this.pieces[piece].mirror();

            if (piece < 2)
            {
                this.progress[piece * 11] = '4';
                this.progress[piece * 11 + 4] = (char)(this.board.height - this.pieces[piece].height + 48);
                this.progress[piece * 11 + 8] = (char)(this.board.width - this.pieces[piece].width + 48);
            }

            for (int y = 0; y < this.board.height - this.pieces[piece].height + 1; y++)
            {
                for (int x = 0; x < this.board.width - this.pieces[piece].width + 1; x++)
                {
                    if (piece < 2)
                    {
                        this.progress[piece * 11 + 2] = (char)(y + 48);
                        this.progress[piece * 11 + 6] = (char)(x + 48);
                    }

                    if (this.board.place(this.pieces[piece], x, y, types))
                    {
                        if (solve(piece + 1, placePiece(types, this.pieces[piece]))) return true;
                        else this.board.pieces.RemoveAt(piece);
                    }
                }
            }

            this.pieces[piece].rotate(1);

            if (piece < 2)
            {
                this.progress[piece * 11] = '5';
                this.progress[piece * 11 + 4] = (char)(this.board.height - this.pieces[piece].height + 48);
                this.progress[piece * 11 + 8] = (char)(this.board.width - this.pieces[piece].width + 48);
            }

            for (int y = 0; y < this.board.height - this.pieces[piece].height + 1; y++)
            {
                for (int x = 0; x < this.board.width - this.pieces[piece].width + 1; x++)
                {
                    if (piece < 2)
                    {
                        this.progress[piece * 11 + 2] = (char)(y + 48);
                        this.progress[piece * 11 + 6] = (char)(x + 48);
                    }

                    if (this.board.place(this.pieces[piece], x, y, types))
                    {
                        if (solve(piece + 1, placePiece(types, this.pieces[piece]))) return true;
                        else this.board.pieces.RemoveAt(piece);
                    }
                }
            }

            this.pieces[piece].rotate(1);

            if (piece < 2)
            {
                this.progress[piece * 11] = '6';
                this.progress[piece * 11 + 4] = (char)(this.board.height - this.pieces[piece].height + 48);
                this.progress[piece * 11 + 8] = (char)(this.board.width - this.pieces[piece].width + 48);
            }

            for (int y = 0; y < this.board.height - this.pieces[piece].height + 1; y++)
            {
                for (int x = 0; x < this.board.width - this.pieces[piece].width + 1; x++)
                {
                    if (piece < 2)
                    {
                        this.progress[piece * 11 + 2] = (char)(y + 48);
                        this.progress[piece * 11 + 6] = (char)(x + 48);
                    }

                    if (this.board.place(this.pieces[piece], x, y, types))
                    {
                        if (solve(piece + 1, placePiece(types, this.pieces[piece]))) return true;
                        else this.board.pieces.RemoveAt(piece);
                    }
                }
            }

            this.pieces[piece].rotate(1);

            if (piece < 2)
            {
                this.progress[piece * 11] = '7';
                this.progress[piece * 11 + 4] = (char)(this.board.height - this.pieces[piece].height + 48);
                this.progress[piece * 11 + 8] = (char)(this.board.width - this.pieces[piece].width + 48);
            }

            for (int y = 0; y < this.board.height - this.pieces[piece].height + 1; y++)
            {
                for (int x = 0; x < this.board.width - this.pieces[piece].width + 1; x++)
                {
                    if (piece < 2)
                    {
                        this.progress[piece * 11 + 2] = (char)(y + 48);
                        this.progress[piece * 11 + 6] = (char)(x + 48);
                    }

                    if (this.board.place(this.pieces[piece], x, y, types))
                    {
                        if (solve(piece + 1, placePiece(types, this.pieces[piece]))) return true;
                        else this.board.pieces.RemoveAt(piece);
                    }
                }
            }

            this.pieces[piece].rotate(1);
            return false;
        }

        public bool solve1(int piece, List<List<int>> types)
        {
            if (piece == 9) return true;

            /*
            Console.WriteLine();
            board.print();
            Console.WriteLine(piece);
            */

            Console.WriteLine(this.progress);

            this.progress[0] = '0';
            this.progress[4] = (char)(this.board.height - this.pieces[piece].height + 48);
            this.progress[8] = (char)(this.board.width - this.pieces[piece].width + 48);
            /*
            for (int y = 0; y < this.board.height - this.pieces[piece].height + 1; y++)
            {
                for (int x = 0; x < this.board.width - this.pieces[piece].width + 1; x++)
                {
                    if (this.board.place(this.pieces[piece], x, y, types))
                    {
                        if (solve(piece + 1, placePiece(types, this.pieces[piece]))) return true;
                        else this.board.pieces.RemoveAt(piece);
                    }
                }
            }
            */

            this.pieces[piece].rotate(1);

            this.progress[0] = '1';
            this.progress[4] = (char)(this.board.height - this.pieces[piece].height + 48);
            this.progress[8] = (char)(this.board.width - this.pieces[piece].width + 48);

            for (int y = 0; y < this.board.height - this.pieces[piece].height + 1; y++)
            {
                for (int x = 0; x < this.board.width - this.pieces[piece].width + 1; x++)
                {
                    this.progress[2] = (char)(y + 48);
                    this.progress[6] = (char)(x + 48);

                    if (this.board.place(this.pieces[piece], x, y, types))
                    {
                        if (solve(piece + 1, placePiece(types, this.pieces[piece]))) return true;
                        else this.board.pieces.RemoveAt(piece);
                    }
                }
            }

            this.pieces[piece].rotate(3);
            this.pieces[piece].mirror();

            this.progress[0] = '2';
            this.progress[4] = (char)(this.board.height - this.pieces[piece].height + 48);
            this.progress[8] = (char)(this.board.width - this.pieces[piece].width + 48);

            for (int y = 0; y < this.board.height - this.pieces[piece].height + 1; y++)
            {
                for (int x = 0; x < this.board.width - this.pieces[piece].width + 1; x++)
                {
                    this.progress[2] = (char)(y + 48);
                    this.progress[6] = (char)(x + 48);

                    if (this.board.place(this.pieces[piece], x, y, types))
                    {
                        if (solve(piece + 1, placePiece(types, this.pieces[piece]))) return true;
                        else this.board.pieces.RemoveAt(piece);
                    }
                }
            }

            this.pieces[piece].rotate(1);

            this.progress[0] = '3';
            this.progress[4] = (char)(this.board.height - this.pieces[piece].height + 48);
            this.progress[8] = (char)(this.board.width - this.pieces[piece].width + 48);

            for (int y = 0; y < this.board.height - this.pieces[piece].height + 1; y++)
            {
                for (int x = 0; x < this.board.width - this.pieces[piece].width + 1; x++)
                {
                    this.progress[2] = (char)(y + 48);
                    this.progress[6] = (char)(x + 48);

                    if (this.board.place(this.pieces[piece], x, y, types))
                    {
                        if (solve(piece + 1, placePiece(types, this.pieces[piece]))) return true;
                        else this.board.pieces.RemoveAt(piece);
                    }
                }
            }

            this.pieces[piece].rotate(1);

            this.progress[0] = '4';
            this.progress[4] = (char)(this.board.height - this.pieces[piece].height + 48);
            this.progress[8] = (char)(this.board.width - this.pieces[piece].width + 48);

            for (int y = 0; y < this.board.height - this.pieces[piece].height + 1; y++)
            {
                for (int x = 0; x < this.board.width - this.pieces[piece].width + 1; x++)
                {
                    this.progress[2] = (char)(y + 48);
                    this.progress[6] = (char)(x + 48);

                    if (this.board.place(this.pieces[piece], x, y, types))
                    {
                        if (solve(piece + 1, placePiece(types, this.pieces[piece]))) return true;
                        else this.board.pieces.RemoveAt(piece);
                    }
                }
            }

            this.pieces[piece].rotate(1);

            this.progress[0] = '5';
            this.progress[4] = (char)(this.board.height - this.pieces[piece].height + 48);
            this.progress[8] = (char)(this.board.width - this.pieces[piece].width + 48);

            for (int y = 0; y < this.board.height - this.pieces[piece].height + 1; y++)
            {
                for (int x = 0; x < this.board.width - this.pieces[piece].width + 1; x++)
                {
                    this.progress[2] = (char)(y + 48);
                    this.progress[6] = (char)(x + 48);

                    if (this.board.place(this.pieces[piece], x, y, types))
                    {
                        if (solve(piece + 1, placePiece(types, this.pieces[piece]))) return true;
                        else this.board.pieces.RemoveAt(piece);
                    }
                }
            }

            this.pieces[piece].rotate(3);
            this.pieces[piece].mirror();

            this.progress[0] = '6';
            this.progress[4] = (char)(this.board.height - this.pieces[piece].height + 48);
            this.progress[8] = (char)(this.board.width - this.pieces[piece].width + 48);

            for (int y = 0; y < this.board.height - this.pieces[piece].height + 1; y++)
            {
                for (int x = 0; x < this.board.width - this.pieces[piece].width + 1; x++)
                {
                    this.progress[2] = (char)(y + 48);
                    this.progress[6] = (char)(x + 48);

                    if (this.board.place(this.pieces[piece], x, y, types))
                    {
                        if (solve(piece + 1, placePiece(types, this.pieces[piece]))) return true;
                        else this.board.pieces.RemoveAt(piece);
                    }
                }
            }

            this.pieces[piece].rotate(1);

            this.progress[0] = '7';
            this.progress[4] = (char)(this.board.height - this.pieces[piece].height + 48);
            this.progress[8] = (char)(this.board.width - this.pieces[piece].width + 48);

            for (int y = 0; y < this.board.height - this.pieces[piece].height + 1; y++)
            {
                for (int x = 0; x < this.board.width - this.pieces[piece].width + 1; x++)
                {
                    this.progress[2] = (char)(y + 48);
                    this.progress[6] = (char)(x + 48);

                    if (this.board.place(this.pieces[piece], x, y, types))
                    {
                        if (solve(piece + 1, placePiece(types, this.pieces[piece]))) return true;
                        else this.board.pieces.RemoveAt(piece);
                    }
                }
            }

            this.pieces[piece].rotate(1);
            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace PuzzleSolver
{
    internal class Piece
    {
        public int id;
        public int x;
        public int y;
        public int width;
        public int height;
        public int rotation;
        public int mirrored;
        public List<Tile> tiles;

        // constructor

        public Piece(int Width, int Height, List<int> Tiles, List<int> Rotations, int Id)
        {
            this.width = Width;
            this.height = Height;
            this.id = Id;
            this.rotation = 0;

            populateShape(Tiles, Rotations);
        }

        // public methods

        public void print()
        {
            for (int i = 0; i < this.height * 5; i++)
            {
                this.print(i);
                Console.WriteLine();
            }
        }

        public void print(int line)
        {
            for (int x = 0; x < this.width; x++)
                this.tiles[line / 5 * this.width + x].print(line % 5);
        }

        public bool print(int line, int tile)
        {
            if (this.tiles[line / 5 * this.width + tile].id == 0) return false;

            this.tiles[line / 5 * this.width + tile].print(line % 5);
            return true;
        }

        public void rotate(int r)
        {
            this.rotation += r;

            for (int i = 0; i < r; i++)
            {
                List<Tile> newTiles = new List<Tile>();

                for (int x = 0; x < this.width; x++)
                    for (int y = this.height - 1; y >= 0; y--)
                        newTiles.Add(this.tiles[y * this.width + x]);

                this.tiles = newTiles;

                int temp = this.height;
                this.height = this.width;
                this.width = temp; 
            }

            for (int i = 0; i < this.width * this.height; i++)
            {
                this.tiles[i].rotate(r);
                if (this.tiles[i].type >= 2)
                    this.tiles[i].type = (this.tiles[i].type - 2) / 4 * 4 + 2 + this.tiles[i].rotation;
            }
        }

        public void mirror()
        {
            this.mirrored = this.mirrored == 0 ? 1 : 0;

            List<Tile> newTiles = new List<Tile>();

            for (int y = this.height - 1; y >= 0; y--)
                for (int x = 0; x < this.width; x++)
                    newTiles.Add(this.tiles[y * this.width + x]);

            this.tiles = newTiles;

            for (int i = 0; i < this.width * this.height; i++)
            {
                this.tiles[i].mirror();
                if (this.tiles[i].type >= 2)
                    this.tiles[i].type = (this.tiles[i].type - 2) / 4 * 4 + 2 + this.tiles[i].rotation;
            }

        }

        // private methods

        private void populateShape(List<int> Tiles, List<int> Rotations)
        {
            this.tiles = new List<Tile>();

            for (int i = 0; i < this.width * this.height; i++)
                this.tiles.Add(new Tile(Tiles[i], Rotations[i]));
        }
    }
}


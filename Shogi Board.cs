using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect_four
{
    internal class Shogi_Board
    {
        /*private Texture2D _pawn; //num 1,2,  promoted 17,18
        private Texture2D _lance; // num 3,4 promoted 19,20 
        private Texture2D _knight;// num 5,6 promoted 21,22
        private Texture2D _silverGeneral; // num 7,8 promoted 23,24
        private Texture2D _goldGeneral;// num 9, 10
        private Texture2D _bishop;// num 11,12 promoted 25,26
        private Texture2D _rook;//num 13,14 promoted 27,28
        private Texture2D _king;// num 15,16*/

        private int[,] _boardPositions;
        private ShogiPiece[,] _pieces;
        private List<ShogiPiece> _player1Bench;
        private List<ShogiPiece> _player2Bench;

        public Shogi_Board(List<Texture2D> textures)
        {
            //Benches
            _player1Bench = new List<ShogiPiece>();
            _player2Bench= new List<ShogiPiece>();
            //Empty Spaces
            _boardPositions = new int[9,9];
            for (int i = 0; i < 9; i++)
                for (int j = 0; j< 9; j++)
                    _boardPositions[i,j] = 0;
            //Seting up board
            _pieces = new ShogiPiece[9, 9];
            for (int i = 0; i < 9; i++)//Pawn
                _pieces[i, 2] = new ShogiPiece(textures[1], textures[2], textures[17], textures[18], 1, 1);

            for (int i = 0; i < 9; i++)
                _pieces[i, 6] = new ShogiPiece(textures[1], textures[2], textures[17], textures[18],1,2);//Pawn
            _pieces[0, 0] = new ShogiPiece(textures[3], textures[4], textures[19], textures[20], 2, 1);//Lance
            _pieces[8, 0] = new ShogiPiece(textures[3], textures[4], textures[19], textures[20], 2, 1);
            _pieces[0, 8] = new ShogiPiece(textures[3], textures[4], textures[19], textures[20], 2, 2);
            _pieces[8, 8] = new ShogiPiece(textures[3], textures[4], textures[19], textures[20], 2, 2);//Lance
            _pieces[1, 0] = new ShogiPiece(textures[5], textures[6], textures[21], textures[22], 3, 1);//Knight
            _pieces[7, 0] = new ShogiPiece(textures[5], textures[6], textures[21], textures[22], 3, 1);
            _pieces[1, 8] = new ShogiPiece(textures[5], textures[6], textures[21], textures[22], 3, 2);
            _pieces[7, 8] = new ShogiPiece(textures[5], textures[6], textures[21], textures[22], 3, 2);//Knight
            _pieces[2, 0] = new ShogiPiece(textures[7], textures[8], textures[23], textures[24], 4, 1);//Silver
            _pieces[6, 0] = new ShogiPiece(textures[7], textures[8], textures[23], textures[24], 4, 1);
            _pieces[2, 8] = new ShogiPiece(textures[7], textures[8], textures[23], textures[24], 4, 2);
            _pieces[6, 8] = new ShogiPiece(textures[7], textures[8], textures[23], textures[24], 4, 2);//Silver
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 9; ++i)
            {
                for (int j = 0; j < 9; ++j)
                {
                    if (_boardPositions[i,j] == 1){

                    }
                }
            }
        }
        
    }
}

using Microsoft.VisualBasic.FileIO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect_four
{
    internal class ShogiBench
    {
        private Texture2D _pawnTex;
        private Texture2D _lanceTex;
        private Texture2D _knightTex;
        private Texture2D _silverTex;
        private Texture2D _goldTex;
        private Texture2D _bishopTex;
        private Texture2D _rookTex;
        private SpriteFont _font;
        private ShogiPiece[] _piecesToDrop;
        private int _startPositionX;
        private int _startPositionY;
        private int[] _storedPieces;
        private int _player;

        public ShogiBench(List<Texture2D>textures, SpriteFont font, int startPositionX, int startPositionY, int player)
        {
            _pawnTex = textures[1];
            _lanceTex = textures[3];
            _knightTex = textures[5];
            _silverTex = textures[7];
            _goldTex = textures[9];
            _bishopTex = textures[11];
            _rookTex = textures[13];
            _font = font;
            _storedPieces = new int[7];
            for (int i = 0; i<7; i++)
            {
                _storedPieces[i] = 0;
            }
            _startPositionX = startPositionX;
            _startPositionY = startPositionY;
            _player = player;
            _piecesToDrop = new ShogiPiece[7];
            
        }
        public void ClonePiece(ShogiPiece pawn, ShogiPiece lance, ShogiPiece knight, ShogiPiece silver, ShogiPiece gold, ShogiPiece bishop, ShogiPiece rook)
        {
            _piecesToDrop[0] = pawn;
            _piecesToDrop[1] = lance;
            _piecesToDrop[2] = knight;
            _piecesToDrop[3] = silver;
            _piecesToDrop[4] = gold;
            _piecesToDrop[5] = bishop;
            _piecesToDrop[6] = rook;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_pawnTex, new Rectangle(_startPositionX, _startPositionY, 40, 40), Color.White);
            spriteBatch.DrawString(_font, $"x{_storedPieces[0]}", new Vector2(_startPositionX + 40, _startPositionY + 15), Color.White);
            spriteBatch.Draw(_lanceTex, new Rectangle(_startPositionX, _startPositionY+50, 40, 40), Color.White);
            spriteBatch.DrawString(_font, $"x{_storedPieces[1]}", new Vector2(_startPositionX + 40, _startPositionY + 65), Color.White);
            spriteBatch.Draw(_knightTex, new Rectangle(_startPositionX, _startPositionY + 100, 40, 40), Color.White);
            spriteBatch.DrawString(_font, $"x{_storedPieces[2]}", new Vector2(_startPositionX + 40, _startPositionY + (115)), Color.White);
            spriteBatch.Draw(_silverTex, new Rectangle(_startPositionX, _startPositionY + 150, 40, 40), Color.White);
            spriteBatch.DrawString(_font, $"x{_storedPieces[3]}", new Vector2(_startPositionX + 40, _startPositionY + (165)), Color.White);
            spriteBatch.Draw(_goldTex, new Rectangle(_startPositionX, _startPositionY + 200, 40, 40), Color.White);
            spriteBatch.DrawString(_font, $"x{_storedPieces[4]}", new Vector2(_startPositionX + 40, _startPositionY + (215)), Color.White);
            spriteBatch.Draw(_bishopTex, new Rectangle(_startPositionX, _startPositionY + 250, 40, 40), Color.White);
            spriteBatch.DrawString(_font, $"x{_storedPieces[5]}", new Vector2(_startPositionX + 40, _startPositionY + (265)), Color.White);
            spriteBatch.Draw(_rookTex, new Rectangle(_startPositionX, _startPositionY + 300, 40, 40), Color.White);
            spriteBatch.DrawString(_font, $"x{_storedPieces[6]}", new Vector2(_startPositionX + 40, _startPositionY + (315)), Color.White);

        }
        public void AddPiece(int pieceType)
        {
            pieceType -= 1;
            _storedPieces[pieceType] += 1;
        }
        public ShogiPiece PieceToDrop(int pieceType)
        {
            return _piecesToDrop[(pieceType - 1)];
        }
        public int IsBenchClicked(MouseState mouseState)
        {
            int temp = -1;
            for (int i = 1; i < 8; i++)
            {
                if (new Rectangle(_startPositionX, (_startPositionY + (50 * (i - 1))), 40, 40).Contains(mouseState.X, mouseState.Y))
                    temp = i;
            }
            if (temp != -1)
                if (_storedPieces[temp - 1] <= 0)
                    temp = -1;
            return temp;
        }
    }
}

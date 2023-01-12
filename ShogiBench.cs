using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private List<ShogiPiece> _shogiPieces;
        private SpriteFont _font;

        public ShogiBench(Texture2D pawn, Texture2D lance, Texture2D knight, Texture2D silver, Texture2D gold, Texture2D bishop, Texture2D rook, SpriteFont font)
        {
            _pawnTex = pawn;
            _lanceTex = lance;
            _knightTex = knight;
            _silverTex = silver;
            _goldTex = gold;
            _bishopTex = bishop;
            _rookTex = rook;
            _font = font;
            _shogiPieces= new List<ShogiPiece>();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_pawnTex, new Rectangle(0, 10, 30, 30), Color.White);
        }
    }
}

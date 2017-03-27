using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.Interfaces
{
    public interface IRectangle
    {
        Vector2 Position { get; set; }
        Vector2 Size { get; set; }

        //float X { get { return Position.X; } set { Position = new Vector2(value, Position.Y); } }
        //float Y { get { return Position.Y; } set { Position = new Vector2(Position.X, value); } }

    }
}

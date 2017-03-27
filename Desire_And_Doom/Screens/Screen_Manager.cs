using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.Screens
{
    class Screen_Manager
    {
        Dictionary<string, Screen> screens;
        Screen active;

        public Screen_Manager()
        {
            screens = new Dictionary<string, Screen>();
        }

        public void Register(Screen _screen, bool _goto = false) {
            this.screens.Add(_screen.ID, _screen);

            if (_goto)
                Goto_Screen(_screen.ID);
        }

        public void Goto_Screen(string _id)
        {
            active?.Destroy();
            active = screens[_id];
            active?.Load();
        }

        public void Update(GameTime time)
        {
            active?.Update(time);
        }

        public void Draw(SpriteBatch batch)
        {
            active?.Draw(batch);
        }
    }
}

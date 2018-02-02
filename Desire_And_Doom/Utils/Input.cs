using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom
{
    class Key_State
    {
        public bool current = false, last = false;
        public Keys key = Keys.None;
        public bool reset = false;
    }

    class Button_State { 
        public bool current = false, last = false;
        public Buttons key = 0;
        public bool reset = false;
    }

    class Input
    {
        private static Input it;
        private Dictionary<Keys, Key_State> states;
        private Dictionary<Buttons, Button_State> button_states;
        private ButtonState left_current = ButtonState.Released, left_last = ButtonState.Released;

        private Vector2 mouse_drag = Vector2.Zero;
        private Vector2 start_mpos = Vector2.Zero;
        private Vector2 last_mpos = Vector2.Zero;

        private Input() {
            states = new Dictionary<Keys, Key_State>();
            button_states = new Dictionary<Buttons, Button_State>();
        }

        public void Update(GameTime time)
        {
            foreach(Key_State key in states.Values)
            {
                if (key.reset)
                    key.last = key.current;
            }

            foreach (var button in button_states.Values)
                if (button.reset)
                    button.last = button.current;

            left_last = left_current;
        }

        public Vector2 Mouse_Drag() { return mouse_drag; }

        public Vector2 Mouse_Position() {
            return Mouse.GetState().Position.ToVector2();
        }

        public bool Mouse_Left() {
            return Mouse.GetState().LeftButton == ButtonState.Pressed;
        }

        public bool Mouse_Right() {
            return Mouse.GetState().RightButton == ButtonState.Pressed;
        }

        public bool Mouse_Left_Pressed()
        {
            bool clicked = false;
            left_current = Mouse.GetState().LeftButton;
            if (left_current == ButtonState.Pressed && left_last == ButtonState.Released){
                clicked = true;
            }
            left_last = left_current;
            return clicked;
        }

        // Game pads
        public bool Is_Gamepad_Button_Down(Buttons button) =>
            (GamePad.GetState(PlayerIndex.One).IsButtonDown(button));

        public bool Is_Gamepad_Button_Up(Buttons button) =>
            !Is_Gamepad_Button_Down(button);

        public bool Is_Gamepad_Button_Pressed(Buttons key)
        {
            if (button_states.ContainsKey(key) == false)
            {
                var key_state = new Button_State() {
                    key = key, current = Is_Gamepad_Button_Down(key)
                };
                button_states.Add(key, key_state);
                return false;
            }else
            {
                var key_state = button_states[key];
                key_state.current = Is_Gamepad_Button_Down(key);
                if (key_state.current && !key_state.last)
                {
                    button_states[key].reset = true;
                    return true;
                }else {
                    return false;
                }
            }
        }

        public bool Is_Key_Down(Keys key) {
            return (Keyboard.GetState().IsKeyDown(key));
        }

        public bool Is_Key_Up(Keys key) {
            return (Keyboard.GetState().IsKeyUp(key));
        }

        public bool Is_Key_Pressed(Keys key)
        {
            if (states.ContainsKey(key) == false)
            {
                var key_state = new Key_State() {
                    key = key, current = Is_Key_Down(key)
                };
                states.Add(key, key_state);
                return false;
            }else
            {
                var key_state = states[key];
                key_state.current = Is_Key_Down(key);
                if (key_state.current && !key_state.last)
                {
                    states[key].reset = true;
                    return true;
                }else {
                    return false;
                }
            }
        }

        public static Input It {
            get {
                if (it == null) it = new Input();
                return it;
            }
        }
    }
}

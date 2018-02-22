using System;
using NLua;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.Graphics
{
    class Dialog_Option {
        public string Value { get; set; }
        public int NextDialogText { get; set; } = 0;
    }

    class Dialog_Text {
        /*
         * String
         * List of options (if any)
         * Link to next dialog text
         */
        public string Value { get; set; } = "";
        public int NextDialogText { get; set; } = 0;
        public List<Dialog_Option> options = new List<Dialog_Option>();
    }

    class Dialog
    {
        public Dictionary<int, Dialog_Text> Dialog_Texts = new Dictionary<int, Dialog_Text>();
        public int CurrentDialogText {get; set;} = 0;

        public Dialog() {}
        public Dialog(LuaTable table) {
            foreach (var keyPair in table)
            {
                Console.WriteLine(keyPair);
            }
        }
    }
}

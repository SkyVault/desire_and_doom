using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 
{
    item: 100

    another: "Hello World"

    list: {
        mylistitem: false

    }

}
     
*/

namespace Desire_And_Doom.Utils
{
    class OKore
    {

        public object this[string key]{
            get => elements[key];
        }

        public void Add(string key, object obj)
        {
            elements.Add(key, obj);
        }

        public object Get(string key)
        {
            return elements[key];
        }

        private Dictionary<string, object> elements = new Dictionary<string, object>();
    }

    class OKore_Parser
    {
        public OKore_Parser()
        {
            Parse_String(@"
                {
                    item: 100
                    another: 'Hello Jello'
                    list: {
                        my_list_item: false
                    }
                }
            ");
        }

        private List<string> tokenize_string(string code)
        {
            var tokens = new List<string>();

            string delim = " \n\t\r`~!@#$%^&*()+-={}[]\\|:\";\',.<>/?";
            string bad = " \n\t\r";
            string word = "";
            bool in_string = false;

            foreach (var c in code)
            {
                if (c == '\'')
                {
                    if (in_string)
                    {
                        in_string = false;
                        word += c;
                        continue;
                    }
                    else
                        in_string = true;
                }

                if (in_string)
                {
                    word += c;
                    continue;
                }

                if (delim.Contains(c))
                {
                    if (word.Length > 0) tokens.Add(word);
                    if (bad.Contains(c) == false) tokens.Add(new string(c, 1));
                    word = "";
                }
                else
                {
                    if (bad.Contains(c) == false) word += c;
                }
            }
            if (word.Length > 0) tokens.Add(word);

            return tokens;
        }

        public void Parse_File(string file_path)
        {

        }

        public OKore Parse_String(string code)
        {
            var tokens = tokenize_string(code);

            var obj = Compile_Tokens(tokens);

            return obj;
        }

        private List<string> Get_Token_Block(List<string> tokens)
        {
            List<string> new_tokens = new List<string>();

            return new_tokens;
        }

        private OKore Compile_Tokens(List<string> tokens)
        {
            OKore obj = new OKore();
            int i = 0;
            while(i < tokens.Count)
            {
                var token = tokens[i];
                if (token == ":")
                {
                    if (i < 1) throw new Exception("OKORE:: Syntax Error!");

                    var name = tokens[i - 1];
                    var value = tokens[i + 1];

                    if (value == "{")
                    {
                        i++;

                    }
                    else
                    {
                        bool res = double.TryParse(value, out double result);
                        if (res)
                        {
                            obj.Add(name, result);
                        }
                        else
                        {
                            if (value.First() == '\'' && value.Last() == '\'')
                            {
                                obj.Add(name, value.Substring(1, value.Length - 2));
                            }
                            else
                            {
                                obj.Add(name, value);
                            }
                        }
                        i += 2;
                    }
                }
            }
            return obj;
        }

    }
}

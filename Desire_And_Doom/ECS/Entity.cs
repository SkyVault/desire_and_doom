using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Desire_And_Doom.ECS.Component;

namespace Desire_And_Doom.ECS
{
    class Entity
    {

        //private Dictionary<Component.Types, Component> components;
        private Component[] components;
        private static int uuid_generater = 0;

        public List<string> Tags { get; set; }

        public int UUID { get; private set; } = uuid_generater++;
        public bool Remove { get; private set; }

        public Entity()
        {
            components = new Component[(int)Component.Types.Num_Of_Types];
            Tags = new List<string>();
        }

        public void Destroy() {
            Remove = true;
        }

        public bool Has(Types name) {
            return components[(int)name] != null;
        }

        public List<Component> Get_Components_List() {
            return components.ToList();
        }

        public List<Types> Get_Component_Types_List() {
            List<Types> types = new List<Types>();
            foreach (var c in components)
                if (c != null)
                    types.Add(c.Type);
            return types;
        }

        public Component Add(Component component){
            if (components[(int)component.Type] == null){
                components[(int)component.Type] = component;
            }
            return component;
        }

        public Component Get(Component.Types id)
        {
            return (components[(int)id]);
        }
    }
}

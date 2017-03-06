using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.ECS
{
    class Entity
    {

        private Dictionary<Type, Component> components;
        private static int uuid_generater = 0;

        public int UUID { get; private set; } = uuid_generater++;
        public bool Remove { get; private set; }

        public Entity()
        {
            components = new Dictionary<Type, Component>();
        }

        public void Destroy() {
            Remove = true;
        }

        public bool Has<T>() {
            return components.ContainsKey(typeof(T));
        }

        public List<Component> Get_Components_List() {
            return components.Values.ToList();
        }

        public List<Type> Get_Component_Types_List() {
            return components.Keys.ToList();
        }

        public T Add<T> (T component){
            if (components.ContainsKey(typeof(T)) == false){
                components.Add(typeof(T), component as Component);
            }
            return component;
        }

        public Component Get<T>()
        {
            if (components.ContainsKey(typeof(T)) == false)
                return null;
            return (components[typeof(T)]);
        }
    }
}

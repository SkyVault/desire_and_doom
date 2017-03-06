using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.ECS
{
    class World
    {
        private List<Entity> entities;
        private Dictionary<Type, System> systems;

        public World()
        {
            entities = new List<Entity>();
            systems = new Dictionary<Type, System>();
        }

        public Entity Create_Entity()
        {
            var entity = new Entity();
            entities.Add(entity);
            return entity;
        }

        public void Add_System<T>(System system) {
            systems.Add(typeof(T), system);
        }

        public List<Entity> Get_All_With_Component<T>()
        {
            List<Entity> list = new List<Entity>();

            foreach(var e in entities)
                if (e.Has<T>()) list.Add(e);
        
            return list;
        }

        public void Update(GameTime time)
        {
            foreach(var entity in entities) {
                if (entity.Remove) {
                    entities.Remove(entity);
                    continue;
                }

                foreach (var system in systems.Values)
                    if (system.Has_All_Types(entity))
                    {
                        system.Update(time, entity);
                    }
            }
        }

        public void Draw(SpriteBatch batch)
        {
            foreach (var entity in entities) {
                foreach (var system in systems.Values)
                    if (system.Has_All_Types(entity))
                        system.Draw(batch, entity);
            }
        }
        
    }
}

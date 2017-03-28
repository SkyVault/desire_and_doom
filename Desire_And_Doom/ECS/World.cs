using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using NLua;
using Penumbra;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.ECS
{
    class World
    {
        private List<Entity> entities;
        private Dictionary<Type, System> systems;
        private PenumbraComponent lighting;

        public World(PenumbraComponent lighting)
        {
            entities = new List<Entity>();
            systems = new Dictionary<Type, System>();
            this.lighting = lighting;
        }

        public Entity Find_With_Tag(string tag)
        {
            foreach(var e in entities)
                if (e.Tags.Contains(tag)) return e;
            return null;
        }

        public Entity Create_Entity(LuaTable table, float x = 0, float y = 0)
        {
            var entity = Create_Entity();
            var components = table["components"] as LuaTable;
            Debug.Assert(components != null);

            if (table["tags"] is LuaTable tags)
                foreach (var t in tags.Values)
                    entity.Tags.Add(t as string);

            foreach (var key in components.Keys)
            {
                var component = components[key] as LuaTable;
                switch (key)
                {
                    case "Sprite": {
                            string image = component[1] as string;
                            int qx = (int)(component[2] as double?);
                            int qy = (int)(component[3] as double?);
                            int qw = (int)(component[4] as double?);
                            int qh = (int)(component[5] as double?);
                            entity.Add(new Sprite(
                                Assets.It.Get<Texture2D>(image),
                                new Rectangle(qx, qy, qw, qh)));
                            break;
                        }
                    case "Body": {
                            float w = (float)(component[1] as double?);
                            float h = (float)(component[2] as double?);
                            entity.Add(new Body(
                                new Vector2(x, y), new Vector2(w, h)
                                ));
                            break;
                        }
                    case "Physics":
                        entity.Add(new Physics(Vector2.Zero));
                        break;
                    case "Animation": {
                            string image = component[1] as string;
                            
                            var anim = new List<string>();
                            for (int i = 2; i < component.Values.Count + 1; i++)
                            {
                                var t = component[i] as LuaTable;
                                anim.Add(t[1] as string);
                            }

                            entity.Add(new Animated_Sprite(
                                Assets.It.Get<Texture2D>(image), anim
                                ));
                            break;
                        }
                    case "Player":
                        entity.Add(new Player());
                        break;
                    case "Invatory": {
                            float w = (float)(component[1] as double?);
                            float h = (float)(component[2] as double?);
                            entity.Add(new Invatory((int)w, (int)h));
                            break;
                        }
                    case "Item": entity.Add(new Item());  break;
                    case "Light":
                        entity.Add(new Light_Emitter(lighting));
                        break;
                    case "Ai":
                        var type = component[1] as string;
                        if (type == "Table")
                        {
                            var ai = Assets.It.Get<LuaTable>(component[2] as string);
                            entity.Add(new AI(ai));
                        }else
                        {

                        }
                        break;
                    default:
                        Console.WriteLine("Unknown Component: " + key);
                        break;
                }
            }

            return entity;
        }

        public Entity Create_Entity()
        {
            var entity = new Entity();
            entities.Add(entity);
            return entity;
        }



        public System Add_System<T>(System system) {
            systems.Add(typeof(T), system);
            system.World_Ref = this;
            return system;
        }

        public System Get_System<T>()
        {
            return systems[typeof(T)];
        }

        public List<Entity> Get_All_With_Component(Component.Types T)
        {
            List<Entity> list = new List<Entity>();

            foreach(var e in entities)
                if (e.Has(T)) list.Add(e);
        
            return list;
        }

        bool load = true;
        public int timing = 0;
        public void Update(GameTime time)
        {
            timing = DateTime.Now.Millisecond;
            for(int i = entities.Count - 1; i >= 0; i--) {
                var entity = entities[i];
                if (entity.Remove) {
                    entities.Remove(entity);
                    continue;
                }

                foreach (var system in systems.Values)
                    if (system.Has_All_Types(entity))
                    {
                        if (load)
                            system.Load(entity);
                        system.Update(time, entity);
                    }
            }
            if (load) load = false;
        }

        public void Draw(SpriteBatch batch)
        {
            foreach (var entity in entities) {
                foreach (var system in systems.Values)
                    if (system.Has_All_Types(entity))
                        system.Draw(batch, entity);
            }
        }

        public void UIDraw(SpriteBatch batch, Camera_2D camera)
        {
            foreach (var entity in entities)
            {
                foreach (var system in systems.Values)
                    if (system.Has_All_Types(entity))
                        system.UIDraw(batch, camera, entity);
            }

            //if (DateTime.Now.Millisecond % 100 == 0)
            //    Console.WriteLine("systems: {0}", DateTime.Now.Millisecond - timing);
            timing = 0;
        }

        public void Destroy_All()
        {
            entities.Clear();
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Desire_And_Doom.Graphics
{
    class Renderer3D
    {
        VertexPosition []vertices;
        GraphicsDevice device;
        ContentManager content;
        BasicEffect effect;

        Model ico;

        public Renderer3D(GraphicsDevice device, ContentManager content)
        {
            this.device = device;
            this.content= content;

            vertices = new VertexPosition[6];
            int i = 0;
            vertices[i++].Position = new Vector3(-20, -20, 0);
            vertices[i++].Position = new Vector3(-20,  20, 0);
            vertices[i++].Position = new Vector3( 20, -20, 0);
            vertices[i++].Position = vertices[1].Position;
            vertices[i++].Position = new Vector3( 20, 20, 0);
            vertices[i++].Position = vertices[2].Position;

            effect = new BasicEffect(device);

            ico = content.Load<Model>("ico");
        }

        public void Draw(){
            
            foreach(var mesh in ico.Meshes){
                foreach(BasicEffect effect in mesh.Effects){
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;

                    effect.World = Matrix.Identity;

                    var c_pos = new Vector3(0, 8, 0);
                    var look = Vector3.Zero;
                    var up = Vector3.UnitZ;

                    effect.View = Matrix.CreateLookAt(c_pos, look, up);

                    float aspect = Game1.WIDTH / Game1.HEIGHT;
                    float fov = (float)Math.PI / 4; // 45 deg

                    float near = 0.001f;
                    float far = 1000f;

                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                        fov, aspect, near, far
                    );
                }

                mesh.Draw();
            }

        }
    }
}

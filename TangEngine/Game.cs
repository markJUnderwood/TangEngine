using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.ES10;

namespace TangEngine
{
    public class Game:GameWindow
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Title = "Tang Game";
            GL.ClearColor(0.3921568627450980392156862745098f, 0.58431372549019607843137254901961f, 0.92941176470588235294117647058824f,1f);
        }
    }
}

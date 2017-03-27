return {
  version = "1.1",
  luaversion = "5.1",
  tiledversion = "0.18.1",
  orientation = "orthogonal",
  renderorder = "right-down",
  width = 256,
  height = 256,
  tilewidth = 16,
  tileheight = 16,
  nextobjectid = 17,
  properties = {},
  tilesets = {
    {
      name = "tiles_main",
      firstgid = 1,
      tilewidth = 16,
      tileheight = 16,
      spacing = 0,
      margin = 0,
      image = "../../../../../../../../../../Visual Studio 2015/Projects/Desire_And_Doom/Desire_And_Doom/Content/tiles_main.png",
      imagewidth = 512,
      imageheight = 512,
      tileoffset = {
        x = 0,
        y = 0
      },
      properties = {},
      terrains = {
        {
          name = "grass",
          tile = -1,
          properties = {}
        },
        {
          name = "dirt",
          tile = -1,
          properties = {}
        }
      },
      tilecount = 1024,
      tiles = {
        {
          id = 32,
          terrain = { 0, 0, 0, 1 }
        },
        {
          id = 33,
          terrain = { 0, 0, 1, 1 }
        },
        {
          id = 34,
          terrain = { 0, 0, 1, 0 }
        },
        {
          id = 35,
          terrain = { 1, 1, 1, 0 }
        },
        {
          id = 36,
          terrain = { 1, 1, 0, 0 }
        },
        {
          id = 37,
          terrain = { 1, 1, 0, 1 }
        },
        {
          id = 64,
          terrain = { 0, 1, 0, 1 }
        },
        {
          id = 65,
          terrain = { 1, 1, 1, 1 }
        },
        {
          id = 66,
          terrain = { 1, 0, 1, 0 }
        },
        {
          id = 67,
          terrain = { 1, 0, 1, 0 }
        },
        {
          id = 68,
          terrain = { 0, 0, 0, 0 }
        },
        {
          id = 69,
          terrain = { 0, 1, 0, 1 }
        },
        {
          id = 96,
          terrain = { 0, 1, 0, 0 }
        },
        {
          id = 97,
          terrain = { 1, 1, 0, 0 }
        },
        {
          id = 98,
          terrain = { 1, 0, 0, 0 }
        },
        {
          id = 99,
          terrain = { 1, 0, 1, 1 }
        },
        {
          id = 100,
          terrain = { 0, 0, 1, 1 }
        },
        {
          id = 101,
          terrain = { 0, 1, 1, 1 }
        }
      }
    }
  },
  layers = {
    {
      type = "tilelayer",
      name = "Tile Layer 1",
      x = 0,
      y = 0,
      width = 256,
      height = 256,
      visible = true,
      opacity = 1,
      offsetx = 0,
      offsety = 0,
      properties = {},
      encoding = "base64",
      compression = "gzip",
      data = "H4sIAAAAAAAAC+3c603DMBQGUK/SQtmDR9kDUNl/BGIVCytKozoPJyGn0lHzz1alrzf2dXIOIZwBAAAAAAAAAAAAADbq0Dg2HlYwF6Cu98ZL43UFcwHqeg7X/L+tYC6wZzXvxeNYl8Z3UP9hCSmDx19zZzEfL40VPTa+VvB7wNblGcu/03Uuz+CcWTzcGC+O9TnDeLAnKV+X0J3pe43Nfj6PY+ieUxzjFOQephAzNybzU9ThW/VdrYf5jMl+qsPR2Jp/zxzs78N0SrKfam9yCtPU4ZI5yD+M1+6bzb2m75tHyb2H/h4MkzIfleT+FNaRffmH4dIZuaVzn7z3jO/+H6Z1T/5r7a8P3XNU/2GYj/DXM8/38HK1+mpDar/8w/YNrf3O98L2qf2wT2o/7JPsw36V3PdPda4YWIc19R+Bful5vOgSxr/PJ/Ufn4JaD2uUZ769Vp/q7F38H8jPH8g8LKP9np++/Tn9N9iG/FmfY8f1rRrfp13/u97Tk8vH815+qGfoGZuS8zclY3huB+qZOv9dPfi0jj+F7mcK8mtrfKgn7bUn7T33ktzrx8F2OXcH+1V67k5fDv6PvN/etUa3TgcAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAC46vosPScAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAoK4fRqXX1QAABAA="
    },
    {
      type = "tilelayer",
      name = "Tile Layer 2",
      x = 0,
      y = 0,
      width = 256,
      height = 256,
      visible = true,
      opacity = 1,
      offsetx = 0,
      offsety = 0,
      properties = {},
      encoding = "base64",
      compression = "gzip",
      data = "H4sIAAAAAAAAC+3BMQEAAADCoPVP7W0HoAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA3gAi6g7iAAAEAA=="
    },
    {
      type = "tilelayer",
      name = "forground",
      x = 0,
      y = 0,
      width = 256,
      height = 256,
      visible = true,
      opacity = 1,
      offsetx = 0,
      offsety = 0,
      properties = {
        ["layer"] = "1",
        ["sort"] = "true"
      },
      encoding = "base64",
      compression = "gzip",
      data = "H4sIAAAAAAAAC+3OMQEAAAgDIKsbxaYehpgHJKAKAADgdDoAxEw6AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAALyzckdQwAAABAA="
    },
    {
      type = "objectgroup",
      name = "props",
      visible = true,
      opacity = 1,
      offsetx = 0,
      offsety = 0,
      draworder = "topdown",
      properties = {
        ["props"] = true
      },
      objects = {
        {
          id = 12,
          name = "Tree",
          type = "Tree",
          shape = "rectangle",
          x = 160,
          y = 64,
          width = 16,
          height = 32,
          rotation = 0,
          visible = true,
          properties = {}
        },
        {
          id = 13,
          name = "Tree",
          type = "Tree",
          shape = "rectangle",
          x = 194,
          y = 17,
          width = 16,
          height = 32,
          rotation = 0,
          visible = true,
          properties = {}
        },
        {
          id = 14,
          name = "Tree",
          type = "Tree",
          shape = "rectangle",
          x = 208,
          y = 80,
          width = 16,
          height = 32,
          rotation = 0,
          visible = true,
          properties = {}
        },
        {
          id = 15,
          name = "Tree",
          type = "Tree",
          shape = "rectangle",
          x = 256,
          y = 32,
          width = 16,
          height = 32,
          rotation = 0,
          visible = true,
          properties = {}
        },
        {
          id = 16,
          name = "Tree",
          type = "Tree",
          shape = "rectangle",
          x = 128,
          y = 144,
          width = 16,
          height = 32,
          rotation = 0,
          visible = true,
          properties = {}
        }
      }
    }
  }
}

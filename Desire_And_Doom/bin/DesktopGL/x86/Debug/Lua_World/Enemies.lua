local AI = require "Lua_World/AI"

return {
	["Zombie"] = {
		tags = {"Enemy","Zombie"},
		components = {
			["Body"] = {15, 5},
			["Sprite"] = {"entities", 482, 0, 30, 52},
			["Physics"] = {},
			["Ai"] = {"Table","Zombie_Ai"}
		}
	},

	["Wolf"] = {
		tags = {"Enemy", "Wolf"},
		components = {
			["Body"] = {32, 8},
			["Animation"] = 
				{"entities",  {"wolf-run", 0.08},{"wolf-idle", 0.1}},
			["Physics"] = {},
			["Ai"] = {"Function",AI.Wolf},
			["Health"] = {3}
		}
	},

	["Bird"] = {
		tags = {"Meat"},
		components = {
			["Body"] = {8, 4},
			["Animation"] =
				{"entities", {"bird-fly", 0.1}, {"bird-idle", 0.1}},
			["Physics"] = {},
			["Ai"] = {"Table", "Bird_Ai"},
		}
	},

	["NPC"] = {
		tags = {"Npc"},
		components = {
			["Body"] = {8, 4},
			["Animation"] =
				{"entities", {"player-run", 0.8}},
			["Physics"]={},
		}
	},

	["Mech"] = {
		tags = {"Enemy"},
		components = {
			["Body"] = {12, 6},
			["Animation"] = {"entities", {"mech-idle", 0.1}, {"mech-attack", 0.1}},
			["Physics"] = {},
			["Ai"] = {"Table","Mech_Ai"},
			["Light"] = {}
		}
	},

	["Grendle"] = {
		tags = {"Enemy"},
		components = {
			["Body"] = {12, 6},
			["Animation"] = {"entities", {"grendle-idle", 0.1}, {"grendle-run", 0.1}},
			["Physics"] = {},
			["Ai"] = {"Function", AI.Grendle},
			["Light"] = {},
			["Health"] = {10}
		}
	},
	
	["Flag"] = {
		tags = {"Flag"},
		components = {
			["Body"] = {4, 4},
			["Animation"] = {"entities", {"flag", 0.01}},
			["Physics"] = {},
		}
	},

	["Shulk"] = {
		tags = {"Enemy"},
		components = {
			["Body"] = {32, 24},
			["Animation"] = {"entities", {"shulk-idle", 0.1}, {"shulk-attack", 0.1}},
			["Physics"] = {},
			["Ai"] = {"Function", AI.Shulk},
			["Light"] = {},
			["Health"] = {3},
			["Enemy"] = {
				drops = {
					{"Coin", 400, 400}, -- name, min, max
					{"Orange", 1, 1}, -- name, min, max
					{"Apple", 1, 1}, -- name, min, max
					{"Baseball", 1, 1}, -- name, min, max
					{"Potato", 1, 1}, -- name, min, max
					{"Bread", 1, 1}, -- name, min, max
					{"Tomato", 1, 1}, -- name, min, max
					{"Blueberrys", 1, 1}, -- name, min, max
					{"Carrot", 1, 1}, -- name, min, max
				},
			}
		}
	}

}
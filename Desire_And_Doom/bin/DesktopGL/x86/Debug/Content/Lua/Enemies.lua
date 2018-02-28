local AI = require "Content/Lua/AI"
require "Content/Lua/Grendle_AI"

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

	["Movable-Block"] = {
		tags = {},
		components = {
			["Body"] = {48, 24},
			["Sprite"] = {"entities", 512, 0, 48, 48},
			["Physics"] = {type = "dynamic"},
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
			["Health"] = {3},

			["Enemy"] = {
				drops = {
					{"Coin", 2, 5}, -- name, min, max
					{"Carrot", 0, 1}, -- name, min, max
					{"Tomato", 0, 1}, -- name, min, max
					{"Baseball", 0, 1}, -- name, min, max
				},
			}
		}
	},

	["Blue-Flame"] = {
		tags = {},
		components = {
			["Body"] = {8, 4},
			["Light"] = {},
			["Animation"] =
				{"entities", {"blue-flame", 0.1}},
			["Physics"] = {},
		}
	},


	["Bird"] = {
		tags = {"Meat"},
		components = {
			["Body"] = {8, 4},
			["Animation"] =
				{"entities", {"bird-fly", 0.1}, {"bird-idle", 0.1}},
			["Physics"] = {},
			["Ai"] = {"Function", AI.Bird},
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
			["Ai"] = {"Function", AI.Mech_AI},
			["Light"] = {},
			["Health"] = {5}
		}
	},

	["Grendle"] = {
		tags = {"Enemy"},
		components = {
			["Body"] = {12, 6},
			["Advanced_Animation"] = {"entities", {"grendle-idle", 0.1},{"grendle-awake", 0.1}, {"grendle-run", 0.1}, {"grendle-attack", 0.02}},
			["Physics"] = {},
			["Ai"] = {"Function", Grendle_AI},
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
			["Physics"] = {blacklist = {"Enemy-Hit"}},
			["Ai"] = {"Function", AI.Shulk},
			["Light"] = {},
			["Health"] = {3},
			["Enemy"] = {
				drops = {
					{"Coin", 2, 5}, -- name, min, max
					{"Orange", 0, 2}, -- name, min, max
				},
			}
		}
	},

	["Shulk-Bullet"] = {
		tags = {},
		components = {
			["Body"] = {8, 4},
			["Light"] = {},
			["Animation"] = {"entities", {"shulk-bullet", 0.1}},
			["Physics"] = {},
		}
	},

}
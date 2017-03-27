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
			["Ai"] = {"Table","Wolf_Ai"}
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
				{"entities", {"new-player-run", 0.8}},
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
			["Ai"] = {"Table","Grendle_Ai"},
			["Light"] = {}
		}
	}
}
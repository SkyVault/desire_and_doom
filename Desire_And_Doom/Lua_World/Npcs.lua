function npc_ai(entity, engine)

end

return {
	["npc-1"] = {
		tags = {"Npc"},
		components = {
			["Body"] = {8, 4},
			["Animation"] =
				{"Charactors", {"npc-1-walk", 0.8}},
			["Physics"]={},
			["Npc"] = {},
			["Ai"] = {"Function", npc_ai},
		}
	},
	["npc-2"] = {
		tags = {"Npc"},
		components = {
			["Body"] = {8, 4},
			["Animation"] =
				{"Charactors", {"npc-2-walk", 0.8}},
			["Physics"]={},
			["Npc"] = {},
			["Ai"] = {"Function", npc_ai},
		}
	},
	["npc-3"] = {
		tags = {"Npc"},
		components = {
			["Body"] = {8, 4},
			["Animation"] =
				{"Charactors", {"npc-3-walk", 0.8}},
			["Physics"]={},
			["Npc"] = {},
			["Ai"] = {"Function", npc_ai},
		}
	},

	["npc-4"] = {
		tags = {"Npc"},
		components = {
			["Body"] = {8, 4},
			["Animation"] =
				{"Charactors", {"npc-4-walk", 0.8}},
			["Physics"]={},
			["Npc"] = {},
			["Ai"] = {"Function", npc_ai},
		}
	},

	["npc-5"] = {
		tags = {"Npc"},
		components = {
			["Body"] = {8, 4},
			["Animation"] =
				{"Charactors", {"npc-5-walk", 0.8}},
			["Physics"]={},
			["Npc"] = {},
			["Ai"] = {"Function", npc_ai},
		}
	},

	["npc-6"] = {
		tags = {"Npc"},
		components = {
			["Body"] = {8, 4},
			["Animation"] =
				{"Charactors", {"npc-6-walk", 0.8}},
			["Physics"]={},
			["Npc"] = {},
			["Ai"] = {"Function", npc_ai},
		}
	},

	["npc-7"] = {
		tags = {"Npc"},
		components = {
			["Body"] = {8, 4},
			["Animation"] =
				{"Charactors", {"npc-7-walk", 0.8}},
			["Physics"]={},
			["Npc"] = {},
			["Ai"] = {"Function", npc_ai},
		}
	},


}
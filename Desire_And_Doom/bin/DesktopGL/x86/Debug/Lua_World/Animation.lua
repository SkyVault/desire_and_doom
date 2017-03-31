return {
	generate = {
		["wolf-run" ] = {192, 0, 32, 16, 6},
		["wolf-idle"] = {192 + 32, 0, 32, 16, 1},

		["bird-idle"] = {192, 16, 8, 8, 1},
		["bird-fly"]  = {192+8*2, 16, 8, 8, 3},

		["mech-idle"]   = {0, 464, 48, 48, 1},
		["mech-attack"] = {0, 464, 48, 48, 8},
		
		["player-idle"]			= {0, 133, 16, 26, 1},
		["player-run"]			= {34, 133, 18, 26, 8},
		["player-attack"]		= {37, 183, 32, 26, 4},
		["player-claymore-run"] = {0, 209, 36, 26, 8},

		["grendle-run"]  = {0, 400, 64, 64, 6},
		["grendle-idle"] = {0, 400, 64, 64, 1},

		["shulk-attack"]	= {0, 236, 32, 32, 16},
		["shulk-idle"]		= {0, 236, 32, 32, 1}
	}
}
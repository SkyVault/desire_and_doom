return {
	generate = {
		-- [name] = {sx, sy, w, h, num, ?offset_x, ?offset_y, ?speed, ?left_offset_x, ?right_offset_x}
		["wolf-run" ]				= {192, 0, 32, 16, 6, offset_x = 0, offset_y = 0},
		["wolf-idle"]				= {192 + 32, 0, 32, 16, 1},
		--
		["bird-idle"]				= {192, 16, 8, 8, 1},
		["bird-fly"]				= {192+8*2, 16, 8, 8, 3},
		--
		["mech-idle"]				= {0, 464, 48, 48, 1},
		["mech-attack"]				= {0, 464, 48, 48, 8},
		--
		["player-idle"]				= {0, 133, 16, 26, 1},
		["player-run"]				= {34, 133, 18, 26, 8},
		["player-attack"]			= {37, 183, 32, 26, 4},
		["player-claymore-run"]		= {0, 209, 36, 26, 8,	left_offset_x = 10, right_offset_x = -4},
		["player-claymore-idle"]	= {0, 209, 36, 26, 1,	left_offset_x = 10},
		["player-claymore-attack"]	= {128, 64, 64, 36, 6,	right_offset_x = 15, right_offset_y = 10, left_offset_x = -3, left_offset_y = 10, speed = 0.08},
		--
		["grendle-run"]				= {0, 400, 64, 64, 6},
		["grendle-idle"]			= {0, 400, 64, 64, 1},
		--
		["shulk-attack"]			= {0, 236, 32, 32, 16},
		["shulk-idle"]				= {0, 236, 32, 32, 1},

		["coin-flip"] = {0, 0, 8, 8, 4},

		["flag"] = {0, 268, 24, 24, 4, offset_x = 24-8, speed = 0.15}
	},
	-- TODO: implement explicit frame creation, with seperate speeds and offsets
	frames = {
		["grendle-attack"] = {
		--  {x, y, w, h, ox, oy}
			{0, 323, 64, 64, 0, 0},
--			{}
		}
	}
}
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
		["player-idle"]				= {0, 69, 16, 26, 1},
		["player-run"]				= {34, 69, 18, 26, 8},
		["player-attack"]			= {37, 121, 32, 26, 4},
		["player-claymore-run"]		= {0, 146, 36, 26, 8,	left_offset_x = 10, right_offset_x = -8},
		["player-claymore-idle"]	= {0, 380, 34, 26, 4,	left_offset_x = 10, right_offset_x = -8},
		["player-claymore-attack"]	= {128, 0, 64, 36, 6,	right_offset_x = 6, right_offset_y = 10, left_offset_x = -3, left_offset_y = 10, speed = 0.08},
		--
		["grendle-run"]				= {0, 400, 64, 64, 6},
		["grendle-idle"]			= {0, 400, 64, 64, 1},
		["grendle-attack"]			= {0, 292, 64, 80, 5, right_offset_y = 16, left_offset_y = 16, speed = 0.2},
		["grendle-awake"] 			= {0, 512, 64, 64, 16},

		--
		["shulk-attack"]			= {0, 236, 32, 32, 16},
		["shulk-idle"]				= {0, 236, 32, 32, 1},

		["coin-flip"] = {0, 0, 8, 8, 4},

		["flag"] = {0, 268, 24, 24, 4, offset_x = 24-8, speed = 0.15},


		-- npcs
		["npc-1-idle"] = {0, 172, 16, 26, 1},
		["npc-1-walk"] = {0, 172, 16, 26, 9},

		["npc-2-idle"] = {0, 224, 16, 26, 1},
		["npc-2-walk"] = {0, 224, 16, 26, 9},

		["npc-3-idle"] = {0, 224 + 26 * 1, 16, 26, 1},
		["npc-3-walk"] = {0, 224 + 26 * 1, 16, 26, 9},

		["npc-4-idle"] = {0, 224 + 26 * 2, 16, 26, 1},
		["npc-4-walk"] = {0, 224 + 26 * 2, 16, 26, 9},

		["npc-5-idle"] = {0, 224 + 26 * 3, 16, 26, 1},
		["npc-5-walk"] = {0, 224 + 26 * 3, 16, 26, 9},

		["npc-6-idle"] = {0, 224 + 26 * 4, 16, 26, 1},
		["npc-6-walk"] = {0, 224 + 26 * 4, 16, 26, 9},

		["npc-7-idle"] = {0, 224 + 26 * 5, 16, 26, 1},
		["npc-7-walk"] = {0, 224 + 26 * 5, 16, 26, 9},

		["chest-open"] = {0, 0, 32, 32, 5, offset_y = 6},
	},

	-- TODO: implement explicit frame creation, with seperate speeds and offsets
	frames = {
		-- x, y, w, h, speed?, x_offset? y_offset?, left_offset_x? left_offset_y? right_offset_x? right_offset_y?
		["player-smoke"] = {
			{0,   406, 16, 26},
			{16,  406, 16, 26},
			{32,  406, 16, 26},
			{48,  406, 16, 26},
			{64,  406, 16, 26},
			{80,  406, 34, 34},
			{114, 406, 34, 34},
			{148, 406, 34, 34},
			{182, 406, 34, 34},
			{216, 406, 34, 34},
			{250, 406, 34, 34},
			{284, 406, 34, 34},
			{318, 406, 34, 34},
			{352, 406, 34, 34},
			{386, 406, 34, 34},
		},
	}
}

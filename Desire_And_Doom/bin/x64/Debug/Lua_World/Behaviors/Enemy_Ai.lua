local is_flying = 0

return {
	["Zombie_Ai"] = 
	{"block",
		{"entity_within", "Player", 70},
		{"block_with_skip", --[[{"track", "Player", 2}]]},
		{"block",
			{"if_timer", 1},
			{"block",
				{"new_target_dir", 10},
				{"timer_reset"}
			},
			{"move_towards_target", 2},
		}
	},

	["Wolf_Ai"] = {
	"block",
		{"face_move_dir"},

		{"velocity_lessthan", 0.2},
		{"block_with_skip", {"set_animation", "wolf-idle"}},
		{"block", {"set_animation", "wolf-run"}},

		{"entity_within", "Player", 70},
		{"block_with_skip", {"track", "Player", 4}},
		{"block", 
			--{"timer", 1},
			--{"block",
			--	{"new_target_dir", 10},
			--	{"timer_reset"}
			--},
			--{"move_towards_target", 2}
		},
	},

	["Bird_Ai"] = {
	"block",
		
		{"if_not_flag", is_flying},
		{"block_with_skip",
			{"entity_within", "Player", 60},
			{"block_with_skip", 
				{"set_flag", is_flying},
			},
			{"block", {"set_animation", "bird-idle"}},
			{"timer_reset"},
		},
		{"block",
			--{"if_timer", 10},
			--{"destroy", "self"},

			{"set_animation", "bird-fly"},
					{"set_target", -45 },
					{"move_towards_target", 10}
			}
	}
}
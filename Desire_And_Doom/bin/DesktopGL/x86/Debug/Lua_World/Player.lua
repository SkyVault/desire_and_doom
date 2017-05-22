return {
	["Player"] = {
		tags = {"Player"},
		components = {
			["Body"] = {10, 5},
			["Physics"] = {},
			["Animation"] = {
				"Charactors", 
				{"player-run", 0.08}, 
				{"player-idle", 0.1}, 
				{"player-attack", 0.08}, 
				{"player-claymore-run", 0.08}, 
				{"player-claymore-idle", 0.1}, 
				{"player-claymore-attack", 0.8},
				{"player-smoke", 0.8},
			},
			["Invatory"] = {3, 3},
			["Player"] = {},
			["Light"] = {},
			["Equipment"] = {},
			["Health"] = {3},

			["Character"] = {
				name	= "player",
				age		= 34
			}
		},
	}
}
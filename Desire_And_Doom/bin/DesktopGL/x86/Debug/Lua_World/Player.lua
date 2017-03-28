return {
	["Player"] = {
		tags = {"Player"},
		components = {
			["Body"] = {10, 5},
			["Physics"] = {},
			["Animation"] = {
				"entities", {"new-player-run", 0.08}, {"new-player-idle", 0.1}, {"new-player-attack", 0.08}
			},
			["Invatory"] = {3, 3},
			["Player"] = {},
			["Light"] = {},
--			[""]
		},
	}
}
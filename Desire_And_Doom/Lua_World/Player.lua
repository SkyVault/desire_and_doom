return {
	["Player"] = {
		tags = {"Player"},
		components = {
			["Body"] = {10, 5},
			["Physics"] = {},
			["Animation"] = {
				"entities", {"player-run", 0.08}, {"player-idle", 0.1}, {"player-attack", 0.08}, {"player-claymore-run"}
			},
			["Invatory"] = {3, 3},
			["Player"] = {},
			["Light"] = {},
--			[""]
		},
	}
}
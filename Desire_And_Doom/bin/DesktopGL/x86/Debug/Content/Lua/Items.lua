function simple_item(name, quad)
	return {
		tags = {name, "Item"},
		components = {
			["Body"] = {4, 2},
			["Physics"] = { btags = {"Item"} },
			["Sprite"] = {
				"items",
				quad[1], quad[2], quad[3], quad[4],
			},
			["Item"] = {}
		}
	}
end


return {
--
--	["Apple"] = {
--		tags = {"Apple"},
--		components = {
--			["Body"] = {8, 4},
--			["Physics"] = {},
--			["Sprite"] = {
--				"items", 0, 0, 16, 16
--			},
--			["Item"] = {}
--		}
--	},
--

	["Coin"] = {
		tags = {"Coin", "Item"},
		components = {
			["Body"] = {4, 2},
			["Physics"] = { btags = {"Item"} },
			["Animation"] = {
				"items",
				{"coin-flip", 0.08},
			},

			["Item"] = {}
		}
	},

	["Apple"]		= simple_item("Apple",		{0, 8, 8, 8}),
	["Carrot"]		= simple_item("Carrot",		{8, 8, 8, 8}),
	["Potato"]		= simple_item("Potato",		{16, 8, 8, 8}),
	["Bread"]		= simple_item("Bread",		{24, 8, 8, 8}),
	["Orange"]		= simple_item("Orange",		{32, 8, 8, 8}),
	["Tomato"]		= simple_item("Tomato",		{48, 8, 8, 8}),
	["Blueberrys"]	= simple_item("Blueberrys", {64, 8, 8, 8}),
	["Baseball"]	= simple_item("Baseball",	{96, 8, 8, 8}),
}
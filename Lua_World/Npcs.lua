math.randomseed(os.time())

function string:split( inSplitPattern, outResults )
	if not outResults then
		outResults = { }
	end
	local theStart = 1
	local theSplitStart, theSplitEnd = string.find( self, inSplitPattern, theStart )
	while theSplitStart do
		table.insert( outResults, string.sub( self, theStart, theSplitStart-1 ) )
		theStart = theSplitEnd + 1
		theSplitStart, theSplitEnd = string.find( self, inSplitPattern, theStart )
	end
	table.insert( outResults, string.sub( self, theStart ) )
	return outResults
end

function npc_ai(entity, engine)
	local body = engine:Get_Component(entity, "Body")
	local physics = engine:Get_Component(entity, "Physics")
	local animation = engine:Get_Component(entity, "Animation")


	if body and physics then
		engine:Face_Move_Dir(entity)

		local anim_string = animation.Current_Animation_ID
		local tokens = anim_string:split "-"

		local anim_id = tokens[1] .. "-" .. tokens[2] .. "-"

		if physics.Current_Speed < 0.2 then
			anim_id = anim_id .. "idle"
		else
			anim_id = anim_id .. "walk"
		end
		animation.Current_Animation_ID = anim_id

		local lua = engine:Get_Component(entity, "Lua_Function")
		if lua then
			if lua.Table == nil then
				-- setup the table for timers and stuff
				lua.Table = {walk_timer = 0, target_x = body.X + -50 + math.random() * 100, target_y = body.Y + -50 + math.random() * 100}
			else
				local dist = 200
				local speed = 3
				local buffer = 10
				local time = 6

				local dot = body:Angle_To_Other(engine:Vec2(lua.Table.target_x, lua.Table.target_y))
				physics:Apply_Force(speed, dot)

				local function retarget()
					lua.Table.target_x = body.X + (-(dist/2) + math.random() * dist)
					lua.Table.target_y = body.Y + (-(dist/2) + math.random() * dist)
				end

				if engine:Dist(body.X, body.Y, lua.Table.target_x, lua.Table.target_y) < 20 then
					retarget()
				end

				if (lua.Table.walk_timer <= 0) then
					retarget()
					lua.Table.walk_timer = time
				end
				lua.Table.walk_timer = lua.Table.walk_timer - engine:Get_DT()

--				var o_body = (Body) other.Get(Types.Body);
--                var dot = body.Angle_To_Other(o_body);
--                physics.Apply_Force(force, dot);
			end
		end
	end
end

return {
	["npc-1"] = {
		tags = {"Npc"},
		components = {
			["Body"] = {8, 4},
			["Animation"] =
				{"Charactors", {"npc-1-walk", 0.8},{"npc-1-idle", 0.8}},
			["Physics"]={},
			["Npc"] = {},
			["Ai"] = {"Function", npc_ai},
			["Invatory"] = {3, 2},
		}
	},
	["npc-2"] = {
		tags = {"Npc"},
		components = {
			["Body"] = {8, 4},
			["Animation"] =
				{"Charactors", {"npc-2-walk", 0.8},{"npc-2-idle", 0.8}},
			["Physics"]={},
			["Npc"] = {"wolf_gui"},
			["Ai"] = {"Function", npc_ai},
		}
	},
	["npc-3"] = {
		tags = {"Npc"},
		components = {
			["Body"] = {8, 4},
			["Animation"] =
				{"Charactors", {"npc-3-walk", 0.8},{"npc-3-idle", 0.8}},
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
				{"Charactors", {"npc-4-walk", 0.8},{"npc-4-idle", 0.8}},
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
				{"Charactors", {"npc-5-walk", 0.8},{"npc-5-idle", 0.8}},
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
				{"Charactors", {"npc-6-walk", 0.8},{"npc-6-idle", 0.8}},
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
				{"Charactors", {"npc-7-walk", 0.8},{"npc-7-idle", 0.8}},
			["Physics"]={},
			["Npc"] = {},
			["Ai"] = {"Function", npc_ai},
		}
	},


}

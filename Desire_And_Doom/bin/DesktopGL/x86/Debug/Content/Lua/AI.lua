local 
function handle_player_hit(entity, engine, damage, power)

	local physics = engine:Get_Component(entity, "Physics")
	local body = engine:Get_Component(entity, "Body")

	if physics and physics.Other then
			local other = physics.Other
			if other:Has_Tag("Player-Hit") then
				local o_body = engine:Get_Component(other, "Body")
				local health = engine:Get_Component(entity, "Health")
				if health then
					print "I have been hit!";
					-- take damage according to the weapons damage
					health:Hurt(damage)
					physics.Other:Destroy()

					local camera = engine:Get_Camera()
					camera:Shake(5, 0.1);

					local sprite = engine:Get_Component(entity, "Animation")
					sprite.Flash_Timer = 0.1

					local side = (o_body.X > body.X) and -1 or 1
					
					physics.Vel_X = (power or 10) * side 

					if health.Should_Die then
						entity:Destroy()
					end
				else
					print("[LUA]::Warning:: handle_player_hit function requires the entity to have a health component.")
				end
			end
		end
end

local shulk_bullet = {
	tags = {"Enemy-Hit", "Enemy"},
	components = {
		["Body"] = {8, 4},
		["Animation"] = {"entities", {"shulk-bullet", 0.1}},
		["Physics"] = {type = "dynamic", blacklist = {"Enemy"}},
	}
}

return {

	------------------------------------------------------- WOLF AI -------------------------------------------------------------
	--																														   --
	-----------------------------------------------------------------------------------------------------------------------------
	Wolf = function(entity, engine)
		engine:Face_Move_Dir(entity)

		local physics = engine:Get_Component(entity, "Physics")
		local anim = engine:Get_Component(entity, "Animation")
		local body = engine:Get_Component(entity, "Body")
		if physics and anim and body then
			
			if physics.Current_Speed < 0.2 then
				anim.Current_Animation_ID = "wolf-idle"
			else
				anim.Current_Animation_ID = "wolf-run"
			end

			if (engine:Entity_Within("Player", body.X, body.Y, 140)) then
				engine:Track(entity, engine:Get_With_Tag "Player", 4)
			end

			handle_player_hit(entity, engine, 1, 100)

		end
	end,

	------------------------------------------------------ SHULK AI -------------------------------------------------------------
	--																														   --
	-----------------------------------------------------------------------------------------------------------------------------
	Shulk = function(entity, engine)
		local mx_dist = 150
		local body = engine:Get_Component(entity, "Body")
		local anim = engine:Get_Component(entity, "Animation")
		local physics = engine:Get_Component(entity, "Physics")
		local fn = engine:Get_Component(entity, "Lua_Function")

		if fn.Table == nil then 
			fn.Table = {
				shoot_timer = 0	
			}
		end

		if body and anim and physics then
			local player = engine:Get_Player()
			if player then
				
				local p_body = engine:Get_Component(player, "Body")
				if p_body then
					local dist = engine:Dist(body.X, body.Y, p_body.X, p_body.Y)
					if (dist < mx_dist) then
						anim.Current_Animation_ID = "shulk-attack";
					else
						anim.Current_Animation_ID = "shulk-idle";
					end
				end

				if anim.Current_Frame == 9 - 1 and fn.Table.shoot_timer <= 0 then
					fn.Table.shoot_timer = 1

					local bullet = engine:Spawn(shulk_bullet, body.Center.X - 4, body.Center.Y - 4)	
					local bphysics = engine:Get_Component(bullet, "Physics")
					local bbody = engine:Get_Component(bullet, "Body")

					local angle = math.atan2(
						p_body.Y - bbody.Y,
						p_body.X - bbody.X
					)

					bphysics.DestroyOnCollision = true;

					bphysics.Vel_X = math.cos(angle) * 100
					bphysics.Vel_Y = math.sin(angle) * 100
					bphysics.Friction = 1
				end

				local dt = engine:Get_DT()
				fn.Table.shoot_timer = fn.Table.shoot_timer - dt

				handle_player_hit(entity, engine, 1, 100)
				
			end
		end
	end,

	----------------------------------------------------- GRENDLE AI ------------------------------------------------------------
	--																														   --
	-----------------------------------------------------------------------------------------------------------------------------
	Grendle = function(entity, engine)
		local mx_dist = 150
		local body = engine:Get_Component(entity, "Body")
		local anim = engine:Get_Component(entity, "Animation")
		local physics = engine:Get_Component(entity, "Physics")
		local fn = engine:Get_Component(entity, "Lua_Function")

		engine:Face_Move_Dir(entity)
		
		-- initialize the table if it is nil
		if fn.Table == nil then 
			fn.Table = {}
		end

		--if body and anim and physics then
		--	local player = engine:Get_Player()
		--
		--	if physics.Current_Speed < 0.2 then
		--		anim.Current_Animation_ID = "grendle-idle"
		--	else
		--		anim.Current_Animation_ID = "grendle-run"
		--	end
		--
		--	if (engine:Entity_Within("Player", body.X, body.Y, 150)) then
		--		if engine:Entity_Within("Player", body.X, body.Y, 75) then
		--			-- do attack
		--			anim.Current_Animation_ID = "grendle-attack"
		--			fn.Table["state"] = "attacking"
		--		else
		--			fn.Table["state"] = "tracking"
		--			engine:Track(entity, engine:Get_With_Tag "Player", 4)
		--		end
		--	else
		--		fn.Table["state"] = "idling"
		--	end
		--
		--	handle_player_hit(entity, engine, 1, 100)
		--	
		--end
	end,

	----------------------------------------------------- GRENDLE AI ------------------------------------------------------------
	--																														   --
	-----------------------------------------------------------------------------------------------------------------------------
	Bird = function(entity, engine)
		local mx_dist	= 60
		local body		= engine:Get_Component(entity, "Body")
		local anim		= engine:Get_Component(entity, "Animation")
		local physics	= engine:Get_Component(entity, "Physics")
		local fn		= engine:Get_Component(entity, "Lua_Function")

		if fn.Table == nil then fn.Table = {}; end

		if not fn.Table.is_flying then
			anim.Current_Animation_ID = "bird-idle"
			if (engine:Entity_Within("Player", body.X, body.Y, mx_dist)) then
				fn.Table.is_flying = true
			end
		else
			anim.Current_Animation_ID = "bird-fly"
			physics.Flying = true

			physics.Vel_X =  75 * 0.5
			physics.Vel_Y = -60 * 0.5
		end
	end,

	-- Mech AI
	Mech_AI = function(entity, engine)
		local mx_dist	= 75
		local body		= engine:Get_Component(entity, "Body")
		local anim		= engine:Get_Component(entity, "Animation")
		local physics	= engine:Get_Component(entity, "Physics")
		local fn		= engine:Get_Component(entity, "Lua_Function")

		if fn.Table == nil then fn.Table = {}; end

		if (engine:Entity_Within("Player", body.X, body.Y, mx_dist)) then
			anim.Current_Animation_ID = "mech-attack"
			engine:Track(entity, engine:Get_With_Tag "Player", 3)
		else
			anim.Current_Animation_ID = "mech-idle"
		end

		handle_player_hit(entity, engine, 1, 100)
	end,
}
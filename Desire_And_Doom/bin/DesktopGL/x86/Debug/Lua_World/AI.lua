local 
function handle_player_hit(entity, engine, dammage, power)
	local physics = engine:Get_Component(entity, "Physics")
	local body = engine:Get_Component(entity, "Body")
	if physics and physics.Other then
			local other = physics.Other
			if other:Has_Tag("Player-Hit") then
				local o_body = engine:Get_Component(other, "Body")
				local health = engine:Get_Component(entity, "Health")
				if health then
					-- take dammage according to the weapons dammage
					health:Hurt(dammage)
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
				end

			end
		end
end

function Init_Entity(entity, callback)
	local fn = entity:Get "Lua_Function"
	if not fn then return end

	if fn.Table == null then
		fn.Table = {__initialized = false}
	end
	assert(callback)

	if fn.Table.__initialized == false then
		callback()
		fn.Table.__initialized = true
	end
end

function Get_The_Correct_Component(entity, list)
	local sprite = nil
	for i = 1, #list do
		local l = list[i]
		if entity:Has(l) then
			return entity:Get(l) 
		end
	end
	return sprite
end

--   var sprite = (Animated_Sprite)e.Get(Types.Animation);
--   var physics = (Physics) e.Get(Types.Physics);
--   if (sprite != null && physics != null)
--   {
--       if ( physics.Velocity.X > 0 )
--           sprite.Scale = new Vector2(1, sprite.Scale.Y);
--       else
--           sprite.Scale = new Vector2(-1, sprite.Scale.Y);
--   }
function Face_Move_Dir(entity, engine)
	local sprite = Get_The_Correct_Component(entity, {
		"Advanced_Animation",
		"Animated_Sprite",
		"Sprite",
	})
	if not sprite then return end

	local physics = entity:Get "Physics"
	if not physics then return end

	if physics.Velocity.X > 0 then
		sprite.Scale = engine:Vec2(1, sprite.Scale.Y)
	else
		sprite.Scale = engine:Vec2(-1, sprite.Scale.Y)
	end
end

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

			if (engine:Entity_Within("Player", body.X, body.Y,70)) then
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
	end
}
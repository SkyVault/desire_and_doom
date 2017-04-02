return {
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

		end
	end,

	Shulk = function(entity, engine)
		local mx_dist = 150
		local body = engine:Get_Component(entity, "Body")
		local anim = engine:Get_Component(entity, "Animation")

		if body and anim then
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
				
			end
		end
	end
}
local timer = 0

require "Lua_World/Utilities"

local first = false

local opening_events = 0
local shoot_timer = 0

local
function Load_Ai(entity, engine)
	local art = engine:Get_Component(entity, "Multipart_Animation");
	local body = engine:Get_Component(entity, "Body")

	if (art == nil) then
		print "cannot find the Multipart_Animation component!"
	end
	
	local eyes = art.Animation_Components["eyes"]
	local head = art.Animation_Components["head"]
	local mouth = art.Animation_Components["mouth"]

	eyes.Playing = false
	head.Playing = false
	mouth.Playing = false

	eyes.Current_Frame = 0
	head.Current_Frame = 0
	mouth.Current_Frame = 0

	eyes.Animation_End = false
	head.Animation_End = false
	mouth.Animation_End = false

	local mtimer = 0

	opening_events = Eventing.new {
		function(self, dt)
			if engine:Entity_Within("Player", body.X, body.Y, 100) then
				self:delay_next("eyes-delay", 1, dt)
			end
		end,

		function(self, dt)
			if self.done == false then
				eyes.Playing = true
				if eyes.Animation_End then
					eyes.Playing = false
					self:delay_next("eyes-open", 1.5, dt)
				end
			end
		end,

		function(self, dt)
			mouth.Playing = true
			if mouth.Animation_End then
				mouth.Playing = false
				self:delay_next("mouth-open", 0.2, dt)
			end
		end,

		function(self, dt)
			local physics 	= engine:Get_Component(entity, "Physics")
			local emitter 	= engine:Get_Component(entity, "Entity_Particle_Emitter")
			local health 	= engine:Get_Component(entity, "Health")
			local body 		= engine:Get_Component(entity, "Body")

			if emitter then
				emitter.Active = true
			end

			mtimer = mtimer + dt
			-- print "hello"
			-- physics.Vel_X = math.cos(mtimer)
			body.X = body.X + math.cos(mtimer) * 2
			body.Y = body.Y + math.sin(mtimer *  4)

			local player = engine:Get_With_Tag "Player"

			if player then
				local p_body = engine:Get_Component(player, "Body")

				local dy = (p_body.Y - 80) - body.Y
				local dx = (p_body.X) - body.X 
				
				body.X = body.X + dx * 0.008
				body.Y = body.Y + dy * 0.01
			end

			if (health.Ammount < 6) then
				-- self:goto_fn(1)
				-- next wave thingy
			end

			-- self:next()
		end,	
	}
end

local 
function Ai(entity, engine)
	if not first then
		Load_Ai(entity, engine)
		first = true
	end


	local dt = engine:Get_DT()
	opening_events:update(dt)

	--Animation_Components
	
	local physics = engine:Get_Component(entity, "Physics")
	local art = engine:Get_Component(entity, "Multipart_Animation");
	local body = engine:Get_Component(entity, "Body")
	local health = engine:Get_Component(entity, "Health")

	timer = timer + dt

	if health.Should_Die then 
		entity:Destroy()
		return
	end

	if physics.Other then
		if physics.Other:Has_Tag "Player-Hit" then
			local o_body = engine:Get_Component(physics.Other, "Body")

			-- apply velocity
			local side = (o_body.X > body.X) and -1 or 1

			local power = 200					
			physics.Vel_X = (power) * side 

			-- local eyes = art.Animation_Components["eyes"]
			-- local head = art.Animation_Components["head"]
			-- local mouth = art.Animation_Components["mouth"]

			-- if eyes and head and mouth then
			-- 	eyes.Flash_Timer = 0.1
			-- 	head.Flash_Timer = 0.1
			-- 	mouth.Flash_Timer = 0.1
			-- end			

			local camera = engine:Get_Camera()
			camera:Shake(5, 0.1);

			health:Hurt(1)
			physics.Other:Destroy()
		end
	end

	-- body.X = body.X + math.cos(timer / 2)

	-- body.Y = body.Y + math.sin(timer) / 2	
end

return Ai

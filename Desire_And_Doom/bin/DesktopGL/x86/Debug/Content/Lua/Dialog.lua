local enemies = require "Content/Lua/Enemies"

return {
    ["dialog_npc_test"] = {
        [1] = {"#White Hello I am an #Cyan Npc! #White What can I #Yellow do #White for you?"}
    },

    ["wolf_gui"] = {
        [1] = { "Help me dude! can you fight a wolf?", {
                {"yes", 2, action = function(self, other, engine) 
					self:Destroy()

					local body = engine:Get_Component(self, "Body")
					if engine ~= nil and body ~= nil then
						engine:Spawn(enemies["Wolf"], body.X, body.Y)
					end
				end},
                {"no", 0}
		}},
        [2] = { "Whaaa hahaha... this will be your doom", 0 }
    },

	["orange_guy"] = {
		[1]	= {"Can you find me some oranges?", {

			{"yes", 0, action = function(self, other, engine)
			
			end},

			{"no", 2, action = function(self, other, engine)
				self:Destroy()

				local body = engine:Get_Component(self, "Body")
				if engine ~= nil and body ~= nil then
					engine:Spawn(enemies["Wolf"], body.X, body.Y)
				end
			end},
		}},
		[2] = {"Well then DIE!!!"}
	}
}

--[[
--
--= {
        },

--]]

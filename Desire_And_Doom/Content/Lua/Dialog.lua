return {
    ["dialog_npc_test"] = {
        [1] = {"#White Hello I am an #Cyan Npc! #White What can I #Yellow do #White for you?"}
    },

    ["wolf_gui"] = {
        [1] = { "Help me dude! can you fight a wolf?", {
                {"yes", 2, action = function(self, other) 
					print "Boogers!"	
				end},
                {"no", 0}
		}},
        [2] = { "Whaaa hahaha... this will be your doom", 0 }
    }
}

--[[
--
--= {
        },

--]]

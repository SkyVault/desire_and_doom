require "Lua_World/AI"
require "Lua_World/Utilities"

local 
function Load_Grendle(entity, engine)

end

function Grendle_AI(entity, engine)
    local mx_dist = 100
    local body      = entity:Get "Body"
    local anim      = entity:Get "Advanced_Animation"
    local physics   = entity:Get "Physics"
    local fn        = entity:Get "Lua_Function"

    Init_Entity(entity, function()
        
        fn.Table["eventer"] = Eventing.new {
            function (self, dt)
                if engine:Entity_Within("Player", body.X, body.Y, mx_dist) then
                
                    anim:Request_Animation_Playback "grendle-awake"  
                    if (anim.Animation_Finished) then 
                        self:next()
                    end
                
                end
            end,

            function(self, dt)
                 anim:Stop()

                 engine:Track(entity, engine:Get_With_Tag "Player", 4)
            end,    
        }

    end)

    -- face the current moving direction
    engine:Face_Move_Dir(entity)
    
    if fn.Table["eventer"] then
        fn.Table["eventer"]:update(engine:Get_DT())
    end
end
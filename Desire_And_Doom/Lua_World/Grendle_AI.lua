require "Lua_World/AI"
require "Lua_World/Utilities"

local 
function Load_Grendle(entity, engine)

end

function Grendle_AI(entity, engine)
    local mx_dist = 100
    local body      = entity:Get "Body"
    local anim      = entity:Get "Animation"
    local physics   = entity:Get "Physics"
    local fn        = entity:Get "Lua_Function"

    Init_Entity(entity, function()
        
        fn.Table["eventer"] = Eventing.new {
            function (self, dt)
                if engine:Entity_Within("Player", body.X, body.Y, mx_dist) then
                    anim.Current_Animation_ID = "grendle-awake"
                    if anim.Animation_End then
                        self:next()
                    end
                end
            end,

            function (self, dt)
                anim.Current_Animation_ID = "grendle-idle"
                print "now attack"
            end
        }

    end)

    if fn.Table["eventer"] then
        fn.Table["eventer"]:update(engine:Get_DT())
    end
end
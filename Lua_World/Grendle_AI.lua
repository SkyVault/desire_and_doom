require "Lua_World/AI"
require "Lua_World/Utilities"

local 
function Load_Grendle(entity, engine)

end

function Grendle_AI(entity, engine)
    local mx_dist           = 100
    local mx_attack_time    = 8
    local body              = entity:Get "Body"
    local anim              = entity:Get "Advanced_Animation"
    local physics           = entity:Get "Physics"
    local fn                = entity:Get "Lua_Function"
    
    -- face the current moving direction
    Face_Move_Dir(entity, engine)

    Init_Entity(entity, function()
        fn.Table.Attack_Timer = 0
        fn.Table.Do_Awake = false

        fn.Table["eventer"] = Eventing.new {
            function (self, dt)
                if engine:Entity_Within("Player", body.X, body.Y, mx_dist) then
                    fn.Table.Do_Awake = true
                end

                if fn.Table.Do_Awake then
                    anim:Request_Animation_Playback "grendle-awake"  
                    if (anim.Animation_Finished) then 
                        anim:Stop()
                        self:next()
                    end
                end
            end,

            function(self, dt)
                 if engine:Entity_Within("Player", body, mx_dist / 2) and
                    fn.Table.Attack_Timer <= 0 then

                    fn.Table.Attack_Timer = mx_attack_time
                    anim:Force_Animation_Playback "grendle-attack"
                 else
                    
                    engine:Track(entity, engine:Get_With_Tag "Player", 4)
                    
                    if anim.Current_Animation_ID == "grendle-attack" and
                       anim.Animation_Finished then
                    
                        anim:Force_Animation_Playback "grendle-run"
                    elseif anim.Current_Animation_ID == "grendle-run" then
                        anim:Force_Animation_Playback "grendle-run"
                    end
                end
            end,        
        }

    end)

    
    if fn.Table["eventer"] then
        fn.Table["eventer"]:update(engine:Get_DT())

        if fn.Table.Attack_Timer > 0 then
            fn.Table.Attack_Timer = fn.Table.Attack_Timer - engine:Get_DT()
        end
    end
end
_G.Eventing = {
    new = function(list)
        return {
            functions = list,
            pointer = 1,
            done = false,

            timers = {},

            goto_fn = function(self, num)
                self.pointer = num
            
                if self.pointer > #self.functions then
                    self.pointer = 1
                    self.done = true
                end
            end,
            
            next = function(self)
                self.pointer = self.pointer + 1
                
                if self.pointer > #self.functions then
                    self.pointer = 1
                    self.done = true
                end
                
            end,

            delay_next = function(self, id, time, dt)
                local timer = self.timers[id]
                if timer == nil then
                    self.timers[id] = {
                        max_time = time,
                        ticker = 0
                    }
                else
                    if timer.ticker >= timer.max_time then
                        self.timers[id] = nil
                        self:next()
                    else
                        timer.ticker = timer.ticker + dt
                    end
                end
            end,

            update = function(self, dt)
                self.functions[self.pointer](self, dt)
            end
        }
    end
}
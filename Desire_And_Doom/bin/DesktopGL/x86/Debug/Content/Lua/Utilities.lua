_G.Eventing = {
    new = function(list)
        return {
            functions = list,
            pointer = 1,
            done = false,

			timer = 0,
			delaying = false,

			delayed_next = function(self, time)
				if not self.delaying then
					self.timer = time
					self.delaying = true
				end
			end,
            
            next = function(self)
                self.pointer = self.pointer + 1

                if self.pointer > #self.functions then
                    self.pointer = 1
                    self.done = true
                end
            end,

			goto_fn = function(self, num)
				self.pointer = num
			end,

            update = function(self, dt)
				if self.timer > 0 then
					self.timer = self.timer - dt
				else
					if self.delaying then
						self:next()
						self.delaying = false
					end
				end
                self.functions[self.pointer](self, dt)
            end
        }
    end
}
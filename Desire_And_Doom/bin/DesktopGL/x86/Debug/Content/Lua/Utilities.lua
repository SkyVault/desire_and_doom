_G.Eventing = {
    new = function(list)
        return {
            functions = list,
            pointer = 1,
            done = false,
            
            next = function(self)
                self.pointer = self.pointer + 1

                if self.pointer > #self.functions then
                    self.pointer = 1
                    self.done = true
                end
            end,

            update = function(self)
                self.functions[self.pointer](self)
            end
        }
    end
}
local entity_tables = {}

return {
	["Chest"] = {
		tags = {"Chest"},
		components = {
			["Body"] = {30, 8},
			["Animation"] = {"Chest", {"chest-open", 0.08}},
			["Physics"] = {},

			["Spawner"] = {
				entities = {
					{"Coin", 10, 20}
				}
			},

			["Ai"] = {"Function", function(self, engine)
				local anim = self:Get "Animation"
				if entity_tables[self.UUID] == nil then
					entity_tables[self.UUID] = {
						opened = false,
						opening = false,
					}
					local wi = self:Get "World_Interaction"
					if wi then
						wi.Constant_Update = true
					end

					anim.Playing = false
				end
				local body = self:Get "Body"

				if body then
					local t = entity_tables[self.UUID]
					if t.opening == false and t.opened == false and (engine:Entity_Within("Player", body.X, body.Y, 40)) then
						t.opening = true
						anim.Playing = true
					end

					if t.opening and not t.opened and anim.Animation_End then
						anim.Playing = false

						local spawner = self:Get "Spawner"
						spawner:Do_Spawn(body.X + body.Width / 2, body.Y + body.Height / 2)

						t.opening = false
						t.opened = true
					end
				end
			end},
		}
	}
}
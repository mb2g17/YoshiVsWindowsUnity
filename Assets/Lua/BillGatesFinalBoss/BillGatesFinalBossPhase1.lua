phase = require("phase")

-- Shoots fireballs
function fireball()
	flamethrower.ShootYellowFlash()
	for i = 0, 5 do
		fireballshooter.ShootFireball()
		wait(0.4)
	end
end

-- Shoots money 5 times
function shootmoney()
	for i = 0, 5 do
		money.ShootMoney()
		wait(0.4)
	end
end

wait(0.25)
timeline = {
	function() fireball() end,
	function() wait(0.2) end,
	function() shootmoney() end,
	function() wait(1) end
}
phase.begin(timeline, function() return billgates.Health <= 45 end)

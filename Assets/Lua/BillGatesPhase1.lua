phase = require("phase")

-- Shoots fireballs 5 times
function fireball()
	for i = 0, 5 do
		fireballshooter.ShootFireball()
		wait(0.5)
	end
end

-- Fires that yellow thing
function yellowflash()
	flamethrower.ShootYellowFlash()
	wait(1.5)
end

-- Shoots money 5 times
function shootmoney()
	for i = 0, 5 do
		money.ShootMoney()
		wait(0.5)
	end
end

timeline = {
	function() fireball() end,
	function() wait(0.5) end,
	function() yellowflash() end,
	function() wait(0.5) end,
	function() shootmoney() end,
	function() wait(0.5) end,
	function() yellowflash() end,
	function() wait(0.5) end
}
phase.begin(timeline, function() return billgates.Health <= 45 end)

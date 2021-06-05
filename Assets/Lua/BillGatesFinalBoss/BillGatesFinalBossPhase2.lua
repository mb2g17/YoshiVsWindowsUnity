phase = require("phase")

function yellowflash()
	flamethrower.ShootYellowFlash()
end

-- Shoots fireballs
function fireball()
	for i = 0, 4 do
		fireballshooter.ShootFireball()
		wait(0.05)
	end
end

function coinspit()
	runblock(flowchart, "Coin Spit", 0, true)
end

-- Shoots a stream of money
function shootmoney()
	for i = 0, 4 do
		money.ShootMoney()
		wait(0.1)
	end
end

wait(0.15)
timeline = {
	function() fireball() end,
	function() wait(1.2) end,
	function() fireball() end,
	function() wait(1.2) end,
	function() fireball() end,
	function() wait(1.2) end,

	function() wait(0.25) end,
	function() coinspit() end,

	function() shootmoney() end,
	function() wait(1.2) end,
	function() shootmoney() end,
	function() wait(1.2) end,
	function() shootmoney() end,
	function() wait(1.2) end,

	function() wait(0.5) end
}
phase.begin(timeline, function() return billgates.Health <= 25 end)

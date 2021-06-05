phase = require("phase")

-- Shoots fireballs and does yellow thing
function fireball()
	flamethrower.ShootYellowFlash() -- Does yellow flash thing
	for i = 0, 5 do
		fireballshooter.ShootFireball()
		wait(0.5)
	end
	laser.SetTrigger("Fire")
end

-- Shoots money and coins
function shootmoney()
	runblock(flowchart, "Coin Spit", 0, true)
	for i = 0, 5 do
		money.ShootMoney()
		wait(0.5)
	end
end

timeline = {
	function() fireball() end,
	function() wait(0.5) end,
	function() shootmoney() end,
	function() wait(0.5) end
}
phase.begin(timeline, function() return billgates.Health <= 15 end)

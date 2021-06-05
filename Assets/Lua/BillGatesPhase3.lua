phase = require("phase")

-- Shoots money and coins
function shootmoney()
	billgatesphase3.ShootMoneyParticle()
end

timeline = {
	function() shootmoney() end,
	function() wait(0.5) end
}
phase.begin(timeline, function() return billgates.Health == 0 end)

phase = require("phase")

-- Shoots money from gates
function shootmoney()
	for x=1,2 do
		for x=1,3 do
			billgatesphase3.ShootMoneyParticle()
			wait(0.25)
		end
		wait(0.25)
	end
end

-- Shoots money down
function shootmoneydown()
	for x=-8,5,3 do
		makemoneydown(x)
		wait(0.2)
	end
	for x=5,-8,-3 do
		makemoneydown(x)
		wait(0.2)
	end
end

function makemoneydown(x)
	money = luautils.Instantiate(moneyparticledown)
	money.transform.position = factory.vector3(x, 5, 0)
end

timeline = {
	function() shootmoney() end,
	function() wait(0.4) end,
	function() shootmoneydown() end,
	function() wait(0.4) end
}
phase.begin(timeline, function() return billgates.Health == 0 end)

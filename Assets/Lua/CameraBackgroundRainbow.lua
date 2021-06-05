math.randomseed(os.time())

function randomColor()
	local min = 0.1
	local max = 0.9
	return math.random() * (max - min) + min;
end

while true do
	local r = randomColor()
	local g = randomColor()
	local b = randomColor()
	maincamera.backgroundColor = factory.color(r, g, b, 1)
	wait(0.1)
end
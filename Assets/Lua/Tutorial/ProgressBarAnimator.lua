function random_float(min, max)
	return (math.random() * max - min) + min
end

installs = {
	"Installing Devices",
	"Installing Network",
	"Copying Files",
	"Completing installation...",
	"Installing Start menu items",
	"Registering components",
	"Saving settings",
	"Removing any temporary files used"
}
local installState = 0

math.randomseed( os.time() )

while true do

	-- Increments install text state
	installState = installState + 1
	if installState == 9 then installState = 1 end

	-- Updates text
	progresstext.text = installs[installState]

	-- Resets fill amount
	progress.fillAmount = 0

	-- Makes bar go up
	while progress.fillAmount < 0.99 do
		local increment = random_float(0.001, 0.005)
		local wait_time = random_float(0.01, 0.05)
		progress.fillAmount = progress.fillAmount + increment
		wait(wait_time)
	end

end
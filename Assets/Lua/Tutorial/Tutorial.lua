-- Updates the bullet point images
function UpdateBulletPoints()
	-- Gets state
	local state = getvar(flowchart, "state").value

	-- Clamp function
	function Clamp(value, min, max)
		if value < min then
			 return min
		elseif value > max then
			return max
		else
			return value
		end
	end

	-- Updates bullet points
	bulletpointcontrols		.SetBulletPointImage(Clamp(state + 1, 0, 2))
	bulletpointenemies		.SetBulletPointImage(Clamp(state + 0, 0, 2))
	bulletpointaimofgame	.SetBulletPointImage(Clamp(state - 1, 0, 2))
	bulletpointmiscellaneous.SetBulletPointImage(Clamp(state - 2, 0, 2))
end

-- Moves on to the next state
function NextState()
	getvar(flowchart, "state").value = getvar(flowchart, "state").value + 1
end

-- Resets Yoshi's position
function ResetYoshi()
	runblock(flowchart, "ResetYoshi", 0, true)
end

function Controls1()
	wait(4)
	message.text = "There are two sets of controls: the modern controls and the classic controls. Pick whichever feels comfortable!"
	wait(4)
	message.text = "Use the WASD keys (modern) or the arrow keys and left shift button (classic) to jump and move around."
	wait(4)
	message.text = "Move onto that dark platform. Remember: WASD or arrow keys + left shift button."
	ResetYoshi()
end

function Controls2()
	message.text = "Well done!"
	wait(2)
	message.text = "If you hold down the jump button (W or left shift), you can jump higher."
	wait(4)
	message.text = "Try scaling this tower by holding down the jump button (W or left shift)."
	ResetYoshi()
end

function Enemies()
	NextState()
	UpdateBulletPoints()
	message.text = "Nice!"
	wait(2)
	message.text = "To go through a level, you'll need to eat all the enemies."
	wait(4)
	message.text = "When you touch an enemy, you'll lose health, as depicted by the white number at the top left."
	wait(4)
	message.text = "When your health reaches 0, it's game over!"
	wait(4)
	message.text = "To eat enemies, walk up to them and stick your tongue out at them with space (modern) or left CTRL (classic)."
	wait(4)
	message.text = "You can see the number of enemies remaining by the red text at the top right."
	wait(4)
	message.text = "Eat this enemy with space or left CTRL to proceed."
	ResetYoshi()
end

function AimOfGame()
	NextState()
	UpdateBulletPoints()
	message.text = "Great job!"
	wait(2)
	message.text = "The aim of the game is to find and defeat all of the enemies in the room you're in."
	wait(4)
	message.text = "Once all of the enemies are defeated, a passageway to the next room will become available to you."
	wait(4)
	message.text = "Typically, the passageway is blocked by question mark blocks, which disappear when all the enemies are eaten."
	wait(4)
	message.text = "Defeat all of the enemies and then touch the computer to progress."
	ResetYoshi()
end

function Miscellaneous()
	NextState()
	UpdateBulletPoints()
	message.text = "The game auto saves every time you complete a level, so don't worry about closing the game."
	wait(4)
	message.text = "Additionally, you can pause by pressing enter or the pause key (if you have one)."
	wait(4)
	NextState()
	UpdateBulletPoints()
	message.text = "You have now completed the tutorial. Congratulations!"
	wait(4)
	message.text = "Good luck, and have fun playing Yoshi vs Windows: Unity Edition!"
	wait(4)
end

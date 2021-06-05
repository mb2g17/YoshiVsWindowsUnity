local phase = {}

-- Function that executes a timeline with an exit predicate
 function phase.begin(timeline, exit_predicate)
	local loop = true
	while loop do
		for i=1,#timeline do
			if exit_predicate() then
				loop = false
			else
				timeline[i]()
			end
		end
	end
end

return phase